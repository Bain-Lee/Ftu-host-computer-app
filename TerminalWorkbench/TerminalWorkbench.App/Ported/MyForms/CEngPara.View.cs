using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CEngPara
    {
        private ToolStrip toolStrip1;
        private ToolStripButton BtnAllDown;
        private ToolStripButton BtnAllRead;
        private ToolStripButton BtnNewFile;
        private ToolStripButton BtnEdit;
        private TreeView ParaFileTree;
        private ListView ParaLV;
        private Timer TaskTime;
        private ToolStripButton BtnModPara;
        private ToolStripButton BtnSavePara;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton BtnSelectAll;
        private ToolStripButton BtnCancel;
        private ToolStripButton BtnSelectNo;
        private Timer FreshTime;
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            BtnAllDown = new ToolStripButton();
            BtnAllRead = new ToolStripButton();
            BtnNewFile = new ToolStripButton();
            BtnEdit = new ToolStripButton();
            ParaFileTree = new TreeView();
            ParaLV = new ListView();
            TaskTime = new Timer();
            Own(TaskTime);
            BtnModPara = new ToolStripButton();
            BtnSavePara = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            BtnSelectAll = new ToolStripButton();
            BtnCancel = new ToolStripButton();
            BtnSelectNo = new ToolStripButton();
            FreshTime = new Timer();
            Own(FreshTime);
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1020, 28);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnAllDown.Name = "BtnAllDown";
            this.BtnAllDown.Size = new System.Drawing.Size(94, 25);
            this.BtnAllDown.Text = "全部下发";
            this.BtnAllRead.Name = "BtnAllRead";
            this.BtnAllRead.Size = new System.Drawing.Size(94, 25);
            this.BtnAllRead.Text = "全部召唤";
            this.BtnNewFile.Name = "BtnNewFile";
            this.BtnNewFile.Size = new System.Drawing.Size(94, 25);
            this.BtnNewFile.Text = "新建文件";
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(94, 25);
            this.BtnEdit.Text = "编辑文件";
            this.BtnModPara.Name = "BtnModPara";
            this.BtnModPara.Size = new System.Drawing.Size(94, 25);
            this.BtnModPara.Text = "修改参数";
            this.BtnSavePara.Name = "BtnSavePara";
            this.BtnSavePara.Size = new System.Drawing.Size(94, 25);
            this.BtnSavePara.Text = "保存参数";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            this.BtnSelectAll.Name = "BtnSelectAll";
            this.BtnSelectAll.Size = new System.Drawing.Size(78, 25);
            this.BtnSelectAll.Text = "全部选中";
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(78, 25);
            this.BtnCancel.Text = "全部取消";
            this.BtnSelectNo.Name = "BtnSelectNo";
            this.BtnSelectNo.Size = new System.Drawing.Size(78, 25);
            this.BtnSelectNo.Text = "反向选择";
            this.ParaFileTree.BackColor = System.Drawing.Color.Lavender;
            this.ParaFileTree.Dock = DockStyle.Left;
            this.ParaFileTree.Location = new System.Drawing.Point(0, 28);
            this.ParaFileTree.Name = "ParaFileTree";
            this.ParaFileTree.Size = new System.Drawing.Size(271, 478);
            this.ParaFileTree.TabIndex = 1;
            this.ParaLV.Dock = DockStyle.Fill;
            this.ParaLV.Location = new System.Drawing.Point(271, 28);
            this.ParaLV.Name = "ParaLV";
            this.ParaLV.Size = new System.Drawing.Size(749, 478);
            this.ParaLV.TabIndex = 2;
            this.TaskTime.Interval = 50;
            this.FreshTime.Interval = 50;
            this.ClientSize = new System.Drawing.Size(1020, 506);
            this.Name = "CEngPara";
            this.Text = "工程参数";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.BtnAllDown,
            this.BtnAllRead,
            this.BtnNewFile,
            this.BtnEdit,
            this.BtnModPara,
            this.BtnSavePara,
            this.toolStripSeparator1,
            this.BtnSelectAll,
            this.BtnCancel,
            this.BtnSelectNo});
            this.Controls.Add(this.ParaLV);
            this.Controls.Add(this.ParaFileTree);
            this.Controls.Add(this.toolStrip1);
            BtnAllDown.Click += BtnAllDown_Click;
            BtnAllRead.Click += BtnAllRead_Click;
            BtnNewFile.Click += BtnNewFile_Click;
            BtnEdit.Click += BtnEdit_Click;
            BtnModPara.Click += BtnModPara_Click;
            BtnSavePara.Click += BtnSavePara_Click;
            BtnSelectAll.Click += BtnSelectAll_Click;
            BtnCancel.Click += BtnCancel_Click;
            BtnSelectNo.Click += BtnSelectNo_Click;
            ParaFileTree.AfterSelect += ParaFileTree_AfterSelect;
            TaskTime.Tick += TaskTime_Tick;
            FreshTime.Tick += FreshTime_Tick;
            Load += CEngPara_Load;
        }
    }
}
