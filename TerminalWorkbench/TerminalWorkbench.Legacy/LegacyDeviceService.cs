using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DtuTestManager;
using TerminalWorkbench.Core;

namespace TerminalWorkbench.Legacy
{
    // 过渡阶段仅此适配器接触旧程序的全局状态和窗口消息。
    public sealed class LegacyDeviceService : IDeviceService
    {
        private readonly SemaphoreSlim operation = new SemaphoreSlim(1, 1);
        private readonly string originalDirectory = Environment.CurrentDirectory;
        private DeviceSnapshot snapshot = DeviceSnapshot.Empty;
        private bool configured;
        private bool channelOpen;
        private IntPtr windowHandle;

        public event EventHandler<DeviceNoticeEventArgs> Notice;

        public string RuntimeDirectory { get; private set; }

        public void AttachWindow(IntPtr handle)
        {
            windowHandle = handle;
            if (configured)
            {
                CGlobal.m_Device.m_MainFormHandle = handle.ToInt32();
            }
        }

        public async Task LoadAsync(string configurationFile)
        {
            await operation.WaitAsync().ConfigureAwait(false);
            try
            {
                if (channelOpen)
                {
                    throw new InvalidOperationException("请先断开连接，再载入工程。");
                }
                await Task.Run(() => LoadRuntime(configurationFile)).ConfigureAwait(false);
            }
            finally
            {
                operation.Release();
            }
        }

        private void LoadRuntime(string configurationFile)
        {
            if (!File.Exists(configurationFile))
            {
                throw new FileNotFoundException("找不到所选通信配置文件。", configurationFile);
            }

            CloseTemplate();
            configured = false;
            snapshot = DeviceSnapshot.Empty;
            RuntimeDirectory = Path.GetDirectoryName(Path.GetFullPath(configurationFile));
            Environment.CurrentDirectory = RuntimeDirectory;
            CGlobal.FileName = Path.GetFullPath(configurationFile);
            CGlobal.m_Device = new CDeviceClass();
            CGlobal.m_ModuleList.Clear();
            CGlobal.ReadSysSetting();
            CGlobal.m_RunSendMode = false;

            foreach (string folder in new[] { "DefaultDevModule", "DevModuleDataBase", "工程模板" })
            {
                string directory = Path.Combine(RuntimeDirectory, folder);
                if (!Directory.Exists(directory))
                {
                    continue;
                }
                foreach (string file in Directory.GetFiles(directory, "*.mdb"))
                {
                    CModuleClass module = new CModuleClass();
                    module.m_ModuleName = Path.GetFileNameWithoutExtension(file);
                    module.m_ModuleDB.m_Path = file;
                    if (!module.m_ModuleDB.ConnectDataBase())
                    {
                        throw new InvalidOperationException("无法打开模板数据库，请确认已安装匹配的 32 位 Jet OLE DB 组件。");
                    }
                    try
                    {
                        module.InitialzeReadOption();
                        CGlobal.m_ModuleList.Add(module);
                    }
                    finally
                    {
                        module.m_ModuleDB.CloseDatabase();
                    }
                }
            }

            if (CGlobal.m_ModuleList.Count == 0)
            {
                throw new InvalidOperationException("运行目录中没有找到终端模板，请选择包含 DefaultDevModule 等模板目录的配置文件。");
            }
            CDeviceClass device = CGlobal.m_Device;
            device.m_DeviceMsg = CGlobal.m_ModuleList.FirstOrDefault(module =>
                module.m_ModuleName == device.m_Connection.m_DevName) ?? CGlobal.m_ModuleList[0];
            if (!device.m_DeviceMsg.m_ModuleDB.ConnectDataBase())
            {
                throw new InvalidOperationException("无法打开当前终端模板。");
            }
            try
            {
                device.InitialzeSetDeviceModuleOption();
                device.InitializationReadDataList();
                device.m_MainFormHandle = windowHandle.ToInt32();
                configured = true;
                snapshot = Capture();
            }
            catch
            {
                device.m_DeviceMsg.m_ModuleDB.CloseDatabase();
                throw;
            }
        }

        public async Task ConnectAsync()
        {
            await operation.WaitAsync().ConfigureAwait(false);
            try
            {
                if (!configured)
                {
                    throw new InvalidOperationException("请先载入工程。");
                }
                if (channelOpen)
                {
                    return;
                }
                await Task.Run(Connect).ConfigureAwait(false);
            }
            finally
            {
                operation.Release();
            }
        }

