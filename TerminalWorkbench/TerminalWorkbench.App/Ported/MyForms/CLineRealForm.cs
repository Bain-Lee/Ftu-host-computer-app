using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public partial class CLineRealForm : Form
    {
        public CDevModuleLineSet m_LineSetModule = null;
        public int m_LineCount = 1;
        public CLineRealForm()
        {
            InitializeComponent();
        }
        private void CLineRealForm_Load(object sender, EventArgs e)
        {
            m_LineSetModule.IniShowListView(LineSetView);
            DC1Index.Text = CGlobal.m_Device.m_Connection.m_DC1Index.ToString();
            DC2Index.Text = CGlobal.m_Device.m_Connection.m_DC2Index.ToString();

        }
        private void ClearZero_Click(object sender, EventArgs e)
        {
            m_LineSetModule.ClearZero();
        }

        private void FastSet_Click(object sender, EventArgs e)
        {
            m_LineSetModule.FastSet(Convert.ToInt32(AddSpaceCount.Text));
        }

        private void SaveSet_Click(object sender, EventArgs e)
        {
            m_LineSetModule.SaveNowSet();
            CGlobal.m_Device.m_Connection.m_DC1Index = Convert.ToInt32(DC1Index.Text);
            CGlobal.m_Device.m_Connection.m_DC2Index = Convert.ToInt32(DC2Index.Text);
            CGlobal.WriteComPara();
        }
    }
}
