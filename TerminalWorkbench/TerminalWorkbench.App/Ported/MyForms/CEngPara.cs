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
using Microsoft.VisualBasic;
namespace DtuTestManager
{
    public partial class CEngPara : Form
    {
        private ParaTaskMsg pNowTask = null;
        private List<ParaTaskMsg> m_TaskList = new List<ParaTaskMsg>();
        public static List<CParaCtrl> m_CurrentParaDataList = new List<CParaCtrl>();
        public static List<int> m_SelectParaIndex = new List<int>();
        public static string m_CurrentPath = "";
        public static CEngParaNewForm pform = null;
        public static CEngModPara pModParaForm = null;
        public List<string> m_FileName = new List<string>();
        public static string m_filePath => TerminalWorkbench.RuntimePaths.WorkspaceDirectory + "工程参数";
        public string m_SelectParaFileName = "";
        public bool IsRead = false;  //读参数 :true   写参数: false
        public CEngPara()
        {
            InitializeComponent();
        }
        private void CEngPara_Load(object sender, EventArgs e)
        {
            FreshTime.Stop();
            IniParaFile();
            IniParaLV();
            ParaFileTree.HideSelection = false;
            ParaFileTree.DrawMode = TreeViewDrawMode.OwnerDrawText;
            FreshTime.Start();
        }
        private void IniParaLV()
        {
            ParaLV.Columns.Add("选择", 80, HorizontalAlignment.Center);
            ParaLV.Columns.Add("序号", 80, HorizontalAlignment.Center);
            ParaLV.Columns.Add("参数名称", 250);
            ParaLV.Columns.Add("类型", 100, HorizontalAlignment.Center);
            ParaLV.Columns.Add("地址", 100, HorizontalAlignment.Center);
            ParaLV.Columns.Add("值", 100, HorizontalAlignment.Center);
            ParaLV.GridLines = true;
            ParaLV.FullRowSelect = true;
            ParaLV.View = View.Details;
            ParaLV.Scrollable = true;
            ParaLV.MultiSelect = true;
            ParaLV.HeaderStyle = ColumnHeaderStyle.Clickable;
            ParaLV.CheckBoxes = true;
            ParaLV.Dock = DockStyle.Fill;
        }
        public void IniParaFile()
        {
            int i = 0;
            if (Directory.Exists(m_filePath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(m_filePath);
            }
            try
            {
                string[] files = Directory.GetFiles(m_filePath);//得到文件列表
                foreach (string FileName in files)//循环文件
                {
                    if (FileName.LastIndexOf(".") != -1)
                    {

                        string strName = FileName.Substring(FileName.LastIndexOf("\\") + 1);
                        m_FileName.Add(strName);
                        TreeNode m_Node = new TreeNode();
                        m_Node.Text = strName;
                        m_Node.Tag = i++;
                        ParaFileTree.Nodes.Add(m_Node);
                    }
                }
            }
            catch
            {

            }
        }
        private void FreshLV()
        {
            ParaLV.Items.Clear();
            for (int i = 0; i < m_CurrentParaDataList.Count; i++)
            {
                string[] strHead = new string[6];

                strHead[1] = i.ToString();
                strHead[2] = m_CurrentParaDataList[i].m_ParaName;
                strHead[3] = m_CurrentParaDataList[i].GetValueTypeName();
                strHead[4] = m_CurrentParaDataList[i].m_ParaAddress.ToString() + "( " + Convert.ToString(m_CurrentParaDataList[i].m_ParaAddress, 16).ToUpper() + " )";
                strHead[5] = m_CurrentParaDataList[i].m_ParaValue.ToString();
                ListViewItem item = new ListViewItem(strHead);
                ParaLV.Items.Add(item);
            }
        }
        public void ReadFileAndDisPlay(string m_path)
        {

            StringBuilder temp = new StringBuilder(100);//存储读出ini内容变量
            CGlobal.GetPrivateProfileString("ParaInfo", "ParaCnt", "100", temp, 100, m_path);
            int ParaCnt = Convert.ToInt32(temp.ToString());
            string[] strHead = new string[6];
            m_CurrentParaDataList.Clear();
            ParaLV.Items.Clear();
            for (int i = 0; i < ParaCnt; i++)
            {
                while (CGlobal.GetPrivateProfileString("ParaInfo", "Para" + i.ToString(), "1", temp, temp.Capacity, m_path) == temp.Capacity - 1)
                {
                    temp.Capacity *= 2;
                }
                string[] m_Value = temp.ToString().Split(new[] { ',' }, 2);
                int Addr = Convert.ToInt32(m_Value[0]);
                string value = m_Value[1];
                CParaCtrl pPara = new CParaCtrl(CGlobal.m_Device.m_TotalParaDataList[Addr]);
                ;
                pPara.m_ParaValue = value;
                pPara.m_ParaAddress = Addr;
                pPara.m_Select = false;
                m_CurrentParaDataList.Add(pPara);

                strHead[1] = i.ToString();
                strHead[2] = pPara.m_ParaName;
                strHead[3] = pPara.GetValueTypeName();
                strHead[4] = pPara.m_ParaAddress.ToString() + "( " + Convert.ToString(pPara.m_ParaAddress, 16).ToUpper() + " )";
                strHead[5] = pPara.m_ParaValue.ToString();
                ListViewItem item = new ListViewItem(strHead);
                ParaLV.Items.Add(item);
            }
        }
        private void ParaFileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_SelectParaFileName = ParaFileTree.SelectedNode.Text;
            m_CurrentPath = m_filePath + "\\" + m_SelectParaFileName;
            ReadFileAndDisPlay(m_CurrentPath);

        }
        private void BtnAllDown_Click(object sender, EventArgs e)
        {
            IsRead = false;
            if (GetSelectPara(true))
            {
                // SavePara(true);
                GetParaOperationList(9, 20, true);
            }
        }
        private void BtnAllRead_Click(object sender, EventArgs e)
        {
            IsRead = true;
            if (GetSelectPara(false))
            {
                GetParaOperationList(8, 20);
            }
        }
        private void GetParaOperationList(int nTaskNo, int nGroupNo, bool bNoWait = false)
        {
            m_TaskList.Clear();
            if (CGlobal.m_Device.m_SelectParaList.Count == 0)
                return;
            TaskTime.Stop();
            int nMaxCount = CGlobal.m_Device.m_SelectParaList.Count;
            int nCroup = 0;
            while (nMaxCount > 0)
            {
                ParaTaskMsg pNewTask = new ParaTaskMsg();
                pNewTask.m_TaskNo = nTaskNo;
                pNewTask.m_AnswerFlag = bNoWait;
                pNewTask.m_StartPalce = nCroup * nGroupNo;
                if (nMaxCount >= nGroupNo)
                    pNewTask.m_ParaCount = nGroupNo;
                else
                    pNewTask.m_ParaCount = nMaxCount;
                pNewTask.m_EndFlag = false;
                m_TaskList.Add(pNewTask);
                nCroup++;
                nMaxCount -= pNewTask.m_ParaCount;
            }
            m_TaskList[m_TaskList.Count - 1].m_EndFlag = true;
            m_TaskList[m_TaskList.Count - 1].m_AnswerFlag = false;
            pNowTask = null;
            if (IsRead == false)
            {
                for (int i = 10; i < m_TaskList.Count; i++)
                {
                    if (i % 10 == 0)
                    {
                        m_TaskList[i].m_EndFlag = true;
                        m_TaskList[i].m_AnswerFlag = false;
                        i = i + 10;
                    }
                }
            }
            TaskTime.Start();
        }
        //参数操作
        private bool GetSelectPara(bool bRw)
        {
            CGlobal.m_Device.m_Ptr.Is_PriPara = true;
            CGlobal.m_Device.m_ParaHandle = this.Handle.ToInt32();
            CGlobal.m_Device.m_ParaFormType = 2;
            CGlobal.m_Device.m_SelectParaList.Clear();
            for (int i = 0; i < m_CurrentParaDataList.Count; i++)
            {
                CParaCtrl pPara = m_CurrentParaDataList[i];
                pPara.m_Select = true;
                if (bRw)
                {
                    if (ParaLV.Items[i].SubItems[5].Text.Length == 0)
                        ParaLV.Items[i].SubItems[5].Text = "0";
                    pPara.m_ParaValue = ParaLV.Items[i].SubItems[5].Text;
                }
                else
                {
                    ParaLV.Items[i].SubItems[5].Text = "";
                }
                CGlobal.m_Device.m_SelectParaList.Add(pPara);
            }
            if (CGlobal.m_Device.m_SelectParaList.Count > 0)
                return true;
            return false;
        }
        private void TaskTime_Tick(object sender, EventArgs e)
        {
            if (pNowTask != null)
            {
                if (pNowTask.m_AnswerFlag == true)
                    pNowTask = null;
            }
            else
            {
                if (m_TaskList.Count > 0)
                {
                    pNowTask = m_TaskList[0];
                    CGlobal.m_Device.m_Ptr.m_NowParaTask = pNowTask;
                    CGlobal.SendTaskOrder(pNowTask.m_TaskNo);
                    m_TaskList.RemoveAt(0);
                }
                else
                {
                    TaskTime.Stop();
                }
            }
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_1:
                    CGlobal.SendTaskOrder(10);
                    break;
                case UseMessage.MY_MSG_2:
                    if (m_TaskList.Count <= 0)
                    {
                        if (IsRead == false)
                        {
                            MessageBox.Show("下发参数成功");
                        }
                    }
                    break;
                case UseMessage.MY_MSG_3:
                    TaskTime.Stop();
                    MessageBox.Show("下发参数失败");
                    break;
            }
            base.WndProc(ref m);
        }


