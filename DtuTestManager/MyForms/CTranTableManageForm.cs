using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DtuTestManager
{
    public partial class CTranTableManageForm : Form
    {
        public CTableInterface m_Table = null;       
        public CTranTableManageForm()
        {
            InitializeComponent();
        }
        private void IniListView_1()
        {
            int i,j;
            DataListView.ClearAllItem();
            DataListView.GridLines = true;
            DataListView.FullRowSelect = true;
            DataListView.View = View.Details;
            DataListView.Scrollable = true;
            DataListView.MultiSelect = false;
            DataListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            DataListView.Visible = true;
            int nColCount = m_Table.GetColCount();
            int nRowCount = m_Table.GetRowCount();

            for (i = 0; i < nColCount; i++)
            {
                CTranValue pValue = m_Table.GetColNameValue(i);
                if (pValue == null) break;
                if (pValue.m_ValueType == 2)
                {
                    DataListView.Columns.Add(pValue.m_StrDataName, 120, HorizontalAlignment.Center);
                }
                else
                {
                    DataListView.Columns.Add(pValue.m_StrDataName, 100, HorizontalAlignment.Center);
                }
                
            }
            string[] strItem = new string[nColCount];
            for (i = 0; i < nRowCount; i++)
            {
                for (j = 0; j < nColCount; j++)
                {
                    CTranValue pData = m_Table.GetTranValue(i,j);
                    strItem[j] = (string)pData.m_TranValue;
                }
                ListViewItem pItem = new ListViewItem(strItem);
                DataListView.AddItem(pItem);

                for (j = 0; j < nColCount; j++)
                {
                    CTranValue pData = m_Table.GetTranValue(i, j);
                    if (pData.m_EditMode)
                    {
                        int nIndex = DataListView.AddComBoxListStr(pData.m_ComStringList);
                        DataListView.SetCellMode(i, j, 2, nIndex);
                    }
                    else
                    {
                        DataListView.SetCellMode(i, j, 1, 0);
                    }
                }
            }
        }
        private void IniListView_2()
        {
            int i, j;
            DataListView.ClearAllItem();
            DataListView.GridLines = true;
            DataListView.FullRowSelect = true;
            DataListView.View = View.Details;
            DataListView.Scrollable = true;
            DataListView.MultiSelect = false;
            DataListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            DataListView.Visible = true;
            int nColCount = m_Table.GetColCount();  
            int nRowCount = m_Table.GetRowCount(); 
            DataListView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            DataListView.Columns.Add("名称", 120, HorizontalAlignment.Center);
            DataListView.Columns.Add("值", 120, HorizontalAlignment.Center);
            string[] strItem = new string[3];
            for (i = 0; i < nColCount; i++)
            {
                strItem[0] = i.ToString();
                CTranValue pValue = m_Table.GetColNameValue(i);
                if (pValue == null) break;
                strItem[1] = pValue.m_StrDataName;
                CTranValue pData = m_Table.GetTranValue(0, i);
                strItem[2] = (string)pData.m_TranValue;
                ListViewItem pItem = new ListViewItem(strItem);
                DataListView.AddItem(pItem);
                if (pData.m_EditMode)
                {
                    int nIndex = DataListView.AddComBoxListStr(pData.m_ComStringList);
                    DataListView.SetCellMode(i, 2, 2, nIndex);
                }
                else
                {
                    DataListView.SetCellMode(i, 2, 1, 0);
                }
            }
        }

        private void IniListView()
        {
            if (m_Table.m_tableType!= 6)
            {
                IniListView_1();
            }
            else
            {
                IniListView_2();
            }
        }

        private void CTranTableManageForm_Load(object sender, EventArgs e)
        {
            IniListView();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            m_Table.AddNewData();
            IniListView();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            m_Table.InsertData(m_Table.m_SelectIndex);
            IniListView();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            m_Table.RemoveAtData(m_Table.m_SelectIndex);
            IniListView();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            m_Table.ClearAllData();
            IniListView();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           Thread thread = new Thread(m_Table.SaveTableContent);
           thread.Start();
            //m_Table.SaveTableContent();
        }

        private void DataListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataListView.SelectedIndices != null && DataListView.SelectedIndices.Count > 0)
            {
                m_Table.m_SelectIndex = DataListView.SelectedItems[0].Index;
            }
            else
            {
                m_Table.m_SelectIndex = -1;
            }
        }

        private void DataListView_DoubleClick(object sender, EventArgs e)
        {
            if (m_Table.m_SelectIndex >= 0)
            {
                DataListView.theProcessFun = new MyListView.ProcessDelegate(m_Table.UpdateTranValue);
            }
        }
    }
}
