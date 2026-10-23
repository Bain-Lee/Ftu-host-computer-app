namespace DtuTestManager
{
    partial class CParaSetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CParaSetForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ParaListTree = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddRoot = new System.Windows.Forms.ToolStripButton();
            this.DeleteRoot = new System.Windows.Forms.ToolStripButton();
            this.AddList = new System.Windows.Forms.ToolStripButton();
            this.DeleteList = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ParaDataListView = new DtuTestManager.MyListView(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.ResetAddNo = new System.Windows.Forms.Button();
            this.AddChileModule = new System.Windows.Forms.Button();
            this.FontNameList = new System.Windows.Forms.ComboBox();
            this.ChildMdList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FirstAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.BtnChangeName = new System.Windows.Forms.ToolStripButton();
            this.BtnAdd = new System.Windows.Forms.ToolStripButton();
            this.BtnInsert = new System.Windows.Forms.ToolStripButton();
            this.BtnDelete = new System.Windows.Forms.ToolStripButton();
            this.BtnClear = new System.Windows.Forms.ToolStripButton();
            this.BtnSave = new System.Windows.Forms.ToolStripButton();
            this.BtnReset = new System.Windows.Forms.ToolStripButton();
            this.BtnLoadNowPara = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnParaCheck = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ParaListTree);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 431);
            this.panel1.TabIndex = 0;
            // 
            // ParaListTree
            // 
            this.ParaListTree.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ParaListTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaListTree.HideSelection = false;
            this.ParaListTree.Location = new System.Drawing.Point(0, 40);
            this.ParaListTree.Name = "ParaListTree";
            this.ParaListTree.Size = new System.Drawing.Size(170, 391);
            this.ParaListTree.TabIndex = 1;
            this.ParaListTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ParaListTree_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddRoot,
            this.DeleteRoot,
            this.AddList,
            this.DeleteList});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(170, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddRoot
            // 
            this.AddRoot.Image = ((System.Drawing.Image)(resources.GetObject("AddRoot.Image")));
            this.AddRoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddRoot.Name = "AddRoot";
            this.AddRoot.Size = new System.Drawing.Size(36, 37);
            this.AddRoot.Text = "新建";
            this.AddRoot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AddRoot.ToolTipText = "新建参数根目录";
            this.AddRoot.Click += new System.EventHandler(this.AddRoot_Click);
            // 
            // DeleteRoot
            // 
            this.DeleteRoot.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRoot.Image")));
            this.DeleteRoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRoot.Name = "DeleteRoot";
            this.DeleteRoot.Size = new System.Drawing.Size(36, 37);
            this.DeleteRoot.Text = "清除";
            this.DeleteRoot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DeleteRoot.ToolTipText = "清除参数根目录";
            this.DeleteRoot.Click += new System.EventHandler(this.DeleteRoot_Click);
            // 
            // AddList
            // 
            this.AddList.Image = ((System.Drawing.Image)(resources.GetObject("AddList.Image")));
            this.AddList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddList.Name = "AddList";
            this.AddList.Size = new System.Drawing.Size(36, 37);
            this.AddList.Text = "添加";
            this.AddList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AddList.ToolTipText = "添加参数子目录";
            this.AddList.Click += new System.EventHandler(this.AddList_Click);
            // 
            // DeleteList
            // 
            this.DeleteList.Image = ((System.Drawing.Image)(resources.GetObject("DeleteList.Image")));
            this.DeleteList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteList.Name = "DeleteList";
            this.DeleteList.Size = new System.Drawing.Size(36, 37);
            this.DeleteList.Text = "删除";
            this.DeleteList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DeleteList.ToolTipText = "删除参数子目录";
            this.DeleteList.Click += new System.EventHandler(this.DeleteList_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ParaDataListView);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(170, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(940, 431);
            this.panel2.TabIndex = 3;
            // 
            // ParaDataListView
            // 
            this.ParaDataListView.BackColor = System.Drawing.Color.AliceBlue;
            this.ParaDataListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaDataListView.FullRowSelect = true;
            this.ParaDataListView.GridLines = true;
            this.ParaDataListView.Location = new System.Drawing.Point(0, 66);
            this.ParaDataListView.Name = "ParaDataListView";
            this.ParaDataListView.OwnerDraw = true;
            this.ParaDataListView.ProgressColumnIndex = -1;
            this.ParaDataListView.Size = new System.Drawing.Size(940, 365);
            this.ParaDataListView.TabIndex = 2;
            this.ParaDataListView.UseCompatibleStateImageBehavior = false;
            this.ParaDataListView.View = System.Windows.Forms.View.Details;
            this.ParaDataListView.SelectedIndexChanged += new System.EventHandler(this.ParaDataListView_SelectedIndexChanged);
            this.ParaDataListView.DoubleClick += new System.EventHandler(this.ParaDataListView_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ResetAddNo);
            this.panel3.Controls.Add(this.AddChileModule);
            this.panel3.Controls.Add(this.FontNameList);
            this.panel3.Controls.Add(this.ChildMdList);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.FirstAddress);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(940, 41);
            this.panel3.TabIndex = 1;
            // 
            // ResetAddNo
            // 
            this.ResetAddNo.Location = new System.Drawing.Point(773, 10);
            this.ResetAddNo.Name = "ResetAddNo";
            this.ResetAddNo.Size = new System.Drawing.Size(111, 23);
            this.ResetAddNo.TabIndex = 4;
            this.ResetAddNo.Text = "地址重新排序";
            this.ResetAddNo.UseVisualStyleBackColor = true;
            this.ResetAddNo.Click += new System.EventHandler(this.ResetAddNo_Click);
            // 
            // AddChileModule
            // 
            this.AddChileModule.Location = new System.Drawing.Point(656, 10);
            this.AddChileModule.Name = "AddChileModule";
            this.AddChileModule.Size = new System.Drawing.Size(111, 23);
            this.AddChileModule.TabIndex = 4;
            this.AddChileModule.Text = "添加子模板";
            this.AddChileModule.UseVisualStyleBackColor = true;
            this.AddChileModule.Click += new System.EventHandler(this.AddChileModule_Click);
            // 
            // FontNameList
            // 
            this.FontNameList.FormattingEnabled = true;
            this.FontNameList.Location = new System.Drawing.Point(514, 12);
            this.FontNameList.Name = "FontNameList";
            this.FontNameList.Size = new System.Drawing.Size(135, 20);
            this.FontNameList.TabIndex = 3;
            // 
            // ChildMdList
            // 
            this.ChildMdList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChildMdList.FormattingEnabled = true;
            this.ChildMdList.Location = new System.Drawing.Point(266, 13);
            this.ChildMdList.Name = "ChildMdList";
            this.ChildMdList.Size = new System.Drawing.Size(135, 20);
            this.ChildMdList.TabIndex = 3;
            this.ChildMdList.SelectedIndexChanged += new System.EventHandler(this.ChildMdList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "子模板前缀名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "可用子模板：";
            // 
            // FirstAddress
            // 
            this.FirstAddress.Location = new System.Drawing.Point(75, 13);
            this.FirstAddress.Name = "FirstAddress";
            this.FirstAddress.Size = new System.Drawing.Size(100, 21);
            this.FirstAddress.TabIndex = 1;
            this.FirstAddress.TextChanged += new System.EventHandler(this.FirstAddress_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "首地址：";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.LightBlue;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnChangeName,
            this.BtnAdd,
            this.BtnInsert,
            this.BtnDelete,
            this.BtnClear,
            this.BtnSave,
            this.BtnReset,
            this.BtnLoadNowPara,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.BtnParaCheck});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(940, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // BtnChangeName
            // 
            this.BtnChangeName.Image = ((System.Drawing.Image)(resources.GetObject("BtnChangeName.Image")));
            this.BtnChangeName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnChangeName.Name = "BtnChangeName";
            this.BtnChangeName.Size = new System.Drawing.Size(100, 22);
            this.BtnChangeName.Text = "修改目录名称";
            this.BtnChangeName.Click += new System.EventHandler(this.BtnChangeName_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("BtnAdd.Image")));
            this.BtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(52, 22);
            this.BtnAdd.Text = "添加";
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnInsert
            // 
            this.BtnInsert.Image = ((System.Drawing.Image)(resources.GetObject("BtnInsert.Image")));
            this.BtnInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnInsert.Name = "BtnInsert";
            this.BtnInsert.Size = new System.Drawing.Size(52, 22);
            this.BtnInsert.Text = "插入";
            this.BtnInsert.Click += new System.EventHandler(this.BtnInsert_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(52, 22);
            this.BtnDelete.Text = "删除";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Image = ((System.Drawing.Image)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(52, 22);
            this.BtnClear.Text = "清空";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(52, 22);
            this.BtnSave.Text = "保存";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Image = ((System.Drawing.Image)(resources.GetObject("BtnReset.Image")));
            this.BtnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(52, 22);
            this.BtnReset.Text = "还原";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnLoadNowPara
            // 
            this.BtnLoadNowPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnLoadNowPara.Image")));
            this.BtnLoadNowPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnLoadNowPara.Name = "BtnLoadNowPara";
            this.BtnLoadNowPara.Size = new System.Drawing.Size(100, 22);
            this.BtnLoadNowPara.Text = "加载已有参数";
            this.BtnLoadNowPara.Click += new System.EventHandler(this.BtnLoadNowPara_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnParaCheck
            // 
            this.BtnParaCheck.Image = ((System.Drawing.Image)(resources.GetObject("BtnParaCheck.Image")));
            this.BtnParaCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnParaCheck.Name = "BtnParaCheck";
            this.BtnParaCheck.Size = new System.Drawing.Size(76, 22);
            this.BtnParaCheck.Text = "参数检查";
            this.BtnParaCheck.Click += new System.EventHandler(this.BtnParaCheck_Click);
            // 
            // CParaSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1110, 431);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "CParaSetForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "CParaSetForm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CParaSetForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView ParaListTree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddList;
        private System.Windows.Forms.ToolStripButton DeleteList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton BtnAdd;
        private System.Windows.Forms.ToolStripButton BtnInsert;
        private System.Windows.Forms.ToolStripButton BtnDelete;
        private System.Windows.Forms.ToolStripButton BtnClear;
        private System.Windows.Forms.ToolStripButton BtnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private MyListView ParaDataListView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox FirstAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ChildMdList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FontNameList;
        private System.Windows.Forms.Button ResetAddNo;
        private System.Windows.Forms.Button AddChileModule;
        private System.Windows.Forms.ToolStripButton AddRoot;
        private System.Windows.Forms.ToolStripButton DeleteRoot;
        private System.Windows.Forms.ToolStripButton BtnReset;
        private System.Windows.Forms.ToolStripButton BtnLoadNowPara;
        private System.Windows.Forms.ToolStripButton BtnChangeName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton BtnParaCheck;
    }
}