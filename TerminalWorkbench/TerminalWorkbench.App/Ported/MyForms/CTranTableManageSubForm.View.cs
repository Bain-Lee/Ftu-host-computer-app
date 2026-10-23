using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CTranTableManageSubForm
    {
        private ToolStrip toolStrip1;
        private MyListView DataListView;
        private ToolStripButton BtnAdd;
        private ToolStripButton BtnDelete;
        private ToolStripButton BtnClear;
        private ToolStripButton BtnSave;
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            DataListView = new MyListView();
            BtnAdd = new ToolStripButton();
            BtnDelete = new ToolStripButton();
            BtnClear = new ToolStripButton();
            BtnSave = new ToolStripButton();
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(933, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(52, 22);
            this.BtnAdd.Text = "添加";
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(52, 22);
            this.BtnDelete.Text = "删除";
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(52, 22);
            this.BtnClear.Text = "清空";
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(52, 22);
            this.BtnSave.Text = "保存";
            this.DataListView.BackColor = System.Drawing.Color.Lavender;
            this.DataListView.Dock = DockStyle.Fill;
            this.DataListView.Location = new System.Drawing.Point(0, 25);
            this.DataListView.Name = "DataListView";
            this.DataListView.Size = new System.Drawing.Size(933, 433);
            this.DataListView.TabIndex = 1;
            this.ClientSize = new System.Drawing.Size(933, 458);
            this.Name = "CTranTableManageSubForm";
            this.Text = "数据表管理";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.BtnAdd,
            this.BtnDelete,
            this.BtnClear,
            this.BtnSave});
            this.Controls.Add(this.DataListView);
            this.Controls.Add(this.toolStrip1);
            BtnAdd.Click += BtnAdd_Click;
            BtnDelete.Click += BtnDelete_Click;
            BtnClear.Click += BtnClear_Click;
            BtnSave.Click += BtnSave_Click;
            DataListView.SelectedIndexChanged += DataListView_SelectedIndexChanged;
            DataListView.DoubleClick += DataListView_DoubleClick;
            FormClosing += CTranTableManageSubForm_FormClosing;
            Load += CTranTableManageSubForm_Load;
        }
    }
}
