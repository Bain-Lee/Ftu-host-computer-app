using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace DtuTestManager
{
    public partial class CTermForm : Form
    {
        public CTransStation m_Station = null;
        private CDeviceConfig m_NowSelectTerm = null;      
        Form m_SelectForm = null;
        public CTermForm()
        {
            InitializeComponent();
        }

        private void CTermForm_Load(object sender, EventArgs e)
        {
            if (m_Station.m_Term.IsInial == false)
            {
                m_Station.m_Term.LoadTerminalSet();
            } 
            IniTermTreeView();          
        }

        private void IniTermTreeView()
        {
            int i;
            TermTreeView.Nodes.Clear();
            for (i = 0; i < m_Station.m_Term.m_TermList.Count; i++)
            {

                    CDeviceConfig pDevice = m_Station.m_Term.m_TermList[i];
                    pDevice.m_Node = new TreeNode();
                    pDevice.m_Node.Text = pDevice.m_TermName;               
                    pDevice.m_Node.Tag = pDevice;
                    TermTreeView.Nodes.Add(pDevice.m_Node);

             string m_Nodetype=(string)m_Station.m_Term.m_TermList[i].m_ValueList[2].m_TranValue;
                    pDevice.UpdateINIFileCnt(m_Nodetype);
 
            }
            if (m_Station.m_Term.m_TermList.Count > 0)
            {              
                TermTreeView.SelectedNode = TermTreeView.TopNode;
                TermTreeView.SelectedNode.Expand();              
            }
        }

        private void AddTermBtn_Click(object sender, EventArgs e)
        {
            string m_TermName = "";
            string m_NodeType = "";
            int nYcCount, nYxCount, nYkCount, nParaCount, nYmCount;
            CAddTermForm pform = new CAddTermForm();
            if(pform.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            m_TermName = pform.m_TermName;
            m_NodeType = pform.m_NodeType;  //模板名称
            nYcCount = pform.m_YcCount;
            nYxCount = pform.m_YxCount;
            nYkCount = pform.m_YkCount;
            nParaCount = pform.m_ParaCount;
            nYmCount = pform.m_YmCount;

            if (m_TermName == "") return;
            CDeviceConfig pDevice = m_Station.m_Term.AddNewTerm(m_TermName, m_NodeType, nYcCount, nYxCount, nYkCount, nParaCount, nYmCount);
            if (pDevice == null) return;           
            pDevice.m_Node = new TreeNode();
            pDevice.m_Node.Text = pDevice.m_TermName;
            pDevice.m_Node.Tag = pDevice;
            m_Station.m_Term.AddDeviceRecord(pDevice);
            TermTreeView.Nodes.Add(pDevice.m_Node);
            string FileName = m_Station.m_StationPath + "\\DevName.ini";
            CGlobal.WritePrivateProfileString("TermName", "Name"+pDevice.m_ValueList[1].m_TranValue.ToString(), m_TermName, FileName);

            int m_TermNum = m_Station.m_Term.m_TermList.Count;
            m_Station.UpdateTerminalNum(m_TermNum);
        }

        private void CopyTermBtn_Click(object sender, EventArgs e)
        {
            CDeviceConfig pDevice = m_Station.m_Term.CopyTerm(m_NowSelectTerm);
            if (pDevice == null) return;
            pDevice.m_Node = new TreeNode();
            pDevice.m_Node.Text = pDevice.m_TermName;         
            pDevice.m_Node.Tag = pDevice;
            TermTreeView.Nodes.Add(pDevice.m_Node);

            int m_TermNum = m_Station.m_Term.m_TermList.Count;
            m_Station.UpdateTerminalNum(m_TermNum);
        }

        private void DelTermBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将删除该终端及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_Station.m_Term.DeleteTerm(m_NowSelectTerm);
                int m_TermNum = m_Station.m_Term.m_TermList.Count;
                m_Station.UpdateTerminalNum(m_TermNum);
            }
        }

        private void TermTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = TermTreeView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CDeviceConfig))
                {
                    m_NowSelectTerm = (CDeviceConfig)pSelectNode.Tag;
                }               
                IniShowNowSelectTermGroup();
            }
        }
        private void IniShowNowSelectTermGroup()
        {
           
                FormPanel.Visible = false;           
           
                if (m_NowSelectTerm != null)
                {                   
                    FormPanel.Visible = true;
                    FormPanel.Dock = DockStyle.Fill;
                    if (m_SelectForm != null)
                    {
                        FormPanel.Controls.Remove(m_SelectForm);
                        m_SelectForm.Close();
                    }
                     m_SelectForm = new CTermOptionForm();
                    ((CTermOptionForm)m_SelectForm).pDevice = m_NowSelectTerm;
                    ((CTermOptionForm)m_SelectForm).m_Station = m_Station;
                    m_SelectForm.TopLevel = false; //指示子窗体非顶级窗体
                    FormPanel.Controls.Add(m_SelectForm);//将子窗体载入panel
                    m_SelectForm.Show();
                }
            
        }


      
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       
    }
}
