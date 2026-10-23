using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TerminalWorkbench.Presentation;
using System.Threading;

namespace DtuTestManager
{

    public class CEnCodePtr
    {
        public CSecretInterface m_SecretInter = null;     //加密接口
        public static int m_SecretType = 0;     //加密版本类型

        public static bool m_MacErrFlag = false;                   //使用错误的加密MAC
        public static bool m_NormalErrCSFlag = false;            //远程加密使用错误的校验和
        public static bool m_SignErrFlag = false;                   //使用错误的签名
        public static bool m_TimeErrFlag = false;                   //使用错误的时间
        public static bool m_RandErrFlag = false;                   //使用错误的随机数
        public static bool m_UseNoSignFlag = false;                   //使用不带签名的报文
        public static bool m_UseNoTimeFlag = false;                   //使用不带时间的报文
        public static bool m_UseNoRandFlag = false;                   //使用不带随机数的报文
        public static bool m_UseMingWenFlag = false;                   //使用明文报文

        public static int m_EncodeFormHandle = -1;               //加密操作窗口句柄
        public static byte m_KeyIndex;                           //秘钥索引

        public static string m_EncodeIP = "192.168.0.222";       //加密机IP地址
        public static int m_EncodePort = 1024;                   //加密机连接端口号
        public static int m_EncodeHanle = -1;                    //加密机连接句柄

        public static string m_Result = "";                      //操作结果
        public static string m_StepName = "";                    //当前操作步骤名称


        public static byte[] m_GateRandKey = new byte[8];        //网关认证随机数
        public static string m_GateRandString = "";              //网关认证随机数字符串
        public static byte[] m_GateTermRandKey = new byte[8];    //网关认证终端随机数
        public static string m_GateTermRandString = "";          //网关认证终端随机数字符串
        public static byte[] m_GateTermSign = new byte[64];      //网关认证终端签名结果
        public static string m_GateTermSignString = "";          //网关认证终端签名结果字符串
        public static byte[] m_GateSign = new byte[64];          //网关认证签名结果
        public static string m_GateSignString = "";              //网关认证签名结果字符串

        public static byte[] m_MasterRandKey = new byte[8];      //主站认证随机数
        public static string m_MasterRandString = "";            //主站认证随机数字符串
        public static byte[] m_TermRandKey = new byte[8];        //主站认证终端随机数
        public static string m_TermRandString = "";              //主站认证终端随机数字符串
        public static byte[] m_TermSign = new byte[64];          //主站认证终端签名结果
        public static string m_TermSignString = "";              //主站认证终端签名结果字符串
        public static byte[] m_MasterSign = new byte[64];        //主站认证签名结果
        public static string m_MasterSignString = "";            //主站认证签名结果字符串
        public static byte[] m_VersionRand = new byte[8];        //主站认证_终端返回秘钥版本号时的随机数
        public static string m_VersionRandString = "";           //主站认证_终端返回秘钥版本号时的随机数字符串

        public static byte[] m_ProModule_ChipSerialNo = new byte[8];//协议模块返回芯片序列号
        public static byte[] m_ProModule_Random = new byte[8];//发给协议模块的随机数R1


        public static byte[] m_YW_RandKey = new byte[8];        //运维证书管理工具ID
        public static string m_YW_RandKeyString = "";           //运维证书管理工具ID
        public static byte[] m_YW_TermRand = new byte[8];       //运维认证终端返回随机数
        public static string m_YW_TermRandString = "";          //运维认证终端返回随机数字符串
        public static byte[] m_YW_Sign = new byte[64];          //运维认证签名结果
        public static string m_YW_SignString = "";              //运维认证签名结果字符串
        public static byte[] m_YW_ConRand = new byte[8];        //运维读取终端秘钥随机数
        public static string m_YW_ConRandString = "";           //运维读取终端秘钥随机数字符串
        public static byte[] m_YW_PrivateKey = new byte[64];    //运维私钥
        public static string m_YW_PrivateKeyString = "";        //运维私钥字符串
        public static byte[] m_YW_ResetSecret = new byte[500];  //运维恢复秘钥包
        public static string m_YW_ResetSecretString = "";       //运维恢复秘钥包字符串


