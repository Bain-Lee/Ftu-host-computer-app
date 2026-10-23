using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;

namespace DtuTestManager
{
    public class CBattertyAcv : CTrasnSingleTable<CBatteryData>
    {
        public CBattertyAcv()
        {
            m_tableType = 6;
            CBatteryData m_ColNameData = new CBatteryData();
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
            CBatteryData pRowData = m_DataList.Last();
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
            if (m_TableName == "NULL" || m_TableName == "null") return;
            strSql = "SELECT  * FROM  " + m_TableName + " order by ID";
            pTable = (DataTable)m_SqlDataBase.SelectDataTable(strSql);
            if (pTable == null) return;

            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CBatteryData pNewData = new CBatteryData();
                m_DataList.Add(pNewData);
                for (int j = 0; j < pNewData.m_ValueList.Count; j++)
                {
                    CTranValue pData = pNewData.m_ValueList[j];
                    pData.m_TranValue = m_SqlDataBase.GetTableString(pTable, i, j);
                }
            }
        }
        public override int  SaveTableContent()
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
                    cmd.Parameters[0].Value = (i+1).ToString();
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
    public class CBatteryData : CTranValueList
    {
        public CBatteryData()
        {
            AddNewValue(0, "ID", "自动序号", 0);
            AddNewValue(0, "DataNo", "数据序号", 0);
            AddNewValue(2, "ActiveTime", "活化时间", 0);
        }
        public void SetDefaultValue()
        {
            m_ValueList[0].m_TranValue = 0;
            m_ValueList[1].m_TranValue = 0;
            m_ValueList[2].m_TranValue = 0;
        }
    }
}
