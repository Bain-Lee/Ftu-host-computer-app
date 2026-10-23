using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Data.OleDb;

namespace DtuTestManager
{
    public class CDevModuleData
    {
        public int m_DataNo;            //序号
        public int m_DataType;          //类型
        public string m_DataName;       //名称
        public string m_DataUnit;       //单位
        public string m_DataDesc;       //说明
        public float m_fReadio;         //系数
        public float m_Offset;          //偏移量
        public int m_Address;           //地址
        public int m_GroupNo;           //组号
        public string m_DataValue;

        public CDevModuleData()
        {

        }
    }
    public class CBaseModuleGroup
    {
        public List<CDevModuleData> m_DataModuleList = new List<CDevModuleData>();
        public string m_DataTableName;
        public MyListView m_ListView = null;
        public int m_FaultStartAddress = 0;
        public string m_NodesName;
        public int m_DataType;
        public int m_NowSelectIndex = -1;
        public CDeviceMudule m_TheRoot = null;
        public CBaseModuleGroup()
        {

        }
        public virtual void IniReadDataList()
        {

        }
        public virtual void IniShowListView(MyListView pListView)
        {
            m_ListView = pListView;
            pListView.GridLines = true;
            pListView.FullRowSelect = true;
            pListView.View = View.Details;
            pListView.Scrollable = true;
            pListView.MultiSelect = false;
            pListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            pListView.Visible = true;
        }
        public virtual void AddNewDataRecord(CDevModuleData pNewData)
        {
        }
        public virtual void AddNewRow()
        {
           
        }
        public virtual void InsertNewRow(int nPlaceIndex)
        {

        }
        public virtual void DelSelectRow(int nPlaceIndex)
        {

        }
        public virtual void ClearAllRow()
        {

        }
        public virtual void SaveNowSet()
        {

        }
        public virtual void SetNowSelectValue(int nIndex, object pData)
        {

        }
        public virtual void SetFaultAddress()
        {

        }
        public int GetNowAddress()
        {
            if (m_DataModuleList.Count == 0) return m_FaultStartAddress;
            return m_DataModuleList[m_DataModuleList.Count - 1].m_Address + 1;
        }
    }
    public class CDevYcGroup : CBaseModuleGroup
    {
        public CDevYcGroup()
        {
            m_FaultStartAddress = 0x4001;
            m_NodesName = "遥测列表";
            m_DataType = 1;
        }
        public override void IniReadDataList()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_DataTableName + " order by YcID";
            pTable = (DataTable)m_TheRoot.m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            m_DataModuleList.Clear();
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CDevModuleData pNewData = new CDevModuleData();
                pNewData.m_DataNo = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewData.m_DataName = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewData.m_fReadio = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableFloatValue(pTable, i, 2);
                pNewData.m_Offset = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableFloatValue(pTable, i, 3);
                pNewData.m_DataUnit = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 4);
                pNewData.m_Address = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 5);
                m_DataModuleList.Add(pNewData);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.ClearAllItem();
            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("遥测名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("遥测地址", 120, HorizontalAlignment.Left);
            pListView.Columns.Add("遥测单位", 80, HorizontalAlignment.Left);
            pListView.Columns.Add("遥测系数", 80, HorizontalAlignment.Left);
            pListView.Columns.Add("偏移量", 80, HorizontalAlignment.Left);
           
            string[] strHead = new string[6];
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                CDevModuleData pData = m_DataModuleList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = pData.m_Address.ToString();
                strHead[3] = pData.m_DataUnit;
                strHead[4] = pData.m_fReadio.ToString("F5");
                strHead[5] = pData.m_Offset.ToString("F5");
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);
            pListView.SetColumnsEditMode(3);
            pListView.SetColumnsEditMode(4);
            pListView.SetColumnsEditMode(5);
        }
        public override void AddNewDataRecord(CDevModuleData pNewData)
        {
            m_DataModuleList.Add(pNewData);
            string[] strHead = new string[6];

            strHead[0] = pNewData.m_DataNo.ToString();
            strHead[1] = pNewData.m_DataName;
            strHead[2] = pNewData.m_Address.ToString();
            strHead[3] = pNewData.m_DataUnit;
            strHead[4] = pNewData.m_fReadio.ToString("F5");
            strHead[5] = pNewData.m_Offset.ToString("F5");
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
            if ((m_ListView.TopItem.Index + 1) >= m_ListView.Items.Count) return;
            m_ListView.TopItem = m_ListView.Items[m_ListView.TopItem.Index + 1];
        }
        public override void AddNewRow()
        {
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "遥测" + pNewData.m_DataNo.ToString();
            pNewData.m_DataUnit = "";
            pNewData.m_fReadio = 1;
            pNewData.m_Offset = 0;
            pNewData.m_Address = GetNowAddress();
            AddNewDataRecord(pNewData);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            if (nPlaceIndex < 0) return;
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "遥测" + pNewData.m_DataNo.ToString();
            pNewData.m_DataUnit = "";
            pNewData.m_fReadio = 1;
            pNewData.m_Offset = 0;
            pNewData.m_Address = GetNowAddress();

            m_DataModuleList.Insert(nPlaceIndex, pNewData);
            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            if (nPlaceIndex >= m_DataModuleList.Count || nPlaceIndex<0) return;
            m_DataModuleList.RemoveAt(nPlaceIndex);

            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataModuleList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void ClearAllRow()
        {
            m_DataModuleList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataModuleList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataModuleList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataModuleList[m_NowSelectIndex].m_Address = Convert.ToInt32((string)pData);
                        break;
                    case 3:
                        m_DataModuleList[m_NowSelectIndex].m_DataUnit = (string)pData;
                        break;
                    case 4:
                        m_DataModuleList[m_NowSelectIndex].m_fReadio = Convert.ToSingle((string)pData);
                        break;
                    case 5:
                        m_DataModuleList[m_NowSelectIndex].m_Offset = Convert.ToSingle((string)pData);
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            int i;
            strSql = "delete from " + m_DataTableName;
            m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_TheRoot.m_DeviceMsg.m_ModuleDB.m_Dbconn;
            cmd.Parameters.Add(cmd.CreateParameter());
            OleDbTransaction trans = m_TheRoot.m_DeviceMsg.m_ModuleDB.m_Dbconn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {

                for (i = 0; i < m_DataModuleList.Count; i++)
                {
                    strSql = "insert into " + m_DataTableName + " values(" +
                        m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                        m_DataModuleList[i].m_DataName + "'," +
                        m_DataModuleList[i].m_fReadio.ToString("F5") + "," +
                        m_DataModuleList[i].m_Offset.ToString("F5") + ",'" +
                        m_DataModuleList[i].m_DataUnit + "'," +
                        m_DataModuleList[i].m_Address.ToString() + ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;

            }

            /*
            for (i = 0; i < m_DataModuleList.Count; i++)
            {
                strSql = "insert into " + m_DataTableName + " values(" +
                    m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                    m_DataModuleList[i].m_DataName+ "'," +
                    m_DataModuleList[i].m_fReadio.ToString("F5") + "," +
                    m_DataModuleList[i].m_Offset.ToString("F5") + ",'" +
                    m_DataModuleList[i].m_DataUnit + "'," +
                    m_DataModuleList[i].m_Address.ToString() + ")";
                m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            }*/
        }
        public override void SetFaultAddress()
        {
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                m_DataModuleList[i].m_Address = m_FaultStartAddress + i;
            }
            IniShowListView(m_ListView);
        }
    }
    public class CDevYxGroup : CBaseModuleGroup
    {
        public CDevYxGroup()
        {
            m_FaultStartAddress = 1;
            m_NodesName = "遥信列表";
            m_DataType = 2;
        }
        public override void IniReadDataList()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_DataTableName + " order by YxID";
            pTable = (DataTable)m_TheRoot.m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            m_DataModuleList.Clear();
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CDevModuleData pNewData = new CDevModuleData();
                pNewData.m_DataNo = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewData.m_DataName = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewData.m_Address = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 2);
                m_DataModuleList.Add(pNewData);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.ClearAllItem();
            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("遥信名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("遥信地址", 120, HorizontalAlignment.Left);
           
            string[] strHead = new string[3];
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                CDevModuleData pData = m_DataModuleList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = pData.m_Address.ToString();
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);
        }
        public override void AddNewDataRecord(CDevModuleData pNewData)
        {
            m_DataModuleList.Add(pNewData);
            string[] strHead = new string[3];

            strHead[0] = pNewData.m_DataNo.ToString();
            strHead[1] = pNewData.m_DataName;
            strHead[2] = pNewData.m_Address.ToString();
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
            m_ListView.TopItem = m_ListView.Items[m_ListView.TopItem.Index];
        }
        public override void AddNewRow()
        {
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "遥信" + pNewData.m_DataNo.ToString();
            pNewData.m_Address = GetNowAddress();
            AddNewDataRecord(pNewData);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            if (nPlaceIndex < 0) return;
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "遥信" + pNewData.m_DataNo.ToString();
            pNewData.m_Address = GetNowAddress();

            m_DataModuleList.Insert(nPlaceIndex, pNewData);
            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            if (nPlaceIndex >= m_DataModuleList.Count) return;
            m_DataModuleList.RemoveAt(nPlaceIndex);

            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataModuleList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void ClearAllRow()
        {
            m_DataModuleList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataModuleList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataModuleList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataModuleList[m_NowSelectIndex].m_Address = Convert.ToInt32((string)pData);
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + m_DataTableName;
            m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            int i;

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_TheRoot.m_DeviceMsg.m_ModuleDB.m_Dbconn;
            cmd.Parameters.Add(cmd.CreateParameter());
            OleDbTransaction trans = m_TheRoot.m_DeviceMsg.m_ModuleDB.m_Dbconn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {

                for (i = 0; i < m_DataModuleList.Count; i++)
                {
                    strSql = "insert into " + m_DataTableName + " values(" +
                        m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                        m_DataModuleList[i].m_DataName + "'," +
                        m_DataModuleList[i].m_Address.ToString() + ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;

            }
        }
        public override void SetFaultAddress()
        {
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                m_DataModuleList[i].m_Address = m_FaultStartAddress + i;
            }
            IniShowListView(m_ListView);
        }
    }
    public class CDevYkGroup : CBaseModuleGroup
    {
        public CDevYkGroup()
        {
            m_FaultStartAddress = 0x6001;
            m_NodesName = "遥控列表";
            m_DataType = 5;
        }
        public override void IniReadDataList()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_DataTableName + " order by YkID";
            pTable = (DataTable)m_TheRoot.m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            m_DataModuleList.Clear();
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CDevModuleData pNewData = new CDevModuleData();
                pNewData.m_DataNo = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewData.m_DataName = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewData.m_Address = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 2);
                m_DataModuleList.Add(pNewData);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.ClearAllItem();
            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("遥控名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("遥控地址", 120, HorizontalAlignment.Left);

            string[] strHead = new string[3];
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                CDevModuleData pData = m_DataModuleList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = pData.m_Address.ToString();
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);
        }
        public override void AddNewDataRecord(CDevModuleData pNewData)
        {
            m_DataModuleList.Add(pNewData);
            string[] strHead = new string[3];

            strHead[0] = pNewData.m_DataNo.ToString();
            strHead[1] = pNewData.m_DataName;
            strHead[2] = pNewData.m_Address.ToString();
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
            m_ListView.TopItem = m_ListView.Items[m_ListView.TopItem.Index];
        }
        public override void AddNewRow()
        {
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "遥控" + pNewData.m_DataNo.ToString();
            pNewData.m_Address = GetNowAddress();
            AddNewDataRecord(pNewData);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "遥控" + pNewData.m_DataNo.ToString();
            pNewData.m_Address = GetNowAddress();

            m_DataModuleList.Insert(nPlaceIndex, pNewData);
            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            if (nPlaceIndex >= m_DataModuleList.Count) return;
            m_DataModuleList.RemoveAt(nPlaceIndex);

            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataModuleList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void ClearAllRow()
        {
            m_DataModuleList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataModuleList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataModuleList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataModuleList[m_NowSelectIndex].m_Address = Convert.ToInt32((string)pData);
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + m_DataTableName;
            m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            int i;
            for (i = 0; i < m_DataModuleList.Count; i++)
            {
                strSql = "insert into " + m_DataTableName + " values(" +
                    m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                    m_DataModuleList[i].m_DataName + "'," +
                    m_DataModuleList[i].m_Address.ToString() + ")";
                m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            }
        }
        public override void SetFaultAddress()
        {
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                m_DataModuleList[i].m_Address = m_FaultStartAddress + i;
            }
            IniShowListView(m_ListView);
        }
    }
    public class CDevPowerGroup : CBaseModuleGroup
    {
        public CDevPowerGroup()
        {
            m_FaultStartAddress = 0x6401;
            m_NodesName = "电量列表";
            m_DataType = 3;
        }
        public override void IniReadDataList()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + m_DataTableName + " order by PowerID";
            pTable = (DataTable)m_TheRoot.m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            m_DataModuleList.Clear();
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CDevModuleData pNewData = new CDevModuleData();
                pNewData.m_DataNo = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                pNewData.m_DataName = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 1);
                pNewData.m_DataUnit = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 2);
                pNewData.m_Address = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 3);
                m_DataModuleList.Add(pNewData);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.ClearAllItem();
            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("电量名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("电量地址", 120, HorizontalAlignment.Left);
            pListView.Columns.Add("电量单位", 80, HorizontalAlignment.Left);

            string[] strHead = new string[4];
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                CDevModuleData pData = m_DataModuleList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = pData.m_Address.ToString();
                strHead[3] = pData.m_DataUnit;
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);
        }
        public override void AddNewDataRecord(CDevModuleData pNewData)
        {
            m_DataModuleList.Add(pNewData);
            string[] strHead = new string[4];

            strHead[0] = pNewData.m_DataNo.ToString();
            strHead[1] = pNewData.m_DataName;
            strHead[2] = pNewData.m_Address.ToString();
            strHead[3] = pNewData.m_DataUnit;
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
            m_ListView.TopItem = m_ListView.Items[m_ListView.TopItem.Index];
        }
        public override void AddNewRow()
        {
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "电量" + pNewData.m_DataNo.ToString();
            pNewData.m_Address = GetNowAddress();
            pNewData.m_DataUnit = "";
            AddNewDataRecord(pNewData);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            CDevModuleData pNewData = new CDevModuleData();
            pNewData.m_DataNo = m_DataModuleList.Count + 1;
            pNewData.m_DataName = "电量" + pNewData.m_DataNo.ToString();
            pNewData.m_Address = GetNowAddress();
            pNewData.m_DataUnit = "";
            m_DataModuleList.Insert(nPlaceIndex, pNewData);
            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            if (nPlaceIndex >= m_DataModuleList.Count) return;
            m_DataModuleList.RemoveAt(nPlaceIndex);
            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataModuleList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public override void ClearAllRow()
        {
            m_DataModuleList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataModuleList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataModuleList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataModuleList[m_NowSelectIndex].m_Address = Convert.ToInt32((string)pData);
                        break;
                    case 3:
                        m_DataModuleList[m_NowSelectIndex].m_DataUnit = (string)pData;
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + m_DataTableName;
            m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            int i;
            for (i = 0; i < m_DataModuleList.Count; i++)
            {
                strSql = "insert into " + m_DataTableName + " values(" +
                    m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                    m_DataModuleList[i].m_DataName + "','" +
                    m_DataModuleList[i].m_DataUnit + "'," +
                    m_DataModuleList[i].m_Address.ToString() + ")";
                m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            }
        }
        public override void SetFaultAddress()
        {
            for (int i = 0; i < m_DataModuleList.Count; i++)
            {
                m_DataModuleList[i].m_Address = m_FaultStartAddress + i;
            }
            IniShowListView(m_ListView);
        }
    }
    public class CDevParaGroup : CBaseModuleGroup
    {
        public CDevParaGroup()
        {
            m_FaultStartAddress = 0x8001;
            m_NodesName = "参数配置";
            m_DataType = 4;
        }
    }
    public class CDevModuleLineSet : CBaseModuleGroup
    {
        public List<MainRealYc> m_MainRealList = new List<MainRealYc>();
        public int m_LineCount = 1;
        public CDevModuleLineSet()
        {
            m_NodesName = "线路配置";
            m_DataType = 6;
        }
        public override void IniReadDataList()
        {
            string strSql;
            DataTable pDataTable;
            int nRow;
            int i;
            strSql = "SELECT  * FROM  " + m_DataTableName + "  order by LineNo";
            pDataTable = (DataTable)m_TheRoot.m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            m_MainRealList.Clear();
            if (pDataTable == null) return;
            nRow = pDataTable.Rows.Count;
            for (i = 0; i <= m_LineCount; i++)
            {
                MainRealYc pIndex = new MainRealYc();
                if (i == 0) 
                    pIndex.m_LineName = "母线电压索引配置";
                else
                    pIndex.m_LineName = (i-1).ToString() + "#线路索引配置";
                pIndex.m_LineNo = i;
                if (i < nRow)
                {
                    for (int j = 1; j <= pIndex.m_DataList.Count; j++)
                    {
                        pIndex.m_DataList[j - 1] = m_TheRoot.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pDataTable, i, j);
                    }
                }
                m_MainRealList.Add(pIndex);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);
            int nWith = 50;
            pListView.ClearAllItem();
            pListView.Columns.Add("名称", 120, HorizontalAlignment.Center);
            pListView.Columns.Add("CIa", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("CIb", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("CIc", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("BIa", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("BIb", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("BIc", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Io", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("ΦA", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("ΦB", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("ΦC", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Ua", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Ub", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Uc", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Uab", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Ubc", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Uca", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Uo", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("P", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Q", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Cos", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("F", nWith, HorizontalAlignment.Center);
            pListView.Columns.Add("Yx", nWith, HorizontalAlignment.Center);

            string[] strHead = new string[23];
            int i;
            for (i = 0; i < m_MainRealList.Count; i++)
            {
                MainRealYc pIndex = m_MainRealList[i];
                strHead[0] = pIndex.m_LineName;
                for (int j = 0; j < pIndex.m_DataList.Count; j++)
                {
                    strHead[j + 1] = pIndex.m_DataList[j].ToString();
                }
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            for (i = 1; i < pListView.Columns.Count; i++) pListView.SetColumnsEditMode(i);
        }
        public void ClearZero()
        {
            int i, j;
            for (i = 0; i < m_ListView.Items.Count; i++)
            {
                for (j = 1; j < m_ListView.Columns.Count; j++)
                {
                    m_ListView.Items[i].SubItems[j].Text = "0";
                }
            }
        }
        public void FastSet(int nSpaceCount)
        {
            int i, j;
            for (i = 2; i < m_ListView.Items.Count; i++)
            {
                for (j = 1; j < (m_ListView.Columns.Count-1); j++)
                {
                    try
                    {
                        if (Convert.ToInt32(m_ListView.Items[1].SubItems[j].Text) != 0)
                        {
                            m_ListView.Items[i].SubItems[j].Text = (Convert.ToInt32(m_ListView.Items[1].SubItems[j].Text) +
                                nSpaceCount * (i - 1)).ToString();
                        }
                    }
                    catch 
                    {
                        m_ListView.Items[i].SubItems[j].Text = "0";
                    }
                }
            }
        }
        private void GetSetValue()
        {
            int i, j;
            for (i = 0; i < m_ListView.Items.Count; i++)
            {
                for (j = 1; j < m_ListView.Columns.Count; j++)
                {
                    try
                    {
                        m_MainRealList[i].m_DataList[j - 1] = Convert.ToInt32(m_ListView.Items[i].SubItems[j].Text);
                    }
                    catch
                    {
                        m_MainRealList[i].m_DataList[j - 1] = 0;
                    }
                }
            }
        }
        public override void SaveNowSet()
        {
            GetSetValue();
            string strSql;
            int i,j;
            strSql = "delete from " + m_DataTableName;
            m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            for (i = 0; i < m_ListView.Items.Count; i++)
            {
                strSql = "insert into " + m_DataTableName + " values(" + i.ToString() + ",";
                for (j = 0; j < m_MainRealList[i].m_DataList.Count; j++)
                {
                    strSql += m_MainRealList[i].m_DataList[j].ToString();
                    if (j < (m_MainRealList[i].m_DataList.Count - 1))
                        strSql += ",";
                    else
                        strSql += ")";
                }
                m_TheRoot.m_DeviceMsg.m_ModuleDB.ExecSql(strSql);
            }
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {

        }
    }
    public class CDeviceMudule
    {
        public CModuleClass m_DeviceMsg = new CModuleClass();
        public TreeNode m_Node = null;
        public CDevYcGroup m_YcDataGroup = new CDevYcGroup();
        public CDevYxGroup m_YxDataGroup = new CDevYxGroup();
        public CDevYkGroup m_YkDataGroup = new CDevYkGroup();
        public CDevPowerGroup m_PowerDataGroup = new CDevPowerGroup();
        public CDevParaGroup m_ParaDataGroup = new CDevParaGroup();
        public CDevModuleLineSet m_DevLineSet = new CDevModuleLineSet();

        public string m_YcTableName;
        public string m_YxTableName;
        public string m_YkTableName;
        public string m_PowerTableName;
        public string m_SoeTableName;
        public string m_ParaRootTableName;
        public string m_ParaGroupTableName;
        public string m_ParaDataTableName;
        public string m_RealTableName;
        public List<CBaseModuleGroup> m_GroupList = new List<CBaseModuleGroup>();

        public bool m_bDefaultFlag = false;

        public CDeviceMudule()
        {
            m_GroupList.Add(m_YcDataGroup);
            m_GroupList.Add(m_YxDataGroup);
            m_GroupList.Add(m_YkDataGroup);
            m_GroupList.Add(m_PowerDataGroup);
            m_GroupList.Add(m_ParaDataGroup);
            m_GroupList.Add(m_DevLineSet);
        }
        public void InitialzeReadAllData()
        {
            IniAllDataTableName();
            m_YcDataGroup.m_TheRoot = this;
            m_YxDataGroup.m_TheRoot = this;
            m_YkDataGroup.m_TheRoot = this;
            m_PowerDataGroup.m_TheRoot = this;
            m_ParaDataGroup.m_TheRoot = this;
            m_DevLineSet.m_TheRoot = this;

            m_YcDataGroup.IniReadDataList();
            m_YxDataGroup.IniReadDataList();
            m_YkDataGroup.IniReadDataList();
            m_PowerDataGroup.IniReadDataList();
            m_ParaDataGroup.IniReadDataList();
            m_DevLineSet.IniReadDataList();
        }
        public bool CheckNameExist(string strNewName)
        {
            string strPath = "DevModuleDataBase\\" + strNewName + ".mdb";
            if (File.Exists(strPath))
            {
                return true;
            }
            strPath = "DefaultDevModule\\" + strNewName + ".mdb";
            if (File.Exists(strPath))
            {
                return true;
            }
            return false;
        }
        public void ReNameDeviceName(string strNewName)
        {
            m_DeviceMsg.m_ModuleDB.CloseDatabase();
            string[] pStr = m_DeviceMsg.m_ModuleDB.m_Path.Split('\\');
            string NewNamePath = pStr[0] + "\\" + strNewName + ".mdb";
            FileInfo fileInfo = new FileInfo(m_DeviceMsg.m_ModuleDB.m_Path);
            fileInfo.MoveTo(NewNamePath);
            m_DeviceMsg.m_ModuleDB.m_Path = NewNamePath;
            m_DeviceMsg.m_ModuleDB.ConnectDataBase();
        }
        public void IniAllDataTableName()
        {
            m_YcTableName = "Mod_YcTable";
            m_YxTableName = "Mod_YxTable";
            m_YkTableName = "Mod_YkTable";
            m_PowerTableName = "Mod_PowerTable";
            m_SoeTableName = "Mod_SoeTable";
            m_ParaRootTableName = "Mod_ParaRoot";
            m_ParaGroupTableName = "Mod_ParaGroup";
            m_ParaDataTableName = "Mod_ParaTable";
            m_RealTableName = "Mod_MainRealTable";

            m_YcDataGroup.m_DataTableName = m_YcTableName;
            m_YxDataGroup.m_DataTableName = m_YxTableName;
            m_YkDataGroup.m_DataTableName = m_YkTableName;
            m_PowerDataGroup.m_DataTableName = m_PowerTableName;
            m_ParaDataGroup.m_DataTableName = m_ParaDataTableName;
            m_DevLineSet.m_DataTableName = m_RealTableName;
        }
        public void DropDevTable()
        {
            m_DeviceMsg.m_ModuleDB.CloseDatabase();
            File.Delete(m_DeviceMsg.m_ModuleDB.m_Path); 
            if (m_Node != null) m_Node.Remove();
        }
        public void ReloadDefault()
        {
            InitialzeReadAllData();
        }
    }
    public class CDeviceRoot
    {
        public string m_DevRootName;
        public static int m_DevType = 0;
        public int m_ThisDevType = 0;
        public List<CDeviceMudule> m_DeviceModuleList = new List<CDeviceMudule>();
        public CDeviceRoot()
        {
            m_DevType++;
            m_ThisDevType = m_DevType;
        }
        public  void GetDevModuleFile(string strFloder,bool bChangeFlag)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(strFloder);
            if (TheFolder == null) return;
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Extension != ".mdb") continue;
                CDeviceMudule pNewDevice = new CDeviceMudule();
                pNewDevice.m_bDefaultFlag = bChangeFlag;
                pNewDevice.m_DeviceMsg.m_ModuleDB.m_Path = strFloder + "\\" + NextFile.Name;
                string[] pStr = NextFile.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                pNewDevice.m_DeviceMsg.m_ModuleName = pStr[0];
                pNewDevice.m_DeviceMsg.m_ModuleDB.ConnectDataBase();
                pNewDevice.m_DeviceMsg.InitialzeReadOption();
                pNewDevice.m_DeviceMsg.m_ModuleNo = m_DeviceModuleList.Count + 1;
                pNewDevice.InitialzeReadAllData();
                m_DeviceModuleList.Add(pNewDevice);
            }
        }
        public void CloseDataBase()
        {
            for (int i = 0; i < m_DeviceModuleList.Count; i++)
            {
                m_DeviceModuleList[i].m_DeviceMsg.m_ModuleDB.CloseDatabase();
            }
        }
        public void LoadDeviceModuleSet()
        {
            m_DeviceModuleList.Clear();
            GetDevModuleFile("DefaultDevModule",true);
            GetDevModuleFile("DevModuleDataBase",false);
        }
        public bool CheckNameExist(string strNewName)
        {
            string strPath = "DevModuleDataBase\\" + strNewName + ".mdb";
            if (File.Exists(strPath))
            {
                return true;
            }
            strPath = "DefaultDevModule\\" + strNewName + ".mdb";
            if (File.Exists(strPath))
            {
                return true;
            }
            return false;
        }
        public CDeviceMudule AddNewDevice()
        {
            string strName = Interaction.InputBox("请输入新建装置名称", "名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0) return null;
            if (CheckNameExist(strName)) return null;

            string strSourceFile = "DefaultDevModule\\空模板.mdb";
            string strItemFile = "DevModuleDataBase\\" + strName + ".mdb";
            if (File.Exists(strSourceFile) == false) return null;

            CDeviceMudule pNewDevice = new CDeviceMudule();

            pNewDevice.m_DeviceMsg.m_ModuleNo = m_DeviceModuleList.Count + 1;
            pNewDevice.m_DeviceMsg.m_ModuleName = strName;
            pNewDevice.m_DeviceMsg.m_ModuleType = m_ThisDevType;
            pNewDevice.m_DeviceMsg.m_LineCount = 16;
            pNewDevice.m_DeviceMsg.m_DevAddress = 1;

            File.Copy(strSourceFile, strItemFile);
            pNewDevice.m_DeviceMsg.m_ModuleDB.m_Path = strItemFile;
            pNewDevice.m_DeviceMsg.m_ModuleDB.ConnectDataBase();
            pNewDevice.m_DeviceMsg.UpdateMsg();
        //    pNewDevice.IniAllDataTableName();
            pNewDevice.InitialzeReadAllData();
            m_DeviceModuleList.Add(pNewDevice);
            return pNewDevice;
        }
        public CDeviceMudule CopyDevice(CDeviceMudule   pBasicDev)
        {
            if (pBasicDev == null) return null;
            string strName = Interaction.InputBox("请输入拷贝后装置名称", "名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0) return null;
            if (CheckNameExist(strName)) return null;

            string strSourceFile = pBasicDev.m_DeviceMsg.m_ModuleDB.m_Path;
            string strItemFile = "DevModuleDataBase\\" + strName + ".mdb";


            CDeviceMudule pNewDevice = new CDeviceMudule();
            pNewDevice.m_DeviceMsg.m_ModuleNo = m_DeviceModuleList.Count + 1;
            pNewDevice.m_DeviceMsg.m_ModuleName = strName;
            pNewDevice.m_DeviceMsg.m_ModuleType = m_ThisDevType;
            pNewDevice.m_DeviceMsg.m_LineCount = pBasicDev.m_DeviceMsg.m_LineCount;
            pNewDevice.m_DeviceMsg.m_DevAddress = 1;

            
            System.IO.File.Copy(strSourceFile, strItemFile, true);
            pNewDevice.m_DeviceMsg.m_ModuleDB.m_Path = strItemFile;
            pNewDevice.m_DeviceMsg.m_ModuleDB.ConnectDataBase();
            pNewDevice.m_DeviceMsg.UpdateMsg();
            pNewDevice.InitialzeReadAllData();
            m_DeviceModuleList.Add(pNewDevice);
            return pNewDevice;
        }
        public void DeleteDevice(CDeviceMudule   pBasicDev)
        {
            if(pBasicDev == null) return;
            pBasicDev.DropDevTable();
            m_DeviceModuleList.Remove(pBasicDev);
        }
    }
}
