using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;
using System.IO;
namespace DtuTestManager
{
    public partial class CSFTPForm : Form
    {

        public string m_SelPath = "";
        public SFTPHelper test;
        public CSFTPForm()
        {
            InitializeComponent();
        }
        //下载
        private void button3_Click(object sender, EventArgs e)
        {
            test = new SFTPHelper(textBoxIP.Text, textBoxPortNum.Text, textBoxUserName.Text, textBoxPassWord.Text);
            if (test.Connect() == false)
            {
                button4.Enabled = true;
                label5.Text = "连接失败";
                return;
            }
            label5.Text = "正在下载中，请稍后......";
            string localPath = m_SelPath;
            string filePath = "/root";
            string fileName0 = "MainCfgBase.db";
            string fileName1 = "UserCfgBase.db";
            string ftpServerIP = textBoxIP.Text;
            string ftpPort = textBoxPortNum.Text;
            string ftpUserID = textBoxUserName.Text;
            string ftpPassword = textBoxPassWord.Text;
            CTransForm.pSelStation.m_MainSqlite.CloseDatabase();
            CTransForm.pSelStation.m_UserSqlite.CloseDatabase();
            string FileFullName0 = localPath + "\\" + fileName0;

            if (test.DownloadFtp(filePath, localPath, fileName0, ftpServerIP, ftpPort, ftpUserID, ftpPassword) != 0)
            {
                label5.Text = "下载失败，请检查连接参数是否正确！";
                return;
            }
            string FileFullName1 = localPath + "\\" + fileName1;

            test.DownloadFtp(filePath, localPath, fileName1, ftpServerIP, ftpPort, ftpUserID, ftpPassword);
            CTransForm.pSelStation.m_MainSqlite.ConnectDataBase(CTransForm.pSelStation.m_Sqlite_Main);
            CTransForm.pSelStation.m_UserSqlite.ConnectDataBase(CTransForm.pSelStation.m_Sqlite_User);
            label5.Text = "下载完成";
            CTransForm.pSelStation.m_IsEmpty = true;
            button4.Enabled = true;
        }
        //上传
        private void button4_Click(object sender, EventArgs e)
        {
            test = new SFTPHelper(textBoxIP.Text, textBoxPortNum.Text, textBoxUserName.Text, textBoxPassWord.Text);
            if (test.Connect() == false)
            {
                label5.Text = "连接失败";
                return;
            }
            label5.Text = "文件正在上传，请稍后......";
            string ftpServerIP = textBoxIP.Text;
            string ftpPort = textBoxPortNum.Text;
            string ftpUserID = textBoxUserName.Text;
            string ftpPassword = textBoxPassWord.Text;
            CTransForm.pSelStation.m_MainSqlite.CloseDatabase();
            CTransForm.pSelStation.m_UserSqlite.CloseDatabase();

            string localPath = m_SelPath + "\\" + "MainCfgBase.db";
            string remotePath = "/root" + "/" + "MainCfgBase.db";

            test.Put(localPath, remotePath);

            localPath = m_SelPath + "\\" + "UserCfgBase.db";
            remotePath = "/root" + "/" + "UserCfgBase.db";

            test.Put(localPath, remotePath);
            CTransForm.pSelStation.m_MainSqlite.ConnectDataBase(CTransForm.pSelStation.m_Sqlite_Main);
            CTransForm.pSelStation.m_UserSqlite.ConnectDataBase(CTransForm.pSelStation.m_Sqlite_User);
            label5.Text = "文件上传成功";
        }
        private void CSFTPForm_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            textBoxIP.Text = "192.168.11.15";
            textBoxUserName.Text = "root";
            textBoxPassWord.Text = "root";
            textBoxPortNum.Text = "22";
            if ((File.Exists(m_SelPath + "\\" + "MainCfgBase.db")) && (File.Exists(m_SelPath + "\\" + "UserCfgBase.db")))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }
    }
}
