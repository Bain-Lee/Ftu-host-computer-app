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
    public partial class CAddTermForm : Form
    {
        public CDeviceConfig pDevice = null;


        public string m_TermName = "";
        public string m_NodeType = "";
        public int m_YcCount = 0;
        public int m_YxCount = 0;
        public int m_YkCount = 0;
        public int m_ParaCount = 0;
        public int m_YmCount = 0;
        public CAddTermForm()
        {
            InitializeComponent();
        }
        private void IniCom_NodeType()
        {
            Com_NodeType.Items.Clear();
            for (int i = 0; i < CGlobal.strDevModulList.Length; i++)
            {
                Com_NodeType.Items.Add(CGlobal.strDevModulList[i]);
            }
        }
        private void CAddTermForm_Load(object sender, EventArgs e)
        {
            IniCom_NodeType();
            TerName.Text = "新建终端";
            Com_NodeType.SelectedIndex = 0;
            YcCount.Text = "0";
            YxCount.Text = "0";
            YkCount.Text = "0";
            ParaCount.Text = "0";
            YmCount.Text = "0";
        }
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            m_TermName = TerName.Text;
            m_NodeType = Com_NodeType.Text;
            m_YcCount = Convert.ToInt32(YcCount.Text);
            m_YxCount = Convert.ToInt32(YxCount.Text);
            m_YkCount = Convert.ToInt32(YkCount.Text);
            m_ParaCount = Convert.ToInt32(ParaCount.Text);
            m_YmCount = Convert.ToInt32(YmCount.Text);
            this.DialogResult = TerminalWorkbench.Presentation.DialogResult.OK;
            this.Close();
        }
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            m_TermName = "";
            this.Close();
        }

        private void CAddTermForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
