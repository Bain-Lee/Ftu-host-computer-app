using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace DtuTestManager
{
    public partial class CParaForm : Form
    { /// <summary>
        /// 解决闪烁问题
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        CParaGroupTable m_NowParaGroup = null;
        private List<ParaTaskMsg> m_TaskList = new List<ParaTaskMsg>();
        private ParaTaskMsg pNowTask = null;
        public bool IsRead = false;  //读参数 :true   写参数: false
        public CParaForm()
        {
            InitializeComponent();
        }
        private void InitionTreeView()
        {
            int i,j;
            ParaTreeView.Nodes.Clear();
            for(i=0;i<CGlobal.m_Device.m_ParaList.Count;i++)
            {
                TreeNode pRootNode = new TreeNode();
                pRootNode.Text = CGlobal.m_Device.m_ParaList[i].m_RootName;
                pRootNode.Tag = CGlobal.m_Device.m_ParaList[i];
                ParaTreeView.Nodes.Add(pRootNode);
                for(j=0;j<CGlobal.m_Device.m_ParaList[i].m_GroupList.Count;j++)
                {
                    TreeNode pGroupNode = new TreeNode();
                    pGroupNode.Text = CGlobal.m_Device.m_ParaList[i].m_GroupList[j].m_GroupName;
                    pGroupNode.Tag = CGlobal.m_Device.m_ParaList[i].m_GroupList[j];
                    pRootNode.Nodes.Add(pGroupNode);
                }
            }
        }
        public void ReLoadForm()
        {
            int i, j;
            FreshTime.Stop();
            SetListViewMode();
            TableNameList.Items.Clear();
            for (i = 0; i < CGlobal.m_Device.m_ParaList.Count; i++)
            {
                for(j=0;j<CGlobal.m_Device.m_ParaList[i].m_GroupList.Count;j++)
                {
                    TableNameList.Items.Add(CGlobal.m_Device.m_ParaList[i].m_GroupList[j].m_GroupName);
                }
            }           
            if (TableNameList.Items.Count > 0) TableNameList.SelectedIndex = 0;
            m_NowParaGroup = CGlobal.m_Device.GetParaGroup(1);
            Init_LV_Para(m_NowParaGroup);
            InitionTreeView();
            CGlobal.m_Device.m_ParaHandle = this.Handle.ToInt32();          
            FreshTime.Start();
        }
        private void CParaForm_Load(object sender, EventArgs e)
        {
            ReLoadForm();
            Use_Normal.Checked = true;
            CEnCodePtr.NormalTest();
        }
        private void FreshVersion(UInt32 nVersion)
        {
            string strVersion, strTemp, strSoft = "";
            strVersion = Convert.ToString(CVersionForm.pIntVersion, 16).ToUpper();
            strVersion = "00000000" + strVersion;
            strVersion = strVersion.Substring(strVersion.Length - 6, 6);
            UInt32 pValue = CVersionForm.pIntVersion;
            for (int i = 0; i < 12; i++)
            {
                UInt32 nValue = pValue % 4;
                pValue = pValue >> 2;
                strTemp = nValue.ToString();
                if (i != 0)
                    strSoft = strTemp + "-" + strSoft;
                else
                    strSoft = strTemp;
            }

            LV_Para.Items[2].SubItems[3].Text = "板件类型：" + strVersion;
            LV_Para.Items[3].SubItems[3].Text =  strSoft;
        }
        private void DevType()
        {
            ushort pVersionL = 0,pVersionH = 0;
            try
            {
                if (LV_Para.Items[2].SubItems[1].Text.Length == 0)
                {
                    pVersionL = 0;
                }
                else
                {
                    pVersionL = (ushort)Convert.ToUInt16(LV_Para.Items[2].SubItems[1].Text);
                }
                if (LV_Para.Items[3].SubItems[1].Text.Length == 0)
                {
                    pVersionL = 0;
                }
                else
                {
                    pVersionH = (ushort)Convert.ToUInt16(LV_Para.Items[3].SubItems[1].Text);
                }
                CVersionForm.pIntVersion = (UInt32)((pVersionH << 16) + pVersionL);
                FreshVersion(CVersionForm.pIntVersion);
            }
            catch { }
        }
        private void FreshPara()
        {
            if (m_NowParaGroup == null) return;
            for (int i = 0; i < m_NowParaGroup.m_ParaDataList.Count; i++)
            {
                CParaCtrl pPara = m_NowParaGroup.m_ParaDataList[i];
                if (pPara.m_Fresh)
                {
                    pPara.m_Fresh = false;
                    LV_Para.Items[i].SubItems[1].Text = pPara.m_ParaValue.ToString();
                    LV_Para.Items[i].Checked = pPara.m_Select;
                }
            }
            if (m_NowParaGroup.m_GroupNo == 7) DevType();
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1:
                    CGuHuaForm pform = new CGuHuaForm();
                    pform.ShowDialog();
                    break;
                case UseMessage.MY_MSG_2:
                    Result.Text = "固化成功";
                   // MessageBox.Show("固化成功");
                    break;
                case UseMessage.MY_MSG_3:
                    Result.Text = "预置失败";
                    //MessageBox.Show("预置失败");
                    break;
                case UseMessage.MY_MSG_4:
                    AeroNo.Text = CGlobal.m_Device.m_Ptr.m_ParaAreaNo.ToString();
                    break;
            }
            base.WndProc(ref m);
        }
        private void SetListViewMode()
        {
            LV_Para.ClearAllItem();
            LV_Para.Columns.Add("选择", 240, HorizontalAlignment.Center);
            LV_Para.Columns.Add("值", 120, HorizontalAlignment.Center);
            LV_Para.Columns.Add("单位", 80, HorizontalAlignment.Center);
            LV_Para.Columns.Add("说明", 300, HorizontalAlignment.Center);
            LV_Para.Columns.Add("序号", 80, HorizontalAlignment.Center);
            LV_Para.Columns.Add("地址", 120, HorizontalAlignment.Center);
            LV_Para.Columns.Add("类型", 80, HorizontalAlignment.Center);

            LV_Para.GridLines = true;
            LV_Para.FullRowSelect = true;
            LV_Para.View = View.Details;
            LV_Para.Scrollable = true;
            LV_Para.MultiSelect = false;
            LV_Para.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV_Para.Visible = true;
            LV_Para.CheckBoxes = true;
        }
        private void Init_LV_Para(CParaGroupTable pParaGroup)
        {
            if (pParaGroup == null) return;
            if (pParaGroup.m_GroupNo <=5)
            {
                CGlobal.m_Device.m_Ptr.Is_PriPara = false;
            }
            else
            {
                CGlobal.m_Device.m_Ptr.Is_PriPara = true;
            }
            if (pParaGroup.m_GroupNo == 7)
            {
                // SetVersionType.Visible = true;
            }
            else
            {
                // SetVersionType.Visible = false;
            }
                CGlobal.m_Device.m_ParaSelectGroup = pParaGroup;

            NowSelctParaName.Text = "当前参数组：" + m_NowParaGroup.m_GroupName;
            try
            {
                string[] strHead = new string[7];         
                LV_Para.ClearAllItem(false);
                for (int i = 0; i < pParaGroup.m_ParaDataList.Count; i++)
                {
                    CParaCtrl pPara = pParaGroup.m_ParaDataList[i];
                    strHead[0] = pPara.m_ParaName;
                    strHead[1] = pPara.m_ParaValue.ToString();
                    strHead[2] = pPara.m_ParaUnit;
                    strHead[3] = pPara.m_ParaLimit;
                    strHead[4] = pPara.m_ParaNo.ToString();
                    strHead[5] = pPara.m_ParaAddress.ToString() + "( " +  Convert.ToString(pPara.m_ParaAddress, 16).ToUpper() + " )";
                    strHead[6] = pPara.GetValueTypeName();
             
                    ListViewItem item1 = new ListViewItem(strHead);
                    this.LV_Para.AddItem(item1);
                    LV_Para.Items[i].Checked = pPara.m_Select;
                }
                LV_Para.SetColumnsEditMode(1);
                LV_Para.SetColumnsEditMode(3);
            }
            catch { }
        }       //初始化参数界面
        private void SetListViewSelectStatus(int nSelectType)
        {
            if (m_NowParaGroup == null) return;
            for (int i = 0; i < m_NowParaGroup.m_ParaDataList.Count; i++)
            {
                switch (nSelectType)
                {
                    case 0:
                        LV_Para.Items[i].Checked = true;
                        break;
                    case 1:
                        LV_Para.Items[i].Checked = false;
                        break;
                    case 2:
                        LV_Para.Items[i].Checked = !LV_Para.Items[i].Checked;
                        break;
                }
            }
        }    
        //参数选择
        private void BtnSelectAllPara_Click(object sender, EventArgs e)
        {
            SetListViewSelectStatus(0);
        }
        private void BtnCancleAllPara_Click(object sender, EventArgs e)
        {
            SetListViewSelectStatus(1);
        }
        private void BtnSelecDifferent_Click(object sender, EventArgs e)
        {
            SetListViewSelectStatus(2);
        }
        //参数操作
        private bool GetSelectPara(bool bRw)
        {
            if (m_NowParaGroup == null) return false;
            if (m_NowParaGroup.m_GroupNo <= 5)
            {
                CGlobal.m_Device.m_Ptr.Is_PriPara = false;
            }
            else
            {
                CGlobal.m_Device.m_Ptr.Is_PriPara = true;
            }

            CGlobal.m_Device.m_ParaHandle = this.Handle.ToInt32();
            CGlobal.m_Device.m_ParaFormType = 1;
            CGlobal.m_Device.m_SelectParaList.Clear();
            for (int i = 0; i < m_NowParaGroup.m_ParaDataList.Count; i++)
            {
                CParaCtrl pPara = m_NowParaGroup.m_ParaDataList[i];
                pPara.m_Select = LV_Para.Items[i].Checked;
                if (LV_Para.Items[i].Checked)
                {
                    if (bRw)
                    {
                        if(LV_Para.Items[i].SubItems[1].Text.Length == 0) LV_Para.Items[i].SubItems[1].Text = "0";
                        pPara.m_ParaValue = LV_Para.Items[i].SubItems[1].Text;
                    }
                    else
                    {
                        LV_Para.Items[i].SubItems[1].Text = "";
                    }
                    CGlobal.m_Device.m_SelectParaList.Add(pPara);
                }
            }
            if (CGlobal.m_Device.m_SelectParaList.Count > 0) return true;
            return false;
        }
        private void GetParaOperationList(int nTaskNo,int nGroupNo,bool bNoWait = false)
        {
            m_TaskList.Clear();
            if (CGlobal.m_Device.m_SelectParaList.Count == 0) return;
            TaskTime.Stop();
            int nMaxCount = CGlobal.m_Device.m_SelectParaList.Count;
            int nCroup = 0;
            while (nMaxCount > 0)
            {
                ParaTaskMsg pNewTask = new ParaTaskMsg();
                pNewTask.m_TaskNo = nTaskNo;
                pNewTask.m_AnswerFlag = bNoWait;
                pNewTask.m_StartPalce = nCroup * nGroupNo;
                if (nMaxCount >= nGroupNo)
                    pNewTask.m_ParaCount = nGroupNo;
                else
                    pNewTask.m_ParaCount = nMaxCount;
                pNewTask.m_EndFlag = false;
                m_TaskList.Add(pNewTask);
                nCroup++;
                nMaxCount -= pNewTask.m_ParaCount;
            }
            m_TaskList[m_TaskList.Count - 1].m_EndFlag = true;
            m_TaskList[m_TaskList.Count - 1].m_AnswerFlag = false;
            pNowTask = null;
            if (IsRead ==false)
            {
                for (int i = 5; i < m_TaskList.Count; i++)
                {
                    if (i % 5 == 0)
                    {
                        m_TaskList[i].m_EndFlag = true;
                        m_TaskList[i].m_AnswerFlag = false;
                        i = i + 5;
                    }
                }           
            }
            TaskTime.Start();
        }
        private void BtnReadAllPara_Click(object sender, EventArgs e)
        {
            IsRead = true;
            SetListViewSelectStatus(0);
            if (GetSelectPara(false))
            {
                GetParaOperationList(8,20);
            }
        }
        private void BtnReadSelectPara_Click(object sender, EventArgs e)
        {
            IsRead = true ;
            if (GetSelectPara(false))
            {
                GetParaOperationList(8,20);
            }
        }
        private void BtnDownAllPara_Click(object sender, EventArgs e)
        {
            Result.Text = "参数下发结果:";
            IsRead = false;
            SetListViewSelectStatus(0);
            if (GetSelectPara(true))
            {
                SavePara(true);
                GetParaOperationList(9,20,true);         
            }
           

        }
        private void BtnDownSelect_Click(object sender, EventArgs e)
        {
            Result.Text = "参数下发结果:";
            IsRead = false;
            if (GetSelectPara(true))
            {
                SavePara(true);
                GetParaOperationList(9,20,true);
            }
        }
        private void SavePara(bool bSelect = false)
        {
            if(m_NowParaGroup == null) return;
            for (int i = 0; i < m_NowParaGroup.m_ParaDataList.Count; i++)
            {
                CParaCtrl pPara = m_NowParaGroup.m_ParaDataList[i];
                pPara.m_Select = LV_Para.Items[i].Checked;
                if (bSelect)    if (pPara.m_Select == false) continue;
                if (LV_Para.Items[i].SubItems[1].Text.Length == 0) LV_Para.Items[i].SubItems[1].Text = "0";
                pPara.m_ParaValue = LV_Para.Items[i].SubItems[1].Text;
                
            }
            CGlobal.m_Device.SaveNowPara(m_NowParaGroup);
        }
        private void BtnSaveAll_Click(object sender, EventArgs e)
        {
            SavePara();
            MessageBox.Show("保存完成!");
        }
        private void BtnSaveSelcet_Click(object sender, EventArgs e)
        {
            SavePara(true);
            MessageBox.Show("保存完成!");
        }       
        private void FreshTime_Tick(object sender, EventArgs e)
        {
            if (CGlobal.m_Device.GetParaFreshStatus())
            {
                FreshPara();
            }
            if (m_NowParaGroup != null && m_NowParaGroup.m_GroupNo == 7) DevType();
        }
        private void BtnReadStandPara_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_ParaFormType = 1;
            bool bPriPara = CGlobal.m_Device.m_Ptr.Is_PriPara;
            CGlobal.m_Device.m_Ptr.Is_PriPara = false; 
            CGlobal.SendTaskOrder(28);
            CGlobal.m_Device.m_Ptr.Is_PriPara = bPriPara;
        }
        private void BtnReadLocalPara_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_ParaFormType = 1;
            bool bPriPara = CGlobal.m_Device.m_Ptr.Is_PriPara;
            CGlobal.m_Device.m_Ptr.Is_PriPara = true;
            CGlobal.SendTaskOrder(28);
            CGlobal.m_Device.m_Ptr.Is_PriPara = bPriPara;
        }
        private void TaskTime_Tick(object sender, EventArgs e)
        {
            if (CGlobal.m_Device.m_bOnlineStatus == false)
            {
                TaskTime.Stop();
            }
            if (pNowTask != null)
            {
                if (pNowTask.m_AnswerFlag == true) pNowTask = null;
            }
            else
            {
                if (m_TaskList.Count > 0)
                {
                    pNowTask = m_TaskList[0];
                    CGlobal.m_Device.m_Ptr.m_NowParaTask = pNowTask;
                    CGlobal.SendTaskOrder(pNowTask.m_TaskNo);
                    m_TaskList.RemoveAt(0);
                }
                else
                {
                    TaskTime.Stop();
                }
            }
        }
        private void SetVersionType_Click(object sender, EventArgs e)
        {
            CVersionForm m_CVersionForm = new CVersionForm();
            CVersionForm.pIntVersion = Convert.ToUInt32(LV_Para.Items[2].SubItems[1].Text) + (Convert.ToUInt32(LV_Para.Items[3].SubItems[1].Text)<<16);
            if (m_CVersionForm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            FreshVersion(CVersionForm.pIntVersion);
            LV_Para.Items[2].SubItems[1].Text = (CVersionForm.pIntVersion & 0xFFFF).ToString();
            LV_Para.Items[3].SubItems[1].Text = (CVersionForm.pIntVersion >> 16).ToString();
        }
        private void ReWritePara_Click(object sender, EventArgs e)
        {
            if(m_NowParaGroup == null) return;
            int nIndex = TableNameList.SelectedIndex;
            CParaGroupTable pSelectGroup = CGlobal.m_Device.GetParaGroup(nIndex + 1);
            int i;
            if ((pSelectGroup.m_RootIndex != m_NowParaGroup.m_RootIndex) || (pSelectGroup.m_ParaDataList.Count != m_NowParaGroup.m_ParaDataList.Count))
            {
                MessageBox.Show("参数组类型不一致!");
                return;
            }
            if (pSelectGroup.m_GroupNo == m_NowParaGroup.m_GroupNo)
            {
                MessageBox.Show("与当前选择参数组相同，无需回填!");
                return;
            }
            for (i = 0; i < pSelectGroup.m_ParaDataList.Count; i++)
            {
                m_NowParaGroup.m_ParaDataList[i].m_ParaValue = pSelectGroup.m_ParaDataList[i].m_ParaValue;
                m_NowParaGroup.m_ParaDataList[i].m_Select = pSelectGroup.m_ParaDataList[i].m_Select;
            }
            FreshPara();
            MessageBox.Show("回填结束!");
        }
        private void UpdateParaDesc_Click(object sender, EventArgs e)
        {
            if (m_NowParaGroup == null) return;
            for (int i = 0; i < m_NowParaGroup.m_ParaDataList.Count; i++)
            {
                CParaCtrl pPara = m_NowParaGroup.m_ParaDataList[i];
                pPara.m_ParaLimit = LV_Para.Items[i].SubItems[3].Text;
            }
            CGlobal.m_Device.UpdateNowParaDesc(m_NowParaGroup);
        }
        private void ParaTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = ParaTreeView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CParaGroupTable))
                {
                    m_NowParaGroup = (CParaGroupTable)pSelectNode.Tag;
                    Init_LV_Para(m_NowParaGroup);
                }
            }
        }
        private void BtnReadAeroNO_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(37);
        }
        private void BtnSwitchAeroNo_Click(object sender, EventArgs e)
        {
            try
            {
                CGlobal.m_Device.m_Ptr.m_ParaAreaNo = Convert.ToInt32(AeroNo.Text);
                CGlobal.SendTaskOrder(38);
            }
            catch
            {}
        }
        private void BtnGH_Click(object sender, EventArgs e)
        {
           CGlobal.SendTaskOrder(10);
        }
        private void Flag_Status()
        {
            CEnCodePtr.m_SignErrFlag = UseErrSign.Checked;
            CEnCodePtr.m_RandErrFlag = UseErrRand.Checked;
            CEnCodePtr.m_UseNoSignFlag = UseNoSign.Checked;
            CEnCodePtr.m_UseNoRandFlag = UseNoRand.Checked;
            CEnCodePtr.m_UseMingWenFlag = UseMingWen.Checked;
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
        private void UseNoRand_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }
        private void UseMingWen_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }
        private void Use_Normal_CheckedChanged(object sender, EventArgs e)
        {
            if (Use_Normal.Checked == true)
            {
                CEnCodePtr.NormalTest();
            }
        }
        private void BtnCan_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(42);
        }      
    }
}
