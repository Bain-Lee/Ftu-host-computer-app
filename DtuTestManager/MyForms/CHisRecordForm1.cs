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

        private List<ListView> pListView = new List<ListView>();

        public CHisRecordForm()
        {
            InitializeComponent();
        }
        private void EnableDataMode(bool bTable = false, bool bImage = false)
        {
            BtnTableMode.Enabled = bTable;
            BtnImageMode.Enabled = bImage;
        }
        private void CHisRecordForm_Load(object sender, EventArgs e)
        {
            IniReadFileListView();
            IniFloderFileList();
            IniTreeViewList();
            IniHisFileListView();
            Btn_StopReadFile.Enabled = false;
            EnableDataMode();
            pListView.Add(LV_File);
            pListView.Add(HisDataView);
            pListView.Add(FileView);
            CGlobal.m_104Ptr.m_FileFomrHandle = (int)this.Handle;
            ShowMode(0);
        }
        private void ShowMode(int nSelectView)
        {
            if (m_ShowMode == nSelectView) return;
            m_ShowMode = nSelectView;
            if (nSelectView < pListView.Count)
            {
                for (int i = 0; i < pListView.Count; i++)
                {
                    if (i == nSelectView)
                        pListView[i].Visible = true;
                    else
                        pListView[i].Visible = false;
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
            pNewFolder.m_FloaderType = CGlobal.m_RealFloderList.Count;
            pNewFolder.IniReadFloderFile();
            CGlobal.m_RealFloderList.Add(pNewFolder);
        }
        private void AddHisFloder(string strPath, string strName, string strFileType, string strFloderKey = "")
        {
            CFloderCtrl pNewFolder = new CFloderCtrl();
            pNewFolder.m_FloderPath = strPath;
            pNewFolder.m_FloderName = strName;
            pNewFolder.m_FileType = strFileType;
            pNewFolder.m_FloderKey = strFloderKey;
            pNewFolder.m_FloaderType = 100 + CGlobal.m_HisFloderList.Count;
            pNewFolder.IniReadFloderFile();
            CGlobal.m_HisFloderList.Add(pNewFolder);
        }
        private void IniRealFloderFileList()
        {
            if (CGlobal.m_RealFloderList.Count == 0)
            {
                AddRealFloder("实时文件记录\\录波文件", "录波文件", ".cfg", "Comtrade");
                AddRealFloder("实时文件记录\\极值记录文件", "极值记录文件", ".msg", "exv");
                AddRealFloder("实时文件记录\\定点记录文件", "定点记录文件", ".msg", "fixpt");
                AddRealFloder("实时文件记录\\日冻结能量记录文件", "日冻结能量记录文件", ".msg", "frz");
                AddRealFloder("实时文件记录\\功率反向电能量冻结值", "功率反向电能量冻结值", ".msg", "flowrev");
                AddRealFloder("实时文件记录\\日志文件", "日志文件", ".msg", "Ulog");
                AddRealFloder("实时文件记录\\SOE记录文件", "SOE记录文件", ".msg", "soe");
                AddRealFloder("实时文件记录\\遥控操作记录文件", "遥控操作记录文件", ".msg", "co");
            }
            else
            {
                for (int i = 0; i < CGlobal.m_RealFloderList.Count; i++) CGlobal.m_RealFloderList[i].IniReadFloderFile();
            }
        }
        private void IniHiseFloderFileList()
        {
            if (CGlobal.m_HisFloderList.Count == 0)
            {
                AddHisFloder("历史文件记录\\录波文件", "录波文件", ".cfg");
                AddHisFloder("历史文件记录\\极值记录文件", "极值记录文件", ".msg");
                AddHisFloder("历史文件记录\\定点记录文件", "定点记录文件", ".msg");
                AddHisFloder("历史文件记录\\日冻结能量记录文件", "日冻结能量记录文件", ".msg");
                AddHisFloder("历史文件记录\\功率反向电能量冻结值", "功率反向电能量冻结值", ".msg");
                AddHisFloder("历史文件记录\\日志文件", "日志文件", ".msg");
                AddHisFloder("历史文件记录\\SOE记录文件", "SOE记录文件", ".msg");
                AddHisFloder("历史文件记录\\遥控操作记录文件", "遥控操作记录文件", ".msg");
            }
            else
            {
                for (int i = 0; i < CGlobal.m_HisFloderList.Count; i++) CGlobal.m_HisFloderList[i].IniReadFloderFile();
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
            for (i = 0; i < CGlobal.m_RealFloderList.Count; i++)
            {
                pFloder = CGlobal.m_RealFloderList[i];
                pFloder.IniFileTreeView();
            }
        }
        private void UpdateHisFileTreeViewList()
        {
            int i;
            CFloderCtrl pFloder;
            for (i = 0; i < CGlobal.m_HisFloderList.Count; i++)
            {
                pFloder = CGlobal.m_HisFloderList[i];
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
            for (i = 0; i < CGlobal.m_RealFloderList.Count; i++)
            {
                pFloder = CGlobal.m_RealFloderList[i];
                pFloder.m_FloderNode = new TreeNode();
                pFloder.m_FloderNode.Text = pFloder.m_FloderName;
                pFloder.m_FloderNode.Tag = pFloder;
                RealRootNode.Nodes.Add(pFloder.m_FloderNode);
                pFloder.IniFileTreeView();
            }

            HisRootNode.Text = "历史数据文件";
            HisRootNode.Tag = null;
            FileTreeViewList.Nodes.Add(HisRootNode);
            for (i = 0; i < CGlobal.m_HisFloderList.Count; i++)
            {
                pFloder = CGlobal.m_HisFloderList[i];
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
            if (pFile == null) return;
            switch (pFile.m_FileType % 100)
            {
                case 0:         //录波文件
                    EnableDataMode(false,true);
                    break;
                case 1:         //极值文件
                    EnableDataMode(true, true);
                    break;
                case 2:         //定点记录文件
                    EnableDataMode(true, true);
                    break;
                case 3:         //日冻结能量记录文件
                    EnableDataMode(true, true);
                    break;
                case 4:         //功率反向电能量冻结值
                    EnableDataMode(true, true);
                    break;
                case 5:         //日志文件
                    EnableDataMode(true, false);
                    break;
                case 6:         //SOE记录文件
                    EnableDataMode(true, false);
                    break;
                case 7:         //遥控操作记录文件
                    EnableDataMode(true, false);
                    break;
            }
        }
        private void FileTreeViewList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_SelectNode = FileTreeViewList.SelectedNode;
            if (m_SelectNode.Tag != null)
            {
                Type pSelectType = m_SelectNode.Tag.GetType();
                Type pFloderType = typeof(CFloderCtrl);
                Type pFileType = typeof(CFileCtrl);

                m_SelectFloder = null;
                m_SelectFile = null;
                EnableDataMode();
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
        private void ShowWaitReadFileList(CFloderCtrl pSelectFloder)
        {
            string[] ColHead = new string[8];
            ListViewItem ListItem;
            ShowMode(0);
            LV_File.ClearAllItem();

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
            m_SelectFloder.ClearTempFileList();
            LV_File.ClearAllItem();
            CGlobal.m_104Ptr.m_SelectFloder = m_SelectFloder;
            CGlobal.SendTaskOrder(22);
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
                m_ReadFileThread.Abort();
                bReadThread = false;
                m_ReadFileThread.Join();
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
        /*private void ReadAndDisplayFile( string filepath)
        {
           
            string str = "";
            try
            {
                using (FileStream fs = File.OpenRead(filepath))
                {
                    long left = fs.Length;
                    int maxLength = 100;//每次读取的最大长度  
                    int start = 0;//起始位置  
                    int num = 0;//已读取长度  
                    while (left > 0)
                    {
                        byte[] buffer = new byte[maxLength];//缓存读取结果  
                      //  char[] cbuffer = new char[maxLength];
                        fs.Position = start;//读取开始的位置  
                        num = 0;
                        if (left < maxLength)
                        {
                            num = fs.Read(buffer, 0, Convert.ToInt32(left));
                        }
                        else
                        {
                            num = fs.Read(buffer, 0, maxLength);
                        }
                        if (num == 0)
                        {
                            break;
                        }
                        for (int i = 0; i < num; i++)
                        {
                            CGlobal.m_showfile.filebuffer.Add(buffer[i]);
                        }
                        start += num;
                        left -= num;
                    }
                }

                byte[] sss = new byte[CGlobal.m_showfile.filebuffer.Count];
                for (int i = 0; i < sss.Length; i++)
                {
                    sss[i] = CGlobal.m_showfile.filebuffer[i];
                }
                str = CGlobal.BytesToString(sss);
                string S = str.ToString().Trim();;
                if (S == "")
                    return;
                CGlobal.m_showfile.value = S.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch
            { }
        }*/
        //差异读取
        private void btn_diff_read_Click(object sender, EventArgs e)
        {
            if (CheckSelectRealdFloder() == false) return;
            int nIndex = m_SelectFloder.m_FloaderType;
            for (int i = 0; i < m_SelectFloder.m_TempFileList.Count; i++)
            {
                if (CGlobal.m_HisFloderList[nIndex].CheckFileExits(m_SelectFloder.m_TempFileList[i].m_FileTypeName) == false)
                {
                    LV_File.Items[i].Checked = true;
                }
                if (CGlobal.m_RealFloderList[nIndex].CheckFileExits(m_SelectFloder.m_TempFileList[i].m_FileTypeName) == false)
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
                case UseMessage.MY_MSG_5:
                    break;
            }
            base.WndProc(ref m);

        }

        private void ReturnMainForm_Click(object sender, EventArgs e)
        {
            CGlobal.HideTabCtrl();
        }

        private void Time_FreshView_Tick(object sender, EventArgs e)
        {
            Time_FreshView.Stop();
            ShowWaitReadFileList(m_SelectFloder);
        }

        private void CHisRecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ReadFileThread != null)
            {
                m_ReadFileThread.Abort();
                bReadThread = false;
                m_ReadFileThread.Join();
                m_ReadFileThread = null;
            }
            CGlobal.m_104Ptr.m_FileFomrHandle = -1;
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
            for (i = 0; i < CGlobal.m_RealFloderList.Count; i++)
            {
                CGlobal.m_RealFloderList[i].CopyFile(CGlobal.m_HisFloderList[i].m_FloderPath);
            }
            IniHiseFloderFileList();
            UpdateHisFileTreeViewList();
        }
    }
}
