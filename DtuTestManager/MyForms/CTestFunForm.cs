using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CTestFunForm : Form
    {
        private int[] m_ComBaund = new int[] { 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400,115200 };
        ComOption m_ComOption;
        CSerialPort m_SerialPort = new CSerialPort();
        Thread RecvThread = null;
        bool bThread = false;
        object m_Locker = new object();
        byte[] pSendBuff = new byte[30];
        public CTestFunForm()
        {
            InitializeComponent();
        }
        private void IniComBox()
        {
            int i;
            for (i = 1; i <= 10; i++)
            {
                ComBox_Com.Items.Add("串口" + i.ToString());
            }
            ComBox_Com.SelectedIndex = 0;

            foreach (int nBaund in m_ComBaund)
            {
                ComBox_Baund.Items.Add(nBaund);
            }
            ComBox_Baund.SelectedIndex = 8;

            ComBox_Check.Items.Add("无校验");
            ComBox_Check.Items.Add("奇校验");
            ComBox_Check.Items.Add("偶校验");
            ComBox_Check.SelectedIndex = 0;
        }
        public bool Ping(string strIP)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒  
            System.Net.NetworkInformation.PingReply reply = p.Send(strIP, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }  
        public bool TestPingTheIP(string strIP)
        {
            strIP = strIP.Trim();
            if (strIP == string.Empty) return false;
            try
            {
                // 是否 Ping 的通  
                if (Ping(strIP)) return true;
               
            }
            catch (Exception)
            {
               
            }
            return false;
        }  
        private void CTestFunForm_Load(object sender, EventArgs e)
        {
            IniComBox();
            /*
            SuperTerm.SelectionColor = Color.Black;
            SuperTerm.AppendText("测试内容\n");
            SuperTerm.SelectionColor = Color.Blue;
            SuperTerm.AppendText("测试内容\n");*/

        }

        private void BtnPingTest_Click(object sender, EventArgs e)
        {
            bool bIP1 = false, bIP2 = false;
            string strResult = "";

            this.Cursor = Cursors.WaitCursor;//等待
            bIP1 = TestPingTheIP(IPAddress1.Text);
            bIP2 = TestPingTheIP(IPAddress2.Text);
            if (bIP1)
            {
                strResult = IPAddress1.Text + "  测试正常\n";
                AddRichText(PingRichText,Color.Green, "---" + strResult);
            }
            else
            {
                strResult = IPAddress1.Text + " 不通\n";
                AddRichText(PingRichText,Color.Red, "---" + strResult);
            }

            if (bIP2)
            {
                strResult = IPAddress2.Text + " 测试正常\n";
                AddRichText(PingRichText,Color.Green, "---" + strResult);
            }
            else
            {
                strResult = IPAddress2.Text + " 不通\n";
                AddRichText(PingRichText,Color.Red, "---" + strResult);
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnComTest_Click(object sender, EventArgs e)
        {
            CSerialPort pSerialPort = new CSerialPort();
            ComOption pComOptoin;
            pComOptoin.m_nCom_No = ComBox_Com.SelectedIndex + 1;
            pComOptoin.m_nCom_Baund = m_ComBaund[ComBox_Baund.SelectedIndex];
            pComOptoin.m_nCom_DataBit = 8;
            pComOptoin.m_nCom_StopBit = 1;
            pComOptoin.m_nCom_CheckBit = ComBox_Check.SelectedIndex;

            this.Cursor = Cursors.WaitCursor;//等待
            if (pSerialPort.OpenComPort(pComOptoin) == false)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("串口打开失败!");
                return;
            }
            byte[] pBuff = new byte[6];
            pBuff[0] = 0x10;
            pBuff[1] = 0x49;
            pBuff[2] = 0x01;
            pBuff[3] = 0;
            pBuff[4] = 0x4A;
            pBuff[5] = 0x16;

            string strBuff = "";
            for (int i = 0; i < 6; i++)
            {
                strBuff += String.Format("{0:X2}", pBuff[i]);
                strBuff += " ";
            }
            AddRichText(SuperTerm,Color.Blue, "--->" + strBuff + "\n");


            pSerialPort.SendToComPort(pBuff, 6);
            byte[] pRecBuff = new byte[128];
            Thread.Sleep(100);
            int nRecvLeng = pSerialPort.ReadComPort(pRecBuff, 20, 128);

            strBuff = "";
            for (int i = 0; i < nRecvLeng; i++)
            {
                strBuff += String.Format("{0:X2}", pRecBuff[i]);
                strBuff += " ";
            }

            if (nRecvLeng > 0) AddRichText(SuperTerm,Color.Green, "<---" + strBuff + "\n");


            if (nRecvLeng >= 6 && pRecBuff[0] == 0x10)
            {
                AddRichText(SuperTerm,Color.Green, "---串口101通讯测试成功!\n");
            }
            else
            {
                AddRichText(SuperTerm,Color.Red, "---串口101通讯测试失败!\n");
            }
            this.Cursor = Cursors.Default;
            pSerialPort.CloseComPort();
        }

        private void BtnSelectBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog pDlg = new ColorDialog();
            if (pDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SuperTerm.BackColor = pDlg.Color;
            }
        }
        private void CloseThread()
        {
            if (bThread)
            {
                m_SerialPort.exitEvent.Set();
                bThread = false;
                Thread.Sleep(100);
               // RecvThread.Join();
            }
        }
        private void RtuRecThread()
        {
            bThread = true;
            int m_nRecLeng = 0;
            byte[] pNowRedBuff = new byte[10240];
            m_SerialPort.exitEvent.Reset();
            while (bThread)
            {
                if (m_SerialPort.exitEvent.WaitOne(10))
                {
                    break;
                }
                m_nRecLeng = 0;
                Array.Clear(pNowRedBuff,0,10240);
                m_nRecLeng = m_SerialPort.ReadComPort(pNowRedBuff, 1, 10240);
                if (m_nRecLeng > 0)
                {
                    
                    byte[] pNewBuff = new byte[m_nRecLeng];
                    Array.Copy(pNowRedBuff, 0, pNewBuff, 0, m_nRecLeng);
                    ShowBuffer(pNewBuff, m_nRecLeng);
                }
            }
        }
        private void BtnOpenSuperCom_Click(object sender, EventArgs e)
        {/*
            CloseThread();
            m_SerialPort.CloseComPort();*/
            if (m_SerialPort.m_OpenFlag == false)
            {
                m_ComOption.m_nCom_No = ComBox_Com.SelectedIndex + 1;
                m_ComOption.m_nCom_Baund = m_ComBaund[ComBox_Baund.SelectedIndex];
                m_ComOption.m_nCom_DataBit = 8;
                m_ComOption.m_nCom_StopBit = 1;
                m_ComOption.m_nCom_CheckBit = ComBox_Check.SelectedIndex;

                CloseThread();

                if (m_SerialPort.OpenComPort(m_ComOption) == false)
                {
                    AddRichText(SuperTerm, Color.Red, "---串口打开失败\n");
                    return;
                }
                BtnOpenSuperCom.Text = "关闭串口调试";
                RecvThread = new Thread(new ThreadStart(RtuRecThread));
                RecvThread.IsBackground = true;
                RecvThread.Start();
            }
            else
            {
                BtnOpenSuperCom.Text = "打开串口调试";
                CloseThread();
                m_SerialPort.CloseComPort();
            }
        }

        private void ClearAllText_Click(object sender, EventArgs e)
        {
            SuperTerm.Text = "";
            PingRichText.Text = "";
        }
        private void AddRichText(RichTextBox pTexBox,Color pColor, string strText)
        {
            pTexBox.SelectionColor = pColor;
            pTexBox.AppendText(strText + "\r\n");
            pTexBox.Focus();
        }
        public void ShowBuffer(Byte[] pBuffer, int nLeng)
        {
            string strOut = System.Text.Encoding.Default.GetString(pBuffer);
            SuperTerm.Invoke
            (
                  new MethodInvoker
                  (
                      delegate
                      {
                          AddRichText(SuperTerm,Color.Blue, "<--" + strOut);
                      }
                  )
            );
           
        }
        private void SendOrder(string strOrder)
        {
            if (m_SerialPort.m_OpenFlag == false)
            {
                MessageBox.Show("串口未打开!");
                return;
            }
            strOrder = strOrder.Replace("\n", "");
            strOrder = strOrder.Replace("\r", "");
            if (strOrder.Length == 0) return;
            AddRichText(SuperTerm, Color.Black, "-->" + strOrder + "\n");
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(strOrder);
            Array.Clear(pSendBuff, 0, pSendBuff.Length);
            Array.Copy(byteArray, 0, pSendBuff, 0, byteArray.Length);
            pSendBuff[byteArray.Length] = 0x0D;
            pSendBuff[byteArray.Length + 1] = 0x0A;
            m_SerialPort.SendToComPort(pSendBuff, byteArray.Length + 2);
        }

        private void BtnSendOrder_Click(object sender, EventArgs e)
        {
            string strValue = " ";
            SendOrder(strValue);
        }

        private void FreshTimer_Tick(object sender, EventArgs e)
        {
            FreshTimer.Stop();
        }

        private void BtnPreOrder_Click(object sender, EventArgs e)
        {
            string strValue = "run updateall";
            SendOrder(strValue);
        }

        private void BtnNextOrder_Click(object sender, EventArgs e)
        {
            string strValue = "run updatesys";
            SendOrder(strValue);
        }

        private void BtnClearPingMsg_Click(object sender, EventArgs e)
        {
            PingRichText.Text = "";
        }
    }
}
