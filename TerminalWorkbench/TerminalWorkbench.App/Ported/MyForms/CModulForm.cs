using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TerminalWorkbench.Presentation;
using Microsoft.VisualBasic;
namespace DtuTestManager
{
    public partial class CModulForm : Form
    {
        CGroupInterface pSelectGroup = null;
        CChildModuleRoot pSelectRoot = null;
        List<CChildModuleRoot> m_ChildModuleList = new List<CChildModuleRoot>();
        private int m_ModuleViewSelectIndex = -1;

        public CChildModlueSelect m_ChildSelect = new CChildModlueSelect();
        private CDeviceMudule m_NowSelectDevice = null;
        public CBaseModuleGroup m_DevBaseModuleSlect = null;

        CDeviceRoot m_DevRoot = new CDeviceRoot();
        Form m_SelectForm = null;
        private int m_DevViewSelectIndex = -1;

        public CModulForm()
        {
            InitializeComponent();
        }
        private void AddNewChildModule(string strName, int nType)
        {
            CChildModuleRoot pNewModule = new CChildModuleRoot();
            pNewModule.m_RootName = strName;
            pNewModule.m_RootType = nType;
            pNewModule.CreateTreeNode();
            pNewModule.IniReadGroup();
            m_ChildModuleList.Add(pNewModule);
        }
        private void IniFontNameList()
        {
            int i;
            string strName;
            FontName.Items.Clear();
            for (i = 0; i < 16; i++)
            {
                strName = i.ToString() + "#线路";
                FontName.Items.Add(strName);
            }
            for (i = 0; i < 16; i++)
            {
                strName = i.ToString() + "回路";
                FontName.Items.Add(strName);
            }
            FontName.SelectedIndex = 0;
        }
        private void LoadDeviceModuleSet()
        {
            m_DevRoot.m_ThisDevType = 1;
            m_DevRoot.LoadDeviceModuleSet();
        }
        private void IniDevModuleList()
        {
            int i;
            DevModuleComList.Items.Clear();
            for (i = 0; i < m_DevRoot.m_DeviceModuleList.Count; i++)
            {
                CDeviceMudule pDevice = m_DevRoot.m_DeviceModuleList[i];
                DevModuleComList.Items.Add(pDevice.m_DeviceMsg.m_ModuleName);
            }

            for (i = 0; i < m_DevRoot.m_DeviceModuleList.Count; i++)
            {
                CDeviceMudule pDevice = m_DevRoot.m_DeviceModuleList[i];
                if (pDevice.m_DeviceMsg.m_ModuleName == CGlobal.m_Device.m_Connection.m_DevName)
                {
                    DevModuleComList.SelectedItem = CGlobal.m_Device.m_Connection.m_DevName;
                }
            }
            if (DevModuleComList.SelectedIndex == -1 && DevModuleComList.Items.Count > 0)
                DevModuleComList.SelectedIndex = 0;
        }
        private void IniDeviceTreeView()
        {
            int i, j;
            DeviceTreeView.Nodes.Clear();
            for (i = 0; i < m_DevRoot.m_DeviceModuleList.Count; i++)
            {
                CDeviceMudule pDevice = m_DevRoot.m_DeviceModuleList[i];
                pDevice.m_Node = new TreeNode();
                pDevice.m_Node.Text = pDevice.m_DeviceMsg.m_ModuleName;
                pDevice.m_Node.Tag = pDevice;

                DeviceTreeView.Nodes.Add(pDevice.m_Node);
                for (j = 0; j < pDevice.m_GroupList.Count; j++)
                {
                    TreeNode pGroupNode = new TreeNode();
                    pGroupNode.Text = pDevice.m_GroupList[j].m_NodesName;
                    pGroupNode.Tag = pDevice.m_GroupList[j];
                    pDevice.m_Node.Nodes.Add(pGroupNode);
                }
            }
            if (m_DevRoot.m_DeviceModuleList.Count > 0)
            {
                DeviceTreeView.SelectedNode = DeviceTreeView.TopNode;
                DeviceTreeView.SelectedNode.Expand();
            }
            IniDevModuleList();
        }
        private void ReSetChildComList()
        {
            int i, j;
            if (m_ChildSelect.m_GroupType == 0)
                return;
            ChildModuleList.Items.Clear();
            for (i = 0; i < m_ChildModuleList.Count; i++)
            {
                CChildModuleRoot pRoot = m_ChildModuleList[i];
                if (pRoot.m_RootType == m_ChildSelect.m_GroupType)
                {
                    m_ChildSelect.m_SelectRoot = pRoot;
                    for (j = 0; j < pRoot.m_ModuleList.Count; j++)
                    {
                        CGroupInterface pGroup = pRoot.m_ModuleList[j];
                        ChildModuleList.Items.Add(pGroup.m_ModuleName);
                    }
                    if (ChildModuleList.Items.Count > 0)
                        ChildModuleList.SelectedIndex = 0;
                    return;
                }
            }
        }
        private void CModulForm_Load(object sender, EventArgs e)
        {
            if (CGlobal.m_DataBase.ConnectDataBase() == false)
            {
                MessageBox.Show("连接数据库失败!");
                System.Environment.Exit(0);
                return;
            }
            IniFontNameList();
            m_ChildModuleList.Clear();
            CChildModuleRoot.m_ModuleNoList.Clear();
            AddNewChildModule("遥测模板", 1);
            AddNewChildModule("遥信模板", 2);
            AddNewChildModule("电量模板", 3);
            AddNewChildModule("参数模板", 4);

            IniChildModuleTreeView();
            LoadDeviceModuleSet();
            DevDataView.Dock = DockStyle.Fill;
            FormPanel.Dock = DockStyle.Fill;
            IniDeviceTreeView();
        }
        private void IniChildModuleTreeView()
        {
            int i, j;
            LineListView.Nodes.Clear();
            for (i = 0; i < m_ChildModuleList.Count; i++)
            {
                CChildModuleRoot pRoot = m_ChildModuleList[i];
                LineListView.Nodes.Add(pRoot.m_Nodes);
                for (j = 0; j < pRoot.m_ModuleList.Count; j++)
                {
                    CGroupInterface pGroup = pRoot.m_ModuleList[j];
                    pRoot.m_Nodes.Nodes.Add(pGroup.m_Nodes);
                }
            }
        }
        private void ChileAddModule_Click(object sender, EventArgs e)
        {
            if (pSelectRoot != null)
            {
                pSelectRoot.AddNewGroup();
            }
            else
            {
                MessageBox.Show("请先选择模板类型");
            }
        }

