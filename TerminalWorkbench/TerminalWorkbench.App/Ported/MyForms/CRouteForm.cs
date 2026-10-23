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
    public partial class CRouteForm : Form
    {
        public CTransStation m_Station = null;
        private CRouteTable m_NowSelectPri = null;
        public int NodeType = 0;
        Form m_SelectForm = null;
        public CRouteForm()
        {
            InitializeComponent();
        }
        private void CRouteForm_Load(object sender, EventArgs e)
        {
            IniTreeView();
        }
        private void IniTreeView()
        {
            int i;
            RouteTreeView.Nodes.Clear();
            DataTable dt = m_Station.m_UserSqlite.GetTableName();
            if (dt == null)
                return;
            m_Station.m_PriRoute[NodeType].Clear();
            m_Station.m_PriNoList[NodeType].Clear();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                string tablename = dt.Rows[i][2].ToString();
                switch (NodeType)
                {
                    case 0:
                        if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYxTab"))
                        {
                            CRouteTable temp = new CRouteTable();
                            temp.m_TableName = tablename;
                            temp.m_Node = new TreeNode();
                            temp.m_TheStation = m_Station;
                            temp.LoadData();
                            m_Station.m_PriRoute[NodeType].Add(temp);
                            int n = Convert.ToInt32(tablename.Substring(8, tablename.Length - 8));
                            m_Station.m_PriNoList[NodeType].Add(n, n);
                        }
                        break;
                    case 1:
                        if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYcTab"))
                        {
                            CRouteTable temp = new CRouteTable();
                            temp.m_TableName = tablename;
                            temp.m_Node = new TreeNode();
                            temp.m_TheStation = m_Station;
                            temp.LoadData();
                            m_Station.m_PriRoute[NodeType].Add(temp);
                            int n = Convert.ToInt32(tablename.Substring(8, tablename.Length - 8));
                            m_Station.m_PriNoList[NodeType].Add(n, n);
                        }
                        break;
                    case 2:
                        if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYkTab"))
                        {
                            CRouteTable temp = new CRouteTable();
                            temp.m_TableName = tablename;
                            temp.m_Node = new TreeNode();
                            temp.m_TheStation = m_Station;
                            temp.LoadData();
                            m_Station.m_PriRoute[NodeType].Add(temp);
                            int n = Convert.ToInt32(tablename.Substring(8, tablename.Length - 8));
                            m_Station.m_PriNoList[NodeType].Add(n, n);
                        }
                        break;
                    case 3:
                        if ((tablename.Length > 9) && (tablename.Substring(0, 9) == "PriSetTab"))
                        {
                            CRouteTable temp = new CRouteTable();
                            temp.m_TableName = tablename;
                            temp.m_Node = new TreeNode();
                            temp.m_TheStation = m_Station;
                            temp.LoadData();
                            m_Station.m_PriRoute[NodeType].Add(temp);
                            int n = Convert.ToInt32(tablename.Substring(9, tablename.Length - 9));
                            m_Station.m_PriNoList[NodeType].Add(n, n);
                        }
                        break;
                    case 4:
                        if ((tablename.Length > 11) && (tablename.Substring(0, 11) == "PriPowerTab"))
                        {
                            CRouteTable temp = new CRouteTable();
                            temp.m_TableName = tablename;
                            temp.m_Node = new TreeNode();
                            temp.m_TheStation = m_Station;
                            temp.LoadData();
                            m_Station.m_PriRoute[NodeType].Add(temp);
                            int n = Convert.ToInt32(tablename.Substring(11, tablename.Length - 11));
                            m_Station.m_PriNoList[NodeType].Add(n, n);
                        }
                        break;
                }
            }


            for (i = 0; i < m_Station.m_PriRoute[NodeType].Count; i++)
            {
                CRouteTable temp = m_Station.m_PriRoute[NodeType][i];
                temp.m_Node.Tag = temp;
                temp.m_Node.Text = temp.m_TableName;
                RouteTreeView.Nodes.Add(temp.m_Node);
            }
        }


        private void IniShowNowSelectGroup()
        {

            FormPanel.Visible = false;
            if (m_NowSelectPri != null)
            {
                FormPanel.Visible = true;
                FormPanel.Dock = DockStyle.Fill;
                if (m_SelectForm != null)
                {
                    FormPanel.Controls.Remove(m_SelectForm);
                    m_SelectForm.Close();
                }
                m_SelectForm = new CRouteFormSetting();
                ((CRouteFormSetting)m_SelectForm).m_Station = m_Station;
                ((CRouteFormSetting)m_SelectForm).m_RouteTable = m_NowSelectPri;
                ((CRouteFormSetting)m_SelectForm).NodeType = NodeType;
                m_SelectForm.TopLevel = false; //指示子窗体非顶级窗体
                FormPanel.Controls.Add(m_SelectForm);//将子窗体载入panel
                m_SelectForm.Show();
            }

        }



        private void RouteTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = RouteTreeView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                m_NowSelectPri = (CRouteTable)pSelectNode.Tag;
                IniShowNowSelectGroup();
            }
        }
        private int GetCanuUseNo(Dictionary<int, int> pMap)
        {
            int nNo = 0;
            while (true)
            {
                if (pMap.ContainsKey(nNo))
                {
                    nNo++;
                    continue;
                }
                else
                {
                    pMap.Add(nNo, nNo);
                    return nNo;
                }
            }
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            string m_TableName = "";
            int n = GetCanuUseNo(m_Station.m_PriNoList[NodeType]);
            switch (NodeType)
            {
                case 0:
                    m_TableName = "PriYxTab" + n.ToString();
                    m_Station.StrListYxRouteTable.Add(m_TableName);
                    break;
                case 1:
                    m_TableName = "PriYcTab" + n.ToString();
                    m_Station.StrListYcRouteTable.Add(m_TableName);
                    break;
                case 2:
                    m_TableName = "PriYkTab" + n.ToString();
                    m_Station.StrListYkRouteTable.Add(m_TableName);
                    break;
                case 4:
                    m_TableName = "PriPowerTab" + n.ToString();
                    m_Station.StrListPowerRouteTable.Add(m_TableName);
                    break;
                case 3:
                    m_TableName = "PriSetTab" + n.ToString();
                    m_Station.StrListSetRouteTable.Add(m_TableName);
                    break;
            }
            CRouteTable temp = new CRouteTable();
            temp.m_TheStation = m_Station;
            temp.m_Node = new TreeNode();
            temp.m_TableName = m_TableName;
            temp.m_Node.Text = m_TableName;
            temp.AddPriTab(m_TableName);
            temp.m_Node.Tag = temp;
            temp.LoadData();
            m_Station.m_PriRoute[NodeType].Add(temp);
            RouteTreeView.Nodes.Add(temp.m_Node);
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            string m_TableName = "";
            int n = GetCanuUseNo(m_Station.m_PriNoList[NodeType]);
            switch (NodeType)
            {
                case 0:
                    m_TableName = "PriYxTab" + n.ToString();
                    m_Station.StrListYxRouteTable.Add(m_TableName);
                    break;
                case 1:
                    m_TableName = "PriYcTab" + n.ToString();
                    m_Station.StrListYcRouteTable.Add(m_TableName);
                    break;
                case 2:
                    m_TableName = "PriYkTab" + n.ToString();
                    m_Station.StrListYkRouteTable.Add(m_TableName);
                    break;
                case 4:
                    m_TableName = "PriPowerTab" + n.ToString();
                    m_Station.StrListPowerRouteTable.Add(m_TableName);
                    break;
                case 3:
                    m_TableName = "PriSetTab" + n.ToString();
                    m_Station.StrListSetRouteTable.Add(m_TableName);
                    break;
            }
            CRouteTable temp = new CRouteTable();
            temp.m_TheStation = m_Station;
            temp.m_Node = new TreeNode();
            temp.m_TableName = m_TableName;
            temp.m_Node.Text = m_TableName;
            temp.CopyPriTab(m_TableName, m_NowSelectPri);
            temp.m_Node.Tag = temp;
            temp.LoadData();
            m_Station.m_PriRoute[NodeType].Add(temp);
            RouteTreeView.Nodes.Add(temp.m_Node);
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将删除该终端及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_NowSelectPri.DeletePritab(m_NowSelectPri, NodeType);
                switch (NodeType)
                {
                    case 0:
                        m_Station.StrListYxRouteTable.Add(m_NowSelectPri.m_TableName);
                        break;
                    case 1:
                        m_Station.StrListYcRouteTable.Add(m_NowSelectPri.m_TableName);
                        break;
                    case 2:
                        m_Station.StrListYkRouteTable.Add(m_NowSelectPri.m_TableName);
                        break;
                    case 4:
                        m_Station.StrListPowerRouteTable.Add(m_NowSelectPri.m_TableName);
                        break;
                    case 3:
                        m_Station.StrListSetRouteTable.Add(m_NowSelectPri.m_TableName);
                        break;
                }
                RouteTreeView.Nodes.Remove(m_NowSelectPri.m_Node);
            }
        }


    }
}
