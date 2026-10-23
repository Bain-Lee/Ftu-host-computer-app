using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace DtuTestManager
{
    public class CSoeCosRecord
    {
        public const int  RealGroupCount = 200;      
        private CSOEOption[] RealSOEGroup = new CSOEOption[RealGroupCount];  //实时SOE信息表
        private int nSOE_StartPoint = 0;
        private int nSOE_EndPoint = 0;   
        public CSoeCosRecord()
        {
            for (int i = 0; i < RealSOEGroup.Length; i++) RealSOEGroup[i] = new CSOEOption();
        }     
        public  void AddRealMsg(int nYxNo, int nYxValue, string YxName,string strTimeMsg,string strStatus)
        {
            RealSOEGroup[nSOE_EndPoint].m_YxOption.m_YxNo = nYxNo;
            RealSOEGroup[nSOE_EndPoint].m_YxOption.m_YxName = YxName;
            RealSOEGroup[nSOE_EndPoint].m_YxOption.m_YxValue = nYxValue;
            RealSOEGroup[nSOE_EndPoint].m_YxOption.m_YxChangeTime = strTimeMsg;
            RealSOEGroup[nSOE_EndPoint].m_StatusDesc = strStatus;
            RealSOEGroup[nSOE_EndPoint].m_RecordTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            CSOEOption pNewRecord = new CSOEOption(RealSOEGroup[nSOE_EndPoint]);       
            nSOE_EndPoint++;
            nSOE_EndPoint %= RealGroupCount;
        }
        public CSOEOption GetNewestMsg()
        {
            if (nSOE_StartPoint != nSOE_EndPoint)
            {
                CSOEOption pData = RealSOEGroup[nSOE_StartPoint];
                nSOE_StartPoint++;
                nSOE_StartPoint %= RealGroupCount;
                return pData;
            }
            return null;
        }  
    }
}
