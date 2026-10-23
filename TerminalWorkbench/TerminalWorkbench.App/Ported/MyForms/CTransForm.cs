using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;
using System.IO;
using Microsoft.VisualBasic;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using System.Data.OleDb;
using System.Collections;


namespace DtuTestManager
{
    public partial class CTransForm : Form
    {
        private CFloderCtrl m_NowReadFloder = null;
        private Thread m_ReadFileThread;
        private bool bReadThread = false;
        public int m_Success = 0;
        private List<CDownFileTaskMsg> m_TaskList = new List<CDownFileTaskMsg>();
        private List<CTransStation> m_StationList = new List<CTransStation>();
        public static CTransStation pSelStation = null;
        Form m_NowForm = null;
        public CTransForm()
        {
            InitializeComponent();
        }
        private void IniStationTreeView()
        {
            int i;
            for (i = 0; i < m_StationList.Count; i++)
            {
                CTransStation pStation = m_StationList[i];
                pStation.CreateTreeNode();
                StationTree.Nodes.Add(pStation.m_Nodes);
            }
        }
        private void CTransForm_Load(object sender, EventArgs e)
        {
            InitDevTermInfo();
            string[] dirs = Directory.GetDirectories("转发表配置\\");
            foreach (string dir in dirs)
            {
                CTransStation pNewStation = new CTransStation();
                pNewStation.m_StationName = dir.Split('\\')[1];
                pNewStation.m_StationPath = dir;
                m_StationList.Add(pNewStation);
            }
            if (m_StationList.Count == 0)
                return;
            IniStationTreeView();
            CTransGlobal.InitDafault();
            CGlobal.strDevModulList = new string[CGlobal.m_PriTerm.Count];
            for (int i = 0; i < CGlobal.strDevModulList.Length; i++)
            {
                CGlobal.strDevModulList[i] = CGlobal.m_PriTerm[i].m_ModulName;
            }
        }
        private void InitDevTermInfo()
        {
            CGlobal.m_PriTerm.Clear();
            DirectoryInfo TheFolder = new DirectoryInfo("DevModuleDataBase");
            if (TheFolder == null)
                return;
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Extension != ".mdb")
                    continue;
                CPriTerm temp = new CPriTerm();
                temp.m_ModulName = NextFile.Name.Substring(0, NextFile.Name.Length - 4);
                CDataBase m_DB = new CDataBase();
                m_DB.m_Path = "DevModuleDataBase" + "\\" + NextFile.Name;
                m_DB.ConnectDataBase();
                temp.ReadDataName(m_DB);
                CGlobal.m_PriTerm.Add(temp);
            }
            // InitGetSubModuleInfo();
        }
        private void StationTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode pSelectNode = StationTree.SelectedNode;
            if (pSelectNode == null)
                return;
            TreeNode pParentNode = pSelectNode.Parent;
            if (m_NowForm != null)
            {
                PanelForm.Controls.Remove(m_NowForm);
                m_NowForm.Close();
                m_NowForm = null;
            }
            if (pParentNode == null)
            {
                pSelStation = (CTransStation)pSelectNode.Tag;
                if (pSelStation.m_IsEmpty)
                {
                    if (pSelStation.IniSetAndConnSqlite())  //初始化厂站数据Sub
                    {
                        pSelStation.VisitAllPriTable();
                        pSelStation.m_IsEmpty = false;
                    }
                }
                if (pSelStation.m_IsEmpty)
                    return;
                pSelStation.IniReadStationTable();
                CTranStationOptionForm pNewForm = new CTranStationOptionForm();
                pNewForm.m_Station = pSelStation;
                m_NowForm = pNewForm;
            }
            else
            {
                if (pSelectNode.Level == 1)
                {
                    pSelStation = (CTransStation)pSelectNode.Parent.Tag;
                    if (pSelStation.m_IsEmpty)
                        return;
                    int nNodeNo = (int)pSelectNode.Tag;
                    switch (nNodeNo)
                    {
                        case 1:
                            CTermForm pTermForm = new CTermForm();
                            pTermForm.m_Station = pSelStation;
                            m_NowForm = pTermForm;
                            break;
                        case 2:
                            CPriForm pPriForm = new CPriForm();
                            pPriForm.m_Station = pSelStation;
                            m_NowForm = pPriForm;
                            break;
                        case 3:
                            break;
                    }
                }
                else
                {

                    pSelStation = (CTransStation)pSelectNode.Parent.Parent.Tag;
                    if (pSelStation.m_IsEmpty)
                        return;
                    if (pSelStation.m_Term.IsInial == false)
                    {
                        pSelStation.m_Term.LoadTerminalSet();
                    }
                    int nNodeNo = (int)pSelectNode.Tag;
                    GetPriTerminal(nNodeNo);
                    CRouteForm pRouteForm = new CRouteForm();
                    pRouteForm.m_Station = pSelStation;
                    pRouteForm.NodeType = nNodeNo;
                    m_NowForm = pRouteForm;

                }
            }
            if (m_NowForm != null)
            {
                m_NowForm.TopLevel = false; //指示子窗体非顶级窗体
                PanelForm.Controls.Add(m_NowForm);
                m_NowForm.Show();
            }
        }
        private void GetTermEachDataCnt(CDeviceConfig pTermDevice, int nNodeNo)
        {
            switch (nNodeNo)
            {
                case 0:
                    pTermDevice.m_TermDataValue[nNodeNo].nRealCnt = Convert.ToInt32(pTermDevice.m_ValueList[25].m_TranValue);
                    break;
                case 1:
                    pTermDevice.m_TermDataValue[nNodeNo].nRealCnt = Convert.ToInt32(pTermDevice.m_ValueList[26].m_TranValue);
                    break;
                case 2:
                    pTermDevice.m_TermDataValue[nNodeNo].nRealCnt = Convert.ToInt32(pTermDevice.m_ValueList[27].m_TranValue);
                    break;
                case 3:
                    pTermDevice.m_TermDataValue[nNodeNo].nRealCnt = Convert.ToInt32(pTermDevice.m_ValueList[28].m_TranValue);
                    break;
                case 4:
                    pTermDevice.m_TermDataValue[nNodeNo].nRealCnt = Convert.ToInt32(pTermDevice.m_ValueList[29].m_TranValue);
                    break;
            }
        }
        private void GetPriTerminal(int nNodeNo)
        {
            int nCnt = 0, i = 0, n = 0, nCnt1 = 0;
            pSelStation.L_PriTerm.Clear();
            string m_NodeType;
            for (i = 0; i < pSelStation.m_Term.m_TermList.Count; i++)
            {
                CDeviceConfig pTermDevice = pSelStation.m_Term.m_TermList[i];
                if (pTermDevice.m_TermDataValue[nNodeNo].m_isReadDataName == false)
                {
                    m_NodeType = (string)pTermDevice.m_ValueList[58].m_TranValue;  //
                    GetTermEachDataCnt(pTermDevice, nNodeNo);
                    pSelStation.m_Term.m_TermList[i].ReadDataINIFile(nNodeNo, m_NodeType);
                    if (pTermDevice.m_TermDataValue[nNodeNo].m_DataName.Count < pTermDevice.m_TermDataValue[nNodeNo].nRealCnt)
                    {
                        for (int j = pTermDevice.m_TermDataValue[nNodeNo].m_DataName.Count; j < pTermDevice.m_TermDataValue[nNodeNo].nRealCnt; j++)
                        {
                            string m_name = "备用";
                            pTermDevice.m_TermDataValue[nNodeNo].m_DataName.Add(m_name);
                        }
                    }
                }
                CPriTerm m_term = new CPriTerm();
                pSelStation.L_PriTerm.Add(m_term);
                nCnt = pTermDevice.m_TermDataValue[nNodeNo].nRealCnt;
                if (i == 0)
                {
                    pSelStation.L_PriTerm[i].m_Data[nNodeNo].StartAddr = 0;
                    pSelStation.L_PriTerm[i].m_Data[nNodeNo].EndAddr = nCnt - 1;
                }
                else
                {
                    pSelStation.L_PriTerm[i].m_Data[nNodeNo].StartAddr = pSelStation.L_PriTerm[i - 1].m_Data[nNodeNo].EndAddr + 1;
                    pSelStation.L_PriTerm[i].m_Data[nNodeNo].EndAddr = nCnt + pSelStation.L_PriTerm[i].m_Data[nNodeNo].StartAddr - 1;
                }
                for (n = 0; n < nCnt; n++)
                {
                    try
                    {
                        CPriTerminal temp = new CPriTerminal();
                        temp.TotalNode = n + pSelStation.L_PriTerm[i].m_Data[nNodeNo].StartAddr;
                        temp.DataNode = n;
                        temp.Name = pTermDevice.m_TermDataValue[nNodeNo].m_DataName[n];
                        pSelStation.L_PriTerm[i].m_Data[nNodeNo].m_DataName.Add(temp);
                    }
                    catch { }
                }
                if (i == 0 && nNodeNo == 0)     //第一台装置的遥信表最后增加一个通讯状态
                {
                    pSelStation.L_PriTerm[i].m_Data[nNodeNo].EndAddr++;

                    CPriTerminal temp = new CPriTerminal();
                    temp.TotalNode = n + pSelStation.L_PriTerm[i].m_Data[nNodeNo].StartAddr;
                    temp.DataNode = n;
                    temp.Name = "BF533通讯状态";
                    pSelStation.L_PriTerm[i].m_Data[nNodeNo].m_DataName.Add(temp);
                }
            }
            CPriTerm m_StationTerm = new CPriTerm();
            pSelStation.L_PriTerm.Add(m_StationTerm);
            pSelStation.IniSubStationDataList(nNodeNo);
            if (nNodeNo == 0)
            {
                nCnt = pSelStation.m_Sub.m_TermDataValue[0].nRealCnt;
                nCnt1 = pSelStation.m_Sub.m_TermDataValue[5].nRealCnt;
                if (pSelStation.m_Term.m_TermList.Count == 0)
                {
                    m_StationTerm.m_Data[nNodeNo].StartAddr = 0;
                    m_StationTerm.m_Data[nNodeNo].EndAddr = nCnt + nCnt1 - 1;
                }
                else
                {
                    m_StationTerm.m_Data[nNodeNo].StartAddr = pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 2].m_Data[nNodeNo].EndAddr + 1;
                    m_StationTerm.m_Data[nNodeNo].EndAddr = nCnt + nCnt1 + pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[nNodeNo].StartAddr - 1;
                }


                for (i = 0; i < nCnt; i++)
                {
                    try
                    {
                        CPriTerminal temp = new CPriTerminal();
                        temp.TotalNode = i + pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[nNodeNo].StartAddr;
                        temp.DataNode = i;
                        temp.Name = pSelStation.m_SubTerm.m_Data[nNodeNo].m_DataName[i].Name;
                        m_StationTerm.m_Data[nNodeNo].m_DataName.Add(temp);
                    }
                    catch { }
                }

                for (i = 0; i < nCnt1; i++)
                {
                    try
                    {
                        CPriTerminal temp = new CPriTerminal();
                        temp.TotalNode = nCnt + i + pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[nNodeNo].StartAddr;
                        temp.DataNode = i;
                        temp.Name = pSelStation.m_SubTerm.m_Data[5].m_DataName[i].Name;
                        m_StationTerm.m_Data[nNodeNo].m_DataName.Add(temp);
                    }
                    catch { }
                }

                pSelStation.TemYxSumCnt = m_StationTerm.m_Data[nNodeNo].StartAddr;
            }
            else
            {
                nCnt = pSelStation.m_Sub.m_TermDataValue[nNodeNo].nRealCnt;
                if (pSelStation.m_Term.m_TermList.Count == 0)
                {
                    m_StationTerm.m_Data[nNodeNo].StartAddr = 0;
                    m_StationTerm.m_Data[nNodeNo].EndAddr = nCnt - 1;
                }
                else
                {
                    m_StationTerm.m_Data[nNodeNo].StartAddr = pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 2].m_Data[nNodeNo].EndAddr + 1;
                    m_StationTerm.m_Data[nNodeNo].EndAddr = nCnt + pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[nNodeNo].StartAddr - 1;
                }
                for (i = 0; i < nCnt; i++)
                {
                    CPriTerminal temp = new CPriTerminal();
                    temp.TotalNode = i + pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[nNodeNo].StartAddr;
                    temp.DataNode = i;
                    temp.Name = pSelStation.m_SubTerm.m_Data[nNodeNo].m_DataName[i].Name;
                    pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[nNodeNo].m_DataName.Add(temp);
                }
                pSelStation.TemYxSumCnt = pSelStation.L_PriTerm[pSelStation.L_PriTerm.Count - 1].m_Data[0].StartAddr;
            }

        }
        private void BtnNewStation_Click(object sender, EventArgs e)
        {
            string strName = Interaction.InputBox("请输入新建厂站名称", "名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0)
                return;
            if (CheckNameExist(strName))
                return;
            string Path = "转发表配置\\" + strName;
            System.IO.Directory.CreateDirectory(Path);

            CTransStation pNewStation = new CTransStation();
            pNewStation.m_StationName = strName;
            pNewStation.m_StationPath = "转发表配置\\" + strName;
            m_StationList.Add(pNewStation);
            pNewStation.CreateTreeNode();
            StationTree.Nodes.Add(pNewStation.m_Nodes);

        }
        public bool CheckNameExist(string strNewName)
        {
            string strPath = "转发表配置\\" + strNewName;
            if (System.IO.Directory.Exists(strPath))
            {
                return true;
            }
            return false;
        }
        public void LoadFile(string FilePath)
        {
            CGlobal.m_Device.m_TransFile.m_data.Clear();
            if (CGlobal.DoWithLoadFile(FilePath) == false)
                return;
            m_TaskList.Clear();
            CGlobal.m_Device.m_TransFile.CurrentIndex = 0;
            DownFileTime.Stop();
            GetParaOperationList(29, 1, false);
            GetParaOperationList(30, CGlobal.m_Device.m_TransFile.SumFrames, true);
            DownFileTime.Start();
        }
        private CDownFileTaskMsg pNowTask = null;
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
        private void ReadFileProcess()
        {
            m_NowReadFloder = CGlobal.m_Device.m_SelectFloder;
            m_NowReadFloder.m_NowReadPalce = 0;
            while (bReadThread)
            {
                if (m_NowReadFloder == null)
                    break;
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
                        CGlobal.SendTaskOrder(23);
                    }
                    else
                    {
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
                    m_TaskList.RemoveAt(0);
                }
                else
                {
                    DownFileTime.Stop();
                    m_Success++;
                    if (m_Success < 2)
                    {
                        string FilePath = "转发表配置\\" + pSelStation.m_StationName + "\\UserCfgBase.db";
                        LoadFile(FilePath);
                    }

                }
            }
        }
        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_SelectFloder = new CFloderCtrl();
            CGlobal.m_Device.m_SelectFloder.m_FloderPath = "转发表配置\\" + pSelStation.m_StationName;
            CFileCtrl pfile = new CFileCtrl();
            pfile.m_FileTypeName = "MainCfgBase.db";
            pfile.m_FilePath = "转发表配置\\" + pSelStation.m_StationName + "\\" + "MainCfgBase.db";
            CGlobal.m_Device.m_SelectFloder.m_TempFileList.Add(pfile);
            pfile = new CFileCtrl();
            pfile.m_FileTypeName = "UserCfgBase.db";
            pfile.m_FilePath = "转发表配置\\" + pSelStation.m_StationName + "\\" + "UserCfgBase.db";
            CGlobal.m_Device.m_SelectFloder.m_TempFileList.Add(pfile);
            bool bReadFlag = false;
            for (int i = 0; i < CGlobal.m_Device.m_SelectFloder.m_TempFileList.Count; i++)
            {
                CGlobal.m_Device.m_SelectFloder.m_TempFileList[i].m_ReadSelect = true;
                if (CGlobal.m_Device.m_SelectFloder.m_TempFileList[i].m_ReadSelect)
                {
                    bReadFlag = true;
                    CGlobal.m_Device.m_SelectFloder.m_TempFileList[i].IniReadFile();
                }
            }
            if (bReadFlag)
            {
                bReadThread = true;
                m_ReadFileThread = new Thread(new ThreadStart(this.ReadFileProcess));
                m_ReadFileThread.IsBackground = true;
                m_ReadFileThread.Start();
                // BtnEnableTime.Start();
            }
        }
        private void BtnUpdateFile_Click(object sender, EventArgs e)
        {
            m_Success = 0;
            string FilePath = "转发表配置\\" + pSelStation.m_StationName + "\\" + "MainCfgBase.db";
            LoadFile(FilePath);
        }
        private void BtnSFTP_Click(object sender, EventArgs e)
        {
            if (pSelStation == null)
            {
                MessageBox.Show("请先选择一个厂站！");
                return;
            }
            CSFTPForm pform = new CSFTPForm();
            pform.m_SelPath = pSelStation.m_StationPath;
            pform.ShowDialog();
        }

        private void CTransForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