        public static int m_NowKeyVersion;                      //终端当前秘钥版本号
        public static byte[] m_TermSerialNo = new byte[24];     //终端序列号
        public static string m_TermSerialNoString = "";         //终端序列号字符串
        public static byte[] m_ChipSerialNo = new byte[8];      //终端芯片序列号
        public static string m_ChipSerialNoString = "";         //终端芯片序列号字符串
        public static byte[] m_SafeSerialKey = new byte[64];    //终端内置安全芯片序公钥
        public static string m_SafeSerialKeyString = "";        //终端内置安全芯片序公钥字符串



        public static byte[] m_IniVectorData = new byte[16];    //主站初始向量
        public static byte[] m_VerIniVec = new byte[16];        //终端返回秘钥版本时随机数向量
        public static byte[] m_YW_VerIniVec = new byte[16];    //运维 终端返回秘钥版本时随机数向量
        public static byte[] m_YWCert = new byte[1024];        //运维证书
        public static int m_YwCertLeng = 0;                    //运维证书长度
        public static byte[] m_YWID = new byte[8];             //运维ID
        public static int m_YwIDLeng = 0;                      //运维ID长度
        public static byte[] m_YWVectorData = new byte[16];    //运维认证分散银子：运维工具ID + 运维终端随机数

        public static string[] m_CerFileList = new string[8] { "CAcerti.txt", "1certi.txt", "2certi.txt", "3certi.txt", "4certi.txt", "网关certi.txt", "TemCerti.txt", "YW_Cert.txt" };

        public static byte[] m_SecretPacket = new byte[190];


        public static string m_Cerfilepath;//证书路径
        public static string m_Cerfilename;//证书名字
        public static byte m_CerBiaosi = 0;//证书标识

        public static int PacketLeng = 199;     //每包传199个字节
        public static List<byte> m_CerDataBao = new List<byte>();   //收发证书数据
        public static int m_CerDataLeng = 0;                        //总文件大小
        public static byte m_CurrFrameNum = 1;                      //当前帧序号（升级证书）
        public static int m_TotalPacketNo;//总帧数


        public static int m_YWDowFileType = 1;  //1是身份认证传输数据标志 2对证书请求文件进行签名3证书导入终端

        private object m_Lock = new object();

