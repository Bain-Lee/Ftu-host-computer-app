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
    public partial class CPriForm : Form
    {
        public CTransStation m_Station = null;
        private CPriConfig m_NowSelectTerm = null;
        Form m_SelectForm = null;
        public CPriForm()
        {
            InitializeComponent();
        }

        private void CPriForm_Load(object sender, EventArgs e)
        {
            m_Station.m_Pri.LoadPriStationSet();
            IniPriTreeView();
        }
        private void IniPriTreeView()
        {
            int i;
            PriTreeView.Nodes.Clear();
            for (i = 0; i < m_Station.m_Pri.m_PriList.Count; i++)
            {
                CPriConfig pDevice = m_Station.m_Pri.m_PriList[i];
                pDevice.m_Node = new TreeNode();
                pDevice.m_Node.Text = (string)(pDevice.m_ValueList[2].m_TranValue);
                pDevice.m_Node.Tag = pDevice;
                PriTreeView.Nodes.Add(pDevice.m_Node);
            }
            if (m_Station.m_Term.m_TermList.Count > 0)
            {
                PriTreeView.SelectedNode = PriTreeView.TopNode;
                PriTreeView.SelectedNode.Expand();
            }
        }

        private void AddTermBtn_Click(object sender, EventArgs e)
        {
            CPriConfig pDevice = m_Station.m_Pri.AddNewPri();
            if (pDevice == null)
                return;
            pDevice.m_Node = new TreeNode();
            pDevice.m_Node.Text = (string)(pDevice.m_ValueList[2].m_TranValue);
            pDevice.m_Node.Tag = pDevice;
            PriTreeView.Nodes.Add(pDevice.m_Node);
            int m_PriNum = m_Station.m_Pri.m_PriList.Count;
            m_Station.UpdatePriNum(m_PriNum);
        }

        private void CopyTermBtn_Click(object sender, EventArgs e)
        {
            CPriConfig pDevice = m_Station.m_Pri.CopyPri(m_NowSelectTerm);
            if (pDevice == null)
                return;
            pDevice.m_Node = new TreeNode();
            pDevice.m_Node.Text = (string)(pDevice.m_ValueList[2].m_TranValue);
            pDevice.m_Node.Tag = pDevice;
            PriTreeView.Nodes.Add(pDevice.m_Node);
            int m_PriNum = m_Station.m_Pri.m_PriList.Count;
            m_Station.UpdatePriNum(m_PriNum);
        }

        private void DelTermBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将删除该转发通道及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_Station.m_Pri.DeletePri(m_NowSelectTerm);
                int m_PriNum = m_Station.m_Pri.m_PriList.Count;
                m_Station.UpdatePriNum(m_PriNum);
            }
        }

        private void PriTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = PriTreeView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CPriConfig))
                {
                    m_NowSelectTerm = (CPriConfig)pSelectNode.Tag;

                }
                IniShowNowSelectTermGroup();
            }
        }
        private void IniShowNowSelectTermGroup()
        {
            FormPanel.Visible = false;
            FormPanel.Dock = DockStyle.Fill;
            if (m_NowSelectTerm != null)
            {
                FormPanel.Visible = true;
                if (m_SelectForm != null)
                {
                    FormPanel.Controls.Remove(m_SelectForm);
                    m_SelectForm.Close();
                }
                m_SelectForm = new CPriOptionForm();
                ((CPriOptionForm)m_SelectForm).pDevice = m_NowSelectTerm;
                ((CPriOptionForm)m_SelectForm).m_Station = m_Station;
                m_SelectForm.TopLevel = false; //指示子窗体非顶级窗体
                FormPanel.Controls.Add(m_SelectForm);//将子窗体载入panel
                m_SelectForm.Show();
            }
        }
    }
}
