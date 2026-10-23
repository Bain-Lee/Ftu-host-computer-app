using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CSoePicForm
    {
        private ToolStrip toolStrip1;
        private Panel PicMainPanel;
        private Panel SoeNamePanel;
        private Panel CurrentPicPanel;
        private PictureBox CurrentPicBox;
        private ToolStripComboBox ComBox_X;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox LineSel;
        private ToolStripComboBox ComBox_Y;
        private ToolStripButton BtnReset;
        private Timer FreshTimer;
        private ToolStripLabel YHeightSet;
        private ToolStripButton BtnOpenFile;
        private ToolStripLabel StartTime;
        private ToolStripLabel EndTime;
        private ToolStripLabel LeftTime;
        private ToolStripLabel RightTime;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ShowMsg;
        private ToolStripMenuItem HideMsg;
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            PicMainPanel = new Panel();
            SoeNamePanel = new Panel();
            CurrentPicPanel = new Panel();
            CurrentPicBox = new PictureBox();
            ComBox_X = new ToolStripComboBox();
            toolStripLabel1 = new ToolStripLabel();
            LineSel = new ToolStripComboBox();
            ComBox_Y = new ToolStripComboBox();
            BtnReset = new ToolStripButton();
            FreshTimer = new Timer();
            Own(FreshTimer);
            YHeightSet = new ToolStripLabel();
            BtnOpenFile = new ToolStripButton();
            StartTime = new ToolStripLabel();
            EndTime = new ToolStripLabel();
            LeftTime = new ToolStripLabel();
            RightTime = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            contextMenuStrip1 = new ContextMenuStrip();
            ShowMsg = new ToolStripMenuItem();
            HideMsg = new ToolStripMenuItem();
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(885, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 37);
            this.toolStripLabel1.Text = "横向比例：";
            this.ComBox_X.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComBox_X.Name = "ComBox_X";
            this.ComBox_X.Size = new System.Drawing.Size(80, 40);
            this.YHeightSet.Name = "YHeightSet";
            this.YHeightSet.Size = new System.Drawing.Size(68, 37);
            this.YHeightSet.Text = "纵向比例：";
            this.LineSel.DropDownStyle = ComboBoxStyle.DropDownList;
            this.LineSel.Name = "LineSel";
            this.LineSel.Size = new System.Drawing.Size(121, 40);
            this.ComBox_Y.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComBox_Y.Name = "ComBox_Y";
            this.ComBox_Y.Size = new System.Drawing.Size(80, 40);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(60, 37);
            this.BtnReset.Text = "全部还原";
            this.BtnOpenFile.Name = "BtnOpenFile";
            this.BtnOpenFile.Size = new System.Drawing.Size(60, 37);
            this.BtnOpenFile.Text = "打开文件";
            this.StartTime.Name = "StartTime";
            this.StartTime.Size = new System.Drawing.Size(56, 37);
            this.StartTime.Text = "开始时间";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            this.EndTime.Name = "EndTime";
            this.EndTime.Size = new System.Drawing.Size(56, 37);
            this.EndTime.Text = "结束时间";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            this.LeftTime.Name = "LeftTime";
            this.LeftTime.Size = new System.Drawing.Size(44, 37);
            this.LeftTime.Text = "左时间";
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            this.RightTime.Name = "RightTime";
            this.RightTime.Size = new System.Drawing.Size(44, 37);
            this.RightTime.Text = "右时间";
            this.PicMainPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.PicMainPanel.Dock = DockStyle.Fill;
            this.PicMainPanel.Location = new System.Drawing.Point(0, 40);
            this.PicMainPanel.Name = "PicMainPanel";
            this.PicMainPanel.Size = new System.Drawing.Size(885, 323);
            this.PicMainPanel.TabIndex = 2;
            this.CurrentPicPanel.Dock = DockStyle.Fill;
            this.CurrentPicPanel.Location = new System.Drawing.Point(151, 0);
            this.CurrentPicPanel.Name = "CurrentPicPanel";
            this.CurrentPicPanel.Size = new System.Drawing.Size(734, 323);
            this.CurrentPicPanel.TabIndex = 1;
            this.CurrentPicBox.BackColor = System.Drawing.Color.Black;
            this.CurrentPicBox.Dock = DockStyle.Fill;
            this.CurrentPicBox.Location = new System.Drawing.Point(0, 0);
            this.CurrentPicBox.Name = "CurrentPicBox";
            this.CurrentPicBox.Size = new System.Drawing.Size(734, 323);
            this.CurrentPicBox.TabIndex = 0;
            this.SoeNamePanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.SoeNamePanel.ContextMenuStrip = this.contextMenuStrip1;
            this.SoeNamePanel.Dock = DockStyle.Left;
            this.SoeNamePanel.Location = new System.Drawing.Point(0, 0);
            this.SoeNamePanel.Name = "SoeNamePanel";
            this.SoeNamePanel.Size = new System.Drawing.Size(151, 323);
            this.SoeNamePanel.TabIndex = 0;
            this.FreshTimer.Interval = 50;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            this.ShowMsg.Name = "ShowMsg";
            this.ShowMsg.Size = new System.Drawing.Size(152, 22);
            this.ShowMsg.Text = "显示数值";
            this.HideMsg.Name = "HideMsg";
            this.HideMsg.Size = new System.Drawing.Size(152, 22);
            this.HideMsg.Text = "隐藏数值";
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(885, 363);
            this.Name = "CSoePicForm";
            this.Text = "录波图形分析";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.toolStripLabel1,
            this.ComBox_X,
            this.YHeightSet,
            this.LineSel,
            this.ComBox_Y,
            this.BtnReset,
            this.BtnOpenFile,
            this.StartTime,
            this.toolStripSeparator1,
            this.EndTime,
            this.toolStripSeparator2,
            this.LeftTime,
            this.toolStripSeparator3,
            this.RightTime});
            this.PicMainPanel.Controls.Add(this.CurrentPicPanel);
            this.PicMainPanel.Controls.Add(this.SoeNamePanel);
            this.CurrentPicPanel.Controls.Add(this.CurrentPicBox);
            this.contextMenuStrip1.Items.AddRange(new Control[] {
            this.ShowMsg,
            this.HideMsg});
            this.Controls.Add(this.PicMainPanel);
            this.Controls.Add(this.toolStrip1);
            ComBox_X.SelectedIndexChanged += ComBox_X_SelectedIndexChanged;
            LineSel.SelectedIndexChanged += LineSel_SelectedIndexChanged;
            ComBox_Y.SelectedIndexChanged += ComBox_Y_SelectedIndexChanged;
            BtnReset.Click += BtnReset_Click;
            BtnOpenFile.Click += BtnOpenFile_Click;
            CurrentPicBox.Paint += CurrentPicBox_Paint;
            CurrentPicBox.MouseDown += CurrentPicBox_MouseDown;
            CurrentPicBox.MouseMove += CurrentPicBox_MouseMove;
            CurrentPicBox.MouseUp += CurrentPicBox_MouseUp;
            FreshTimer.Tick += FreshTimer_Tick;
            ShowMsg.Click += ShowMsg_Click;
            HideMsg.Click += HideMsg_Click;
            Load += CSoePicForm_Load;
            Resize += CSoePicForm_Resize;
        }
    }
}