        public static void NormalTest()
        {
            m_MacErrFlag = false;
            m_NormalErrCSFlag = false;
            m_SignErrFlag = false;
            m_TimeErrFlag = false;
            m_RandErrFlag = false;
            m_UseNoSignFlag = false;
            m_UseNoTimeFlag = false;
            m_UseNoRandFlag = false;
            m_UseMingWenFlag = false;
        }
        public void CreateSecretDevice()
        {
            if (m_SecretType == 0)
            {
                m_SecretInter = new C11SecretInterface();
            }
            else
            {
                m_SecretInter = new C12SecretInterface();
            }
        }
        public bool GetSecretPacketFile()
        {
            int nDataLeng = 0;
            int result = m_SecretInter.Secret_GetUpdateKeyYW(m_EncodeHanle, m_NowKeyVersion, 0, 1, m_ChipSerialNo, m_YW_VerIniVec, m_KeyIndex, m_YW_ResetSecret, ref nDataLeng);
            if (ShowEncodeErrMsg(result, 8))
                return false;
            return true;
        }
        public bool GetYWCertAndID()
        {
            if (m_SecretInter.Secret_GetYW_CertAndID(ref m_YWCert, ref m_YwCertLeng, ref m_YWID, ref m_YwIDLeng) == false)
            {
                MessageBox.Show("获取运维证书和ID失败!");
                return false;
            }
            return true;
        }
        public void YW_GetCertInfo()
        {
            if (m_EncodeHanle < 0)
            if (ConnectEncodeDev() == false)
                return;
            byte[] pBuff = new byte[2048];
            int pLeing = 0;
            byte[] pID = new byte[16];

            int nResult = m_SecretInter.Secret_GetCertInfoYW(m_EncodeHanle, pID, pBuff, ref pLeing);
            if (nResult < 0)
            {
                MessageBox.Show("获取失败!");
            }
            CGlobal.SaveBuffToTxtFile("证书\\YW_Cert.txt", pBuff, pLeing);
            CGlobal.SaveBuffToTxtFile("证书\\YW_ID.txt", pID, 8);

            MessageBox.Show("获取成功!");
        }
        public static void UpdateCerFileMsg(byte bCerFileNo)
        {
            m_CerBiaosi = bCerFileNo;
            m_Cerfilename = m_CerFileList[m_CerBiaosi];
        }
        //刷新加密窗口显示信息
        public static void FreshEncodeFormMsg()
        {
            if (m_EncodeFormHandle >= 0)
                CGlobal.PostMessage(m_EncodeFormHandle, UseMessage.MY_MSG_1_Encode, 0, 0);
        }
        //刷新加密窗口当前操作步骤显示信息
        public static void FreshEncodeFormOperateMsg(string strStepMsg)
        {
            m_StepName = strStepMsg;
            if (m_EncodeFormHandle >= 0)
                CGlobal.PostMessage(m_EncodeFormHandle, UseMessage.MY_MSG_2_Encode, 0, 0);
        }
        //刷新加密窗口当前操作结果显示信息
        public static void FreshEncodeFormResultMsg(string strResultMsg)
        {
            m_Result = strResultMsg;
            if (m_EncodeFormHandle >= 0)
                CGlobal.SendMessage(m_EncodeFormHandle, UseMessage.MY_MSG_3_Encode, 0, 0);
        }
        //显示加密机错误信息
        public bool ShowEncodeErrMsg(int nResult, int nType)
        {
            string strErrMsg = "";
            bool bErrFlag = true;
            switch (nType)
            {
                case 1:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "加密机已打开";
                            break;
                        case -2:
                            strErrMsg = "加密机打开失败";
                            break;
                        case -3:
                            strErrMsg = "加密机复位失败";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 2:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "加密机关闭失败";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 3:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "取随机数失败";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 4:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "非对称秘钥索引值有误";
                            break;
                        case -3:
                            strErrMsg = "原始数据长度有误";
                            break;
                        case -4:
                            strErrMsg = "计算签名值失败";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 5:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "原始数据长度有误";
                            break;
                        case -3:
                            strErrMsg = "证书长度传入有误";
                            break;
                        case -4:
                            strErrMsg = "验证签名值错误";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 6:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "秘钥版本不能小于0";
                            break;
                        case -3:
                            strErrMsg = "分散次数有误";
                            break;
                        case -4:
                            strErrMsg = "明文数据长度有误";
                            break;
                        case -5:
                            strErrMsg = "加密失败";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 7:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "秘钥版本不能小于0";
                            break;
                        case -3:
                            strErrMsg = "分散次数有误";
                            break;
                        case -4:
                            strErrMsg = "明文数据长度有误";
                            break;
                        case -5:
                            strErrMsg = "解密失败";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                case 8:
                    switch (nResult)
                    {
                        case -1:
                            strErrMsg = "传入句柄有误";
                            break;
                        case -2:
                            strErrMsg = "参数 oldKeyVersion与 NewKeyVersion传入值有误 传入值有误 传";
                            break;
                        case -3:
                            strErrMsg = "分散次数有误";
                            break;
                        case -4:
                            strErrMsg = "主站非对称密钥索引传入有误";
                            break;
                        case -5:
                            strErrMsg = "密钥更新数据计算有误";
                            break;
                        default:
                            bErrFlag = false;
                            break;
                    }
                    break;
                default:
                    bErrFlag = false;
                    break;
            }
            if (bErrFlag)
                FreshEncodeFormResultMsg(strErrMsg);
            return bErrFlag;
        }
        //关闭加密机
        public void CloseEncode()
        {
            if (m_EncodeHanle > 0)
            {
                m_SecretInter.Secret_DisConnect(m_EncodeHanle);
                m_EncodeHanle = -1;
            }
        }

        //将报文输出字符串
        public static string BufferToString(byte[] pBuffer, int nLeng)
        {
            string strOut = "";
            for (int i = 0; i < nLeng; i++)
            {
                strOut += String.Format("{0:X2}", pBuffer[i]);
            }
            return strOut;
        }
        //将报文转为字符串
        public static string BufferToASCII(byte[] pBuffer)
        {
            return TerminalWorkbench.Presentation.TextEncoding.Ansi.GetString(pBuffer);
        }

