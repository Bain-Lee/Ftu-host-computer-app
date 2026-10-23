namespace DtuTestManager
{
    partial class CRouteFormSetting
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
            this.Total_Listview = new System.Windows.Forms.ListView();
            this.Pri_ListView = new System.Windows.Forms.ListView();
            this.BtnAddSelect = new System.Windows.Forms.Button();
            this.BtnInsert = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Dev_Combox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Total_Listview
            // 
            this.Total_Listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Total_Listview.Location = new System.Drawing.Point(3, 3);
            this.Total_Listview.Name = "Total_Listview";
            this.Total_Listview.Size = new System.Drawing.Size(587, 481);
            this.Total_Listview.TabIndex = 0;
            this.Total_Listview.UseCompatibleStateImageBehavior = false;
            this.Total_Listview.SelectedIndexChanged += new System.EventHandler(this.Total_Listview_SelectedIndexChanged);
            this.Total_Listview.Validated += new System.EventHandler(this.Total_Listview_Validated);
            // 
            // Pri_ListView
            // 
            this.Pri_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pri_ListView.Location = new System.Drawing.Point(726, 3);
            this.Pri_ListView.Name = "Pri_ListView";
            this.Pri_ListView.Size = new System.Drawing.Size(587, 481);
            this.Pri_ListView.TabIndex = 1;
            this.Pri_ListView.UseCompatibleStateImageBehavior = false;
            this.Pri_ListView.SelectedIndexChanged += new System.EventHandler(this.Pri_ListView_SelectedIndexChanged);
            this.Pri_ListView.Validated += new System.EventHandler(this.Pri_ListView_Validated);
            // 
            // BtnAddSelect
            // 
            this.BtnAddSelect.Location = new System.Drawing.Point(22, 124);
            this.BtnAddSelect.Name = "BtnAddSelect";
            this.BtnAddSelect.Size = new System.Drawing.Size(75, 23);
            this.BtnAddSelect.TabIndex = 2;
            this.BtnAddSelect.Text = "添加";
            this.BtnAddSelect.UseVisualStyleBackColor = true;
            this.BtnAddSelect.Click += new System.EventHandler(this.BtnAddSelect_Click);
            // 
            // BtnInsert
            // 
            this.BtnInsert.Location = new System.Drawing.Point(22, 163);
            this.BtnInsert.Name = "BtnInsert";
            this.BtnInsert.Size = new System.Drawing.Size(75, 23);
            this.BtnInsert.TabIndex = 2;
            this.BtnInsert.Text = "插入";
            this.BtnInsert.UseVisualStyleBackColor = true;
            this.BtnInsert.Click += new System.EventHandler(this.BtnInsert_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(22, 202);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "移除";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(22, 241);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "保存";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Total_Listview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Pri_ListView, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1316, 487);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Dev_Combox);
            this.panel1.Controls.Add(this.BtnAddSelect);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.BtnInsert);
            this.panel1.Controls.Add(this.BtnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(596, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 481);
            this.panel1.TabIndex = 2;
            // 
            // Dev_Combox
            // 
            this.Dev_Combox.FormattingEnabled = true;
            this.Dev_Combox.Location = new System.Drawing.Point(22, 86);
            this.Dev_Combox.Name = "Dev_Combox";
            this.Dev_Combox.Size = new System.Drawing.Size(75, 20);
            this.Dev_Combox.TabIndex = 3;
            this.Dev_Combox.SelectedIndexChanged += new System.EventHandler(this.Dev_Combox_SelectedIndexChanged);
            // 
            // CRouteFormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1316, 487);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CRouteFormSetting";
            this.Text = "CRouteFormSetting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CRouteFormSetting_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView Total_Listview;
        private System.Windows.Forms.ListView Pri_ListView;
        private System.Windows.Forms.Button BtnAddSelect;
        private System.Windows.Forms.Button BtnInsert;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox Dev_Combox;
    }
}