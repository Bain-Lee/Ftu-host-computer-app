using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;     //浮动窗口
namespace DtuTestManager
{
    public partial class MainForm : Form
    {
        Form pLoadForm = null;     
        public Object m_SaveRepLock = new object();   
        public MainForm()
        {
            InitializeComponent();
        }      
        protected override void WndProc(ref Message m)
        {            
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1:
                    this.MainContain.Panel2Collapsed = true;
                    break;
                case UseMessage.MY_MSG_2: //产生事件就刷新
                    IniEventView();
                    CGlobal.m_EventView.FreshEventView();
                    break;
                case UseMessage.MY_MSG_3: //产生SOE就刷新
                    IniSOEView();
                    CGlobal.m_SOEView.FreshSOEView();
                    break;
                case UseMessage.MY_MSG_5:           ///心跳超时中断
                    if (CGlobal.m_Device.m_Connection.m_SocketMode != 0)
                    {
                        CGlobal.m_Device.CloseReadSoketThread();
                    }
                    else
                    {
                        Btn_StopRun_Click(null, null);
                    }
                    //MessageBox.Show("心跳3次无应答，通讯中断");
                    break;
                case UseMessage.MY_MSG_6:
                    MessageBox.Show("应答超时！");
                    break;              
            }
            base.WndProc(ref m);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            CGlobal.ReadSysSetting();           ///读取配置文件     
            if (IniReadSqlData() == false) return;
            CGlobal.m_Device.m_MainFormHandle = this.Handle.ToInt32();
            string LogPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\Log.png";
            TopLogPic.BackgroundImage = Image.FromFile(LogPic);
            TopLogPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            CGlobal.m_SecretEncode.CreateSecretDevice();           
            Btn_Output_Click(sender, e);
            pLoadForm = new CRealCtrlForm();
            FreshForm();
        }   
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {              
                CGlobal.m_Device.CloseThread();
                CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.CloseDatabase();
                CGlobal.m_BufferView = null;
                CGlobal.m_EventView = null;
                CGlobal.m_SOEView = null;
                System.Environment.Exit(0);
            }
            catch
            {
                System.Environment.Exit(0);
            }
        }
        public void GetDevModuleFile(string strFloder)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(strFloder);
            if (TheFolder == null) return;
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Extension != ".mdb") continue;
                CModuleClass pNewModule = new CModuleClass();
                pNewModule.m_ModuleDB.m_Path = strFloder + "\\" + NextFile.Name;
                string[] pStr = NextFile.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                pNewModule.m_ModuleName = pStr[0];
                pNewModule.m_ModuleDB.ConnectDataBase();
                pNewModule.InitialzeReadOption();
                pNewModule.m_ModuleDB.CloseDatabase();
                CGlobal.m_ModuleList.Add(pNewModule);
            }
        }
        private bool IniReadSqlData()
        {
            CGlobal.m_ModuleList.Clear();
            GetDevModuleFile("DefaultDevModule");
            GetDevModuleFile("DevModuleDataBase");
            GetDevModuleFile("工程模板");
            if (CGlobal.m_ModuleList.Count == 0) return false;
            CGlobal.m_Device.m_DeviceMsg = CGlobal.m_ModuleList[0];
            for (int i = 0; i < CGlobal.m_ModuleList.Count; i++)
            {
                if (CGlobal.m_Device.m_Connection.m_DevName == CGlobal.m_ModuleList[i].m_ModuleName)
                {
                    CGlobal.m_Device.m_DeviceMsg = CGlobal.m_ModuleList[i];
                    break;
                }
            }
            CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.ConnectDataBase();
            CGlobal.m_Device.InitialzeSetDeviceModuleOption();
            CGlobal.m_Device.InitializationReadDataList();           
            return true;
        }                   
        private void SetAndRunButton(bool bStatus)
        {
            Btu_RunStart.Enabled = !bStatus;
            Btn_StopRun.Enabled = bStatus;
            Btn_ConSet.Enabled = !bStatus;
            Btn_ModeSet1.Enabled = !bStatus;
        }
        public void FreshForm()
        {
            pLoadForm.TopLevel = false; //指示子窗体非顶级窗体
            pLoadForm.Dock = DockStyle.Fill;
            myPanelForm.SuspendLayout();
            try
            {
                myPanelForm.Controls.Clear();
                myPanelForm.Controls.Add(pLoadForm);//将子窗体载入panel
                pLoadForm.Show();
            }
            finally
            {
                myPanelForm.ResumeLayout(true);
            }
            this.Focus();
        }
        private bool StartConnDevice()
        {           
            if (CGlobal.m_Device.m_bOnlineStatus == false)
            {              
                CGlobal.m_Device.CloseThread();
                this.Cursor = Cursors.WaitCursor;//等待
                if (CGlobal.m_Device.OpenConnChannel())     //连接装置
                {
                    if (CGlobal.m_Device.m_Connection.IsEncode == true)         ///判断加密状态
                    {
                        if (CGlobal.m_SecretEncode.ConnectEncodeDev() == false)     ///连接加密机
                        {
                            CGlobal.m_Device.CloseThread();
                        }
                    }
                    if (CGlobal.m_Device.m_bOnlineStatus)
                    {
                        CGlobal.m_Device.IniCreateProcessThread();  ///创建数据处理线程  
                        if (CGlobal.m_Device.m_Connection.m_SocketMode == 0 && CGlobal.m_RunSendMode)            ///客户端模式，可允许发送判断
                        {
                            if (CGlobal.m_Device.m_Connection.IsEncode)
                            {
                                CGlobal.SendTaskOrder((int)EncodeStatus.Auto_Master_Indentity);
                            }
                            else
                            {
                                CGlobal.SendTaskOrder(1);
                            }
                        }
                    }
                    SetAndRunButton(true);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;                  
                }
                FreshTimer.Start();
                timer1.Start();
            }
            return CGlobal.m_Device.m_bOnlineStatus;
        }
        private void Btu_RunStart_Click(object sender, EventArgs e)
        {                 
            CGlobal.m_RunSendMode = true;          
            StartConnDevice();           
        }
        private void Btn_StopRun_Click(object sender, EventArgs e)
        {
            if (CGlobal.m_Device.m_bOnlineStatus)
            {
                CGlobal.m_Device.CloseThread();
                CGlobal.m_SecretEncode.CloseEncode();  //每次退出都需要释放句柄
                SetAndRunButton(false);
                FreshTimer.Stop();               
                timer1.Stop();
            }           
        }
        private void BtnLuBo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "录波文件(*.cfg)|*.cfg";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CFileCtrl pFile = new CFileCtrl();
                pFile.m_FilePath = ofd.FileName;
                pFile.m_FileType = 0;

                if (".cfg".IndexOf(pFile.m_FilePath.Substring(pFile.m_FilePath.LastIndexOf("."))) != -1)
                {
                    pFile.m_FileTypeName = pFile.m_FilePath.Substring(pFile.m_FilePath.LastIndexOf("\\") + 1);
                    pFile.ReadFileContent();//读取文件
                    if (pFile.m_CWaveInfo == null || pFile.m_CWaveInfo.m_Success == false) return;
                    CSoePicForm pForm = new CSoePicForm();
                    pForm.m_File = pFile;
                    pForm.ShowDialog();
                }
            }
        }
        private bool UpdateForm()
        {
            if (pLoadForm != null)
            {
                myPanelForm.Controls.Remove(pLoadForm);
                pLoadForm.Close();
                pLoadForm = null;

            }
            return true;
        }
        private void Btn_ConSet_Click(object sender, EventArgs e)
        {
            CMasterConForm pForm = new CMasterConForm();
            pForm.ShowDialog();
        }
        private void Btn_RealData_Click(object sender, EventArgs e)
        {         
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CRealCtrlForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CRealCtrlForm();
                FreshForm();
            }           
        }      
        private void Btn_LongCtrl_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CYKForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CYKForm();
                FreshForm();
            }
        }
        private void Btn_ParaSet_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CParaForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CParaForm();
                FreshForm();
            }
        }
        private void Btn_EngPara_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CEngPara))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CEngPara();
                FreshForm();
            }
        }
        private void Btn_HisRecord_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CHisRecordForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CHisRecordForm();
                FreshForm();
            }
        }
        private void Btn_Encode_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CSecrteKeyForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CSecrteKeyForm();
                FreshForm();
            }
        }
        //  模板配置   
        private void Btn_ModeSet1_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.CloseDatabase();
            CModulForm pForm = new CModulForm();
            pForm.Show();
            CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.ConnectDataBase();
        }
        private void DeviceSetting1_Click(object sender, EventArgs e)
        {
            if (CGlobal.m_PreDefauleDevice == null)
            {
                CGlobal.m_PreDefauleDevice = new CDeviceClass();
                for (int i = 0; i < CGlobal.m_ModuleList.Count; i++)
                {
                    if (CGlobal.m_ModuleList[i].m_ModuleName == "DTU标准模板")
                    {
                        CGlobal.m_PreDefauleDevice.m_DeviceMsg = CGlobal.m_ModuleList[i];
                        break;
                    }
                }
                CGlobal.m_PreDefauleDevice.m_DeviceMsg.m_ModuleDB.ConnectDataBase();
                CGlobal.m_PreDefauleDevice.InitialzeSetDeviceModuleOption();
                CGlobal.m_PreDefauleDevice.InitializationReadDataList();
            }
            CTransForm pForm = new CTransForm();
            pForm.Show();
            this.MainContain.Panel2Collapsed = true;
        }
        private void Btn_CommModeSet1_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CommuCFG))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CommuCFG();
                FreshForm();
                this.MainContain.Panel2Collapsed = true;
            }
        }
        private void Btn_DataCheck1_Click(object sender, EventArgs e)
        {

            if (pLoadForm != null && pLoadForm.GetType() != typeof(CDataCheckForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CDataCheckForm();
                FreshForm();
                this.MainContain.Panel2Collapsed = false;
            }
        }
        private void BtnSpePara_Click(object sender, EventArgs e)
        {
            CSpeParaForm pform = new CSpeParaForm();
            pform.ShowDialog();
        }
        private void BtnLuBo1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "录波文件(*.cfg)|*.cfg";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CFileCtrl pFile = new CFileCtrl();
                pFile.m_FilePath = ofd.FileName;
                pFile.m_FileType = 0;

                if (".cfg".IndexOf(pFile.m_FilePath.Substring(pFile.m_FilePath.LastIndexOf("."))) != -1)
                {
                    pFile.m_FileTypeName = pFile.m_FilePath.Substring(pFile.m_FilePath.LastIndexOf("\\") + 1);
                    pFile.ReadFileContent();//读取文件
                    if (pFile.m_CWaveInfo == null || pFile.m_CWaveInfo.m_Success == false) return;
                    CSoePicForm pForm = new CSoePicForm();
                    pForm.m_File = pFile;
                    pForm.ShowDialog();
                }
            }
        }
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CUpdataForm pForm = new CUpdataForm();
            pForm.Show();
        }
        private void BtnDownFile1_Click(object sender, EventArgs e)
        {
            CDownFileForm pDownForm = new CDownFileForm();
            pDownForm.Show();
        }
        private void BtnFTUFastUpdate_Click(object sender, EventArgs e)
        {
            CFTUFastUpdate pForm = new CFTUFastUpdate();
            pForm.Show();
        }
        private void Btn_Function1_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CTestFunForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CTestFunForm();
                FreshForm();
            }
        }
        private void BtnDataTran1_Click(object sender, EventArgs e)
        {

            if (pLoadForm != null && pLoadForm.GetType() != typeof(CDataTranForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CDataTranForm();
                FreshForm();
            }
        }
        private void Btn_ProExp1_Click(object sender, EventArgs e)
        {
            if (pLoadForm != null && pLoadForm.GetType() != typeof(CTtestCaseForm))
            {
                if (!UpdateForm()) return;
                pLoadForm = new CTtestCaseForm();
                FreshForm();
            }
        }      
        //监视窗口     
        private void Btn_Output_Click(object sender, EventArgs e)
        {
            if (Btn_Output.Text == "监视窗口")
            {
                this.MainContain.Panel2Collapsed = false;               
                IniBuffView();
                Btn_Output.Text = "关闭监视";
            }
            else
            {
                Btn_Output.Text = "监视窗口";                           
                if (CGlobal.m_BufferView != null)
                {
                    CGlobal.m_BufferView.Close();
                    CGlobal.m_BufferView = null;
                }
            }
        }
        private void IniBuffView()
        {
            try
            {
                this.MainContain.Panel2Collapsed = false; 
                if (CGlobal.m_BufferView == null)
                    CGlobal.m_BufferView = new DtuTestManager.CBufferViewForm();
                PanelView.DocumentStyle = DocumentStyle.DockingWindow;
                CGlobal.m_BufferView.Show(PanelView);
                PanelView.Dock = DockStyle.Fill;
            }
            catch
            {
                CGlobal.m_BufferView = null;
                IniBuffView();
            }
        }          //初始化报文显示窗口
        private void IniSOEView()
        {
            try
            {
                this.MainContain.Panel2Collapsed = false; 
                if (CGlobal.m_SOEView == null)
                    CGlobal.m_SOEView = new DtuTestManager.CSOEMsgForm();
                PanelView.DocumentStyle = DocumentStyle.DockingWindow;
                CGlobal.m_SOEView.Show(PanelView);
                PanelView.Dock = DockStyle.Fill;
            }
            catch
            {
                CGlobal.m_SOEView = null;
                IniSOEView();
            }
        }            //初始化SOE信息窗口
        private void IniEventView()
        {
            try
            {
                this.MainContain.Panel2Collapsed = false; 
                if (CGlobal.m_EventView == null)
                    CGlobal.m_EventView = new DtuTestManager.CEventMsgForm();
                PanelView.DocumentStyle = DocumentStyle.DockingWindow;
                CGlobal.m_EventView.Show(PanelView);
                PanelView.Dock = DockStyle.Fill;
            }
            catch
            {
                CGlobal.m_EventView = null;
                IniEventView();
            }
        }
        private void FreshTimer_Tick(object sender, EventArgs e)
        {
            if (CGlobal.IsAutoCon == 1)
            {
                StartConnDevice();
            }         
            SetAndRunButton(CGlobal.m_Device.m_bOnlineStatus);           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CGlobal.IsAutoCall == 1)
            {
                CGlobal.SendTaskOrder(4);
            }
        }

        private void BtnSFTPFileDOwn_Click(object sender, EventArgs e)
        {
            CSFTPLogin pform = new CSFTPLogin();
            pform.ShowDialog();
        }       
    }
    public class MainRealYc
    {
        public string m_LineName = "";
        public List<int> m_DataList = new List<int>();
        public int m_LineNo = 0;
        public int m_CIaIndex = 0;
        public int m_CIbIndex = 0;
        public int m_CIcIndex = 0;
        public int m_BIaIndex = 0;
        public int m_BIbIndex = 0;
        public int m_BIcIndex = 0;
        public int m_IoIndex = 0;
        public int m_PhAIndex = 0;
        public int m_PhBIndex = 0;
        public int m_PhCIndex = 0;
        public int m_UaIndex = 0;
        public int m_UbIndex = 0;
        public int m_UcIndex = 0;
        public int m_UabIndex = 0;
        public int m_UbcIndex = 0;
        public int m_UcaIndex = 0;
        public int m_UoIndex = 0;
        public int m_PIndex = 0;
        public int m_QIndex = 0;
        public int m_CosIndex = 0;
        public int m_FIndex = 0;
        public int m_YxIndex = 0;
        public MainRealYc()
        {
            m_DataList.Add(m_CIaIndex);
            m_DataList.Add(m_CIbIndex);
            m_DataList.Add(m_CIcIndex);
            m_DataList.Add(m_BIaIndex);
            m_DataList.Add(m_BIbIndex);
            m_DataList.Add(m_BIcIndex);
            m_DataList.Add(m_IoIndex);
            m_DataList.Add(m_PhAIndex);
            m_DataList.Add(m_PhBIndex);
            m_DataList.Add(m_PhCIndex);
            m_DataList.Add(m_UaIndex);
            m_DataList.Add(m_UbIndex);
            m_DataList.Add(m_UcIndex);
            m_DataList.Add(m_UabIndex);
            m_DataList.Add(m_UbcIndex);
            m_DataList.Add(m_UcaIndex);
            m_DataList.Add(m_UoIndex);
            m_DataList.Add(m_PIndex);
            m_DataList.Add(m_QIndex);
            m_DataList.Add(m_CosIndex);
            m_DataList.Add(m_FIndex);
            m_DataList.Add(m_YxIndex);
        }
    }   
}
