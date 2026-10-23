using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CAddCom : Form
    {
        private int[] m_ComBaund = new int[] { 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 115200 };

        private CComModule _TempMyCOM = null;

        public CComModule TempMyCOM
        {
            get
            {
                return _TempMyCOM;
            }
            set
            {
                _TempMyCOM = value;
            }
        }

        public CAddCom()
        {
            InitializeComponent();
        }
        public CAddCom(string form_name)
        {
            InitializeComponent();
            this.Text = form_name;
        }
        private void CAddCom_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            combox_ComIndexSel.Items.Clear();

            for (int i = 1; i <= 20;i++ )
                combox_ComIndexSel.Items.Add(i);

            combox_ComBaud.Items.Clear();
            foreach (int nBaund in m_ComBaund)
            {
                combox_ComBaud.Items.Add(nBaund);
            }
           
            if (this.Tag != null)
            {
                CComModule defaultCom = (CComModule)this.Tag;
                combox_ComIndexSel.Text = defaultCom.ComPort.ToString();

                combox_ComBaud.Text = defaultCom.ComBaud.ToString();
            }
            else
            {
                combox_ComIndexSel.SelectedIndex = 0;

                combox_ComBaud.SelectedIndex = 4;
            }
       
        }

       
        private void btn_addIP_Click(object sender, EventArgs e)
        {
            try
            {
                CComModule m_ComGroup = new CComModule();
                m_ComGroup.ComPort = Convert.ToInt32(combox_ComIndexSel.Text);
                m_ComGroup.ComBaud = Convert.ToInt32(combox_ComBaud.Text);

                this._TempMyCOM = m_ComGroup;
                //
                //FreshMainFormMsg();
                //this.Close();
            }
            catch
            {
                MessageBox.Show("串口数据错误!");
                //this._tempMyIP = null;              //不确定是否需要添加
            }           
        }
    }
}
