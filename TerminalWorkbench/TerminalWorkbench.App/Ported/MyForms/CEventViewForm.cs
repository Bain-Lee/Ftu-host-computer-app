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
    public partial class CEventViewForm : Form
    {
        public int m_Type = 0;
        string m_Content = "";
        CEventMesg[] m_DataGroup = new CEventMesg[16];
        int m_RowCount = 0;
        public CEventViewForm()
        {
            InitializeComponent();
        }
        private void IniView()
        {
            EventView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            EventView.Columns.Add("数据名称", 250);
            EventView.Columns.Add("数据地址", 100, HorizontalAlignment.Center);
            EventView.Columns.Add("数据值", 200, HorizontalAlignment.Center);


            EventView.GridLines = true;
            EventView.FullRowSelect = true;
            EventView.View = View.Details;
            EventView.Scrollable = true;
            EventView.MultiSelect = false;
            EventView.HeaderStyle = ColumnHeaderStyle.Clickable;
            EventView.CheckBoxes = true;
        }
        private void IniListData()
        {
            int i;
            for (i = 0; i < 16; i++)
            {
                m_DataGroup[i].m_No = i % 8 + 1;
            }

            m_DataGroup[0].m_Name = "正向有功电量";
            m_DataGroup[1].m_Name = "正向无功电量";
            m_DataGroup[2].m_Name = "一象限无功电量";
            m_DataGroup[3].m_Name = "四象限无功电量";
            m_DataGroup[4].m_Name = "反向有功电量";
            m_DataGroup[5].m_Name = "反向无功电量";
            m_DataGroup[6].m_Name = "二象限无功电量";
            m_DataGroup[7].m_Name = "三象限无功电量";



        }
        private void IniDataList()
        {
            int i = 0;
            m_RowCount = 0;
            int nRowPlace = 1;
            string[] strDataList = m_Content.Split(',');
            if (m_Type <= 3 && strDataList.Length >= 18)
            {
                m_RowCount = 16;
                for (i = 0; i < 8; i++)
                {
                    m_DataGroup[i].m_Address = strDataList[nRowPlace++];
                    m_DataGroup[i].m_Value = strDataList[nRowPlace++];
                }
                EndTimer.Text = strDataList[nRowPlace++];
                for (i = 8; i < 16; i++)
                {
                    m_DataGroup[i].m_Address = strDataList[nRowPlace++];
                    m_DataGroup[i].m_Value = strDataList[nRowPlace++];
                }

            }
            else if (strDataList.Length == 9)
            {
                m_RowCount = 8;

                for (i = 0; i < 8; i++)
                {
                    m_DataGroup[i].m_Address = strDataList[nRowPlace++];
                    m_DataGroup[i].m_Value = strDataList[nRowPlace++];
                }
            }
            string[] strRow = new string[4];
            for (i = 0; i < m_RowCount; i++)
            {
                strRow[0] = m_DataGroup[i].m_No.ToString();
                strRow[1] = m_DataGroup[i].m_Name;
                strRow[2] = m_DataGroup[i].m_Address;
                strRow[3] = m_DataGroup[i].m_Value;
                ListViewItem item = new ListViewItem(strRow);
                EventView.Items.Add(item);
            }
        }
        private void CEventViewForm_Load(object sender, EventArgs e)
        {
            toolStrip1.Visible = false;
            switch (m_Type)
            {
                case 1:
                    toolStrip1.Visible = true;
                    this.Text = "A相有功功率反向事件信息";
                    break;
                case 2:
                    toolStrip1.Visible = true;
                    this.Text = "B相有功功率反向事件信息";
                    break;
                case 3:
                    toolStrip1.Visible = true;
                    this.Text = "C相有功功率反向事件信息";
                    break;
                case 4:
                    this.Text = "潮流方向改变事件信息";
                    break;
                case 5:
                    this.Text = "线损模块清零事件信息";
                    break;
                default:
                    this.Close();
                    return;
            }
            IniView();
            m_Content = (string)this.Tag;
            IniDataList();


        }
    }
    public class CEventMesg
    {
        public int m_No;
        public string m_Name = "";
        public string m_Address = "";
        public string m_Value = "";
    }
}
