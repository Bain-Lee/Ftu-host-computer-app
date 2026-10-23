using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace DtuTestManager
{
    public partial class CUpdataForm : Form
    {
        private List<CDownFileTaskMsg> m_TaskList = new List<CDownFileTaskMsg>();
        protected bool bUpdateFlag;                    //接收线程标志
        protected Thread UpdateThread;
        protected AutoResetEvent exitEvent;
        public static bool bNeedFlag = true;
        public CUpdataForm()
        {
            InitializeComponent();
            exitEvent = new AutoResetEvent(false);
            //    NeedCheck.Checked = true;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1:
                    UpResult.Text = "写文件激活失败";
                    break;
                case UseMessage.MY_MSG_2:
                    UpResult.Text = "写文件成功";
                    break;
                case UseMessage.MY_MSG_3:
                    UpResult.Text = "写文件失败";
                    break;
                case UseMessage.MY_MSG_4:
                    UpResult.Text = "升级结束";
                    break;
                case UseMessage.MY_MSG_5:
                    UpResult.Text = "撤销升级成功";
                    break;
                case UseMessage.MY_MSG_6:
                    UpResult.Text = "协议模块认证成功，点击开始升级";
                    break;

            }
            base.WndProc(ref m);

        }
        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_TransFile.m_data.Clear();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath.Text = fileDialog.FileName;
                CGlobal.m_Device.MD5 = CGlobal.GetMD5Hash(fileDialog.FileName);
                try
                {
                    if (CGlobal.DoWithLoadFile(fileDialog.FileName) == false) return;
                    FileDownMsg.Value = 0;
                    FileDownMsg.Minimum = 0;
                    FileDownMsg.Maximum = CGlobal.m_Device.m_TransFile.SumFrames;
                    FileDownMsg.Step = 1;
                    FpbValue.Text = "进度0%";
                    BtnStartDown.Enabled = true;
                    BtnUpID.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("文件打开出错", "提示");
                }
            }
        }
        //升级身份认证
        private void BtnUpID_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder((int)EncodeStatus.Master_Update_Start);
        }
        //开始升级
        private void BtnStartDown_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_Ptr.m_IsUpdateFlag = true;
            UpResult.Text = "";
            CGlobal.m_Device.m_TransFile.CurrentIndex = 0;
            if (m_TaskList.Count == 0)
            {
                m_TaskList.Clear();
                DownFileTime.Stop();
                GetParaOperationList(34, 1, false);
                GetParaOperationList(29, 1, false);
                GetParaOperationList(30, CGlobal.m_Device.m_TransFile.SumFrames, true);
                m_TaskList[m_TaskList.Count - 1].m_AnswerFlag = false;
            }
            BtnEndDown.Enabled = true;
            BtnStartDown.Enabled = false;

            Btn_SendVerfy.Enabled = true;
            BtnSendEnd.Enabled = true;

            DownFileTime.Start();
            bUpdateFlag = true;
            UpdateThread = new Thread(new ThreadStart(this.UpdateProcess));
            UpdateThread.IsBackground = true;
            UpdateThread.Start();
        }
        private void Btn_SendVerfy_Click(object sender, EventArgs e)
        {
            Btn_SendVerfy.Enabled = false;
            BtnSendEnd.Enabled = true;
            CGlobal.SendTaskOrder((int)EncodeStatus.Master_Update_Vertify);

        }
        private void BtnSendEnd_Click(object sender, EventArgs e)
        {
            BtnSendEnd.Enabled = false;
            CGlobal.SendTaskOrder(35);
        }
        private void GetParaOperationList(int nTaskNo, int nMaxCount, bool bNoWait = false)
        {
            int i = 0;
            int nCroup = 0;
            for (i = 0; i < nMaxCount; i++)
            {
                CDownFileTaskMsg pNewTask = new CDownFileTaskMsg();
                pNewTask.m_TaskNo = nTaskNo;
                pNewTask.m_AnswerFlag = bNoWait;
                m_TaskList.Add(pNewTask);
                nCroup++;
            }
            pNowTask = null;
        }
        private CDownFileTaskMsg pNowTask = null;
        private void UpdateProcess()
        {
            int nTimieOut = 0;
            exitEvent.Reset();
            int nSpaceTime = Convert.ToInt32(DelayTime.Text);
            if (nSpaceTime <= 0) nSpaceTime = 1;
            if (nSpaceTime > 10000) nSpaceTime = 10000;
            int nWaitCount = 20000 / nSpaceTime;
            while (bUpdateFlag)
            {
                if (nTimieOut >= nWaitCount) break;
                if (exitEvent.WaitOne(nSpaceTime))
                {
                    break;
                }
                if (pNowTask != null)
                {
                    if (pNowTask.m_AnswerFlag == true)
                    {
                        pNowTask = null;
                    }
                    else
                    {
                        nTimieOut++;
                        continue;
                    }
                }
                if (m_TaskList.Count > 0)
                {
                    if (m_TaskList.Count == 1)
                    {
                        Thread.Sleep(1000);
                    }
                    nTimieOut = 0;
                    pNowTask = m_TaskList[0];
                    if (pNowTask.m_AnswerFlag) pNowTask.m_AnswerFlag = bNeedFlag;
                    CGlobal.m_Device.m_Ptr.m_NowDownFileTask = pNowTask;
                    CGlobal.SendTaskOrder(pNowTask.m_TaskNo);
                    m_TaskList.RemoveAt(0);
                }
                else
                {
                    bUpdateFlag = false;
                }
            }
            bUpdateFlag = false;
        }
        private void DownFileTime_Tick(object sender, EventArgs e)
        {
            int nNowStep = CGlobal.m_Device.m_TransFile.CurrentIndex;
            if (nNowStep < 0) nNowStep = 0;
            if (nNowStep > FileDownMsg.Maximum) nNowStep = FileDownMsg.Maximum;
            FileDownMsg.Value = nNowStep;
            float fValue = (float)(((float)CGlobal.m_Device.m_TransFile.CurrentIndex) / ((float)CGlobal.m_Device.m_TransFile.SumFrames));
            fValue *= 100;
            FpbValue.Text = "进度" + fValue.ToString("F2") + "%";
            /*  if (pNowTask != null)
              {
                  if (pNowTask.m_AnswerFlag == true) pNowTask = null;
              }
              else
              {
                  if (m_TaskList.Count > 0)
                  {
                      pNowTask = m_TaskList[0];
                      CGlobal.m_Device.m_Ptr.m_NowDownFileTask = pNowTask;
                      CGlobal.SendTaskOrder(pNowTask.m_TaskNo);
                      FileDownMsg.PerformStep();
                      float fValue = (float)(((float)CGlobal.m_Device.m_TransFile.CurrentIndex) / ((float)CGlobal.m_Device.m_TransFile.SumFrames));
                      fValue *= 100;
                      FpbValue.Text = "进度" + fValue.ToString("F2") + "%";
                      m_TaskList.RemoveAt(0);
                  }
                  else
                  {
                      DownFileTime.Stop();
                  }
              }*/
        }
        private void BtnEndDown_Click(object sender, EventArgs e)
        {
            DownFileTime.Stop();
            //     UpdateThread.Abort();
            exitEvent.Set();
            Thread.Sleep(1000);
            bUpdateFlag = false;
         //   UpdateThread.Join();

            CGlobal.SendTaskOrder(36);
            m_TaskList.Clear();
            BtnEndDown.Enabled = false;
            BtnStartDown.Enabled = true;
            Btn_SendVerfy.Enabled = true;
            BtnSendEnd.Enabled = true;
            BtnStartDown.Text = "继续升级";
        }
        private void CUpdataForm_Load(object sender, EventArgs e)
        {           
            if (CGlobal.m_Device.m_Connection.IsEncode == true)
            {
                Btn_SendVerfy.Visible = true;
                BtnUpID.Visible = true;
                Use_Normal.Checked = true;
                CEnCodePtr.NormalTest();
            }
            else
            {
                myPanel1.Visible = false;   
                Btn_SendVerfy.Visible = false;
                BtnUpID.Visible = false;
            }
            CGlobal.m_Device.m_SelectDownFileType = 1;
            CGlobal.m_Device.m_UpdataFormHandle = (int)this.Handle;
        }
        private void UseErrTime_CheckedChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_TimeErrFlag = UseErrTime.Checked;
        }
        private void UseErrSign_CheckedChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_SignErrFlag = UseErrSign.Checked;
        }
        private void UseErrRand_CheckedChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_RandErrFlag = UseErrRand.Checked;
        }
        private void UseNoSign_CheckedChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_UseNoSignFlag = UseNoSign.Checked;
        }
        private void UseMingWen_CheckedChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_UseMingWenFlag = UseMingWen.Checked;
        }
        private void UseErrSign_Click(object sender, EventArgs e)
        {
            if (UseErrSign.Checked == true)
            {
                UseErrSign.Checked = false;
            }
            else
            {
                UseErrSign.Checked = true;
            }
        }
        private void UseErrTime_Click(object sender, EventArgs e)
        {
            if (UseErrTime.Checked == true)
            {
                UseErrTime.Checked = false;
            }
            else
            {
                UseErrTime.Checked = true;
            }
        }
        private void UseErrRand_Click(object sender, EventArgs e)
        {
            if (UseErrRand.Checked == true)
            {
                UseErrRand.Checked = false;
            }
            else
            {
                UseErrRand.Checked = true;
            }
        }
        private void UseNoSign_Click(object sender, EventArgs e)
        {
            if (UseNoSign.Checked == true)
            {
                UseNoSign.Checked = false;
            }
            else
            {
                UseNoSign.Checked = true;
            }
        }
        private void UseMingWen_Click(object sender, EventArgs e)
        {
            if (UseMingWen.Checked == true)
            {
                UseMingWen.Checked = false;
            }
            else
            {
                UseMingWen.Checked = true;
            }

        }

        private void CUpdataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CGlobal.m_Device.m_Ptr.m_IsUpdateFlag = false;
        }

        private void Use_Normal_CheckedChanged(object sender, EventArgs e)
        {
            if (Use_Normal.Checked == true)
            {
                CEnCodePtr.NormalTest();
            }
        }

        private void CUpdataForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
