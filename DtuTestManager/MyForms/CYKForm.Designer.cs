namespace DtuTestManager
{
    partial class CYKForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.myPanel1 = new DtuTestManager.MyPanel();
            this.UseMingWen = new System.Windows.Forms.RadioButton();
            this.UseNoSign = new System.Windows.Forms.RadioButton();
            this.UseErrRand = new System.Windows.Forms.RadioButton();
            this.UseNoTime = new System.Windows.Forms.RadioButton();
            this.UseErrTime = new System.Windows.Forms.RadioButton();
            this.Use_Normal = new System.Windows.Forms.RadioButton();
            this.UseNoRand = new System.Windows.Forms.RadioButton();
            this.UseErrSign = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.YK_H_Sel_btn = new System.Windows.Forms.Button();
            this.YK_F_Sel_btn = new System.Windows.Forms.Button();
            this.YK_H_C_btn = new System.Windows.Forms.Button();
            this.YK_F_Ex_btn = new System.Windows.Forms.Button();
            this.YK_F_C_btn = new System.Windows.Forms.Button();
            this.YK_H_Ex_btn = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.LV_YK = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.myPanel1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(841, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 585);
            this.panel1.TabIndex = 1;
            // 
            // myPanel1
            // 
            this.myPanel1.Controls.Add(this.UseMingWen);
            this.myPanel1.Controls.Add(this.UseNoSign);
            this.myPanel1.Controls.Add(this.UseErrRand);
            this.myPanel1.Controls.Add(this.UseNoTime);
            this.myPanel1.Controls.Add(this.UseErrTime);
            this.myPanel1.Controls.Add(this.Use_Normal);
            this.myPanel1.Controls.Add(this.UseNoRand);
            this.myPanel1.Controls.Add(this.UseErrSign);
            this.myPanel1.Location = new System.Drawing.Point(359, 31);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(149, 391);
            this.myPanel1.TabIndex = 16;
            // 
            // UseMingWen
            // 
            this.UseMingWen.AutoSize = true;
            this.UseMingWen.Location = new System.Drawing.Point(16, 258);
            this.UseMingWen.Name = "UseMingWen";
            this.UseMingWen.Size = new System.Drawing.Size(71, 16);
            this.UseMingWen.TabIndex = 14;
            this.UseMingWen.Text = "下发明文";
            this.UseMingWen.UseVisualStyleBackColor = true;
            this.UseMingWen.CheckedChanged += new System.EventHandler(this.UseMingWen_CheckedChanged_1);
            // 
            // UseNoSign
            // 
            this.UseNoSign.AutoSize = true;
            this.UseNoSign.Location = new System.Drawing.Point(16, 48);
            this.UseNoSign.Name = "UseNoSign";
            this.UseNoSign.Size = new System.Drawing.Size(71, 16);
            this.UseNoSign.TabIndex = 8;
            this.UseNoSign.Text = "不带签名";
            this.UseNoSign.UseVisualStyleBackColor = true;
            this.UseNoSign.CheckedChanged += new System.EventHandler(this.UseNoSign_CheckedChanged);
            // 
            // UseErrRand
            // 
            this.UseErrRand.AutoSize = true;
            this.UseErrRand.Location = new System.Drawing.Point(16, 223);
            this.UseErrRand.Name = "UseErrRand";
            this.UseErrRand.Size = new System.Drawing.Size(95, 16);
            this.UseErrRand.TabIndex = 13;
            this.UseErrRand.Text = "错误的随机数";
            this.UseErrRand.UseVisualStyleBackColor = true;
            this.UseErrRand.CheckedChanged += new System.EventHandler(this.UseErrRand_CheckedChanged);
            // 
            // UseNoTime
            // 
            this.UseNoTime.AutoSize = true;
            this.UseNoTime.Location = new System.Drawing.Point(16, 118);
            this.UseNoTime.Name = "UseNoTime";
            this.UseNoTime.Size = new System.Drawing.Size(71, 16);
            this.UseNoTime.TabIndex = 10;
            this.UseNoTime.Text = "不带时间";
            this.UseNoTime.UseVisualStyleBackColor = true;
            this.UseNoTime.CheckedChanged += new System.EventHandler(this.UseNoTime_CheckedChanged);
            // 
            // UseErrTime
            // 
            this.UseErrTime.AutoSize = true;
            this.UseErrTime.Location = new System.Drawing.Point(16, 153);
            this.UseErrTime.Name = "UseErrTime";
            this.UseErrTime.Size = new System.Drawing.Size(83, 16);
            this.UseErrTime.TabIndex = 11;
            this.UseErrTime.Text = "错误的时间";
            this.UseErrTime.UseVisualStyleBackColor = true;
            this.UseErrTime.CheckedChanged += new System.EventHandler(this.UseErrTime_CheckedChanged);
            // 
            // Use_Normal
            // 
            this.Use_Normal.AutoSize = true;
            this.Use_Normal.Location = new System.Drawing.Point(16, 13);
            this.Use_Normal.Name = "Use_Normal";
            this.Use_Normal.Size = new System.Drawing.Size(71, 16);
            this.Use_Normal.TabIndex = 15;
            this.Use_Normal.Text = "正常测试";
            this.Use_Normal.UseVisualStyleBackColor = true;
            this.Use_Normal.CheckedChanged += new System.EventHandler(this.Use_Normal_CheckedChanged);
            // 
            // UseNoRand
            // 
            this.UseNoRand.AutoSize = true;
            this.UseNoRand.Location = new System.Drawing.Point(16, 188);
            this.UseNoRand.Name = "UseNoRand";
            this.UseNoRand.Size = new System.Drawing.Size(83, 16);
            this.UseNoRand.TabIndex = 12;
            this.UseNoRand.Text = "不带随机数";
            this.UseNoRand.UseVisualStyleBackColor = true;
            this.UseNoRand.CheckedChanged += new System.EventHandler(this.UseNoRand_CheckedChanged);
            // 
            // UseErrSign
            // 
            this.UseErrSign.AutoSize = true;
            this.UseErrSign.Location = new System.Drawing.Point(16, 83);
            this.UseErrSign.Name = "UseErrSign";
            this.UseErrSign.Size = new System.Drawing.Size(83, 16);
            this.UseErrSign.TabIndex = 9;
            this.UseErrSign.Text = "错误的签名";
            this.UseErrSign.UseVisualStyleBackColor = true;
            this.UseErrSign.CheckedChanged += new System.EventHandler(this.UseErrSign_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.YK_H_Sel_btn);
            this.groupBox2.Controls.Add(this.YK_F_Sel_btn);
            this.groupBox2.Controls.Add(this.YK_H_C_btn);
            this.groupBox2.Controls.Add(this.YK_F_Ex_btn);
            this.groupBox2.Controls.Add(this.YK_F_C_btn);
            this.groupBox2.Controls.Add(this.YK_H_Ex_btn);
            this.groupBox2.Location = new System.Drawing.Point(24, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 391);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "遥控操作";
            // 
            // YK_H_Sel_btn
            // 
            this.YK_H_Sel_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.YK_H_Sel_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YK_H_Sel_btn.Location = new System.Drawing.Point(176, 76);
            this.YK_H_Sel_btn.Name = "YK_H_Sel_btn";
            this.YK_H_Sel_btn.Size = new System.Drawing.Size(118, 35);
            this.YK_H_Sel_btn.TabIndex = 5;
            this.YK_H_Sel_btn.Text = "遥控合(选择)";
            this.YK_H_Sel_btn.UseVisualStyleBackColor = false;
            this.YK_H_Sel_btn.Click += new System.EventHandler(this.YK_S_H_btn_Click);
            // 
            // YK_F_Sel_btn
            // 
            this.YK_F_Sel_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.YK_F_Sel_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YK_F_Sel_btn.Location = new System.Drawing.Point(26, 76);
            this.YK_F_Sel_btn.Name = "YK_F_Sel_btn";
            this.YK_F_Sel_btn.Size = new System.Drawing.Size(118, 35);
            this.YK_F_Sel_btn.TabIndex = 2;
            this.YK_F_Sel_btn.Text = "遥控分(选择)";
            this.YK_F_Sel_btn.UseVisualStyleBackColor = false;
            this.YK_F_Sel_btn.Click += new System.EventHandler(this.YK_S_F_btn_Click);
            // 
            // YK_H_C_btn
            // 
            this.YK_H_C_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.YK_H_C_btn.Enabled = false;
            this.YK_H_C_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YK_H_C_btn.Location = new System.Drawing.Point(176, 178);
            this.YK_H_C_btn.Name = "YK_H_C_btn";
            this.YK_H_C_btn.Size = new System.Drawing.Size(118, 35);
            this.YK_H_C_btn.TabIndex = 7;
            this.YK_H_C_btn.Text = "遥控合(撤销)";
            this.YK_H_C_btn.UseVisualStyleBackColor = false;
            this.YK_H_C_btn.Click += new System.EventHandler(this.YK_H_C_btn_Click);
            // 
            // YK_F_Ex_btn
            // 
            this.YK_F_Ex_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.YK_F_Ex_btn.Enabled = false;
            this.YK_F_Ex_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YK_F_Ex_btn.Location = new System.Drawing.Point(26, 127);
            this.YK_F_Ex_btn.Name = "YK_F_Ex_btn";
            this.YK_F_Ex_btn.Size = new System.Drawing.Size(118, 35);
            this.YK_F_Ex_btn.TabIndex = 3;
            this.YK_F_Ex_btn.Text = "遥控分(执行)";
            this.YK_F_Ex_btn.UseVisualStyleBackColor = false;
            this.YK_F_Ex_btn.Click += new System.EventHandler(this.YK_F_btn_Click);
            // 
            // YK_F_C_btn
            // 
            this.YK_F_C_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.YK_F_C_btn.Enabled = false;
            this.YK_F_C_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YK_F_C_btn.Location = new System.Drawing.Point(26, 178);
            this.YK_F_C_btn.Name = "YK_F_C_btn";
            this.YK_F_C_btn.Size = new System.Drawing.Size(118, 35);
            this.YK_F_C_btn.TabIndex = 4;
            this.YK_F_C_btn.Text = "遥控分(撤销)";
            this.YK_F_C_btn.UseVisualStyleBackColor = false;
            this.YK_F_C_btn.Click += new System.EventHandler(this.YK_C_F_btn_Click);
            // 
            // YK_H_Ex_btn
            // 
            this.YK_H_Ex_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.YK_H_Ex_btn.Enabled = false;
            this.YK_H_Ex_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YK_H_Ex_btn.Location = new System.Drawing.Point(176, 127);
            this.YK_H_Ex_btn.Name = "YK_H_Ex_btn";
            this.YK_H_Ex_btn.Size = new System.Drawing.Size(118, 35);
            this.YK_H_Ex_btn.TabIndex = 6;
            this.YK_H_Ex_btn.Text = "遥控合(执行)";
            this.YK_H_Ex_btn.UseVisualStyleBackColor = false;
            this.YK_H_Ex_btn.Click += new System.EventHandler(this.YK_H_btn_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(38, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "单点";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(150, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "双点";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // LV_YK
            // 
            this.LV_YK.BackColor = System.Drawing.Color.Azure;
            this.LV_YK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_YK.Location = new System.Drawing.Point(3, 3);
            this.LV_YK.Name = "LV_YK";
            this.LV_YK.Size = new System.Drawing.Size(832, 585);
            this.LV_YK.TabIndex = 0;
            this.LV_YK.UseCompatibleStateImageBehavior = false;
            this.LV_YK.SelectedIndexChanged += new System.EventHandler(this.LV_YK_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.13903F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.86097F));
            this.tableLayoutPanel1.Controls.Add(this.LV_YK, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1417, 591);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CYKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 591);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CYKForm";
            this.Text = "遥控";
            this.Load += new System.EventHandler(this.CYKForm_Load);
            this.panel1.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button YK_H_Sel_btn;
        private System.Windows.Forms.Button YK_F_Sel_btn;
        private System.Windows.Forms.Button YK_H_C_btn;
        private System.Windows.Forms.Button YK_F_Ex_btn;
        private System.Windows.Forms.Button YK_F_C_btn;
        private System.Windows.Forms.Button YK_H_Ex_btn;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ListView LV_YK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton UseErrRand;
        private System.Windows.Forms.RadioButton UseMingWen;
        private System.Windows.Forms.RadioButton UseNoRand;
        private System.Windows.Forms.RadioButton UseNoTime;
        private System.Windows.Forms.RadioButton UseNoSign;
        private System.Windows.Forms.RadioButton UseErrTime;
        private System.Windows.Forms.RadioButton UseErrSign;
        private System.Windows.Forms.RadioButton Use_Normal;
        private MyPanel myPanel1;
        private System.Windows.Forms.Timer timer1;

    }
}