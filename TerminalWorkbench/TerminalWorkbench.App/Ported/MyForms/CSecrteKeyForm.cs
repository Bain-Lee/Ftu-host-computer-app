using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;
using System.Threading;

namespace DtuTestManager
{
    public partial class CSecrteKeyForm : Form
    {
        private List<CerTaskMsg> m_TaskList = new List<CerTaskMsg>();
        private Thread TaskThread = null;
        public CSecrteKeyForm()
        {
            InitializeComponent();
        }
        private void IniSetGridStyle(DataGridView pGrid)
        {
            pGrid.RowHeadersVisible = false;
            pGrid.AllowUserToAddRows = false;
            pGrid.RowsDefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Regular);
            pGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            pGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pGrid.MultiSelect = false;
            pGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void AddNewRow(string strName, DataTable pDataTable)
        {
            DataRow pRowData = pDataTable.NewRow();
            pRowData[0] = strName;
            pRowData[1] = "";
            pDataTable.Rows.Add(pRowData);
        }
        private void IniShowDataGridView(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();


            pDataTable.Columns.Add("名称");
            pDataTable.Columns.Add("值");

            AddNewRow("网关认证_网关随机数", pDataTable);
            AddNewRow("网关认证_终端随机数", pDataTable);
            AddNewRow("网关认证_终端签名结果", pDataTable);
            AddNewRow("网关认证_网关签名结果", pDataTable);
            AddNewRow("主站认证_主站随机数", pDataTable);
            AddNewRow("主站认证_终端随机数", pDataTable);
            AddNewRow("主站认证_终端签名结果", pDataTable);
            AddNewRow("主站认证_主站签名结果", pDataTable);
            AddNewRow("主站认证_秘钥版本随机数", pDataTable);
            AddNewRow("运维认证_随机数(ID)", pDataTable);
            AddNewRow("运维认证_终端随机数", pDataTable);
            AddNewRow("运维认证_签名结果", pDataTable);
            AddNewRow("运维认证_秘钥版本随机数", pDataTable);
            AddNewRow("运维认证_私钥", pDataTable);
            AddNewRow("终端当前秘钥版本号", pDataTable);
            AddNewRow("终端序列号", pDataTable);
            AddNewRow("终端芯片序列号", pDataTable);
            AddNewRow("终端芯片公钥", pDataTable);
            AddNewRow("秘钥恢复包", pDataTable);

            pGridView.DataSource = pDataTable;
            pGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pGridView.Columns[0].ReadOnly = true;
            pGridView.Columns[0].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            for (i = 1; i < pGridView.ColumnCount; i++)
            {
                pGridView.Columns[i].DefaultCellStyle.BackColor = Color.Lavender;
            }
            pGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (i = 0; i < pGridView.Columns.Count; i++)
            {
                pGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            pGridView.Columns[0].Width = 180;
            pGridView.Columns[1].Width = 1000;
        }
        private void FreshGridRowValue(int nRow, string strValue, DataGridView pGridView)
        {
            if (nRow >= pGridView.RowCount)
                return;
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            pDataTable.Rows[nRow][1] = strValue;
        }
        private void FreshGridPara()
        {
            FreshGridRowValue(0, CEnCodePtr.m_GateRandString, EncodeParaGrid);
            FreshGridRowValue(1, CEnCodePtr.m_GateTermRandString, EncodeParaGrid);
            FreshGridRowValue(2, CEnCodePtr.m_GateTermSignString, EncodeParaGrid);
            FreshGridRowValue(3, CEnCodePtr.m_GateSignString, EncodeParaGrid);

            FreshGridRowValue(4, CEnCodePtr.m_MasterRandString, EncodeParaGrid);
            FreshGridRowValue(5, CEnCodePtr.m_TermRandString, EncodeParaGrid);
            FreshGridRowValue(6, CEnCodePtr.m_TermSignString, EncodeParaGrid);
            FreshGridRowValue(7, CEnCodePtr.m_MasterSignString, EncodeParaGrid);
            FreshGridRowValue(8, CEnCodePtr.m_VersionRandString, EncodeParaGrid);

            FreshGridRowValue(9, CEnCodePtr.m_YW_RandKeyString, EncodeParaGrid);
            FreshGridRowValue(10, CEnCodePtr.m_YW_TermRandString, EncodeParaGrid);
            FreshGridRowValue(11, CEnCodePtr.m_YW_SignString, EncodeParaGrid);
            FreshGridRowValue(12, CEnCodePtr.m_YW_ConRandString, EncodeParaGrid);
            FreshGridRowValue(13, CEnCodePtr.m_YW_PrivateKeyString, EncodeParaGrid);

            FreshGridRowValue(14, CEnCodePtr.m_NowKeyVersion.ToString(), EncodeParaGrid);
            FreshGridRowValue(15, CEnCodePtr.m_TermSerialNoString, EncodeParaGrid);
            FreshGridRowValue(16, CEnCodePtr.m_ChipSerialNoString, EncodeParaGrid);
            FreshGridRowValue(17, CEnCodePtr.m_SafeSerialKeyString, EncodeParaGrid);
            FreshGridRowValue(18, CEnCodePtr.m_YW_ResetSecretString, EncodeParaGrid);
        }
        private void AddRichTextContent(Color pColor, string strContent)
        {
            ResultMsg.SelectionColor = pColor;
            ResultMsg.AppendText(strContent + "\r\n");
            ResultMsg.Focus();
        }
        private void BtnClearRichText_Click(object sender, EventArgs e)
        {
            ResultMsg.Text = "";
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1_Encode:        //刷新当前加密数据
                    FreshGridPara();
                    break;
                case UseMessage.MY_MSG_2_Encode:
                    AddRichTextContent(Color.Black, CEnCodePtr.m_StepName);
                    break;
                case UseMessage.MY_MSG_3_Encode:
                    AddRichTextContent(Color.Blue, CEnCodePtr.m_Result);
                    break;
            }
            base.WndProc(ref m);
        }
        private void BtnConnectDev_Click(object sender, EventArgs e)
        {
            CGlobal.m_RunSendMode = false;
            if (CGlobal.m_SecretEncode.ConnectEncodeDev() == false)
                return;
            if (CGlobal.m_Device.m_bOnlineStatus == false)
            {
                if (CGlobal.m_Device.OpenConnChannel())
                {
                    CGlobal.m_Device.IniCreateProcessThread();
                    BtnConnectDev.Text = "断开连接";
                    StatusTimer.Start();
                }
            }
            else
            {
                StatusTimer.Stop();
                CGlobal.m_Device.CloseThread();
                CGlobal.m_SecretEncode.CloseEncode();
                BtnConnectDev.Text = "连接装置";
            }
        }

