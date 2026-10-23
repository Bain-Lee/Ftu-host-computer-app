using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace DtuTestManager
{
    public class CWaveData
    {
        public int m_WaveNo;            //通道号
        public string m_ChanelName;     //通道名称
        public string m_ChanelType;     //通道类型
        public string m_PhaseType;      //相位特征
        public string m_Cchm;           //回路原件
        public string m_Unit;           //单位
        public float m_Mul = 1;            //增益系数
        public float m_Offset = 0;         //偏移量
        public float m_Skew = 0;           //采样迟滞
        public int m_MinValue;         //理论最小值
        public int m_MaxValue;         //理论最大值
        public float m_Primary = 1;        //一次系数
        public float m_Secondary = 1;      //二次系数
        public int m_PsType = 0;           //一次二次标志，0表示一次，1表示二次
        public int m_Status = 0;           //状态量
        public int m_nModeType = 0;            //类型，0表示模拟量，1表示状态量
        public List<float> m_DataList = new List<float>();
    }
    public  class CWaveFileInfo
    {
        public string m_DeviceType;      //位置名称
        public string m_Serial;          //特征好或名称
        public string m_Version;         //版本号

        public int m_ChanelCount = 0;       //通道数量
        public int m_AnCount = 0;           //模拟量数量
        public int m_DnCount = 0;           //状态量数量

        public float m_Hz;                  //频率
        public int m_Nrates;                //采样率数字
        public int m_Samp;                  //采样频率
        public int m_EndSamp;               //采样数量

        public double m_StartTime;
        public double m_EndTime;
        public string m_strStartTime;
        public string m_strEndTime;
        public bool m_Success = false;      //数据文件读取结果标志
        public int m_DatType = 0;               //数据文件类型,0表示bin,1表示文本模式

        public List<CWaveData> m_ChanelList = new List<CWaveData>();
        public List<int> m_TimeList = new List<int>();

        bool bAnErrBox = false;
        bool bDnErrBox = false;
        string strErrMsg = "";
        public CWaveFileInfo()
        {

        }
        private void DowWithHead(string[] strContent)
        {
            if (strContent.Length != 3)
            {
                strErrMsg += "第一行出错\r\n";
            }
            for (int i = 0; i < strContent.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        m_DeviceType = strContent[0];
                        break;
                    case 1:
                        m_Serial = strContent[1];
                        break;
                    case 2:
                        m_Version = strContent[2];
                        break;
                }
            }
        }
        private void DowWithChanel(string[] strContent)
        {
            if (strContent.Length != 3)
            {
                strErrMsg += "第二行通道配置出错\r\n";
            }
            for (int i = 0; i < strContent.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        m_ChanelCount = Convert.ToInt32(strContent[i]);
                        break;
                    case 1:
                        m_AnCount = Convert.ToInt32(strContent[i].Substring(0, strContent[i].Length - 1));
                        break;
                    case 2:
                        m_DnCount = Convert.ToInt32(strContent[i].Substring(0, strContent[i].Length - 1));
                        break;
                }
            }
        }
        private void DowWithDnChanelMsg(string[] strContent)
        {
            if (strContent.Length != 5 && bDnErrBox == false)
            {
                strErrMsg += "状态量通道数据描述出错\r\n";
                bDnErrBox = true;
            }
            CWaveData pNewWave = new CWaveData();
            pNewWave.m_nModeType = 1;
            m_ChanelList.Add(pNewWave);
            for (int i = 0; i < strContent.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        pNewWave.m_WaveNo = Convert.ToInt32(strContent[i]);
                        break;
                    case 1:
                        pNewWave.m_ChanelType = strContent[i];
                        break;
                    case 2:
                        pNewWave.m_PhaseType = strContent[i];
                        break;
                    case 3:
                        pNewWave.m_Cchm = strContent[i];
                        break;
                    case 4:
                        if (strContent[i].Trim().Length>0)
                            pNewWave.m_Status = Convert.ToInt32(strContent[i]);
                        break;
                }
            }
            if (pNewWave.m_PhaseType != null) pNewWave.m_ChanelName = pNewWave.m_ChanelType + "(" + pNewWave.m_PhaseType + ")";
            if (pNewWave.m_Unit != null && pNewWave.m_Unit.Length > 0) pNewWave.m_ChanelName += "(" + pNewWave.m_Unit + ")";
        }
        private void DowWithAnChanelMsg(string[] strContent)
        {
            if (strContent.Length <= 13 && bAnErrBox == false)
            {
                strErrMsg += "模拟量通道数据描述出错\r\n";
                bAnErrBox = true;
            }
            CWaveData pNewWave = new CWaveData();
            pNewWave.m_nModeType = 0;
            m_ChanelList.Add(pNewWave);
            for(int i=0;i<strContent.Length;i++)
            {
                switch(i)
                {
                    case 0:
                        pNewWave.m_WaveNo = Convert.ToInt32(strContent[i]);
                        break;
                    case 1:
                        pNewWave.m_ChanelType = strContent[i];
                        break;
                    case 2:
                        pNewWave.m_PhaseType = strContent[i];
                        break;
                    case 3:
                        pNewWave.m_Cchm = strContent[i];
                        break;
                    case 4:
                        pNewWave.m_Unit = strContent[i];
                        break;
                    case 5:
                        pNewWave.m_Mul = Convert.ToSingle(strContent[i]);
                        break;
                    case 6:
                        pNewWave.m_Offset = Convert.ToSingle(strContent[i]);
                        break;
                    case 7:
                        pNewWave.m_Skew = Convert.ToSingle(strContent[i]);
                        break;
                    case 8:
                        pNewWave.m_MinValue = Convert.ToInt32(strContent[i]);
                        break;
                    case 9:
                        pNewWave.m_MaxValue = Convert.ToInt32(strContent[i]);
                        break;
                    case 10:
                        pNewWave.m_Primary = Convert.ToSingle(strContent[i]);
                        break;
                    case 11:
                        pNewWave.m_Secondary = Convert.ToSingle(strContent[i]);
                        break;
                    case 12:
                        if (strContent[1] == "S" || strContent[1] == "s")
                        {
                            pNewWave.m_PsType = 1;
                        }
                        else
                        {
                            pNewWave.m_PsType = 0;
                        }
                        break;
                }
            }
            if (pNewWave.m_PhaseType != null) pNewWave.m_ChanelName = pNewWave.m_ChanelType + "(" + pNewWave.m_PhaseType + ")";
            if (pNewWave.m_Unit != null && pNewWave.m_Unit.Length > 0) pNewWave.m_ChanelName += "(" + pNewWave.m_Unit + ")";
        }
        private string DoWithTime(string strInputTime, ref double nTime)
        {
            string[] strSplit = strInputTime.Split(',');
            string[] strDate = strSplit[0].Split('/');
            string[] strTime = strSplit[1].Split(':');
            string[] strMill = strTime[2].Split('.');
            string TimeFormat;
            int nYear = Convert.ToInt32(strDate[2]);
            int nMonth = Convert.ToInt32(strDate[1]);
            int nDay = Convert.ToInt32(strDate[0]);

            int nHour = Convert.ToInt32(strTime[0]);
            int nMinute = Convert.ToInt32(strTime[1]);
            int nSecond = Convert.ToInt32(strMill[0]);
            int nMillSecond = Convert.ToInt32(strMill[1].Substring(0,3));

            TimeFormat = nYear.ToString("D4") + "-";
            TimeFormat += nMonth.ToString("D2") + "-";
            TimeFormat += nDay.ToString("D2") + " ";
            TimeFormat += nHour.ToString("D2") + ":";
            TimeFormat += nMinute.ToString("D2") + ":";
            TimeFormat += nSecond.ToString("D2") + ".";
            TimeFormat += nMillSecond.ToString("D3");
            return TimeFormat;
            /*
            DateTime pTime = new DateTime(nYear,nMonth,nDay,nHour,nMinute,nSecond,nMillSecond);
            TimeFormat = pTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            TimeSpan Ticks = new TimeSpan(pTime.Ticks);
            nTime = Ticks.Ticks;
            return TimeFormat;*/

        }
        public void DoWithLuboFile(string[] strCFGFile,string strDataFile)
        {
            if (strCFGFile.Length < 2)
            {
                MessageBox.Show("录波CFG文件初始化出错，停止解析");
                return;
            }
            int i;
            try
            {
                int nOffPlace = 0;
                string[] strValue = null;
                strErrMsg = "";
                DowWithHead(strCFGFile[nOffPlace++].Split(','));
                DowWithChanel(strCFGFile[nOffPlace++].Split(','));
                if (strCFGFile.Length != (m_ChanelCount + 8))
                {
                    strErrMsg += "录波CFG文件数据行出错\r\n";
                }
                if (strCFGFile.Length < (m_ChanelCount + 2))
                {
                    MessageBox.Show("录波CFG文件通道数据出错，停止解析");
                    return;
                }
                
                for (i = 0; i < m_ChanelCount; i++)
                {
                    if(i<m_AnCount)
                        DowWithAnChanelMsg(strCFGFile[nOffPlace++].Split(','));
                    else
                        DowWithDnChanelMsg(strCFGFile[nOffPlace++].Split(','));
                }
                strValue = strCFGFile[nOffPlace++].Split(',');
                m_Hz = Convert.ToSingle(strValue[0]);
                strValue = strCFGFile[nOffPlace++].Split(',');
                m_Nrates = Convert.ToInt32(strValue[0]);
                if (m_Nrates > 1)
                {
                    MessageBox.Show("本软件不支持多采样频率录波解析,停止解析");
                    return;
                }
                strValue = strCFGFile[nOffPlace++].Split(',');
                m_Samp = Convert.ToInt32(strValue[0]);
                m_EndSamp = Convert.ToInt32(strValue[1]);

                m_strStartTime = DoWithTime(strCFGFile[nOffPlace++],ref m_StartTime);
                m_strEndTime = DoWithTime(strCFGFile[nOffPlace++],ref m_EndTime);
           
                m_DatType = 0;
                if (strCFGFile[nOffPlace] == "ASCII" || strCFGFile[nOffPlace] == "ascii")
                {
                    m_DatType = 1;
                }
                m_Success = ReadLuboDataFile(strDataFile);
            }
            catch
            {
                MessageBox.Show("录波CFG文件出现异常,停止解析");
                return;
            }
        }
        private int ByteToInt(Byte b1,Byte b2,Byte b3,Byte b4)
        {
            return b1 | (b2<<8) | (b3<<16) | (b4<<24);
        }
        private bool ReadLuboDataFile(string strDataPath)
        {
            if (File.Exists(strDataPath) == false)
            {
                MessageBox.Show("录波数据文件不存在!");
                return false;
            }
            FileStream ReadFile = new FileStream(strDataPath, FileMode.Open);
            if(ReadFile == null || ReadFile.CanRead == false) return false;
            Byte[] DataArray = new Byte[ReadFile.Length];
            ReadFile.Read(DataArray, 0, DataArray.Length);
            ReadFile.Close();

            if(DataArray.Length == 0) return false;
            int i,j;
            int nPlac = 0;
            int nTimeStamp = 0;
            int nGroupNo = 0;
            try
            {
                for(i=0;i<m_EndSamp;i++)
                {
                    nPlac += 4;     //跳过4字节序号值
                    nTimeStamp = ByteToInt(DataArray[nPlac],DataArray[nPlac+1],DataArray[nPlac+2],DataArray[nPlac+3]);
                    m_TimeList.Add(nTimeStamp);
                    nPlac += 4;
                    for(j=0;j<m_AnCount;j++)
                    {
                        short nValue = (short)(DataArray[nPlac] | (DataArray[nPlac+1]<<8));
                        float fValue = (float)nValue;
                        fValue = m_ChanelList[j].m_Mul * fValue + m_ChanelList[j].m_Offset;
                        m_ChanelList[j].m_DataList.Add(fValue);
                        nPlac += 2;
                    }
                    for(j=0;j<m_DnCount;j++)
                    {
                        nGroupNo = j/8;
                        Byte bValue = DataArray[nPlac + nGroupNo];
                        float fValue;
                        if((bValue & (1<<(j%8))) == 0)
                        {
                            fValue = 0;
                        }
                        else
                        {
                            fValue = 1;
                        }
                        m_ChanelList[j + m_AnCount].m_DataList.Add(fValue);
                    }
                    nGroupNo = m_DnCount / 16;
                    if(m_DnCount % 16 != 0) nGroupNo++;
                    nPlac += nGroupNo * 2;
                }
                if (strErrMsg.Length > 0)
                {
                    strErrMsg = "错误信息：\r\n" + strErrMsg;
                    MessageBox.Show(strErrMsg);
                }
                return true;
            }
            catch
            { 
            }
            return false;
        }
    }
}
