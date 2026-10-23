using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using System.Data.OleDb;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace DtuTestManager
{
    public partial class CHisRecordForm : Form
    {
        private TreeNode RealRootNode = new TreeNode();
        private TreeNode HisRootNode = new TreeNode();
        private TreeNode m_SelectNode = null;
        private CFloderCtrl m_SelectFloder = null;
        private CFloderCtrl m_NowReadFloder = null;
        private CFileCtrl m_SelectFile = null;
        private Thread m_ReadFileThread;
        private bool bReadThread = false;
        private int m_ShowMode = -1;
        CWaitForm WaitForm = null;
        private List<Panel> pListViewPanel = new List<Panel>();
        public CHisRecordForm()
        {
            InitializeComponent();
        }      
        private void CHisRecordForm_Load(object sender, EventArgs e)
        {
          
            IniReadFileListView();
            IniFloderFileList();
            IniTreeViewList();
            IniHisFileListView();
            Btn_StopReadFile.Enabled = false;

            pListViewPanel.Add(LV_File_Panel);
            pListViewPanel.Add(HisDataView_Panel);
            pListViewPanel.Add(FileView_Panel);

            CGlobal.m_Device.m_FileFormHandle = (int)this.Handle;
            ShowMode(0);
            BtnCurrent.Visible = false;
            CurrentNameList.Visible = false;
            BtnLeft.Visible = false;
            BtnRight.Visible = false;
        }
        private void ShowMode(int nSelectView)
        {
            if (m_ShowMode == nSelectView) return;
            m_ShowMode = nSelectView;
            if (nSelectView < pListViewPanel.Count)
            {
                for (int i = 0; i < pListViewPanel.Count; i++)
                {
                    if (i == nSelectView)
                    {
                        pListViewPanel[i].Dock = DockStyle.Fill;
                        pListViewPanel[i].Visible = true;
                    }
                    else
                    {
                        pListViewPanel[i].Visible = false;
                    }
                }
            }
        }
        private void IniReadFileListView()
        {
            LV_File.ProgressColumnIndex = 5;
            LV_File.Columns.Add("选择", 40, HorizontalAlignment.Center);
            LV_File.Columns.Add("序号", 50, HorizontalAlignment.Center);
            LV_File.Columns.Add("文件名称", 250);
            LV_File.Columns.Add("大小(Byte)", 100, HorizontalAlignment.Center);
            LV_File.Columns.Add("修改时间", 200, HorizontalAlignment.Center);
            LV_File.Columns.Add("进度", 200, HorizontalAlignment.Center);
            LV_File.Columns.Add("读文件结果", 80, HorizontalAlignment.Center);
            LV_File.Columns.Add("状态", 80, HorizontalAlignment.Center);

            LV_File.GridLines = true;
            LV_File.FullRowSelect = true;
            LV_File.View = View.Details;
            LV_File.Scrollable = true;
            LV_File.MultiSelect = false;
            LV_File.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV_File.CheckBoxes = true;
            LV_File.Dock = DockStyle.Fill;
        }
        private void IniHisFileListView()
        {
            HisDataView.Columns.Add("序号", 50, HorizontalAlignment.Center);
            HisDataView.Columns.Add("文件名称", 250, HorizontalAlignment.Center);

            HisDataView.GridLines = true;
            HisDataView.FullRowSelect = true;
            HisDataView.View = View.Details;
            HisDataView.Scrollable = true;
            HisDataView.MultiSelect = false;
            HisDataView.HeaderStyle = ColumnHeaderStyle.Clickable;
            HisDataView.Dock = DockStyle.Fill;
        }
        private void AddRealFloder(string strPath, string strName, string strFileType, string strFloderKey = "")
        {
            CFloderCtrl pNewFolder = new CFloderCtrl();
            pNewFolder.m_FloderPath = strPath;
            pNewFolder.m_FloderName = strName;
            pNewFolder.m_FileType = strFileType;
            pNewFolder.m_FloderKey = strFloderKey;
            pNewFolder.m_FloaderType = CGlobal.m_Device.m_RealFloderList.Count;
            pNewFolder.IniReadFloderFile();
            CGlobal.m_Device.m_RealFloderList.Add(pNewFolder);
        }
        private void AddHisFloder(string strPath, string strName, string strFileType, string strFloderKey = "")
        {
            CFloderCtrl pNewFolder = new CFloderCtrl();
            pNewFolder.m_FloderPath = strPath;
            pNewFolder.m_FloderName = strName;
            pNewFolder.m_FileType = strFileType;
            pNewFolder.m_FloderKey = strFloderKey;
            pNewFolder.m_FloaderType = 100 + CGlobal.m_Device.m_HisFloderList.Count;
            pNewFolder.IniReadFloderFile();
            CGlobal.m_Device.m_HisFloderList.Add(pNewFolder);
        }
        private void IniRealFloderFileList()
        {
            if (CGlobal.m_Device.m_RealFloderList.Count == 0)
            {
                AddRealFloder("实时文件记录\\录波文件", "录波文件", ".cfg", "COMTRADE");
                AddRealFloder("实时文件记录\\极值记录文件", "极值记录文件", ".msg", "HISTORY/EXV");
                AddRealFloder("实时文件记录\\定点记录文件", "定点记录文件", ".msg", "HISTORY/FIXPT");
                AddRealFloder("实时文件记录\\日冻结能量记录文件", "日冻结能量记录文件", ".msg", "HISTORY/FRZ");
                AddRealFloder("实时文件记录\\功率反向电能量冻结值", "功率反向电能量冻结值", ".msg", "HISTORY/FLOWREV");
                AddRealFloder("实时文件记录\\日志文件", "日志文件", ".msg", "HISTORY/ULOG");
                AddRealFloder("实时文件记录\\SOE记录文件", "SOE记录文件", ".msg", "HISTORY/SOE");
                AddRealFloder("实时文件记录\\遥控操作记录文件", "遥控操作记录文件", ".msg", "HISTORY/CO");
              
                AddRealFloder("实时文件记录\\线损模块定时冻结", "线损模块定时冻结", ".msg", "LINELOSS/FIXD");
                AddRealFloder("实时文件记录\\线损模块瞬时冻结", "线损模块瞬时冻结", ".msg", "LINELOSS/RAND");
                AddRealFloder("实时文件记录\\线损模块日冻结", "线损模块日冻结", ".msg", "LINELOSS/FRZD");
                AddRealFloder("实时文件记录\\线损模块整点冻结", "线损模块整点冻结", ".msg", "LINELOSS/SHARPD");
                AddRealFloder("实时文件记录\\线损模块考核日冻结", "线损模块考核日冻结", ".msg", "LINELOSS/MONTHD");
                AddRealFloder("实时文件记录\\线损模块事件记录", "线损模块事件记录", ".msg", "LINELOSS/EVENTD");

                AddRealFloder("转发表配置\\数据库文件", "数据库文件", ".db", "SQLITE/DB");
            }
            else
            {
                for (int i = 0; i < CGlobal.m_Device.m_RealFloderList.Count; i++) CGlobal.m_Device.m_RealFloderList[i].IniReadFloderFile();
            }
        }
        private void IniHiseFloderFileList()
        {
            if (CGlobal.m_Device.m_HisFloderList.Count == 0)
            {
                AddHisFloder("历史文件记录\\录波文件", "录波文件", ".cfg");
                AddHisFloder("历史文件记录\\极值记录文件", "极值记录文件", ".msg");
                AddHisFloder("历史文件记录\\定点记录文件", "定点记录文件", ".msg");
                AddHisFloder("历史文件记录\\日冻结能量记录文件", "日冻结能量记录文件", ".msg");
                AddHisFloder("历史文件记录\\功率反向电能量冻结值", "功率反向电能量冻结值", ".msg");
                AddHisFloder("历史文件记录\\日志文件", "日志文件", ".msg");
                AddHisFloder("历史文件记录\\SOE记录文件", "SOE记录文件", ".msg");
                AddHisFloder("历史文件记录\\遥控操作记录文件", "遥控操作记录文件", ".msg");
              
                AddHisFloder("实时文件记录\\线损模块定时冻结", "线损模块定时冻结", ".msg");
                AddHisFloder("实时文件记录\\线损模块瞬时冻结", "线损模块瞬时冻结", ".msg");
                AddHisFloder("实时文件记录\\线损模块日冻结", "线损模块日冻结", ".msg");
                AddHisFloder("实时文件记录\\线损模块整点冻结", "线损模块整点冻结", ".msg");
                AddHisFloder("实时文件记录\\线损模块考核日冻结", "线损模块考核日冻结", ".msg");
                AddHisFloder("实时文件记录\\线损模块事件记录", "线损模块事件记录", ".msg");

                AddHisFloder("转发表配置\\数据库文件", "数据库文件", ".db", "SQLITE/DB");
            }
            else
            {
                for (int i = 0; i < CGlobal.m_Device.m_HisFloderList.Count; i++) CGlobal.m_Device.m_HisFloderList[i].IniReadFloderFile();
            }
        }
        private void IniFloderFileList()
        {
            IniRealFloderFileList();
            IniHiseFloderFileList();
        }
        private void UpdateRealFileTreeViewList()
        {
            int i;
            CFloderCtrl pFloder;
            for (i = 0; i < CGlobal.m_Device.m_RealFloderList.Count; i++)
            {
                pFloder = CGlobal.m_Device.m_RealFloderList[i];
                pFloder.IniFileTreeView();
            }
        }
        private void UpdateHisFileTreeViewList()
        {
            int i;
            CFloderCtrl pFloder;
            for (i = 0; i < CGlobal.m_Device.m_HisFloderList.Count; i++)
            {
                pFloder = CGlobal.m_Device.m_HisFloderList[i];
                pFloder.IniFileTreeView();
            }
        }
        private void IniTreeViewList()
        {
            int i;
            CFloderCtrl pFloder;
            RealRootNode.Text = "实时数据文件";
            RealRootNode.Tag = null;
            FileTreeViewList.Nodes.Add(RealRootNode);
            for (i = 0; i < CGlobal.m_Device.m_RealFloderList.Count; i++)
            {
                pFloder = CGlobal.m_Device.m_RealFloderList[i];
                pFloder.m_FloderNode = new TreeNode();
                pFloder.m_FloderNode.Text = pFloder.m_FloderName;
                pFloder.m_FloderNode.Tag = pFloder;
                RealRootNode.Nodes.Add(pFloder.m_FloderNode);
                pFloder.IniFileTreeView();
            }

            HisRootNode.Text = "历史数据文件";
            HisRootNode.Tag = null;
            FileTreeViewList.Nodes.Add(HisRootNode);
            for (i = 0; i < CGlobal.m_Device.m_HisFloderList.Count; i++)
            {
                pFloder = CGlobal.m_Device.m_HisFloderList[i];
                pFloder.m_FloderNode = new TreeNode();
                pFloder.m_FloderNode.Text = pFloder.m_FloderName;
                pFloder.m_FloderNode.Tag = pFloder;
                HisRootNode.Nodes.Add(pFloder.m_FloderNode);
                pFloder.IniFileTreeView();
            }

            RealRootNode.Expand();
            HisRootNode.Expand();
        }
        private void ShowFileDataView(CFileCtrl pFile)
        {
            try
            {
                if (pFile == null) return;
                if (pFile.m_bHaveReadFileCOntent == false)
                {
                    pFile.ReadFileContent();//读取文件               
                }
                if (pFile.m_FileType % 100 == 0)
                {
                    pFile.ShowLuboForm();
                }
                else if (pFile.m_FileType % 100 == 2)
                {
                    BtnLeft.Visible = true;
                    BtnRight.Visible = true;
                    DisplayFixptFileContent(pFile); //定点数据表格显示
                }
                else
                {
                    DisplayFileContent(pFile); //表格显示            
                }
            }
            catch (Exception)
            {
            }
        }
        private void DisplayFixptFileContent(CFileCtrl pFile)
        {
            int i,j;
            int nStartIndex, nEndIndex;
            label_FileName.Text = "文件名：" + pFile.ReadFileName;
            label_Version.Text = "文件版本号：" + pFile.ReadFileVersion;
            label_ID.Text = "终端ID：" + pFile.TerminalID;



            nStartIndex = pFile.ColumnCnt * pFile.ColumnGap;
            nEndIndex = nStartIndex + pFile.ColumnGap;
            if (nEndIndex > CFixData.m_MaxCount) nEndIndex = CFixData.m_MaxCount;

            pFile.m_StrHead = new string[2 + nEndIndex - nStartIndex];
            ShowMode(2);
            FileView.Items.Clear();
            FileView.Columns.Clear();
            pFile.m_StrHead[0] = "序号";
            pFile.m_StrHead[1] = "时间";

            FileView.Columns.Add(pFile.m_StrHead[0], 60, HorizontalAlignment.Center);
            FileView.Columns.Add(pFile.m_StrHead[1], 160, HorizontalAlignment.Center);
            for (i = nStartIndex; i < nEndIndex; i++)
            {
                for (j = 0; j < pFile.m_FixDataList.Count; j++)
                {
                    if (i < pFile.m_FixDataList[j].m_DataList.Count)
                    {
                        pFile.m_StrHead[i-nStartIndex+2] = pFile.m_FixDataList[j].m_DataList[i].m_DataAddress;
                        FileView.Columns.Add(pFile.m_StrHead[i-nStartIndex+2], 80, HorizontalAlignment.Center);
                        break;
                    }
                }
            }
            FileView.GridLines = true;
            FileView.FullRowSelect = true;
            FileView.View = View.Details;
            FileView.Scrollable = true;
            FileView.MultiSelect = false;
            FileView.HeaderStyle = ColumnHeaderStyle.Clickable;
            FileView.Dock = DockStyle.Fill;
            BtnCurrent.Visible = true;
            CurrentNameList.Visible = true;
            BtnRight.Visible = true;
            BtnLeft.Visible = true;

            string[] strRow = new string[2 + nEndIndex - nStartIndex];
            for (i = 0; i < pFile.m_FixDataList.Count; i++)
            {
                strRow[0] = (i + 1).ToString();
                strRow[1] = pFile.m_FixDataList[i].m_TimeFlag;
                for(j=nStartIndex;j<nEndIndex;j++)
                {
                    if(j<pFile.m_FixDataList[i].m_DataList.Count)
                    {
                        strRow[j - nStartIndex + 2] = pFile.m_FixDataList[i].m_DataList[j].m_DataValue;
                    }
                    else
                    {
                        strRow[j - nStartIndex + 2] = "---";
                    }
                }
                ListViewItem item = new ListViewItem(strRow);
                FileView.Items.Add(item);
            }
            if (CurrentNameList.Visible == true)
            {
                CurrentNameList.Items.Clear();
                for (i = 0; i < CFixData.m_MaxCount; i++)
                {
                    for (j = 0; j < pFile.m_FixDataList.Count; j++)
                    {
                        if (i < pFile.m_FixDataList[j].m_DataList.Count)
                        {
                            CurrentNameList.Items.Add(pFile.m_FixDataList[j].m_DataList[i].m_DataAddress);
                            break;
                        }
                    }
                }
                if (CurrentNameList.Items.Count > 0) CurrentNameList.SelectedIndex = 0;
            }
        }        
        private void DisplayFileContent(CFileCtrl pFile)
        {
            int i;
            FileView.Items.Clear();
            FileView.Columns.Clear();
            label_FileName.Text ="文件名："+ pFile.ReadFileName;
            label_Version.Text = "文件版本号：" + pFile.ReadFileVersion;
            label_ID.Text = "终端ID：" + pFile.TerminalID;


            for (i = 0; i < pFile.m_StrHead.Length; i++)
            {
                int nWidth = pFile.m_StrHead[i].Length * 12;
                if (nWidth < pFile.m_ColumnsWith[i]) nWidth = pFile.m_ColumnsWith[i];
                FileView.Columns.Add(pFile.m_StrHead[i], nWidth, HorizontalAlignment.Left);
            }            
            FileView.GridLines = true;
            FileView.FullRowSelect = true;
            FileView.View = View.Details;
            FileView.Scrollable = true;
            FileView.MultiSelect = false;
            FileView.HeaderStyle = ColumnHeaderStyle.Clickable;
            FileView.Dock = DockStyle.Fill;
            ShowMode(2);

            if (((pFile.m_FileType % 100) == 3) || ((pFile.m_FileType % 100 >= 8) && (pFile.m_FileType % 100 <=12)))
            {
                BtnCurrent.Visible = true;
                CurrentNameList.Visible = true;
            }

            string[] h = new string[pFile.m_StrHead.Length];
            int S_Row = pFile.FileContent.Count / (pFile.m_StrHead.Length - 1);
            for (i = 0; i < S_Row; i++)
            {
                h[0] = (i + 1).ToString();
                for (int j = 1; j < pFile.m_StrHead.Length; j++)
                {
                    h[j] = pFile.FileContent[j - 1 + i * (pFile.m_StrHead.Length - 1)];
                }
                ListViewItem item = new ListViewItem(h);
                FileView.Items.Add(item);
            }
            if (CurrentNameList.Visible == true)
            {
                CurrentNameList.Items.Clear();
                for (i = 2; i < pFile.m_StrHead.Length; i++)
                {
                    CurrentNameList.Items.Add(pFile.m_StrHead[i]);
                }
                if (CurrentNameList.Items.Count > 0) CurrentNameList.SelectedIndex = 0;
            }


            
        }
        private void ShowTreeSelDataMsg()
        {
            m_SelectNode = FileTreeViewList.SelectedNode;
            if (m_SelectNode != null && m_SelectNode.Tag != null)
            {
                Type pSelectType = m_SelectNode.Tag.GetType();
                Type pFloderType = typeof(CFloderCtrl);
                Type pFileType = typeof(CFileCtrl);

                m_SelectFloder = null;
                m_SelectFile = null;

                if (pSelectType == pFloderType)     //文件夹类型
                {
                    m_SelectFloder = (CFloderCtrl)m_SelectNode.Tag;
                    ShowFlieMsgListView(m_SelectFloder);
                }
                else if (pSelectType == pFileType)
                {
                    m_SelectFile = (CFileCtrl)m_SelectNode.Tag;
                    ShowFileDataView(m_SelectFile);
                }
            }
        }
        private void FileTreeViewList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BtnCurrent.Visible = false;
            CurrentNameList.Visible = false;
            BtnLeft.Visible = false;
            BtnRight.Visible = false;
            ShowTreeSelDataMsg();
        }
        private void ShowWaitReadFileList(CFloderCtrl pSelectFloder)
        {
            string[] ColHead = new string[8];
            ListViewItem ListItem;
            ShowMode(0);
            LV_File.ClearAllItem(false);
            /*
            LV_File.Columns.Add("选择", 60, HorizontalAlignment.Center);
            LV_File.Columns.Add("序号", 160, HorizontalAlignment.Center);
            LV_File.Columns.Add("文件名称", 160, HorizontalAlignment.Center);
            LV_File.Columns.Add("文件大小", 160, HorizontalAlignment.Center);
            LV_File.Columns.Add("记录时间", 80, HorizontalAlignment.Center);*/

            for (int i = 0; i < pSelectFloder.m_TempFileList.Count; i++)
            {
                ColHead[1] = Convert.ToString(i + 1);
                ColHead[2] = pSelectFloder.m_TempFileList[i].m_FileTypeName;
                ColHead[3] = pSelectFloder.m_TempFileList[i].m_FileSize.ToString();
                ColHead[4] = pSelectFloder.m_TempFileList[i].m_RecordTime;
                float fValue = 0;
                if (pSelectFloder.m_TempFileList[i].m_FileSize > 0) fValue = (float)(pSelectFloder.m_TempFileList[i].m_ReadFileLeng / pSelectFloder.m_TempFileList[i].m_FileSize);
                fValue *= 100;
                ColHead[5] = fValue.ToString("F2");
                ColHead[6] = pSelectFloder.m_TempFileList[i].m_ReadResult;
                if (pSelectFloder.CheckFileExits(pSelectFloder.m_TempFileList[i].m_FileTypeName) == true)
                {
                    pSelectFloder.m_TempFileList[i].m_bHaveRead = true;
                    ColHead[7] = "已存在";
                }
                else
                {
                    pSelectFloder.m_TempFileList[i].m_bHaveRead = false;
                    ColHead[7] = "----";
                }
                ListItem = new ListViewItem(ColHead);
                ListItem.Checked = pSelectFloder.m_TempFileList[i].m_ReadSelect;
                LV_File.AddItem(ListItem);
            }
        }
        private void ShowHisDataFileList(CFloderCtrl pSelectFloder)
        {
            string[] ColHead = new string[2];
            ListViewItem ListItem;
            ShowMode(1);
            HisDataView.Items.Clear();
            for (int i = 0; i < pSelectFloder.m_FileList.Count; i++)
            {
                ColHead[0] = Convert.ToString(i + 1);
                ColHead[1] = pSelectFloder.m_FileList[i].m_FileName;
          
                ListItem = new ListViewItem(ColHead);
                HisDataView.Items.Add(ListItem);
            }
        }
        private void ShowFlieMsgListView(CFloderCtrl pSelectFloder)
        {
            if (pSelectFloder != null && pSelectFloder.m_FloaderType < 100)
            {
                Time_FreshView.Start();
                return;
            }
            if (pSelectFloder != null && pSelectFloder.m_FloaderType >= 100)
            {
                ShowHisDataFileList(pSelectFloder);
                return;
            }
        }
        private bool CheckSelectRealdFloder()
        {
            if (m_SelectFloder == null || m_SelectFloder.m_FloaderType >= 100)
            {
                MessageBox.Show("请选择实时数据文件目录!");
                return false;
            }
            return true;
        }
        //召唤目录
        private void btn_CallDir_Click(object sender, EventArgs e)
        {
            if (CheckSelectRealdFloder() == false) return;
            CFileTimeForm pTimeForm = new CFileTimeForm();
            CGlobal.m_Device.m_CallFileTime.IniTime();
            pTimeForm.m_CallFileTime = CGlobal.m_Device.m_CallFileTime;
            pTimeForm.ShowDialog();
            if (pTimeForm.DialogResult != DialogResult.OK) return;

            m_SelectFloder.ClearTempFileList();
            LV_File.ClearAllItem(false);
            LV_File.Items.Clear();
            CGlobal.m_Device.m_SelectFloder = m_SelectFloder;
            CGlobal.SendTaskOrder(22);
            if (CGlobal.m_Device.m_bOnlineStatus == false) return;
            WaitForm = new CWaitForm();
            WaitForm.ShowDialog();
        }
        private void FreshTheFileMsg(CFileCtrl pFile, int nRow,string strResult)
        {
            float fValue = 0;
            if (pFile.m_FileSize > 0) fValue = (float)(((float)pFile.m_ReadFileLeng) / ((float)pFile.m_FileSize));
            fValue *= 100;
            try
            {
                LV_File.Invoke
                (
                      new MethodInvoker
                      (
                          delegate
                          {
                              LV_File.Items[nRow].SubItems[5].Text = fValue.ToString("F2");
                              LV_File.Items[nRow].SubItems[6].Text = strResult;
                          }
                      )
                );
            }
            catch { }
        }
        private void BtnEnableTime_Tick(object sender, EventArgs e)
        {
            if (m_ReadFileThread == null)
            {
                btn_CallAllFile.Enabled = true;
                btn_CallDir.Enabled = true;
                btn_raed_SelFile.Enabled = true;
                btn_diff_read.Enabled = true;
                Btn_StopReadFile.Enabled = false;
                BtnCancelAll.Enabled = true;
                IniRealFloderFileList();
                UpdateRealFileTreeViewList();
                BtnEnableTime.Stop();
            }
        }
        private void ReadFileProcess()
        {
            m_NowReadFloder = m_SelectFloder;
            m_NowReadFloder.m_NowReadPalce = 0;
            while (bReadThread)
            {
                if (m_NowReadFloder == null) break;
                if (m_NowReadFloder.m_NowReadPalce >= m_NowReadFloder.m_TempFileList.Count)
                {
                    bReadThread = false;
                    break;
                }
                if (m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_ReadSelect)
                {
                    Thread.Sleep(100);
                    if (m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_StartFlag == false)
                    {
                        m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_StartFlag = true;
                        m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_ReadResult = "正在读取...";
                        CGlobal.SendTaskOrder(23);
                    }
                    else
                    {
                        if (Object.Equals(m_SelectFloder, m_NowReadFloder))
                        {
                            FreshTheFileMsg(m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce],
                                                    m_NowReadFloder.m_NowReadPalce,
                                                    m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_ReadResult);
                        }
                        if (m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_EndFlag)
                        {
                            if (m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].m_ReadSuccess)
                            {
                                m_NowReadFloder.m_TempFileList[m_NowReadFloder.m_NowReadPalce].SaveFile();
                            }
                            m_NowReadFloder.m_NowReadPalce++;
                        }
                    }
                }
                else
                    m_NowReadFloder.m_NowReadPalce++;
            }
            bReadThread = false;
            m_ReadFileThread = null;    
        }
        private void ExcuteNeedReadFile()
        {
            bool bReadFlag = false;
            for (int i = 0; i < LV_File.Items.Count; i++)
            {
                m_SelectFloder.m_TempFileList[i].m_ReadSelect = LV_File.Items[i].Checked;
                if (m_SelectFloder.m_TempFileList[i].m_ReadSelect)
                {
                    bReadFlag = true;
                    m_SelectFloder.m_TempFileList[i].IniReadFile();
                }
            }
            if (bReadFlag)
            {
                btn_CallAllFile.Enabled = false;
                btn_CallDir.Enabled = false;
                btn_raed_SelFile.Enabled = false;
                btn_diff_read.Enabled = false;
                Btn_StopReadFile.Enabled = true;
                BtnCancelAll.Enabled = false;
                bReadThread = true;
                m_ReadFileThread = new Thread(new ThreadStart(this.ReadFileProcess));
                m_ReadFileThread.IsBackground = true;
                m_ReadFileThread.Start();
                BtnEnableTime.Start();
            }
        }
        private void Btn_StopReadFile_Click(object sender, EventArgs e)
        {
            btn_CallAllFile.Enabled = true;
            btn_CallDir.Enabled = true;
            btn_raed_SelFile.Enabled = true;
            btn_diff_read.Enabled = true;
            Btn_StopReadFile.Enabled = false;
            BtnCancelAll.Enabled = true;
            if (m_ReadFileThread != null)
            {
                //m_ReadFileThread.Abort();
                bReadThread = false;
              //  m_ReadFileThread.Join();
                m_ReadFileThread = null;
            }
        }
        //全部读取
        private void btn_CallAllFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckSelectRealdFloder() == false) return;
                if (LV_File.Items.Count == 0) return;

                for (int i = 0; i < LV_File.Items.Count; i++)
                {
                    LV_File.Items[i].Checked = true;
                }
                ExcuteNeedReadFile();
            }
            catch { }
        }
        //选择读取
        private void btn_raed_SelFile_Click(object sender, EventArgs e)
        {
            if (CheckSelectRealdFloder() == false) return;
            ExcuteNeedReadFile(); 
        }
        //差异读取
        private void btn_diff_read_Click(object sender, EventArgs e)
        {
            if (CheckSelectRealdFloder() == false) return;
            int nIndex = m_SelectFloder.m_FloaderType;
            for (int i = 0; i < m_SelectFloder.m_TempFileList.Count; i++)
            {
                if (CGlobal.m_Device.m_HisFloderList[nIndex].CheckFileExits(m_SelectFloder.m_TempFileList[i].m_FileTypeName) == false)
                {
                    LV_File.Items[i].Checked = true;
                }
                if (CGlobal.m_Device.m_RealFloderList[nIndex].CheckFileExits(m_SelectFloder.m_TempFileList[i].m_FileTypeName) == false)
                {
                    LV_File.Items[i].Checked = true;
                }
            }
            ExcuteNeedReadFile();
        }
        /*
        private void RemoveFile(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo file in fileinfo)
                {
                    File.Copy(file.FullName, destPath + "\\" + file.Name, true);  //复制文件，true表示可以覆盖同名文件
                }

                Directory.Delete(srcPath, true);
                Directory.CreateDirectory(srcPath);
            }
            catch (Exception e)
            {
                throw;
            }
        }*/
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_2:
                    MessageBox.Show("此目录下没有文件", "提示");
                    break;
                case UseMessage.MY_MSG_3:
                    CWaitForm.m_bCose = true;
                    MessageBox.Show("无此目录", "提示");
                    break;
                case UseMessage.MY_MSG_4:
                    if (WaitForm != null)
                    {
                        WaitForm.Close();
                        WaitForm = null;
                    }
                    ShowFlieMsgListView(m_SelectFloder);
                    break;
                case 0x18:
                    if (m_ReadFileThread != null)
                    {
                        bReadThread = false;
                        m_ReadFileThread.Abort();
                        m_ReadFileThread = null;
                    }
                    break;
            }
           
            base.WndProc(ref m);

        }

       

        private void Time_FreshView_Tick(object sender, EventArgs e)
        {
            Time_FreshView.Stop();
            ShowWaitReadFileList(m_SelectFloder);
        }

       

        private void BtnCancelAll_Click(object sender, EventArgs e)
        {
            if (m_SelectFloder == null || m_SelectFloder.m_FloaderType >= 100) return;
            if (LV_File.Items.Count == 0) return;

            for (int i = 0; i < LV_File.Items.Count; i++)
            {
                LV_File.Items[i].Checked = false;
            }
        }
        private void BtnCopySave_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < CGlobal.m_Device.m_RealFloderList.Count; i++)
            {
                CGlobal.m_Device.m_RealFloderList[i].CopyFile(CGlobal.m_Device.m_HisFloderList[i].m_FloderPath);
            }
            IniHiseFloderFileList();
            UpdateHisFileTreeViewList();
        }

        private void BtnCurrent_Click(object sender, EventArgs e)
        {
            CCurrentForm fCurrent = new CCurrentForm();
          
            if (m_SelectFile.m_FileType % 100 == 2)
            { fCurrent.show(m_SelectFile.G_fixpt_data[CurrentNameList.SelectedIndex]); }
            else if (m_SelectFile.m_FileType % 100 == 3)
            { fCurrent.show(m_SelectFile.G_frz_data[CurrentNameList.SelectedIndex]); }
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            if (m_SelectFile.ColumnCnt > 0)
            {
                m_SelectFile.ColumnCnt--;
                DisplayFixptFileContent(m_SelectFile);
            }
        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            if (m_SelectFile.ColumnCnt < (m_SelectFile.ColumnMax-1))
            {
                m_SelectFile.ColumnCnt++;
                DisplayFixptFileContent(m_SelectFile);
            }
        }

        private void FileView_DoubleClick(object sender, EventArgs e)
        {
            if (m_SelectFile.m_FileType % 100 == 13)
            {
                if (FileView.SelectedItems != null && FileView.SelectedItems.Count > 0)
                {
                    string strContent = FileView.SelectedItems[0].SubItems[3].Text.Replace(" ", "");
                    CEventViewForm pForm = new CEventViewForm();
                    pForm.m_Type = Convert.ToInt32(FileView.SelectedItems[0].SubItems[1].Text.Replace(" ", ""));
                    if (pForm.m_Type < 1 || pForm.m_Type > 5) return;
                    pForm.Tag = strContent;
                    pForm.ShowDialog();
                }
            }
        }
        private void CHisRecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ReadFileThread != null)
            {
                bReadThread = false;
                m_ReadFileThread = null;
            }
            CGlobal.m_Device.m_FileFormHandle = -1;
        }
        private void CHisRecordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_ReadFileThread != null)
            {
                bReadThread = false;
                m_ReadFileThread.Abort();
                m_ReadFileThread = null;
            }
            CGlobal.m_Device.m_FileFormHandle = -1;
        }
    }
}
