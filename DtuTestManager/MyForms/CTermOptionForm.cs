using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CTermOptionForm: Form
    {
        string m_NodeType;//当前终端的设备类型
        public CTransStation m_Station = null;
        public CDeviceConfig pDevice = null;      
        private List<CTranValue> m_OptionList = new List<CTranValue>();
        private List<CTranValue> m_CountList = new List<CTranValue>();
        private List<CTranValue> m_RecordList = new List<CTranValue>();

        private int m_OptionIndex = 0;
        private int m_CountIndex = 0;
        private int m_RecordIndex = 0;


        private CTransYxDB m_YxTable = new CTransYxDB();
        private CTransYcDB m_YcTable = new CTransYcDB();
        private CTransYkDB m_YkTable = new CTransYkDB();
        private CTransPowerDB m_PowerTable = new CTransPowerDB();
        private CTransSetDB m_SetTable = new CTransSetDB();
        private CTransProtocl m_ProtoclTable = new CTransProtocl(); 

        public CTermOptionForm()
        {
            InitializeComponent();
        }
        private void IniListView_One()
        {
            OptionView.Clear();
            OptionView.GridLines = true;
            OptionView.FullRowSelect = true;
            OptionView.View = View.Details;
            OptionView.Scrollable = true;
            OptionView.MultiSelect = false;
            OptionView.HeaderStyle = ColumnHeaderStyle.Clickable;
            OptionView.Visible = true;

            OptionView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            OptionView.Columns.Add("名称", 120, HorizontalAlignment.Left);
            OptionView.Columns.Add("数值", 120, HorizontalAlignment.Left);
            OptionView.Columns.Add("列名", 120, HorizontalAlignment.Left);
            OptionView.Columns.Add("类型", 60, HorizontalAlignment.Left);
            int i = 0;
             int nIndex =0;
            ListViewItem pItem;
            string[] strItem = new string[5];
            for ( i = 0; i < m_OptionList.Count; i++)
            {
                CTranValue pData = m_OptionList[i];
                strItem[0] = (i+1).ToString();
                strItem[1] = pData.m_StrDataName;
                strItem[2] = (string)pData.m_TranValue;
                strItem[3] = pData.m_StrColName;
                strItem[4] = pData.GetDataTypeDesc();
                 pItem = new ListViewItem(strItem);
                OptionView.AddItem(pItem);
                if (pData.m_EditMode)
                {
                     nIndex = OptionView.AddComBoxListStr(pData.m_ComStringList);
                    OptionView.SetCellMode(i, 2, 2, nIndex);
                }
                else
                {
                    OptionView.SetCellMode(i, 2, 1, 0);
                }
            }                   
        }
        private void IniListView_Two()
        {
            CountSetView.Clear();
            CountSetView.GridLines = true;
            CountSetView.FullRowSelect = true;
            CountSetView.View = View.Details;
            CountSetView.Scrollable = true;
            CountSetView.MultiSelect = false;
            CountSetView.HeaderStyle = ColumnHeaderStyle.Clickable;
            CountSetView.Visible = true;

            CountSetView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            CountSetView.Columns.Add("名称", 120, HorizontalAlignment.Left);
            CountSetView.Columns.Add("数值", 120, HorizontalAlignment.Left);
            CountSetView.Columns.Add("列名", 120, HorizontalAlignment.Left);
            CountSetView.Columns.Add("类型", 60, HorizontalAlignment.Left);

            string[] strItem = new string[5];
            for (int i = 0; i < m_CountList.Count; i++)
            {
                CTranValue pData = m_CountList[i];
                strItem[0] = (i + 1).ToString();
                strItem[1] = pData.m_StrDataName;
                strItem[2] = (string)pData.m_TranValue;
                strItem[3] = pData.m_StrColName;
                strItem[4] = pData.GetDataTypeDesc();
                ListViewItem pItem = new ListViewItem(strItem);
                CountSetView.AddItem(pItem);
                if(i<10 || i>14)
                {
                    if (pData.m_EditMode)
                    {
                        int nIndex = CountSetView.AddComBoxListStr(pData.m_ComStringList);
                        CountSetView.SetCellMode(i, 2, 2, nIndex);
                    }
                    else
                    {
                        CountSetView.SetCellMode(i, 2, 1, 0);
                    }
                }
            }
        }
        private void IniListView_Three()
        {
            RecordView.Clear();
            RecordView.GridLines = true;
            RecordView.FullRowSelect = true;
            RecordView.View = View.Details;
            RecordView.Scrollable = true;
            RecordView.MultiSelect = false;
            RecordView.HeaderStyle = ColumnHeaderStyle.Clickable;
            RecordView.Visible = true;

            RecordView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            RecordView.Columns.Add("名称", 120, HorizontalAlignment.Left);
            RecordView.Columns.Add("数值", 120, HorizontalAlignment.Left);
            RecordView.Columns.Add("列名", 120, HorizontalAlignment.Left);
            RecordView.Columns.Add("类型", 60, HorizontalAlignment.Left);

            string[] strItem = new string[5];
            for (int i = 0; i < m_RecordList.Count; i++)
            {
                CTranValue pData = m_RecordList[i];
                strItem[0] = (i + 1).ToString();
                strItem[1] = pData.m_StrDataName;
                strItem[2] = (string)pData.m_TranValue;
                strItem[3] = pData.m_StrColName;
                strItem[4] = pData.GetDataTypeDesc();
                ListViewItem pItem = new ListViewItem(strItem);
                RecordView.AddItem(pItem);
                if (pData.m_EditMode)
                {
                    int nIndex = RecordView.AddComBoxListStr(pData.m_ComStringList);
                    RecordView.SetCellMode(i, 2, 2, nIndex);
                }
                else
                {
                    RecordView.SetCellMode(i, 2, 1, 0);
                }
            }
        }
        private void CTranStationOptionForm_Load(object sender, EventArgs e)
        {

            m_NodeType = (string)pDevice.m_ValueList[2].m_TranValue;
            m_YxTable.m_TableName = "YxDB" + (string)(pDevice.m_ValueList[1].m_TranValue);
            m_YxTable.m_SqlDataBase = pDevice.m_TermRoot.m_TheStation.m_MainSqlite;
            m_YxTable.IniReadTableData();

            m_YcTable.m_TableName = "YcDB" + (string)(pDevice.m_ValueList[1].m_TranValue);
            m_YcTable.m_SqlDataBase = pDevice.m_TermRoot.m_TheStation.m_MainSqlite;
            m_YcTable.IniReadTableData();

            m_YkTable.m_TableName = "YkDB" + (string)(pDevice.m_ValueList[1].m_TranValue);
            m_YkTable.m_SqlDataBase = pDevice.m_TermRoot.m_TheStation.m_MainSqlite;
            m_YkTable.IniReadTableData();

            m_PowerTable.m_TableName = "PowerDB" + (string)(pDevice.m_ValueList[1].m_TranValue);
            m_PowerTable.m_SqlDataBase = pDevice.m_TermRoot.m_TheStation.m_MainSqlite;
            m_PowerTable.IniReadTableData();

            m_SetTable.m_TableName = "SetDB" + (string)(pDevice.m_ValueList[1].m_TranValue);
            m_SetTable.m_SqlDataBase = pDevice.m_TermRoot.m_TheStation.m_MainSqlite;
            m_SetTable.IniReadTableData();

            m_ProtoclTable.m_TableName = (string)(pDevice.m_ValueList[14].m_TranValue);
            m_ProtoclTable.m_SqlDataBase = pDevice.m_TermRoot.m_TheStation.m_UserSqlite;
            m_ProtoclTable.IniReadTableData();
            for (int i = 0; i < pDevice.m_ValueList.Count; i++)
            {
                CTranValue pData = pDevice.m_ValueList[i];
                switch (pData.m_GroupNo)
                {
                    case 1:
                        m_OptionList.Add(pData);
                        break;
                    case 2:
                        m_CountList.Add(pData);
                        break;
                    case 3:
                        m_RecordList.Add(pData);
                        break;
                }
            }


            IniListView_One();
            IniListView_Two();
            IniListView_Three();


        }       
        private void OptionView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptionView.SelectedIndices != null && OptionView.SelectedIndices.Count > 0)
            {
                m_OptionIndex = OptionView.SelectedItems[0].Index;
            }
            else
            {
                m_OptionIndex = -1;
            }
        }
        private void CountSetView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountSetView.SelectedIndices != null && CountSetView.SelectedIndices.Count > 0)
            {
                m_CountIndex = CountSetView.SelectedItems[0].Index;
            }
            else
            {
                m_CountIndex = -1;
            }
        }
        private void RecordView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RecordView.SelectedIndices != null && RecordView.SelectedIndices.Count > 0)
            {
                m_RecordIndex = RecordView.SelectedItems[0].Index;
            }
            else
            {
                m_RecordIndex = -1;
            }
        }
        private void OptionView_DoubleClick(object sender, EventArgs e)
        {
            if (m_OptionIndex >=0)
            {
                OptionView.theProcessFun = new MyListView.ProcessDelegate(m_OptionList[m_OptionIndex].UpateValue);
            }
        }

        private void CountSetView_DoubleClick(object sender, EventArgs e)
        {
            if (m_CountIndex >= 0)
            {
                CountSetView.theProcessFun = new MyListView.ProcessDelegate(m_CountList[m_CountIndex].UpateValue);
            }
        }
        private void RecordView_DoubleClick(object sender, EventArgs e)
        {
            if (m_RecordIndex >= 0)
            {
                RecordView.theProcessFun = new MyListView.ProcessDelegate(m_RecordList[m_RecordIndex].UpateValue);
            }
        }

        private void SaveSetBtn_Click(object sender, EventArgs e)
        {
            string strSql = "";
            string strDataContent = "";
            int i, nCount = 0;
            bool m_DevChangeFlag=false;
            strSql = "update TerminalNodeCfg set ";
            for (i = 0; i < m_OptionList.Count; i++)
            {
                CTranValue pData = m_OptionList[i];
               if (pData.m_bChange == false) continue;

                if(i==14)
                {
                    m_DevChangeFlag=true;
                }
                pData.m_bChange = false;
                if (nCount > 0) strDataContent += ",";
                strDataContent += pData.GetUpdateValueString();
                nCount++;
            }
            for (i = 0; i < m_RecordList.Count; i++)
            {
                CTranValue pData = m_RecordList[i];
              if (pData.m_bChange == false) continue;
                pData.m_bChange = false;
                if (nCount > 0) strDataContent += ",";
                strDataContent += pData.GetUpdateValueString();
                nCount++;
            }
            if (nCount == 0) return;
            strSql += strDataContent;
            strSql += " where(NodeNo=" + pDevice.m_TermNo.ToString() + ")";
            pDevice.m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);

            m_NodeType =  OptionView.Items[14].SubItems[2].Text;
            if (m_DevChangeFlag)
            {
                pDevice.UpdateNIFileContent(m_NodeType);
                string strDevName = pDevice.m_TermName;
                string FileName = m_Station.m_StationPath + "\\"+strDevName+".ini";
                CGlobal.WritePrivateProfileString("ModuleName", "Name" , m_NodeType, FileName);
            
            }
        }
        private void BtnYc_Click(object sender, EventArgs e)
        {
            int YcCnt = m_YcTable.GetRowCount();
            if (m_YcTable.m_TableName == "null" || m_YcTable.m_TableName == "NULL") return;
            if (pDevice.m_TermDataValue[1].m_isReadDataName == false)
            {
                pDevice.ReadDataINIFile(1,m_NodeType);
            }
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_YcTable;
            pForm.pDevice = pDevice;
            pForm.NodeType =1;
            pForm.ShowDialog();
            int nAfterYcCnt = m_YcTable.GetRowCount();
            pDevice.m_ValueList[26].m_TranValue = nAfterYcCnt;
            string strSql = "update TerminalNodeCfg set ";
            strSql += "YcDB=" + nAfterYcCnt.ToString();
            pDevice.m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (YcCnt != nAfterYcCnt)
            {
                pDevice.ClearRouteTableData(1);
                pDevice.CreatRouteTableData(1);
            }
            IniListView_Two();
        }
        private void BtnYx_Click(object sender, EventArgs e)
        {
            int YxCnt = m_YxTable.GetRowCount();
            if (m_YxTable.m_TableName == "null" || m_YxTable.m_TableName == "NULL") return;
            if (pDevice.m_TermDataValue[0].m_isReadDataName == false)
            {
                pDevice.ReadDataINIFile(0,m_NodeType);
            }
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_YxTable;
            pForm.pDevice = pDevice;
            pForm.NodeType = 0;
            pForm.ShowDialog();
            int nAfterYXCnt = m_YxTable.GetRowCount();
            pDevice.m_ValueList[25].m_TranValue = nAfterYXCnt;
            string strSql = "update TerminalNodeCfg set ";
            strSql += "YxDB=" + nAfterYXCnt.ToString();
            pDevice.m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (YxCnt != nAfterYXCnt)
            {
                pDevice.ClearRouteTableData(0);
                pDevice.CreatRouteTableData(0);
            }
            IniListView_Two();

        }

        private void BtnYk_Click(object sender, EventArgs e)
        {
            int YkCnt = m_YkTable.GetRowCount();
            if (m_YkTable.m_TableName == "null" || m_YkTable.m_TableName == "NULL") return;
            if (pDevice.m_TermDataValue[2].m_isReadDataName == false)
            {
                pDevice.ReadDataINIFile(2,m_NodeType);
            } 
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_YkTable;
            pForm.pDevice = pDevice;
            pForm.NodeType = 2;
            pForm.ShowDialog();
            int nAfterYkCnt = m_YkTable.GetRowCount();
            pDevice.m_ValueList[27].m_TranValue = nAfterYkCnt;
            string strSql = "update TerminalNodeCfg set ";
            strSql += "YkDB=" + nAfterYkCnt.ToString();
            pDevice.m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (YkCnt != nAfterYkCnt)
            {
                pDevice.ClearRouteTableData(2);
                pDevice.CreatRouteTableData(2);
            }
            IniListView_Two();
        }

        private void BtnYm_Click(object sender, EventArgs e)
        {
            int PowerCnt = m_PowerTable.GetRowCount();
            if (m_PowerTable.m_TableName == "null" || m_PowerTable.m_TableName == "NULL") return;
            if (pDevice.m_TermDataValue[4].m_isReadDataName == false)
            {
                pDevice.ReadDataINIFile(4,m_NodeType);
            }
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_PowerTable;
            pForm.pDevice = pDevice;
            pForm.NodeType = 4;
            pForm.ShowDialog();
            int nAfterPowerCnt = m_PowerTable.GetRowCount();
            pDevice.m_ValueList[29].m_TranValue = nAfterPowerCnt;
            string strSql = "update TerminalNodeCfg set ";
            strSql += "PowerDB=" + nAfterPowerCnt.ToString();
            pDevice.m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (PowerCnt != nAfterPowerCnt)
            {
                pDevice.ClearRouteTableData(4);
                pDevice.CreatRouteTableData(4);
            }
            IniListView_Two();
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            int SetCnt = m_SetTable.GetRowCount();
            if (m_SetTable.m_TableName == "null" || m_SetTable.m_TableName == "NULL") return;
            if (pDevice.m_TermDataValue[3].m_isReadDataName == false)
            {
                pDevice.ReadDataINIFile(3,m_NodeType);
            }
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_SetTable;
            pForm.pDevice = pDevice;
            pForm.NodeType = 3;
            pForm.ShowDialog();
            int nAfterSetCnt = m_SetTable.GetRowCount();
            pDevice.m_ValueList[28].m_TranValue = nAfterSetCnt;
            string strSql = "update TerminalNodeCfg set ";
            strSql += "SetDB=" + nAfterSetCnt.ToString();
            pDevice.m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (SetCnt != nAfterSetCnt)
            {
                pDevice.ClearRouteTableData(3);
                pDevice.CreatRouteTableData(3);
            }
            IniListView_Two();
        }
        private void BtnProtocl_Click(object sender, EventArgs e)
        {
            if (m_ProtoclTable.m_TableName == "null" || m_ProtoclTable.m_TableName == "NULL") return;
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_ProtoclTable;
            pForm.pDevice = pDevice;
            pForm.NodeType = 5;
            pForm.ShowDialog();
        }

        private void OptionView_Click(object sender, EventArgs e)
        {
           
        }

        private void CountSetView_Click(object sender, EventArgs e)
        {
        }

        private void RecordView_Click(object sender, EventArgs e)
        {
            
        }
    }
}
