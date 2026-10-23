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
    public partial class CSOEMsgForm : DockContent
    {
        private int nSOEMaxNo = 0;       
        public CSOEMsgForm()
        {
            InitializeComponent();           
        }
        private void CSOEMsgForm_Load(object sender, EventArgs e)
        {
            Init_LV_SOE();   
        }
        public void Init_LV_SOE()
        {
            try
            {
                SOE_View.Items.Clear();
                SOE_View.Columns.Clear();
                SOE_View.Columns.Add("序号", 60, HorizontalAlignment.Left);
                SOE_View.Columns.Add("遥信号", 60, HorizontalAlignment.Left);
                SOE_View.Columns.Add("遥信名称", 160, HorizontalAlignment.Left);
                SOE_View.Columns.Add("遥信值", 60, HorizontalAlignment.Left);
                SOE_View.Columns.Add("记录时间", 160, HorizontalAlignment.Left);
                SOE_View.Columns.Add("SOE时间", 160, HorizontalAlignment.Left);
                SOE_View.Columns.Add("状态描述", 120, HorizontalAlignment.Left);
                this.SOE_View.View = View.Details;
                SOE_View.GridLines = true;
                SOE_View.Scrollable = true;
                SOE_View.Visible = true;
            }
            catch
            { }
        }   
        public void  FreshSOEView()
        {
            CSOEOption pNewData = CGlobal.m_Device.m_SoeClass.GetNewestMsg();
            while (pNewData != null)
            {
                string[] RowContent = new string[7];
                RowContent[0] = (nSOEMaxNo + 1).ToString();
                RowContent[1] = pNewData.m_YxOption.m_YxNo.ToString();
                RowContent[2] = pNewData.m_YxOption.m_YxName;
                RowContent[3] = pNewData.m_YxOption.m_YxValue.ToString();
                RowContent[4] = pNewData.m_RecordTime;
                RowContent[5] = pNewData.m_YxOption.m_YxChangeTime;
                RowContent[6] = pNewData.m_StatusDesc;
                nSOEMaxNo++;
                ListViewItem item = new ListViewItem(RowContent);
                SOE_View.Items.Insert(0, item);
                pNewData = CGlobal.m_Device.m_SoeClass.GetNewestMsg();           
            } 
           
        }    
        private void BtnClearSOE_Click(object sender, EventArgs e)
        {
            SOE_View.Items.Clear();            
        }
        private void CSOEMsgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CGlobal.m_SOEView = null;
            bool flag1 = (CGlobal.m_BufferView == null || CGlobal.m_BufferView.IsDisposed == true);
            bool flag3 = (CGlobal.m_EventView == null || CGlobal.m_EventView.IsDisposed == true); 
            if (flag1 && flag3)
            {
                CGlobal.PostMessage(CGlobal.m_Device.m_MainFormHandle, UseMessage.MY_MSG_1, 0, 0);
            }
        }
    }
}
