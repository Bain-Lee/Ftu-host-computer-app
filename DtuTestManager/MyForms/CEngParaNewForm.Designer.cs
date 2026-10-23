namespace DtuTestManager
{
    partial class CEngParaNewForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ParaTreeView = new System.Windows.Forms.TreeView();
            this.LV_Para = new DtuTestManager.MyListView(this.components);
            this.ParaLV = new DtuTestManager.MyListView(this.components);
            this.myPanel1 = new DtuTestManager.MyPanel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.51852F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.03704F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel1.Controls.Add(this.LV_Para, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ParaTreeView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ParaLV, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.myPanel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1081, 544);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // ParaTreeView
            // 
            this.ParaTreeView.BackColor = System.Drawing.Color.Azure;
            this.ParaTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaTreeView.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaTreeView.Location = new System.Drawing.Point(3, 3);
            this.ParaTreeView.Name = "ParaTreeView";
            this.ParaTreeView.Size = new System.Drawing.Size(172, 538);
            this.ParaTreeView.TabIndex = 4;
            this.ParaTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ParaTreeView_AfterSelect);
            // 
            // LV_Para
            // 
            this.LV_Para.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.LV_Para.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_Para.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LV_Para.FullRowSelect = true;
            this.LV_Para.GridLines = true;
            this.LV_Para.Location = new System.Drawing.Point(181, 3);
            this.LV_Para.Name = "LV_Para";
            this.LV_Para.OwnerDraw = true;
            this.LV_Para.ProgressColumnIndex = -1;
            this.LV_Para.Size = new System.Drawing.Size(351, 538);
            this.LV_Para.TabIndex = 5;
            this.LV_Para.UseCompatibleStateImageBehavior = false;
            this.LV_Para.View = System.Windows.Forms.View.Details;
            // 
            // ParaLV
            // 
            this.ParaLV.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ParaLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaLV.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaLV.FullRowSelect = true;
            this.ParaLV.GridLines = true;
            this.ParaLV.Location = new System.Drawing.Point(655, 3);
            this.ParaLV.Name = "ParaLV";
            this.ParaLV.OwnerDraw = true;
            this.ParaLV.ProgressColumnIndex = -1;
            this.ParaLV.Size = new System.Drawing.Size(423, 538);
            this.ParaLV.TabIndex = 2;
            this.ParaLV.UseCompatibleStateImageBehavior = false;
            this.ParaLV.View = System.Windows.Forms.View.Details;
            this.ParaLV.SelectedIndexChanged += new System.EventHandler(this.ParaLV_SelectedIndexChanged);
            this.ParaLV.Validated += new System.EventHandler(this.ParaLV_Validated);
            // 
            // myPanel1
            // 
            this.myPanel1.Controls.Add(this.button5);
            this.myPanel1.Controls.Add(this.button4);
            this.myPanel1.Controls.Add(this.button3);
            this.myPanel1.Controls.Add(this.button2);
            this.myPanel1.Controls.Add(this.button6);
            this.myPanel1.Controls.Add(this.button1);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(538, 3);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(111, 538);
            this.myPanel1.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(3, 334);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(108, 33);
            this.button5.TabIndex = 0;
            this.button5.Text = "取消";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(3, 271);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(105, 33);
            this.button4.TabIndex = 0;
            this.button4.Text = "保存";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(3, 208);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 33);
            this.button3.TabIndex = 0;
            this.button3.Text = "移除<-";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(3, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 33);
            this.button2.TabIndex = 0;
            this.button2.Text = "插入->";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Location = new System.Drawing.Point(3, 19);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 33);
            this.button6.TabIndex = 0;
            this.button6.Text = "全部添加->";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(3, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "添加->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CEngParaNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1081, 544);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CEngParaNewForm";
            this.Text = "参数配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CEngParaNewForm_FormClosing);
            this.Load += new System.EventHandler(this.CEngParaNewForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyListView ParaLV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MyPanel myPanel1;
        private System.Windows.Forms.TreeView ParaTreeView;
        private MyListView LV_Para;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}