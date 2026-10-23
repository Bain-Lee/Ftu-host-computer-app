using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.OleDb;


namespace DtuTestManager
{
    public class CTransSetDB : CTrasnSingleTable<CSetData>
    {
        public CTransSetDB()
        {
            m_tableType = 3;
            CSetData m_ColNameData = new CSetData();
            for (int i = 0; i < m_ColNameData.m_ValueList.Count; i++)
            {
                m_ValueList.Add(m_ColNameData.m_ValueList[i]);
            }
        }
        private void ResetDataNoID()
        {
            for (int i = 0; i < m_DataList.Count; i++)
            {
                m_DataList[i].m_ValueList[0].m_TranValue = i + 1;
                m_DataList[i].m_ValueList[1].m_TranValue = i;
            }
        }
        public override CTranValue GetTranValue(int nRow, int nCol)
        {
            if (nRow < m_DataList.Count)
            {
                if (nCol < m_DataList[nRow].m_ValueList.Count)
                    return m_DataList[nRow].m_ValueList[nCol];
            }
            return null;
        }
     
        public override void AddNewData()
        {
            base.AddNewData();
            CSetData pRowData = m_DataList.Last();
            pRowData.SetDefaultValue();
            ResetDataNoID();
        }
        public override void RemoveAtData(int nIndex)
        {
            base.RemoveAtData(nIndex);
            ResetDataNoID();
        }
        public override void IniReadTableData()
        {
            string strSql;
            DataTable pTable;
            ClearAllData();
            if (m_TableName == "NULL" || m_TableName == "null") return;
            strSql = "SELECT  * FROM  " + m_TableName + " order by ID";
            pTable = (DataTable)m_SqlDataBase.SelectDataTable(strSql);
            if (pTable == null) return;

            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CSetData pNewData = new CSetData();
                m_DataList.Add(pNewData);
                for (int j = 0; j < pNewData.m_ValueList.Count; j++)
                {
                    CTranValue pData = pNewData.m_ValueList[j];
                    pData.m_TranValue = m_SqlDataBase.GetTableString(pTable, i, j);
                }
            }
        }
        public override int SaveTableContent()
        {
            int i, j;
            string strSql;
            DbCommand cmd = m_SqlDataBase.m_TranConn.CreateCommand();
            // 添加参数
            cmd.Parameters.Add(cmd.CreateParameter());
            DbTransaction trans = m_SqlDataBase.m_TranConn.BeginTransaction();
            try
            {
                strSql = "delete from " + m_TableName;
                cmd.CommandText = strSql;
                cmd.Parameters[0].Value = "0";
                cmd.ExecuteNonQuery();

                for (i = 0; i < m_DataList.Count; i++)
                {
                    strSql = "insert into " + m_TableName + " values(";
                    for (j = 0; j < m_DataList[i].m_ValueList.Count; j++)
                    {
                        if (j > 0) strSql += ",";
                        strSql += m_DataList[i].m_ValueList[j].GetAddStringDesc();
                    }
                    strSql += ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = (i + 1).ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return m_DataList.Count;
            }
            catch (OleDbException)
            {
                trans.Rollback();
                throw;
            }
        }
        public override void UpdateTranValue(int nIndex, object pValue)
        {
            if (m_SelectIndex < m_DataList.Count)
            {
                if (nIndex < m_DataList[m_SelectIndex].m_ValueList.Count)
                    m_DataList[m_SelectIndex].m_ValueList[nIndex].m_TranValue = pValue;
            }
        }
    }
    public class CSetData : CTranValueList
    {
        public CSetData()
        {
            string[] strList1 = { "TRUE", "FALSE" };
            AddNewValue(0, "ID", "自动序号", 0);
            AddNewValue(0, "DataNo", "数据序号", 0);
            AddNewValue(2, "SetVal0", "定值0", 0);
            AddNewValue(2, "SetVal1", "定值1", 0);
            AddNewValue(0, "ValType", "定值类型", 0);

        }
        public void SetDefaultValue()
        {
            m_ValueList[0].m_TranValue = 0;
            m_ValueList[1].m_TranValue = 0;
            m_ValueList[2].m_TranValue = 1;
            m_ValueList[3].m_TranValue = 0;
            m_ValueList[4].m_TranValue = 1;

        }
    }
}
