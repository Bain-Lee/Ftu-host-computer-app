using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading;
namespace DtuTestManager
{
   public  class CTransSubStation
    {
        public CTransStation m_TheStation = null;
        public TreeNode m_Node = null;
        public List<CTermData> m_TermDataValue = new List<CTermData>();//六种数据的名称 
        public string m_YcTableName;
        public string m_YxTableName;
        public string m_YkTableName;
        public string m_SetTableName;
        public string m_PowerTableName;
        public CTransSubStation()
        {
            for (int i = 0; i < 6; i++)
            {
                CTermData temp = new CTermData();
                m_TermDataValue.Add(temp);
            }          
        }
        public void IniAllDataTableName()
        {
            m_YcTableName = "SubYcDB";
            m_YxTableName = "SubYxDB";
            m_YkTableName = "SubYkDB";
            m_SetTableName = "SubSetDB";
            m_PowerTableName = "SubPowerDB";
        }
       
        public void ReadDataFromINIFile()
        {
            for (int i = 0; i < 5; i++)
            {
                ReadDataINIFile(i);
            }
        }
       
       public void ReadDataINIFile(int NodeNo)
        {
            if (NodeNo >= 6) return;
            string FileName = m_TheStation.m_StationPath + "\\" + "公共配置.ini";
            StringBuilder m_Value = new StringBuilder(100);
            m_TermDataValue[NodeNo].m_DataName.Clear();
            switch (NodeNo)
            {
                case 0:
                    CGlobal.GetPrivateProfileString("DataCount", "YxCount", "0", m_Value, 100, FileName);
                    int YxCnt = Convert.ToInt32(m_Value.ToString());

                    for (int j = 0; j < m_TermDataValue[NodeNo].nRealCnt; j++)
                    {
                        CGlobal.GetPrivateProfileString("YxNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName);
                        string m_name = m_Value.ToString();
                        m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                    }
                    break;
                case 1:     
                    for (int j = 0; j < m_TermDataValue[NodeNo].nRealCnt; j++)
                    {
                        CGlobal.GetPrivateProfileString("YcNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName);
                        string m_name = m_Value.ToString();
                        m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                    }
                    break;
                case 2:                   
                    for (int j = 0; j < m_TermDataValue[NodeNo].nRealCnt; j++)
                    {
                        CGlobal.GetPrivateProfileString("YkNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName);
                        string m_name = m_Value.ToString();
                        m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                    }
                    break;
                case 3:                  
                    for (int j = 0; j < m_TermDataValue[NodeNo].nRealCnt; j++)
                    {
                        CGlobal.GetPrivateProfileString("SetNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName);
                        string m_name = m_Value.ToString();
                        m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                    }
                    break;
                case 4:
                    for (int j = 0; j < m_TermDataValue[NodeNo].nRealCnt; j++)
                    {
                        CGlobal.GetPrivateProfileString("PowerNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName);
                        string m_name = m_Value.ToString();
                        m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                    }
                    break;
                case 5:                   
                    for (int j = 0; j < m_TermDataValue[NodeNo].nRealCnt; j++)
                    {
                        CGlobal.GetPrivateProfileString("ClacYxNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName);
                        string m_name = m_Value.ToString();
                        m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                    }
                    break;
            }
            m_TheStation.m_Sub.m_TermDataValue[NodeNo].m_isReadDataName = true;
        }
       public void WriteDtaToINIFile(int NodeNo, int Cnt)
       {
           string FileName = m_TheStation.m_StationPath + "\\" + "公共配置.ini";          
           switch (NodeNo)
           {
               case 0:
                   CGlobal.WritePrivateProfileString("DataCount", "YxCount", Cnt.ToString(), FileName);
                   for (int i = 0; i < m_TermDataValue[NodeNo].m_DataName.Count; i++)
                   {
                       string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                       CGlobal.WritePrivateProfileString("YxNameList", "Name" + i.ToString(), m_name, FileName);
                   }
                   break;
               case 1:
                   CGlobal.WritePrivateProfileString("DataCount", "YcCount", Cnt.ToString(), FileName);
                   for (int i = 0; i < m_TermDataValue[NodeNo].m_DataName.Count; i++)
                   {
                       string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                       CGlobal.WritePrivateProfileString("YcNameList", "Name" + i.ToString(), m_name, FileName);
                   }
                   break;
               case 2:
                   CGlobal.WritePrivateProfileString("DataCount", "YkCount", Cnt.ToString(), FileName);
                   for (int i = 0; i < m_TermDataValue[NodeNo].m_DataName.Count; i++)
                   {
                       string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                       CGlobal.WritePrivateProfileString("YkNameList", "Name" + i.ToString(), m_name, FileName);
                   }
                   break;
               case 3:
                   CGlobal.WritePrivateProfileString("DataCount", "SetCount", Cnt.ToString(), FileName);
                   for (int i = 0; i < m_TermDataValue[NodeNo].m_DataName.Count; i++)
                   {
                       string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                       CGlobal.WritePrivateProfileString("SetNameList", "Name" + i.ToString(), m_name, FileName);
                   }
                   break;
               case 4:
                   CGlobal.WritePrivateProfileString("DataCount", "PowerCount", Cnt.ToString(), FileName);
                   for (int i = 0; i < m_TermDataValue[NodeNo].m_DataName.Count; i++)
                   {
                       string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                       CGlobal.WritePrivateProfileString("PowerNameList", "Name" + i.ToString(), m_name, FileName);
                   }
                   break;
               case 5:
                   CGlobal.WritePrivateProfileString("DataCount", "ClacYxCount", Cnt.ToString(), FileName);
                   for (int i = 0; i < m_TermDataValue[NodeNo].m_DataName.Count; i++)
                   {
                       string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                       CGlobal.WritePrivateProfileString("ClacYxNameList", "Name" + i.ToString(), m_name, FileName);
                   }
                   break;
           }
           m_TheStation.m_Sub.m_TermDataValue[NodeNo].m_isReadDataName = false;
       }
        public void WriteINIFile()
        {
            string FileName= m_TheStation.m_StationPath + "\\" + "公共配置.ini";
            CGlobal.WritePrivateProfileString("DataCount", "YxCount", m_TermDataValue[0].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "YcCount", m_TermDataValue[1].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "YkCount", m_TermDataValue[2].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "SetCount", m_TermDataValue[3].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "PowerCount", m_TermDataValue[4].m_DataName.Count.ToString(), FileName);
            for (int i = 0; i < m_TermDataValue[0].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[0].m_DataName[i];
                CGlobal.WritePrivateProfileString("YxNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[1].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[1].m_DataName[i];
                CGlobal.WritePrivateProfileString("YcNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[2].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[2].m_DataName[i];
                CGlobal.WritePrivateProfileString("YkNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[3].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[3].m_DataName[i];
                CGlobal.WritePrivateProfileString("SetNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[4].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[4].m_DataName[i];
                CGlobal.WritePrivateProfileString("PowerNameList", "Name" + i.ToString(), m_name, FileName);
            }

        }
    }
}
