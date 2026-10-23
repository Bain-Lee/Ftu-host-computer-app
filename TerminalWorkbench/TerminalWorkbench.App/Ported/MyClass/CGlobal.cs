using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using TerminalWorkbench.Presentation;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Collections;

using System.Threading;

namespace DtuTestManager
{
    public class CGlobal
    {
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);//系统dll导入ini写函数
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);//系统dll导入ini读函数
        public static int PostMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            return UiMessages.Post(hWnd, Msg, wParam, lParam);
        }
        public static int SendMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            return UiMessages.Send(hWnd, Msg, wParam, lParam);
        }

        public static int m_YxNo = 0;
        public static byte m_YxValue = 0;
        public static int m_YcNo = 0;
        public static float m_YcValue = 0;



        public static int IsAutoCon = 0;
        public static int IsAutoCall = 0;
        public static CEnCodePtr m_SecretEncode = new CEnCodePtr();
        public static bool m_RunSendMode = false;           ///连接发送标志,默认不发送任何报文
        public static List<CModuleClass> m_ModuleList = new List<CModuleClass>();
        public static CDataBase m_DataBase = new CDataBase("DataBase.mdb");
        public static Object m_BuffLoker = new object();
        public static CBufferViewForm m_BufferView = null;
        public static CEventMsgForm m_EventView = null;
        public static CSOEMsgForm m_SOEView = null;
        public static string FileName = TerminalWorkbench.RuntimePaths.WorkspaceDirectory + "PrjSet_Main.ini";//ini文件名
        public static CDeviceClass m_Device = new CDeviceClass();
        public static CDeviceClass m_PreDefauleDevice = null;

        public static List<CPriTerm> m_PriTerm = new List<CPriTerm>();//所有模板中的数据名称
        public static CPriTerm m_PriTerm_Sub = new CPriTerm();//Sub模板中的数据名称
        public static bool m_SaveFlag = false;
        public static string[] strDevModulList = null;            //所有的模板名称
        private static Object m_Sendlocker = new Object();
        //4G模块
        public static List<CIPModule> m_IPGroup = new List<CIPModule>();
        public static List<CMacModule> m_MacGroup = new List<CMacModule>();
        public static List<CGateWayModule> m_GateWayGroup = new List<CGateWayModule>();
        public static List<CComModule> m_GComList = new List<CComModule>();
        public static List<CHostModule> m_GHostList = new List<CHostModule>();
        public static List<CHostModule> m_GDevList = new List<CHostModule>();
        public static List<CParaCtrl4G> m_4GList = new List<CParaCtrl4G>();
        public static List<CParaCtrl4G> m_HostList = new List<CParaCtrl4G>();
        public static byte[] GetMD5Hash(string FileName)
        {
            FileStream pathfile = new FileStream(FileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] retVal = md5.ComputeHash(pathfile);
            pathfile.Close();
            return retVal;
        }
        public static void SaveBuffToTxtFile(string strPath, byte[] pData, int nLeng)
        {
            string str = "";
            for (int i = 0; i < nLeng; i++)
            {
                str += string.Format("{0:X2}", pData[i]);
            }
            FileStream SaveFile = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(SaveFile, TerminalWorkbench.Presentation.TextEncoding.Ansi);

            SaveFile.SetLength(0);
            streamWriter.Write(str);
            streamWriter.Flush();
            streamWriter.Close();
            SaveFile.Close();
        }
        public static bool DoWithLoadFile(string strPath)
        {
            FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (file.Length == 0)
                return false;

            byte[] data = new byte[file.Length];
            file.ReadExactly(data);
            file.Close();

            FileInfo myFile = new FileInfo(strPath);
            m_Device.m_TransFile.m_FileName = myFile.Name;
            m_Device.m_TransFile.m_FileNameLength = myFile.Name.Length;
            m_Device.m_TransFile.m_FileID = 0;
            m_Device.m_TransFile.m_FileSize = myFile.Length;
            int i = 0;
            int nCount = 0;
            nCount = (int)(myFile.Length / 199);
            m_Device.m_TransFile.SumFrames = nCount;
            for (i = 0; i < nCount; i++)
            {
                CFileData temp = new CFileData();
                temp.PackNo = i;
                temp.DataLength = 199;
                temp.Offset = i * 199;
                temp.DataBuff = new byte[temp.DataLength];
                Array.Copy(data, temp.Offset, temp.DataBuff, 0, temp.DataLength);
                m_Device.m_TransFile.m_data.Add(temp);
            }
            if ((myFile.Length % 199) > 0)
            {
                CFileData temp = new CFileData();
                temp.PackNo = nCount;
                temp.DataLength = (int)(myFile.Length % 199);
                temp.DataBuff = new byte[temp.DataLength];
                temp.Offset = nCount * 199;
                Array.Copy(data, temp.Offset, temp.DataBuff, 0, temp.DataLength);
                m_Device.m_TransFile.m_data.Add(temp);
                nCount++;
            }
            m_Device.m_TransFile.SumFrames = nCount;
            m_Device.m_TransFile.CurrentIndex = 0;
            return true;
        }
        private string UnicodeToString(string unicode)
        {
            string resultStr = "";
            string[] strList = unicode.Split('u');
            for (int i = 1; i < strList.Length; i++)
            {
                resultStr += (char)int.Parse(strList[i], System.Globalization.NumberStyles.HexNumber);
            }
            return resultStr;
        }
        public static int GetCanuUseNo(Dictionary<int, int> pMap)
        {
            int nNo = 1;
            while (true)
            {
                if (pMap.ContainsKey(nNo))
                {
                    nNo++;
                    continue;
                }
                else
                {
                    pMap.Add(nNo, nNo);
                    return nNo;
                }
            }
        }
        public static string GetYcValue(int nYcIndex, float fMul = 1)
        {
            float fValue = 0;
            string strValue = "";
            if (nYcIndex > 0 && nYcIndex <= m_Device.m_YcList.Count)
            {
                fValue = m_Device.m_YcList[nYcIndex - 1].m_YcValue;
                fValue *= fMul;
                strValue = fValue.ToString("F3");
            }
            else
            {
                strValue = "###.###";
            }
            return strValue;
        }
        public static bool GetYxStatus(int nYxIndex)
        {
            if (nYxIndex > 0 && nYxIndex <= CGlobal.m_Device.m_YxList.Count)
            {
                int nValue = m_Device.m_YxList[nYxIndex - 1].m_YxValue;
                if (nValue == 0 || nValue == 3 || (nValue == 1 && CGlobal.m_Device.m_Connection.m_SingleType == 1))
                {
                    return false;
                }
            }
            else
                return false;
            return true;
        }
        public static void ShowBuffer(string strBuffName, Byte[] pBuffer, int nLeng)
        {
            try
            {
                if (m_BufferView != null && m_BufferView.IsDisposed == false)
                {
                    m_BufferView.ShowBuffer(strBuffName, pBuffer, nLeng);
                }
            }
            catch { }
        }
        public static void ShowTipsInfo(string strInfoTips)
        {
            try
            {
                if (m_BufferView != null && m_BufferView.IsDisposed == false)
                {
                    m_BufferView.ShowTips(strInfoTips);
                }
            }
            catch { }
        }
        public static void SendTaskOrder(int nTaskNo)
        {
            lock (m_Sendlocker)
            {
                m_Device.SendTaskOrder(nTaskNo);
            }
        }
        //读写通讯参数
        public static void ReadSysSetting()
        {
            StringBuilder temp = new StringBuilder(100);//存储读出ini内容变量
            GetPrivateProfileString("ComType", "IsAutoCon", "1", temp, 100, FileName);
            CGlobal.IsAutoCon = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "IsAutoCall", "1", temp, 100, FileName);
            CGlobal.IsAutoCall = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "ConnType", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_nConnType = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "ProType", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_n_ProType = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "SingleType", "0", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_SingleType = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "ModuleName", "0", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_DevName = temp.ToString();
            GetPrivateProfileString("ComType", "DC1", "0", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_DC1Index = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "DC2", "0", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_DC2Index = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "AC1", "0", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_AC1Index = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "AC2", "0", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_AC2Index = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("ComType", "SecretType", "0", temp, 100, FileName);
            CEnCodePtr.m_SecretType = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("SerialSet", "Port", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_No = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("SerialSet", "Band", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_Baund = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("SerialSet", "databit", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_DataBit = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("SerialSet", "parity", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("SerialSet", "stopbit", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_StopBit = Convert.ToInt32(temp.ToString());

            GetPrivateProfileString("NetSet", "IP", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_strIP_Address = temp.ToString();
            GetPrivateProfileString("NetSet", "Port", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_nIP_Port = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("NetSet", "SocketType", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_SocketMode = Convert.ToInt32(temp.ToString());

            GetPrivateProfileString("104", "ASDUAddr", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_104Address.m_AsduAddress = Convert.ToUInt16(temp.ToString());
            GetPrivateProfileString("104", "YXStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_104Address.m_YxStartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("104", "YCStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_104Address.m_YcStartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("104", "YKStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_104Address.m_YkSrartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("104", "PowerStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_104Address.m_PowerStartAddress = Convert.ToInt32(temp.ToString());

            GetPrivateProfileString("101", "ASDUAddr", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("101", "YXStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_YxStartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("101", "YCStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_YcStartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("101", "YKStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_YkSrartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("101", "PowerStart", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_PowerStartAddress = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("101", "AddressType", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_AddressType = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("101", "CorType", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.m_101Address.m_CotType = Convert.ToInt32(temp.ToString());
            GetPrivateProfileString("Encode", "IsEncode", "1", temp, 100, FileName);
            CGlobal.m_Device.m_Connection.IsEncode = Convert.ToBoolean(temp.ToString());
            GetPrivateProfileString("Encode", "KenIndex", "1", temp, 100, FileName);
            CEnCodePtr.m_KeyIndex = Convert.ToByte(temp.ToString());
            GetPrivateProfileString("Encode", "EncodeIP", "1", temp, 100, FileName);
            CEnCodePtr.m_EncodeIP = temp.ToString();
            GetPrivateProfileString("Encode", "EncodePort", "1", temp, 100, FileName);
            CEnCodePtr.m_EncodePort = Convert.ToInt32(temp.ToString());
            CGlobal.m_Device.InitConnect();

        }
        public static void WriteComPara()
        {
            WritePrivateProfileString("ComType", "IsAutoCon", CGlobal.IsAutoCon.ToString(), FileName);
            WritePrivateProfileString("ComType", "IsAutoCall", CGlobal.IsAutoCall.ToString(), FileName);
            WritePrivateProfileString("ComType", "ConnType", CGlobal.m_Device.m_Connection.m_nConnType.ToString(), FileName);
            WritePrivateProfileString("ComType", "ProType", CGlobal.m_Device.m_Connection.m_n_ProType.ToString(), FileName);
            WritePrivateProfileString("ComType", "SingleType", CGlobal.m_Device.m_Connection.m_SingleType.ToString(), FileName);
            WritePrivateProfileString("ComType", "ModuleName", CGlobal.m_Device.m_Connection.m_DevName, FileName);
            WritePrivateProfileString("ComType", "DC1", CGlobal.m_Device.m_Connection.m_DC1Index.ToString(), FileName);
            WritePrivateProfileString("ComType", "DC2", CGlobal.m_Device.m_Connection.m_DC2Index.ToString(), FileName);
            WritePrivateProfileString("ComType", "AC1", CGlobal.m_Device.m_Connection.m_AC1Index.ToString(), FileName);
            WritePrivateProfileString("ComType", "AC2", CGlobal.m_Device.m_Connection.m_AC2Index.ToString(), FileName);
            WritePrivateProfileString("ComType", "SecretType", CEnCodePtr.m_SecretType.ToString(), FileName);


            WritePrivateProfileString("SerialSet", "Port", CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_No.ToString(), FileName);
            WritePrivateProfileString("SerialSet", "Band", CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_Baund.ToString(), FileName);
            WritePrivateProfileString("SerialSet", "databit", CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_DataBit.ToString(), FileName);
            WritePrivateProfileString("SerialSet", "stopbit", CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_StopBit.ToString(), FileName);
            WritePrivateProfileString("SerialSet", "parity", CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit.ToString(), FileName);

            WritePrivateProfileString("NetSet", "IP", CGlobal.m_Device.m_Connection.m_strIP_Address.ToString(), FileName);
            WritePrivateProfileString("NetSet", "Port", CGlobal.m_Device.m_Connection.m_nIP_Port.ToString(), FileName);
            WritePrivateProfileString("NetSet", "SocketType", CGlobal.m_Device.m_Connection.m_SocketMode.ToString(), FileName);

            WritePrivateProfileString("104", "ASDUAddr", CGlobal.m_Device.m_Connection.m_104Address.m_AsduAddress.ToString(), FileName);
            WritePrivateProfileString("104", "YXStart", CGlobal.m_Device.m_Connection.m_104Address.m_YxStartAddress.ToString(), FileName);
            WritePrivateProfileString("104", "YCStart", CGlobal.m_Device.m_Connection.m_104Address.m_YcStartAddress.ToString(), FileName);
            WritePrivateProfileString("104", "YKStart", CGlobal.m_Device.m_Connection.m_104Address.m_YkSrartAddress.ToString(), FileName);
            WritePrivateProfileString("104", "PowerStart", CGlobal.m_Device.m_Connection.m_104Address.m_PowerStartAddress.ToString(), FileName);


            WritePrivateProfileString("101", "ASDUAddr", CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress.ToString(), FileName);
            WritePrivateProfileString("101", "YXStart", CGlobal.m_Device.m_Connection.m_101Address.m_YxStartAddress.ToString(), FileName);
            WritePrivateProfileString("101", "YCStart", CGlobal.m_Device.m_Connection.m_101Address.m_YcStartAddress.ToString(), FileName);
            WritePrivateProfileString("101", "YKStart", CGlobal.m_Device.m_Connection.m_101Address.m_YkSrartAddress.ToString(), FileName);
            WritePrivateProfileString("101", "PowerStart", CGlobal.m_Device.m_Connection.m_101Address.m_PowerStartAddress.ToString(), FileName);
            WritePrivateProfileString("101", "AddressType", CGlobal.m_Device.m_Connection.m_101Address.m_AddressType.ToString(), FileName);
            WritePrivateProfileString("101", "CorType", CGlobal.m_Device.m_Connection.m_101Address.m_CotType.ToString(), FileName);



            WritePrivateProfileString("Encode", "IsEncode", CGlobal.m_Device.m_Connection.IsEncode.ToString(), FileName);
            WritePrivateProfileString("Encode", "KenIndex", CEnCodePtr.m_KeyIndex.ToString(), FileName);
            WritePrivateProfileString("Encode", "EncodeIP", CEnCodePtr.m_EncodeIP, FileName);
            WritePrivateProfileString("Encode", "EncodePort", CEnCodePtr.m_EncodePort.ToString(), FileName);
        }
        public static byte GetCS(byte[] TB, int nStart, int nLeng)
        {
            int L = 0;
            for (int i = nStart; i < (nStart + nLeng); i++)
            {
                L = L + TB[i];
            }

            return Convert.ToByte(L % 256);

        }
        //编码转汉字
        public static string BytesToString(byte[] Bytes)
        {
            string Mystring;
            Encoding FromEcoding = Encoding.GetEncoding("gb2312");
            Encoding ToEcoding = Encoding.GetEncoding("UTF-8");
            byte[] Tobytes = Encoding.Convert(FromEcoding, ToEcoding, Bytes);
            Mystring = ToEcoding.GetString(Tobytes);                                            //得到的是UTF8字节码序列，需要转换为UTF8字符串
            return Mystring;                                                                    //转换
        }
        public static float GetBuffToFloat(Byte bValue1, Byte bValue2, int nMul)
        {
            int nValue = bValue1 + bValue2 * 256;
            float fValue = (float)nValue;
            fValue = fValue / nMul;
            return fValue;
        }
        public static int GetBuffToShort(Byte bValue1, Byte bValue2)
        {
            int nLValue = bValue1 + bValue2 * 256;
            return nLValue;
        }
        public static int GetBuffToInt(Byte bValue1, Byte bValue2, Byte bValue3, Byte bValue4)
        {
            int nHValue = bValue3 + bValue4 * 256;
            int nLValue = bValue1 + bValue2 * 256;
            int nValue = nHValue * 65536 + nLValue;
            return nValue;
        }
        //浅拷贝函数

    }
    public class CModuleClass
    {
        public int m_ModuleNo;
        public string m_ModuleName;
        public int m_ModuleType;
        public int m_LineCount;
        public int m_DevAddress;
        public string m_ModulePath;
        public CDataBase m_ModuleDB = new CDataBase();
        public void InitialzeReadOption()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  DevModule";
            pTable = (DataTable)m_ModuleDB.SelectDataTable(strSql);
            if (pTable == null)
                return;
            m_ModuleType = m_ModuleDB.GetTableIntValue(pTable, 0, 1);
            m_LineCount = m_ModuleDB.GetTableIntValue(pTable, 0, 2);
            m_DevAddress = m_ModuleDB.GetTableIntValue(pTable, 0, 3);
        }
        public void UpdateMsg()
        {
            string strSql;
            strSql = "update DevModule set ModuleName='" + m_ModuleName + "',LineCount=" + m_LineCount.ToString() + ",DevAddress=" + m_DevAddress.ToString();
            m_ModuleDB.ExecSql(strSql);
        }
        public static void SaveRecvLog(string strContent)
        {
            string strPath = "接收报文.txt";
            FileStream pFile = new FileStream(strPath, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(pFile, TerminalWorkbench.Presentation.TextEncoding.Ansi);
            string strTimeHead = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strTimeHead = strTimeHead + " " + strContent;
            streamWriter.WriteLine(strTimeHead);
            streamWriter.Flush();
            streamWriter.Close();
            pFile.Close();
        }
    }
}
