using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using DtuTestManager;
using TerminalWorkbench;
using TerminalWorkbench.Presentation;
using W = System.Windows.Controls;

internal static class WorkflowRegression
{
    private static int failures;
    private static int passed;

    internal static int Run()
    {
        var app = new System.Windows.Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };
        SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(app.Dispatcher));
        app.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/TerminalWorkbench;component/Presentation/Styles.xaml")
        });
        TerminalWorkbench.Presentation.MessageBox.DialogHandler = (_, _) => throw new InvalidOperationException("Unexpected workflow dialog.");
        string originalDirectory = Environment.CurrentDirectory;
        string workspace = Path.Combine(Path.GetTempPath(), "TerminalWorkbench-workflow-" + Guid.NewGuid().ToString("N"));
        string ini = DefaultWorkspace.EnsureCreated(workspace);
        RuntimePaths.Configure(ini);
        Console.WriteLine("Temporary workflow workspace: " + workspace);
        try
        {
            Check("bundled workspace starts without an external configuration", BundledWorkspace);
            Check("INI write/reopen preserves 101 cause length", IniRoundTrip);
            Check("real main startup, repeated navigation and shutdown", MainWorkflow);
            Check("responsive layouts and rendered previews", LayoutRegression.Run);
            Check("independent dialogs load and close", DialogWorkflow);
            Check("communication selection and native value editing", CommunicationEditor);
            Check("engineering string parameters retain commas", () => EngineeringParameters(false));
            Check("editing a parameter subset preserves all entries", () => EngineeringParameters(true));
            Check("SFTP connection keeps the dispatcher responsive", SftpConnection);
            Check("worker replies do not block UI shutdown", WorkerReply);
            Check("processing shutdown waits for its worker", ProcessingShutdown);
            Check("104 client receives fragmented/coalesced telemetry", Client104);
            Check("104 listener receives telemetry and releases its port", Listener104);
            Check("101 TCP transport receives fragmented telemetry", Client101);
            Check("TCP client reconnect leaves no old workers", Reconnect);
            Check("TCP send transmits the complete requested buffer", CompleteSend);
        }
        finally
        {
            CGlobal.m_Device.CloseThread();
            foreach (CModuleClass module in CGlobal.m_ModuleList)
            {
                module.m_ModuleDB.CloseDatabase();
            }
            Environment.CurrentDirectory = originalDirectory;
            app.Shutdown();
        }
        Console.WriteLine($"Workflow passed: {passed}; failures: {failures}");
        return failures == 0 ? 0 : 1;
    }

    private static void Check(string name, Action action)
    {
        try
        {
            action();
            passed++;
            Console.WriteLine("PASS workflow: " + name);
        }
        catch (Exception error)
        {
            failures++;
            Console.WriteLine("FAIL workflow: " + name + ": " + error);
        }
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private static T Field<T>(object target, string name)
    {
        return (T)target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(target);
    }

    private static void Wait(Func<bool> condition, string message, int timeout = 5000)
    {
        var elapsed = Stopwatch.StartNew();
        while (!condition())
        {
            Assert(elapsed.ElapsedMilliseconds < timeout, message);
            Pump();
            Thread.Sleep(2);
        }
        Pump();
    }

    private static void Pump()
    {
        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.ContextIdle, new Action(() => { }));
    }

    private static void Complete(Task task)
    {
        Wait(() => task.IsCompleted, "Background operation did not complete.", 10000);
        task.GetAwaiter().GetResult();
    }

    private static HwndSource Attach(FrameworkElement view)
    {
        var host = new HwndSource(new HwndSourceParameters("TerminalWorkbench workflow renderer")
        {
            Width = 1440,
            Height = 900,
            PositionX = -32000,
            PositionY = -32000,
            WindowStyle = unchecked((int)0x90000000)
        });
        host.RootVisual = view;
        view.Measure(new Size(1440, 900));
        view.Arrange(new Rect(0, 0, 1440, 900));
        view.UpdateLayout();
        Pump();
        return host;
    }

    private static void BundledWorkspace()
    {
        string workspace = RuntimePaths.WorkspaceDirectory;
        foreach (string path in new[] { "PrjSet_Main.ini", "DataBase.mdb", "res/Log.png", "DefaultDevModule/DTU标准模板.mdb", "DefaultDevModule/空模板.mdb" })
        {
            Assert(File.Exists(Path.Combine(workspace, path)), "Missing bundled asset: " + path);
        }
        Assert(Directory.GetFiles(Path.Combine(workspace, "DevModuleDataBase"), "*.mdb").Length == 3, "Device templates are missing.");
        CGlobal.m_Device = new CDeviceClass();
        CGlobal.ReadSysSetting();
        Assert(CGlobal.IsAutoCon == 0 && CGlobal.IsAutoCall == 0 && !CGlobal.m_Device.m_Connection.IsEncode, "First launch enabled automatic device operations.");
        Assert(CGlobal.m_Device.m_Connection.m_DevName == "DTU标准模板", "The default template name was not read correctly.");
        CGlobal.WritePrivateProfileString("ComType", "SingleType", "1", CGlobal.FileName);
        DateTime templateWriteTime = File.GetLastWriteTimeUtc(Path.Combine(workspace, "DefaultDevModule", "DTU标准模板.mdb"));
        DefaultWorkspace.EnsureCreated(workspace);
        CGlobal.ReadSysSetting();
        Assert(CGlobal.m_Device.m_Connection.m_SingleType == 1, "Relaunch overwrote saved communication settings.");
        Assert(File.GetLastWriteTimeUtc(Path.Combine(workspace, "DefaultDevModule", "DTU标准模板.mdb")) == templateWriteTime, "Relaunch rewrote the user's template.");
    }

    private static void IniRoundTrip()
    {
        CGlobal.m_Device = new CDeviceClass();
        CGlobal.ReadSysSetting();
        CGlobal.m_Device.m_Connection.m_101Address.m_CotType = 0;
        CGlobal.m_Device.m_Connection.m_101Address.m_AddressType = 0;
        CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress = 17;
        CGlobal.WriteComPara();
        CGlobal.m_Device.m_Connection = new SystemConnection();
        CGlobal.ReadSysSetting();
        Assert(CGlobal.m_Device.m_Connection.m_101Address.m_CotType == 0, "The saved one-byte cause reverted on reopen.");
        Assert(CGlobal.m_Device.m_Connection.m_101Address.m_AddressType == 0 && CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress == 17, "The saved 101 address changed on reopen.");
    }

    private static void MainWorkflow()
    {
        CGlobal.m_Device = new CDeviceClass();
        CGlobal.m_RunSendMode = false;
        using var main = new MainForm();
        using var host = Attach(main.Element);
        main.ActivateScreen();
        Assert(CGlobal.m_Device.m_YcList.Count == 400 && CGlobal.m_Device.m_YxList.Count == 930, "Product startup did not load the standard template.");
        Assert(!CGlobal.m_Device.m_bOnlineStatus && !CGlobal.m_RunSendMode, "Startup connected without the start action.");
        string[] actions = { "Btn_LongCtrl", "Btn_ParaSet", "Btn_EngPara", "Btn_HisRecord", "Btn_Encode", "Btn_CommModeSet1", "Btn_DataCheck1", "Btn_Function1", "BtnDataTran1", "Btn_ProExp1", "Btn_RealData" };
        var durations = new List<double>();
        for (int round = 0; round < 3; round++)
        {
            foreach (string action in actions)
            {
                Form previous = Field<Form>(main, "pLoadForm");
                var elapsed = Stopwatch.StartNew();
                Field<Control>(main, action).PerformClick();
                main.Element.UpdateLayout();
                Pump();
                durations.Add(elapsed.Elapsed.TotalMilliseconds);
                Assert(previous.IsDisposed, "Navigation retained its previous page: " + action);
                Form current = Field<Form>(main, "pLoadForm");
                Assert(current != null && !current.IsDisposed, "Navigation lost its current page: " + action);
            }
        }
        Console.WriteLine($"Navigation samples: {durations.Count}; median={durations.Order().ElementAt(durations.Count / 2):F1}ms; maximum={durations.Max():F1}ms (includes layout).");
        Assert(CGlobal.m_4GList.Count == 6 && CGlobal.m_HostList.Count == 10, "Repeated navigation accumulated duplicate communication parameters.");
        var currentRealtime = Field<Form>(main, "pLoadForm");
        var table = Field<MyListView>(currentRealtime, "LV_YC");
        Assert(((W.DataGrid)table.Element).Items.Count == 400, "Repeated navigation lost analog points.");
        main.Close();
        Wait(() => main.IsDisposed, "The main window did not finish asynchronous shutdown.");
        Assert(currentRealtime.IsDisposed, "Main shutdown retained its current page.");
    }

    private static void DialogWorkflow()
    {
        CGlobal.strDevModulList = new[] { "DTU", "FTU" };
        Type[] types = { typeof(CAddCom), typeof(CAddGateWay), typeof(CAddHost), typeof(CAddIPForm), typeof(CAddTermForm), typeof(CDownFileForm), typeof(CFileTimeForm), typeof(CFixDataForm), typeof(CFTUFastUpdate), typeof(CGuHuaForm), typeof(CVersionForm), typeof(CWaitForm), typeof(CUpdataForm), typeof(CSFTPLogin) };
        foreach (Type type in types)
        {
            using var form = (Form)(type.GetConstructor(Type.EmptyTypes) != null
                ? Activator.CreateInstance(type)
                : Activator.CreateInstance(type, new object[] { "Workflow test" }));
            using var host = Attach(form.Element);
            form.ActivateScreen();
            Pump();
            form.Close();
            Wait(() => form.IsDisposed, "Dialog did not close: " + type.Name);
            Console.WriteLine("Loaded and closed dialog: " + type.Name);
        }
    }

    private static void CommunicationEditor()
    {
        using var form = new CommuCFG();
        form.ActivateScreen();
        var editor = Field<DataGridView>(form, "Set4GGrid");
        var source = (DataTable)editor.DataSource;
        Assert(editor.Columns.Count == 3 && editor.Rows.Count == 6, "The communication selection column or rows are missing.");
        Assert(editor.Columns[0] is DataGridViewCheckBoxColumn, "The first column is no longer the selection column.");
        using var host = Attach(editor.Element);
        var grid = ((W.Grid)editor.Element).Children.OfType<W.DataGrid>().Single();
        string name = source.Rows[1][0].ToString();
        editor.Rows[1].Cells[0].Value = true;
        grid.CurrentCell = new W.DataGridCellInfo(editor.Rows[1], grid.Columns[2]);
        Assert(grid.BeginEdit(), "Native communication value editor did not start.");
        Pump();
        var text = grid.Columns[2].GetCellContent(editor.Rows[1]) as W.TextBox;
        Assert(text != null, "Native value editor did not create its text field.");
        text.Text = "workflow-value";
        Assert(editor.EndEdit(), "Native value editor did not commit.");
        Pump();
        Assert(source.Rows[1][0].ToString() == name && source.Rows[1][1].ToString() == "workflow-value", $"Editing with an unbound selection column changed the wrong data field: name={source.Rows[1][0]}, value={source.Rows[1][1]}.");
        Assert((bool)editor.Rows[1].Cells[0].Value && editor.Rows[1].Cells["值"].Value.ToString() == "workflow-value", "Editing lost the selected row or named-column mapping.");
        form.Close();
    }

    private static void EngineeringParameters(bool subset)
    {
        CGlobal.m_Device.m_TotalParaDataList = new List<CParaCtrl>
        {
            new CParaCtrl { m_ParaAddress = 0, m_ParaType = 32, m_ParaName = "Byte", m_ParaValue = 12 },
            new CParaCtrl { m_ParaAddress = 1, m_ParaType = 4, m_ParaName = "Text", m_ParaValue = "初始" },
            new CParaCtrl { m_ParaAddress = 2, m_ParaType = 38, m_ParaName = "Float", m_ParaValue = 12.5F }
        };
        string path = Path.Combine(RuntimePaths.WorkspaceDirectory, "工程参数", subset ? "subset.ini" : "comma.ini");
        CGlobal.WritePrivateProfileString("ParaInfo", "ParaCnt", "3", path);
        CGlobal.WritePrivateProfileString("ParaInfo", "Para0", "0,12", path);
        CGlobal.WritePrivateProfileString("ParaInfo", "Para1", subset ? "1,初始" : "1,通信,备用", path);
        CGlobal.WritePrivateProfileString("ParaInfo", "Para2", "2,12.5", path);
        CEngPara.m_CurrentPath = path;
        using var form = new CEngPara();
        form.ActivateScreen();
        form.ReadFileAndDisPlay(path);
        if (subset)
        {
            CEngPara.m_SelectParaIndex.Clear();
            CEngPara.m_SelectParaIndex.Add(1);
            using var edit = new CEngModPara();
            edit.ActivateScreen();
            Field<MyListView>(edit, "ParaLV").Items[0].SubItems[4].Text = "修改完成";
            Field<Control>(edit, "BtnSave").PerformClick();
            edit.Close();
            form.ReadFileAndDisPlay(path);
            Assert(CEngPara.m_CurrentParaDataList.Count == 3, "Editing one parameter hid the other saved parameters on reopen.");
            Assert(CEngPara.m_CurrentParaDataList[1].m_ParaValue.ToString() == "修改完成", "The selected parameter edit did not persist.");
        }
        else
        {
            Assert(CEngPara.m_CurrentParaDataList[1].m_ParaValue.ToString() == "通信,备用", "The saved string was truncated at its first comma.");
            Field<Control>(form, "BtnSavePara").PerformClick();
            form.ReadFileAndDisPlay(path);
            Assert(CEngPara.m_CurrentParaDataList[1].m_ParaValue.ToString() == "通信,备用", "The string changed after saving and reopening.");
            string longValue = new string('参', 180) + ",保存后完整重读";
            Field<ListView>(form, "ParaLV").Items[1].SubItems[5].Text = longValue;
            Field<Control>(form, "BtnSavePara").PerformClick();
            form.ReadFileAndDisPlay(path);
            Assert(CEngPara.m_CurrentParaDataList[1].m_ParaValue.ToString() == longValue, "A long string parameter was truncated by the INI read buffer.");
        }
        form.Close();
    }

    private static void SftpConnection()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        using var form = new CSFTPLogin();
        form.ActivateScreen();
        Field<TextBox>(form, "textBoxIP").Text = "127.0.0.1";
        Field<TextBox>(form, "textBoxPortNum").Text = ((IPEndPoint)listener.LocalEndpoint).Port.ToString();
        Field<TextBox>(form, "textBoxUserName").Text = "workflow";
        Field<TextBox>(form, "textBoxPassWord").Text = "synthetic-test";
        Task accept = Task.Run(async () =>
        {
            using TcpClient peer = await listener.AcceptTcpClientAsync();
            await Task.Delay(400);
        });
        var elapsed = Stopwatch.StartNew();
        Field<Control>(form, "button3").PerformClick();
        Assert(elapsed.ElapsedMilliseconds < 200, "The SFTP button blocked the UI while waiting for its server.");
        bool dispatched = false;
        System.Windows.Application.Current.Dispatcher.BeginInvoke(() => dispatched = true);
        Pump();
        Assert(dispatched, "The UI stopped dispatching during SFTP connection.");
        form.Close();
        Complete(accept);
        Wait(() => form.IsDisposed, "Closing the SFTP login did not finish after the connection operation.");
    }

    private sealed class ReplyForm : Form
    {
        internal int Replies;
        protected override void WndProc(ref Message message)
        {
            Replies++;
        }
    }

    private static void WorkerReply()
    {
        using var form = new ReplyForm();
        var worker = new Thread(() => CGlobal.SendMessage(form.Handle.ToInt32(), UseMessage.MY_MSG_1, 0, 0));
        worker.Start();
        Assert(worker.Join(1000), "A worker waited for the UI while shutdown was waiting for the worker.");
        Pump();
        Assert(form.Replies == 1, "The worker reply was not delivered.");
    }

    private sealed class DelayedProtocol : CPtrInterface
    {
        internal readonly ManualResetEventSlim Entered = new ManualResetEventSlim();
        internal readonly ManualResetEventSlim Release = new ManualResetEventSlim();
        public override void Process(byte[] received, ref int receivedLength, byte[] response, ref int responseLength)
        {
            Entered.Set();
            Release.Wait(3000);
        }
    }

    private static void ProcessingShutdown()
    {
        var device = new CDeviceClass { m_ReceiveFlag = true };
        var protocol = new DelayedProtocol { theDevice = device };
        device.m_Ptr = protocol;
        device.IniCreateProcessThread();
        Thread worker = Field<Thread>(device, "m_ProcessThread");
        Assert(protocol.Entered.Wait(2000), "Processing worker did not start.");
        Task closing = Task.Run(device.CloseProcessThread);
        try
        {
            Thread.Sleep(650);
            Assert(!closing.IsCompleted, "Shutdown returned while its processing worker was still active.");
        }
        finally
        {
            protocol.Release.Set();
            Complete(closing);
            Assert(worker.Join(2000), "Processing worker survived shutdown.");
        }
    }

    private static CDeviceClass Device(int protocol)
    {
        var device = new CDeviceClass();
        device.m_Connection = new SystemConnection
        {
            m_nConnType = 1,
            m_n_ProType = protocol,
            m_strIP_Address = "127.0.0.1",
            m_104Address = new Ptr104Address { m_AsduAddress = 1, m_YcStartAddress = 0x4001, m_YxStartAddress = 1, m_YkSrartAddress = 0x6001 },
            m_101Address = new Ptr101Address { m_AsduAddress = 1, m_YcStartAddress = 0x4001, m_YxStartAddress = 1, m_YkSrartAddress = 0x6001, m_AddressType = 1, m_CotType = 1 }
        };
        device.m_YcList.Add(new CYcOption { m_YcNo = 1, m_YcAddress = 0x4001, m_YcRatio = 1, m_YcName = "Simulator voltage", m_YcUnit = "V" });
        device.m_YxList.Add(new CYxOption { m_YxNo = 1, m_YxAddress = 1, m_YxName = "Simulator contact" });
        device.InitConnect();
        CGlobal.m_Device = device;
        CGlobal.m_RunSendMode = false;
        return device;
    }

    private static byte[] IFrame(byte type, int address, byte[] value, int sequence = 0)
    {
        return new byte[] { 0x68, (byte)(13 + value.Length), (byte)(sequence * 2), 0, 0, 0, type, 1, 3, 0, 1, 0, (byte)address, (byte)(address >> 8), 0 }.Concat(value).ToArray();
    }

    private static byte[] Analog(float value, int sequence = 0)
    {
        return IFrame(13, 0x4001, BitConverter.GetBytes(value).Concat(new byte[] { 0 }).ToArray(), sequence);
    }

    private static void Send(NetworkStream stream, byte[] data, bool fragment = false)
    {
        if (fragment)
        {
            stream.Write(data, 0, 1);
            Thread.Sleep(20);
            stream.Write(data, 1, 2);
            Thread.Sleep(20);
            stream.Write(data, 3, data.Length - 3);
        }
        else
        {
            stream.Write(data);
        }
    }

    private static byte[] Receive104(NetworkStream stream)
    {
        stream.ReadTimeout = 3000;
        byte[] header = new byte[2];
        stream.ReadExactly(header);
        Assert(header[0] == 0x68, "Invalid simulator response header.");
        byte[] frame = new byte[header[1] + 2];
        header.CopyTo(frame, 0);
        stream.ReadExactly(frame.AsSpan(2));
        return frame;
    }

    private static void Client104()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var device = Device(1);
        device.m_Connection.m_nIP_Port = ((IPEndPoint)listener.LocalEndpoint).Port;
        try
        {
            Assert(device.OpenConnChannel(), "104 client did not connect to the local simulator.");
            using var peer = listener.AcceptTcpClient();
            using var stream = peer.GetStream();
            device.IniCreateProcessThread();
            CGlobal.SendTaskOrder(1);
            Assert(Receive104(stream)[2] == 7, "Start action did not reach the simulator.");
            Send(stream, new byte[] { 0x68, 4, 0x0B, 0, 0, 0 }, true);
            Send(stream, Analog(231.25F), true);
            Wait(() => device.m_YcList[0].m_YcValue == 231.25F, "Fragmented analog frame did not reach the model.");
            Send(stream, IFrame(1, 1, new byte[] { 1 }, 1).Concat(Analog(232.5F, 2)).ToArray());
            Wait(() => device.m_YxList[0].m_YxValue == 1 && device.m_YcList[0].m_YcValue == 232.5F, "Coalesced frames were lost.");
            using var page = new CRealCtrlForm();
            page.ActivateScreen();
            Assert(Field<MyListView>(page, "LV_YC").Items[0].SubItems[2].Text.Contains("232.5"), "Received analog value did not reach the page.");
            Send(stream, new byte[] { 0x68, 4, 0x43, 0, 0, 0 });
            byte[] response = Receive104(stream);
            Assert(response[2] == 0x83, "Test activation was not acknowledged.");
            peer.Close();
            Wait(() => !device.m_bOnlineStatus, "Peer disconnect did not change the online state.");
        }
        finally
        {
            device.CloseThread();
        }
    }

    private static void Listener104()
    {
        var device = Device(1);
        device.m_Connection.m_SocketMode = 1;
        device.m_Connection.m_nIP_Port = 0;
        try
        {
            Assert(device.OpenConnChannel(), "Listener did not start.");
            Socket socket = Field<Socket>(device.m_SocketDevice, "m_ListenSocket");
            int port = ((IPEndPoint)socket.LocalEndPoint).Port;
            using var peer = new TcpClient();
            peer.Connect(IPAddress.Loopback, port);
            Wait(() => Field<Thread>(device.m_SocketDevice, "RecvThread") != null, "Accepted peer did not start its reader.");
            device.IniCreateProcessThread();
            Send(peer.GetStream(), Analog(219.75F), true);
            Wait(() => device.m_YcList[0].m_YcValue == 219.75F, "Listener telemetry was lost.");
            Thread reader = Field<Thread>(device.m_SocketDevice, "RecvThread");
            Thread acceptor = Field<Thread>(device.m_SocketDevice, "AcceptThread");
            Complete(Task.Run(device.CloseThread));
            Assert(!reader.IsAlive && !acceptor.IsAlive, "Listener shutdown retained socket workers.");
            using var probe = new TcpListener(IPAddress.Loopback, port);
            probe.Start();
        }
        finally
        {
            device.CloseThread();
        }
    }

    private static byte[] Analog101(float value)
    {
        byte[] body = new byte[] { 8, 1, 0, 13, 1, 3, 0, 1, 0, 1, 0x40 }.Concat(BitConverter.GetBytes(value)).Concat(new byte[] { 0 }).ToArray();
        byte sum = unchecked((byte)body.Sum(item => item));
        return new byte[] { 0x68, (byte)body.Length, (byte)body.Length, 0x68 }.Concat(body).Concat(new byte[] { sum, 0x16 }).ToArray();
    }

    private static void Client101()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var device = Device(0);
        device.m_Connection.m_nIP_Port = ((IPEndPoint)listener.LocalEndpoint).Port;
        try
        {
            Assert(device.OpenConnChannel(), "101 TCP client did not connect.");
            using var peer = listener.AcceptTcpClient();
            device.IniCreateProcessThread();
            Send(peer.GetStream(), Analog101(12.5F), true);
            Wait(() => device.m_YcList[0].m_YcValue == 12.5F, "101 fragmented analog data was not decoded.");
        }
        finally
        {
            device.CloseThread();
        }
    }

    private static void Reconnect()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var device = Device(1);
        device.m_Connection.m_nIP_Port = ((IPEndPoint)listener.LocalEndpoint).Port;
        for (int round = 0; round < 5; round++)
        {
            try
            {
                Assert(device.OpenConnChannel(), "Reconnect failed: " + round);
                using var peer = listener.AcceptTcpClient();
                device.IniCreateProcessThread();
                Thread reader = Field<Thread>(device.m_SocketDevice, "RecvThread");
                Thread processor = Field<Thread>(device, "m_ProcessThread");
                Send(peer.GetStream(), Analog(100 + round));
                Wait(() => device.m_YcList[0].m_YcValue == 100 + round, "Reconnect received stale data.");
                Complete(Task.Run(device.CloseThread));
                Assert(!reader.IsAlive && !processor.IsAlive, "Reconnect retained an old worker.");
            }
            finally
            {
                device.CloseThread();
            }
        }
    }

    private static void CompleteSend()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        using var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(listener.LocalEndpoint);
        client.Blocking = false;
        client.SendBufferSize = 1024;
        using var peer = listener.AcceptTcpClient();
        using var stream = peer.GetStream();
        stream.ReadTimeout = 3000;
        var channel = new CSocketChannel();
        typeof(CSocketChannel).GetField("SocketClient", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(channel, client);
        byte[] data = Enumerable.Range(0, 1024 * 1024).Select(index => (byte)(index % 251)).ToArray();
        byte[] received = new byte[data.Length];
        Task reading = Task.Run(() => stream.ReadExactly(received));
        bool success = channel.SendToSocket(data, data.Length);
        Assert(success, "TCP send failed while its local peer was reading.");
        Complete(reading);
        Assert(data.SequenceEqual(received), "TCP send returned success without transmitting the complete buffer.");
    }
}
