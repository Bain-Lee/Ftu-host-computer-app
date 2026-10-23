using System.Collections.Generic;
using System.Linq;

namespace DtuTestManager
{
    internal static class ModelCopies
    {
        internal static List<CTranValue> CopyValues(List<CTranValue> source)
        {
            return source.Select(value => new CTranValue
            {
                m_ValueType = value.m_ValueType,
                m_EditMode = value.m_EditMode,
                m_ComStringList = value.m_ComStringList == null ? null : (string[])value.m_ComStringList.Clone(),
                m_GroupNo = value.m_GroupNo,
                m_StrColName = value.m_StrColName,
                m_StrDataName = value.m_StrDataName,
                m_StringLeng = value.m_StringLeng,
                m_bChange = value.m_bChange,
                m_TranValue = value.m_TranValue
            }).ToList();
        }
        internal static List<CTermData> CopyTerminalData(List<CTermData> source)
        {
            return source.Select(value => new CTermData
            {
                nRealCnt = value.nRealCnt,
                m_DataName = new List<string>(value.m_DataName),
                m_isReadDataName = value.m_isReadDataName
            }).ToList();
        }
    }
}
