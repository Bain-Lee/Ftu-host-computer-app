namespace DtuTestManager
{
    partial class CPriOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPriOptionForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SetCountView = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptionView = new DtuTestManager.MyListView(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CountSetView = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RecordView = new DtuTestManager.MyListView(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveSetBtn = new System.Windows.Forms.ToolStripButton();
            this.BtnProtocl = new System.Windows.Forms.ToolStripButton();
            this.BtnCreateTestModule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
            this.groupBox3.Text = "数量配置";
            // 
            // CountSetView
            // 
            this.CountSetView.BackColor = System.Drawing.Color.Lavender;
            this.CountSetView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountSetView.Location = new System.Drawing.Point(3, 17);
            this.CountSetView.Name = "CountSetView";
            this.CountSetView.Size = new System.Drawing.Size(322, 380);
            this.CountSetView.TabIndex = 0;
            this.CountSetView.UseCompatibleStateImageBehavior = false;
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
            this.RecordView.DoubleClick += new System.EventHandler(this.RecordView_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveSetBtn,
            this.BtnProtocl,
            this.toolStripSeparator1,
            this.BtnCreateTestModule});
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
            // BtnProtocl
            // 
            this.BtnProtocl.Image = ((System.Drawing.Image)(resources.GetObject("BtnProtocl.Image")));
            this.BtnProtocl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnProtocl.Name = "BtnProtocl";
            this.BtnProtocl.Size = new System.Drawing.Size(76, 22);
            this.BtnProtocl.Text = "规约配置";
            this.BtnProtocl.Click += new System.EventHandler(this.BtnProtocl_Click);
            // 
            // BtnCreateTestModule
            // 
            this.BtnCreateTestModule.Image = ((System.Drawing.Image)(resources.GetObject("BtnCreateTestModule.Image")));
            this.BtnCreateTestModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCreateTestModule.Name = "BtnCreateTestModule";
            this.BtnCreateTestModule.Size = new System.Drawing.Size(100, 22);
            this.BtnCreateTestModule.Text = "生成测试模板";
            this.BtnCreateTestModule.Click += new System.EventHandler(this.BtnCreateTestModule_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CPriOptionForm
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
            this.Name = "CPriOptionForm";
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
        private System.Windows.Forms.ListView CountSetView;
        private MyListView RecordView;
        private System.Windows.Forms.ToolStripButton BtnProtocl;
        private System.Windows.Forms.ToolStripButton BtnCreateTestModule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}