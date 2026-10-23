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
using System.Runtime.InteropServices;
using System.Threading;
using System.IO.Ports;
using System.Reflection;
namespace DtuTestManager
{
    public partial class CRealCtrlForm : Form
    {       
        public CRealCtrlForm()
        {
            InitializeComponent();
            //这里给窗体开双缓存
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            //给窗体内控件开启双缓存
            DoubleBuffer(this);
        }
        /// <summary>
        /// 遍历窗体，给每个控件开启双缓存才是真的解决闪屏问题，只开窗体双缓存作用不大
        /// </summary>
        /// <param name="control"></param>
        private void DoubleBuffer(Control control)
        {
            Control.ControlCollection sonControls = control.Controls;
            //遍历所有控件  
            foreach (Control con in sonControls)
            {
                Type dgvType = con.GetType();
                if (dgvType.Name.Equals("Button") || dgvType.Name.Equals("TextBox") || dgvType.Name.Equals("Label") || dgvType.Name.Equals("DateTimePicker") || dgvType.Name.Equals("Panel") || dgvType.Name.Equals("DataGridView") || dgvType.Name.Equals("SplitterPanel") || dgvType.Name.Equals("TabControl") || dgvType.Name.Equals("Label") || dgvType.Name.Equals("SplitContainer"))
                {
                    PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                    pi.SetValue(con, true, null);
                    if (dgvType.Name.Equals("Panel") || dgvType.Name.Equals("SplitterPanel") || dgvType.Name.Equals("TabControl") || dgvType.Name.Equals("SplitContainer"))
                    {
                        DoubleBuffer(con);
                    }
                }
                else if (dgvType.Name.Equals("TabPage"))
                {
                    DoubleBuffer(con);
                }
            }
        }

