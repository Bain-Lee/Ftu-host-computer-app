using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CTtestCaseForm
    {
        private ToolStrip toolStrip1;
        private ToolStripButton BtnSendOrder;
        private Panel panel1;
        private MyListView TestCaseView;
        private ToolStripButton BtnConnectDev;
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            BtnSendOrder = new ToolStripButton();
            panel1 = new Panel();
            TestCaseView = new MyListView();
            BtnConnectDev = new ToolStripButton();
            this.toolStrip1.BackColor = System.Drawing.Color.Lavender;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1300, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnConnectDev.Name = "BtnConnectDev";
            this.BtnConnectDev.Size = new System.Drawing.Size(84, 28);
            this.BtnConnectDev.Text = "连接装置";
            this.BtnSendOrder.Name = "BtnSendOrder";
            this.BtnSendOrder.Size = new System.Drawing.Size(84, 28);
            this.BtnSendOrder.Text = "下发命令";
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 567);
            this.panel1.TabIndex = 1;
            this.TestCaseView.BackColor = System.Drawing.Color.GhostWhite;
            this.TestCaseView.Dock = DockStyle.Fill;
            this.TestCaseView.Location = new System.Drawing.Point(0, 0);
            this.TestCaseView.Name = "TestCaseView";
            this.TestCaseView.Size = new System.Drawing.Size(1300, 567);
            this.TestCaseView.TabIndex = 0;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(1300, 598);
            this.Name = "CTtestCaseForm";
            this.Text = "CTtestCaseForm";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.BtnConnectDev,
            this.BtnSendOrder});
            this.panel1.Controls.Add(this.TestCaseView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            BtnConnectDev.Click += BtnConnectDev_Click;
            BtnSendOrder.Click += BtnSendOrder_Click;
            TestCaseView.SelectedIndexChanged += TestCaseView_SelectedIndexChanged;
            FormClosing += CTtestCaseForm_FormClosing;
            Load += CTtestCaseForm_Load;
        }
    }
}
