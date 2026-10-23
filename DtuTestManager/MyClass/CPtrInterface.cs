using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DtuTestManager
{

    public class UseMessage4G
    {
        private const int WM_USER = 0x0400;
        public const int MY_MSG_1 = WM_USER + 1;
        public const int MY_MSG_2 = WM_USER + 2;
        public const int MY_MSG_3 = WM_USER + 3;
        public const int MY_MSG_4 = WM_USER + 4;
        public const int MY_MSG_5 = WM_USER + 5;
        public const int MY_MSG_6 = WM_USER + 6;
        public const int MY_MSG_7 = WM_USER + 7;
        public const int MY_MSG_8 = WM_USER + 8;
        public const int MY_MSG_9 = WM_USER + 9;
        public const int MY_MSG_10 = WM_USER + 10;
        public const int MY_MSG_11 = WM_USER + 11;
    }
    public enum EncodeStatus
    {
        Auto_Gate_Identity = 1000000,           //网关身份认证
        Auto_Gate_Request,                   //网关身份认证请求响应
        Auto_Master_Indentity,               //主站身份认证
        Auto_Master_Request,                 //主站身份认证请求响应
        Auto_Master_ReadChipNo,              //读取终端芯片序列号
        Master_Update_Rand,          //主站下发随机数
        Master_Update_Start,          //远程升级启动
        Auto_Master_ReadVersion,             //读取终端秘钥版本

        Gate_Identity,                      //网关身份认证
        Gate_Request,                       //网关身份认证请求响应
        Master_Indentity,                   //主站身份认证
        Master_Request,                     //主站身份认证请求响应
        Master_ReadChipNo,                  //读取终端芯片序列号
        Master_ReadVersion,                 //读取终端秘钥版本
        Master_UpdateSecret,                //远程秘钥更新
        Master_ResetSecret,                 //远程秘钥恢复
        Master_UpdateCer,                   //证书更新
        Master_DownCer,                     //证书下载
        Master_ReadCer,                     //读取证书
        Master_ReturnResult,                //返回证书读取结果
        Master_Answer_OK,                   //分帧确认应答
        Master_Update_Vertify,              //远程升级验证包
      
       

        YW_Identity,                        //运维 身份认证 
        YW_Request,                         //运维 身份认证请求响应
        YW_ReadVersion,                     //运维 读取终端秘钥版本
        YW_ReadSerialNo,                    //运维 读取终端序列号
        YW_ReadChipNo,                      //运维 读取终端芯片序列号
        YW_ReadSecretKey,                   //运维 读取安全芯片公钥
        YW_FileSign,                        //运维 证书文件签名
        YW_DownCer,                         //运维 下载证书
        YW_ReWrite,                         //运维 终端初始证书回写
        YW_ReadTerCer,                      //运维 读取终端证书
        YW_AnswerRead,                      //运维 返回读取结果
        YW_ResetKey,                        //运维 恢复终端对称秘钥
        YW_Answer_OK,                        //运维 分帧确认应答
        YW_SendData,                         //运维发送配电数据
    }
    public class CPtrInterface
    {

       
        public EncodeStatus m_EncodeStatus = EncodeStatus.Gate_Identity;
        private int[,] m_AppErrMsg = { { 0, 0},{1,0x41},{2,8},{3,0x49},{4,0},{5,0x47},{6,0x0E},{7,0x4F},{8,0x4F},{0x1F,2},
                                {0x21,0x49},{0x23,2},{0x51,0x49},{0x53,2},{0x61,9},{0x63,2},{0x65,2},{0x71,2},{0x73,2},{0x83,0x10}};
        string[] m_MsgErr = { "业务应用类型错误", "报文验签失败", "报文解密失败", "随机数验证失败", 
                                         "时间校验失败", "业务安全要求不合规，不符合报文安全规范", "业务安全流程非法", 
                                         "权限不够", "未知错误", "报文长度有误或解析失败" };
        public delegate void Delegate_ShowTips(string str);
        public Delegate_ShowTips pDelegate;

        public CDeviceClass theDevice = null;           //对应装置指针
        public CYkOption pYkMsg = null;

        public bool Is_PriPara = false;
        public int m_ParaAreaNo = 0;

        protected DateTime m_TimeOutTime;
        protected bool m_bCheckTimeOut = false;

        protected DateTime m_HeartTime = DateTime.Now;
        protected bool m_bHeartOnlie = false;
        protected bool m_StartFlag = false;

        public int m_YxStartAddress = 0x01;
        public int m_YcStartAddress = 0x4001;
        public int m_YkSrartAddress = 0x6001;
        public int m_PowerStartAddress = 0x6401;
        public int m_AddressType = 0;
        public int m_CotType = 0;
        public ParaTaskMsg m_NowParaTask;   
        public CDownFileTaskMsg m_NowDownFileTask;
        public CerTaskMsg m_NowCerTask;

        private byte[] pSolveBuff = new byte[2000];
        private byte[] pPlainBuff = new byte[2000];
        private int nSolveLeng = 0;

        public bool m_IsUpdateFlag = false;  //101升级使用标志
        public CPtrInterface()
        {
            m_HeartTime = DateTime.Now;
            m_StartFlag = false;
            m_bCheckTimeOut = false;
            m_bHeartOnlie = false;
            if (theDevice != null)
            {
                theDevice.m_SelectFloder = null;
                theDevice.m_NowReadFile = null;
            }
        }
        protected bool CheckParaStatus( bool bPri,int nAddress)
        {
            if (bPri && nAddress >= 0x8001)
            {
                MessageBox.Show("地址错误!");
                return true;
            }
            if (bPri == false && nAddress < 0x8001)
            {
                MessageBox.Show("地址错误!");
                return true;
            }
            return false;
        }
        
        protected string GetSOETimeString(Byte[] pTime)
        {
            string strTime;
            int MillSecond = pTime[0] + pTime[1] * 256;
            int Second = MillSecond / 1000;
            MillSecond %= 1000;
            int nMinute = pTime[2];
            int nHour = pTime[3];
            int nDay = pTime[4] & 0x1F;
            int nMonth = pTime[5];
            int nYear = pTime[6] + 2000;
            strTime = nYear.ToString("D4") + "-";
            strTime += nMonth.ToString("D2") + "-";
            strTime += nDay.ToString("D2") + " ";
            strTime += nHour.ToString("D2") + ":";
            strTime += nMinute.ToString("D2") + ":";
            strTime += Second.ToString("D2") + ".";
            strTime += MillSecond.ToString("D3");
            return strTime;
        }
        protected string GetYxStatus(int bValue, bool bIsDouble)
        {
            string strStatus;
            if (bIsDouble)
            {
                if (bValue == 1)
                    strStatus = "分（双点）";
                else if (bValue == 2)
                    strStatus = "合（双点）";
                else
                    strStatus = "无效（双点）";
            }
            else
            {
                if (bValue == 1)
                    strStatus = "合（单点）";
                else
                    strStatus = "分（单点）";
            }
            return strStatus;
        }
        private CYxOption GetYxOption(int nYxAddress,int nLow,int nHigh)
        {
            if (nLow >= theDevice.m_YxList.Count || nHigh >= theDevice.m_YxList.Count) return null;
            if (nLow == nHigh)
            {
                if (theDevice.m_YxList[nLow].m_YxAddress != nYxAddress) return null;
                return theDevice.m_YxList[nLow];
            }
            else if (nLow > nHigh)
                return null;
            int nMid = (nLow + nHigh) / 2;
            CYxOption pYxOtion = theDevice.m_YxList[nMid];
            if (nYxAddress == pYxOtion.m_YxAddress) return pYxOtion;

            if (pYxOtion.m_YxAddress > nYxAddress)
            {
                nHigh = nMid - 1;
            }
            else
            {
                nLow = nMid + 1;
            }
            return GetYxOption(nYxAddress, nLow, nHigh);
        }
        protected void SetYxNowStatus(int nYxNumber, int nValue)
        {
            CYxOption pYxOpton = GetYxOption(nYxNumber, 0, theDevice.m_YxList.Count-1);
            if (pYxOpton == null) return;
            pYxOpton.m_YxValue = nValue;           
        }
        protected void SetYxNowStatus_SOE(int nYxNumber, int nValue, string strTimeMsg, bool bIsDouble, bool bIsChange, bool bSoeFlag)
        {
            CYxOption pYxOpton = GetYxOption(nYxNumber, 0, theDevice.m_YxList.Count - 1);
            if (pYxOpton == null) return;
            pYxOpton.m_YxValue = nValue;
            string strStatus = GetYxStatus(nValue, bIsDouble);
            if (bIsChange)
            {
                if (bSoeFlag)
                {
                    theDevice.m_SoeClass.AddRealMsg(nYxNumber, nValue, pYxOpton.m_YxName, strTimeMsg, strStatus);
                }
            }
        }
        private CYcOption GetYcOption(int nYcAddress, int nLow, int nHigh)
        {
            if (nLow >= theDevice.m_YcList.Count || nHigh >= theDevice.m_YcList.Count) return null;
            if (nLow == nHigh)
            {
                if (theDevice.m_YcList[nLow].m_YcAddress != nYcAddress) return null;
                return theDevice.m_YcList[nLow];
            }
            else if (nLow > nHigh)
                return null;
            int nMid = (nLow + nHigh) / 2;
            CYcOption pYcOtion = theDevice.m_YcList[nMid];
            if (nYcAddress == pYcOtion.m_YcAddress) return pYcOtion;

            if (pYcOtion.m_YcAddress > nYcAddress)
            {
                nHigh = nMid - 1;
            }
            else
            {
                nLow = nMid + 1;
            }
            return GetYcOption(nYcAddress, nLow, nHigh);
        }
        protected void SetYcValue(int nYcIndex, float fValue, Byte bEnable)
        {
            CYcOption pData = GetYcOption(nYcIndex, 0, theDevice.m_YcList.Count-1);
            if (pData == null) return;
            if (Math.Abs(pData.m_YcRatio) > 0.000001)
            {
                pData.m_YcValue = fValue * pData.m_YcRatio + pData.m_YcOffset;
            }
            else
            {
                pData.m_YcValue = fValue + pData.m_YcOffset;
            }
            if ((bEnable & 0x80) == 0x80) pData.m_YcValue = 0;
        }
        protected void SetPowerValue(int nPowerIndex, float fValue, Byte bEnable, string strTimeMsg)
        {
            if (nPowerIndex >= 0 && nPowerIndex < theDevice.m_PowerList .Count)
            {
                CPowerOption pData = theDevice.m_PowerList[nPowerIndex];
                pData.m_PowerValue = fValue;               
                if ((bEnable & 0x80) == 0x80) pData.m_PowerValue = 0;
                pData.m_PowerChangeTime = strTimeMsg;     
            }
        }
        protected void SetTimeOut()
        {
            m_TimeOutTime = DateTime.Now;
            m_bCheckTimeOut = true;
        
        }
        public bool CheckTimeOut()
        {
            if (m_bCheckTimeOut == false) return false;
         //   Console.WriteLine("TimeOut Check\n");
            TimeSpan spanTime = DateTime.Now - m_TimeOutTime;
            if (spanTime.Seconds >= 15)
            {
                m_bCheckTimeOut = false;
                CGlobal.PostMessage(theDevice.m_MainFormHandle, UseMessage.MY_MSG_6, 0, 0);
                return true;
            }
            return false;
        }
        public virtual void InitPara()
        {
            if (theDevice != null)
            {
                theDevice.m_bYcFresh = false;
                theDevice.m_bYxFresh = false;
                theDevice.m_bPwFresh = false;
                theDevice.m_bParaFresh = false;
            }
            m_HeartTime = DateTime.Now;
            m_StartFlag = false;
            Console.WriteLine("Cancel Check TimeOut\n");
            m_bCheckTimeOut = false;
            m_bHeartOnlie = false;
            if (theDevice != null)
            {
                theDevice.m_SelectFloder = null;
                theDevice.m_NowReadFile = null;
            }
        }
        public virtual void GetTestOrSBuff(Byte[] pSendBuff, ref int pSendLeng)
        {

        }
        public void ShowTipStr(string str)
        {
            CGlobal.ShowTipsInfo(str);
        }
        public void ShowTips(string strMsg)
        {
            CEnCodePtr.FreshEncodeFormResultMsg(strMsg);
            pDelegate = new Delegate_ShowTips(ShowTipStr);
            pDelegate(strMsg);
        }
        public virtual int GetTheRightBuffContent(byte[] pRecBuff, int nBuffLeng, Byte[] pGetBuff, ref int nGetLeng)
        {
            ///非加密模式，直接返回0
            if (theDevice.m_Connection.IsEncode == false) return 0;
            if (nBuffLeng < 11) return 0;        ///最小长度为11
            int nDataLeng = 0;                  ///有效数据长度
            int nProcessLeng = nBuffLeng;       ///默认截取全部报文

            for (int i = 0; i < nBuffLeng; i++)
            {
                if (pRecBuff[i] == 0xEB)
                {
                    if ((nBuffLeng - i) < 5) return i;
                    if (pRecBuff[i + 3] == 0xEB)
                    {
                        nDataLeng = pRecBuff[i + 1] * 256 + pRecBuff[i + 2] + 6;
                        if ((nDataLeng + i) > nBuffLeng) return i;    //如果接收报文长度小于实际有效报文长度，则表示接收未完成

                        //如果长度足够，则判断报文的合法性，报文合法则截取这段报文，并退出，如果不合法则判定此帧报文为异常报文，丢弃
                        if ((pRecBuff[i + nDataLeng - 1] == 0xD7) && (CGlobal.GetCS(pRecBuff, 4, nDataLeng - 6) == pRecBuff[i + nDataLeng - 2]))
                        {
                            ///有效报文长度
                            nGetLeng = nDataLeng;
                            Array.Copy(pRecBuff, i, pGetBuff, 0, nGetLeng);
                            //实际截取的报文长度
                            nProcessLeng = nGetLeng + i;
                        }
                        else
                        {
                            nProcessLeng = i + 1;
                        }
                        break;
                    }
                    else
                    {
                        nProcessLeng = i + 1;
                    }
                }
                else
                {
                    nProcessLeng = i + 1;
                }
            }
            return nProcessLeng;
        }

        public int MakeMingWenFrame(byte BuffType, byte APPType, byte[] AppDatabuff, int AppdataLen, byte[] Infosave, int InfosaveLen, byte[] m_SendBuff)
        {
            return CGlobal.m_SecretEncode.MakeMingWenFrame(BuffType, APPType, AppDatabuff, AppdataLen, Infosave, InfosaveLen, m_SendBuff);
        }

        public int MakeMiWenFrame(byte type, byte Apptype, byte[] appdata, int appdataLen, byte[] Infosave, int InfosaveLen, byte[] m_SendBuff, bool bYwFlag = false)
        {
            return CGlobal.m_SecretEncode.MakeMiWenFrame(type, Apptype, appdata, appdataLen, Infosave, InfosaveLen, m_SendBuff, bYwFlag);      
        }


        public int Dowith_YW_Data(byte[] pRecvBuff)
        {
            ShowTips("运维管理：提取配电数据成功");          
            return 0;
        }
        public int DowithYWkeyVersion(byte[] pRecvBuff)
        {
            ShowTips("运维管理：返回秘钥版本");
            CEnCodePtr.m_NowKeyVersion = pRecvBuff[9];
            CEnCodePtr.SetRandData(pRecvBuff, 10, 8, 12);
            return 0;
        }
        public int DowithTermRespond(byte[] pRecvBuff)
        {
            ShowTips("运维管理：返回终端随机数");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 8, 10);
            return 0;
        }
        public int DowithTermSerialNo(byte[] pRecvBuff)
        {
            ShowTips("运维管理：返回终端序列号");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 24, 13);
            return 0;
        }
        public int DowithChipSerialNo(byte[] pRecvBuff)
        {
            ShowTips("运维管理：返回终端芯片序列号");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 8, 14);
            return 0;
        }
        public int DowithChipSecretNo(byte[] pRecvBuff)
        {
            ShowTips("运维管理：返回终端公钥");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 64, 15);
            return 0;
        }
        public int DowithPrivateKey(byte[] pRecvBuff)
        {
            ShowTips("运维管理：返回签名结果");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 64, 16);
            return 0;
        }
        public int DowithResult(byte[] pRecvBuff,byte Apptype)
        {
            int nTask = 0;
            int nResult = pRecvBuff[9] * 256 + pRecvBuff[10];
            string strStepName = "";
            string strResult = "";

            switch (Apptype)
            {
                case 0x23:
                    strStepName = "网关身份认证返回结果：";
                    nTask = (int)EncodeStatus.Auto_Master_Indentity;
                    break;
                case 0x53:
                    strStepName = "主站身份认证返回结果：";
                    nTask = (int)EncodeStatus.Auto_Master_ReadChipNo;
                    break;
                case 0x33:
                    strStepName = "运维身份认证返回结果：";
                    break;
                case 0x63: 
                    strStepName = "主站秘钥更新：";
                    //nTask = (int)EncodeStatus.Master_ReadVersion;
                    break;
                case 0x65:  
                    strStepName = "主站秘钥恢复：";
                    //nTask = (int)EncodeStatus.Master_ReadVersion;
                    break;
                case 0x71:  
                    strStepName = "主站证书更新：";
                    break;
                case 0x73:
                    strStepName = "主站证书下载：";
                    break;
                case 0x3F:  
                    strStepName = "运维证书导入：";
                    break;
                case 0x41:
                    strStepName = "运维 终端初始证书回写：";
                    break;
                case 0x47:
                    strStepName = "运维 恢复终端对称秘钥：";
                    break;
            }
            if (nResult == 0x9000)
            {
                strResult = "成功";
            }
            else
            {
                strResult = "失败";
                nTask = 0;
            }
            ShowTips(strStepName + strResult);
            return nTask;
        }
        public int DowithFramingResult(byte[] pRecvBuff)
        {
            int nResult = pRecvBuff[9] * 256 + pRecvBuff[10];
            string strStepName = "";
            string strResult = "";

            switch (CEnCodePtr.m_YWDowFileType)
            {
                case 1:
                    strStepName = "运维身份认证分帧传输：";
                    break;
                case 2:
                    strStepName = "运维证书请求文件签名分帧传输：";
                    break;
                case 3:
                    strStepName = "运维导入证书分帧传输：";
                    break;
            }
            if (nResult == 0x9000)
            {
                strResult = "成功";
            }
            else
            {
                strResult = "失败";
            }
            ShowTips(strStepName + strResult);
            return 0;
        }
        //网关认证确并请求主站 
        public int DowithGateID(byte[] pRecvBuff)
        {
            ShowTips("网关身份认证：返回签名结果");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 8, 1);
            CEnCodePtr.SetRandData(pRecvBuff, 17, 64, 2);
            CEnCodePtr.UpdateCerFileMsg(6);

            byte[] bCerBuff = null;//终端芯片的证书 
            int nCerFileLeng = CEnCodePtr.ReadCerfileCmd(ref bCerBuff, CEnCodePtr.m_Cerfilepath + CEnCodePtr.m_Cerfilename);//证书长度

            byte[] pDataBuff = new byte[16];//计算签名时的原始数据
            Array.Copy(CEnCodePtr.m_GateRandKey, 0, pDataBuff,0,8);
            Array.Copy(CEnCodePtr.m_GateTermRandKey, 0, pDataBuff, 8, 8);


            if (CGlobal.m_SecretEncode.CheckVerSign(bCerBuff, nCerFileLeng, pDataBuff, 16, CEnCodePtr.m_GateTermSign))
            {
                ShowTips("网关身份认证：验证通过");
                return (int)EncodeStatus.Auto_Gate_Request;
            }
            else
            {
                ShowTips("网关身份认证：验证失败");
                return 0;
            }
        }

        //终端认证确并请求主站 
        public int DowithTermID(byte[] pRecvBuff)
        {
            ShowTips("主站身份认证：返回签名结果");
            CEnCodePtr.SetRandData(pRecvBuff, 9, 8, 5);
            CEnCodePtr.SetRandData(pRecvBuff, 17, 64, 6);
            CEnCodePtr.UpdateCerFileMsg(6);

            return (int)EncodeStatus.Auto_Master_Request;
            /*
            byte[] bCerBuff = null;//终端芯片的证书 
            int nCerFileLeng = CEnCodePtr.ReadCerfileCmd(ref bCerBuff , CEnCodePtr.m_Cerfilename + CEnCodePtr.m_Cerfilename);//证书长度

            byte[] pDataBuff = new byte[16];//计算签名时的原始数据
            Array.Copy(CEnCodePtr.m_MasterRandKey, 0, pDataBuff, 0, 8);
            Array.Copy(CEnCodePtr.m_TermRandKey, 0, pDataBuff, 8, 8);

            if (CEnCodePtr.CheckVerSign(bCerBuff, nCerFileLeng, pDataBuff, 16, CEnCodePtr.m_TermSign))
            {
                ShowTips("主站身份认证：验证通过");
                return (int)EncodeStatus.Auto_Master_Request;
            }
            else
            {
                ShowTips("主站身份认证：验证失败");
                return 0;
            }*/
        }
        //终端返回芯片的序列号
        public int DowithTEMXP(byte[] pRecvBuff)
        {
            ShowTips("主站身份认证：终端返回芯片的序列号");
            CEnCodePtr.SetRandData(pRecvBuff,9,8,14);
            return (int)EncodeStatus.Auto_Master_ReadVersion; ;//发送读取终端秘钥版本            
        }
        
              //协议模块返回芯片的序列号
        public int Dowith_ProModule_TEMXP(byte[] pRecvBuff)
        {
            ShowTips("协议模块返回芯片的序列号");
            CEnCodePtr.SetRandData(pRecvBuff,9,8,18);
            return (int)EncodeStatus.Master_Update_Rand; ;//发送随机数           
        }


        private bool PasswordEquals(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length) return false;
            if (b1 == null || b2 == null) return false;
            for (int i = 0; i < b1.Length; i++)
                if (b1[i] != b2[i])
                { return false; }
            return true;
        }
        //协议模块返回认证数据
        public int Dowith_ProModule_Data(byte[] pRecvBuff)
        {
            byte[] RandomResult = new byte[8];
            ShowTips("协议模块返回认证数据");
            byte[] pSolveBuff=new byte[16];
             byte[] pPlainBuff=new byte[16];
             Array.Copy(pRecvBuff, 9, pSolveBuff,0,16);
              int nSolveLeng = 16;
              int result = CGlobal.m_SecretEncode.ProModuleDeCodeData(0x10, pSolveBuff, pPlainBuff, ref nSolveLeng);
              if (result == 0)
              {
                  Array.Copy(pPlainBuff, 0, RandomResult,0,8);
                  if (PasswordEquals(RandomResult, CEnCodePtr.m_ProModule_Random))
                  {
                      CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_6, 0, 0);                                        
                  }              
              }  
            return 0;         
        }

        
        //终端返回秘钥版本
        public int DowithTEMMY(byte[] pRecvBuff)
        {
            ShowTips("主站身份认证：终端返回秘钥版本");
            CEnCodePtr.m_NowKeyVersion = pRecvBuff[9];//秘钥版本号
            CEnCodePtr.SetRandData(pRecvBuff, 10, 8, 8);
            return 1;
        }
       //终端向主站返回证书
        private int DoWithTiQuTemCerti(byte[] pRecvBuff, bool bYWFlag = false)
        {

            int N = pRecvBuff[7] * 256 + pRecvBuff[8] - 3;

            CEnCodePtr.m_CerBiaosi = pRecvBuff[9];
            CEnCodePtr.m_TotalPacketNo = pRecvBuff[10];
            CEnCodePtr.m_CurrFrameNum = pRecvBuff[11];
            for (int i = 12; i < 12 + N; i++)
            {
                CEnCodePtr.m_CerDataBao.Add(pRecvBuff[i]);
            }

            ShowTips("返回终端证书数据");
            if (CEnCodePtr.m_CurrFrameNum == CEnCodePtr.m_TotalPacketNo) //接收完毕  处理存在本地
            {
                string path = CEnCodePtr.m_Cerfilepath + "TemCerti.txt";
                string str = "";
                for (int i = 0; i < CEnCodePtr.m_CerDataBao.Count; i++)
                {
                    str += string.Format("{0:X2}", CEnCodePtr.m_CerDataBao[i]);
                }
                FileStream SaveFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(SaveFile, Encoding.Default);
                SaveFile.SetLength(0);
                streamWriter.Write(str);
                streamWriter.Flush();
                streamWriter.Close();
                SaveFile.Close();

                byte[] m_Value = new byte[CEnCodePtr.m_CerDataBao.Count];
                for (int i = 0; i < CEnCodePtr.m_CerDataBao.Count; i++)
                {
                    m_Value[i]= CEnCodePtr.m_CerDataBao[i];
                }          
                path = CEnCodePtr.m_Cerfilepath + "TemCerti.cer";
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                BinaryWriter binWriter = new BinaryWriter(fs);
                binWriter.Write(m_Value, 0, m_Value.Length);
                binWriter.Close();
                fs.Close();

                if (bYWFlag)
                    return (int)EncodeStatus.YW_AnswerRead;
                else
                    return (int)EncodeStatus.Master_ReturnResult;
            }
            else
            {
                if (bYWFlag)
                    return (int)EncodeStatus.YW_Answer_OK;
                else
                    return (int)EncodeStatus.Master_Answer_OK;
            }
        }
        //秘钥更新或恢复
        public int MakeKeyFrame(byte Apptype, int NewKeyVersion, byte[] pSendBuffer)
        {
            byte[] Infosave = new byte[512];
           
            int nKeyLeng = 181;
            byte[] UpdateKeyData = new byte[512];

            if (CGlobal.m_SecretEncode.GetUpdateKey(UpdateKeyData, NewKeyVersion,ref nKeyLeng) == false) return 0;
         
          
            if( CEnCodePtr.m_UseNoSignFlag==true)
            {
                Infosave[0] = 0x00;
                Infosave[1] = 0xB6;
                Array.Copy(UpdateKeyData, 0, Infosave, 2, 181);
                Infosave[183] = CEnCodePtr.m_KeyIndex;
                return MakeMingWenFrame(0x01, Apptype, null, 0, Infosave, 184, pSendBuffer);
            }
            if (CEnCodePtr.m_SignErrFlag == true)
            {
                Infosave[0] = 0x00;
                Infosave[1] = 0xF6;
                byte[] temp = new byte[64];
                Array.Copy(UpdateKeyData, 0, Infosave, 2, 181);              
                Infosave[247] = CEnCodePtr.m_KeyIndex;
                return MakeMingWenFrame(0x01, Apptype, null, 0, Infosave, 248, pSendBuffer);
            }
            Infosave[0] = 0x00;
            Infosave[1] = 0xF6;
            Array.Copy(UpdateKeyData, 0, Infosave, 2, nKeyLeng);            
            Infosave[247] = CEnCodePtr.m_KeyIndex;
            return MakeMingWenFrame(0x01, Apptype, null, 0, Infosave, 248, pSendBuffer);
        }
        //运维恢复终端对称秘钥
        public int YW_ResetTerKeyFrame(byte Apptype, int NewKeyVersion, byte[] pSendBuffer)
        {
            byte[] Infosave = new byte[512];

            int nKeyLeng = 181;
            byte[] UpdateKeyData = new byte[512];

            if (CGlobal.m_SecretEncode.GetUpdateKey(UpdateKeyData, NewKeyVersion, ref nKeyLeng) == false) return 0;

            Infosave[0] = 0x00;
            Infosave[1] = 0xF6;
            Array.Copy(UpdateKeyData, 0, Infosave, 2, nKeyLeng);

            Infosave[247] = CEnCodePtr.m_KeyIndex;
            return MakeMingWenFrame(0x01, Apptype, null, 0, Infosave, 248, pSendBuffer);
        }
        //发送证书更新密文包
        public int SendDataPacket(byte[] pSendBuffer,byte type, byte AppType)
        {
            int nDataLeng;
            byte[] pDataBuff = new byte[CEnCodePtr.PacketLeng];
            nDataLeng = CEnCodePtr.GetFileContent(pDataBuff);
            if (nDataLeng == 0) return 0;

            byte[] Infosave = new byte[nDataLeng + 5];
            Infosave[0] = Convert.ToByte((nDataLeng + 3) / 256);
            Infosave[1] = Convert.ToByte((nDataLeng + 3) % 256);
            Infosave[2] = CEnCodePtr.m_CerBiaosi;
            Infosave[3] = Convert.ToByte(CEnCodePtr.m_TotalPacketNo);
            Infosave[4] = CEnCodePtr.m_CurrFrameNum;
            Array.Copy(pDataBuff, 0, Infosave, 5, nDataLeng);
            CEnCodePtr.m_CurrFrameNum++;
            return MakeMingWenFrame(type, AppType, null, 0, Infosave, nDataLeng + 5, pSendBuffer);

        }
        public int SendYWDataPacket(byte[] pSendBuffer, byte type, byte AppType)
        {
            int nDataLeng;
            byte[] pDataBuff = new byte[CEnCodePtr.PacketLeng];
            nDataLeng = CEnCodePtr.GetFileContent(pDataBuff);
            if (nDataLeng == 0) return 0;
            byte[] Infosave = new byte[nDataLeng + 13];
            Infosave[0] = Convert.ToByte((nDataLeng + 11) / 256);
            Infosave[1] = Convert.ToByte((nDataLeng + 11) % 256);
            Infosave[2] = 7;
            Infosave[3] = Convert.ToByte(CEnCodePtr.m_TotalPacketNo);
            Infosave[4] = CEnCodePtr.m_CurrFrameNum;
            Array.Copy(pDataBuff, 0, Infosave, 5, nDataLeng);
            Array.Copy(CEnCodePtr.m_YW_RandKey, 0, Infosave, 5 + nDataLeng, 8);
            nDataLeng = nDataLeng + 13;
            CEnCodePtr.m_CurrFrameNum++;
            return MakeMingWenFrame(type, AppType, null, 0, Infosave, nDataLeng, pSendBuffer);
        }
        public int SendYWFileSignPacket(byte[] pSendBuffer, byte type, byte AppType)
        {
            int nDataLeng;
            byte[] pDataBuff = new byte[CEnCodePtr.PacketLeng];
            nDataLeng = CEnCodePtr.GetFileContent(pDataBuff);
            if (nDataLeng == 0) return 0;
            byte[] Infosave = new byte[nDataLeng + 1024];

            Infosave[0] = Convert.ToByte((nDataLeng + 2) / 256);
            Infosave[1] = Convert.ToByte((nDataLeng + 2) % 256);
            Infosave[2] = Convert.ToByte(CEnCodePtr.m_TotalPacketNo);
            Infosave[3] = CEnCodePtr.m_CurrFrameNum;
            Array.Copy(pDataBuff, 0, Infosave, 4, nDataLeng);
            nDataLeng = nDataLeng + 4;

            CEnCodePtr.m_CurrFrameNum++;

            return CGlobal.m_SecretEncode.MakeMiWenFrame(type, AppType, Infosave, nDataLeng, null, 0, pSendBuffer,true);
        }
        public virtual string GetBuffContent(byte[] pBuffer)
        {
            return "";
        }
        private bool CheckInfoLen(int Len, byte Apptype)
        {
            int i;
            for (i = 0; i < (m_AppErrMsg.Length/2); i++)
            {
                if (Apptype == m_AppErrMsg[i, 0])
                {
                    if (Len != m_AppErrMsg[i, 1])
                    {
                        string strErr = string.Format("应用类型{0:X2}，长度错误", Apptype);
                        ShowTips(strErr);
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
        private bool GetNoSecretBuff(Byte[] pRecvBuff)
        {
            int nSecretLeng = pRecvBuff[1]*256 + pRecvBuff[2];
            nSecretLeng -= 2;
            if (nSecretLeng <= 0) return false;
            Array.Copy(pRecvBuff, 6, pSolveBuff, 0, nSecretLeng);        
            if (CGlobal.m_SecretEncode.DeCodeData(pRecvBuff[5],nSecretLeng, pSolveBuff, pPlainBuff, ref nSolveLeng) == false) return false;
            Array.Copy(pPlainBuff, 0, pRecvBuff, 6, nSolveLeng);
            return true;
        }
        private int GetIEC_Buff(Byte[] pRecvBuff, byte AppType)
        {
            int nMsgLeng = 0;//信息扩展区报文长度        
            int IEC_Len = pRecvBuff[7];//IEC报文长度
            nMsgLeng = pRecvBuff[8 + IEC_Len] * 256 + pRecvBuff[9 + IEC_Len];
            if (CheckInfoLen(nMsgLeng, AppType)) return 0;

            if (AppType == 2 || AppType == 3 || AppType == 6 || AppType == 7 || AppType == 8)
            {
                CEnCodePtr.SetRandData(pRecvBuff, 10 + IEC_Len,8,5);
            }
            for (int i = 0; i < IEC_Len; i++)
            {
                pRecvBuff[i] = pRecvBuff[8 + i];
            }
//            CGlobal.AddShowBuff("接收", pRecvBuff, IEC_Len);
            return IEC_Len;
        }
        private void ShowAppErrMsg(Byte[] pRecvBuff, byte AppType)
        {
            int nMsgLeng = pRecvBuff[7] * 256 + pRecvBuff[8];
            if (CheckInfoLen(nMsgLeng, AppType)) return;
            string strIndex = string.Format("{0:X2}", pRecvBuff[10]);
            int nInde = Convert.ToInt32(strIndex);
            if (nInde > 0 && nInde <= m_MsgErr.Length && pRecvBuff[9] == 0x91)
            {
                ShowTips(m_MsgErr[nInde - 1]);
            }
        }
        public virtual void Process(Byte[] pRecvBuff, ref int pRecvLeng, Byte[] pSendBuff, ref int pSendLeng)
        {
            if (pRecvLeng == 0) return;
            int nTaskNo = 0;
            bool bSectetFlag = false;
            pRecvLeng = 0;
            if ((pRecvBuff[5] & 0x08) > 0) bSectetFlag = true;
            if (bSectetFlag)
            {               
                if (GetNoSecretBuff(pRecvBuff) == false) return;
                if (m_NowCerTask != null) m_NowCerTask.m_AnswerFlag = true;
            }
            byte AppType = pRecvBuff[6]; //应用类型
            int nMsgLeng = 0;//信息扩展区报文长度        

            if (AppType < 0x1F) //有应用数据区101  104
            {
                pRecvLeng = GetIEC_Buff(pRecvBuff, AppType);
            }
            else if (AppType == 0x1f)
            {
                m_bCheckTimeOut = false;
                ShowAppErrMsg(pRecvBuff, AppType);
            }
            else
            {
                
                nMsgLeng = pRecvBuff[7] * 256 + pRecvBuff[8];//信息扩展区报文长度
                if (CheckInfoLen(nMsgLeng, AppType)) return;

                switch (AppType)
                {
                    case 0x21:          //网关认证应答
                        nTaskNo = DowithGateID(pRecvBuff);
                        break;
                    case 0x23:          //网关反馈应答
                    case 0x53:          //终端向主站返回认证结果
                    case 0x63:          //终端返回秘钥更新结果
                    case 0x65:          //终端返回秘钥恢复结果
                    case 0x71:          //终端返回证书远程更新结果
                    case 0x73:          //终端返回证书远程下载结果
                    case 0x33:          //运维 终端向证书管理工具返回认证结果
                    case 0x3F:          //终端返回证书导入结果
                    case 0x41:          //终端初始证书回写
                    case 0x47:
                        nTaskNo = DowithResult(pRecvBuff, AppType);
                        break;
                    case 0x51:          //终端认证确并请求主站    
                        nTaskNo = DowithTermID(pRecvBuff);
                        break;
                    case 0x55:          //终端返回芯片的序列号
                        nTaskNo = DowithTEMXP(pRecvBuff);
                        break;
                    case 0x61:          //终端返回秘钥版本
                        nTaskNo = DowithTEMMY(pRecvBuff);
                        break;
                    case 0x75:          //终端向主站返回证书
                        nTaskNo = DoWithTiQuTemCerti(pRecvBuff);
                        break;
                    case 0x31:          //运维 终端响应证书管理工具请求
                        nTaskNo = DowithTermRespond(pRecvBuff);
                        break;
                    case 0x35:          //运维 终端返回秘钥版本
                        nTaskNo = DowithYWkeyVersion(pRecvBuff);
                        break;
                    case 0x37:          //运维 终端序列号
                        nTaskNo = DowithTermSerialNo(pRecvBuff);
                        break;
                    case 0x39:          //终端芯片序列号
                        nTaskNo = DowithChipSerialNo(pRecvBuff);
                        break;
                    case 0x3B:          //终端芯片公钥
                        nTaskNo = DowithChipSecretNo(pRecvBuff);
                        break;
                    case 0x3D:          //运维 返回签名结果
                        nTaskNo = DowithPrivateKey(pRecvBuff);
                        break;
                    case 0x43:          //运维 接收应答
                        nTaskNo = DoWithTiQuTemCerti(pRecvBuff,true);
                        break;
                    case 0x45:          //运维工具分帧传输结果
                        nTaskNo = DowithFramingResult(pRecvBuff);
                        break;
                    case 0x81:   //处理协议模块返回的芯片序列号
                         nTaskNo = Dowith_ProModule_TEMXP(pRecvBuff);
                        break;
                    case 0x83://处理协议模块返回的认证数据
                         nTaskNo = Dowith_ProModule_Data(pRecvBuff);
                         break;
                    case 0x49:
                         nTaskNo = Dowith_YW_Data(pRecvBuff);
                         break;
                    default:
                        return;

                }
                if (m_NowCerTask != null) m_NowCerTask.m_AnswerFlag = true;
                pSendLeng = 0;
                if (nTaskNo > 0 && m_EncodeStatus <= EncodeStatus.Auto_Master_ReadVersion)
                {
                    pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                }
                else
                {
                    switch (nTaskNo)
                    {
                        case (int)EncodeStatus.YW_AnswerRead:
                        case (int)EncodeStatus.Master_ReturnResult:
                        case (int)EncodeStatus.YW_Answer_OK:
                        case (int)EncodeStatus.Master_Answer_OK:
                        case (int)EncodeStatus.Master_ReadVersion:
                            pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                            break;

                    }
                }
            }
        }
        public virtual int GetTaskBuffer(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;
            byte[] pGetBuff = new byte[512];
            byte[] Infosave = new byte[512]; ;
            byte[] pSignBuff = new byte[128];//签名结果
            int nSignLeng = 64;//签名结果长度

            m_EncodeStatus = (EncodeStatus)nTaskNo;
            switch (nTaskNo)
            {
                case (int)EncodeStatus.Auto_Gate_Identity:
                case (int)EncodeStatus.Gate_Identity:               
                    Infosave[0] = 0x00;
                    Infosave[1] = 0x08;
                    if (CGlobal.m_SecretEncode.GetRand(pGetBuff) == false) return 0;
                    CEnCodePtr.SetRandData(pGetBuff, 0, 8, 0);
                    Array.Copy(CEnCodePtr.m_GateRandKey, 0, Infosave, 2, 8);
                    CEnCodePtr.FreshEncodeFormOperateMsg("网关身份认证请求");
                    nSendLeng = MakeMingWenFrame(0x81, 0x20, null, 0, Infosave, 10, pSendBuffer); //网关认证
                    break;
                case (int)EncodeStatus.Auto_Gate_Request:
                case (int)EncodeStatus.Gate_Request:
                    if (CEnCodePtr.m_UseNoSignFlag)
                    {
                        Infosave[0] = 0x00;
                        Infosave[1] = 0x01;
                        Infosave[2] = 5;       //签名秘钥标识
                        CEnCodePtr.FreshEncodeFormOperateMsg("网关身份认证无签名");
                        nSendLeng = MakeMingWenFrame(0x81, 0x22, null, 0, Infosave, 3, pSendBuffer);
                    }
                    else
                    {
                        Infosave[0] = 0x00;
                        Infosave[1] = 0x41;
                        if (CGlobal.m_SecretEncode.GetSign(CEnCodePtr.m_GateTermRandKey, 8, pGetBuff, ref nSignLeng, 5) == false) return 0;
                        CEnCodePtr.SetRandData(pGetBuff, 0, nSignLeng, 3);
                        Array.Copy(CEnCodePtr.m_GateSign, 0, Infosave, 2, nSignLeng);
                        Infosave[66] = 5;       //签名秘钥标识
                        CEnCodePtr.FreshEncodeFormOperateMsg("网关身份认证签名");
                        nSendLeng = MakeMingWenFrame(0x81, 0x22, null, 0, Infosave, 67, pSendBuffer);
                    }
                    break;
                case (int)EncodeStatus.Auto_Master_Indentity:
                case (int)EncodeStatus.Master_Indentity:
                    Infosave[0] = 0x00;
                    Infosave[1] = 0x08;
                    if (CGlobal.m_SecretEncode.GetRand(pGetBuff) == false) return 0;
                    CEnCodePtr.SetRandData(pGetBuff, 0, 8, 4);
                    Array.Copy(CEnCodePtr.m_MasterRandKey, 0, Infosave, 2, 8);
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站身份认证请求");
                    nSendLeng = MakeMingWenFrame(0x01, 0x50, null, 0, Infosave, 10, pSendBuffer);//主站认证
                    break;
                case (int)EncodeStatus.Auto_Master_Request:  
                case (int)EncodeStatus.Master_Request:
                    if (CEnCodePtr.m_UseNoSignFlag)
                    {
                        Infosave[0] = 0x00;
                        Infosave[1] = 0x01;                     
                        Infosave[2] = CEnCodePtr.m_KeyIndex;//签名秘钥标识
                        CEnCodePtr.FreshEncodeFormOperateMsg("主站身份认证无签名");
                        nSendLeng = MakeMingWenFrame(0x01, 0x52, null, 0, Infosave, 3, pSendBuffer);
                    }
                    else
                    {
                    Infosave[0] = 0x00;
                    Infosave[1] = 0x41;
                    if (CGlobal.m_SecretEncode.GetSign(CEnCodePtr.m_TermRandKey, 8, pGetBuff, ref nSignLeng, CEnCodePtr.m_KeyIndex) == false) return 0;
                    CEnCodePtr.SetRandData(pGetBuff, 0, nSignLeng, 7);
                    Array.Copy(CEnCodePtr.m_MasterSign, 0, Infosave, 2, nSignLeng);
                    Infosave[66] = CEnCodePtr.m_KeyIndex;//签名秘钥标识
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站身份认证签名");
                    nSendLeng = MakeMingWenFrame(0x01, 0x52, null, 0, Infosave, 67, pSendBuffer);
                    }
                    break;
                case (int)EncodeStatus.Auto_Master_ReadChipNo:
                case (int)EncodeStatus.Master_ReadChipNo:    
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站读取终端芯片序列号");
                    nSendLeng = MakeMingWenFrame(0x01, 0x54, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.Auto_Master_ReadVersion:
                case (int)EncodeStatus.Master_ReadVersion:   
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站读取终端秘钥版本");
                    nSendLeng = MakeMingWenFrame(0x01, 0x60, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.Master_UpdateSecret:   
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站执行远程秘钥更新");
                    nSendLeng = MakeKeyFrame(0x62, CEnCodePtr.m_NowKeyVersion + 1, pSendBuffer);
                    break;
                case (int)EncodeStatus.Master_ResetSecret:
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站执行远程秘钥恢复");
                    nSendLeng = MakeKeyFrame(0x64, 0, pSendBuffer);
                    break;
                case (int)EncodeStatus.Master_UpdateCer:             
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站向终端发起证书更新");
                    nSendLeng = SendDataPacket(pSendBuffer, 0x01, 0x70);
                    break;
                case (int)EncodeStatus.Master_DownCer:            
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站下发终端证书");
                    nSendLeng = SendDataPacket(pSendBuffer, 0x01, 0x72);
                    break;
                case (int)EncodeStatus.Master_ReadCer:                  
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.m_CerDataBao.Clear();
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站读取终端证书");
                    nSendLeng = MakeMingWenFrame(0x01, 0x74, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.Master_ReturnResult:             
                    Infosave[0] = 0;
                    Infosave[1] = 0x02;
                    Infosave[2] = 0x90;
                    Infosave[3] = 0x00;
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站发送读取终端证书结果:正确");
                    nSendLeng = MakeMingWenFrame(0x01, 0x76, null, 0, Infosave, 4, pSendBuffer);
                    break;

                case (int)EncodeStatus.Master_Update_Vertify:
                    byte[] inData = new byte[30];
                    Array.Copy(CGlobal.m_Device.MD5, 0, inData, 0, 16);
                    byte[] m_Time = CEnCodePtr.GetCurrentTime();
                    Array.Copy(m_Time, 0, inData, 16, 6); 
                    CEnCodePtr.GetTermRandData(inData, 22);
                    if (CGlobal.m_SecretEncode.GetSign(inData, 30, pGetBuff, ref nSignLeng, CEnCodePtr.m_KeyIndex) == false)
                    {
                        CEnCodePtr.FreshEncodeFormOperateMsg("升级验证包签名错误");
                        return 0;

                    }
                    Infosave[0] = 0;
                    Infosave[1] = 0x4F;
                    Array.Copy(inData, 16, Infosave, 2, 6);
                    CEnCodePtr.GetTermRandData(Infosave, 8);                    
                    Array.Copy(pGetBuff, 0, Infosave, 16,64);
                    Infosave[80] = CEnCodePtr.m_KeyIndex;
                    byte[] appdata = new byte[0];
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站发送升级验证包报文");
                    nSendLeng = MakeMiWenFrame(0x09, 0x08, appdata, 0, Infosave, 81, pSendBuffer, false);
                    break;
                case(int)EncodeStatus.Master_Update_Start:
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站下发升级启动报文");
                    nSendLeng = MakeMingWenFrame(0x01, 0x80, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.Master_Update_Rand:
                   Infosave[0] = 0x00;
                    Infosave[1] = 0x08;
                    if (CGlobal.m_SecretEncode.GetRand(pGetBuff) == false) return 0;
                    CEnCodePtr.SetRandData(pGetBuff, 0, 8,19);
                    Array.Copy(CEnCodePtr.m_ProModule_Random, 0, Infosave, 2, 8);
                    CEnCodePtr.FreshEncodeFormOperateMsg("主站对协议模块发送随机数");
                    nSendLeng = MakeMingWenFrame(0x01, 0x82, null, 0, Infosave, 10, pSendBuffer);
                   
                    break;                  
                case (int)EncodeStatus.YW_Identity:
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 身份认证");
                    nSendLeng = SendYWDataPacket(pSendBuffer, 0x42, 0x30);
                    break;
                case (int)EncodeStatus.YW_Request:
                    Infosave[0] = 0x00;
                    Infosave[1] = 0x40;
                    if (CGlobal.m_SecretEncode.YW_GetSign(CEnCodePtr.m_YW_TermRand, 8, pGetBuff, ref nSignLeng, CEnCodePtr.m_KeyIndex) == false) return 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维身份认证签名");
                    CEnCodePtr.SetRandData(pGetBuff, 0, nSignLeng, 11);
                    Array.Copy(CEnCodePtr.m_YW_Sign, 0, Infosave, 2, nSignLeng);
                    nSendLeng = MakeMingWenFrame(0x42, 0x32, null, 0, Infosave, 66, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_ReadVersion:
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 提取终端秘钥版本");
                    nSendLeng = MakeMingWenFrame(0x42, 0x34, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_ReadSerialNo:    
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 读取终端序列号");
                    nSendLeng = MakeMingWenFrame(0x42, 0x36, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_ReadChipNo: 
                    Infosave = new byte[2];
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 读取终端芯片序列号");
                    nSendLeng = MakeMingWenFrame(0x42, 0x38, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_ReadSecretKey:
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 读取安全芯片公钥");
                    nSendLeng = MakeMingWenFrame(0x42, 0x3A, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_FileSign:    
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 将生成的证书请求文件发送给终端");
                    nSendLeng = SendYWFileSignPacket(pSendBuffer, 0x4A, 0x3C);
                    break;
                case (int)EncodeStatus.YW_DownCer:
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 证书导入");
                    nSendLeng = SendDataPacket(pSendBuffer, 0x42, 0x3E);
                    break;
                case (int)EncodeStatus.YW_ReWrite:
                    Infosave = new byte[2];
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 终端初始证书回写");
                    nSendLeng = MakeMingWenFrame(0x42, 0x40, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_ReadTerCer:
                    Infosave = new byte[2];
                    Infosave[0] = 0;
                    Infosave[1] = 0;
                    CEnCodePtr.m_CerDataBao.Clear();
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 读取终端证书");
                    nSendLeng = MakeMingWenFrame(0x42, 0x42, null, 0, Infosave, 2, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_AnswerRead:
                    Infosave[0] = 0;
                    Infosave[1] = 0x02;
                    Infosave[2] = 0x90;
                    Infosave[3] = 0x00;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维发送读取终端证书结果：正确");
                    nSendLeng = MakeMingWenFrame(0x42, 0x44, null, 0, Infosave, 4, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_ResetKey:               
                    Infosave[0] = 0;
                    Infosave[1] = 0xB9;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维 恢复终端对称秘钥");
                    if (CGlobal.m_SecretEncode.GetSecretPacketFile() == false) return 0;
                    Array.Copy(CEnCodePtr.m_YW_ResetSecret, 0, Infosave, 2, 185);
                    nSendLeng = MakeMingWenFrame(0x42, 0x46, null, 0, Infosave, 187, pSendBuffer);
                    break;
                case (int)EncodeStatus.YW_Answer_OK:
                    Infosave[0] = 0;
                    Infosave[1] = 0x02;
                    Infosave[2] = 0x90;
                    Infosave[3] = 0x00;
                    CEnCodePtr.FreshEncodeFormOperateMsg("分帧确认应答");
                    nSendLeng = MakeMingWenFrame(0x42, 0x45, null, 0, Infosave, 4, pSendBuffer);
                    break;

                case (int)EncodeStatus.YW_SendData:
                    int nPlace = 0;                                 
                    byte[] appData = new byte[19];
                    appData[nPlace++] = 18;
                    appData[nPlace++] = 0x68;
                    appData[nPlace++] = 0x0C;
                    appData[nPlace++] = 0x0C;
                    appData[nPlace++] = 0x68;
                    appData[nPlace++] = 0x53;
                    appData[nPlace++] = 0x01;
                    appData[nPlace++] = 0x00;
                    appData[nPlace++] = 0x64;
                    appData[nPlace++] = 0x01;
                    appData[nPlace++] = 0x06;
                    appData[nPlace++] = 0x00;
                    appData[nPlace++] = 0x01;
                    appData[nPlace++] = 0x00;
                    appData[nPlace++] = 0x00;
                    appData[nPlace++] = 0x00;
                    appData[nPlace++] = 0x14;
                    appData[nPlace++] = 0xD4;                  
                    appData[nPlace++] = 0x16;
                    CEnCodePtr.FreshEncodeFormOperateMsg("运维发送配电数据报文");
                    nSendLeng = MakeMiWenFrame(0x4A, 0x48, appData, nPlace, null, 0, pSendBuffer, true);                   
                    break;
                default:
                    return 0;
            }
            return nSendLeng;
        }

        public int MakeFrame_AppType_00(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {
            byte[] Infosave = new byte[2] { 0, 0 };//信息安全扩展区 0x00
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x00, appdata, nSendLeng + 1, Infosave, 2, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x00, appdata, nSendLeng + 1, Infosave, 2, pSendBuffer);
            }
        }
        public int MakeFrame_AppType_07(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {
            byte[] sData = new byte[64];//签名结果
            int sDataLen = 64;//签名结果长度
            byte[] inData = new byte[1000];
            int inDataLen = 0;

            byte[] m_Time = CEnCodePtr.GetCurrentTime();
            byte[] Infosave = new byte[81];
            Infosave[0] = 0x00;
            Infosave[1] = 0x4F;
            Array.Copy(m_Time, 0, Infosave, 2, 6);
            CEnCodePtr.GetTermRandData(Infosave, 8);

            inDataLen = nSendLeng + 6 + 8;
            for (int i = 0; i < nSendLeng; i++)
            {
                inData[i] = pSendBuffer[i];
            }
            for (int i = 0; i < 6; i++)
            {
                inData[i + nSendLeng] = Infosave[2 + i];
            }
            CEnCodePtr.GetTermRandData(inData, nSendLeng + 6);
            if (CGlobal.m_SecretEncode.GetSign(inData, inDataLen, sData, ref sDataLen, CEnCodePtr.m_KeyIndex) == false) return 0;
            for (int k = 0; k < 64; k++)
            {
                Infosave[k + 16] = sData[k];
            }
            Infosave[80] = CEnCodePtr.m_KeyIndex;

            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x07, appdata, nSendLeng + 1, Infosave, 81, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x07, appdata, nSendLeng + 1, Infosave, 81, pSendBuffer);
            }


        }
        public int MakeFrame_AppType_05(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {
            byte[] sData = new byte[64];//签名结果
            int sDataLen = 64;//签名结果长度
            byte[] inData = new byte[1000];
            int inDataLen = 0;
            byte[] Infosave = Infosave = new byte[73];
            byte[] m_Time = CEnCodePtr.GetCurrentTime();
            Infosave[0] = 0x00;
            Infosave[1] = 0x47;
            Array.Copy(m_Time, 0, Infosave, 2, 6);
            inDataLen = nSendLeng + 6;
            for (int i = 0; i < nSendLeng; i++)
            {
                inData[i] = pSendBuffer[i];
            }
            for (int i = 0; i < 6; i++)
            {
                inData[i + nSendLeng] = Infosave[2 + i];
            }
            if (CGlobal.m_SecretEncode.GetSign(inData, inDataLen, sData, ref sDataLen, CEnCodePtr.m_KeyIndex) == false) return 0;

            for (int k = 0; k < 64; k++)
            {
                Infosave[k + 8] = sData[k];
            }
            Infosave[72] = CEnCodePtr.m_KeyIndex;
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x05, appdata, nSendLeng + 1, Infosave, 73, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x05, appdata, nSendLeng + 1, Infosave, 73, pSendBuffer);
            }

        }
        public int MakeFrame_AppType_01(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {
            byte[] sData = new byte[64];//签名结果
            int sDataLen = 64;//签名结果长度          

            byte[] Infosave = new byte[67];//信息安全扩展区 0x01
            Infosave[0] = 0x00;
            Infosave[1] = 0x41;
            if (CGlobal.m_SecretEncode.GetSign(pSendBuffer, nSendLeng, sData, ref sDataLen, CEnCodePtr.m_KeyIndex) == false) return 0;
            for (int k = 0; k < 64; k++)
            {
                Infosave[k + 2] = sData[k];
            }
            Infosave[66] = CEnCodePtr.m_KeyIndex;
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x01, appdata, nSendLeng + 1, Infosave, 67, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x01, appdata, nSendLeng + 1, Infosave, 67, pSendBuffer);
            }


        }
        public int MakeFrame_AppType_03(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {
            byte[] sData = new byte[64];//签名结果
            int sDataLen = 64;//签名结果长度
            byte[] inData = new byte[1000];
            int inDataLen = 0;

            byte[] Infosave = new byte[75];//信息安全扩展区 0x03
            Infosave[0] = 0x00;
            Infosave[1] = 0x49;
            CEnCodePtr.GetTermRandData(Infosave, 2);

            inDataLen = nSendLeng + 8;
            for (int i = 0; i < nSendLeng; i++)
            {
                inData[i] = pSendBuffer[i];
            }
            CEnCodePtr.GetTermRandData(inData, nSendLeng);
            if (CGlobal.m_SecretEncode.GetSign(inData, inDataLen, sData, ref sDataLen, CEnCodePtr.m_KeyIndex) == false) return 0;

            for (int k = 0; k < 64; k++)
            {
                Infosave[k + 10] = sData[k];
            }
            Infosave[74] = CEnCodePtr.m_KeyIndex;
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x03, appdata, nSendLeng + 1, Infosave, 75, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x03, appdata, nSendLeng + 1, Infosave, 75, pSendBuffer);
            }

        }
        public int MakeFrame_AppType_04(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {

            byte[] Infosave = Infosave = new byte[8];
            byte[] m_Time = CEnCodePtr.GetCurrentTime();
            Infosave[0] = 0x00;
            Infosave[1] = 0x06;
            Array.Copy(m_Time, 0, Infosave, 2, 6);
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x04, appdata, nSendLeng + 1, Infosave, 8, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x04, appdata, nSendLeng + 1, Infosave, 8, pSendBuffer);
            }

        }
        public int MakeFrame_AppType_06(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {

            byte[] Infosave = Infosave = new byte[16];
            byte[] m_Time = CEnCodePtr.GetCurrentTime();
            Infosave[0] = 0x00;
            Infosave[1] = 0x06;
            Array.Copy(m_Time, 0, Infosave, 2, 6);
            CEnCodePtr.GetTermRandData(Infosave, 8);
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x06, appdata, nSendLeng + 1, Infosave, 16, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x06, appdata, nSendLeng + 1, Infosave, 16, pSendBuffer);
            }

        }
        public int MakeFrame_AppType_02(int nSendLeng, byte[] pSendBuffer, byte[] appdata)
        {

            byte[] Infosave = Infosave = new byte[10];
            Infosave[0] = 0x00;
            Infosave[1] = 0x06;
            CEnCodePtr.GetTermRandData(Infosave, 2);
            if (CEnCodePtr.m_UseMingWenFlag)
            {

                return nSendLeng = CGlobal.m_SecretEncode.MakeMingWenFrame(0x01, 0x02, appdata, nSendLeng + 1, Infosave, 10, pSendBuffer);
            }
            else
            {
                return nSendLeng = CGlobal.m_SecretEncode.MakeMiWenFrame(0x09, 0x02, appdata, nSendLeng + 1, Infosave, 10, pSendBuffer);
            }

        }

        public int GetTaskBuffer_Encode(Byte[] pSendBuffer, int nTaskNo, int nSendLeng)
        {
            byte[] appdata = new byte[nSendLeng + 1];
            appdata[0] = Convert.ToByte(nSendLeng);
            for (int i = 1; i < appdata.Length; i++)
            {
                appdata[i] = pSendBuffer[i - 1];
            }
            switch (nTaskNo)
            {
                case 1: //发送连接报文
                case 2:  //发送测试报文
                case 3:  //发送停止报文    
                case 20://通讯测试应答
                case 21://S格式  
                case 40:
                    byte[] Infosave = new byte[2] { 0, 0 };
                    nSendLeng = MakeMingWenFrame(0x01, 0x00, appdata, nSendLeng + 1, Infosave, 2, pSendBuffer);
                    break;
                case 39:              
                case (int)TI_Type.R_MC_PA_1:          //读4G参数
                case (int)TI_Type.W_MC_PA_1:
                case (int)TI_Type.R_4G_PA_1:          //读4G参数
                case (int)TI_Type.W_4G_PA_1:
                case (int)TI_Type.R_NT_PA_1:         //读IP地址
                case (int)TI_Type.W_NT_PA_1:          //写IP地址
                case (int)TI_Type.R_GW_PA_1:         //读网关地址
                case (int)TI_Type.W_GW_PA_1:          //写网关地址
                case (int)TI_Type.R_SR_PA_1:          //读串口地址
                case (int)TI_Type.W_SR_PA_1:          //写串口地址
                case (int)TI_Type.R_PI_PA_1:          //读主站参数
                case (int)TI_Type.W_PI_PA_1:          //写主站参数 0x8e
                case (int)TI_Type.R_TI_PA_1:          //读终端配置                      
                case (int)TI_Type.W_TI_PA_1:          //写终端配置 
                case (int)TI_Type.Restart_D:
                case 4://发送总召报文
                case 5://发送对时报文
                case 7://召电度
                case 8://读定值参数  
                case 22://读目录
                case 23://读文件激活
                case 24://读文件数据传输确认
                case 28: //读取所有的参数
                case 30://发送写文件数据传输命令
                case 35:  //国网升级结束
                case 36://国网升级撤销
                case 25://发送校验报文
                case 26://发送召唤系数
                case 27://发送下装系数
                case 31://发送召唤绝对相角
                case 32://发送直流校验
                case 33: //私有升级
                case 41://读时钟
                    nSendLeng = MakeFrame_AppType_00(nSendLeng, pSendBuffer, appdata);
                    break;
                case 6://遥控
                    byte[] m_Time = CEnCodePtr.GetCurrentTime();
                    if (pYkMsg.m_YkStatus == 1)//执行
                    {
                        if (CEnCodePtr.m_UseNoSignFlag)
                        {
                            nSendLeng = MakeFrame_AppType_06(nSendLeng, pSendBuffer, appdata);
                        }
                        else if (CEnCodePtr.m_UseNoTimeFlag)
                        {
                            nSendLeng = MakeFrame_AppType_03(nSendLeng, pSendBuffer, appdata);
                        }
                        else if (CEnCodePtr.m_UseNoRandFlag)
                        {
                            nSendLeng = MakeFrame_AppType_05(nSendLeng, pSendBuffer, appdata);
                        }
                        else
                        {
                            nSendLeng = MakeFrame_AppType_07(nSendLeng, pSendBuffer, appdata);
                        }
                    }
                    else
                    {
                        if (CEnCodePtr.m_UseNoSignFlag == true)
                        {
                            nSendLeng = MakeFrame_AppType_04(nSendLeng, pSendBuffer, appdata);
                        }
                        else if (CEnCodePtr.m_UseNoTimeFlag == true)
                        {
                            nSendLeng = MakeFrame_AppType_01(nSendLeng, pSendBuffer, appdata);
                        }
                        else
                        {
                            nSendLeng = MakeFrame_AppType_05(nSendLeng, pSendBuffer, appdata);
                        }
                    }
                    break;
                case 29://发送写文件激活命令
                case 38://切换定值区号
                case 34:  //国网升级启动                  
                case 9://定值参数预置  
                    if (CEnCodePtr.m_UseNoSignFlag)
                    {
                        nSendLeng = MakeFrame_AppType_00(nSendLeng, pSendBuffer, appdata);
                    }
                    else
                    {
                        nSendLeng = MakeFrame_AppType_01(nSendLeng, pSendBuffer, appdata);
                    }
                    break;
                case 10://定值参数固化
                    if (CEnCodePtr.m_UseNoSignFlag)
                    {
                        nSendLeng = MakeFrame_AppType_02(nSendLeng, pSendBuffer, appdata);
                    }
                    else if (CEnCodePtr.m_UseNoRandFlag)
                    {
                        nSendLeng = MakeFrame_AppType_01(nSendLeng, pSendBuffer, appdata);
                    }
                    else
                    {
                        nSendLeng = MakeFrame_AppType_03(nSendLeng, pSendBuffer, appdata);
                    }
                    break;
                case 37:
                    nSendLeng = MakeFrame_AppType_00(nSendLeng, pSendBuffer, appdata);
                    break;
                case 42:  //参数撤销
                    nSendLeng = MakeFrame_AppType_03(nSendLeng, pSendBuffer, appdata);
                    break;
            }
            return nSendLeng;
        }
    }
}
