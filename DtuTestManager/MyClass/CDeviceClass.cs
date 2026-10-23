using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Threading;

namespace DtuTestManager
{
    public class CDeviceClass
    {    
        public byte[] MD5 = new byte[16];
        public  SystemConnection m_Connection = new SystemConnection();
        public int m_ASDUAddr;          //地址
        public bool m_bOnlineStatus = false;                       //当前连接状态
        public CModuleClass m_DeviceMsg = null;

        public int m_SelectDownFileType = 0;        //0为文件传输窗口，1为远程升级窗口
        public int m_MainFormHandle=0;                //主窗口句柄
     
        public int m_ParaHandle = 0;                //参数窗口句柄
        public int m_ParaFormType = 0;   //1表示参数配置界面，2表示工程参数界面
      
        public int m_FileFormHandle = 0;            //文件窗口句柄
        public int m_DownFileFormHandle = 0;        //下发文件窗口句柄
        public int m_YkFormHandle = 0;              //遥控窗口句柄
        public int m_UpdataFormHandle = 0;          //远程升级窗口句柄
        public int m_EncodeFormHandle = 0;          //远程加密窗口句柄
        public int m_CommuCFGFormHandle=0;                //主窗口句柄
        public int m_SpeParaFormHandle = 0;                //产站参数界面
        public string m_DevID = "";                //装置ID
        public List<CTetstCasePro> m_TestCaseList = new List<CTetstCasePro>();


        private string m_YcTableName;       //遥测表名称
        private string m_YxTableName;       //遥信表名称
        private string m_YkTableName;       //遥控表名称
        private string m_PowerTableName;    //电量表名称

        private string m_ParaRootTableName; //参数总目录表名称
        private string m_ParaGroupName;     //参数组表名称
        private string m_ParaTableName;     //参数表名称

        //private string m_SoeTableName;      //SOE表名称
        private string m_RealFormTableName; //实时主界面配置

        public CTransFile m_TransFile = new CTransFile();
        public CFloderCtrl m_SelectFloder = null;
        public CFileCtrl m_NowReadFile = null;


     

        //实时更新表
        public List<CYxOption> m_YxList = new List<CYxOption>();
        public List<CYcOption> m_YcList = new List<CYcOption>();
        public List<CYkOption> m_YkList = new List<CYkOption>();
        public List<CPowerOption> m_PowerList = new List<CPowerOption>();

        public List<CParaRootTable> m_ParaList = new List<CParaRootTable>();
        public List<CParaCtrl> m_TotalParaDataList = new List<CParaCtrl>();






        public List<CParaCtrl> m_SelectParaList = new List<CParaCtrl>();
        public List<MainRealYc> m_MainRealIndex = new List<MainRealYc>();      //实时界面索引

        public  List<List<CCheckdata>> m_AC_Checkdata = new List<List<CCheckdata>>();     //交流量
        public  List<CDCCheckdata> m_DC_Checkdata = new List<CDCCheckdata>();             //直流量
        public  List<List<CPhasedata>> m_Phasedata = new List<List<CPhasedata>>();        //绝对相角
        public  int SelLineNo = 0;//选择的线路号

        public  List<CFloderCtrl> m_RealFloderList = new List<CFloderCtrl>();
        public  List<CFloderCtrl> m_HisFloderList = new List<CFloderCtrl>();

        public CSoeCosRecord m_SoeClass = null;
        public CSoeCosRecord m_CosClass = null;
        public CEventRecord m_EventClass = null;
        public  CDevVersion m_Version = new CDevVersion();

        public CSocketDevice m_SocketDevice = new CSocketDevice();
        public CComDevice m_ComDevice = new CComDevice();
        public CPtrInterface m_Ptr = null;


        public byte[] m_SendBuff = new byte[1024];
        public  int m_SendLeng = 0;
        public  CParaGroupTable     m_ParaSelectGroup = null;

        public bool m_bRatioFresh = false;
        public bool m_bCheckFresh = false;
        public bool m_bPhaseFresh = false;
        public bool m_bYcFresh = false;
        public bool m_bYxFresh = false;
        public bool m_bPwFresh = false;
        public bool m_bParaFresh = false;

        public CCallFileTime m_CallFileTime = new CCallFileTime();
        private Object m_SendLock = new object();


        private Object m_ProcessLock = new object();
        public bool m_ReceiveFlag = false;

