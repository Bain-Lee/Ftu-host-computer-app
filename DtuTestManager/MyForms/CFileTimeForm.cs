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
    public partial class CFileTimeForm : Form
    {
        public CCallFileTime m_CallFileTime = null;

        public CFileTimeForm()
        {
            InitializeComponent();
        }

        private void SetOK_Click(object sender, EventArgs e)
        {
            if (m_CallFileTime != null)
            {
                if (SelectAll.Checked == true)
                    m_CallFileTime.m_CallType = 0;
                else
                    m_CallFileTime.m_CallType = 1;

                m_CallFileTime.m_StartTime[6] = (Byte)(StartTimePicker.Value.Year % 100);
                m_CallFileTime.m_StartTime[5] = (Byte)(StartTimePicker.Value.Month);
                m_CallFileTime.m_StartTime[4] = (Byte)(StartTimePicker.Value.Day);
                m_CallFileTime.m_StartTime[3] = (Byte)(StartTimePicker.Value.Hour);
                m_CallFileTime.m_StartTime[2] = (Byte)(StartTimePicker.Value.Minute);
                int nMillSecond = StartTimePicker.Value.Second * 1000;
                m_CallFileTime.m_StartTime[1] = (Byte)(nMillSecond/256);
                m_CallFileTime.m_StartTime[0] = (Byte)(nMillSecond%256);

                
                m_CallFileTime.m_EndTime[6] = (Byte)(EndTimePicker.Value.Year % 100);
                m_CallFileTime.m_EndTime[5] = (Byte)(EndTimePicker.Value.Month);
                m_CallFileTime.m_EndTime[4] = (Byte)(EndTimePicker.Value.Day);
                m_CallFileTime.m_EndTime[3] = (Byte)(EndTimePicker.Value.Hour);
                m_CallFileTime.m_EndTime[2] = (Byte)(EndTimePicker.Value.Minute);
                nMillSecond = EndTimePicker.Value.Second * 1000;
                m_CallFileTime.m_EndTime[1] = (Byte)(nMillSecond / 256);
                m_CallFileTime.m_EndTime[0] = (Byte)(nMillSecond % 256);

                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }
        private void CFileTimeForm_Load(object sender, EventArgs e)
        {
            DateTime pNowTime = DateTime.Now;
            DateTime pStartTime = new DateTime(pNowTime.Year, pNowTime.Month, pNowTime.Day, 0, 0, 0);
            StartTimePicker.Value = pStartTime;
            EndTimePicker.Value = DateTime.Now;
        }
    }
}
