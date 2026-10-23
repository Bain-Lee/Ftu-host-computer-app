using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public partial class CTtestCaseForm : Form
    {
        private int m_CaseTaskNo = -1;
        private int m_RowIndex = -1;
        public CTtestCaseForm()
        {
            InitializeComponent();
        }
        private void IniSetListViewMode()
        {
            TestCaseView.ClearAllItem();
            TestCaseView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            TestCaseView.Columns.Add("用例名称", 160, HorizontalAlignment.Center);
            TestCaseView.Columns.Add("测试项目", 300, HorizontalAlignment.Center);
            TestCaseView.Columns.Add("测试结果", 120, HorizontalAlignment.Center);

            TestCaseView.GridLines = true;
            TestCaseView.FullRowSelect = true;
            TestCaseView.View = View.Details;
            TestCaseView.Scrollable = true;
            TestCaseView.MultiSelect = false;
            TestCaseView.HeaderStyle = ColumnHeaderStyle.Clickable;
            TestCaseView.Visible = true;
            TestCaseView.CheckBoxes = false;


            int i;
            string[] strHead = new string[4];
            for (i = 0; i < CGlobal.m_Device.m_TestCaseList.Count; i++)
            {
                strHead[0] = (i + 1).ToString();
                strHead[1] = CGlobal.m_Device.m_TestCaseList[i].m_TestCaseName;
                strHead[2] = CGlobal.m_Device.m_TestCaseList[i].m_ItemList[0].m_CaseName;
                strHead[3] = "";
                ListViewItem pItem = new ListViewItem(strHead);
                TestCaseView.AddItem(pItem);
                TestCaseView.AddComBoxListStr(CGlobal.m_Device.m_TestCaseList[i].m_ItemNameList.ToArray());
                TestCaseView.SetCellMode(i, 2, 2, i);
            }
        }
        private void CTtestCaseForm_Load(object sender, EventArgs e)
        {
            IniSetListViewMode();
            TestCaseView.theProcessFun = new MyListView.ProcessDelegate(SetNowSelectValue);
        }
        private void SetNowSelectValue(int nIndex, object pStrContent)
        {
            if (m_RowIndex >= 0 && m_RowIndex < CGlobal.m_Device.m_TestCaseList.Count)
            {
                CGlobal.m_Device.m_TestCaseList[m_RowIndex].SetSelIndex((string)pStrContent);
                m_CaseTaskNo = CGlobal.m_Device.m_TestCaseList[m_RowIndex].m_CaseTaskNo;
            }
        }
        private void CTtestCaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void BtnConnectDev_Click(object sender, EventArgs e)
        {
            CGlobal.m_RunSendMode = false;
            if (CGlobal.m_Device.m_bOnlineStatus == false)
            {
                if (CGlobal.m_Device.OpenConnChannel())
                {
                    CGlobal.m_Device.IniCreateProcessThread();
                    BtnConnectDev.Text = "断开连接";

                }
            }
            else
            {
                CGlobal.m_Device.CloseThread();
                CGlobal.m_SecretEncode.CloseEncode();
                BtnConnectDev.Text = "连接装置";
            }
        }

        private void BtnSendOrder_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(m_CaseTaskNo);
        }

        private void TestCaseView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TestCaseView.SelectedIndices != null && TestCaseView.SelectedIndices.Count > 0)
            {
                try
                {
                    m_RowIndex = TestCaseView.SelectedItems[0].Index;
                }
                catch { }
            }
        }
    }
}
