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
    public partial class CFixDataForm : Form
    {
        bool flagSelect = false; //选yxfalse,yc true
        public CFixDataForm()
        {
            InitializeComponent();
        }

        private void CFixDataForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
        //置数
        private void button1_Click(object sender, EventArgs e)
        {
            if (flagSelect == false)
            {
                CGlobal.m_YxNo = Convert.ToInt32(textBox_No.Text);
                CGlobal.m_YxValue = Convert.ToByte(textBox_Value.Text);
                CGlobal.SendTaskOrder(61);
            }
            else
            {
                CGlobal.m_YcNo = Convert.ToInt32(textBox_No.Text);
                CGlobal.m_YcValue = Convert.ToSingle(textBox_Value.Text);
                CGlobal.SendTaskOrder(63);
            }
        }
        //解锁
        private void button2_Click(object sender, EventArgs e)
        {
            if (flagSelect == false)
            {
                CGlobal.SendTaskOrder(62);
            }
            else
            {
                CGlobal.SendTaskOrder(64);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            flagSelect = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            flagSelect = true;
        }
    }
}
