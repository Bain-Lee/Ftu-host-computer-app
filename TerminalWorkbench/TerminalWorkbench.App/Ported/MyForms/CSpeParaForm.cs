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
    public partial class CSpeParaForm : Form
    {
        public CSpeParaForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //读取
        private void button1_Click(object sender, EventArgs e)
        {

            IDvalue.Text = "";
            label2.Text = "";
            CGlobal.SendTaskOrder(50);
        }
        //下发
        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            CGlobal.m_Device.m_DevID = IDvalue.Text;
            CGlobal.SendTaskOrder(51);
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1:
                    IDvalue.Text = CGlobal.m_Device.m_DevID;
                    break;
                case UseMessage.MY_MSG_2:
                    label2.Text = "参数下发成功,装置已重启";
                    CGlobal.SendTaskOrder((int)TI_Type.Restart_D);
                    break;
            }
            base.WndProc(ref m);
        }
        private void CSpeParaForm_Load(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_SpeParaFormHandle = (int)this.Handle;
        }
    }
}
