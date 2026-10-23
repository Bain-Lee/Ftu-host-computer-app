namespace DtuTestManager
{
    partial class CGetSelectParaForm
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
            this.ParaGroupTree = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSetOK = new System.Windows.Forms.Button();
            this.ParaListView = new System.Windows.Forms.ListView();
            this.HaveSelectView = new System.Windows.Forms.ListView();
            this.BtnAddSelect = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnInsert = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParaGroupTree
            // 
            this.ParaGroupTree.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ParaGroupTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParaGroupTree.HideSelection = false;
            this.ParaGroupTree.Location = new System.Drawing.Point(0, 0);
            this.ParaGroupTree.Name = "ParaGroupTree";
            this.ParaGroupTree.Size = new System.Drawing.Size(189, 433);
            this.ParaGroupTree.TabIndex = 0;
            this.ParaGroupTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ParaGroupTree_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.BtnSetOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(189, 377);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 56);
            this.panel1.TabIndex = 1;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(485, 21);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSetOK
            // 
            this.BtnSetOK.Location = new System.Drawing.Point(266, 19);
            this.BtnSetOK.Name = "BtnSetOK";
            this.BtnSetOK.Size = new System.Drawing.Size(75, 23);
            this.BtnSetOK.TabIndex = 0;
            this.BtnSetOK.Text = "确认";
            this.BtnSetOK.UseVisualStyleBackColor = true;
            this.BtnSetOK.Click += new System.EventHandler(this.BtnSetOK_Click);
            // 
            // ParaListView
            // 
            this.ParaListView.BackColor = System.Drawing.Color.AliceBlue;
            this.ParaListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParaListView.Location = new System.Drawing.Point(189, 0);
            this.ParaListView.Name = "ParaListView";
            this.ParaListView.Size = new System.Drawing.Size(400, 377);
            this.ParaListView.TabIndex = 2;
            this.ParaListView.UseCompatibleStateImageBehavior = false;
            this.ParaListView.SelectedIndexChanged += new System.EventHandler(this.ParaListView_SelectedIndexChanged);
            this.ParaListView.Validated += new System.EventHandler(this.ParaListView_Validated);
            // 
            // HaveSelectView
            // 
            this.HaveSelectView.BackColor = System.Drawing.Color.AliceBlue;
            this.HaveSelectView.Dock = System.Windows.Forms.DockStyle.Right;
            this.HaveSelectView.Location = new System.Drawing.Point(689, 0);
            this.HaveSelectView.Name = "HaveSelectView";
            this.HaveSelectView.Size = new System.Drawing.Size(401, 377);
            this.HaveSelectView.TabIndex = 3;
            this.HaveSelectView.UseCompatibleStateImageBehavior = false;
            this.HaveSelectView.SelectedIndexChanged += new System.EventHandler(this.HaveSelectView_SelectedIndexChanged);
            this.HaveSelectView.Validated += new System.EventHandler(this.HaveSelectView_Validated);
            // 
            // BtnAddSelect
            // 
            this.BtnAddSelect.Location = new System.Drawing.Point(601, 41);
            this.BtnAddSelect.Name = "BtnAddSelect";
            this.BtnAddSelect.Size = new System.Drawing.Size(75, 23);
            this.BtnAddSelect.TabIndex = 4;
            this.BtnAddSelect.Text = "添加 —>";
            this.BtnAddSelect.UseVisualStyleBackColor = true;
            this.BtnAddSelect.Click += new System.EventHandler(this.BtnAddSelect_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(601, 159);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 4;
            this.BtnDelete.Text = "<— 移除";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnInsert
            // 
            this.BtnInsert.Location = new System.Drawing.Point(601, 100);
            this.BtnInsert.Name = "BtnInsert";
            this.BtnInsert.Size = new System.Drawing.Size(75, 23);
            this.BtnInsert.TabIndex = 4;
            this.BtnInsert.Text = "插入 —>";
            this.BtnInsert.UseVisualStyleBackColor = true;
            this.BtnInsert.Click += new System.EventHandler(this.BtnInsert_Click);
            // 
            // CGetSelectParaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1090, 433);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnInsert);
            this.Controls.Add(this.BtnAddSelect);
            this.Controls.Add(this.HaveSelectView);
            this.Controls.Add(this.ParaListView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ParaGroupTree);
            this.Name = "CGetSelectParaForm";
            this.Text = "参数选择";
            this.Load += new System.EventHandler(this.CGetSelectParaForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ParaGroupTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSetOK;
        private System.Windows.Forms.ListView ParaListView;
        private System.Windows.Forms.ListView HaveSelectView;
        private System.Windows.Forms.Button BtnAddSelect;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnInsert;
    }
}