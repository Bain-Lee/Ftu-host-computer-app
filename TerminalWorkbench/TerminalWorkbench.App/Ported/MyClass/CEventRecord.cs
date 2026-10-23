using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtuTestManager
{
    public class CEventRecord
    {
        public const int RealGroupCount = 200;
        private string[] RealEventGroup = new string[RealGroupCount];
        private int nEvent_StartPoint = 0;
        private int nEvent_EndPoint = 0;
        public CEventRecord()
        {

        }
        public void AddRealMsg(string strInfo)
        {
            RealEventGroup[nEvent_EndPoint] = strInfo;
            nEvent_EndPoint++;
            nEvent_EndPoint %= RealGroupCount;
        }
        public string GetNewestMsg()
        {
            if (nEvent_StartPoint != nEvent_EndPoint)
            {
                string pData = RealEventGroup[nEvent_StartPoint];
                nEvent_StartPoint++;
                nEvent_StartPoint %= RealGroupCount;
                return pData;
            }
            return null;
        }
    }
}
