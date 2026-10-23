namespace DtuTestManager
{
    partial class CLineRealForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLineRealForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LineSetView = new DtuTestManager.MyListView(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ClearZero = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.AddSpaceCount = new System.Windows.Forms.ToolStripTextBox();
            this.FastSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.DC1Index = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.DC2Index = new System.Windows.Forms.ToolStripTextBox();
            this.SaveSet = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LineSetView);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 428);
            this.panel1.TabIndex = 0;
            // 
            // LineSetView
            // 
            this.LineSetView.BackColor = System.Drawing.Color.Azure;
            this.LineSetView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineSetView.FullRowSelect = true;
            this.LineSetView.GridLines = true;
            this.LineSetView.Location = new System.Drawing.Point(0, 25);
            this.LineSetView.Name = "LineSetView";
            this.LineSetView.OwnerDraw = true;
            this.LineSetView.ProgressColumnIndex = -1;
            this.LineSetView.Size = new System.Drawing.Size(881, 403);
            this.LineSetView.TabIndex = 0;
            this.LineSetView.UseCompatibleStateImageBehavior = false;
            this.LineSetView.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearZero,
            this.toolStripLabel1,
            this.AddSpaceCount,
            this.FastSet,
            this.toolStripLabel2,
            this.DC1Index,
            this.toolStripLabel3,
            this.DC2Index,
            this.SaveSet});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(881, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ClearZero
            // 
            this.ClearZero.Image = ((System.Drawing.Image)(resources.GetObject("ClearZero.Image")));
            this.ClearZero.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearZero.Name = "ClearZero";
            this.ClearZero.Size = new System.Drawing.Size(52, 22);
            this.ClearZero.Text = "清零";
            this.ClearZero.Click += new System.EventHandler(this.ClearZero_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel1.Text = "    累加间隔：";
            // 
            // AddSpaceCount
            // 
            this.AddSpaceCount.Name = "AddSpaceCount";
            this.AddSpaceCount.Size = new System.Drawing.Size(100, 25);
            this.AddSpaceCount.Text = "18";
            // 
            // FastSet
            // 
            this.FastSet.Image = ((System.Drawing.Image)(resources.GetObject("FastSet.Image")));
            this.FastSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FastSet.Name = "FastSet";
            this.FastSet.Size = new System.Drawing.Size(76, 22);
            this.FastSet.Text = "快速设置";
            this.FastSet.Click += new System.EventHandler(this.FastSet_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel2.Text = "直流1索引：";
            // 
            // DC1Index
            // 
            this.DC1Index.Name = "DC1Index";
            this.DC1Index.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel3.Text = "直流2索引：";
            // 
            // DC2Index
            // 
            this.DC2Index.Name = "DC2Index";
            this.DC2Index.Size = new System.Drawing.Size(100, 25);
            // 
            // SaveSet
            // 
            this.SaveSet.Image = ((System.Drawing.Image)(resources.GetObject("SaveSet.Image")));
            this.SaveSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveSet.Name = "SaveSet";
            this.SaveSet.Size = new System.Drawing.Size(52, 22);
            this.SaveSet.Text = "保存";
            this.SaveSet.Click += new System.EventHandler(this.SaveSet_Click);
            // 
            // CLineRealForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(881, 428);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CLineRealForm";
            this.Text = "CLineRealForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CLineRealForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyListView LineSetView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ClearZero;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox AddSpaceCount;
        private System.Windows.Forms.ToolStripButton FastSet;
        private System.Windows.Forms.ToolStripButton SaveSet;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox DC1Index;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox DC2Index;
    }
}