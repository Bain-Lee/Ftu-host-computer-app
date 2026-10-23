namespace DtuTestManager
{
    partial class CRealCtrlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CRealCtrlForm));
            this.MyImageList = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.myPanel1 = new DtuTestManager.MyPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LV_YX = new DtuTestManager.MyListView(this.components);
            this.LV_DD = new DtuTestManager.MyListView(this.components);
            this.LV_YC = new DtuTestManager.MyListView(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Btn_StartOrder = new System.Windows.Forms.ToolStripButton();
            this.Btn_StopOrder = new System.Windows.Forms.ToolStripButton();
            this.Btn_HeartTest = new System.Windows.Forms.ToolStripButton();
            this.Btn_CheckNo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_SetTime = new System.Windows.Forms.ToolStripButton();
            this.BtnCallTime = new System.Windows.Forms.ToolStripButton();
            this.Btn_CallTotalData = new System.Windows.Forms.ToolStripButton();
            this.Btn_CallPowerData = new System.Windows.Forms.ToolStripButton();
            this.Btn_GetFrzData = new System.Windows.Forms.ToolStripButton();
            this.BtnSetFixData = new System.Windows.Forms.ToolStripButton();
            this.myPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyImageList
            // 
            this.MyImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MyImageList.ImageStream")));
            this.MyImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.MyImageList.Images.SetKeyName(0, "0Status.png");
            this.MyImageList.Images.SetKeyName(1, "green.png");
            this.MyImageList.Images.SetKeyName(2, "red.png");
            this.MyImageList.Images.SetKeyName(3, "3Status.png");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.LightCyan;
            this.myPanel1.Controls.Add(this.tableLayoutPanel1);
            this.myPanel1.Controls.Add(this.toolStrip1);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(1249, 576);
            this.myPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.6800001F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8800001F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8800001F));
            this.tableLayoutPanel1.Controls.Add(this.LV_YX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LV_DD, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.LV_YC, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 56);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1249, 520);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // LV_YX
            // 
            this.LV_YX.BackColor = System.Drawing.Color.Azure;
            this.LV_YX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LV_YX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_YX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LV_YX.FullRowSelect = true;
            this.LV_YX.GridLines = true;
            this.LV_YX.Location = new System.Drawing.Point(3, 3);
            this.LV_YX.Name = "LV_YX";
            this.LV_YX.OwnerDraw = true;
            this.LV_YX.ProgressColumnIndex = -1;
            this.LV_YX.Size = new System.Drawing.Size(450, 514);
            this.LV_YX.TabIndex = 0;
            this.LV_YX.UseCompatibleStateImageBehavior = false;
            this.LV_YX.View = System.Windows.Forms.View.Details;
            // 
            // LV_DD
            // 
            this.LV_DD.BackColor = System.Drawing.Color.Azure;
            this.LV_DD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LV_DD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_DD.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LV_DD.FullRowSelect = true;
            this.LV_DD.GridLines = true;
            this.LV_DD.Location = new System.Drawing.Point(858, 3);
            this.LV_DD.Name = "LV_DD";
            this.LV_DD.OwnerDraw = true;
            this.LV_DD.ProgressColumnIndex = -1;
            this.LV_DD.Size = new System.Drawing.Size(375, 514);
            this.LV_DD.TabIndex = 2;
            this.LV_DD.UseCompatibleStateImageBehavior = false;
            this.LV_DD.View = System.Windows.Forms.View.Details;
            // 
            // LV_YC
            // 
            this.LV_YC.BackColor = System.Drawing.Color.Azure;
            this.LV_YC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LV_YC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_YC.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LV_YC.FullRowSelect = true;
            this.LV_YC.GridLines = true;
            this.LV_YC.Location = new System.Drawing.Point(467, 3);
            this.LV_YC.Name = "LV_YC";
            this.LV_YC.OwnerDraw = true;
            this.LV_YC.ProgressColumnIndex = -1;
            this.LV_YC.Size = new System.Drawing.Size(375, 514);
            this.LV_YC.TabIndex = 1;
            this.LV_YC.UseCompatibleStateImageBehavior = false;
            this.LV_YC.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_StartOrder,
            this.Btn_StopOrder,
            this.Btn_HeartTest,
            this.Btn_CheckNo,
            this.toolStripSeparator1,
            this.Btn_SetTime,
            this.BtnCallTime,
            this.Btn_CallTotalData,
            this.Btn_CallPowerData,
            this.Btn_GetFrzData,
            this.BtnSetFixData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1249, 56);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Btn_StartOrder
            // 
            this.Btn_StartOrder.Image = ((System.Drawing.Image)(resources.GetObject("Btn_StartOrder.Image")));
            this.Btn_StartOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_StartOrder.Name = "Btn_StartOrder";
            this.Btn_StartOrder.Size = new System.Drawing.Size(60, 53);
            this.Btn_StartOrder.Text = "启动命令";
            this.Btn_StartOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_StartOrder.Click += new System.EventHandler(this.Btn_StartOrder_Click);
            // 
            // Btn_StopOrder
            // 
            this.Btn_StopOrder.Image = ((System.Drawing.Image)(resources.GetObject("Btn_StopOrder.Image")));
            this.Btn_StopOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_StopOrder.Name = "Btn_StopOrder";
            this.Btn_StopOrder.Size = new System.Drawing.Size(60, 53);
            this.Btn_StopOrder.Text = "停止命令";
            this.Btn_StopOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_StopOrder.Click += new System.EventHandler(this.Btn_StopOrder_Click);
            // 
            // Btn_HeartTest
            // 
            this.Btn_HeartTest.Image = ((System.Drawing.Image)(resources.GetObject("Btn_HeartTest.Image")));
            this.Btn_HeartTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_HeartTest.Name = "Btn_HeartTest";
            this.Btn_HeartTest.Size = new System.Drawing.Size(60, 53);
            this.Btn_HeartTest.Text = "通讯测试";
            this.Btn_HeartTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_HeartTest.Click += new System.EventHandler(this.Btn_HeartTest_Click);
            // 
            // Btn_CheckNo
            // 
            this.Btn_CheckNo.Image = ((System.Drawing.Image)(resources.GetObject("Btn_CheckNo.Image")));
            this.Btn_CheckNo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_CheckNo.Name = "Btn_CheckNo";
            this.Btn_CheckNo.Size = new System.Drawing.Size(60, 53);
            this.Btn_CheckNo.Text = "核对序号";
            this.Btn_CheckNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_CheckNo.Click += new System.EventHandler(this.Btn_CheckNo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // Btn_SetTime
            // 
            this.Btn_SetTime.Image = ((System.Drawing.Image)(resources.GetObject("Btn_SetTime.Image")));
            this.Btn_SetTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_SetTime.Name = "Btn_SetTime";
            this.Btn_SetTime.Size = new System.Drawing.Size(60, 53);
            this.Btn_SetTime.Text = "时钟对时";
            this.Btn_SetTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_SetTime.Click += new System.EventHandler(this.Btn_SetTime_Click);
            // 
            // BtnCallTime
            // 
            this.BtnCallTime.Image = ((System.Drawing.Image)(resources.GetObject("BtnCallTime.Image")));
            this.BtnCallTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCallTime.Name = "BtnCallTime";
            this.BtnCallTime.Size = new System.Drawing.Size(60, 53);
            this.BtnCallTime.Text = "召唤时钟";
            this.BtnCallTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnCallTime.Click += new System.EventHandler(this.BtnCallTime_Click);
            // 
            // Btn_CallTotalData
            // 
            this.Btn_CallTotalData.Image = ((System.Drawing.Image)(resources.GetObject("Btn_CallTotalData.Image")));
            this.Btn_CallTotalData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_CallTotalData.Name = "Btn_CallTotalData";
            this.Btn_CallTotalData.Size = new System.Drawing.Size(60, 53);
            this.Btn_CallTotalData.Text = "数据总召";
            this.Btn_CallTotalData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_CallTotalData.Click += new System.EventHandler(this.Btn_CallTotalData_Click);
            // 
            // Btn_CallPowerData
            // 
            this.Btn_CallPowerData.Image = ((System.Drawing.Image)(resources.GetObject("Btn_CallPowerData.Image")));
            this.Btn_CallPowerData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_CallPowerData.Name = "Btn_CallPowerData";
            this.Btn_CallPowerData.Size = new System.Drawing.Size(60, 53);
            this.Btn_CallPowerData.Text = "召读电度";
            this.Btn_CallPowerData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_CallPowerData.Click += new System.EventHandler(this.Btn_CallPowerData_Click);
            // 
            // Btn_GetFrzData
            // 
            this.Btn_GetFrzData.Image = ((System.Drawing.Image)(resources.GetObject("Btn_GetFrzData.Image")));
            this.Btn_GetFrzData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_GetFrzData.Name = "Btn_GetFrzData";
            this.Btn_GetFrzData.Size = new System.Drawing.Size(60, 53);
            this.Btn_GetFrzData.Text = "瞬时冻结";
            this.Btn_GetFrzData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_GetFrzData.Click += new System.EventHandler(this.Btn_GetFrzData_Click);
            // 
            // BtnSetFixData
            // 
            this.BtnSetFixData.Image = ((System.Drawing.Image)(resources.GetObject("BtnSetFixData.Image")));
            this.BtnSetFixData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSetFixData.Name = "BtnSetFixData";
            this.BtnSetFixData.Size = new System.Drawing.Size(60, 53);
            this.BtnSetFixData.Text = "置数功能";
            this.BtnSetFixData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnSetFixData.Click += new System.EventHandler(this.BtnSetFixData_Click);
            // 
            // CRealCtrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 576);
            this.Controls.Add(this.myPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CRealCtrlForm";
            this.Text = "实时监控";
            this.Load += new System.EventHandler(this.CRealCtrlForm_Load);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Btn_StartOrder;
        private System.Windows.Forms.ToolStripButton Btn_StopOrder;
        private System.Windows.Forms.ToolStripButton Btn_HeartTest;
        private System.Windows.Forms.ToolStripButton Btn_CheckNo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Btn_CallTotalData;
        private System.Windows.Forms.ToolStripButton Btn_CallPowerData;
        private System.Windows.Forms.ToolStripButton Btn_SetTime;
        private MyListView LV_YX;
        private MyListView LV_YC;
        private MyListView LV_DD;
        private System.Windows.Forms.ImageList MyImageList;
        private System.Windows.Forms.ToolStripButton BtnCallTime;
        private System.Windows.Forms.ToolStripButton Btn_GetFrzData;
        private MyPanel myPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton BtnSetFixData;
    }
}