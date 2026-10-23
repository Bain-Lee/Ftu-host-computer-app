using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CParaForm
    {
        private MenuStrip menuStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton BtnSelectAllPara;
        private ToolStripButton BtnCancleAllPara;
        private ToolStripButton BtnSelecDifferent;
        private ToolStripButton BtnReadAllPara;
        private ToolStripButton BtnReadSelectPara;
        private ToolStripButton BtnDownAllPara;
        private ToolStripButton BtnDownSelect;
        private ToolStripButton BtnSaveAll;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton BtnSaveSelcet;
        private MyListView LV_Para;
        private Timer FreshTime;
        private ToolStripButton BtnReadStandPara;
        private ToolStripButton BtnReadLocalPara;
        private ToolStripSeparator toolStripSeparator4;
        private Timer TaskTime;
        private ToolStripMenuItem SetVersionType;
        private ToolStripMenuItem ReWritePara;
        private ToolStripComboBox TableNameList;
        private ToolStripTextBox NowSelctParaName;
        private ToolStripButton UpdateParaDesc;
        private Panel panel1;
        private Panel panel2;
        private TreeView ParaTreeView;
        private ToolStripMenuItem BtnReadAeroNO;
        private ToolStripTextBox AeroNo;
        private ToolStripMenuItem BtnSwitchAeroNo;
        private ToolStripButton BtnGH;
        private Panel panel3;
        private RadioButton UseErrRand;
        private RadioButton UseMingWen;
        private RadioButton UseNoRand;
        private RadioButton UseNoSign;
        private RadioButton UseErrSign;
        private RadioButton Use_Normal;
        private ToolStripButton BtnCan;
        private ToolStripLabel Result;
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            toolStrip1 = new ToolStrip();
            BtnSelectAllPara = new ToolStripButton();
            BtnCancleAllPara = new ToolStripButton();
            BtnSelecDifferent = new ToolStripButton();
            BtnReadAllPara = new ToolStripButton();
            BtnReadSelectPara = new ToolStripButton();
            BtnDownAllPara = new ToolStripButton();
            BtnDownSelect = new ToolStripButton();
            BtnSaveAll = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            BtnSaveSelcet = new ToolStripButton();
            LV_Para = new MyListView();
            FreshTime = new Timer();
            Own(FreshTime);
            BtnReadStandPara = new ToolStripButton();
            BtnReadLocalPara = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            TaskTime = new Timer();
            Own(TaskTime);
            SetVersionType = new ToolStripMenuItem();
            ReWritePara = new ToolStripMenuItem();
            TableNameList = new ToolStripComboBox();
            NowSelctParaName = new ToolStripTextBox();
            UpdateParaDesc = new ToolStripButton();
            panel1 = new Panel();
            panel2 = new Panel();
            ParaTreeView = new TreeView();
            BtnReadAeroNO = new ToolStripMenuItem();
            AeroNo = new ToolStripTextBox();
            BtnSwitchAeroNo = new ToolStripMenuItem();
            BtnGH = new ToolStripButton();
            panel3 = new Panel();
            UseErrRand = new RadioButton();
            UseMingWen = new RadioButton();
            UseNoRand = new RadioButton();
            UseNoSign = new RadioButton();
            UseErrSign = new RadioButton();
            Use_Normal = new RadioButton();
            BtnCan = new ToolStripButton();
            Result = new ToolStripLabel();
            this.menuStrip1.BackColor = System.Drawing.Color.Azure;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1475, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.SetVersionType.Name = "SetVersionType";
            this.SetVersionType.Size = new System.Drawing.Size(93, 28);
            this.SetVersionType.Text = "板件类型";
            this.ReWritePara.Name = "ReWritePara";
            this.ReWritePara.Size = new System.Drawing.Size(93, 28);
            this.ReWritePara.Text = "回填参数";
            this.TableNameList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.TableNameList.Name = "TableNameList";
            this.TableNameList.Size = new System.Drawing.Size(160, 28);
            this.NowSelctParaName.BackColor = System.Drawing.Color.MediumAquamarine;
            this.NowSelctParaName.Name = "NowSelctParaName";
            this.NowSelctParaName.Size = new System.Drawing.Size(200, 28);
            this.BtnReadAeroNO.Name = "BtnReadAeroNO";
            this.BtnReadAeroNO.Size = new System.Drawing.Size(80, 28);
            this.BtnReadAeroNO.Text = "读定值区号";
            this.AeroNo.Name = "AeroNo";
            this.AeroNo.Size = new System.Drawing.Size(100, 28);
            this.BtnSwitchAeroNo.Name = "BtnSwitchAeroNo";
            this.BtnSwitchAeroNo.Size = new System.Drawing.Size(92, 28);
            this.BtnSwitchAeroNo.Text = "切换定值区号";
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1475, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnReadStandPara.Name = "BtnReadStandPara";
            this.BtnReadStandPara.Size = new System.Drawing.Size(141, 24);
            this.BtnReadStandPara.Text = "读取所有标准参数";
            this.BtnReadLocalPara.Name = "BtnReadLocalPara";
            this.BtnReadLocalPara.Size = new System.Drawing.Size(141, 24);
            this.BtnReadLocalPara.Text = "读取所有本地参数";
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            this.BtnSelectAllPara.Name = "BtnSelectAllPara";
            this.BtnSelectAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnSelectAllPara.Text = "全部选中";
            this.BtnCancleAllPara.Name = "BtnCancleAllPara";
            this.BtnCancleAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnCancleAllPara.Text = "全部取消";
            this.BtnSelecDifferent.Name = "BtnSelecDifferent";
            this.BtnSelecDifferent.Size = new System.Drawing.Size(85, 24);
            this.BtnSelecDifferent.Text = "反向选择";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            this.BtnReadAllPara.Name = "BtnReadAllPara";
            this.BtnReadAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnReadAllPara.Text = "全部读取";
            this.BtnReadSelectPara.Name = "BtnReadSelectPara";
            this.BtnReadSelectPara.Size = new System.Drawing.Size(85, 24);
            this.BtnReadSelectPara.Text = "读取选择";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.BtnDownAllPara.Name = "BtnDownAllPara";
            this.BtnDownAllPara.Size = new System.Drawing.Size(85, 24);
            this.BtnDownAllPara.Text = "全部预置";
            this.BtnDownSelect.Name = "BtnDownSelect";
            this.BtnDownSelect.Size = new System.Drawing.Size(85, 24);
            this.BtnDownSelect.Text = "下发预置";
            this.BtnCan.Name = "BtnCan";
            this.BtnCan.Size = new System.Drawing.Size(52, 24);
            this.BtnCan.Text = "撤销";
            this.BtnGH.Name = "BtnGH";
            this.BtnGH.Size = new System.Drawing.Size(52, 24);
            this.BtnGH.Text = "固化";
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            this.BtnSaveAll.Name = "BtnSaveAll";
            this.BtnSaveAll.Size = new System.Drawing.Size(85, 24);
            this.BtnSaveAll.Text = "全部保存";
            this.BtnSaveSelcet.Name = "BtnSaveSelcet";
            this.BtnSaveSelcet.Size = new System.Drawing.Size(85, 24);
            this.BtnSaveSelcet.Text = "保存选择";
            this.UpdateParaDesc.Name = "UpdateParaDesc";
            this.UpdateParaDesc.Size = new System.Drawing.Size(85, 24);
            this.UpdateParaDesc.Text = "更新说明";
            this.FreshTime.Interval = 500;
            this.TaskTime.Interval = 50;
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 481);
            this.panel1.TabIndex = 3;
            this.ParaTreeView.BackColor = System.Drawing.Color.Lavender;
            this.ParaTreeView.Dock = DockStyle.Fill;
            this.ParaTreeView.Location = new System.Drawing.Point(0, 0);
            this.ParaTreeView.Name = "ParaTreeView";
            this.ParaTreeView.Size = new System.Drawing.Size(203, 481);
            this.ParaTreeView.TabIndex = 0;
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(203, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1272, 481);
            this.panel2.TabIndex = 4;
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1045, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 481);
            this.panel3.TabIndex = 3;
            this.UseErrRand.AutoSize = true;
            this.UseErrRand.Location = new System.Drawing.Point(6, 188);
            this.UseErrRand.Name = "UseErrRand";
            this.UseErrRand.Size = new System.Drawing.Size(95, 16);
            this.UseErrRand.TabIndex = 13;
            this.UseErrRand.Text = "错误的随机数";
            this.UseMingWen.AutoSize = true;
            this.UseMingWen.Location = new System.Drawing.Point(6, 232);
            this.UseMingWen.Name = "UseMingWen";
            this.UseMingWen.Size = new System.Drawing.Size(71, 16);
            this.UseMingWen.TabIndex = 14;
            this.UseMingWen.Text = "下发明文";
            this.UseNoRand.AutoSize = true;
            this.UseNoRand.Location = new System.Drawing.Point(6, 144);
            this.UseNoRand.Name = "UseNoRand";
            this.UseNoRand.Size = new System.Drawing.Size(83, 16);
            this.UseNoRand.TabIndex = 15;
            this.UseNoRand.Text = "不带随机数";
            this.Use_Normal.AutoSize = true;
            this.Use_Normal.Location = new System.Drawing.Point(6, 12);
            this.Use_Normal.Name = "Use_Normal";
            this.Use_Normal.Size = new System.Drawing.Size(71, 16);
            this.Use_Normal.TabIndex = 17;
            this.Use_Normal.Text = "正常测试";
            this.UseNoSign.AutoSize = true;
            this.UseNoSign.Location = new System.Drawing.Point(6, 56);
            this.UseNoSign.Name = "UseNoSign";
            this.UseNoSign.Size = new System.Drawing.Size(71, 16);
            this.UseNoSign.TabIndex = 17;
            this.UseNoSign.Text = "不带签名";
            this.UseErrSign.AutoSize = true;
            this.UseErrSign.Location = new System.Drawing.Point(6, 100);
            this.UseErrSign.Name = "UseErrSign";
            this.UseErrSign.Size = new System.Drawing.Size(83, 16);
            this.UseErrSign.TabIndex = 19;
            this.UseErrSign.Text = "错误的签名";
            this.LV_Para.Dock = DockStyle.Left;
            this.LV_Para.Location = new System.Drawing.Point(0, 0);
            this.LV_Para.Name = "LV_Para";
            this.LV_Para.Size = new System.Drawing.Size(1045, 481);
            this.LV_Para.TabIndex = 2;
            this.Result.ForeColor = System.Drawing.Color.Red;
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(110, 24);
            this.Result.Text = "参数下发结果:";
            this.ClientSize = new System.Drawing.Size(1475, 540);
            this.Name = "CParaForm";
            this.Text = "终端参数";
            this.menuStrip1.Items.AddRange(new Control[] {
            this.SetVersionType,
            this.ReWritePara,
            this.TableNameList,
            this.NowSelctParaName,
            this.BtnReadAeroNO,
            this.AeroNo,
            this.BtnSwitchAeroNo});
            this.toolStrip1.Items.AddRange(new Control[] {
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
            this.panel1.Controls.Add(this.ParaTreeView);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.LV_Para);
            this.panel3.Controls.Add(this.UseErrRand);
            this.panel3.Controls.Add(this.UseMingWen);
            this.panel3.Controls.Add(this.UseNoRand);
            this.panel3.Controls.Add(this.Use_Normal);
            this.panel3.Controls.Add(this.UseNoSign);
            this.panel3.Controls.Add(this.UseErrSign);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            SetVersionType.Click += SetVersionType_Click;
            ReWritePara.Click += ReWritePara_Click;
            BtnReadAeroNO.Click += BtnReadAeroNO_Click;
            BtnSwitchAeroNo.Click += BtnSwitchAeroNo_Click;
            BtnReadStandPara.Click += BtnReadStandPara_Click;
            BtnReadLocalPara.Click += BtnReadLocalPara_Click;
            BtnSelectAllPara.Click += BtnSelectAllPara_Click;
            BtnCancleAllPara.Click += BtnCancleAllPara_Click;
            BtnSelecDifferent.Click += BtnSelecDifferent_Click;
            BtnReadAllPara.Click += BtnReadAllPara_Click;
            BtnReadSelectPara.Click += BtnReadSelectPara_Click;
            BtnDownAllPara.Click += BtnDownAllPara_Click;
            BtnDownSelect.Click += BtnDownSelect_Click;
            BtnCan.Click += BtnCan_Click;
            BtnGH.Click += BtnGH_Click;
            BtnSaveAll.Click += BtnSaveAll_Click;
            BtnSaveSelcet.Click += BtnSaveSelcet_Click;
            UpdateParaDesc.Click += UpdateParaDesc_Click;
            FreshTime.Tick += FreshTime_Tick;
            TaskTime.Tick += TaskTime_Tick;
            ParaTreeView.AfterSelect += ParaTreeView_AfterSelect;
            UseErrRand.CheckedChanged += UseErrRand_CheckedChanged;
            UseMingWen.CheckedChanged += UseMingWen_CheckedChanged;
            UseNoRand.CheckedChanged += UseNoRand_CheckedChanged;
            Use_Normal.CheckedChanged += Use_Normal_CheckedChanged;
            UseNoSign.CheckedChanged += UseNoSign_CheckedChanged;
            UseErrSign.CheckedChanged += UseErrSign_CheckedChanged;
            Load += CParaForm_Load;
        }
    }
}
