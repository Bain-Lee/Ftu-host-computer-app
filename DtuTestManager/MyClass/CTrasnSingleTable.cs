using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtuTestManager
{
    public  class CTableInterface
    {
        public List<CTranValue> m_ValueList = new List<CTranValue>();              //列数据
        public int m_SelectIndex = -1;
        public CTranValue pData = null;
        public string m_TableName = "NULL";
        public CSQLite m_SqlDataBase = null;
        public int m_tableType = 0;
        public CTableInterface()
        {

        }
        //获取数据条目数
        public virtual int GetRowCount()            {return 0;}
        //获取每一条目所包含的数据数量
        public virtual int GetColCount()            { return 0; }
        //更新表
        public virtual void UpdateTable()           {}
        //获取指定行列对应数据
        public virtual CTranValue GetTranValue(int nRow, int nCol){return null;}
        //获取指定列头数据定义
        public virtual CTranValue GetColNameValue(int nCol)             { return null; }
        // 插入数据
        public virtual  void InsertData(int nIndex) { }
        //添加数据
        public virtual void AddNewData() { }
        //删除数据
        public virtual void RemoveAtData(int nIndex) { }
        //清空数据
        public virtual void ClearAllData()      { }
        //保存数据
        public virtual int SaveTableContent()
        {
            return 0;
        }
        //初始化读取数据库
        public virtual  void IniReadTableData() { }
        //更新列表数据
        public virtual void UpdateTranValue(int nIndex,object   pValue) { }
    }
    public class CTrasnSingleTable<T> : CTableInterface
    {
        public List<T> m_DataList = new List<T>();

        protected int CheckNameChange()
        {
            if (pData == null) return 0;                                    ///删除表
            string strNewname = (string)pData.m_TranValue;
            if (strNewname == "NULL" || strNewname == "null") return 0;     ///删除表
            if (strNewname == m_TableName) return 1;                        ///表名相同，不做改变
            if (m_TableName == "NULL" || m_TableName == "null")
            {
                m_TableName = (string)strNewname;
                return 2;   ///原表为空，重新创建表
            }
            return 3;                                                       ///表名不同，需要重新命名
        }
        protected void CreateTable()
        {
            int i;
            string strSql = "CREATE TABLE " + m_TableName + "(";
            for (i = 0; i < m_ValueList.Count; i++)
            {
                if (i > 0) strSql += ",";
                strSql += m_ValueList[i].GetCreateDesc();
                if (i == 0) strSql += " PRIMARY KEY";
            }
            strSql += ")";
            m_SqlDataBase.ExecSql(strSql);
        }
        protected void ResetTableName()
        {
            string strNewName = (string)pData.m_TranValue;
            string strOldName = m_TableName;
            string strSql = "rename table " + strOldName + " to " + strNewName;
            m_SqlDataBase.ExecSql(strSql);
        }
        protected void DropTable()
        {
            if (m_TableName == "NULL" || m_TableName == "null") return;
            string  strSql = "drop table " + m_TableName;
            m_SqlDataBase.ExecSql(strSql);
        }
        public override CTranValue GetColNameValue(int nCol) 
        {
            if (nCol < 0 || nCol >= m_ValueList.Count) return null;
            return m_ValueList[nCol]; 
        }
        public override void UpdateTable()
        {
            int nRet = CheckNameChange();
            switch (nRet)
            {
                case 0:
                    DropTable();
                    break;
                case 1:
                    break;
                case 2:
                    CreateTable();
                    break;
                case 3:
                    ResetTableName();
                    break;
            }
        }
        public override int GetRowCount()
        {
            return m_DataList.Count;
        }
        public override int GetColCount()
        {
            return m_ValueList.Count;
        }
        public override void InsertData(int nIndex)
        {
            if (nIndex < 0 || nIndex >= m_DataList.Count) return;
            T pT = System.Activator.CreateInstance<T>();
            m_DataList.Insert(nIndex,pT);
        }
        public override void AddNewData()
        {
            T pT = System.Activator.CreateInstance<T>();
            m_DataList.Add(pT);
        }
        public override void RemoveAtData(int nIndex)
        {
            if (nIndex < 0 || nIndex >= m_DataList.Count) return;
            m_DataList.RemoveAt(nIndex);
        }
        public override void ClearAllData() 
        {
            m_DataList.Clear();
        }

    }
}