        private void ChileDelModule_Click(object sender, EventArgs e)
        {
            if (pSelectGroup != null)
            {
                if (MessageBox.Show("将删除此模板及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    CGroupInterface pGroup = pSelectGroup;
                    pGroup.m_Nodes.Remove();
                    pSelectRoot.DeleteGroup(pGroup);
                }
            }
            else
            {
                MessageBox.Show("请先选择要删除的模板");
            }
        }

        private void IniListView(CGroupInterface pGroup)
        {
            ModuleView.ClearAllItem();
            m_ModuleViewSelectIndex = -1;
            if (pGroup == null)
                return;
            pGroup.IniShowListView(ModuleView);
        }
        private void LineListView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = LineListView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType().IsSubclassOf(typeof(CGroupInterface)))
                {
                    pSelectGroup = (CGroupInterface)pSelectNode.Tag;
                    pSelectRoot = pSelectGroup.m_Root;
                }
                else if (pSelectNode.Tag.GetType() == typeof(CChildModuleRoot))
                {
                    pSelectRoot = (CChildModuleRoot)pSelectNode.Tag;
                    pSelectGroup = null;
                }
                IniListView(pSelectGroup);
            }
        }

        private void ChildAdd_Click(object sender, EventArgs e)
        {
            if (pSelectGroup == null)
                return;
            pSelectGroup.AddNewRow();
        }

        private void ChildInsert_Click(object sender, EventArgs e)
        {
            if (m_ModuleViewSelectIndex < 0)
                return;
            if (pSelectGroup == null)
                return;
            pSelectGroup.InsertNewRow(m_ModuleViewSelectIndex);
        }

        private void ChildDelete_Click(object sender, EventArgs e)
        {
            if (m_ModuleViewSelectIndex < 0)
                return;
            if (pSelectGroup == null)
                return;
            int nIndex = m_ModuleViewSelectIndex;
            m_ModuleViewSelectIndex = -1;
            pSelectGroup.DelSelectRow(nIndex);
        }

        private void ClearClear_Click(object sender, EventArgs e)
        {
            if (pSelectGroup == null)
                return;
            pSelectGroup.ClearAllRow();
            m_ModuleViewSelectIndex = -1;
        }

        private void ChildSave_Click(object sender, EventArgs e)
        {
            if (pSelectGroup == null)
                return;
            pSelectGroup.SaveNowSet();
        }

        private void ModuleView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModuleView.SelectedIndices != null && ModuleView.SelectedIndices.Count > 0)
            {
                m_ModuleViewSelectIndex = ModuleView.SelectedItems[0].Index;
            }
            else
            {
                m_ModuleViewSelectIndex = -1;
            }
        }

        private void ModuleView_DoubleClick(object sender, EventArgs e)
        {
            if (pSelectGroup == null)
                return;
            pSelectGroup.m_NowSelectIndex = m_ModuleViewSelectIndex;
            ModuleView.theProcessFun = new MyListView.ProcessDelegate(pSelectGroup.SetNowSelectValue);
        }

        private void ModuleTabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nTabIndex = ModuleTabCtrl.SelectedIndex;
            if (nTabIndex == 0)
            {
                IniShowNowSelectDevGroup();
            }
        }

        private void AddDeivceBtn_Click(object sender, EventArgs e)
        {
            CDeviceMudule pDevice = m_DevRoot.AddNewDevice();
            if (pDevice == null)
                return;
            pDevice.m_Node = new TreeNode();
            pDevice.m_Node.Text = pDevice.m_DeviceMsg.m_ModuleName;
            pDevice.m_Node.Tag = pDevice;

            DeviceTreeView.Nodes.Add(pDevice.m_Node);
            for (int i = 0; i < pDevice.m_GroupList.Count; i++)
            {
                TreeNode pGroupNode = new TreeNode();
                pGroupNode.Text = pDevice.m_GroupList[i].m_NodesName;
                pGroupNode.Tag = pDevice.m_GroupList[i];
                pDevice.m_Node.Nodes.Add(pGroupNode);
            }
            IniDevModuleList();
        }
        private void CopyDevBtn_Click(object sender, EventArgs e)
        {
            CDeviceMudule pDevice = m_DevRoot.CopyDevice(m_NowSelectDevice);
            if (pDevice == null)
                return;
            pDevice.m_Node = new TreeNode();
            pDevice.m_Node.Text = pDevice.m_DeviceMsg.m_ModuleName;
            pDevice.m_Node.Tag = pDevice;

            DeviceTreeView.Nodes.Add(pDevice.m_Node);
            for (int i = 0; i < pDevice.m_GroupList.Count; i++)
            {
                TreeNode pGroupNode = new TreeNode();
                pGroupNode.Text = pDevice.m_GroupList[i].m_NodesName;
                pGroupNode.Tag = pDevice.m_GroupList[i];
                pDevice.m_Node.Nodes.Add(pGroupNode);
            }
            IniDevModuleList();
        }
        private void DelDeviceBtn_Click(object sender, EventArgs e)
        {
            if (m_NowSelectDevice.m_bDefaultFlag)
                return;
            if (MessageBox.Show("将删除此模板及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_DevRoot.DeleteDevice(m_NowSelectDevice);
                IniDevModuleList();
            }
        }
        private void IniShowNowSelectDevGroup()
        {
            GroupSetPanel.Visible = false;
            DevDataView.Visible = false;
            FormPanel.Visible = false;
            toolStrip5.Visible = true;
            if (m_DevBaseModuleSlect != null)
            {

                switch (m_DevBaseModuleSlect.m_DataType)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 5:
                        GroupSetPanel.Visible = true;
                        DevDataView.Visible = true;
                        FirstAddress.Text = "首地址：" + m_DevBaseModuleSlect.m_FaultStartAddress.ToString() +
                            "(0x" + Convert.ToString(m_DevBaseModuleSlect.m_FaultStartAddress, 16).ToUpper() + ")";
                        m_ChildSelect.m_GroupType = m_DevBaseModuleSlect.m_DataType;
                        m_DevBaseModuleSlect.IniShowListView(DevDataView);
                        ReSetChildComList();
                        break;
                    case 4:
                        toolStrip5.Visible = false;
                        FormPanel.Visible = true;
                        m_ChildSelect.m_GroupType = m_DevBaseModuleSlect.m_DataType;
                        if (m_SelectForm != null)
                        {
                            FormPanel.Controls.Remove(m_SelectForm);
                            m_SelectForm.Close();
                        }
                        m_SelectForm = new CParaSetForm();

                        for (int i = 0; i < m_ChildModuleList.Count; i++)
                        {
                            CChildModuleRoot pRoot = m_ChildModuleList[i];
                            if (pRoot.m_RootType == m_ChildSelect.m_GroupType)
                            {
                                ((CParaSetForm)m_SelectForm).m_ModuleConection = pRoot;
                                break;
                            }
                        }
                        ((CParaSetForm)m_SelectForm).m_ParaListOperation.m_ParaDB = m_NowSelectDevice.m_DeviceMsg.m_ModuleDB;
                        ((CParaSetForm)m_SelectForm).m_ParaListOperation.m_strTootTable = m_NowSelectDevice.m_ParaRootTableName;
                        ((CParaSetForm)m_SelectForm).m_ParaListOperation.m_strGroupTable = m_NowSelectDevice.m_ParaGroupTableName;
                        ((CParaSetForm)m_SelectForm).m_ParaListOperation.m_strDataTable = m_NowSelectDevice.m_ParaDataTableName;
                        m_SelectForm.TopLevel = false; //指示子窗体非顶级窗体
                        FormPanel.Controls.Add(m_SelectForm);//将子窗体载入panel
                        m_SelectForm.Show();
                        break;
                    case 6:
                        toolStrip5.Visible = false;
                        FormPanel.Visible = true;
                        if (m_SelectForm != null)
                        {
                            FormPanel.Controls.Remove(m_SelectForm);
                            m_SelectForm.Close();
                        }
                        m_SelectForm = new CLineRealForm();
                        ((CLineRealForm)m_SelectForm).m_LineSetModule = (CDevModuleLineSet)m_DevBaseModuleSlect;
                        ((CLineRealForm)m_SelectForm).m_LineCount = m_NowSelectDevice.m_DeviceMsg.m_LineCount;
                        ((CDevModuleLineSet)m_DevBaseModuleSlect).m_LineCount = m_NowSelectDevice.m_DeviceMsg.m_LineCount;
                        m_DevBaseModuleSlect.IniReadDataList();
                        m_SelectForm.TopLevel = false; //指示子窗体非顶级窗体
                        FormPanel.Controls.Add(m_SelectForm);//将子窗体载入panel
                        m_SelectForm.Show();
                        break;
                }
            }
            else
            {
                if (m_NowSelectDevice != null)
                {
                    toolStrip5.Visible = false;
                    FormPanel.Visible = true;
                    if (m_SelectForm != null)
                    {
                        FormPanel.Controls.Remove(m_SelectForm);
                        m_SelectForm.Close();
                    }
                    m_SelectForm = new CDeviceSetForm();
                    ((CDeviceSetForm)m_SelectForm).pDevice = m_NowSelectDevice;
                    m_SelectForm.TopLevel = false; //指示子窗体非顶级窗体
                    FormPanel.Controls.Add(m_SelectForm);//将子窗体载入panel
                    m_SelectForm.Show();
                }
            }
        }
        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = DeviceTreeView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CDeviceMudule))
                {
                    m_NowSelectDevice = (CDeviceMudule)pSelectNode.Tag;
                    m_DevBaseModuleSlect = null;
                }
                else if (pSelectNode.Tag.GetType().IsSubclassOf(typeof(CBaseModuleGroup)))
                {
                    m_DevBaseModuleSlect = (CBaseModuleGroup)pSelectNode.Tag;
                    m_NowSelectDevice = (CDeviceMudule)pSelectNode.Parent.Tag;
                }
                if (m_NowSelectDevice.m_bDefaultFlag)
                    DelDeviceBtn.Enabled = false;
                else
                    DelDeviceBtn.Enabled = true;
                IniShowNowSelectDevGroup();
            }
        }

        private void ChildModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nIndex = ChildModuleList.SelectedIndex;
            m_ChildSelect.m_SelectGroup = m_ChildSelect.m_SelectRoot.m_ModuleList[nIndex];
        }

        private void DevDataView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DevDataView.SelectedIndices != null && DevDataView.SelectedIndices.Count > 0)
            {
                m_DevViewSelectIndex = DevDataView.SelectedItems[0].Index;
            }
            else
            {
                m_DevViewSelectIndex = -1;
            }
        }

        private void DevAddBtn_Click(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect == null)
                return;
            m_DevBaseModuleSlect.AddNewRow();
        }

        private void DevInsertBtn_Click(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect == null)
                return;
            m_DevBaseModuleSlect.InsertNewRow(m_DevViewSelectIndex);
        }

        private void DevDeleteBtn_Click(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect == null)
                return;
            m_DevBaseModuleSlect.DelSelectRow(m_DevViewSelectIndex);
        }

        private void DevClearBtn_Click(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect == null)
                return;
            m_DevBaseModuleSlect.ClearAllRow();
        }

        private void DevSaveBtn_Click(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect == null)
                return;
            this.Cursor = Cursors.WaitCursor;//等待
            m_DevBaseModuleSlect.SaveNowSet();
            this.Cursor = Cursors.Default;
        }

        private void DevDataView_DoubleClick(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect != null)
            {
                m_DevBaseModuleSlect.m_NowSelectIndex = m_DevViewSelectIndex;
                DevDataView.theProcessFun = new MyListView.ProcessDelegate(m_DevBaseModuleSlect.SetNowSelectValue);
            }
        }

        private void ResetAddress_Click(object sender, EventArgs e)
        {
            if (m_DevBaseModuleSlect == null)
                return;
            m_DevBaseModuleSlect.SetFaultAddress();
        }

        private void AddChildModule_Click(object sender, EventArgs e)
        {
            if (m_ChildSelect.m_SelectGroup != null && m_DevBaseModuleSlect != null)
            {
                int i;
                for (i = 0; i < m_ChildSelect.m_SelectGroup.m_DataList.Count; i++)
                {
                    CDevModuleData pNewData = new CDevModuleData();
                    pNewData.m_DataNo = m_DevBaseModuleSlect.m_DataModuleList.Count + 1;
                    pNewData.m_DataName = FontName.Text + m_ChildSelect.m_SelectGroup.m_DataList[i].m_DataName;
                    pNewData.m_DataUnit = m_ChildSelect.m_SelectGroup.m_DataList[i].m_DataUnit;
                    pNewData.m_fReadio = 1;
                    pNewData.m_Offset = 0;
                    pNewData.m_Address = m_DevBaseModuleSlect.m_FaultStartAddress + m_DevBaseModuleSlect.m_DataModuleList.Count;
                    m_DevBaseModuleSlect.AddNewDataRecord(pNewData);
                }
            }
        }

        private void SaveModuleSelect_Click(object sender, EventArgs e)
        {
            int nIndex = DevModuleComList.SelectedIndex;
            if (nIndex < 0)
                return;
            CGlobal.m_Device.m_Connection.m_DevName = DevModuleComList.SelectedItem.ToString();
            CGlobal.WriteComPara();
            if (MessageBox.Show("需要重启生效，是否重启？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //   System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);  //重新开启当前程序
                //  Close();
                TerminalWorkbench.Presentation.Application.Restart();
            }
        }

        private void CModulForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_DevRoot.CloseDataBase();
            this.Dispose();
        }
    }
    public class CChildModlueSelect
    {
        public int m_GroupType;
        public int m_GroupNo;
        public CGroupInterface m_SelectGroup;
        public CChildModuleRoot m_SelectRoot;
        public CChildModlueSelect()
        {
            m_GroupType = 0;
            m_GroupNo = 0;
            m_SelectGroup = null;
            m_SelectRoot = null;
        }
    }
}
