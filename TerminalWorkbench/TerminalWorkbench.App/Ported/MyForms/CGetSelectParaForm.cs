using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public partial class CGetSelectParaForm : Form
    {
        public CParaListOperation m_pNowPara = null;
        public CParaGroupTable m_SelectGroup = null;
        public CParaGroupTable m_NowTreeGroup = null;

        private CParaCtrl m_SourceIndex = null;
        private CParaCtrl m_ItemIndex = null;

        private int m_SourcePlace = -1;
        private int m_ItemPlace = -1;

        private int m_SourceTopPlace = -1;
        private int m_ItemTopPlace = -1;

        public CParaGroupTable m_DefaultGroup = new CParaGroupTable();

        public CGetSelectParaForm()
        {
            InitializeComponent();
        }
        private void IniParaTree()
        {
            int i, j;
            for (i = 0; i < m_pNowPara.m_ParaRootList.Count; i++)
            {
                TreeNode RootNode = new TreeNode();
                RootNode.Text = m_pNowPara.m_ParaRootList[i].m_RootName;
                RootNode.Tag = m_pNowPara.m_ParaRootList[i];
                ParaGroupTree.Nodes.Add(RootNode);

                for (j = 0; j < m_pNowPara.m_ParaRootList[i].m_GroupList.Count; j++)
                {
                    if (m_pNowPara.m_ParaRootList[i].m_GroupList[j].m_bShow == false)
                        continue;
                    TreeNode GroupNode = new TreeNode();
                    GroupNode.Text = m_pNowPara.m_ParaRootList[i].m_GroupList[j].m_GroupName;
                    GroupNode.Tag = m_pNowPara.m_ParaRootList[i].m_GroupList[j];
                    RootNode.Nodes.Add(GroupNode);
                }
            }
        }
        private void ResetShowStatus(CParaGroupTable pTable)
        {
            int i, j;
            for (j = 0; j < pTable.m_ParaDataList.Count; j++)
            {
                pTable.m_ParaDataList[j].m_bShow = true;
            }

            for (i = 0; i < m_SelectGroup.m_ParaDataList.Count; i++)
            {
                for (j = 0; j < pTable.m_ParaDataList.Count; j++)
                {
                    if (m_SelectGroup.m_ParaDataList[i].m_ParaAddress == pTable.m_ParaDataList[j].m_ParaAddress)
                    {
                        pTable.m_ParaDataList[j].m_bShow = false;
                        break;
                    }
                }
            }
        }
        private void IniSourceView(CParaGroupTable pTable)
        {
            ParaListView.Items.Clear();
            if (pTable == null)
                return;
            ResetShowStatus(pTable);

            m_SourceIndex = null;
            string[] strHead = new string[3];
            for (int i = 0; i < pTable.m_ParaDataList.Count; i++)
            {
                CParaCtrl pData = pTable.m_ParaDataList[i];
                if (pData.m_bShow == false)
                    continue;
                strHead[0] = pData.m_ParaNo.ToString();
                strHead[1] = pData.m_ParaName;
                strHead[2] = pData.m_ParaAddress.ToString();

                ListViewItem pItem = new ListViewItem(strHead);
                pItem.Tag = pData;
                ParaListView.Items.Add(pItem);
            }
            ParaListView.Select();
            if (m_SourceTopPlace >= 0 && m_SourceTopPlace < ParaListView.Items.Count)
            {
                ParaListView.TopItem = ParaListView.Items[m_SourceTopPlace];
            }
            if (m_SourcePlace >= 0 && m_SourcePlace < ParaListView.Items.Count)
            {
                ParaListView.Items[m_SourcePlace].Selected = true;
            }
            else
            {
                m_SourcePlace = -1;
            }
        }
        private void IniSelectView(CParaGroupTable pTable)
        {
            HaveSelectView.Items.Clear();
            string[] strHead = new string[3];
            for (int i = 0; i < pTable.m_ParaDataList.Count; i++)
            {
                CParaCtrl pData = pTable.m_ParaDataList[i];
                if (pData.m_bShow == false)
                    continue;
                strHead[0] = pData.m_ParaNo.ToString();
                strHead[1] = pData.m_ParaName;
                strHead[2] = pData.m_ParaAddress.ToString();

                ListViewItem pItem = new ListViewItem(strHead);
                pItem.Tag = pData;
                HaveSelectView.Items.Add(pItem);
            }
            HaveSelectView.Select();
            if (m_ItemTopPlace >= 0 && m_ItemTopPlace < HaveSelectView.Items.Count)
            {
                HaveSelectView.TopItem = HaveSelectView.Items[m_ItemTopPlace];
            }
            if (m_ItemPlace >= 0 && m_ItemPlace < HaveSelectView.Items.Count)
            {
                HaveSelectView.Items[m_ItemPlace].Selected = true;
            }
            else
            {
                if (HaveSelectView.Items.Count > 0)
                {
                    m_ItemPlace = HaveSelectView.Items.Count - 1;
                    HaveSelectView.Items[m_ItemPlace].Selected = true;
                }
                else
                    m_ItemPlace = -1;
            }
        }
        private void CGetSelectParaForm_Load(object sender, EventArgs e)
        {
            ParaListView.GridLines = true;
            ParaListView.FullRowSelect = true;
            ParaListView.View = View.Details;
            ParaListView.Scrollable = true;
            ParaListView.MultiSelect = false;
            ParaListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            ParaListView.Visible = true;

            ParaListView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            ParaListView.Columns.Add("参数名称", 240, HorizontalAlignment.Left);
            ParaListView.Columns.Add("参数地址", 60, HorizontalAlignment.Left);

            HaveSelectView.GridLines = true;
            HaveSelectView.FullRowSelect = true;
            HaveSelectView.View = View.Details;
            HaveSelectView.Scrollable = true;
            HaveSelectView.MultiSelect = false;
            HaveSelectView.HeaderStyle = ColumnHeaderStyle.Clickable;
            HaveSelectView.Visible = true;

            HaveSelectView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            HaveSelectView.Columns.Add("参数名称", 240, HorizontalAlignment.Left);
            HaveSelectView.Columns.Add("参数地址", 60, HorizontalAlignment.Left);

            for (int i = 0; i < m_SelectGroup.m_ParaDataList.Count; i++)
            {
                m_DefaultGroup.m_ParaDataList.Add(m_SelectGroup.m_ParaDataList[i]);
            }
            IniParaTree();
            IniSelectView(m_SelectGroup);
        }

        private void ParaGroupTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = ParaGroupTree.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CParaRootTable))
                {
                    m_NowTreeGroup = null;
                }
                else if (pSelectNode.Tag.GetType() == typeof(CParaGroupTable))
                {
                    m_NowTreeGroup = (CParaGroupTable)pSelectNode.Tag;
                }
                IniSourceView(m_NowTreeGroup);
            }
        }

        private void ParaListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ParaListView.Items.Count; i++)
            {
                ParaListView.Items[i].BackColor = Color.Empty;
            }
            if (ParaListView.SelectedIndices != null && ParaListView.SelectedIndices.Count > 0)
            {
                m_SourceIndex = (CParaCtrl)ParaListView.SelectedItems[0].Tag;
                m_SourcePlace = ParaListView.SelectedItems[0].Index;
                m_SourceTopPlace = ParaListView.TopItem.Index;
            }
            else
            {
                m_SourceIndex = null;
                m_SourcePlace = -1;
            }
        }

        private void HaveSelectView_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < HaveSelectView.Items.Count; i++)
            {
                HaveSelectView.Items[i].BackColor = Color.Empty;
            }
            if (HaveSelectView.SelectedIndices != null && HaveSelectView.SelectedIndices.Count > 0)
            {
                m_ItemIndex = (CParaCtrl)HaveSelectView.SelectedItems[0].Tag;
                m_ItemPlace = HaveSelectView.SelectedItems[0].Index;
                m_ItemTopPlace = HaveSelectView.TopItem.Index;
            }
            else
            {
                m_ItemIndex = null;
                m_ItemPlace = -1;
                m_ItemTopPlace = -1;
            }
        }

        private void BtnAddSelect_Click(object sender, EventArgs e)
        {
            if (m_SourceIndex != null)
            {
                m_SourceIndex.m_bShow = false;
                CParaCtrl pNewPara = new CParaCtrl(m_SourceIndex);
                pNewPara.m_ParaListType = m_SelectGroup.m_GroupIndex;
                m_SelectGroup.m_ParaDataList.Add(pNewPara);

                IniSourceView(m_NowTreeGroup);
                IniSelectView(m_SelectGroup);
            }
        }
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if (m_SourceIndex != null && m_ItemPlace >= 0)
            {
                m_SourceIndex.m_bShow = false;
                CParaCtrl pNewPara = new CParaCtrl(m_SourceIndex);
                pNewPara.m_ParaListType = m_SelectGroup.m_GroupIndex;
                m_SelectGroup.m_ParaDataList.Insert(m_ItemPlace, pNewPara);
                IniSourceView(m_NowTreeGroup);
                IniSelectView(m_SelectGroup);
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (m_ItemIndex != null)
            {
                m_SelectGroup.m_ParaDataList.Remove(m_ItemIndex);
                m_ItemIndex = null;
                IniSourceView(m_NowTreeGroup);
                IniSelectView(m_SelectGroup);
            }
        }

        private void BtnSetOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            m_SelectGroup.m_ParaDataList.Clear();
            for (int i = 0; i < m_DefaultGroup.m_ParaDataList.Count; i++)
            {
                m_SelectGroup.m_ParaDataList.Add(m_DefaultGroup.m_ParaDataList[i]);
            }
            this.Close();
        }

        private void ParaListView_Validated(object sender, EventArgs e)
        {
            for (int i = 0; i < ParaListView.Items.Count; i++)
            {
                ParaListView.Items[i].BackColor = Color.Empty;
            }
            if (m_SourcePlace >= 0 && m_SourcePlace < ParaListView.Items.Count)
            {
                ParaListView.Items[m_SourcePlace].BackColor = SystemColors.Highlight;
            }
        }

        private void HaveSelectView_Validated(object sender, EventArgs e)
        {
            for (int i = 0; i < HaveSelectView.Items.Count; i++)
            {
                HaveSelectView.Items[i].BackColor = Color.Empty;
            }
            if (m_ItemPlace >= 0 && m_ItemPlace < HaveSelectView.Items.Count)
            {
                HaveSelectView.Items[m_ItemPlace].BackColor = SystemColors.Highlight;
            }
        }
    }
}
