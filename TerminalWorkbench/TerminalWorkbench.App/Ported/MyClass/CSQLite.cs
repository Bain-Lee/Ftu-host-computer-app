using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace DtuTestManager
{
    public sealed class CSQLite : IDisposable
    {
        public SqliteConnection m_Dbconn;
        public DbConnection m_TranConn => m_Dbconn;
        public bool ConnectDataBase(string path)
        {
            CloseDatabase();
            m_Dbconn = new SqliteConnection(new SqliteConnectionStringBuilder { DataSource = path }.ToString());
            try
            {
                m_Dbconn.Open();
                return true;
            }
            catch (SqliteException)
            {
                CloseDatabase();
                return false;
            }
        }
        public void CloseDatabase()
        {
            m_Dbconn?.Dispose();
            m_Dbconn = null;
        }
        public DataTable GetTableName()
        {
            if (m_Dbconn == null)
                return null;
            using var command = m_Dbconn.CreateCommand();
            command.CommandText = "SELECT '' AS TABLE_CATALOG, '' AS TABLE_SCHEMA, name AS TABLE_NAME, type AS TABLE_TYPE FROM sqlite_master WHERE type='table' ORDER BY name";
            using var reader = command.ExecuteReader();
            var table = new DataTable();
            table.Load(reader);
            return table;
        }
        public object SelectDataTable(string sql)
        {
            if (m_Dbconn == null)
                return null;
            try
            {
                using var command = m_Dbconn.CreateCommand();
                command.CommandText = sql;
                using var reader = command.ExecuteReader();
                var table = new DataTable();
                table.Load(reader);
                return table;
            }
            catch (SqliteException) { return null; }
        }
        public bool ExecSql(string sql)
        {
            if (m_Dbconn == null)
                return false;
            using var transaction = m_Dbconn.BeginTransaction();
            using var command = m_Dbconn.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SqliteException) { transaction.Rollback(); return false; }
        }
        public int GetTableIntValue(DataTable table, int row, int column)
        {
            string value = GetTableString(table, row, column);
            return value.Length == 0 ? 0 : Convert.ToInt32(value);
        }
        public float GetTableFloatValue(DataTable table, int row, int column)
        {
            string value = GetTableString(table, row, column);
            return value.Length == 0 ? 0 : Convert.ToSingle(value);
        }
        public string GetTableString(DataTable table, int row, int column)
        {
            return table.Rows.Count <= row || table.Columns.Count <= column ? string.Empty : table.Rows[row][column].ToString();
        }
        public void Dispose() => CloseDatabase();
    }
}
