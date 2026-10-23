namespace DtuTestManager
{
    partial class CModulForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CModulForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.DevModuleComList = new System.Windows.Forms.ToolStripComboBox();
            this.SaveModuleSelect = new System.Windows.Forms.ToolStripButton();
            this.ChildRightMemu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChileAddModule = new System.Windows.Forms.ToolStripMenuItem();
            this.ChileDelModule = new System.Windows.Forms.ToolStripMenuItem();
            this.ModuleTabCtrl = new DtuTestManager.CNormalTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SetViewPanel = new System.Windows.Forms.Panel();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.DevDataView = new DtuTestManager.MyListView(this.components);
            this.GroupSetPanel = new System.Windows.Forms.Panel();
            this.ResetAddress = new System.Windows.Forms.Button();
            this.AddChildModule = new System.Windows.Forms.Button();
            this.FontName = new System.Windows.Forms.ComboBox();
            this.ChildModuleList = new System.Windows.Forms.ComboBox();
            this.FirstAddress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.DevAddBtn = new System.Windows.Forms.ToolStripButton();
            this.DevInsertBtn = new System.Windows.Forms.ToolStripButton();
            this.DevDeleteBtn = new System.Windows.Forms.ToolStripButton();
            this.DevClearBtn = new System.Windows.Forms.ToolStripButton();
            this.DevSaveBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DeviceTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.AddDeivceBtn = new System.Windows.Forms.ToolStripButton();
            this.CopyDevBtn = new System.Windows.Forms.ToolStripButton();
            this.DelDeviceBtn = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ModuleView = new DtuTestManager.MyListView(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.ChildAdd = new System.Windows.Forms.ToolStripButton();
            this.ChildInsert = new System.Windows.Forms.ToolStripButton();
            this.ChildDelete = new System.Windows.Forms.ToolStripButton();
            this.ClearClear = new System.Windows.Forms.ToolStripButton();
            this.ChildSave = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LineListView = new System.Windows.Forms.TreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.AddModule = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.ChildRightMemu.SuspendLayout();
            this.ModuleTabCtrl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SetViewPanel.SuspendLayout();
            this.GroupSetPanel.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.DevModuleComList,
            this.SaveModuleSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1048, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "选择模板：";
            // 
            // DevModuleComList
            // 
            this.DevModuleComList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DevModuleComList.Name = "DevModuleComList";
            this.DevModuleComList.Size = new System.Drawing.Size(121, 25);
            // 
            // SaveModuleSelect
            // 
            this.SaveModuleSelect.Image = ((System.Drawing.Image)(resources.GetObject("SaveModuleSelect.Image")));
            this.SaveModuleSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveModuleSelect.Name = "SaveModuleSelect";
            this.SaveModuleSelect.Size = new System.Drawing.Size(76, 22);
            this.SaveModuleSelect.Text = "保存选择";
            this.SaveModuleSelect.Click += new System.EventHandler(this.SaveModuleSelect_Click);
            // 
            // ChildRightMemu
            // 
            this.ChildRightMemu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChileAddModule,
            this.ChileDelModule});
            this.ChildRightMemu.Name = "ChildRightMemu";
            this.ChildRightMemu.Size = new System.Drawing.Size(125, 48);
            // 
            // ChileAddModule
            // 
            this.ChileAddModule.Name = "ChileAddModule";
            this.ChileAddModule.Size = new System.Drawing.Size(124, 22);
            this.ChileAddModule.Text = "添加模板";
            this.ChileAddModule.Click += new System.EventHandler(this.ChileAddModule_Click);
            // 
            // ChileDelModule
            // 
            this.ChileDelModule.Name = "ChileDelModule";
            this.ChileDelModule.Size = new System.Drawing.Size(124, 22);
            this.ChileDelModule.Text = "删除模板";
            this.ChileDelModule.Click += new System.EventHandler(this.ChileDelModule_Click);
            // 
            // ModuleTabCtrl
            // 
            this.ModuleTabCtrl.Controls.Add(this.tabPage1);
            this.ModuleTabCtrl.Controls.Add(this.tabPage2);
            this.ModuleTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleTabCtrl.Location = new System.Drawing.Point(0, 25);
            this.ModuleTabCtrl.Name = "ModuleTabCtrl";
            this.ModuleTabCtrl.SelectedIndex = 0;
            this.ModuleTabCtrl.Size = new System.Drawing.Size(1048, 393);
            this.ModuleTabCtrl.TabIndex = 2;
            this.ModuleTabCtrl.SelectedIndexChanged += new System.EventHandler(this.ModuleTabCtrl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Azure;
            this.tabPage1.Controls.Add(this.SetViewPanel);
            this.tabPage1.Controls.Add(this.toolStrip5);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1040, 367);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "装置模板";
            // 
            // SetViewPanel
            // 
            this.SetViewPanel.Controls.Add(this.FormPanel);
            this.SetViewPanel.Controls.Add(this.DevDataView);
            this.SetViewPanel.Controls.Add(this.GroupSetPanel);
            this.SetViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetViewPanel.Location = new System.Drawing.Point(155, 28);
            this.SetViewPanel.Name = "SetViewPanel";
            this.SetViewPanel.Size = new System.Drawing.Size(882, 336);
            this.SetViewPanel.TabIndex = 2;
            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = System.Drawing.Color.Transparent;
            this.FormPanel.Location = new System.Drawing.Point(11, 101);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(200, 140);
            this.FormPanel.TabIndex = 2;
            // 
            // DevDataView
            // 
            this.DevDataView.BackColor = System.Drawing.Color.Azure;
            this.DevDataView.FullRowSelect = true;
            this.DevDataView.GridLines = true;
            this.DevDataView.Location = new System.Drawing.Point(230, 101);
            this.DevDataView.Name = "DevDataView";
            this.DevDataView.OwnerDraw = true;
            this.DevDataView.ProgressColumnIndex = -1;
            this.DevDataView.Size = new System.Drawing.Size(278, 140);
            this.DevDataView.TabIndex = 1;
            this.DevDataView.UseCompatibleStateImageBehavior = false;
            this.DevDataView.View = System.Windows.Forms.View.Details;
            this.DevDataView.SelectedIndexChanged += new System.EventHandler(this.DevDataView_SelectedIndexChanged);
            this.DevDataView.DoubleClick += new System.EventHandler(this.DevDataView_DoubleClick);
            // 
            // GroupSetPanel
            // 
            this.GroupSetPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GroupSetPanel.Controls.Add(this.ResetAddress);
            this.GroupSetPanel.Controls.Add(this.AddChildModule);
            this.GroupSetPanel.Controls.Add(this.FontName);
            this.GroupSetPanel.Controls.Add(this.ChildModuleList);
            this.GroupSetPanel.Controls.Add(this.FirstAddress);
            this.GroupSetPanel.Controls.Add(this.label2);
            this.GroupSetPanel.Controls.Add(this.label1);
            this.GroupSetPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupSetPanel.Location = new System.Drawing.Point(0, 0);
            this.GroupSetPanel.Name = "GroupSetPanel";
            this.GroupSetPanel.Size = new System.Drawing.Size(882, 33);
            this.GroupSetPanel.TabIndex = 0;
            // 
            // ResetAddress
            // 
            this.ResetAddress.Location = new System.Drawing.Point(707, 4);
            this.ResetAddress.Name = "ResetAddress";
            this.ResetAddress.Size = new System.Drawing.Size(117, 26);
            this.ResetAddress.TabIndex = 2;
            this.ResetAddress.Text = "地址重新排序";
            this.ResetAddress.UseVisualStyleBackColor = true;
            this.ResetAddress.Click += new System.EventHandler(this.ResetAddress_Click);
            // 
            // AddChildModule
            // 
            this.AddChildModule.Location = new System.Drawing.Point(584, 3);
            this.AddChildModule.Name = "AddChildModule";
            this.AddChildModule.Size = new System.Drawing.Size(117, 26);
            this.AddChildModule.TabIndex = 2;
            this.AddChildModule.Text = "添加子模板";
            this.AddChildModule.UseVisualStyleBackColor = true;
            this.AddChildModule.Click += new System.EventHandler(this.AddChildModule_Click);
            // 
            // FontName
            // 
            this.FontName.FormattingEnabled = true;
            this.FontName.Location = new System.Drawing.Point(467, 7);
            this.FontName.Name = "FontName";
            this.FontName.Size = new System.Drawing.Size(111, 20);
            this.FontName.TabIndex = 1;
            // 
            // ChildModuleList
            // 
            this.ChildModuleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChildModuleList.FormattingEnabled = true;
            this.ChildModuleList.Location = new System.Drawing.Point(237, 7);
            this.ChildModuleList.Name = "ChildModuleList";
            this.ChildModuleList.Size = new System.Drawing.Size(117, 20);
            this.ChildModuleList.TabIndex = 1;
            this.ChildModuleList.SelectedIndexChanged += new System.EventHandler(this.ChildModuleList_SelectedIndexChanged);
            // 
            // FirstAddress
            // 
            this.FirstAddress.AutoSize = true;
            this.FirstAddress.ForeColor = System.Drawing.Color.Black;
            this.FirstAddress.Location = new System.Drawing.Point(9, 12);
            this.FirstAddress.Name = "FirstAddress";
            this.FirstAddress.Size = new System.Drawing.Size(53, 12);
            this.FirstAddress.TabIndex = 0;
            this.FirstAddress.Text = "首地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "子模板前缀名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可用子模板：";
            // 
            // toolStrip5
            // 
            this.toolStrip5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DevAddBtn,
            this.DevInsertBtn,
            this.DevDeleteBtn,
            this.DevClearBtn,
            this.DevSaveBtn});
            this.toolStrip5.Location = new System.Drawing.Point(155, 3);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(882, 25);
            this.toolStrip5.TabIndex = 1;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // DevAddBtn
            // 
            this.DevAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("DevAddBtn.Image")));
            this.DevAddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DevAddBtn.Name = "DevAddBtn";
            this.DevAddBtn.Size = new System.Drawing.Size(52, 22);
            this.DevAddBtn.Text = "添加";
            this.DevAddBtn.Click += new System.EventHandler(this.DevAddBtn_Click);
            // 
            // DevInsertBtn
            // 
            this.DevInsertBtn.Image = ((System.Drawing.Image)(resources.GetObject("DevInsertBtn.Image")));
            this.DevInsertBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DevInsertBtn.Name = "DevInsertBtn";
            this.DevInsertBtn.Size = new System.Drawing.Size(52, 22);
            this.DevInsertBtn.Text = "插入";
            this.DevInsertBtn.Click += new System.EventHandler(this.DevInsertBtn_Click);
            // 
            // DevDeleteBtn
            // 
            this.DevDeleteBtn.Image = ((System.Drawing.Image)(resources.GetObject("DevDeleteBtn.Image")));
            this.DevDeleteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DevDeleteBtn.Name = "DevDeleteBtn";
            this.DevDeleteBtn.Size = new System.Drawing.Size(52, 22);
            this.DevDeleteBtn.Text = "删除";
            this.DevDeleteBtn.Click += new System.EventHandler(this.DevDeleteBtn_Click);
            // 
            // DevClearBtn
            // 
            this.DevClearBtn.Image = ((System.Drawing.Image)(resources.GetObject("DevClearBtn.Image")));
            this.DevClearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DevClearBtn.Name = "DevClearBtn";
            this.DevClearBtn.Size = new System.Drawing.Size(52, 22);
            this.DevClearBtn.Text = "清空";
            this.DevClearBtn.Click += new System.EventHandler(this.DevClearBtn_Click);
            // 
            // DevSaveBtn
            // 
            this.DevSaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("DevSaveBtn.Image")));
            this.DevSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DevSaveBtn.Name = "DevSaveBtn";
            this.DevSaveBtn.Size = new System.Drawing.Size(52, 22);
            this.DevSaveBtn.Text = "保存";
            this.DevSaveBtn.Click += new System.EventHandler(this.DevSaveBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.DeviceTreeView);
            this.panel2.Controls.Add(this.toolStrip4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 361);
            this.panel2.TabIndex = 0;
            // 
            // DeviceTreeView
            // 
            this.DeviceTreeView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.DeviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceTreeView.HideSelection = false;
            this.DeviceTreeView.Location = new System.Drawing.Point(0, 40);
            this.DeviceTreeView.Name = "DeviceTreeView";
            this.DeviceTreeView.Size = new System.Drawing.Size(152, 321);
            this.DeviceTreeView.TabIndex = 2;
            this.DeviceTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DeviceTreeView_AfterSelect);
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddDeivceBtn,
            this.CopyDevBtn,
            this.DelDeviceBtn});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(152, 40);
            this.toolStrip4.TabIndex = 1;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // AddDeivceBtn
            // 
            this.AddDeivceBtn.Image = ((System.Drawing.Image)(resources.GetObject("AddDeivceBtn.Image")));
            this.AddDeivceBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddDeivceBtn.Name = "AddDeivceBtn";
            this.AddDeivceBtn.Size = new System.Drawing.Size(36, 37);
            this.AddDeivceBtn.Text = "新建";
            this.AddDeivceBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AddDeivceBtn.ToolTipText = "新建装置";
            this.AddDeivceBtn.Click += new System.EventHandler(this.AddDeivceBtn_Click);
            // 
            // CopyDevBtn
            // 
            this.CopyDevBtn.Image = ((System.Drawing.Image)(resources.GetObject("CopyDevBtn.Image")));
            this.CopyDevBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyDevBtn.Name = "CopyDevBtn";
            this.CopyDevBtn.Size = new System.Drawing.Size(36, 37);
            this.CopyDevBtn.Text = "复制";
            this.CopyDevBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CopyDevBtn.ToolTipText = "复制选择装置并新建";
            this.CopyDevBtn.Click += new System.EventHandler(this.CopyDevBtn_Click);
            // 
            // DelDeviceBtn
            // 
            this.DelDeviceBtn.Image = ((System.Drawing.Image)(resources.GetObject("DelDeviceBtn.Image")));
            this.DelDeviceBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DelDeviceBtn.Name = "DelDeviceBtn";
            this.DelDeviceBtn.Size = new System.Drawing.Size(36, 37);
            this.DelDeviceBtn.Text = "删除";
            this.DelDeviceBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DelDeviceBtn.ToolTipText = "删除当前选择的装置";
            this.DelDeviceBtn.Click += new System.EventHandler(this.DelDeviceBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Azure;
            this.tabPage2.Controls.Add(this.ModuleView);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1040, 367);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置模板";
            // 
            // ModuleView
            // 
            this.ModuleView.BackColor = System.Drawing.Color.AliceBlue;
            this.ModuleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModuleView.FullRowSelect = true;
            this.ModuleView.GridLines = true;
            this.ModuleView.Location = new System.Drawing.Point(162, 28);
            this.ModuleView.Name = "ModuleView";
            this.ModuleView.OwnerDraw = true;
            this.ModuleView.ProgressColumnIndex = -1;
            this.ModuleView.Size = new System.Drawing.Size(875, 336);
            this.ModuleView.TabIndex = 2;
            this.ModuleView.UseCompatibleStateImageBehavior = false;
            this.ModuleView.View = System.Windows.Forms.View.Details;
            this.ModuleView.SelectedIndexChanged += new System.EventHandler(this.ModuleView_SelectedIndexChanged);
            this.ModuleView.DoubleClick += new System.EventHandler(this.ModuleView_DoubleClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChildAdd,
            this.ChildInsert,
            this.ChildDelete,
            this.ClearClear,
            this.ChildSave});
            this.toolStrip2.Location = new System.Drawing.Point(162, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(875, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // ChildAdd
            // 
            this.ChildAdd.Image = ((System.Drawing.Image)(resources.GetObject("ChildAdd.Image")));
            this.ChildAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChildAdd.Name = "ChildAdd";
            this.ChildAdd.Size = new System.Drawing.Size(52, 22);
            this.ChildAdd.Text = "添加";
            this.ChildAdd.Click += new System.EventHandler(this.ChildAdd_Click);
            // 
            // ChildInsert
            // 
            this.ChildInsert.Image = ((System.Drawing.Image)(resources.GetObject("ChildInsert.Image")));
            this.ChildInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChildInsert.Name = "ChildInsert";
            this.ChildInsert.Size = new System.Drawing.Size(52, 22);
            this.ChildInsert.Text = "插入";
            this.ChildInsert.Click += new System.EventHandler(this.ChildInsert_Click);
            // 
            // ChildDelete
            // 
            this.ChildDelete.Image = ((System.Drawing.Image)(resources.GetObject("ChildDelete.Image")));
            this.ChildDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChildDelete.Name = "ChildDelete";
            this.ChildDelete.Size = new System.Drawing.Size(52, 22);
            this.ChildDelete.Text = "删除";
            this.ChildDelete.Click += new System.EventHandler(this.ChildDelete_Click);
            // 
            // ClearClear
            // 
            this.ClearClear.Image = ((System.Drawing.Image)(resources.GetObject("ClearClear.Image")));
            this.ClearClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearClear.Name = "ClearClear";
            this.ClearClear.Size = new System.Drawing.Size(52, 22);
            this.ClearClear.Text = "清空";
            this.ClearClear.Click += new System.EventHandler(this.ClearClear_Click);
            // 
            // ChildSave
            // 
            this.ChildSave.Image = ((System.Drawing.Image)(resources.GetObject("ChildSave.Image")));
            this.ChildSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChildSave.Name = "ChildSave";
            this.ChildSave.Size = new System.Drawing.Size(52, 22);
            this.ChildSave.Text = "保存";
            this.ChildSave.Click += new System.EventHandler(this.ChildSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LineListView);
            this.panel1.Controls.Add(this.toolStrip3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 361);
            this.panel1.TabIndex = 0;
            // 
            // LineListView
            // 
            this.LineListView.BackColor = System.Drawing.Color.LightSteelBlue;
            this.LineListView.ContextMenuStrip = this.ChildRightMemu;
            this.LineListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineListView.HideSelection = false;
            this.LineListView.Location = new System.Drawing.Point(0, 40);
            this.LineListView.Name = "LineListView";
            this.LineListView.Size = new System.Drawing.Size(159, 321);
            this.LineListView.TabIndex = 2;
            this.LineListView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LineListView_AfterSelect);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.AddModule});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(159, 40);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 37);
            this.toolStripButton1.Text = "添加模板";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.ChileAddModule_Click);
            // 
            // AddModule
            // 
            this.AddModule.Image = ((System.Drawing.Image)(resources.GetObject("AddModule.Image")));
            this.AddModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddModule.Name = "AddModule";
            this.AddModule.Size = new System.Drawing.Size(60, 37);
            this.AddModule.Text = "删除模板";
            this.AddModule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AddModule.Click += new System.EventHandler(this.ChileDelModule_Click);
            // 
            // CModulForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1048, 418);
            this.Controls.Add(this.ModuleTabCtrl);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "CModulForm";
            this.Text = "模板配置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CModulForm_FormClosed);
            this.Load += new System.EventHandler(this.CModulForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ChildRightMemu.ResumeLayout(false);
            this.ModuleTabCtrl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.SetViewPanel.ResumeLayout(false);
            this.GroupSetPanel.ResumeLayout(false);
            this.GroupSetPanel.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private CNormalTabControl ModuleTabCtrl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private MyListView ModuleView;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton ChildAdd;
        private System.Windows.Forms.ToolStripButton ChildInsert;
        private System.Windows.Forms.ToolStripButton ChildDelete;
        private System.Windows.Forms.ToolStripButton ClearClear;
        private System.Windows.Forms.ToolStripButton ChildSave;
        private System.Windows.Forms.ContextMenuStrip ChildRightMemu;
        private System.Windows.Forms.ToolStripMenuItem ChileAddModule;
        private System.Windows.Forms.ToolStripMenuItem ChileDelModule;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.TreeView LineListView;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton AddModule;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton AddDeivceBtn;
        private System.Windows.Forms.ToolStripButton DelDeviceBtn;
        private System.Windows.Forms.TreeView DeviceTreeView;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripButton DevAddBtn;
        private System.Windows.Forms.ToolStripButton DevInsertBtn;
        private System.Windows.Forms.ToolStripButton DevDeleteBtn;
        private System.Windows.Forms.ToolStripButton DevClearBtn;
        private System.Windows.Forms.ToolStripButton DevSaveBtn;
        private System.Windows.Forms.Panel SetViewPanel;
        private System.Windows.Forms.Panel GroupSetPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ChildModuleList;
        private MyListView DevDataView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox FontName;
        private System.Windows.Forms.Label FirstAddress;
        private System.Windows.Forms.Button AddChildModule;
        private System.Windows.Forms.Button ResetAddress;
        private System.Windows.Forms.Panel FormPanel;
        private System.Windows.Forms.ToolStripButton CopyDevBtn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox DevModuleComList;
        private System.Windows.Forms.ToolStripButton SaveModuleSelect;

    }
}