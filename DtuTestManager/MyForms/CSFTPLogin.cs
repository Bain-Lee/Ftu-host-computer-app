using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CSFTPLogin : Form
    {
        public string m_SelPath = "";
        public SFTPHelper test;
        public CSFTPLogin()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CSFTPMainForm pform = new CSFTPMainForm();
            pform.test = test;
            pform.ShowDialog();
            this.Close(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {          
            label5.Text = "连接中...,请稍后";
            test = new SFTPHelper(textBoxIP.Text, textBoxPortNum.Text, textBoxUserName.Text, textBoxPassWord.Text);
            if (test.Connect() == false)
            {              
                label5.Text = "连接失败";
                return;
            }
            else
            {
                this.Hide();
                CSFTPMainForm pform = new CSFTPMainForm();
                pform.test = test;
                pform.ShowDialog();
                this.Close();
            }
        }

        private void CSFTPLogin_Load(object sender, EventArgs e)
        {            
            textBoxIP.Text = "192.168.11.15";
            textBoxUserName.Text = "root";
            textBoxPassWord.Text = "root";
            textBoxPortNum.Text = "22";
            label5.Text = "";
        }
    }
}
