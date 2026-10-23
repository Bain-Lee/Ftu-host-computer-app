using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CTransForm
    {
        private Panel PanelForm;
        private TreeView StationTree;
        private ToolStrip toolStrip1;
        private ToolTip toolTip1;
        private ToolStripButton BtnNewStation;
        private Timer DownFileTime;
        private ToolStripButton BtnSFTP;
        private void InitializeComponent()
        {
            PanelForm = new Panel();
            StationTree = new TreeView();
            toolStrip1 = new ToolStrip();
            toolTip1 = new ToolTip();
            BtnNewStation = new ToolStripButton();
            DownFileTime = new Timer();
            Own(DownFileTime);
            BtnSFTP = new ToolStripButton();
            this.StationTree.BackColor = System.Drawing.Color.LightSteelBlue;
            this.StationTree.Dock = DockStyle.Left;
            this.StationTree.Location = new System.Drawing.Point(0, 25);
            this.StationTree.Name = "StationTree";
            this.StationTree.Size = new System.Drawing.Size(169, 537);
            this.StationTree.TabIndex = 1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1072, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnNewStation.Name = "BtnNewStation";
            this.BtnNewStation.Size = new System.Drawing.Size(76, 22);
            this.BtnNewStation.Text = "新建厂站";
            this.BtnSFTP.Name = "BtnSFTP";
            this.BtnSFTP.Size = new System.Drawing.Size(79, 22);
            this.BtnSFTP.Text = "SFTP传输";
            this.PanelForm.BackColor = System.Drawing.Color.Azure;
            this.PanelForm.Dock = DockStyle.Fill;
            this.PanelForm.Location = new System.Drawing.Point(169, 25);
            this.PanelForm.Name = "PanelForm";
            this.PanelForm.Size = new System.Drawing.Size(903, 537);
            this.PanelForm.TabIndex = 2;
            this.DownFileTime.Interval = 20;
            this.ClientSize = new System.Drawing.Size(1072, 562);
            this.Name = "CTransForm";
            this.Text = "终端配置";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.BtnNewStation,
            this.BtnSFTP});
            this.Controls.Add(this.PanelForm);
            this.Controls.Add(this.StationTree);
            this.Controls.Add(this.toolStrip1);
            StationTree.AfterSelect += StationTree_AfterSelect;
            BtnNewStation.Click += BtnNewStation_Click;
            BtnSFTP.Click += BtnSFTP_Click;
            DownFileTime.Tick += DownFileTime_Tick;
            FormClosed += CTransForm_FormClosed;
            Load += CTransForm_Load;
        }
    }
}
