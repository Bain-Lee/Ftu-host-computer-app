using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtuTestManager
{
    class CTransGlobal
    {
        public static List<CTranValue> m_DefaultPriValueList = new List<CTranValue>();
        public static List<CTranValue> m_DefaultTermValueList = new List<CTranValue>();

        public static void InitPriDafault()
        {   
            m_DefaultPriValueList.Clear();
            for (int i = 0; i < 58; i++)
            {
                CTranValue temp = new CTranValue();
                m_DefaultPriValueList.Add(temp);
            }
            CDeviceConfig pNewDevice = new CDeviceConfig();
            pNewDevice.IniValueList();
            m_DefaultPriValueList = pNewDevice.m_ValueList;
            int nP = 0;
            m_DefaultPriValueList[nP++].m_TranValue = 1;
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "ConsolePriStation";
            m_DefaultPriValueList[nP++].m_TranValue = 1;
            m_DefaultPriValueList[nP++].m_TranValue = "TcpServerChannel";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "1024";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "0";
            m_DefaultPriValueList[nP++].m_TranValue = "S104_Console";
            m_DefaultPriValueList[nP++].m_TranValue = "S104_ConsoleCfg";
            m_DefaultPriValueList[nP++].m_TranValue = "FALSE";
            m_DefaultPriValueList[nP++].m_TranValue = "TRUE";
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = "TextLog";
            m_DefaultPriValueList[nP++].m_TranValue = 200;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = "TextLog";
            m_DefaultPriValueList[nP++].m_TranValue = 200;
            m_DefaultPriValueList[nP++].m_TranValue = "FALSE";
            m_DefaultPriValueList[nP++].m_TranValue = "FALSE";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = " PriYxTab0";
            m_DefaultPriValueList[nP++].m_TranValue = -1;
            m_DefaultPriValueList[nP++].m_TranValue = " PriYcTab0";
            m_DefaultPriValueList[nP++].m_TranValue = -1;
            m_DefaultPriValueList[nP++].m_TranValue = " PriYkTab0";
            m_DefaultPriValueList[nP++].m_TranValue = -1;
            m_DefaultPriValueList[nP++].m_TranValue = " PriSetTab0";
            m_DefaultPriValueList[nP++].m_TranValue = -1;
            m_DefaultPriValueList[nP++].m_TranValue = " PriPowerTab0";
            m_DefaultPriValueList[nP++].m_TranValue = -1;
            m_DefaultPriValueList[nP++].m_TranValue = "FALSE";
            m_DefaultPriValueList[nP++].m_TranValue = "TRUE";
            m_DefaultPriValueList[nP++].m_TranValue = "Soerecord";
            m_DefaultPriValueList[nP++].m_TranValue = 1024;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
            m_DefaultPriValueList[nP++].m_TranValue = "NULL";
            m_DefaultPriValueList[nP++].m_TranValue = 0;
        }
        public static void InitTermDafault()
        {
            m_DefaultTermValueList.Clear();
            for (int i = 0; i < 58; i++)
            {
                CTranValue temp = new CTranValue();
                m_DefaultTermValueList.Add(temp);
            }
            CDeviceConfig pNewDevice = new CDeviceConfig();
            pNewDevice.IniValueList();
            m_DefaultTermValueList = pNewDevice.m_ValueList;
            int nP = 0;
            m_DefaultTermValueList[nP++].m_TranValue = 1;
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "BF533Terminal";
            m_DefaultTermValueList[nP++].m_TranValue = 1;
            m_DefaultTermValueList[nP++].m_TranValue = "SerialPortChannel";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "COM1";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "0";
            m_DefaultTermValueList[nP++].m_TranValue = "H101_GDW";
            m_DefaultTermValueList[nP++].m_TranValue = "H101_GDWCfg";
            m_DefaultTermValueList[nP++].m_TranValue = "TRUE";
            m_DefaultTermValueList[nP++].m_TranValue = "TRUE";
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = "TextLog";
            m_DefaultTermValueList[nP++].m_TranValue = 200;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = "TextLog";
            m_DefaultTermValueList[nP++].m_TranValue = 200;
            m_DefaultTermValueList[nP++].m_TranValue = "FALSE";
            m_DefaultTermValueList[nP++].m_TranValue = "FALSE";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = " TerYxTab0";
            m_DefaultTermValueList[nP++].m_TranValue = -1;
            m_DefaultTermValueList[nP++].m_TranValue = " TerYcTab0";
            m_DefaultTermValueList[nP++].m_TranValue = -1;
            m_DefaultTermValueList[nP++].m_TranValue = " TerYkTab0";
            m_DefaultTermValueList[nP++].m_TranValue = -1;
            m_DefaultTermValueList[nP++].m_TranValue = " TerSetTab0";
            m_DefaultTermValueList[nP++].m_TranValue = -1;
            m_DefaultTermValueList[nP++].m_TranValue = " TerPowerTab0";
            m_DefaultTermValueList[nP++].m_TranValue = -1;
            m_DefaultTermValueList[nP++].m_TranValue = "FALSE";
            m_DefaultTermValueList[nP++].m_TranValue = "TRUE";
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
            m_DefaultTermValueList[nP++].m_TranValue = "NULL";
            m_DefaultTermValueList[nP++].m_TranValue = 0;
        }

        public static void InitDafault()
        {
            InitPriDafault();
            InitTermDafault();
        }
    }
}