        private void Connect()
        {
            CDeviceClass device = CGlobal.m_Device;
            CGlobal.m_RunSendMode = true;
            if (!device.OpenConnChannel())
            {
                throw new InvalidOperationException("连接未建立，请检查通信设置与终端状态。");
            }
            try
            {
                if (device.m_Connection.IsEncode)
                {
                    CGlobal.m_SecretEncode.CreateSecretDevice();
                    if (!CGlobal.m_SecretEncode.ConnectEncodeDev())
                    {
                        throw new InvalidOperationException("加密设备连接失败。");
                    }
                }
                device.IniCreateProcessThread();
                if (device.m_Connection.m_SocketMode == 0)
                {
                    CGlobal.SendTaskOrder(device.m_Connection.IsEncode ?
                        (int)EncodeStatus.Auto_Master_Indentity : 1);
                }
                channelOpen = true;
            }
            catch
            {
                device.CloseThread();
                CGlobal.m_SecretEncode.CloseEncode();
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            await operation.WaitAsync().ConfigureAwait(false);
            try
            {
                if (!channelOpen)
                {
                    return;
                }
                await Task.Run(() =>
                {
                    CGlobal.m_Device.CloseThread();
                    CGlobal.m_SecretEncode.CloseEncode();
                    CGlobal.m_RunSendMode = false;
                    channelOpen = false;
                }).ConfigureAwait(false);
            }
            finally
            {
                operation.Release();
            }
        }

        public DeviceSnapshot Refresh()
        {
            if (!configured)
            {
                return DeviceSnapshot.Empty;
            }
            CDeviceClass device = CGlobal.m_Device;
            bool changed = device.GetYcFreshStatus();
            changed |= device.GetYxFreshStatus();
            changed |= device.GetPwFreshStatus();
            if (changed || snapshot.IsChannelOpen != channelOpen || snapshot.IsConnected != device.m_bOnlineStatus)
            {
                snapshot = Capture();
            }
            return snapshot;
        }

        private DeviceSnapshot Capture()
        {
            CDeviceClass device = CGlobal.m_Device;
            SystemConnection connection = device.m_Connection;
            string transport = connection.m_nConnType == 0 ?
                "COM" + connection.m_strutComOption.m_nCom_No :
                (connection.m_SocketMode == 0 ? "TCP 客户端 " : "TCP 监听 ") +
                connection.m_strIP_Address + ":" + connection.m_nIP_Port;
            return new DeviceSnapshot(true, channelOpen, device.m_bOnlineStatus,
                device.m_DeviceMsg.m_ModuleName,
                connection.m_n_ProType == 0 ? "IEC 60870-5-101" : "IEC 60870-5-104", transport,
                device.m_YcList.Select(point => new TerminalPoint(point.m_YcNo, point.m_YcAddress,
                    point.m_YcName, point.m_YcValue.ToString("F4"), point.m_YcUnit, null)).ToArray(),
                device.m_YxList.Select(point => new TerminalPoint(point.m_YxNo, point.m_YxAddress,
                    point.m_YxName, point.m_YxValue.ToString(), null, point.m_YxChangeTime)).ToArray(),
                device.m_PowerList.Select(point => new TerminalPoint(point.m_PowerNo, point.m_PowerAddress,
                    point.m_PowerName, point.m_PowerValue.ToString(), point.m_PowerUnit, null)).ToArray());
        }

        // 由宿主的 HWND 消息钩子在界面线程调用。
        public bool ProcessWindowMessage(int message)
        {
            string text;
            bool disconnect = false;
            switch (message)
            {
                case UseMessage.MY_MSG_2:
                    text = "收到终端事件。";
                    break;
                case UseMessage.MY_MSG_3:
                    text = "收到 SOE 变位。";
                    break;
                case UseMessage.MY_MSG_5:
                    text = "终端连接超时，请断开后重新连接。";
                    disconnect = true;
                    break;
                case UseMessage.MY_MSG_6:
                    text = "终端应答超时。";
                    break;
                default:
                    return false;
            }
            Notice?.Invoke(this, new DeviceNoticeEventArgs(text, disconnect));
            return true;
        }

        private void CloseTemplate()
        {
            if (configured)
            {
                CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.CloseDatabase();
            }
        }

        public void Dispose()
        {
            CloseTemplate();
            Environment.CurrentDirectory = originalDirectory;
            operation.Dispose();
        }
    }
}
