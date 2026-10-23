using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TerminalWorkbench.Presentation;
using System.IO;
namespace DtuTestManager
{
    public partial class CDownFileForm : Form
    {
        private List<CDownFileTaskMsg> m_TaskList = new List<CDownFileTaskMsg>();

        public CDownFileForm()
        {
            InitializeComponent();
        }


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1:
                    DownResult.Text = "写文件激活失败";
                    break;
                case UseMessage.MY_MSG_2:
                    DownResult.Text = "写文件成功";
                    break;
                case UseMessage.MY_MSG_3:
                    DownResult.Text = "写文件失败";
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
                try
                {
                    if (CGlobal.DoWithLoadFile(fileDialog.FileName) == false)
                        return;
                    FileDownMsg.Value = 0;
                    FileDownMsg.Minimum = 0;
                    FileDownMsg.Maximum = CGlobal.m_Device.m_TransFile.SumFrames;
                    FileDownMsg.Step = 1;
                    FpbValue.Text = "进度0%";
                    BtnStartDown.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("文件打开出错", "提示");
                }
            }
        }
        //开始传输
        private void BtnStartDown_Click(object sender, EventArgs e)
        {
            DownResult.Text = "";
            m_TaskList.Clear();
            CGlobal.m_Device.m_TransFile.CurrentIndex = 0;
            DownFileTime.Stop();
            GetParaOperationList(29, 1, false);
            GetParaOperationList(30, CGlobal.m_Device.m_TransFile.SumFrames, true);
            DownFileTime.Start();
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
            m_TaskList[m_TaskList.Count - 1].m_AnswerFlag = false;
            pNowTask = null;

        }

        private void CDownFileForm_Load(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_SelectDownFileType = 0;
            CGlobal.m_Device.m_DownFileFormHandle = (int)this.Handle;
        }
        private CDownFileTaskMsg pNowTask = null;
        private void DownFileTime_Tick(object sender, EventArgs e)
        {
            if (pNowTask != null)
            {
                if (pNowTask.m_AnswerFlag == true)
                    pNowTask = null;
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
            }
        }

        private void BtnStartUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认升级？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            CGlobal.SendTaskOrder(33);
        }

        private void CDownFileForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