        private void CSecrteKeyForm_Load(object sender, EventArgs e)
        {
            if (CEnCodePtr.m_SecretType == 0)
            {
                CEnCodePtr.m_Cerfilepath = "证书套件\\";
            }
            else
            {
                CEnCodePtr.m_Cerfilepath = "证书\\";
            }
            if (CGlobal.m_Device.m_bOnlineStatus == false)
            {
                BtnConnectDev.Text = "连接装置";
            }
            else
            {
                BtnConnectDev.Text = "断开连接";
            }

            CGlobal.m_Device.m_EncodeFormHandle = this.Handle.ToInt32();
            CEnCodePtr.m_EncodeFormHandle = this.Handle.ToInt32();

            SecretIndex.Items.Add("秘钥1");
            SecretIndex.Items.Add("秘钥2");
            SecretIndex.Items.Add("秘钥3");
            SecretIndex.Items.Add("秘钥4");
            SecretIndex.SelectedIndex = CEnCodePtr.m_KeyIndex - 1;

            IniShowDataGridView(EncodeParaGrid);
            FreshGridPara();
            Use_Normal.Checked = true;
            CEnCodePtr.NormalTest();
        }

        private void SecretIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_KeyIndex = Convert.ToByte(SecretIndex.SelectedIndex + 1);
        }
        private void AddNewTask(int nTaskNo, bool bWaitFlag = false)
        {
            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = nTaskNo;
            pNewTask.m_AnswerFlag = bWaitFlag;

            m_TaskList.Add(pNewTask);
            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }
        private void BtnIdentity_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Master_Indentity);
        }
        private void BtnMasterAnswer_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Master_Request);
        }
        private void BtnGateDegree_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Gate_Identity);
        }
        private void BtnGateAnswer_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Gate_Request);
        }
        private void BtnTermReadChipNo_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Master_ReadChipNo);
        }
        private void BtnReadVersion_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Master_ReadVersion);
        }
        private void BtnSecretUpdate_Click(object sender, EventArgs e)
        {

            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_UpdateSecret;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);
            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_ReadVersion;
            pNewTask.m_AnswerFlag = false;

            m_TaskList.Add(pNewTask);
            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }
        private void BtnSecretReset_Click(object sender, EventArgs e)
        {

            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_ResetSecret;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);
            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_ReadVersion;
            pNewTask.m_AnswerFlag = false;

            m_TaskList.Add(pNewTask);
            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }
        private void GetParaOperationList(int nTaskNo, int nGroupNo, bool bNoWait = false)
        {
            if (CGlobal.m_Device.m_bOnlineStatus == false)
                return;

            for (int i = 0; i < nGroupNo; i++)
            {
                CerTaskMsg pNewTask = new CerTaskMsg();
                pNewTask.m_TaskNo = nTaskNo;
                pNewTask.m_AnswerFlag = bNoWait;
                m_TaskList.Add(pNewTask);
            }
            if (m_TaskList.Count > 0)
                m_TaskList[m_TaskList.Count - 1].m_AnswerFlag = false;

            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }
        private void CertiUpdata()
        {
            byte[] pSignResult = new byte[64];
            int nSignLeng = 0;

            ///读取证书内容
            byte[] pFileText = null;
            int nFileLeng = CEnCodePtr.ReadCerfileCmd(ref pFileText, CEnCodePtr.m_Cerfilepath + CEnCodePtr.m_Cerfilename);
            if (nFileLeng == 0)
                return;
            //组装需要签名的报文内容
            byte[] pSignBuff = new byte[nFileLeng + 7];
            pSignBuff[0] = CEnCodePtr.m_CerBiaosi;
            Array.Copy(pFileText, 0, pSignBuff, 1, nFileLeng);
            byte[] m_Time = CEnCodePtr.GetCurrentTime();
            Array.Copy(m_Time, 0, pSignBuff, nFileLeng + 1, 6);
            //签名
            if (CGlobal.m_SecretEncode.GetSign(pSignBuff, pSignBuff.Length, pSignResult, ref nSignLeng, CEnCodePtr.m_KeyIndex) == false)
                return;
            if (CEnCodePtr.m_UseNoSignFlag == true)
            {
                //组装需要加密的报文
                byte[] pSecretBuff1 = new byte[8 + nFileLeng];
                byte[] pEncodeBuff1 = new byte[2000];
                int nEncodeLeng1 = 0;
                int nPlace1 = 0;
                Array.Copy(pSignResult, 0, pSecretBuff1, nPlace1, nSignLeng);
                nPlace1 += nSignLeng;
                pSecretBuff1[nPlace1++] = CEnCodePtr.m_KeyIndex;//签名秘钥标识
                //加密
                if (CGlobal.m_SecretEncode.EnCodeData(false, pSecretBuff1, nPlace1, pEncodeBuff1, ref nEncodeLeng1) == false)
                    return;
                //分帧处理
                CEnCodePtr.SetSendDataPacket(pEncodeBuff1, nEncodeLeng1);
                return;
            }
            //组装需要加密的报文
            byte[] pSecretBuff = new byte[8 + nSignLeng + nFileLeng];
            byte[] pEncodeBuff = new byte[2000];
            int nEncodeLeng = 0;
            int nPlace = 0;

            Array.Copy(pSignBuff, 0, pSecretBuff, nPlace, pSignBuff.Length);
            nPlace += pSignBuff.Length;
            Array.Copy(pSignResult, 0, pSecretBuff, nPlace, nSignLeng);
            nPlace += nSignLeng;
            pSecretBuff[nPlace++] = CEnCodePtr.m_KeyIndex;//签名秘钥标识
            //加密
            if (CGlobal.m_SecretEncode.EnCodeData(false, pSecretBuff, nPlace, pEncodeBuff, ref nEncodeLeng) == false)
                return;
            //分帧处理
            CEnCodePtr.SetSendDataPacket(pEncodeBuff, nEncodeLeng);
        }
        private void BtnCredUpdate_Click(object sender, EventArgs e)
        {
            if (Cred1.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 0;
                CEnCodePtr.m_Cerfilename = "CAcerti.txt";
            }
            else if (Cred2.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 1;
                CEnCodePtr.m_Cerfilename = "1certi.txt";
            }
            else if (Cred3.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 2;
                CEnCodePtr.m_Cerfilename = "2certi.txt";
            }
            else if (Cred4.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 3;
                CEnCodePtr.m_Cerfilename = "3certi.txt";
            }
            else if (Cred5.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 4;
                CEnCodePtr.m_Cerfilename = "4certi.txt";
            }
            else if (Cred6.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 5;
                CEnCodePtr.m_Cerfilename = "Gatecerti.txt";
            }
            else
                return;
            m_TaskList.Clear();
            CertiUpdata();
            GetParaOperationList((int)EncodeStatus.Master_UpdateCer, CEnCodePtr.m_TotalPacketNo, true);
        }
        private void BtnCredDown_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.Master_ReadCer);
        }
        private void EnableForm(bool bEnable)
        {
            this.Invoke
            (
                  new MethodInvoker
                  (
                      delegate
                      {
                          if (bEnable == false)
                              ExeStatus.Text = "执行中......";
                          else
                              ExeStatus.Text = "";
                          this.Enabled = bEnable;
                          ResultMsg.Focus();
                      }
                  )
            );
        }
        private void StartSendCerFile()
        {
            int nTimeCount = 0;
            if (CGlobal.m_Device.m_bOnlineStatus == false)
                return;
            EnableForm(false);
            CerTaskMsg pNowTask = null;
            while (CGlobal.m_Device.m_bOnlineStatus)
            {
                Thread.Sleep(200);
                nTimeCount++;
                if (pNowTask != null)
                {
                    if (pNowTask.m_AnswerFlag == false)
                    {
                        if (nTimeCount >= 50)
                            break;
                        else
                            continue;
                    }
                    nTimeCount = 0;
                }
                if (m_TaskList.Count == 0)
                    break;

                pNowTask = m_TaskList[0];
                CGlobal.m_Device.m_Ptr.m_NowCerTask = pNowTask;

                CGlobal.SendTaskOrder(pNowTask.m_TaskNo);
                m_TaskList.RemoveAt(0);
            }
            m_TaskList.Clear();
            EnableForm(true);
        }
        private void BtnCredInput_Click(object sender, EventArgs e)
        {
            CEnCodePtr.m_Cerfilename = "TemCerti.txt";
            CEnCodePtr.m_CerBiaosi = 6;

            byte[] pFileBuff = null;
            int nFileLeng = CEnCodePtr.ReadCerfileCmd(ref pFileBuff, CEnCodePtr.m_Cerfilepath + CEnCodePtr.m_Cerfilename);
            if (nFileLeng == 0)
                return;
            //分帧处理
            CEnCodePtr.SetSendDataPacket(pFileBuff, nFileLeng);
            GetParaOperationList((int)EncodeStatus.Master_DownCer, CEnCodePtr.m_TotalPacketNo, true);
        }
        private void BtnDegree_Click(object sender, EventArgs e)
        {
            if (CGlobal.m_SecretEncode.GetYWCertAndID() == false)
                return;
            CEnCodePtr.SetSendDataPacket(CEnCodePtr.m_YWCert, CEnCodePtr.m_YwCertLeng);
            GetParaOperationList((int)EncodeStatus.YW_Identity, CEnCodePtr.m_TotalPacketNo, false);
        }
        private void BtnYWRequest_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_Request);
        }
        private void BtnYWReadVersion_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_ReadVersion);
        }
        private void BtnReadNo_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_ReadSerialNo);
        }

        private void BtnReadChipNo_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_ReadChipNo);
        }

        private void BtnReadKey_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_ReadSecretKey);
        }
        public string GetSelectTxtFile()
        {
            string strName = "";
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "证书文件(*.txt)|*.txt";
            //FileDialog.InitialDirectory = strDefaultPath;

            FileDialog.Multiselect = true;
            FileDialog.Title = "请选择需要签名的证书文件";
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                strName = FileDialog.FileName;
            }
            return strName;
        }
        private void BtnSign_Click(object sender, EventArgs e)
        {
            string strFilePath = GetSelectTxtFile();
            if (strFilePath.Length == 0)
                return;
            CEnCodePtr.m_YWDowFileType = 2;
            //      CEnCodePtr.m_Cerfilename = "4certi.txt";
            ///读取证书内容
            byte[] pFileBuff = null;
            int nFileLeng = CEnCodePtr.ReadCerfileCmd(ref pFileBuff, strFilePath);
            if (nFileLeng == 0)
                return;
            CEnCodePtr.SetSendDataPacket(pFileBuff, nFileLeng);
            GetParaOperationList((int)EncodeStatus.YW_FileSign, CEnCodePtr.m_TotalPacketNo, false);
        }

        private void BtnCredWrite_Click(object sender, EventArgs e)
        {
            if (ServicCred1.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 0;
                CEnCodePtr.m_Cerfilename = "CAcerti.txt";
            }
            else if (ServicCred2.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 1;
                CEnCodePtr.m_Cerfilename = "1certi.txt";
            }
            else if (ServicCred3.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 2;
                CEnCodePtr.m_Cerfilename = "2certi.txt";
            }
            else if (ServicCred4.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 3;
                CEnCodePtr.m_Cerfilename = "3certi.txt";
            }
            else if (ServicCred5.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 4;
                CEnCodePtr.m_Cerfilename = "4certi.txt";
            }
            else if (ServicCred6.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 5;
                CEnCodePtr.m_Cerfilename = "Gatecerti.txt";
            }
            else if (ServicCred7.Checked == true)
            {
                CEnCodePtr.m_CerBiaosi = 6;
                CEnCodePtr.m_Cerfilename = "TemCerti.txt";
            }
            else
                return;

            byte[] pFileBuff = null;
            int nFileLeng = CEnCodePtr.ReadCerfileCmd(ref pFileBuff, CEnCodePtr.m_Cerfilepath + CEnCodePtr.m_Cerfilename);
            if (nFileLeng == 0)
                return;
            //分帧处理
            CEnCodePtr.SetSendDataPacket(pFileBuff, nFileLeng);
            GetParaOperationList((int)EncodeStatus.YW_DownCer, CEnCodePtr.m_TotalPacketNo, false);
        }

        private void BtnWriteBack_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_ReWrite);
        }
        private void BtnTerDown_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_ReadTerCer);
        }

        private void BtnSendData_Click(object sender, EventArgs e)
        {
            AddNewTask((int)EncodeStatus.YW_SendData);
        }
        private void BtnResetKey_Click(object sender, EventArgs e)
        {
            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.YW_ResetKey;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.YW_ReadVersion;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            if (CGlobal.m_Device.m_bOnlineStatus == false)
            {
                CGlobal.m_Device.CloseThread();
                CGlobal.m_SecretEncode.CloseEncode();
                BtnConnectDev.Text = "连接装置";
                StatusTimer.Stop();
            }
        }

        private void BtnAutoCheck_Click(object sender, EventArgs e)
        {
            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Gate_Identity;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Gate_Request;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_Indentity;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_Request;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_ReadChipNo;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_ReadVersion;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }
        private void BtnCheckCARight_Click(object sender, EventArgs e)
        {
            CGlobal.m_SecretEncode.YW_GetCertInfo();
        }


        private void UseErrTime_CheckedChanged(object sender, EventArgs e)
        {
            CEnCodePtr.m_TimeErrFlag = UseErrTime.Checked;
        }
        private void UseErrMACCheck_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void NormalUseErrCS_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseErrSign_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseErrRand_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseNoSign_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseNoTime_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();

        }

        private void UseNoRand_CheckedChanged(object sender, EventArgs e)
        {

            Flag_Status();

        }

        private void UseMingWen_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void Flag_Status()
        {
            CEnCodePtr.m_MacErrFlag = UseErrMACCheck.Checked;
            CEnCodePtr.m_NormalErrCSFlag = NormalUseErrCS.Checked;
            CEnCodePtr.m_SignErrFlag = UseErrSign.Checked;
            CEnCodePtr.m_RandErrFlag = UseErrRand.Checked;
            CEnCodePtr.m_UseNoSignFlag = UseNoSign.Checked;
            CEnCodePtr.m_UseNoTimeFlag = UseNoTime.Checked;
            CEnCodePtr.m_UseNoRandFlag = UseNoRand.Checked;
            CEnCodePtr.m_UseMingWenFlag = UseMingWen.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_TaskList.Clear();
            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = 1;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_UpdateSecret;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);


            CEnCodePtr.m_CerBiaosi = 5;
            CEnCodePtr.m_Cerfilename = "Gatecerti.txt";
            CertiUpdata();
            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_UpdateCer;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.YW_ResetKey;
            pNewTask.m_AnswerFlag = true;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.YW_ReadVersion;
            pNewTask.m_AnswerFlag = true;
            m_TaskList.Add(pNewTask);

            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UseErrSign.Checked = true;
            m_TaskList.Clear();
            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Gate_Identity;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Gate_Request;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_Indentity;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_Request;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);


            if (CGlobal.m_SecretEncode.GetYWCertAndID() == true)
            {
                CEnCodePtr.SetSendDataPacket(CEnCodePtr.m_YWCert, CEnCodePtr.m_YwCertLeng);

                for (int i = 0; i < CEnCodePtr.m_TotalPacketNo; i++)
                {
                    pNewTask = new CerTaskMsg();
                    pNewTask.m_TaskNo = (int)EncodeStatus.YW_Identity;
                    pNewTask.m_AnswerFlag = false;
                    m_TaskList.Add(pNewTask);
                }

                pNewTask = new CerTaskMsg();
                pNewTask.m_TaskNo = (int)EncodeStatus.YW_Request;
                pNewTask.m_AnswerFlag = false;
                m_TaskList.Add(pNewTask);
            }

            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UseNoSign.Checked = true;
            m_TaskList.Clear();
            CerTaskMsg pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Gate_Identity;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Gate_Request;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_Indentity;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);

            pNewTask = new CerTaskMsg();
            pNewTask.m_TaskNo = (int)EncodeStatus.Master_Request;
            pNewTask.m_AnswerFlag = false;
            m_TaskList.Add(pNewTask);
            TaskThread = new Thread(new ThreadStart(StartSendCerFile));
            TaskThread.IsBackground = true;
            TaskThread.Start();
        }
        private void Use_Normal_CheckedChanged(object sender, EventArgs e)
        {
            if (Use_Normal.Checked == true)
            {
                CEnCodePtr.NormalTest();
            }
        }



    }
}
