namespace DtuTestManager
{
    partial class CTermOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTermOptionForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SetCountView = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptionView = new DtuTestManager.MyListView(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CountSetView = new DtuTestManager.MyListView(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RecordView = new DtuTestManager.MyListView(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveSetBtn = new System.Windows.Forms.ToolStripButton();
            this.BtnYx = new System.Windows.Forms.ToolStripButton();
            this.BtnYc = new System.Windows.Forms.ToolStripButton();
            this.BtnYk = new System.Windows.Forms.ToolStripButton();
            this.BtnSet = new System.Windows.Forms.ToolStripButton();
            this.BtnYm = new System.Windows.Forms.ToolStripButton();
            this.BtnProtocl = new System.Windows.Forms.ToolStripButton();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Location = new System.Drawing.Point(334, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 100);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SetCountView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "";
            this.groupBox2.Text = "配置数量";
            // 
            // SetCountView
            // 
            this.SetCountView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetCountView.Location = new System.Drawing.Point(3, 17);
            this.SetCountView.Name = "SetCountView";
            this.SetCountView.Size = new System.Drawing.Size(319, 80);
            this.SetCountView.TabIndex = 0;
            this.SetCountView.UseCompatibleStateImageBehavior = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1002, 431);
            this.panel3.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1002, 406);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptionView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 400);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性配置";
            // 
            // OptionView
            // 
            this.OptionView.BackColor = System.Drawing.Color.Lavender;
            this.OptionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionView.FullRowSelect = true;
            this.OptionView.GridLines = true;
            this.OptionView.Location = new System.Drawing.Point(3, 17);
            this.OptionView.Name = "OptionView";
            this.OptionView.OwnerDraw = true;
            this.OptionView.ProgressColumnIndex = -1;
            this.OptionView.Size = new System.Drawing.Size(322, 380);
            this.OptionView.TabIndex = 0;
            this.OptionView.UseCompatibleStateImageBehavior = false;
            this.OptionView.View = System.Windows.Forms.View.Details;
            this.OptionView.SelectedIndexChanged += new System.EventHandler(this.OptionView_SelectedIndexChanged);
            this.OptionView.Click += new System.EventHandler(this.OptionView_Click);
            this.OptionView.DoubleClick += new System.EventHandler(this.OptionView_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CountSetView);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(337, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 400);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "默认配置";
            // 
            // CountSetView
            // 
            this.CountSetView.BackColor = System.Drawing.Color.Lavender;
            this.CountSetView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountSetView.FullRowSelect = true;
            this.CountSetView.GridLines = true;
            this.CountSetView.Location = new System.Drawing.Point(3, 17);
            this.CountSetView.Name = "CountSetView";
            this.CountSetView.OwnerDraw = true;
            this.CountSetView.ProgressColumnIndex = -1;
            this.CountSetView.Size = new System.Drawing.Size(322, 380);
            this.CountSetView.TabIndex = 0;
            this.CountSetView.UseCompatibleStateImageBehavior = false;
            this.CountSetView.View = System.Windows.Forms.View.Details;
            this.CountSetView.SelectedIndexChanged += new System.EventHandler(this.CountSetView_SelectedIndexChanged);
            this.CountSetView.Click += new System.EventHandler(this.CountSetView_Click);
            this.CountSetView.DoubleClick += new System.EventHandler(this.CountSetView_DoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RecordView);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(671, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(328, 400);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "记录配置";
            // 
            // RecordView
            // 
            this.RecordView.BackColor = System.Drawing.Color.Lavender;
            this.RecordView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecordView.FullRowSelect = true;
            this.RecordView.GridLines = true;
            this.RecordView.Location = new System.Drawing.Point(3, 17);
            this.RecordView.Name = "RecordView";
            this.RecordView.OwnerDraw = true;
            this.RecordView.ProgressColumnIndex = -1;
            this.RecordView.Size = new System.Drawing.Size(322, 380);
            this.RecordView.TabIndex = 0;
            this.RecordView.UseCompatibleStateImageBehavior = false;
            this.RecordView.View = System.Windows.Forms.View.Details;
            this.RecordView.SelectedIndexChanged += new System.EventHandler(this.RecordView_SelectedIndexChanged);
            this.RecordView.Click += new System.EventHandler(this.RecordView_Click);
            this.RecordView.DoubleClick += new System.EventHandler(this.RecordView_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveSetBtn,
            this.BtnYx,
            this.BtnYc,
            this.BtnYk,
            this.BtnSet,
            this.BtnYm,
            this.BtnProtocl});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1002, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveSetBtn
            // 
            this.SaveSetBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveSetBtn.Image")));
            this.SaveSetBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveSetBtn.Name = "SaveSetBtn";
            this.SaveSetBtn.Size = new System.Drawing.Size(52, 22);
            this.SaveSetBtn.Text = "保存";
            this.SaveSetBtn.Click += new System.EventHandler(this.SaveSetBtn_Click);
            // 
            // BtnYx
            // 
            this.BtnYx.Image = ((System.Drawing.Image)(resources.GetObject("BtnYx.Image")));
            this.BtnYx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnYx.Name = "BtnYx";
            this.BtnYx.Size = new System.Drawing.Size(76, 22);
            this.BtnYx.Text = "遥信点表";
            this.BtnYx.Click += new System.EventHandler(this.BtnYx_Click);
            // 
            // BtnYc
            // 
            this.BtnYc.Image = ((System.Drawing.Image)(resources.GetObject("BtnYc.Image")));
            this.BtnYc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnYc.Name = "BtnYc";
            this.BtnYc.Size = new System.Drawing.Size(76, 22);
            this.BtnYc.Text = "遥测点表";
            this.BtnYc.Click += new System.EventHandler(this.BtnYc_Click);
            // 
            // BtnYk
            // 
            this.BtnYk.Image = ((System.Drawing.Image)(resources.GetObject("BtnYk.Image")));
            this.BtnYk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnYk.Name = "BtnYk";
            this.BtnYk.Size = new System.Drawing.Size(76, 22);
            this.BtnYk.Text = "遥控点表";
            this.BtnYk.Click += new System.EventHandler(this.BtnYk_Click);
            // 
            // BtnSet
            // 
            this.BtnSet.Image = ((System.Drawing.Image)(resources.GetObject("BtnSet.Image")));
            this.BtnSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSet.Name = "BtnSet";
            this.BtnSet.Size = new System.Drawing.Size(76, 22);
            this.BtnSet.Text = "定值点表";
            this.BtnSet.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // BtnYm
            // 
            this.BtnYm.Image = ((System.Drawing.Image)(resources.GetObject("BtnYm.Image")));
            this.BtnYm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnYm.Name = "BtnYm";
            this.BtnYm.Size = new System.Drawing.Size(76, 22);
            this.BtnYm.Text = "遥脉点表";
            this.BtnYm.Click += new System.EventHandler(this.BtnYm_Click);
            // 
            // BtnProtocl
            // 
            this.BtnProtocl.Image = ((System.Drawing.Image)(resources.GetObject("BtnProtocl.Image")));
            this.BtnProtocl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnProtocl.Name = "BtnProtocl";
            this.BtnProtocl.Size = new System.Drawing.Size(76, 22);
            this.BtnProtocl.Text = "规约配置";
            this.BtnProtocl.Click += new System.EventHandler(this.BtnProtocl_Click);
            // 
            // CTermOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1002, 431);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CTermOptionForm";
            this.Text = "CTranStationOptionForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CTranStationOptionForm_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView SetCountView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripButton SaveSetBtn;
        private MyListView OptionView;
        private MyListView CountSetView;
        private MyListView RecordView;
        private System.Windows.Forms.ToolStripButton BtnYx;
        private System.Windows.Forms.ToolStripButton BtnYc;
        private System.Windows.Forms.ToolStripButton BtnYk;
        private System.Windows.Forms.ToolStripButton BtnYm;
        private System.Windows.Forms.ToolStripButton BtnSet;
        private System.Windows.Forms.ToolStripButton BtnProtocl;

    }
}