        //连接主站加密机
        public bool ConnectEncodeDev()
        {
            try
            {
                if (m_SecretInter == null)
                    CreateSecretDevice();
                if (m_EncodeHanle < 0)
                {
                    m_EncodeHanle = m_SecretInter.Secret_ConnectHSM(m_EncodeIP, m_EncodePort, 3000);
                    if (ShowEncodeErrMsg(m_EncodeHanle, 1))
                        return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception error)
            {
                CGlobal.ShowTipsInfo("连接加密接口失败：" + error.Message);
                return false;
            }
            return true;
        }
        ///获取随机数
        public bool GetRand(byte[] pRandBuff)
        {
            int result;
            lock (m_Lock)
            {
                result = m_SecretInter.Secret_GenRand(m_EncodeHanle, 8, pRandBuff);
            }
            if (ShowEncodeErrMsg(result, 3))
                return false;
            return true;
        }
        ///获取签名
        public bool GetSign(byte[] pContentBuff, int pLeng, byte[] pSignBuff, ref int nSignLeng, int nKeyIndex)
        {

            int result = m_SecretInter.Secret_InternalSign(m_EncodeHanle, nKeyIndex, pLeng, pContentBuff, pSignBuff, ref nSignLeng);

            if (ShowEncodeErrMsg(result, 4))
                return false;
            if (m_SignErrFlag)
            {
                for (int i = 0; i < nSignLeng; i++)
                    pSignBuff[i] = (byte)~pSignBuff[i];
            }
            return true;
        }
        ///进行秘钥签名验证
        public bool CheckVerSign(byte[] pFileBuff, int nFileLeng, byte[] pSigenBuff, int nSignLeng, byte[] pSigNature)
        {

            int result = m_SecretInter.Secret_VerSign(m_EncodeHanle, nFileLeng, pFileBuff, nSignLeng, pSigenBuff, 64, pSigNature);

            if (ShowEncodeErrMsg(result, 5))
                return false;
            return true;
        }
        //加密操作
        public bool EnCodeData(bool bYWFlag, byte[] DataBuff, int nDataLeng, byte[] pGetBuff, ref int nGetLeng)
        {
            int result = 0;
            if (bYWFlag == false)
                result = m_SecretInter.Secret_EnData(m_EncodeHanle, m_NowKeyVersion, 1, 1, m_ChipSerialNo, m_IniVectorData, nDataLeng, DataBuff, pGetBuff, ref nGetLeng);
            else
                result = m_SecretInter.Secret_EnDataYW(m_EncodeHanle, m_NowKeyVersion, 1, 2, m_YWVectorData, m_IniVectorData, nDataLeng, DataBuff, pGetBuff, ref nGetLeng);
            if (ShowEncodeErrMsg(result, 6))
                return false;
            return true;

        }


        public int ProModuleDeCodeData(int nSecretLeng, byte[] pSolveBuff, byte[] pPlainBuff, ref int nSolveLeng)
        {
            return m_SecretInter.Secret_DeAuthData(m_EncodeHanle, m_NowKeyVersion, 1, m_ProModule_ChipSerialNo, nSecretLeng, pSolveBuff, pPlainBuff, ref nSolveLeng);

        }
        //解密操作
        public bool DeCodeData(byte bType, int nSecretLeng, byte[] pSolveBuff, byte[] pPlainBuff, ref int nSolveLeng)
        {
            int result = 0;

            if ((bType & 0x40) != 0x40)
            {
                result = m_SecretInter.Secret_DeData(m_EncodeHanle, m_NowKeyVersion, 1, 1, m_ChipSerialNo, m_IniVectorData, nSecretLeng, pSolveBuff, pPlainBuff, ref nSolveLeng);
            }
            else
                result = m_SecretInter.Secret_DeDataYW(m_EncodeHanle, m_NowKeyVersion, 1, 2, m_YWVectorData, m_IniVectorData, nSecretLeng, pSolveBuff, pPlainBuff, ref nSolveLeng);

            if (ShowEncodeErrMsg(result, 7))
                return false;
            return true;
        }
        //获取更新秘钥
        public bool GetUpdateKey(byte[] UpdateKeyData, int nNewKeyVersion, ref int nKeyLeng)
        {
            int result = m_SecretInter.Secret_GetUpdateKey(m_EncodeHanle, m_NowKeyVersion, nNewKeyVersion, 1, m_ChipSerialNo, m_VerIniVec, m_KeyIndex, UpdateKeyData, ref nKeyLeng);

            if (ShowEncodeErrMsg(result, 8))
                return false;
            return true;
        }
        //获取终端随机数
        public static void GetTermRandData(byte[] pDataBuff, int nDataLeng)
        {
            byte[] m_ErrTermRandKey = new byte[8];

            if (m_RandErrFlag)
            {
                for (int i = 0; i < 8; i++)
                {
                    m_ErrTermRandKey[i] = (byte)(m_TermRandKey[i] + 1);
                }
                Array.Copy(m_ErrTermRandKey, 0, pDataBuff, nDataLeng, 8);
            }
            else
            {
                Array.Copy(m_TermRandKey, 0, pDataBuff, nDataLeng, 8);
            }

        }

        ///获取运维签名
        public bool YW_GetSign(byte[] pContentBuff, int pLeng, byte[] pSignBuff, ref int nSignLeng, int nKeyIndex)
        {


            int result = m_SecretInter.Secret_SignYW(m_EncodeHanle, pLeng, pContentBuff, pSignBuff, ref nSignLeng, nKeyIndex);
            if (ShowEncodeErrMsg(result, 4))
                return false;
            if (m_SignErrFlag)
            {
                for (int i = 0; i < nSignLeng; i++)
                    pSignBuff[i] = (byte)~pSignBuff[i];
            }
            return true;
        }

        public static byte[] GetCurrentTime()
        {
            byte[] m_Time = new byte[6];
            m_Time[0] = Convert.ToByte(DateTime.Now.Year - 2000);
            m_Time[1] = Convert.ToByte(DateTime.Now.Month);
            m_Time[2] = Convert.ToByte(DateTime.Now.Day);
            m_Time[3] = Convert.ToByte(DateTime.Now.Hour);
            m_Time[4] = Convert.ToByte(DateTime.Now.Minute);
            m_Time[5] = Convert.ToByte(DateTime.Now.Second);
            if (m_TimeErrFlag)
            {
                m_Time[1] = Convert.ToByte(DateTime.Now.Month + 3);
            }
            return m_Time;
        }
        public static int GetFileContent(byte[] pGetBuff)
        {
            int nDataLeng = 0;
            int nStartPlace = (m_CurrFrameNum - 1) * PacketLeng;
            if (m_CurrFrameNum < m_TotalPacketNo)
            {
                nDataLeng = PacketLeng;
            }
            else if (m_CurrFrameNum == m_TotalPacketNo)
            {
                nDataLeng = m_CerDataLeng % PacketLeng;
            }
            for (int i = 0; i < nDataLeng; i++)
            {
                pGetBuff[i] = m_CerDataBao[i + nStartPlace];
            }
            return nDataLeng;
        }
        public static void SetSendDataPacket(byte[] pDataBuff, int nDataLeng)
        {
            m_CerDataBao.Clear();
            for (int i = 0; i < nDataLeng; i++)
            {
                m_CerDataBao.Add(pDataBuff[i]);
            }
            m_CerDataLeng = nDataLeng;
            m_TotalPacketNo = m_CerDataLeng / PacketLeng;
            if ((m_CerDataLeng % PacketLeng) != 0)
                m_TotalPacketNo++;
            m_CurrFrameNum = 1;
        }
        //设置和刷新随机数、签名结果等加密数据
        public static void SetRandData(byte[] pRandBuff, int nStartPlace, int nLeng, int nType)
        {
            switch (nType)
            {
                case 0:         //网关认证随机数
                    Array.Copy(pRandBuff, nStartPlace, m_GateRandKey, 0, nLeng);
                    m_GateRandString = BufferToString(m_GateRandKey, nLeng);
                    break;
                case 1:         //网关认证终端随机数
                    Array.Copy(pRandBuff, nStartPlace, m_GateTermRandKey, 0, nLeng);
                    m_GateTermRandString = BufferToString(m_GateTermRandKey, nLeng);
                    break;
                case 2:         //网关认证终端签名结果
                    Array.Copy(pRandBuff, nStartPlace, m_GateTermSign, 0, nLeng);
                    m_GateTermSignString = BufferToString(m_GateTermSign, nLeng);
                    break;
                case 3:         //网关认证签名结果
                    Array.Copy(pRandBuff, nStartPlace, m_GateSign, 0, nLeng);
                    m_GateSignString = BufferToString(m_GateSign, nLeng);
                    break;
                case 4:        //主站认证随机数
                    Array.Copy(pRandBuff, nStartPlace, m_MasterRandKey, 0, nLeng);
                    for (int i = 0; i < 8; i++)
                    {
                        m_IniVectorData[i] = m_MasterRandKey[i];
                        m_IniVectorData[i + 8] = (byte)~((int)m_MasterRandKey[i]);
                    }
                    m_MasterRandString = BufferToString(m_MasterRandKey, nLeng);
                    break;
                case 5:       //主站认证终端随机数
                    Array.Copy(pRandBuff, nStartPlace, m_TermRandKey, 0, nLeng);
                    m_TermRandString = BufferToString(m_TermRandKey, nLeng);
                    break;
                case 6:       //主站认证终端签名结果
                    Array.Copy(pRandBuff, nStartPlace, m_TermSign, 0, nLeng);
                    m_TermSignString = BufferToString(m_TermSign, nLeng);
                    break;
                case 7:       //主站认证签名结果
                    Array.Copy(pRandBuff, nStartPlace, m_MasterSign, 0, nLeng);
                    m_MasterSignString = BufferToString(m_MasterSign, nLeng);
                    break;
                case 8:      //主站认证 终端返回秘钥版本时的随机数
                    Array.Copy(pRandBuff, nStartPlace, m_VersionRand, 0, nLeng);
                    m_VersionRandString = BufferToString(m_VersionRand, nLeng);
                    for (int i = 0; i < 8; i++)
                    {
                        m_VerIniVec[i] = m_VersionRand[i];
                        m_VerIniVec[i + 8] = (byte)~((int)m_VersionRand[i]);
                    }
                    break;
                case 9:         //运维认证 ID
                    Array.Copy(pRandBuff, nStartPlace, m_YW_RandKey, 0, nLeng);
                    m_YW_RandKeyString = BufferToString(m_YW_RandKey, nLeng);
                    break;
                case 10:        //运维 终端随机数
                    Array.Copy(pRandBuff, nStartPlace, m_YW_TermRand, 0, nLeng);
                    m_YW_TermRandString = BufferToString(m_YW_TermRand, nLeng);

                    Array.Copy(m_YW_RandKey, 0, m_YWVectorData, 0, 8);
                    Array.Copy(m_YW_TermRand, 0, m_YWVectorData, 8, 8);

                    for (int i = 0; i < 8; i++)
                    {
                        m_IniVectorData[i] = m_YW_TermRand[i];
                        m_IniVectorData[i + 8] = (byte)~((int)m_YW_TermRand[i]);
                    }
                    break;
                case 11:        //运维 认证签名结果
                    Array.Copy(pRandBuff, nStartPlace, m_YW_Sign, 0, nLeng);
                    m_YW_SignString = BufferToString(m_YW_Sign, nLeng);
                    break;
                case 12:        //运维 读取终端秘钥随机数
                    Array.Copy(pRandBuff, nStartPlace, m_YW_ConRand, 0, nLeng);
                    m_YW_ConRandString = BufferToString(m_YW_ConRand, nLeng);
                    for (int i = 0; i < 8; i++)
                    {
                        m_YW_VerIniVec[i] = m_YW_ConRand[i];
                        m_YW_VerIniVec[i + 8] = (byte)~((int)m_YW_ConRand[i]);
                    }
                    break;
                case 13:        //终端序列号
                    Array.Copy(pRandBuff, nStartPlace, m_TermSerialNo, 0, nLeng);
                    m_TermSerialNoString = BufferToString(m_TermSerialNo, nLeng);
                    break;
                case 14:        //终端芯片序列号
                    Array.Copy(pRandBuff, nStartPlace, m_ChipSerialNo, 0, nLeng);
                    m_ChipSerialNoString = BufferToString(m_ChipSerialNo, nLeng);
                    break;
                case 15:        //芯片公共密钥
                    Array.Copy(pRandBuff, nStartPlace, m_SafeSerialKey, 0, nLeng);
                    m_SafeSerialKeyString = BufferToString(m_SafeSerialKey, nLeng);
                    break;
                case 16:
                    Array.Copy(pRandBuff, nStartPlace, m_YW_PrivateKey, 0, nLeng);
                    m_YW_PrivateKeyString = BufferToString(m_YW_PrivateKey, nLeng);
                    break;
                case 17:
                    m_YW_ResetSecretString = BufferToString(m_YW_ResetSecret, 185);
                    break;
                case 18:
                    Array.Copy(pRandBuff, nStartPlace, m_ProModule_ChipSerialNo, 0, nLeng);
                    break;
                case 19:        //主站对协议模块发送随机数
                    Array.Copy(pRandBuff, nStartPlace, m_ProModule_Random, 0, nLeng);
                    break;

                default:
                    return;
            }
            FreshEncodeFormMsg();
        }
        //读txt格式
        public static int ReadCerfileCmd(ref byte[] filebyte, string strFilePath)
        {
            if (File.Exists(strFilePath) == false)
                return 0;
            StreamReader srFileStream = new StreamReader(strFilePath);
            string strContent = srFileStream.ReadToEnd();
            srFileStream.Close();
            strContent = strContent.Replace("\n", "");
            strContent = strContent.Replace("\r", "");
            strContent = strContent.Replace("\t", "");
            strContent = strContent.Replace(" ", "");
            int nByteLeng = strContent.Length / 2;//证书长度
            filebyte = new byte[nByteLeng];
            for (int i = 0; i < nByteLeng; i++)
            {
                filebyte[i] = Convert.ToByte(strContent.Substring(i * 2, 2), 16);
            }
            return nByteLeng;
        }
        public int MakeAllFrame(byte[] m_SendBuff, int Len)
        {
            int offset = 0;
            m_SendBuff[offset++] = 0xEB;
            m_SendBuff[offset++] = Convert.ToByte(Len / 256);//长度
            m_SendBuff[offset++] = Convert.ToByte(Len % 256);//长度
            m_SendBuff[offset++] = 0xEB;
            m_SendBuff[Len + 4] = CGlobal.GetCS(m_SendBuff, 4, Len);
            if (m_NormalErrCSFlag)
            {
                m_SendBuff[Len + 4] = (byte)~m_SendBuff[Len + 4];
            }
            m_SendBuff[Len + 5] = 0xD7;
            return Len + 6;
        }
        //明文发送
        public int MakeMingWenFrame(byte BuffType, byte APPType, byte[] AppDatabuff, int AppdataLen, byte[] Infosave, int InfosaveLen, byte[] m_SendBuff)
        {
            int offset = 4;
            m_SendBuff[offset++] = 0x00;//报文类型
            m_SendBuff[offset++] = BuffType;//报文类型
            m_SendBuff[offset++] = APPType;//应用类型
            //应用数据区
            if (AppdataLen > 0)
                Array.Copy(AppDatabuff, 0, m_SendBuff, offset, AppdataLen);
            offset += AppdataLen;
            //信息安全扩展区
            if (InfosaveLen > 0)
                Array.Copy(Infosave, 0, m_SendBuff, offset, InfosaveLen);
            offset += InfosaveLen;

            return MakeAllFrame(m_SendBuff, offset - 4);
        }
        //密文发送
        public int MakeMiWenFrame(byte type, byte Apptype, byte[] appdata, int appdataLen, byte[] Infosave, int InfosaveLen, byte[] m_SendBuff, bool bYwFlag = false)
        {
            int offset = 0;
            byte[] enData = new byte[2000];
            int enDataLen = 0;
            int i = 0;
            int plainDataLen = 1 + appdataLen + InfosaveLen;
            byte[] plainData = new byte[plainDataLen]; //明文
            plainData[offset++] = Apptype; //应用类型
            for (i = 0; i < appdataLen; i++)
            {
                plainData[offset++] = appdata[i];
            }
            for (i = 0; i < InfosaveLen; i++)
            {
                plainData[offset++] = Infosave[i];
            }

            if (EnCodeData(bYwFlag, plainData, offset, enData, ref enDataLen) == false)
            {

                return 0;
            }

            offset = 4;
            m_SendBuff[offset++] = 0x00;//报文类型
            m_SendBuff[offset++] = type;//报文类型
            if (m_MacErrFlag)           ///加密MAC
            {
                for (i = enDataLen - 4; i < enDataLen; i++)
                {
                    enData[i] = 0; //错误的mac
                }
            }
            for (i = 0; i < enDataLen; i++) //应用数据区
            {
                m_SendBuff[offset++] = enData[i];
            }
            return MakeAllFrame(m_SendBuff, offset - 4);
        }
    }
}
