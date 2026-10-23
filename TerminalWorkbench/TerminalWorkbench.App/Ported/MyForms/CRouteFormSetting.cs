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
using System.IO;
using System.Collections;
using Microsoft.VisualBasic;
namespace DtuTestManager
{
    public partial class CRouteFormSetting : Form
    {
        public CTransStation m_Station = null;
        public CRouteTable m_RouteTable = null;
        public int NodeType = 0;
        private bool bIniFlag = false;

        private int m_SourcePlace = -1;
        private int m_ItemPlace = -1;

        private int m_SourceTopPlace = -1;
        private int m_ItemTopPlace = -1;
        int DevNo = 0;
        List<string> m_DevName = new List<string>();
        public CRouteFormSetting()
        {
            InitializeComponent();
        }

        private void CRouteFormSetting_Load(object sender, EventArgs e)
        {
            int i, j;
            for (i = 0; i < m_Station.m_Term.m_TermList.Count; i++)
            {
                m_DevName.Add(m_Station.m_Term.m_TermList[i].m_TermName);

            }
            m_DevName.Add("厂站数据");
            for (i = 0; i < m_DevName.Count; i++)
                Dev_Combox.Items.Add(m_DevName[i]);

            if (Dev_Combox.Items.Count > 0)
            {
                Dev_Combox.SelectedIndex = 0;
            }



            for (j = 0; j < m_Station.L_PriTerm.Count; j++)
            {
                for (i = 0; i < m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName.Count; i++)
                {
                    m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName[i].SelectFlag = false;
                }
                int n = 0;
                for (i = 0; (i < m_RouteTable.m_PriData.Count); i++)
                {
                    try
                    {
                        n = m_RouteTable.m_PriData[i].SrcNode;
                        if ((n >= m_Station.L_PriTerm[j].m_Data[NodeType].StartAddr) && (n <= m_Station.L_PriTerm[j].m_Data[NodeType].EndAddr))
                        {
                            int index = n - m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName[0].TotalNode;
                            m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName[index].SelectFlag = true;
                            m_RouteTable.m_PriData[i].Name = m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName[index].Name + "_" + m_DevName[j];
                        }
                    }
                    catch
                    {
                        m_RouteTable.m_PriData[i].Name = "未知";
                    }
                }
            }
            InitLVHead();
            InitTotal_Listview();
            InitPri_ListView();
            bIniFlag = true;
        }

        private void InitLVHead()
        {
            Total_Listview.GridLines = true;
            Total_Listview.FullRowSelect = true;
            Total_Listview.View = View.Details;
            Total_Listview.Scrollable = true;
            Total_Listview.MultiSelect = false;
            Total_Listview.HeaderStyle = ColumnHeaderStyle.Clickable;
            Total_Listview.Visible = true;

            Total_Listview.Columns.Add("总序号", 60, HorizontalAlignment.Center);
            Total_Listview.Columns.Add("数据序号", 60, HorizontalAlignment.Left);
            Total_Listview.Columns.Add("名称", 240, HorizontalAlignment.Left);

            Pri_ListView.GridLines = true;
            Pri_ListView.FullRowSelect = true;
            Pri_ListView.View = View.Details;
            Pri_ListView.Scrollable = true;
            Pri_ListView.MultiSelect = false;
            Pri_ListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            Pri_ListView.Visible = true;

            Pri_ListView.Columns.Add("发送序号", 60, HorizontalAlignment.Center);
            Pri_ListView.Columns.Add("原序号", 60, HorizontalAlignment.Center);
            Pri_ListView.Columns.Add("名称", 240, HorizontalAlignment.Left);

        }

        private void InitTotal_Listview()
        {
            Total_Listview.Items.Clear();
            string[] strHead = new string[3];
            for (int i = 0; i < m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName.Count; i++)
            {
                if (m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[i].SelectFlag == false)
                {
                    strHead[0] = m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[i].TotalNode.ToString();
                    strHead[1] = m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[i].DataNode.ToString();
                    strHead[2] = m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[i].Name.ToString();
                    ListViewItem pItem = new ListViewItem(strHead);
                    Total_Listview.Items.Add(pItem);
                }
            }
            Total_Listview.Select();

            if (m_SourceTopPlace >= 0 && m_SourceTopPlace < Total_Listview.Items.Count)
            {
                Total_Listview.TopItem = Total_Listview.Items[m_SourceTopPlace];
            }
            if (m_SourcePlace >= 0 && m_SourcePlace < Total_Listview.Items.Count)
            {
                Total_Listview.Items[m_SourcePlace].Selected = true;
            }
            else
            {
                m_SourcePlace = -1;
            }
        }

        private void InitPri_ListView()
        {
            Pri_ListView.Items.Clear();
            string[] strHead = new string[3];
            for (int i = 0; i < m_RouteTable.m_PriData.Count; i++)
            {
                strHead[0] = i.ToString();
                strHead[1] = m_RouteTable.m_PriData[i].SrcNode.ToString();
                strHead[2] = m_RouteTable.m_PriData[i].Name.ToString();
                ListViewItem pItem = new ListViewItem(strHead);
                Pri_ListView.Items.Add(pItem);

            }
            Pri_ListView.Select();
            if (m_ItemTopPlace >= 0 && m_ItemTopPlace < Pri_ListView.Items.Count)
            {
                Pri_ListView.TopItem = Pri_ListView.Items[m_ItemTopPlace];
            }
            if (m_ItemPlace >= 0 && m_ItemPlace < Pri_ListView.Items.Count)
            {
                Pri_ListView.Items[m_ItemPlace].Selected = true;
            }
            else
            {
                if (Pri_ListView.Items.Count > 0)
                {
                    m_ItemPlace = Pri_ListView.Items.Count - 1;
                    Pri_ListView.Items[m_ItemPlace].Selected = true;
                }
                else
                    m_ItemPlace = -1;
            }
        }

