using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CFTUFastUpdate
    {
        private Label FpbValue;
        private TextBox FilePath;
        private Button button1;
        private ProgressBar FileDownMsg;
        private Label label2;
        private Button BtnStartDown;
        private Button BtnEndDown;
        private Label label3;
        private Timer timer1;
        private void InitializeComponent()
        {
            FpbValue = new Label();
            FilePath = new TextBox();
            button1 = new Button();
            FileDownMsg = new ProgressBar();
            label2 = new Label();
            BtnStartDown = new Button();
            BtnEndDown = new Button();
            label3 = new Label();
            timer1 = new Timer();
            Own(timer1);
            this.FpbValue.AutoSize = true;
            this.FpbValue.Location = new System.Drawing.Point(628, 156);
            this.FpbValue.Name = "FpbValue";
            this.FpbValue.Size = new System.Drawing.Size(41, 12);
            this.FpbValue.TabIndex = 0;
            this.FpbValue.Text = "进度0%";
            this.FilePath.Location = new System.Drawing.Point(135, 68);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(466, 21);
            this.FilePath.TabIndex = 1;
            this.button1.Location = new System.Drawing.Point(607, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "浏览...";
            this.FileDownMsg.Location = new System.Drawing.Point(135, 151);
            this.FileDownMsg.Name = "FileDownMsg";
            this.FileDownMsg.Size = new System.Drawing.Size(466, 23);
            this.FileDownMsg.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "升级文件：";
            this.BtnStartDown.Enabled = false;
            this.BtnStartDown.Location = new System.Drawing.Point(701, 66);
            this.BtnStartDown.Name = "BtnStartDown";
            this.BtnStartDown.Size = new System.Drawing.Size(75, 23);
            this.BtnStartDown.TabIndex = 2;
            this.BtnStartDown.Text = "开始升级";
            this.BtnEndDown.Enabled = false;
            this.BtnEndDown.Location = new System.Drawing.Point(701, 151);
            this.BtnEndDown.Name = "BtnEndDown";
            this.BtnEndDown.Size = new System.Drawing.Size(75, 23);
            this.BtnEndDown.TabIndex = 2;
            this.BtnEndDown.Text = "停止升级";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 0;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(839, 226);
            this.Name = "CFTUFastUpdate";
            this.Text = "CFTUFastUpdate";
            this.Controls.Add(this.FileDownMsg);
            this.Controls.Add(this.BtnEndDown);
            this.Controls.Add(this.BtnStartDown);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FpbValue);
            button1.Click += button1_Click;
            BtnStartDown.Click += BtnStartDown_Click;
            timer1.Tick += timer1_Tick;
            FormClosed += CFTUFastUpdate_FormClosed;
            Load += CFTUFastUpdate_Load;
        }
    }
}
