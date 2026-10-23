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
    public partial class CWaitForm : Form
    {
        public static bool m_bCose = false;
        private int nStatus = 0;
        private int nWaitSec = 20;
        private string[] strWait = new string[7];
        public CWaitForm()
        {
            InitializeComponent();
        }

        private void WaitTime_Tick(object sender, EventArgs e)
        {
            WaitMsg.Text = strWait[nStatus];
            nStatus++;
            nStatus %= 7;
            nWaitSec--;
            if (m_bCose)
            {
                this.Close();
                WaitTime.Stop();
                return;
            }
            if (nWaitSec <= 0) this.Close();
        }

        private void CWaitForm_Load(object sender, EventArgs e)
        {
            strWait[0] = "正在读取，请稍等      ";
            strWait[1] = "正在读取，请稍等.     ";
            strWait[2] = "正在读取，请稍等..    ";
            strWait[3] = "正在读取，请稍等...   ";
            strWait[4] = "正在读取，请稍等....  ";
            strWait[5] = "正在读取，请稍等..... ";
            strWait[6] = "正在读取，请稍等......";
            m_bCose = false;
            WaitTime.Start();
        }
    }
}