        private void BtnAddSelect_Click(object sender, EventArgs e)
        {
            if (m_SourcePlace < 0)
                return;
            if (m_SourcePlace >= Total_Listview.Items.Count)
                return;
            this.Enabled = false;
            int Index = Convert.ToInt32(Total_Listview.Items[m_SourcePlace].SubItems[0].Text) - m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[0].TotalNode;
            m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[Index].SelectFlag = true;
            CPriTable temp = new CPriTable();
            temp.SrcNode = m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[Index].TotalNode;
            temp.Name = m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[Index].Name + "_" + Dev_Combox.Text;
            m_RouteTable.m_PriData.Add(temp);
            InitTotal_Listview();
            InitPri_ListView();
            this.Enabled = true;
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if (m_SourcePlace < 0)
                return;
            if (m_SourcePlace >= Total_Listview.Items.Count)
                return;
            this.Enabled = false;
            int Index = Convert.ToInt32(Total_Listview.Items[m_SourcePlace].SubItems[0].Text) - m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[0].TotalNode;
            m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[Index].SelectFlag = true;
            CPriTable temp = new CPriTable();
            temp.SrcNode = m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[Index].TotalNode;
            temp.Name = Dev_Combox.Text + "_" + m_Station.L_PriTerm[DevNo].m_Data[NodeType].m_DataName[Index].Name;
            m_RouteTable.m_PriData.Insert(m_ItemPlace, temp);
            InitTotal_Listview();
            InitPri_ListView();
            this.Enabled = true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (m_ItemPlace < 0)
                return;
            if (m_ItemPlace >= Pri_ListView.Items.Count)
                return;

            this.Enabled = false;
            int Index = Convert.ToInt32(Pri_ListView.Items[m_ItemPlace].SubItems[1].Text);

            for (int j = 0; j < m_Station.L_PriTerm.Count; j++)
            {
                if ((Index >= m_Station.L_PriTerm[j].m_Data[NodeType].StartAddr) && (Index <= m_Station.L_PriTerm[j].m_Data[NodeType].EndAddr))
                {
                    Index = Index - m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName[0].TotalNode;
                    m_Station.L_PriTerm[j].m_Data[NodeType].m_DataName[Index].SelectFlag = false;
                    break;
                }
            }
            m_RouteTable.m_PriData.RemoveAt(m_ItemPlace);
            InitTotal_Listview();
            InitPri_ListView();
            this.Enabled = true;
        }


        public void SaveDB(string FileName)
        {
            int Cnt = 0;
            Cnt = Pri_ListView.Items.Count;
            CGlobal.WritePrivateProfileString("DataCount", "Count", Cnt.ToString(), FileName);
            for (int i = 0; i < Cnt; i++)
            {
                string m_name = Pri_ListView.Items[i].SubItems[2].Text;
                CGlobal.WritePrivateProfileString("NameList", "Name" + i.ToString(), m_name, FileName);
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            m_RouteTable.SaveDataTable();
            this.Enabled = true;
            string FileName = m_Station.m_StationPath + "\\" + m_RouteTable.m_Node.Text + ".ini";
            SaveDB(FileName);
        }

        private void Total_Listview_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Total_Listview.Items.Count; i++)
            {
                Total_Listview.Items[i].BackColor = Color.Empty;
            }
            if (Total_Listview.SelectedIndices != null && Total_Listview.SelectedIndices.Count > 0)
            {
                m_SourcePlace = Total_Listview.SelectedItems[0].Index;
                m_SourceTopPlace = Total_Listview.TopItem.Index;
            }
            else
            {

                m_SourcePlace = -1;
            }
        }

        private void Pri_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Pri_ListView.Items.Count; i++)
            {
                Pri_ListView.Items[i].BackColor = Color.Empty;
            }
            if (Pri_ListView.SelectedIndices != null && Pri_ListView.SelectedIndices.Count > 0)
            {

                m_ItemPlace = Pri_ListView.SelectedItems[0].Index;
                m_ItemTopPlace = Pri_ListView.TopItem.Index;
            }
            else
            {

                m_ItemPlace = -1;
                m_ItemTopPlace = -1;
            }

        }

        private void Total_Listview_Validated(object sender, EventArgs e)
        {
            for (int i = 0; i < Total_Listview.Items.Count; i++)
            {
                Total_Listview.Items[i].BackColor = Color.Empty;
            }
            if (m_SourcePlace >= 0 && m_SourcePlace < Total_Listview.Items.Count)
            {
                Total_Listview.Items[m_SourcePlace].BackColor = SystemColors.Highlight;
            }
        }

        private void Pri_ListView_Validated(object sender, EventArgs e)
        {
            for (int i = 0; i < Pri_ListView.Items.Count; i++)
            {
                Pri_ListView.Items[i].BackColor = Color.Empty;
            }
            if (m_ItemPlace >= 0 && m_ItemPlace < Pri_ListView.Items.Count)
            {
                Pri_ListView.Items[m_ItemPlace].BackColor = SystemColors.Highlight;
            }
        }

        private void Dev_Combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIniFlag == false)
                return;
            DevNo = Dev_Combox.SelectedIndex;
            InitTotal_Listview();
        }
    }
}
