using System;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public partial class MainForm
    {
        private Task connectionOperation = Task.CompletedTask;
        private bool channelActionActive;
        private bool closeRequested;
        private bool shutdownComplete;

        private async void Btu_RunStart_Click(object sender, EventArgs e)
        {
            if (channelActionActive || closeRequested)
                return;
            connectionOperation = ChangeConnectionAsync(true);
            await connectionOperation;
        }

        private async void Btn_StopRun_Click(object sender, EventArgs e)
        {
            if (channelActionActive || closeRequested)
                return;
            connectionOperation = ChangeConnectionAsync(false);
            await connectionOperation;
        }

        private async Task ChangeConnectionAsync(bool connect)
        {
            channelActionActive = true;
            myPanelForm.Enabled = false;
            menuStrip1.Enabled = false;
            toolStrip1.Enabled = false;
            Btu_RunStart.Enabled = false;
            Btn_StopRun.Enabled = false;
            Btn_ConSet.Enabled = false;
            try
            {
                if (connect)
                {
                    CGlobal.m_RunSendMode = true;
                    await Task.Run(OpenChannel);
                    FreshTimer.Start();
                    timer1.Start();
                }
                else
                {
                    FreshTimer.Stop();
                    timer1.Stop();
                    await Task.Run(CloseChannel);
                }
            }
            catch (Exception error)
            {
                await Task.Run(CloseChannel);
                MessageBox.Show("通信操作失败：" + error.Message, "通信状态", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                channelActionActive = false;
                myPanelForm.Enabled = !closeRequested;
                menuStrip1.Enabled = !closeRequested;
                toolStrip1.Enabled = !closeRequested;
                SetAndRunButton(CGlobal.m_Device.m_bOnlineStatus);
            }
        }

        private static void OpenChannel()
        {
            if (CGlobal.m_Device.m_bOnlineStatus)
                return;
            CGlobal.m_Device.CloseThread();
            if (!CGlobal.m_Device.OpenConnChannel())
                return;
            if (CGlobal.m_Device.m_Connection.IsEncode)
            {
                CGlobal.m_SecretEncode.CreateSecretDevice();
                if (!CGlobal.m_SecretEncode.ConnectEncodeDev())
                {
                    CloseChannel();
                    return;
                }
            }
            CGlobal.m_Device.IniCreateProcessThread();
            if (CGlobal.m_Device.m_Connection.m_SocketMode == 0 && CGlobal.m_RunSendMode)
            {
                CGlobal.SendTaskOrder(CGlobal.m_Device.m_Connection.IsEncode ? (int)EncodeStatus.Auto_Master_Indentity : 1);
            }
        }

        private static void CloseChannel()
        {
            CGlobal.m_RunSendMode = false;
            CGlobal.m_Device.m_bOnlineStatus = false;
            CGlobal.m_Device.CloseThread();
            CGlobal.m_SecretEncode.CloseEncode();
        }

        private async void FreshTimer_Tick(object sender, EventArgs e)
        {
            if (CGlobal.IsAutoCon == 1 && !CGlobal.m_Device.m_bOnlineStatus && !channelActionActive && !closeRequested)
            {
                connectionOperation = ChangeConnectionAsync(true);
                await connectionOperation;
            }
            if (!channelActionActive)
                SetAndRunButton(CGlobal.m_Device.m_bOnlineStatus);
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (shutdownComplete)
                return;
            e.Cancel = true;
            if (closeRequested)
                return;
            closeRequested = true;
            FreshTimer.Stop();
            timer1.Stop();
            await connectionOperation;
            if (pLoadForm is CHisRecordForm history)
                await history.StopReadingAsync();
            await Task.Run(CloseChannel);
            foreach (CModuleClass module in CGlobal.m_ModuleList)
                module.m_ModuleDB.CloseDatabase();
            CGlobal.m_Device.m_DeviceMsg?.m_ModuleDB.CloseDatabase();
            CGlobal.m_BufferView = null;
            CGlobal.m_EventView = null;
            CGlobal.m_SOEView = null;
            shutdownComplete = true;
            Close();
        }
    }
}
