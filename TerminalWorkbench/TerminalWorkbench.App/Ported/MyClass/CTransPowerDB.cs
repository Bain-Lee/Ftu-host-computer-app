using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.Common;
using Microsoft.Data.Sqlite;
namespace DtuTestManager
{
    public class CTransPowerDB : CTrasnSingleTable<CPowerData>
    {
        public CTransPowerDB()
        {
            m_tableType = 4;
            CPowerData m_ColNameData = new CPowerData();
            for (int i = 0; i < m_ColNameData.m_ValueList.Count; i++)
            {
                m_ValueList.Add(m_ColNameData.m_ValueList[i]);
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
            CPowerData pRowData = m_DataList.Last();
            pRowData.SetDefaultValue();
            m_DataList[m_DataList.Count - 1].m_ValueList[0].m_TranValue = m_DataList.Count;
            m_DataList[m_DataList.Count - 1].m_ValueList[1].m_TranValue = m_DataList.Count - 1;
        }
        public override void RemoveAtData(int nIndex)
        {
            base.RemoveAtData(nIndex);

        }
        public override void IniReadTableData()
        {
            string strSql;
            DataTable pTable;
            ClearAllData();
            if (m_TableName == "NULL" || m_TableName == "null")
                return;
            strSql = "SELECT  * FROM  " + m_TableName + " order by ID";
            pTable = (DataTable)m_SqlDataBase.SelectDataTable(strSql);
            if (pTable == null)
                return;

            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPowerData pNewData = new CPowerData();
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

            using DbCommand cmd = m_SqlDataBase.m_TranConn.CreateCommand();

            using DbTransaction trans = m_SqlDataBase.m_TranConn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                strSql = "delete from " + m_TableName;
                cmd.CommandText = strSql;

                cmd.ExecuteNonQuery();

                for (i = 0; i < m_DataList.Count; i++)
                {
                    strSql = "insert into " + m_TableName + " values(";
                    for (j = 0; j < m_DataList[i].m_ValueList.Count; j++)
                    {
                        if (j > 0)
                            strSql += ",";
                        strSql += m_DataList[i].m_ValueList[j].GetAddStringDesc();
                    }
                    strSql += ")";
                    cmd.CommandText = strSql;

                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return m_DataList.Count;
            }
            catch
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
    public class CPowerData : CTranValueList
    {
        public CPowerData()
        {
            string[] strList1 = { "TRUE", "FALSE" };
            AddNewValue(0, "ID", "自动序号", 0);
            AddNewValue(0, "DataNo", "数据序号", 0);
            AddNewValue(0, "PowerType", "遥脉类型", 0);
            AddNewValue(0, "Freeze", "冻结", 0);
            AddNewValue(0, "PowerDeadLimit", "遥脉死区", 0);
            AddNewValue(1, "FloatPowerDeadLimit", "浮点型遥脉死区", 0);
            AddNewValue(1, "PowerMul", "遥脉系数", 0);
            AddNewValue(0, "PowerPlus", "遥脉偏移", 0);
            AddNewValue(1, "PowerFloatPlus", "遥脉浮点型偏移", 0);
        }
        public void SetDefaultValue()
        {
            m_ValueList[0].m_TranValue = 0;
            m_ValueList[1].m_TranValue = 0;
            m_ValueList[2].m_TranValue = 2;
            m_ValueList[3].m_TranValue = 0;
            m_ValueList[4].m_TranValue = 20;
            m_ValueList[5].m_TranValue = 0.2;
            m_ValueList[6].m_TranValue = 1;
            m_ValueList[7].m_TranValue = 0;
            m_ValueList[8].m_TranValue = 0;
        }
    }
}
