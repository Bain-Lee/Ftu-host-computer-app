using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Renci.SshNet;
using System.Collections;
namespace DtuTestManager
{
   
    public partial class CSFTPMainForm : Form
    {
        public SFTPHelper test;
        public string LocalPath = "";//本地路径
        public string RemotePath = "/root";//装置路径
        public List<SFTPFile> m_LocalList = new List<SFTPFile>();
        public List<SFTPFile> m_RomoteList = new List<SFTPFile>();
        public SFTPFile m_SelFile;//当前所选文件
        public string SourceDir = "";//源路径
        public string DestDir = "";//目标路径
        public string    m_Status = "";  //状态false 下装，true 上装
        public int m_SelFileSize = 0;//文件大小        
        public CSFTPMainForm()
        {
            InitializeComponent();
        }
        private void CSFTPMainForm_Load(object sender, EventArgs e)
        {           
            InitLV1();
            InitLV2();
            InitLV3();
            RemoteFilePath.Text = RemotePath;
            GetALlLV2Files();
        }
        private void  InitLV1()
        {           
            LV1.Items.Clear();
            LV1.Columns.Clear();
            LV1.Columns.Add("LocalName", 200, HorizontalAlignment.Center);
            LV1.Columns.Add("Type", 80, HorizontalAlignment.Center);
            LV1.Columns.Add("Size", 80, HorizontalAlignment.Center);          
            LV1.GridLines = false;
            LV1.FullRowSelect = true;
            LV1.View = View.Details;
            LV1.Scrollable = true;
            LV1.MultiSelect = false;
            LV1.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV1.Visible = true;         
        }      
        private void InitLV2()
        {
            LV2.Items.Clear();
            LV2.Columns.Clear();
            LV2.Columns.Add("RemoteName", 200, HorizontalAlignment.Center);
            LV2.Columns.Add("Type", 80, HorizontalAlignment.Center);
            LV2.Columns.Add("Size", 80, HorizontalAlignment.Center);
            LV2.GridLines = false;
            LV2.FullRowSelect = true;
            LV2.View = View.Details;
            LV2.Scrollable = true;
            LV2.MultiSelect = false;
            LV2.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV2.Visible = true;
            string[] h = new string[3];           
        }     
        private void InitLV3()
        {          
            LV3.Items.Clear();
            LV3.Columns.Clear();
            LV3.Columns.Add("文件名", 200, HorizontalAlignment.Center);
            LV3.Columns.Add("源路径", 250, HorizontalAlignment.Center);
            LV3.Columns.Add("目标路径", 250, HorizontalAlignment.Center);
            LV3.Columns.Add("状态", 80, HorizontalAlignment.Center);
            LV3.Columns.Add("进度", 80, HorizontalAlignment.Center);
            LV3.GridLines = false;
            LV3.FullRowSelect = true;
            LV3.View = View.Details;
            LV3.Scrollable = true;
            LV3.MultiSelect = false;
            LV3.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV3.Visible = true;          
        }
        private void FreshLV3()
        {
            string[] h = new string[5];
            h[0] = m_SelFile.FileName;
            h[1] = SourceDir;
            h[2] = DestDir;
            h[3] = m_Status;
            h[4] = " 进行中 ";
            ListViewItem item = new ListViewItem(h);
            LV3.Items.Add(item);
        }
        private void GetALlLV1Files()
        {
            if (LocalPath == "") return;
            m_LocalList.Clear();
            DirectoryInfo root = new DirectoryInfo(LocalPath);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                SFTPFile temp = new SFTPFile();
                temp.FileName = d.Name;
                temp.Type = "文件夹";
                temp.Size = "";
                m_LocalList.Add(temp);
            }
            foreach (FileInfo f in root.GetFiles())
            {
                SFTPFile temp = new SFTPFile();
                temp.FileName = f.Name;
                temp.Type = "文件";
                temp.Size = f.Length.ToString();
                m_LocalList.Add(temp);
            }
            LV1.Items.Clear();
            string[] h = new string[3];
            for (int i = 0; i < m_LocalList.Count; i++)
            {
                h[0] = m_LocalList[i].FileName;
                h[1] = m_LocalList[i].Type;
                h[2] = m_LocalList[i].Size;
                ListViewItem item = new ListViewItem(h);
                LV1.Items.Add(item);
            }       
        }
        private void GetALlLV2Files()
        {
            m_RomoteList = test.GetFileList(RemotePath);
            LV2.Items.Clear();
            string[] h = new string[3];
            for (int i = 0; i < m_RomoteList.Count; i++)
            {
                h[0] = m_RomoteList[i].FileName;
                h[1] = m_RomoteList[i].Type;
                h[2] = m_RomoteList[i].Size;
                ListViewItem item = new ListViewItem(h);
                LV2.Items.Add(item);
            }          
        }
        //浏览
        private void button1_Click(object sender, EventArgs e)
        {
           
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath.Text = dialog.SelectedPath;
                LocalPath = FilePath.Text;
                try
                {
                    GetALlLV1Files();                  
                }
                catch
                {
                    MessageBox.Show("文件打开出错", "提示");
                }
            }
        }
        //上传
        private void button2_Click(object sender, EventArgs e)
        {
            if (m_SelFile == null) return;
            SourceDir = LocalPath;
            DestDir = RemotePath;
            m_Status = "上传";
            FreshLV3();
            if (test.Put(LocalPath + "\\" + m_SelFile.FileName, RemotePath + "/" + m_SelFile.FileName))
            {
                LV3.Items[LV3.Items.Count - 1].SubItems[4].Text = "上传成功";
            }
            else
            {
                LV3.Items[LV3.Items.Count - 1].SubItems[4].Text = "上传失败";
            }
            GetALlLV2Files();           
        }
        //下载
        private void button3_Click(object sender, EventArgs e)
        {
            if (m_SelFile == null) return;
            DestDir = LocalPath;
            SourceDir = RemotePath;
            m_Status = "下载";
            FreshLV3();
            if (test.Get(RemotePath + "/" + m_SelFile.FileName, LocalPath + "\\" + m_SelFile.FileName))
            {
                LV3.Items[LV3.Items.Count - 1].SubItems[4].Text = "下载成功";
            }
            else
            {
                LV3.Items[LV3.Items.Count - 1].SubItems[4].Text = "下载失败";
            }
            GetALlLV1Files();
        }
        //清除列表
        private void button4_Click(object sender, EventArgs e)
        {
            LV3.Items.Clear();
        }
        private void LV1_Click(object sender, EventArgs e)
        {
            if (LV1.SelectedItems[0].SubItems[1].Text == "文件夹") return;
            m_SelFile = new SFTPFile();
            m_SelFile.FileName = LV1.SelectedItems[0].SubItems[0].Text;
            m_SelFile.Type = LV1.SelectedItems[0].SubItems[1].Text;
            m_SelFile.Size = LV1.SelectedItems[0].SubItems[2].Text;
        }
        private void LV2_Click(object sender, EventArgs e)
        {
            if (LV2.SelectedItems[0].SubItems[1].Text == "文件夹") return;
            m_SelFile = new SFTPFile();
            m_SelFile.FileName = LV2.SelectedItems[0].SubItems[0].Text;
            m_SelFile.Type = LV2.SelectedItems[0].SubItems[1].Text;
            m_SelFile.Size = LV2.SelectedItems[0].SubItems[2].Text;
        }
        private void LV1_DoubleClick(object sender, EventArgs e)
        {
            if (LV1.SelectedItems[0].SubItems[1].Text == "文件") return;
            FilePath.Text = LocalPath + "\\"+LV1.SelectedItems[0].SubItems[0].Text;
            LocalPath = FilePath.Text;
            GetALlLV1Files();
        }   
        private void LV2_DoubleClick(object sender, EventArgs e)
        {
            if (LV2.SelectedItems[0].SubItems[1].Text == "文件") return;
            RemoteFilePath.Text = RemotePath + "/" + LV2.SelectedItems[0].SubItems[0].Text;
            RemotePath = RemoteFilePath.Text;
            GetALlLV2Files();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (test.Delete(RemotePath + "/" + m_SelFile.FileName))
            {
                GetALlLV2Files();
            }
        }

        private void CSFTPMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            test.Disconnect();
            test = null;
        }
    }
}
