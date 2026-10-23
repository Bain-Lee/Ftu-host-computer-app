namespace DtuTestManager
{
    partial class CTransForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTransForm));
            this.StationTree = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnNewStation = new System.Windows.Forms.ToolStripButton();
            this.BtnSFTP = new System.Windows.Forms.ToolStripButton();
            this.PanelForm = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DownFileTime = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StationTree
            // 
            this.StationTree.BackColor = System.Drawing.Color.LightSteelBlue;
            this.StationTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.StationTree.Location = new System.Drawing.Point(0, 25);
            this.StationTree.Name = "StationTree";
            this.StationTree.Size = new System.Drawing.Size(169, 537);
            this.StationTree.TabIndex = 1;
            this.StationTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.StationTree_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnNewStation,
            this.BtnSFTP});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1072, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnNewStation
            // 
            this.BtnNewStation.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewStation.Image")));
            this.BtnNewStation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnNewStation.Name = "BtnNewStation";
            this.BtnNewStation.Size = new System.Drawing.Size(76, 22);
            this.BtnNewStation.Text = "新建厂站";
            this.BtnNewStation.Click += new System.EventHandler(this.BtnNewStation_Click);
            // 
            // BtnSFTP
            // 
            this.BtnSFTP.Image = ((System.Drawing.Image)(resources.GetObject("BtnSFTP.Image")));
            this.BtnSFTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSFTP.Name = "BtnSFTP";
            this.BtnSFTP.Size = new System.Drawing.Size(79, 22);
            this.BtnSFTP.Text = "SFTP传输";
            this.BtnSFTP.Click += new System.EventHandler(this.BtnSFTP_Click);
            // 
            // PanelForm
            // 
            this.PanelForm.BackColor = System.Drawing.Color.Azure;
            this.PanelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelForm.Location = new System.Drawing.Point(169, 25);
            this.PanelForm.Name = "PanelForm";
            this.PanelForm.Size = new System.Drawing.Size(903, 537);
            this.PanelForm.TabIndex = 2;
            // 
            // DownFileTime
            // 
            this.DownFileTime.Interval = 20;
            this.DownFileTime.Tick += new System.EventHandler(this.DownFileTime_Tick);
            // 
            // CTransForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 562);
            this.Controls.Add(this.PanelForm);
            this.Controls.Add(this.StationTree);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CTransForm";
            this.Text = "终端配置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CTransForm_FormClosed);
            this.Load += new System.EventHandler(this.CTransForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelForm;
        private System.Windows.Forms.TreeView StationTree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton BtnNewStation;
        private System.Windows.Forms.Timer DownFileTime;
        private System.Windows.Forms.ToolStripButton BtnSFTP;
    }
}