        private Thread m_ProcessThread;
        private bool m_bProcessFlag = true;

        private int m_ByteLeng = 10240;
        private byte[] m_ReceiveBuff = new byte[10240];
        private int m_RecLeng = 0;
		private int m_StartPlace = 0;
		private int m_EndPlace = 0;
        private byte[] m_ProcessBuff = new byte[10240];
        private int m_ProcessLeng = 0;

        //装置原始全表
        public List<CYxOption> m_YxList1 = new List<CYxOption>();
        public List<CYcOption> m_YcList1 = new List<CYcOption>();
        public List<CYkOption> m_YkList1 = new List<CYkOption>();
        public List<CPowerOption> m_PowerList1 = new List<CPowerOption>();
        public List<CParaRootTable> m_ParaList1 = new List<CParaRootTable>();
        public string m_ModeName = "";
        public bool m_bYcLVFresh = false;
        public bool m_bYxLVFresh = false;
        public bool m_bPwLVFresh = false;
        public List<CParaCtrl4G> m_SelectParaList4G = new List<CParaCtrl4G>();
        public bool GetYcLVFreshStatus()
        {
            if (m_bYcLVFresh)
            {
                m_bYcLVFresh = false;
                return true;
            }
            return false;
        }
        public bool GetYxLVFreshStatus()
        {
            if (m_bYxLVFresh)
            {
                m_bYxLVFresh = false;
                return true;
            }
            return false;
        }
        public bool GetPwLVFreshStatus()
        {
            if (m_bPwLVFresh)
            {
                m_bPwLVFresh = false;
                return true;
            }
            return false;
        }
        public CDeviceClass()
        {
            m_ComDevice.theDevice = this;
            m_SocketDevice.theDevice = this;          
            m_SelectFloder = null;
            m_NowReadFile = null;
            m_ASDUAddr = 1;
            m_bYcFresh = false;
            m_bYxFresh = false;
            m_bPwFresh = false;
            m_bParaFresh = false;
            IniTestCaseList();
        }
        private void IniTestCaseList()
        {
            CTetstCasePro   pTestCase = null;
            pTestCase = new CTetstCasePro(1000,"请求链路状态");
            m_TestCaseList.Add(pTestCase);
            pTestCase.AddTestCaseItem("链路:正确命令");
            pTestCase.AddTestCaseItem("链路:控制域错误命令");
            pTestCase.AddTestCaseItem("链路:地址错误");
            pTestCase.AddTestCaseItem("链路:校验和错误");
            pTestCase.AddTestCaseItem("链路:结束符错误");

            pTestCase = new CTetstCasePro(2000, "请求链路复位");
            m_TestCaseList.Add(pTestCase);
            pTestCase.AddTestCaseItem("复位:正确命令");
            pTestCase.AddTestCaseItem("复位:控制域错误命令");

            pTestCase = new CTetstCasePro(3000, "总召唤");
            m_TestCaseList.Add(pTestCase);
            pTestCase.AddTestCaseItem("正确命令");
            pTestCase.AddTestCaseItem("地址错误命令");
            pTestCase.AddTestCaseItem("控制域错误命令");
            pTestCase.AddTestCaseItem("传送原因错误命令");
            pTestCase.AddTestCaseItem("长度错误命令");
            pTestCase.AddTestCaseItem("校验和错误");
            pTestCase.AddTestCaseItem("结束符错误");

            pTestCase = new CTetstCasePro(4000, "对时命令");
            m_TestCaseList.Add(pTestCase);
            pTestCase.AddTestCaseItem("正确命令");
            pTestCase.AddTestCaseItem("传送原因错误命令");
            pTestCase.AddTestCaseItem("长度错误命令");
            pTestCase.AddTestCaseItem("年错误");
            pTestCase.AddTestCaseItem("月错误");
            pTestCase.AddTestCaseItem("日错误");
            pTestCase.AddTestCaseItem("时错误");
            pTestCase.AddTestCaseItem("分错误");
            pTestCase.AddTestCaseItem("秒错误");
           // pTestCase.AddTestCaseItem("月日匹配错误");
            pTestCase.AddTestCaseItem("时钟查询");


            pTestCase = new CTetstCasePro(5000, "遥控命令");
            m_TestCaseList.Add(pTestCase);
            pTestCase.AddTestCaseItem("单点：传送原因错误命令");
            pTestCase.AddTestCaseItem("单点：长度错误命令");

            pTestCase.AddTestCaseItem("单点：正确合闸选择命令");
            pTestCase.AddTestCaseItem("单点：正确合闸执行命令");
            pTestCase.AddTestCaseItem("单点：正确合闸撤销命令");

            pTestCase.AddTestCaseItem("单点：正确分闸选择命令");
            pTestCase.AddTestCaseItem("单点：正确分闸执行命令");
            pTestCase.AddTestCaseItem("单点：正确分闸撤销命令");

            pTestCase.AddTestCaseItem("双点：正确合闸选择命令");
            pTestCase.AddTestCaseItem("双点：正确合闸执行命令");
            pTestCase.AddTestCaseItem("双点：正确合闸撤销命令");

            pTestCase.AddTestCaseItem("双点：正确分闸选择命令");
            pTestCase.AddTestCaseItem("双点：正确分闸执行命令");
            pTestCase.AddTestCaseItem("双点：正确分闸撤销命令");

            pTestCase.AddTestCaseItem("单点：地址越界合闸选择");
            pTestCase.AddTestCaseItem("单点：地址越界合闸执行");
            pTestCase.AddTestCaseItem("双点：地址越界合闸选择");
            pTestCase.AddTestCaseItem("双点：地址越界合闸执行");

        }
        public void AddProcessBuff(byte[] pReadBuff, int pReadLeng)
        {
            if (pReadLeng == 0) return;
            Monitor.Enter(m_ProcessLock);
            int nTempEnd = m_EndPlace + pReadLeng;
            if (nTempEnd <= m_ByteLeng)
            {
                Array.Copy(pReadBuff, 0, m_ReceiveBuff, m_EndPlace, pReadLeng);
            }
            else
            {
                int nOneLeng = m_ByteLeng - m_EndPlace;
                int nTwoLeng = pReadLeng - nOneLeng;
                Array.Copy(pReadBuff, 0, m_ReceiveBuff, m_EndPlace, nOneLeng);
                Array.Copy(pReadBuff, nOneLeng, m_ReceiveBuff, 0, nTwoLeng);
            }
            m_EndPlace += pReadLeng;
            m_EndPlace %= m_ByteLeng;
            m_RecLeng += pReadLeng;
            if (m_RecLeng > m_ByteLeng)
            {
                m_RecLeng -= m_ByteLeng;
            }
            Monitor.Exit(m_ProcessLock);
        }
        public void SetStartPlace(int nGetLeng)
        {
            if (nGetLeng == 0) return;
            Monitor.Enter(m_ProcessLock);
            m_StartPlace += nGetLeng;
            m_StartPlace %= m_ByteLeng;
            m_RecLeng -= nGetLeng;
            Monitor.Exit(m_ProcessLock);
        }
        public void CloseReadSoketThread()
        {
            m_ReceiveFlag = false;
            m_SocketDevice.CloseReadSoketThread();
        }
        public int GetProcessBuff(byte[] ProcessBuff)
        {
            if (m_RecLeng == 0) return 0;
            Monitor.Enter(m_ProcessLock);
            if (m_EndPlace > m_StartPlace)
            {
                Array.Copy(m_ReceiveBuff, m_StartPlace, ProcessBuff, 0, m_RecLeng);
                if ((m_EndPlace - m_StartPlace) != m_RecLeng)
                {
                    return 0;
                }
            }
            else
            {
                int nSectionOne = m_ByteLeng - m_StartPlace;
                int nSectionTwo = m_EndPlace;
                Array.Copy(m_ReceiveBuff, m_StartPlace, ProcessBuff, 0, nSectionOne);
                if (nSectionTwo > 0) Array.Copy(m_ReceiveBuff, 0, ProcessBuff, nSectionOne, nSectionTwo);
                if ((nSectionOne + nSectionTwo) != m_RecLeng)
                {
                    return 0;
                }              
            }
            Monitor.Exit(m_ProcessLock);
            return m_RecLeng;
        }
        private void ProcessThread()
        {
            Byte[] pDataBuff = new Byte[4096];
            Byte[] pSendBuff = new Byte[4096];
            int pSendLeng = 0;
            int nDataPlace = 0;
            int nDataLeng = 0;
            while (m_bProcessFlag)
            {
                Thread.Sleep(5);
                nDataLeng = 0;
                nDataPlace = 0;
                ///获取发送缓冲区所有的报文
                m_ProcessLeng = GetProcessBuff(m_ProcessBuff);
                ///如果接收暂停且无数据处理，则暂停
                if (m_ProcessLeng == 0 && m_ReceiveFlag == false) continue;

                if (m_ProcessLeng > 0)
                {
                    ///nDataPlace:处理报文总长度，可能包含异常报文
                    ///nDataLeng：有效数据区长度 nDataPlace >= nDataLeng
                    nDataPlace = m_Ptr.GetTheRightBuffContent(m_ProcessBuff, m_ProcessLeng, pDataBuff, ref nDataLeng);
                    if (nDataPlace > 0)
                    {
                        SetStartPlace(nDataPlace);
                        CGlobal.ShowBuffer("接收", m_ProcessBuff, nDataPlace);
                    }
                }
                ///清空发送缓冲区
                Array.Clear(pSendBuff, 0, pSendBuff.Length);
                pSendLeng = 0;
                ///处理接收报文，并获取要发送的报文
                Process(pDataBuff, nDataLeng, pSendBuff, ref pSendLeng);
                //如果无需应答则获取需要自动发送的测试命令
                if (pSendLeng == 0)
                {
                    GetTestOrSBuff(pSendBuff, ref pSendLeng);
                }
                ///如果有数据需要发送
                if (pSendLeng > 0)
                {
                   m_bOnlineStatus=SendToDevice(pSendBuff, pSendLeng);
                   m_bProcessFlag = m_bOnlineStatus;
                   if (m_bOnlineStatus == false)
                   {
                       int tt = 0;
                   }
                }
            }
            m_bProcessFlag = false;
        }
        public void IniCreateProcessThread()
        {
            m_bProcessFlag = true;
            m_ProcessThread = new Thread(new ThreadStart(this.ProcessThread));
            m_ProcessThread.IsBackground = true;
            m_ProcessThread.Start();
        }
        public void CloseProcessThread()
        {
            m_bProcessFlag = false;
            Thread.Sleep(500);
        }
        public void InitConnect()
        {

            if (m_Connection.m_n_ProType == 0)
            {
                m_Ptr = new C101Ptr();
                CUpdataForm.bNeedFlag = false;
                m_Ptr.theDevice = this;
                m_ASDUAddr = m_Connection.m_101Address.m_AsduAddress;
                m_Ptr.m_YxStartAddress = m_Connection.m_101Address.m_YxStartAddress;
                m_Ptr.m_YcStartAddress = m_Connection.m_101Address.m_YcStartAddress;
                m_Ptr.m_YkSrartAddress = m_Connection.m_101Address.m_YkSrartAddress;
                m_Ptr.m_PowerStartAddress = m_Connection.m_101Address.m_PowerStartAddress;
                m_Ptr.m_AddressType = m_Connection.m_101Address.m_AddressType; 
                m_Ptr.m_CotType = m_Connection.m_101Address.m_CotType;
            }
            else
            {
                m_Ptr = new C104Ptr();
                CUpdataForm.bNeedFlag = true;
                m_Ptr.theDevice = this;
                m_ASDUAddr = m_Connection.m_104Address.m_AsduAddress;
                m_Ptr.m_YxStartAddress = m_Connection.m_104Address.m_YxStartAddress;
                m_Ptr.m_YcStartAddress = m_Connection.m_104Address.m_YcStartAddress;
                m_Ptr.m_YkSrartAddress = m_Connection.m_104Address.m_YkSrartAddress;
                m_Ptr.m_PowerStartAddress = m_Connection.m_104Address.m_PowerStartAddress;
            }
        }
        public void InitialzeSetDeviceModuleOption()
        {
            m_YcTableName = "Mod_YcTable";
            m_YxTableName = "Mod_YxTable";
            m_YkTableName = "Mod_YkTable";
            m_PowerTableName = "Mod_PowerTable";

            m_ParaRootTableName = "Mod_ParaRoot";
            m_ParaGroupName = "Mod_ParaGroup";
            m_ParaTableName = "Mod_ParaTable";
            //m_SoeTableName = "Mod_SoeTable";
            m_RealFormTableName = "Mod_MainRealTable";

            m_SoeClass = new CSoeCosRecord();
            m_CosClass = new CSoeCosRecord();
            m_EventClass = new CEventRecord();
        }
        private void InitialzeReadYc()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_YcTableName + " order by YcAddress";
            pTable = (DataTable)m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            if (pTable == null) return;
            m_YcList.Clear();
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CYcOption pNewYc = new CYcOption();
                pNewYc.m_YcNo = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewYc.m_YcName = m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewYc.m_YcRatio = m_DeviceMsg.m_ModuleDB.GetTableFloatValue(pTable, i, 2);
                pNewYc.m_YcOffset = m_DeviceMsg.m_ModuleDB.GetTableFloatValue(pTable, i, 3);
                pNewYc.m_YcUnit = m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 4);
                pNewYc.m_YcAddress = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 5);
                pNewYc.m_YcValue = 0;
                m_YcList.Add(pNewYc);
            }
        }
        private void InitialzeReadYx()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_YxTableName + " order by YxAddress";
            pTable = (DataTable)m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            if (pTable == null) return;
            m_YxList.Clear();
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CYxOption pNewYx = new CYxOption();
                pNewYx.m_YxNo = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewYx.m_YxName = m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewYx.m_YxAddress = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 2);
                pNewYx.m_YxValue = 0;
                m_YxList.Add(pNewYx);
            }
        }
        private void InitialzeReadYk()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_YkTableName + " order by YkAddress";
            pTable = (DataTable)m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            if (pTable == null) return;
            m_YkList.Clear();
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CYkOption pNewYk = new CYkOption();
                pNewYk.m_YkNo = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewYk.m_YkName = m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewYk.m_YkAddress = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 2);
                m_YkList.Add(pNewYk);
            }
        }
        private void InitialzeReadPower()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_PowerTableName + " order by PowerAddress";
            pTable = (DataTable)m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            if (pTable == null) return;
            m_PowerList.Clear();
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPowerOption pNewPower = new CPowerOption();
                pNewPower.m_PowerNo = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewPower.m_PowerName = m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewPower.m_PowerUnit = m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 2);
                pNewPower.m_PowerAddress = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 3);
                pNewPower.m_PowerValue = 0;
                m_PowerList.Add(pNewPower);
            }
        }
        private void InitialzeReadPara()
        {
            string strRootSql;
            DataTable pRootTable;
            int i;
            m_ParaList.Clear();

            strRootSql = "SELECT  *  FROM " +   m_ParaRootTableName +  "  order by RootNo";
            pRootTable = (DataTable)m_DeviceMsg.m_ModuleDB.SelectDataTable(strRootSql);
            if (pRootTable == null) return;
            for(i=0; i<pRootTable.Rows.Count; i++)
            {
                CParaRootTable pNewRoot = new CParaRootTable();
                pNewRoot.m_ParaDB = m_DeviceMsg.m_ModuleDB;
                pNewRoot.m_RootNo = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pRootTable, i, 0);
                pNewRoot.m_RootName = m_DeviceMsg.m_ModuleDB.GetTableString(pRootTable, i, 1);
                pNewRoot.m_RootIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pRootTable, i, 2);
                pNewRoot.IniReadGroup(m_ParaGroupName, m_ParaTableName);
                m_ParaList.Add(pNewRoot);
            }
        }
        public  CParaGroupTable GetParaGroup(int nGroupIndex)
        {
            int i, j;
            for(i=0; i< m_ParaList.Count;i++)
            {
                for(j=0;j<m_ParaList[i].m_GroupList.Count;j++)
                {
                    if(m_ParaList[i].m_GroupList[j].m_GroupNo == nGroupIndex) return m_ParaList[i].m_GroupList[j];
                }
            }
            return null;
        }
        public void UpdateNowParaDesc(CParaGroupTable   pUpdateGroup)
        {
            if(pUpdateGroup == null) return;
            string strSql;
            for (int i = 0; i < pUpdateGroup.m_ParaDataList.Count; i++)
            {
                CParaCtrl pPara = pUpdateGroup.m_ParaDataList[i];
                strSql = "Update " + m_ParaTableName + " set ParaLimit='" + pPara.m_ParaLimit +
                        "' where(ParaTableID=" + pUpdateGroup.m_GroupNo.ToString() +
                        " and ParaID=" + pPara.m_ParaNo.ToString() + ")";
                m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            }
            MessageBox.Show("更新完成!");
        }
        public void SaveNowPara(CParaGroupTable   pUpdateGroup)
        {
            if(pUpdateGroup == null) return;
            string strSql;
            int nCheck = 0;
            for(int i=0;i<pUpdateGroup.m_ParaDataList.Count;i++)
            {
                CParaCtrl pPara = pUpdateGroup.m_ParaDataList[i];
                if(pPara.m_Select)
                    nCheck = 1;
                else
                    nCheck = 0;
                strSql = "Update " + m_ParaTableName + " set ParaValue='" + pPara.m_ParaValue.ToString() +
                    "',ParaSelect=" + nCheck.ToString() + " where(ParaTableID=" + pUpdateGroup.m_GroupIndex.ToString() +
                    " and ParaID=" + pPara.m_ParaNo.ToString() + ")";
                m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            }
        }
        private void IniReadRealYxList()
        {
            string strSql;
            DataTable pDataTable;
            int nRow;
            int i;
            strSql = "SELECT  * FROM  " + m_RealFormTableName + "  order by LineNo";
            pDataTable = (DataTable)m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            if (pDataTable == null) return;
            nRow = pDataTable.Rows.Count;
            m_MainRealIndex.Clear();
            int nCol = 0;
            for (i = 0; i < nRow; i++)
            {
                nCol = 0;
                MainRealYc pIndex = new MainRealYc();
                pIndex.m_LineNo = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_CIaIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_CIbIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_CIcIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_BIaIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_BIbIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_BIcIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_IoIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_PhAIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_PhBIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_PhCIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UaIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UbIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UcIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UabIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UbcIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UcaIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_UoIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_PIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_QIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_CosIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_FIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                pIndex.m_YxIndex = m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, nCol++);
                m_MainRealIndex.Add(pIndex);
            }
        }
        public void InitializationReadDataList()
        {
            InitialzeReadYc();
            InitialzeReadYx();
            InitialzeReadYk();
            InitialzeReadPower();
            InitialzeReadPara();
            IniReadRealYxList();
        }
        public void SendTaskOrder(int nTaskNo)
        {
            if (m_bOnlineStatus == false) return;
            Array.Clear(m_SendBuff, 0, m_SendBuff.Length);
            m_SendLeng = m_Ptr.GetTaskBuffer(m_SendBuff, nTaskNo);
           m_bOnlineStatus= SendToDevice(m_SendBuff, m_SendLeng);
           if (m_bOnlineStatus == false)
           {
               int tt = 0;
           }
        }
        public bool SendToDevice(byte[] SendBuff, int SendLen)
        {
            bool bSendFlag = false;
            Monitor.Enter(m_SendLock);
            if (SendLen > 0)
            {
                if (m_Connection.m_nConnType == 0)
                {
                    bSendFlag = m_ComDevice.SendToComPort(SendBuff, SendLen);
                }
                else
                {
                    bSendFlag = m_SocketDevice.SendToSocket(SendBuff, SendLen);
                }
            }
            Monitor.Exit(m_SendLock);
            if (bSendFlag) CGlobal.ShowBuffer("发送", SendBuff, SendLen);
            if (m_Connection.m_nConnType == 0)
            {
                if (SendLen < 100)
                {
                    Thread.Sleep(5);
                }
            }
            else
            {
                if (CGlobal.m_Device.m_Connection.m_SocketMode == 1)
                {
                    Thread.Sleep(200);
                }
            } 
            return bSendFlag;
        }        
        public void CloseThread()
        {
            CloseProcessThread();
            if (m_Connection.m_nConnType == 0)
            {
                m_ComDevice.CloseThread();
            }
            else
            {
                m_SocketDevice.CloseThread();
            }
        }
        public bool OpenConnChannel()
        {
            if (m_Connection.m_nConnType == 0)
            {
                m_bOnlineStatus = m_ComDevice.OpenComPort(m_Connection.m_strutComOption);
            }
            else
            {
                if (m_Connection.m_SocketMode == 0)
                    m_bOnlineStatus = m_SocketDevice.SocketConnect(m_Connection.m_strIP_Address, m_Connection.m_nIP_Port, 300, 2);
                else
                    m_bOnlineStatus = m_SocketDevice.SocketConnect(m_Connection.m_nIP_Port);
            }
            return m_bOnlineStatus;
        }
        public void Process(Byte[] pRecvBuff, int pRecvLeng, Byte[] pSendBuff, ref int pSendLeng)
        {
            m_Ptr.Process(pRecvBuff, ref pRecvLeng, pSendBuff, ref pSendLeng);
        }
        public void GetTestOrSBuff(Byte[] pSendBuff, ref int pSendLeng)
        {
            m_Ptr.GetTestOrSBuff(pSendBuff, ref pSendLeng);
        }
        public void SetParaValue(int nParaAddress,string strValue)
        {
            int i, j, k;
            if (CGlobal.m_Device.m_ParaFormType == 1)
            {
                for (i = 0; i < m_ParaList.Count; i++)
                {
                    for (j = 0; j < m_ParaList[i].m_GroupList.Count; j++)
                    {
                        if (m_ParaList[i].m_GroupList[j].m_ParaDataList.Count > 0)
                        {
                            for (k = 0; k < m_ParaList[i].m_GroupList[j].m_ParaDataList.Count; k++)
                            {
                                if (nParaAddress < m_ParaList[i].m_GroupList[j].m_StartAddress) break;
                                CParaCtrl pPara = m_ParaList[i].m_GroupList[j].m_ParaDataList[k];
                                if (pPara.m_ParaAddress == nParaAddress)
                                {
                                    pPara.m_ParaValue = strValue;
                                    pPara.m_Fresh = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (CGlobal.m_Device.m_ParaFormType == 2)
            {
                for (i = 0; i < CEngPara.m_CurrentParaDataList.Count; i++)
                {
                    if (CEngPara.m_CurrentParaDataList[i].m_ParaAddress == nParaAddress)
                    {
                        CEngPara.m_CurrentParaDataList[i].m_ParaValue = strValue;
                        CEngPara.m_CurrentParaDataList[i].m_Fresh = true;
                    }
                }
            }

        }
        public bool GetRatioFreshStatus()
        {
            if (m_bRatioFresh)
            {
                m_bRatioFresh = false;
                return true;
            }
            return false;
        }
        public bool GetCheckFreshStatus()
        {
            if (m_bCheckFresh)
            {
                m_bCheckFresh = false;
                return true;
            }
            return false;
        }
        public bool GetPhaseFreshStatus()
        {
            if (m_bPhaseFresh)
            {
                m_bPhaseFresh = false;
                return true;
            }
            return false;
        }
        public bool GetYcFreshStatus()
        {
            if (m_bYcFresh)
            {
                m_bYcFresh = false;
                return true;
            }
            return false;
        }
        public bool GetYxFreshStatus()
        {
            if (m_bYxFresh)
            {
                m_bYxFresh = false;
                return true;
            }
            return false;
        }
        public bool GetPwFreshStatus()
        {
            if (m_bPwFresh)
            {
                m_bPwFresh = false;
                return true;
            }
            return false;
        }
        public bool GetParaFreshStatus()
        {
            if (m_bParaFresh)
            {
                m_bParaFresh = false;
                return true;
            }
            return false;
        }
        public void Set4GParaValue(int nParaAddress, object strValue)
        {
            int i;
            for (i = 0; i < CGlobal.m_4GList.Count; i++)
            {
                CParaCtrl4G pPara = CGlobal.m_4GList[i];
                if (pPara.m_ParaAddress == nParaAddress)
                {
                    if (pPara.m_ParaType == ParaType.IP)
                    {
                        object tempIP;
                        tempIP = Convert.ChangeType(strValue, typeof(uint));
                        Byte[] pValueByte = null;
                        pValueByte = System.BitConverter.GetBytes((uint)tempIP);
                        pPara.m_ParaValue = pValueByte[0].ToString() + "." + pValueByte[1].ToString() + "." + pValueByte[2].ToString() + "." + pValueByte[3].ToString();


                    }
                    else
                        pPara.m_ParaValue = strValue;
                    break;
                }
            }
        }
    }
    public class CCallFileTime
    {
        public  Byte m_CallType = 0;
        public  Byte[] m_StartTime = new Byte[7];
        public  Byte[] m_EndTime = new Byte[7];
        public void IniTime()
        {
            int i;
            m_CallType = 0;
            for (i = 0; i < 7; i++)
            {
                m_StartTime[i] = 0;
                m_EndTime[i] = 0;
            }
        }
    }
}
