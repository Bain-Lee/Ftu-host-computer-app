using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CRealCtrlForm
    {
        private ToolStrip toolStrip1;
        private ToolStripButton Btn_StartOrder;
        private ToolStripButton Btn_StopOrder;
        private ToolStripButton Btn_HeartTest;
        private ToolStripButton Btn_CheckNo;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton Btn_CallTotalData;
        private ToolStripButton Btn_CallPowerData;
        private ToolStripButton Btn_SetTime;
        private MyListView LV_YX;
        private MyListView LV_YC;
        private MyListView LV_DD;
        private ImageList MyImageList;
        private ToolStripButton BtnCallTime;
        private ToolStripButton Btn_GetFrzData;
        private MyPanel myPanel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Timer timer1;
        private ToolStripButton BtnSetFixData;
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            Btn_StartOrder = new ToolStripButton();
            Btn_StopOrder = new ToolStripButton();
            Btn_HeartTest = new ToolStripButton();
            Btn_CheckNo = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            Btn_CallTotalData = new ToolStripButton();
            Btn_CallPowerData = new ToolStripButton();
            Btn_SetTime = new ToolStripButton();
            LV_YX = new MyListView();
            LV_YC = new MyListView();
            LV_DD = new MyListView();
            MyImageList = new ImageList();
            BtnCallTime = new ToolStripButton();
            Btn_GetFrzData = new ToolStripButton();
            myPanel1 = new MyPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            timer1 = new Timer();
            Own(timer1);
            BtnSetFixData = new ToolStripButton();
            tableLayoutPanel1.ColumnWidths.Add(new System.Windows.GridLength(36, System.Windows.GridUnitType.Star));
            tableLayoutPanel1.ColumnWidths.Add(new System.Windows.GridLength(32, System.Windows.GridUnitType.Star));
            tableLayoutPanel1.ColumnWidths.Add(new System.Windows.GridLength(32, System.Windows.GridUnitType.Star));
            tableLayoutPanel1.RowHeights.Add(new System.Windows.GridLength(100, System.Windows.GridUnitType.Star));
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.myPanel1.BackColor = System.Drawing.Color.LightCyan;
            this.myPanel1.Dock = DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(1249, 576);
            this.myPanel1.TabIndex = 3;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 56);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1249, 520);
            this.tableLayoutPanel1.TabIndex = 3;
            this.LV_YX.BackColor = System.Drawing.Color.Azure;
            this.LV_YX.Dock = DockStyle.Fill;
            this.LV_YX.Location = new System.Drawing.Point(3, 3);
            this.LV_YX.Name = "LV_YX";
            this.LV_YX.Size = new System.Drawing.Size(450, 514);
            this.LV_YX.TabIndex = 0;
            this.LV_DD.BackColor = System.Drawing.Color.Azure;
            this.LV_DD.Dock = DockStyle.Fill;
            this.LV_DD.Location = new System.Drawing.Point(858, 3);
            this.LV_DD.Name = "LV_DD";
            this.LV_DD.Size = new System.Drawing.Size(375, 514);
            this.LV_DD.TabIndex = 2;
            this.LV_YC.BackColor = System.Drawing.Color.Azure;
            this.LV_YC.Dock = DockStyle.Fill;
            this.LV_YC.Location = new System.Drawing.Point(467, 3);
            this.LV_YC.Name = "LV_YC";
            this.LV_YC.Size = new System.Drawing.Size(375, 514);
            this.LV_YC.TabIndex = 1;
            this.toolStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1249, 56);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.Btn_StartOrder.Name = "Btn_StartOrder";
            this.Btn_StartOrder.Size = new System.Drawing.Size(60, 53);
            this.Btn_StartOrder.Text = "启动命令";
            this.Btn_StopOrder.Name = "Btn_StopOrder";
            this.Btn_StopOrder.Size = new System.Drawing.Size(60, 53);
            this.Btn_StopOrder.Text = "停止命令";
            this.Btn_HeartTest.Name = "Btn_HeartTest";
            this.Btn_HeartTest.Size = new System.Drawing.Size(60, 53);
            this.Btn_HeartTest.Text = "通讯测试";
            this.Btn_CheckNo.Name = "Btn_CheckNo";
            this.Btn_CheckNo.Size = new System.Drawing.Size(60, 53);
            this.Btn_CheckNo.Text = "核对序号";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            this.Btn_SetTime.Name = "Btn_SetTime";
            this.Btn_SetTime.Size = new System.Drawing.Size(60, 53);
            this.Btn_SetTime.Text = "时钟对时";
            this.BtnCallTime.Name = "BtnCallTime";
            this.BtnCallTime.Size = new System.Drawing.Size(60, 53);
            this.BtnCallTime.Text = "召唤时钟";
            this.Btn_CallTotalData.Name = "Btn_CallTotalData";
            this.Btn_CallTotalData.Size = new System.Drawing.Size(60, 53);
            this.Btn_CallTotalData.Text = "数据总召";
            this.Btn_CallPowerData.Name = "Btn_CallPowerData";
            this.Btn_CallPowerData.Size = new System.Drawing.Size(60, 53);
            this.Btn_CallPowerData.Text = "召读电度";
            this.Btn_GetFrzData.Name = "Btn_GetFrzData";
            this.Btn_GetFrzData.Size = new System.Drawing.Size(60, 53);
            this.Btn_GetFrzData.Text = "瞬时冻结";
            this.BtnSetFixData.Name = "BtnSetFixData";
            this.BtnSetFixData.Size = new System.Drawing.Size(60, 53);
            this.BtnSetFixData.Text = "置数功能";
            this.ClientSize = new System.Drawing.Size(1249, 576);
            this.Name = "CRealCtrlForm";
            this.Text = "实时监控";
            this.myPanel1.Controls.Add(this.tableLayoutPanel1);
            this.myPanel1.Controls.Add(this.toolStrip1);
            this.tableLayoutPanel1.Controls.Add(this.LV_YX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LV_DD, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.LV_YC, 1, 0);
            this.toolStrip1.Items.AddRange(new Control[] {
            this.Btn_StartOrder,
            this.Btn_StopOrder,
            this.Btn_HeartTest,
            this.Btn_CheckNo,
            this.toolStripSeparator1,
            this.Btn_SetTime,
            this.BtnCallTime,
            this.Btn_CallTotalData,
            this.Btn_CallPowerData,
            this.Btn_GetFrzData,
            this.BtnSetFixData});
            this.Controls.Add(this.myPanel1);
            timer1.Tick += timer1_Tick;
            Btn_StartOrder.Click += Btn_StartOrder_Click;
            Btn_StopOrder.Click += Btn_StopOrder_Click;
            Btn_HeartTest.Click += Btn_HeartTest_Click;
            Btn_CheckNo.Click += Btn_CheckNo_Click;
            Btn_SetTime.Click += Btn_SetTime_Click;
            BtnCallTime.Click += BtnCallTime_Click;
            Btn_CallTotalData.Click += Btn_CallTotalData_Click;
            Btn_CallPowerData.Click += Btn_CallPowerData_Click;
            Btn_GetFrzData.Click += Btn_GetFrzData_Click;
            BtnSetFixData.Click += BtnSetFixData_Click;
            Load += CRealCtrlForm_Load;
        }
    }
}
