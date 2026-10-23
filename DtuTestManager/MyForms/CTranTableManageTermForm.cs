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
    public partial class CTranTableManageTermForm : Form
    {
        public bool IsSave = false;
        public int NodeType = 0;
        public CDeviceConfig pDevice = null;  //term
        public CTableInterface m_Table = null; 
        public CTranTableManageTermForm()
        {
            InitializeComponent();
        }
        private void IniListView()
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

            for (i = 0; i < nColCount; i++)
            {
                CTranValue pValue = m_Table.GetColNameValue(i);
                if (pValue == null) break;
                DataListView.Columns.Add(pValue.m_StrDataName, 100, HorizontalAlignment.Center);
            }
            if (NodeType<5) DataListView.Columns.Add("名称", 100, HorizontalAlignment.Center);

            string[] strItem = new string[DataListView.Columns.Count];
            for (i = 0; i < nRowCount; i++)
            {
                for (j = 0; j < nColCount; j++)
                {
                    CTranValue pData = m_Table.GetTranValue(i, j);
                    strItem[j] = (string)pData.m_TranValue;
                }
                if (NodeType < 5)
                {
                    if (i < pDevice.m_TermDataValue[NodeType].m_DataName.Count)
                    {
                        strItem[j] = pDevice.m_TermDataValue[NodeType].m_DataName[i];
                    }
                    else
                    {
                        strItem[j] = "备用";
                        pDevice.m_TermDataValue[NodeType].m_DataName.Add(strItem[j]);
                    }
                }
                ListViewItem pItem = new ListViewItem(strItem);
                DataListView.AddItem(pItem);

                for (j = 0; j < DataListView.Columns.Count; j++)
                {
                    CTranValue pData = m_Table.GetTranValue(i, j);
                    if (pData != null && pData.m_EditMode)
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

        private void CTranTableManageTermForm_Load(object sender, EventArgs e)
        {
            IniListView();
            IsSave = false;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            m_Table.AddNewData();
            if (NodeType < 5) pDevice.m_TermDataValue[NodeType].nRealCnt++;
            IniListView();
        }      
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int nIndex = DataListView.Items.Count-1;
            if (nIndex < 0) return;
            m_Table.RemoveAtData(nIndex);
            if (NodeType < 5) pDevice.m_TermDataValue[NodeType].nRealCnt--;
            IniListView();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            m_Table.ClearAllData();
            if (NodeType < 5) pDevice.m_TermDataValue[NodeType].nRealCnt = 0;
            IniListView();
        }

        private void SaveFun()
        {
            if (NodeType < 5)
            {
                for (int i = 0; i < DataListView.Items.Count; i++)
                {
                    pDevice.m_TermDataValue[NodeType].m_DataName[i] = DataListView.Items[i].SubItems[DataListView.Columns.Count - 1].Text;
                }
            }
            int Cnt = m_Table.SaveTableContent();
            if (pDevice != null) pDevice.WriteDtaToINIFile(NodeType, Cnt);
               
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFun();
            IsSave = true;
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
