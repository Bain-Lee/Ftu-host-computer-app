using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CDownFileForm
    {
        private TextBox FilePath;
        private Label Label1;
        private Button BtnOpenFile;
        private ProgressBar FileDownMsg;
        private Button BtnStartDown;
        private Label FpbValue;
        private Timer DownFileTime;
        private Button BtnStartUpdate;
        private Label DownResult;
        private void InitializeComponent()
        {
            FilePath = new TextBox();
            Label1 = new Label();
            BtnOpenFile = new Button();
            FileDownMsg = new ProgressBar();
            BtnStartDown = new Button();
            FpbValue = new Label();
            DownFileTime = new Timer();
            Own(DownFileTime);
            BtnStartUpdate = new Button();
            DownResult = new Label();
            this.FilePath.Location = new System.Drawing.Point(93, 13);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(612, 21);
            this.FilePath.TabIndex = 0;
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(65, 12);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "选择文件：";
            this.BtnOpenFile.BackColor = System.Drawing.Color.Azure;
            this.BtnOpenFile.Location = new System.Drawing.Point(723, 11);
            this.BtnOpenFile.Name = "BtnOpenFile";
            this.BtnOpenFile.Size = new System.Drawing.Size(82, 23);
            this.BtnOpenFile.TabIndex = 2;
            this.BtnOpenFile.Text = "浏览...";
            this.FileDownMsg.Location = new System.Drawing.Point(93, 63);
            this.FileDownMsg.Name = "FileDownMsg";
            this.FileDownMsg.Size = new System.Drawing.Size(612, 23);
            this.FileDownMsg.TabIndex = 3;
            this.BtnStartDown.BackColor = System.Drawing.Color.Azure;
            this.BtnStartDown.Enabled = false;
            this.BtnStartDown.Location = new System.Drawing.Point(5, 63);
            this.BtnStartDown.Name = "BtnStartDown";
            this.BtnStartDown.Size = new System.Drawing.Size(82, 23);
            this.BtnStartDown.TabIndex = 2;
            this.BtnStartDown.Text = "开始传输";
            this.FpbValue.AutoSize = true;
            this.FpbValue.Location = new System.Drawing.Point(723, 67);
            this.FpbValue.Name = "FpbValue";
            this.FpbValue.Size = new System.Drawing.Size(41, 12);
            this.FpbValue.TabIndex = 4;
            this.FpbValue.Text = "进度0%";
            this.DownFileTime.Interval = 20;
            this.BtnStartUpdate.BackColor = System.Drawing.Color.Azure;
            this.BtnStartUpdate.Location = new System.Drawing.Point(348, 118);
            this.BtnStartUpdate.Name = "BtnStartUpdate";
            this.BtnStartUpdate.Size = new System.Drawing.Size(82, 23);
            this.BtnStartUpdate.TabIndex = 2;
            this.BtnStartUpdate.Text = "启动升级";
            this.DownResult.AutoSize = true;
            this.DownResult.Location = new System.Drawing.Point(93, 118);
            this.DownResult.Name = "DownResult";
            this.DownResult.Size = new System.Drawing.Size(0, 12);
            this.DownResult.TabIndex = 5;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(843, 181);
            this.Name = "CDownFileForm";
            this.Text = "文件传输";
            this.Controls.Add(this.DownResult);
            this.Controls.Add(this.FpbValue);
            this.Controls.Add(this.FileDownMsg);
            this.Controls.Add(this.BtnStartUpdate);
            this.Controls.Add(this.BtnStartDown);
            this.Controls.Add(this.BtnOpenFile);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.FilePath);
            BtnOpenFile.Click += BtnOpenFile_Click;
            BtnStartDown.Click += BtnStartDown_Click;
            DownFileTime.Tick += DownFileTime_Tick;
            BtnStartUpdate.Click += BtnStartUpdate_Click;
            FormClosed += CDownFileForm_FormClosed;
            Load += CDownFileForm_Load;
        }
    }
}
