namespace DtuTestManager
{
    partial class CParaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CParaForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SetVersionType = new System.Windows.Forms.ToolStripMenuItem();
            this.ReWritePara = new System.Windows.Forms.ToolStripMenuItem();
            this.TableNameList = new System.Windows.Forms.ToolStripComboBox();
            this.NowSelctParaName = new System.Windows.Forms.ToolStripTextBox();
            this.BtnReadAeroNO = new System.Windows.Forms.ToolStripMenuItem();
            this.AeroNo = new System.Windows.Forms.ToolStripTextBox();
            this.BtnSwitchAeroNo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnReadStandPara = new System.Windows.Forms.ToolStripButton();
            this.BtnReadLocalPara = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnSelectAllPara = new System.Windows.Forms.ToolStripButton();
            this.BtnCancleAllPara = new System.Windows.Forms.ToolStripButton();
            this.BtnSelecDifferent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnReadAllPara = new System.Windows.Forms.ToolStripButton();
            this.BtnReadSelectPara = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnDownAllPara = new System.Windows.Forms.ToolStripButton();
            this.BtnDownSelect = new System.Windows.Forms.ToolStripButton();
            this.BtnCan = new System.Windows.Forms.ToolStripButton();
            this.BtnGH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.BtnSaveSelcet = new System.Windows.Forms.ToolStripButton();
            this.UpdateParaDesc = new System.Windows.Forms.ToolStripButton();
            this.FreshTime = new System.Windows.Forms.Timer(this.components);
            this.TaskTime = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ParaTreeView = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.UseErrRand = new System.Windows.Forms.RadioButton();
            this.UseMingWen = new System.Windows.Forms.RadioButton();
            this.UseNoRand = new System.Windows.Forms.RadioButton();
            this.Use_Normal = new System.Windows.Forms.RadioButton();
            this.UseNoSign = new System.Windows.Forms.RadioButton();
            this.UseErrSign = new System.Windows.Forms.RadioButton();
            this.LV_Para = new DtuTestManager.MyListView(this.components);
            this.Result = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Azure;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetVersionType,
            this.ReWritePara,
            this.TableNameList,
            this.NowSelctParaName,
            this.BtnReadAeroNO,
            this.AeroNo,
            this.BtnSwitchAeroNo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1475, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SetVersionType
            // 
            this.SetVersionType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SetVersionType.Image = ((System.Drawing.Image)(resources.GetObject("SetVersionType.Image")));
            this.SetVersionType.Name = "SetVersionType";
            this.SetVersionType.Size = new System.Drawing.Size(93, 28);
            this.SetVersionType.Text = "板件类型";
            this.SetVersionType.Click += new System.EventHandler(this.SetVersionType_Click);
            // 
            // ReWritePara
            // 
            this.ReWritePara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ReWritePara.Image = ((System.Drawing.Image)(resources.GetObject("ReWritePara.Image")));
            this.ReWritePara.Name = "ReWritePara";
            this.ReWritePara.Size = new System.Drawing.Size(93, 28);
            this.ReWritePara.Text = "回填参数";
            this.ReWritePara.Click += new System.EventHandler(this.ReWritePara_Click);
            // 
            // TableNameList
            // 
            this.TableNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TableNameList.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TableNameList.Name = "TableNameList";
            this.TableNameList.Size = new System.Drawing.Size(160, 28);
            // 
            // NowSelctParaName
            // 
            this.NowSelctParaName.BackColor = System.Drawing.Color.MediumAquamarine;
            this.NowSelctParaName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.NowSelctParaName.Name = "NowSelctParaName";
            this.NowSelctParaName.Size = new System.Drawing.Size(200, 28);
            // 
            // BtnReadAeroNO
            // 
            this.BtnReadAeroNO.Name = "BtnReadAeroNO";
            this.BtnReadAeroNO.Size = new System.Drawing.Size(80, 28);
            this.BtnReadAeroNO.Text = "读定值区号";
            this.BtnReadAeroNO.Click += new System.EventHandler(this.BtnReadAeroNO_Click);
            // 
            // AeroNo
            // 
            this.AeroNo.Name = "AeroNo";
            this.AeroNo.Size = new System.Drawing.Size(100, 28);
            // 
            // BtnSwitchAeroNo
            // 
            this.BtnSwitchAeroNo.Name = "BtnSwitchAeroNo";
            this.BtnSwitchAeroNo.Size = new System.Drawing.Size(92, 28);
            this.BtnSwitchAeroNo.Text = "切换定值区号";
            this.BtnSwitchAeroNo.Click += new System.EventHandler(this.BtnSwitchAeroNo_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnReadStandPara,
            this.BtnReadLocalPara,
            this.toolStripSeparator4,
            this.BtnSelectAllPara,
            this.BtnCancleAllPara,
            this.BtnSelecDifferent,
            this.toolStripSeparator1,
            this.BtnReadAllPara,
            this.BtnReadSelectPara,
            this.toolStripSeparator2,
            this.BtnDownAllPara,
            this.BtnDownSelect,
            this.BtnCan,
            this.BtnGH,
            this.Result,
            this.toolStripSeparator3,
            this.BtnSaveAll,
            this.BtnSaveSelcet,
            this.UpdateParaDesc});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1475, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnReadStandPara
            // 
            this.BtnReadStandPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnReadStandPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnReadStandPara.Image")));
            this.BtnReadStandPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnReadStandPara.Name = "BtnReadStandPara";
            this.BtnReadStandPara.Size = new System.Drawing.Size(141, 24);
            this.BtnReadStandPara.Text = "读取所有标准参数";
            this.BtnReadStandPara.Click += new System.EventHandler(this.BtnReadStandPara_Click);
            // 
            // BtnReadLocalPara
            // 
            this.BtnReadLocalPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnReadLocalPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnReadLocalPara.Image")));
            this.BtnReadLocalPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnReadLocalPara.Name = "BtnReadLocalPara";
            this.BtnReadLocalPara.Size = new System.Drawing.Size(141, 24);
            this.BtnReadLocalPara.Text = "读取所有本地参数";
            this.BtnReadLocalPara.Click += new System.EventHandler(this.BtnReadLocalPara_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // BtnSelectAllPara
            // 
            this.BtnSelectAllPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnSelectAllPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnSelectAllPara.Image")));
            this.BtnSelectAllPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSelectAllPara.Name = "BtnSelectAllPara";
            this.BtnSelectAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnSelectAllPara.Text = "全部选中";
            this.BtnSelectAllPara.Click += new System.EventHandler(this.BtnSelectAllPara_Click);
            // 
            // BtnCancleAllPara
            // 
            this.BtnCancleAllPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnCancleAllPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancleAllPara.Image")));
            this.BtnCancleAllPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCancleAllPara.Name = "BtnCancleAllPara";
            this.BtnCancleAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnCancleAllPara.Text = "全部取消";
            this.BtnCancleAllPara.Click += new System.EventHandler(this.BtnCancleAllPara_Click);
            // 
            // BtnSelecDifferent
            // 
            this.BtnSelecDifferent.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnSelecDifferent.Image = ((System.Drawing.Image)(resources.GetObject("BtnSelecDifferent.Image")));
            this.BtnSelecDifferent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSelecDifferent.Name = "BtnSelecDifferent";
            this.BtnSelecDifferent.Size = new System.Drawing.Size(85, 24);
            this.BtnSelecDifferent.Text = "反向选择";
            this.BtnSelecDifferent.Click += new System.EventHandler(this.BtnSelecDifferent_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // BtnReadAllPara
            // 
            this.BtnReadAllPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnReadAllPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnReadAllPara.Image")));
            this.BtnReadAllPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnReadAllPara.Name = "BtnReadAllPara";
            this.BtnReadAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnReadAllPara.Text = "全部读取";
            this.BtnReadAllPara.Click += new System.EventHandler(this.BtnReadAllPara_Click);
            // 
            // BtnReadSelectPara
            // 
            this.BtnReadSelectPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnReadSelectPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnReadSelectPara.Image")));
            this.BtnReadSelectPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnReadSelectPara.Name = "BtnReadSelectPara";
            this.BtnReadSelectPara.Size = new System.Drawing.Size(85, 24);
            this.BtnReadSelectPara.Text = "读取选择";
            this.BtnReadSelectPara.Click += new System.EventHandler(this.BtnReadSelectPara_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // BtnDownAllPara
            // 
            this.BtnDownAllPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnDownAllPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnDownAllPara.Image")));
            this.BtnDownAllPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnDownAllPara.Name = "BtnDownAllPara";
            this.BtnDownAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnDownAllPara.Text = "全部预置";
            this.BtnDownAllPara.Click += new System.EventHandler(this.BtnDownAllPara_Click);
            // 
            // BtnDownSelect
            // 
            this.BtnDownSelect.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnDownSelect.Image = ((System.Drawing.Image)(resources.GetObject("BtnDownSelect.Image")));
            this.BtnDownSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnDownSelect.Name = "BtnDownSelect";
            this.BtnDownSelect.Size = new System.Drawing.Size(85, 24);
            this.BtnDownSelect.Text = "下发预置";
            this.BtnDownSelect.Click += new System.EventHandler(this.BtnDownSelect_Click);
            // 
            // BtnCan
            // 
            this.BtnCan.Image = ((System.Drawing.Image)(resources.GetObject("BtnCan.Image")));
            this.BtnCan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCan.Name = "BtnCan";
            this.BtnCan.Size = new System.Drawing.Size(52, 24);
            this.BtnCan.Text = "撤销";
            this.BtnCan.Click += new System.EventHandler(this.BtnCan_Click);
            // 
            // BtnGH
            // 
            this.BtnGH.Image = ((System.Drawing.Image)(resources.GetObject("BtnGH.Image")));
            this.BtnGH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnGH.Name = "BtnGH";
            this.BtnGH.Size = new System.Drawing.Size(52, 24);
            this.BtnGH.Text = "固化";
            this.BtnGH.Click += new System.EventHandler(this.BtnGH_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // BtnSaveAll
            // 
            this.BtnSaveAll.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("BtnSaveAll.Image")));
            this.BtnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSaveAll.Name = "BtnSaveAll";
            this.BtnSaveAll.Size = new System.Drawing.Size(85, 24);
            this.BtnSaveAll.Text = "全部保存";
            this.BtnSaveAll.Click += new System.EventHandler(this.BtnSaveAll_Click);
            // 
            // BtnSaveSelcet
            // 
            this.BtnSaveSelcet.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtnSaveSelcet.Image = ((System.Drawing.Image)(resources.GetObject("BtnSaveSelcet.Image")));
            this.BtnSaveSelcet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSaveSelcet.Name = "BtnSaveSelcet";
            this.BtnSaveSelcet.Size = new System.Drawing.Size(85, 24);
            this.BtnSaveSelcet.Text = "保存选择";
            this.BtnSaveSelcet.Click += new System.EventHandler(this.BtnSaveSelcet_Click);
            // 
            // UpdateParaDesc
            // 
            this.UpdateParaDesc.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UpdateParaDesc.Image = ((System.Drawing.Image)(resources.GetObject("UpdateParaDesc.Image")));
            this.UpdateParaDesc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateParaDesc.Name = "UpdateParaDesc";
            this.UpdateParaDesc.Size = new System.Drawing.Size(85, 24);
            this.UpdateParaDesc.Text = "更新说明";
            this.UpdateParaDesc.Click += new System.EventHandler(this.UpdateParaDesc_Click);
            // 
            // FreshTime
            // 
            this.FreshTime.Interval = 500;
            this.FreshTime.Tick += new System.EventHandler(this.FreshTime_Tick);
            // 
            // TaskTime
            // 
            this.TaskTime.Interval = 50;
            this.TaskTime.Tick += new System.EventHandler(this.TaskTime_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ParaTreeView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 481);
            this.panel1.TabIndex = 3;
            // 
            // ParaTreeView
            // 
            this.ParaTreeView.BackColor = System.Drawing.Color.Lavender;
            this.ParaTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaTreeView.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaTreeView.Location = new System.Drawing.Point(0, 0);
            this.ParaTreeView.Name = "ParaTreeView";
            this.ParaTreeView.Size = new System.Drawing.Size(203, 481);
            this.ParaTreeView.TabIndex = 0;
            this.ParaTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ParaTreeView_AfterSelect);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.LV_Para);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(203, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1272, 481);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Controls.Add(this.UseErrRand);
            this.panel3.Controls.Add(this.UseMingWen);
            this.panel3.Controls.Add(this.UseNoRand);
            this.panel3.Controls.Add(this.Use_Normal);
            this.panel3.Controls.Add(this.UseNoSign);
            this.panel3.Controls.Add(this.UseErrSign);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1045, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 481);
            this.panel3.TabIndex = 3;
            // 
            // UseErrRand
            // 
            this.UseErrRand.AutoSize = true;
            this.UseErrRand.Location = new System.Drawing.Point(6, 188);
            this.UseErrRand.Name = "UseErrRand";
            this.UseErrRand.Size = new System.Drawing.Size(95, 16);
            this.UseErrRand.TabIndex = 13;
            this.UseErrRand.Text = "错误的随机数";
            this.UseErrRand.UseVisualStyleBackColor = true;
            this.UseErrRand.CheckedChanged += new System.EventHandler(this.UseErrRand_CheckedChanged);
            // 
            // UseMingWen
            // 
            this.UseMingWen.AutoSize = true;
            this.UseMingWen.Location = new System.Drawing.Point(6, 232);
            this.UseMingWen.Name = "UseMingWen";
            this.UseMingWen.Size = new System.Drawing.Size(71, 16);
            this.UseMingWen.TabIndex = 14;
            this.UseMingWen.Text = "下发明文";
            this.UseMingWen.UseVisualStyleBackColor = true;
            this.UseMingWen.CheckedChanged += new System.EventHandler(this.UseMingWen_CheckedChanged);
            // 
            // UseNoRand
            // 
            this.UseNoRand.AutoSize = true;
            this.UseNoRand.Location = new System.Drawing.Point(6, 144);
            this.UseNoRand.Name = "UseNoRand";
            this.UseNoRand.Size = new System.Drawing.Size(83, 16);
            this.UseNoRand.TabIndex = 15;
            this.UseNoRand.Text = "不带随机数";
            this.UseNoRand.UseVisualStyleBackColor = true;
            this.UseNoRand.CheckedChanged += new System.EventHandler(this.UseNoRand_CheckedChanged);
            // 
            // Use_Normal
            // 
            this.Use_Normal.AutoSize = true;
            this.Use_Normal.Location = new System.Drawing.Point(6, 12);
            this.Use_Normal.Name = "Use_Normal";
            this.Use_Normal.Size = new System.Drawing.Size(71, 16);
            this.Use_Normal.TabIndex = 17;
            this.Use_Normal.Text = "正常测试";
            this.Use_Normal.UseVisualStyleBackColor = true;
            this.Use_Normal.CheckedChanged += new System.EventHandler(this.Use_Normal_CheckedChanged);
            // 
            // UseNoSign
            // 
            this.UseNoSign.AutoSize = true;
            this.UseNoSign.Location = new System.Drawing.Point(6, 56);
            this.UseNoSign.Name = "UseNoSign";
            this.UseNoSign.Size = new System.Drawing.Size(71, 16);
            this.UseNoSign.TabIndex = 17;
            this.UseNoSign.Text = "不带签名";
            this.UseNoSign.UseVisualStyleBackColor = true;
            this.UseNoSign.CheckedChanged += new System.EventHandler(this.UseNoSign_CheckedChanged);
            // 
            // UseErrSign
            // 
            this.UseErrSign.AutoSize = true;
            this.UseErrSign.Location = new System.Drawing.Point(6, 100);
            this.UseErrSign.Name = "UseErrSign";
            this.UseErrSign.Size = new System.Drawing.Size(83, 16);
            this.UseErrSign.TabIndex = 19;
            this.UseErrSign.Text = "错误的签名";
            this.UseErrSign.UseVisualStyleBackColor = true;
            this.UseErrSign.CheckedChanged += new System.EventHandler(this.UseErrSign_CheckedChanged);
            // 
            // LV_Para
            // 
            this.LV_Para.Dock = System.Windows.Forms.DockStyle.Left;
            this.LV_Para.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LV_Para.FullRowSelect = true;
            this.LV_Para.GridLines = true;
            this.LV_Para.Location = new System.Drawing.Point(0, 0);
            this.LV_Para.Name = "LV_Para";
            this.LV_Para.OwnerDraw = true;
            this.LV_Para.ProgressColumnIndex = -1;
            this.LV_Para.Size = new System.Drawing.Size(1045, 481);
            this.LV_Para.TabIndex = 2;
            this.LV_Para.UseCompatibleStateImageBehavior = false;
            this.LV_Para.View = System.Windows.Forms.View.Details;
            // 
            // Result
            // 
            this.Result.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Result.ForeColor = System.Drawing.Color.Red;
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(110, 24);
            this.Result.Text = "参数下发结果:";
            // 
            // CParaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 540);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CParaForm";
            this.Text = "终端参数";
            this.Load += new System.EventHandler(this.CParaForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnSelectAllPara;
        private System.Windows.Forms.ToolStripButton BtnCancleAllPara;
        private System.Windows.Forms.ToolStripButton BtnSelecDifferent;
        private System.Windows.Forms.ToolStripButton BtnReadAllPara;
        private System.Windows.Forms.ToolStripButton BtnReadSelectPara;
        private System.Windows.Forms.ToolStripButton BtnDownAllPara;
        private System.Windows.Forms.ToolStripButton BtnDownSelect;
        private System.Windows.Forms.ToolStripButton BtnSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton BtnSaveSelcet;
        private MyListView LV_Para;
        private System.Windows.Forms.Timer FreshTime;
        private System.Windows.Forms.ToolStripButton BtnReadStandPara;
        private System.Windows.Forms.ToolStripButton BtnReadLocalPara;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Timer TaskTime;
        private System.Windows.Forms.ToolStripMenuItem SetVersionType;
        private System.Windows.Forms.ToolStripMenuItem ReWritePara;
        private System.Windows.Forms.ToolStripComboBox TableNameList;
        private System.Windows.Forms.ToolStripTextBox NowSelctParaName;
        private System.Windows.Forms.ToolStripButton UpdateParaDesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView ParaTreeView;
        private System.Windows.Forms.ToolStripMenuItem BtnReadAeroNO;
        private System.Windows.Forms.ToolStripTextBox AeroNo;
        private System.Windows.Forms.ToolStripMenuItem BtnSwitchAeroNo;
        private System.Windows.Forms.ToolStripButton BtnGH;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton UseErrRand;
        private System.Windows.Forms.RadioButton UseMingWen;
        private System.Windows.Forms.RadioButton UseNoRand;
        private System.Windows.Forms.RadioButton UseNoSign;
        private System.Windows.Forms.RadioButton UseErrSign;
        private System.Windows.Forms.RadioButton Use_Normal;
        private System.Windows.Forms.ToolStripButton BtnCan;
        private System.Windows.Forms.ToolStripLabel Result;
    }
}