        private void CRealCtrlForm_Load(object sender, EventArgs e)
        {
            Init_YxLV();
            Init_YcLV();
            Init_DDLV(); 
        }         
        private void Init_YxLV()
        {   
            string[] h = new string[6]; 
            try
            {
                #region YX
                LV_YX.SmallImageList = MyImageList;
                LV_YX.Width = 480;
                LV_YX.ClearAllItem();                
                LV_YX.Columns.Add("序号", 60, HorizontalAlignment.Left);
                LV_YX.Columns.Add("遥信名称", 100, HorizontalAlignment.Left);
                LV_YX.Columns.Add("状态(值)", 70, HorizontalAlignment.Center);
                LV_YX.Columns.Add("变位时间", 180, HorizontalAlignment.Center);
                LV_YX.GridLines = true;
                LV_YX.FullRowSelect = true;
                LV_YX.View = View.Details;
                LV_YX.Scrollable = true;
                LV_YX.MultiSelect = false;
                LV_YX.HeaderStyle = ColumnHeaderStyle.Clickable;
                LV_YX.Visible = true;

                LV_YX.BeginUpdate();
                try
                {
                    for (int i = 0; i < CGlobal.m_Device.m_YxList.Count; i++)
                    {
                        h[0] = " " + Convert.ToString(CGlobal.m_Device.m_YxList[i].m_YxNo);
                        h[1] = CGlobal.m_Device.m_YxList[i].m_YxName;
                        h[2] = CGlobal.m_Device.m_YxList[i].m_YxValue.ToString();
                        h[3] = CGlobal.m_Device.m_YxList[i].m_YxChangeTime;
                        ListViewItem ViewItem = new ListViewItem(h);

                        this.LV_YX.AddItem(ViewItem);
                    }
                }
                finally
                {
                    LV_YX.EndUpdate();
                }

                for (int i = 0; i <= LV_YX.Items.Count; i++)
                {

                    MyListView.LV_ITEM lvi = new MyListView.LV_ITEM();

                    lvi.iItem = i; // Row
                    lvi.iSubItem = 2;  // Column
                    lvi.mask = MyListView.LVIF_IMAGE | MyListView.LVIF_TEXT;

                    lvi.iImage = 1;
                    lvi.pszText = "0";
                    MyListView.SendMessage(LV_YX.Handle, MyListView.LVM_SETITEM, 0, ref lvi);

                }
                MyListView.SendMessage(LV_YX.Handle, MyListView.LVM_GETEXTENDEDLISTVIEWSTYLE, new IntPtr(MyListView.LVS_EX_SUBITEMIMAGES), new IntPtr(MyListView.LVS_EX_SUBITEMIMAGES));

                #endregion
            }
            catch { }          
        }                           //初始化遥信界面
        private void Init_YcLV()
        {           
            string[] h = new string[6];   
            LV_YC.BeginUpdate();
            try
            {
                #region YC
                LV_YC.ClearAllItem();
                LV_YC.Width = 420;
                LV_YC.Columns.Add("遥测序号", 80, HorizontalAlignment.Center);
                LV_YC.Columns.Add("遥测名称", 140, HorizontalAlignment.Left);
                LV_YC.Columns.Add("实时值", 70, HorizontalAlignment.Center);
                LV_YC.Columns.Add("单位", 50, HorizontalAlignment.Center);
                LV_YC.GridLines = true;
                LV_YC.FullRowSelect = true;
                LV_YC.View = View.Details;
                LV_YC.Scrollable = true;
                LV_YC.MultiSelect = false;
                LV_YC.HeaderStyle = ColumnHeaderStyle.Clickable;
                LV_YC.Visible = true;
                for (int i = 0; i < CGlobal.m_Device.m_YcList.Count; i++)
                {                   
                    h[0] = CGlobal.m_Device.m_YcList[i].m_YcNo.ToString();
                    h[1] = CGlobal.m_Device.m_YcList[i].m_YcName;
                    h[2] = CGlobal.m_Device.m_YcList[i].m_YcValue.ToString();
                    h[3] = CGlobal.m_Device.m_YcList[i].m_YcUnit;
                    ListViewItem item1 = new ListViewItem(h);
                    this.LV_YC.AddItem(item1);
                }
                #endregion
            }
            catch { }
            finally
            {
                LV_YC.EndUpdate();
            }
        
        }                           //初始化遥测界面
        private void Init_DDLV()
        {       
            string[] hh = new string[4];
            LV_DD.BeginUpdate();
            try
            {
                #region DD
                LV_DD.ClearAllItem();
                LV_DD.Width = 420;
                LV_DD.Columns.Add("电度序号", 80, HorizontalAlignment.Center);
                LV_DD.Columns.Add("电度名称", 160, HorizontalAlignment.Left);
                LV_DD.Columns.Add("实时值", 60, HorizontalAlignment.Center);
                LV_DD.Columns.Add("单位", 80, HorizontalAlignment.Center);
                LV_DD.GridLines = true;
                LV_DD.FullRowSelect = true;
                LV_DD.View = View.Details;
                LV_DD.Scrollable = true;
                LV_DD.MultiSelect = false;
                LV_DD.HeaderStyle = ColumnHeaderStyle.Clickable;
                LV_DD.Visible = true;
                for (int i = 0; i < CGlobal.m_Device.m_PowerList.Count; i++)
                {
                    hh[0] = Convert.ToString(CGlobal.m_Device.m_PowerList[i].m_PowerNo, 16).ToUpper();
                    hh[1] = CGlobal.m_Device.m_PowerList[i].m_PowerName;
                    hh[2] = CGlobal.m_Device.m_PowerList[i].m_PowerValue.ToString();
                    hh[3] = CGlobal.m_Device.m_PowerList[i].m_PowerUnit;
                    ListViewItem item1 = new ListViewItem(hh);
                    this.LV_DD.AddItem(item1);
                }
                #endregion
            }
            catch { }
            finally
            {
                LV_DD.EndUpdate();
            }
        }                           //初始化电度界面
        //报文、SOE、事件信息窗口控制按钮函数
        private void Btn_BufferView_Click(object sender, EventArgs e)
        {
          //  IniBuffView();
        }
        private void Btn_SoeView_Click(object sender, EventArgs e)
        {
           // IniSOEView();
        }
        private void Btn_EventView_Click(object sender, EventArgs e)
        {
           // IniEventView();
        }
        //数据刷新
        private void FreshYc()
        {
            for (int i = 0; i < CGlobal.m_Device.m_YcList.Count; i++)
            {
                LV_YC.Items[i].SubItems[2].Text = CGlobal.m_Device.m_YcList[i].m_YcValue.ToString("F4"); ;
            }
        }
        private void FreshYx()
        {
            for (int i = 0; i < CGlobal.m_Device.m_YxList.Count; i++)
            {
                LV_YX.Items[i].SubItems[2].Text = CGlobal.m_Device.m_YxList[i].m_YxValue.ToString();
                LV_YX.Items[i].SubItems[3].Text = CGlobal.m_Device.m_YxList[i].m_YxChangeTime;

                MyListView.LV_ITEM lvi = new MyListView.LV_ITEM();
                lvi.iItem = i; // Row
                lvi.iSubItem = 2;  // Column
                lvi.mask = MyListView.LVIF_IMAGE | MyListView.LVIF_TEXT;
                lvi.pszText = CGlobal.m_Device.m_YxList[i].m_YxValue.ToString();

                if (CGlobal.m_Device.m_Connection.m_SingleType == 1)        //双点模式
                {
                    lvi.iImage = CGlobal.m_Device.m_YxList[i].m_YxValue;
                }
                else
                {
                    lvi.iImage = CGlobal.m_Device.m_YxList[i].m_YxValue + 1;
                }
                MyListView.SendMessage(LV_YX.Handle, MyListView.LVM_SETITEM, 0, ref lvi);
                MyListView.SendMessage(LV_YX.Handle, MyListView.LVM_GETEXTENDEDLISTVIEWSTYLE, new IntPtr(MyListView.LVS_EX_SUBITEMIMAGES), new IntPtr(MyListView.LVS_EX_SUBITEMIMAGES));
            } 
        }
        private void FreshPw()
        {
            for (int i = 0; i < CGlobal.m_Device.m_PowerList.Count; i++)
            {
                LV_DD.Items[i].SubItems[2].Text = CGlobal.m_Device.m_PowerList[i].m_PowerValue.ToString();
            } 
        }  
        private void Btn_StartOrder_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(1);          
        }       
        private void Btn_StopOrder_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(3);
        }
        private void Btn_HeartTest_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(2);
        }
        private void Btn_CheckNo_Click(object sender, EventArgs e)
        {          
            CGlobal.SendTaskOrder(21);
        }
        private void Btn_SetTime_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(5);
        }
        private void Btn_CallTotalData_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(4);
        }
        private void Btn_CallPowerData_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(7);
        }
        private void BtnCallTime_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(41);
        }
        private void Btn_GetFrzData_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(39);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CGlobal.m_Device.m_bOnlineStatus)
            {
                if (CGlobal.m_Device.GetYxFreshStatus())
                {
                    FreshYx();
                }
                if (CGlobal.m_Device.GetYcFreshStatus())
                {
                    FreshYc();
                }
                if (CGlobal.m_Device.GetPwFreshStatus())
                {
                    FreshPw();
                }
            }
        }

        private void BtnSetFixData_Click(object sender, EventArgs e)
        {
            CFixDataForm pform = new CFixDataForm();
            pform.ShowDialog();
        }       
    }
}
