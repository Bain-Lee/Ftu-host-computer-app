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
    public partial class CSFTPLogin : Form
    {
        public string m_SelPath = "";
        public SFTPHelper test;
        private Task<bool> connectionOperation = Task.FromResult(false);
        private bool connecting;
        private bool closeRequested;
        public CSFTPLogin()
        {
            InitializeComponent();
            textBoxPassWord.PasswordChar = '*';
            FormClosing += CSFTPLogin_FormClosing;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CSFTPMainForm pform = new CSFTPMainForm();
            pform.test = test;
            pform.ShowDialog();
            this.Close();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (connecting || closeRequested)
            {
                return;
            }
            connecting = true;
            Enabled = false;
            label5.Text = "连接中...,请稍后";
            try
            {
                test?.Dispose();
                test = new SFTPHelper(textBoxIP.Text, textBoxPortNum.Text, textBoxUserName.Text, textBoxPassWord.Text);
                Own(test);
                connectionOperation = Task.Run(test.Connect);
                bool connected = await connectionOperation;
                if (closeRequested || IsDisposed)
                {
                    return;
                }
                if (!connected)
                {
                    test.Dispose();
                    label5.Text = "连接失败";
                    return;
                }
                Hide();
                using var form = new CSFTPMainForm { test = test };
                form.ShowDialog();
                connecting = false;
                Close();
            }
            finally
            {
                connecting = false;
                if (!IsDisposed)
                {
                    Enabled = !closeRequested;
                }
            }
        }

        private async void CSFTPLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!connecting)
            {
                return;
            }
            e.Cancel = true;
            if (closeRequested)
            {
                return;
            }
            closeRequested = true;
            await connectionOperation;
            connecting = false;
            BeginInvoke(new MethodInvoker(Close));
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
