namespace DtuTestManager
{
    partial class CEngModPara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEngModPara));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnSave = new System.Windows.Forms.ToolStripButton();
            this.ParaLV = new DtuTestManager.MyListView(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(811, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnSave
            // 
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(52, 22);
            this.BtnSave.Text = "保存";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ParaLV
            // 
            this.ParaLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaLV.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaLV.FullRowSelect = true;
            this.ParaLV.GridLines = true;
            this.ParaLV.Location = new System.Drawing.Point(0, 25);
            this.ParaLV.Name = "ParaLV";
            this.ParaLV.OwnerDraw = true;
            this.ParaLV.ProgressColumnIndex = -1;
            this.ParaLV.Size = new System.Drawing.Size(811, 222);
            this.ParaLV.TabIndex = 2;
            this.ParaLV.UseCompatibleStateImageBehavior = false;
            this.ParaLV.View = System.Windows.Forms.View.Details;
            // 
            // CEngModPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 247);
            this.Controls.Add(this.ParaLV);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CEngModPara";
            this.Text = "参数配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CEngModPara_FormClosing);
            this.Load += new System.EventHandler(this.CEngParaNewForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnSave;
        private MyListView ParaLV;
    }
}