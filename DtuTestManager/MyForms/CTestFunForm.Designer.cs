namespace DtuTestManager
{
    partial class CTestFunForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTestFunForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnSelectBackColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ComBox_Com = new System.Windows.Forms.ToolStripComboBox();
            this.ComBox_Baund = new System.Windows.Forms.ToolStripComboBox();
            this.ComBox_Check = new System.Windows.Forms.ToolStripComboBox();
            this.BtnComTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnOpenSuperCom = new System.Windows.Forms.ToolStripButton();
            this.SuperTerm = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearAllText = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSendOrder = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnNextOrder = new System.Windows.Forms.Button();
            this.BtnPreOrder = new System.Windows.Forms.Button();
            this.FreshTimer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.PingRichText = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BtnClearPingMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnPingTest = new System.Windows.Forms.Button();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.IPAddress2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.IPAddress1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnSelectBackColor,
            this.toolStripLabel1,
            this.ComBox_Com,
            this.ComBox_Baund,
            this.ComBox_Check,
            this.BtnComTest,
            this.toolStripSeparator1,
            this.BtnOpenSuperCom});
            this.toolStrip1.Location = new System.Drawing.Point(311, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1059, 29);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnSelectBackColor
            // 
            this.BtnSelectBackColor.Image = ((System.Drawing.Image)(resources.GetObject("BtnSelectBackColor.Image")));
            this.BtnSelectBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSelectBackColor.Name = "BtnSelectBackColor";
            this.BtnSelectBackColor.Size = new System.Drawing.Size(126, 26);
            this.BtnSelectBackColor.Text = "选择背景颜色";
            this.BtnSelectBackColor.Click += new System.EventHandler(this.BtnSelectBackColor_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(90, 26);
            this.toolStripLabel1.Text = "串口属性：";
            // 
            // ComBox_Com
            // 
            this.ComBox_Com.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBox_Com.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ComBox_Com.Name = "ComBox_Com";
            this.ComBox_Com.Size = new System.Drawing.Size(80, 29);
            // 
            // ComBox_Baund
            // 
            this.ComBox_Baund.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBox_Baund.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ComBox_Baund.Name = "ComBox_Baund";
            this.ComBox_Baund.Size = new System.Drawing.Size(80, 29);
            // 
            // ComBox_Check
            // 
            this.ComBox_Check.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBox_Check.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ComBox_Check.Name = "ComBox_Check";
            this.ComBox_Check.Size = new System.Drawing.Size(80, 29);
            // 
            // BtnComTest
            // 
            this.BtnComTest.Image = ((System.Drawing.Image)(resources.GetObject("BtnComTest.Image")));
            this.BtnComTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnComTest.Name = "BtnComTest";
            this.BtnComTest.Size = new System.Drawing.Size(121, 26);
            this.BtnComTest.Text = "101串口测试";
            this.BtnComTest.Click += new System.EventHandler(this.BtnComTest_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // BtnOpenSuperCom
            // 
            this.BtnOpenSuperCom.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpenSuperCom.Image")));
            this.BtnOpenSuperCom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnOpenSuperCom.Name = "BtnOpenSuperCom";
            this.BtnOpenSuperCom.Size = new System.Drawing.Size(126, 26);
            this.BtnOpenSuperCom.Text = "打开调试串口";
            this.BtnOpenSuperCom.Click += new System.EventHandler(this.BtnOpenSuperCom_Click);
            // 
            // SuperTerm
            // 
            this.SuperTerm.BackColor = System.Drawing.Color.Lavender;
            this.SuperTerm.ContextMenuStrip = this.contextMenuStrip1;
            this.SuperTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SuperTerm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SuperTerm.Location = new System.Drawing.Point(311, 29);
            this.SuperTerm.Margin = new System.Windows.Forms.Padding(4);
            this.SuperTerm.Name = "SuperTerm";
            this.SuperTerm.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.SuperTerm.Size = new System.Drawing.Size(1059, 494);
            this.SuperTerm.TabIndex = 1;
            this.SuperTerm.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearAllText});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // ClearAllText
            // 
            this.ClearAllText.Name = "ClearAllText";
            this.ClearAllText.Size = new System.Drawing.Size(100, 22);
            this.ClearAllText.Text = "清空";
            this.ClearAllText.Click += new System.EventHandler(this.ClearAllText_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnSendOrder);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(311, 523);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1059, 109);
            this.panel1.TabIndex = 2;
            // 
            // BtnSendOrder
            // 
            this.BtnSendOrder.BackColor = System.Drawing.Color.PowderBlue;
            this.BtnSendOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnSendOrder.Location = new System.Drawing.Point(243, 0);
            this.BtnSendOrder.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSendOrder.Name = "BtnSendOrder";
            this.BtnSendOrder.Size = new System.Drawing.Size(215, 109);
            this.BtnSendOrder.TabIndex = 1;
            this.BtnSendOrder.Text = "空格（Space）";
            this.BtnSendOrder.UseVisualStyleBackColor = false;
            this.BtnSendOrder.Click += new System.EventHandler(this.BtnSendOrder_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnNextOrder);
            this.panel2.Controls.Add(this.BtnPreOrder);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 109);
            this.panel2.TabIndex = 0;
            // 
            // BtnNextOrder
            // 
            this.BtnNextOrder.BackColor = System.Drawing.Color.LightBlue;
            this.BtnNextOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnNextOrder.Location = new System.Drawing.Point(0, 54);
            this.BtnNextOrder.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNextOrder.Name = "BtnNextOrder";
            this.BtnNextOrder.Size = new System.Drawing.Size(243, 55);
            this.BtnNextOrder.TabIndex = 1;
            this.BtnNextOrder.Text = "run updatesys";
            this.BtnNextOrder.UseVisualStyleBackColor = false;
            this.BtnNextOrder.Click += new System.EventHandler(this.BtnNextOrder_Click);
            // 
            // BtnPreOrder
            // 
            this.BtnPreOrder.BackColor = System.Drawing.Color.LightBlue;
            this.BtnPreOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnPreOrder.Location = new System.Drawing.Point(0, 0);
            this.BtnPreOrder.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPreOrder.Name = "BtnPreOrder";
            this.BtnPreOrder.Size = new System.Drawing.Size(243, 55);
            this.BtnPreOrder.TabIndex = 0;
            this.BtnPreOrder.Text = "run updateall";
            this.BtnPreOrder.UseVisualStyleBackColor = false;
            this.BtnPreOrder.Click += new System.EventHandler(this.BtnPreOrder_Click);
            // 
            // FreshTimer
            // 
            this.FreshTimer.Interval = 20;
            this.FreshTimer.Tick += new System.EventHandler(this.FreshTimer_Tick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.PingRichText);
            this.panel3.Controls.Add(this.BtnPingTest);
            this.panel3.Controls.Add(this.toolStrip3);
            this.panel3.Controls.Add(this.toolStrip2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(311, 632);
            this.panel3.TabIndex = 3;
            // 
            // PingRichText
            // 
            this.PingRichText.BackColor = System.Drawing.Color.Azure;
            this.PingRichText.ContextMenuStrip = this.contextMenuStrip2;
            this.PingRichText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingRichText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PingRichText.Location = new System.Drawing.Point(0, 109);
            this.PingRichText.Margin = new System.Windows.Forms.Padding(4);
            this.PingRichText.Name = "PingRichText";
            this.PingRichText.Size = new System.Drawing.Size(311, 523);
            this.PingRichText.TabIndex = 4;
            this.PingRichText.Text = "";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnClearPingMsg});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // BtnClearPingMsg
            // 
            this.BtnClearPingMsg.Name = "BtnClearPingMsg";
            this.BtnClearPingMsg.Size = new System.Drawing.Size(100, 22);
            this.BtnClearPingMsg.Text = "清空";
            this.BtnClearPingMsg.Click += new System.EventHandler(this.BtnClearPingMsg_Click);
            // 
            // BtnPingTest
            // 
            this.BtnPingTest.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BtnPingTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnPingTest.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnPingTest.Location = new System.Drawing.Point(0, 58);
            this.BtnPingTest.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPingTest.Name = "BtnPingTest";
            this.BtnPingTest.Size = new System.Drawing.Size(311, 51);
            this.BtnPingTest.TabIndex = 3;
            this.BtnPingTest.Text = "以太网Ping测试";
            this.BtnPingTest.UseVisualStyleBackColor = false;
            this.BtnPingTest.Click += new System.EventHandler(this.BtnPingTest_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.PowderBlue;
            this.toolStrip3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.IPAddress2});
            this.toolStrip3.Location = new System.Drawing.Point(0, 29);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(311, 29);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(70, 26);
            this.toolStripLabel4.Text = "IP地址2:";
            // 
            // IPAddress2
            // 
            this.IPAddress2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.IPAddress2.Name = "IPAddress2";
            this.IPAddress2.Size = new System.Drawing.Size(140, 29);
            this.IPAddress2.Text = "192.168.11.15";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.PowderBlue;
            this.toolStrip2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.IPAddress1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(311, 29);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(70, 26);
            this.toolStripLabel3.Text = "IP地址1:";
            // 
            // IPAddress1
            // 
            this.IPAddress1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.IPAddress1.Name = "IPAddress1";
            this.IPAddress1.Size = new System.Drawing.Size(140, 29);
            this.IPAddress1.Text = "192.168.10.15";
            // 
            // CTestFunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1370, 632);
            this.Controls.Add(this.SuperTerm);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CTestFunForm";
            this.Text = "辅助功能";
            this.Load += new System.EventHandler(this.CTestFunForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.RichTextBox SuperTerm;
        private System.Windows.Forms.ToolStripComboBox ComBox_Com;
        private System.Windows.Forms.ToolStripComboBox ComBox_Baund;
        private System.Windows.Forms.ToolStripComboBox ComBox_Check;
        private System.Windows.Forms.ToolStripButton BtnComTest;
        private System.Windows.Forms.ToolStripButton BtnOpenSuperCom;
        private System.Windows.Forms.ToolStripButton BtnSelectBackColor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ClearAllText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnNextOrder;
        private System.Windows.Forms.Button BtnPreOrder;
        private System.Windows.Forms.Button BtnSendOrder;
        private System.Windows.Forms.Timer FreshTimer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox IPAddress1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox IPAddress2;
        private System.Windows.Forms.Button BtnPingTest;
        private System.Windows.Forms.RichTextBox PingRichText;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem BtnClearPingMsg;
    }
}