using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class MainForm
    {
        private MyPanel TopLogPic;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Timer FreshTimer;
        private ToolStrip toolStrip1;
        private ToolStripButton Btn_ConSet;
        private ToolStripButton Btn_RealData;
        private ToolStripButton Btn_LongCtrl;
        private ToolStripButton Btn_ParaSet;
        private ToolStripButton Btn_HisRecord;
        private ToolStripButton Btu_RunStart;
        private ToolStripButton Btn_StopRun;
        private ToolStripButton UpdateButton;
        private ToolStripButton Btn_Encode;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem Btn_ModeSet1;
        private ToolStripMenuItem DeviceSetting1;
        private ToolStripMenuItem Btn_CommModeSet1;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem Btn_DataCheck1;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem BtnLuBo1;
        private ToolStripMenuItem BtnDownFile1;
        private ToolStripMenuItem toolStripMenuItem15;
        private ToolStripMenuItem BtnFTUFastUpdate;
        private ToolStripMenuItem toolStripMenuItem18;
        private ToolStripMenuItem Btn_Function1;
        private ToolStripMenuItem BtnDataTran1;
        private ToolStripMenuItem Btn_ProExp1;
        private ToolStripButton Btn_EngPara;
        private ToolStripMenuItem BtnSpePara;
        private SplitContainer MainContain;
        private DockPanel PanelView;
        private ToolStripButton Btn_Output;
        private MyPanel myPanelForm;
        private Timer timer1;
        private ToolStripMenuItem BtnSFTPFileDOwn;
        private void InitializeComponent()
        {
            TopLogPic = new MyPanel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            FreshTimer = new Timer();
            Own(FreshTimer);
            toolStrip1 = new ToolStrip();
            Btn_ConSet = new ToolStripButton();
            Btn_RealData = new ToolStripButton();
            Btn_LongCtrl = new ToolStripButton();
            Btn_ParaSet = new ToolStripButton();
            Btn_HisRecord = new ToolStripButton();
            Btu_RunStart = new ToolStripButton();
            Btn_StopRun = new ToolStripButton();
            UpdateButton = new ToolStripButton();
            Btn_Encode = new ToolStripButton();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            Btn_ModeSet1 = new ToolStripMenuItem();
            DeviceSetting1 = new ToolStripMenuItem();
            Btn_CommModeSet1 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            Btn_DataCheck1 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            BtnLuBo1 = new ToolStripMenuItem();
            BtnDownFile1 = new ToolStripMenuItem();
            toolStripMenuItem15 = new ToolStripMenuItem();
            BtnFTUFastUpdate = new ToolStripMenuItem();
            toolStripMenuItem18 = new ToolStripMenuItem();
            Btn_Function1 = new ToolStripMenuItem();
            BtnDataTran1 = new ToolStripMenuItem();
            Btn_ProExp1 = new ToolStripMenuItem();
            Btn_EngPara = new ToolStripButton();
            BtnSpePara = new ToolStripMenuItem();
            MainContain = new SplitContainer();
            PanelView = new DockPanel();
            Btn_Output = new ToolStripButton();
            myPanelForm = new MyPanel();
            timer1 = new Timer();
            Own(timer1);
            BtnSFTPFileDOwn = new ToolStripMenuItem();
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabel1.Text = "软件版本：V1.01";
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            this.FreshTimer.Interval = 1000;
            this.menuStrip1.BackColor = System.Drawing.Color.Lavender;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem1.Text = "配置功能";
            this.Btn_ModeSet1.Name = "Btn_ModeSet1";
            this.Btn_ModeSet1.Size = new System.Drawing.Size(148, 22);
            this.Btn_ModeSet1.Text = "模板配置";
            this.DeviceSetting1.Name = "DeviceSetting1";
            this.DeviceSetting1.Size = new System.Drawing.Size(148, 22);
            this.DeviceSetting1.Text = "装置配置";
            this.Btn_CommModeSet1.Name = "Btn_CommModeSet1";
            this.Btn_CommModeSet1.Size = new System.Drawing.Size(148, 22);
            this.Btn_CommModeSet1.Text = "通信模块配置";
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem5.Text = "远动功能";
            this.Btn_DataCheck1.Name = "Btn_DataCheck1";
            this.Btn_DataCheck1.Size = new System.Drawing.Size(152, 22);
            this.Btn_DataCheck1.Text = "数据校验";
            this.BtnSpePara.Name = "BtnSpePara";
            this.BtnSpePara.Size = new System.Drawing.Size(152, 22);
            this.BtnSpePara.Text = "厂站参数";
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem11.Text = "文件功能";
            this.BtnLuBo1.Name = "BtnLuBo1";
            this.BtnLuBo1.Size = new System.Drawing.Size(152, 22);
            this.BtnLuBo1.Text = "录波文件";
            this.BtnDownFile1.Name = "BtnDownFile1";
            this.BtnDownFile1.Size = new System.Drawing.Size(152, 22);
            this.BtnDownFile1.Text = "文件传输";
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem15.Text = "升级功能";
            this.BtnFTUFastUpdate.Name = "BtnFTUFastUpdate";
            this.BtnFTUFastUpdate.Size = new System.Drawing.Size(152, 22);
            this.BtnFTUFastUpdate.Text = "FTU私有升级";
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem18.Text = "其他功能";
            this.Btn_Function1.Name = "Btn_Function1";
            this.Btn_Function1.Size = new System.Drawing.Size(148, 22);
            this.Btn_Function1.Text = "辅助功能";
            this.BtnDataTran1.Name = "BtnDataTran1";
            this.BtnDataTran1.Size = new System.Drawing.Size(148, 22);
            this.BtnDataTran1.Text = "数据转换";
            this.Btn_ProExp1.Name = "Btn_ProExp1";
            this.Btn_ProExp1.Size = new System.Drawing.Size(148, 22);
            this.Btn_ProExp1.Text = "规约异常测试";
            this.MainContain.BackColor = System.Drawing.Color.Pink;
            this.MainContain.Dock = DockStyle.Fill;
            this.MainContain.Location = new System.Drawing.Point(0, 87);
            this.MainContain.Name = "MainContain";
            this.MainContain.Orientation = Orientation.Horizontal;
            this.MainContain.Panel1.BackColor = System.Drawing.Color.GhostWhite;
            this.MainContain.Size = new System.Drawing.Size(1370, 616);
            this.MainContain.SplitterDistance = 484;
            this.MainContain.TabIndex = 12;
            this.myPanelForm.Dock = DockStyle.Fill;
            this.myPanelForm.Location = new System.Drawing.Point(0, 0);
            this.myPanelForm.Name = "myPanelForm";
            this.myPanelForm.Size = new System.Drawing.Size(1370, 484);
            this.myPanelForm.TabIndex = 0;
            this.PanelView.BackColor = System.Drawing.Color.AliceBlue;
            this.PanelView.Dock = DockStyle.Fill;
            this.PanelView.Location = new System.Drawing.Point(0, 0);
            this.PanelView.Name = "PanelView";
            this.PanelView.Size = new System.Drawing.Size(1370, 128);
            this.PanelView.TabIndex = 0;
            this.TopLogPic.Dock = DockStyle.Top;
            this.TopLogPic.Location = new System.Drawing.Point(0, 25);
            this.TopLogPic.Name = "TopLogPic";
            this.TopLogPic.Size = new System.Drawing.Size(1370, 62);
            this.TopLogPic.TabIndex = 0;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = DockStyle.Fill;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 62);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.Btn_ConSet.ForeColor = System.Drawing.Color.Black;
            this.Btn_ConSet.Name = "Btn_ConSet";
            this.Btn_ConSet.Size = new System.Drawing.Size(60, 59);
            this.Btn_ConSet.Text = "通讯配置";
            this.Btn_RealData.ForeColor = System.Drawing.Color.Black;
            this.Btn_RealData.Name = "Btn_RealData";
            this.Btn_RealData.Size = new System.Drawing.Size(60, 59);
            this.Btn_RealData.Text = "实时监测";
            this.Btn_LongCtrl.ForeColor = System.Drawing.Color.Black;
            this.Btn_LongCtrl.Name = "Btn_LongCtrl";
            this.Btn_LongCtrl.Size = new System.Drawing.Size(60, 59);
            this.Btn_LongCtrl.Text = "远程控制";
            this.Btn_ParaSet.ForeColor = System.Drawing.Color.Black;
            this.Btn_ParaSet.Name = "Btn_ParaSet";
            this.Btn_ParaSet.Size = new System.Drawing.Size(60, 59);
            this.Btn_ParaSet.Text = "参数配置";
            this.Btn_EngPara.Name = "Btn_EngPara";
            this.Btn_EngPara.Size = new System.Drawing.Size(60, 59);
            this.Btn_EngPara.Text = "工程参数";
            this.Btn_HisRecord.ForeColor = System.Drawing.Color.Black;
            this.Btn_HisRecord.Name = "Btn_HisRecord";
            this.Btn_HisRecord.Size = new System.Drawing.Size(60, 59);
            this.Btn_HisRecord.Text = "历史记录";
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(60, 59);
            this.UpdateButton.Text = "远程升级";
            this.Btn_Output.Name = "Btn_Output";
            this.Btn_Output.Size = new System.Drawing.Size(60, 59);
            this.Btn_Output.Text = "监视窗口";
            this.Btn_Encode.Name = "Btn_Encode";
            this.Btn_Encode.Size = new System.Drawing.Size(60, 59);
            this.Btn_Encode.Text = "加密管理";
            this.Btu_RunStart.ForeColor = System.Drawing.Color.Black;
            this.Btu_RunStart.Name = "Btu_RunStart";
            this.Btu_RunStart.Size = new System.Drawing.Size(60, 59);
            this.Btu_RunStart.Text = "开始运行";
            this.Btn_StopRun.Enabled = false;
            this.Btn_StopRun.ForeColor = System.Drawing.Color.Black;
            this.Btn_StopRun.Name = "Btn_StopRun";
            this.Btn_StopRun.Size = new System.Drawing.Size(60, 59);
            this.Btn_StopRun.Text = "停止运行";
            this.timer1.Interval = 180000;
            this.BtnSFTPFileDOwn.Name = "BtnSFTPFileDOwn";
            this.BtnSFTPFileDOwn.Size = new System.Drawing.Size(152, 22);
            this.BtnSFTPFileDOwn.Text = "SFTP下载";
            this.ClientSize = new System.Drawing.Size(1370, 703);
            this.Name = "MainForm";
            this.Text = "配电终端测试软件（版本号V2.00）";
            this.menuStrip1.Items.AddRange(new Control[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem5,
            this.toolStripMenuItem11,
            this.toolStripMenuItem15,
            this.toolStripMenuItem18});
            this.toolStripMenuItem1.DropDownItems.AddRange(new Control[] {
            this.Btn_ModeSet1,
            this.DeviceSetting1,
            this.Btn_CommModeSet1});
            this.toolStripMenuItem5.DropDownItems.AddRange(new Control[] {
            this.Btn_DataCheck1,
            this.BtnSpePara});
            this.toolStripMenuItem11.DropDownItems.AddRange(new Control[] {
            this.BtnLuBo1,
            this.BtnDownFile1,
            this.BtnSFTPFileDOwn});
            this.toolStripMenuItem15.DropDownItems.AddRange(new Control[] {
            this.BtnFTUFastUpdate});
            this.toolStripMenuItem18.DropDownItems.AddRange(new Control[] {
            this.Btn_Function1,
            this.BtnDataTran1,
            this.Btn_ProExp1});
            this.MainContain.Panel1.Controls.Add(this.myPanelForm);
            this.MainContain.Panel2.Controls.Add(this.PanelView);
            this.TopLogPic.Controls.Add(this.toolStrip1);
            this.toolStrip1.Items.AddRange(new Control[] {
            this.Btn_ConSet,
            this.Btn_RealData,
            this.Btn_LongCtrl,
            this.Btn_ParaSet,
            this.Btn_EngPara,
            this.Btn_HisRecord,
            this.UpdateButton,
            this.Btn_Output,
            this.Btn_Encode,
            this.Btu_RunStart,
            this.Btn_StopRun});
            this.Controls.Add(this.MainContain);
            this.Controls.Add(this.TopLogPic);
            this.Controls.Add(this.menuStrip1);
            FreshTimer.Tick += FreshTimer_Tick;
            Btn_ModeSet1.Click += Btn_ModeSet1_Click;
            DeviceSetting1.Click += DeviceSetting1_Click;
            Btn_CommModeSet1.Click += Btn_CommModeSet1_Click;
            Btn_DataCheck1.Click += Btn_DataCheck1_Click;
            BtnSpePara.Click += BtnSpePara_Click;
            BtnLuBo1.Click += BtnLuBo1_Click;
            BtnDownFile1.Click += BtnDownFile1_Click;
            BtnFTUFastUpdate.Click += BtnFTUFastUpdate_Click;
            Btn_Function1.Click += Btn_Function1_Click;
            BtnDataTran1.Click += BtnDataTran1_Click;
            Btn_ProExp1.Click += Btn_ProExp1_Click;
            Btn_ConSet.Click += Btn_ConSet_Click;
            Btn_RealData.Click += Btn_RealData_Click;
            Btn_LongCtrl.Click += Btn_LongCtrl_Click;
            Btn_ParaSet.Click += Btn_ParaSet_Click;
            Btn_EngPara.Click += Btn_EngPara_Click;
            Btn_HisRecord.Click += Btn_HisRecord_Click;
            UpdateButton.Click += UpdateButton_Click;
            Btn_Output.Click += Btn_Output_Click;
            Btn_Encode.Click += Btn_Encode_Click;
            Btu_RunStart.Click += Btu_RunStart_Click;
            Btn_StopRun.Click += Btn_StopRun_Click;
            timer1.Tick += timer1_Tick;
            BtnSFTPFileDOwn.Click += BtnSFTPFileDOwn_Click;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
        }
    }
}