        private void BtnNewFile_Click(object sender, EventArgs e)
        {
            string strName = Interaction.InputBox("请输入新建装置名称", "名称", "", 100, 100);
            if (string.IsNullOrEmpty(strName) == false)
            {
                m_FileName.Add(strName);
                m_CurrentPath = m_filePath + "\\" + strName + ".ini";
                CGlobal.WritePrivateProfileString("ParaInfo", "ParaCnt", "0", m_CurrentPath);
                TreeNode m_Node = new TreeNode();
                m_Node.Text = strName + ".ini";
                m_Node.Tag = m_FileName.Count;
                ParaFileTree.Nodes.Add(m_Node);
                m_FileName.Add(strName);
                ParaFileTree.SelectedNode = m_Node;
                int i, j;
                for (i = 0; i < CGlobal.m_Device.m_ParaList.Count; i++)
                {
                    for (j = 0; j < CGlobal.m_Device.m_ParaList[i].m_GroupList.Count; j++)
                    {
                        CGlobal.m_Device.m_ParaList[i].m_GroupList[j].m_bShow = true;
                    }
                }
                pform = new CEngParaNewForm();
                pform.ShowDialog();
                if (pform == null)
                {
                    // ParaFileTree.SelectedNode.Checked = false;
                    // m_Node.Checked = true;

                    FreshLV();
                }
            }
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (ParaFileTree.SelectedNode == null)
            {
                MessageBox.Show("请先选择文件");
                return;
            }
            int i, j;
            for (i = 0; i < CGlobal.m_Device.m_ParaList.Count; i++)
            {
                for (j = 0; j < CGlobal.m_Device.m_ParaList[i].m_GroupList.Count; j++)
                {
                    CGlobal.m_Device.m_ParaList[i].m_GroupList[j].m_bShow = true;
                }
            }
            pform = new CEngParaNewForm();
            pform.ShowDialog();
            if (pform == null)
            {
                FreshLV();
            }
        }
        private void BtnModPara_Click(object sender, EventArgs e)
        {
            m_SelectParaIndex.Clear();
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                if (ParaLV.Items[i].Checked)
                {
                    int t = Convert.ToInt32(ParaLV.Items[i].SubItems[1].Text);
                    m_SelectParaIndex.Add(t);
                }
            }
            pModParaForm = new CEngModPara();
            pModParaForm.ShowDialog();
            if (pModParaForm == null)
            {
                FreshLVPara();
            }
        }
        private void BtnSavePara_Click(object sender, EventArgs e)
        {
            CGlobal.WritePrivateProfileString("ParaInfo", "ParaCnt", ParaLV.Items.Count.ToString(), m_CurrentPath);
            string m_Value;
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                int Addr = m_CurrentParaDataList[i].m_ParaAddress;
                string value = ParaLV.Items[i].SubItems[5].Text;
                m_Value = Addr.ToString() + "," + value;
                CGlobal.WritePrivateProfileString("ParaInfo", "Para" + i.ToString(), m_Value, CEngPara.m_CurrentPath);
            }
        }
        private void FreshLVPara()
        {
            for (int i = 0; i < m_SelectParaIndex.Count; i++)
            {
                ParaLV.Items[m_SelectParaIndex[i]].SubItems[5].Text = Convert.ToString(m_CurrentParaDataList[m_SelectParaIndex[i]].m_ParaValue);
            }
        }
        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            SetListViewSelectStatus(0);
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetListViewSelectStatus(1);
        }
        private void BtnSelectNo_Click(object sender, EventArgs e)
        {
            SetListViewSelectStatus(2);
        }
        private void SetListViewSelectStatus(int nSelectType)
        {
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                switch (nSelectType)
                {
                    case 0:
                        ParaLV.Items[i].Checked = true;
                        break;
                    case 1:
                        ParaLV.Items[i].Checked = false;
                        break;
                    case 2:
                        ParaLV.Items[i].Checked = !ParaLV.Items[i].Checked;
                        break;
                }
            }
        }

        private void FreshTime_Tick(object sender, EventArgs e)
        {
            if (CGlobal.m_Device.GetParaFreshStatus())
            {
                FreshPara();
            }
        }
        private void FreshPara()
        {

            for (int i = 0; i < m_CurrentParaDataList.Count; i++)
            {
                CParaCtrl pPara = m_CurrentParaDataList[i];
                if (pPara.m_Fresh)
                {
                    pPara.m_Fresh = false;
                    ParaLV.Items[i].SubItems[5].Text = pPara.m_ParaValue.ToString();
                    //  ParaLV.Items[i].Checked = pPara.m_Select;
                }
            }
        }

    }
}
