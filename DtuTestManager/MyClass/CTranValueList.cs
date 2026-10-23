using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtuTestManager
{
    public class CTranValueList
    {
        public List<CTranValue> m_ValueList = new List<CTranValue>();
        public void AddNewValue(int nValueType, string strColName, string strDataName, int nGroupNo, int nStringLeng = 0, string[] pStrList = null)
        {
            CTranValue pNewValue = new CTranValue();            
            pNewValue.m_GroupNo = nGroupNo;
            pNewValue.m_ValueType = nValueType;
            pNewValue.m_StrColName = strColName;
            pNewValue.m_StrDataName = strDataName;
            pNewValue.m_StringLeng = nStringLeng;

            if (pStrList != null && pStrList.Length > 0)
            {
                pNewValue.m_ComStringList = new string[pStrList.Length];
                for (int i = 0; i < pStrList.Length; i++) pNewValue.m_ComStringList[i] = pStrList[i];
                pNewValue.m_EditMode = true;
            }
            if (pNewValue.m_ValueType == 2)
                pNewValue.m_TranValue = "NULL";
            else
                pNewValue.m_TranValue = 0;
            m_ValueList.Add(pNewValue);
        }
    }
     [Serializable]
    public class CTranValue
    {
        public int m_ValueType;     //数据类型，0表示整型，1表示浮点型，2表示字符型
        private object m_Value;

        public bool m_EditMode = false;     //编辑模式,false表示编辑模式，true表示选择模式，返回值是整型
        public string[] m_ComStringList;    //选择列表                       

        public int m_GroupNo;
        public string m_StrColName;     //列名
        public string m_StrDataName;    //数据名
        public int m_StringLeng;        //字符最大长度

        public bool m_bChange = false;
        public string GetAddStringDesc()
        {
            string strDesc = "";
            switch (m_ValueType)
            {
                case 0:
                case 1:
                    strDesc = ((string)m_Value);
                    break;
                case 2:
                    strDesc = "'" + ((string)m_Value) + "'";
                    break;
            }
            return strDesc;
        }
        public string GetCreateDesc()
        {
            string strDesc = "";
            switch (m_ValueType)
            {
                case 0:
                    strDesc = m_StrColName + " INTEGER";
                    break;
                case 1:
                    strDesc = m_StrColName + " REAL";
                    break;
                case 2:
                    strDesc = m_StrColName + " VARCHAR(" + m_StringLeng.ToString() + ")";
                    break;
            }
            return strDesc;
        }
        public string GetDataTypeDesc()
        {
            string strType = "";
            switch (m_ValueType)
            {
                case 0:
                    strType = "int";
                    break;
                case 1:
                    strType = "float";
                    break;
                case 2:
                    strType = "string";
                    break;
            }
            return strType;
        }


        public string GetUpdateValueString()
        {
            string strUpdate = "";
            switch (m_ValueType)
            {
                case 0:
                case 1:
                    strUpdate = m_StrColName + "=" + ((string)m_Value);
                    break;
                case 2:
                    strUpdate = m_StrColName + "='" + ((string)m_Value) + "'";
                    break;
            }
            return strUpdate;
        }
        public string GetInsertValueString()
        {
            string strUpdate = "";
            switch (m_ValueType)
            {
                case 0:
                case 1:
                    strUpdate = ((string)m_Value);
                    break;
                case 2:
                    strUpdate =  "'" + ((string)m_Value) + "'";
                    break;
            }
            return strUpdate;
        }
        public void UpateValue(int nIndex, object ValueObject)
        {
            m_bChange = true;
            string strComBoxValue = (string)ValueObject;
            if (m_EditMode)     //选择模式
            {
                if (strComBoxValue.Length < 2) return;
                if (m_ValueType == 0)   //如果是整型
                {
                    m_TranValue = strComBoxValue.Split('.')[0];
                }
                else
                {
                    m_TranValue = strComBoxValue;
                }
            }
            else
            {
                m_TranValue = strComBoxValue;
            }
        }
        public object m_TranValue       //参数值
        {
            set
            {
                try
                {
                    m_Value = Convert.ChangeType(value, typeof(string));
                }
                catch { };
            }
            get
            {
                return (string)m_Value;
            }
        }
    }
}
