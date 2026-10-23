using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CWaitForm
    {
        private Label WaitMsg;
        private Timer WaitTime;
        private void InitializeComponent()
        {
            WaitMsg = new Label();
            WaitTime = new Timer();
            Own(WaitTime);
            this.WaitMsg.AutoSize = true;
            this.WaitMsg.ForeColor = System.Drawing.Color.DarkGreen;
            this.WaitMsg.Location = new System.Drawing.Point(86, 15);
            this.WaitMsg.Name = "WaitMsg";
            this.WaitMsg.Size = new System.Drawing.Size(221, 19);
            this.WaitMsg.TabIndex = 0;
            this.WaitMsg.Text = "正在读取，请稍等......";
            this.WaitTime.Interval = 1000;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(423, 51);
            this.Name = "CWaitForm";
            this.Controls.Add(this.WaitMsg);
            WaitTime.Tick += WaitTime_Tick;
            Load += CWaitForm_Load;
        }
    }
}
