using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;     //浮动窗口

namespace DtuTestManager
{
    public partial class CEventMsgForm : DockContent
    {
        private int nEventMaxNo = 0;
        public CEventMsgForm()
        {
            InitializeComponent();
        }
        private void CEventMsgForm_Load(object sender, EventArgs e)
        {
            Init_LV_Event();           
        }
        public void Init_LV_Event()
        {
            Event_View.Items.Clear();
            Event_View.Columns.Clear();
            Event_View.Columns.Add("序号", 60, HorizontalAlignment.Left);
            Event_View.Columns.Add("事件描述", 3000, HorizontalAlignment.Left);
            this.Event_View.View = View.Details;
            Event_View.GridLines = true;
            Event_View.Scrollable = true;
            Event_View.Visible = true;           
        }
        public  void FreshEventView()
        {
            string pNewData = CGlobal.m_Device.m_EventClass.GetNewestMsg();
            while  (pNewData != null)
            {
                string[] RowContent = new string[2];
                RowContent[0] = (nEventMaxNo + 1).ToString();
                RowContent[1] = pNewData;
                nEventMaxNo++;
                ListViewItem item = new ListViewItem(RowContent);
                Event_View.Items.Insert(0, item);
                pNewData = CGlobal.m_Device.m_EventClass.GetNewestMsg();
            } 
        }     
        private void BtnClear_Click(object sender, EventArgs e)
        {
            Event_View.Items.Clear();
        }
        private void CEventMsgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CGlobal.m_EventView = null;
            bool flag1 = (CGlobal.m_BufferView == null || CGlobal.m_BufferView.IsDisposed == true);
            bool flag2 = (CGlobal.m_SOEView == null || CGlobal.m_SOEView.IsDisposed == true);          
            if (flag2 && flag1)
            {
                CGlobal.PostMessage(CGlobal.m_Device.m_MainFormHandle, UseMessage.MY_MSG_1, 0, 0);
            }
        }
    }
}
