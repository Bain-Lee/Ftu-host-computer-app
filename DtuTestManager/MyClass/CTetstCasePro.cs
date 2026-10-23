using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DtuTestManager
{
    public class  CTetstCasePro
    {
        public string m_TestCaseName = "";
        public int m_CaseNumber = 0;
        public int m_NowSelIndex = -1;
        public int m_CaseTaskNo = -1;
        public List<string> m_ItemNameList = new List<string>();
        public List<CCaseItem> m_ItemList = new List<CCaseItem>();

        public CTetstCasePro(int nCaseNumber,string strName)
        {
            m_CaseNumber = nCaseNumber;
            m_TestCaseName = strName;
        }
        public void AddTestCaseItem(string strName)
        {
            CCaseItem pCase = new CCaseItem(m_CaseNumber + m_ItemList.Count, strName);
            m_ItemList.Add(pCase);
            m_ItemNameList.Add(strName);
        }
        public void SetSelIndex(string strContent)
        {
            int i;
            for (i = 0; i < m_ItemList.Count; i++)
            {
                if (m_ItemList[i].m_CaseName == strContent)
                {
                    m_NowSelIndex = i;
                    m_CaseTaskNo = m_ItemList[i].m_CaseTaskNo;
                    return;
                }
            }
        }
    }
    public class CCaseItem
    {
        public string m_CaseName = "";
        public int m_CaseTaskNo = 0;
        public CCaseItem(int nCaseNumber, string strName)
        {
            m_CaseName = strName;
            m_CaseTaskNo = nCaseNumber;
        }
    }
    public partial class C101Ptr : CPtrInterface
    {

        private int ExpSend_Short_Report(byte iType, byte[] SendB,bool ErrAddrFlag, bool ErrCSflag, bool ErrEndFlag)
        {
            int tempLAddr = theDevice.m_ASDUAddr;
            int nPlace = 0;
            SendB[nPlace++] = 0x10;
            SendB[nPlace++] = iType;
            if (ErrAddrFlag == true)
            {
                tempLAddr = theDevice.m_ASDUAddr + 1;
            }
            nPlace += SetAddress(SendB, nPlace, tempLAddr);
           
            SendB[nPlace] = CGlobal.GetCS(SendB, 1, nPlace - 1);
            if (ErrCSflag == true)
            {
                SendB[nPlace] =Convert.ToByte( SendB[nPlace]+1);
            }
            nPlace++;          
            SendB[nPlace] = 0x16;
            if (ErrEndFlag == true)
            {
                SendB[nPlace] = 0; ;
            }
            nPlace++;
            return nPlace;
        }
        private int ExpSendCallTotalData(byte[] SendB,bool ErrAddr,bool ErrCtrl,bool ErrCot,bool ErrLen,bool ErrCs,bool ErrEnd)
        {
            int m_ASDUAddTemp = theDevice.m_ASDUAddr;
            int m_Cot = 6;
            if (ErrAddr == true)
            {
                m_ASDUAddTemp = theDevice.m_ASDUAddr + 1;
            }
            if (ErrCot == true)
            {
                m_Cot = 0;
            }
            int nPlace = 0;
            SendB[nPlace++] = 0x64; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, m_Cot);
           
            nPlace += SetAddress(SendB, nPlace, m_ASDUAddTemp);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x14;
            return ExpSend_Stardard_101Report(SendB, nPlace, ErrAddr, ErrCtrl, ErrCot, ErrLen, ErrCs, ErrEnd);
        }
        public int ExpSend_Stardard_101Report(byte[] SendB, int FrameLen, bool ErrAddr, bool ErrCtrl, bool ErrCot, bool ErrLen, bool ErrCs, bool ErrEnd)
        {
            byte TempCtrl = GetFCB();
            if (ErrCtrl == true)
            {
                TempCtrl = (byte)(TempCtrl + 1);
            }
            
            int nPlace = 0;
            nPlace = GetAddressLeng() + 5;
            Array.Copy(SendB, 0, SendB, nPlace, FrameLen);

            nPlace = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = TempCtrl;
            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            nPlace += FrameLen;
            SendB[1] = (byte)(nPlace - 4);
            SendB[2] = SendB[1];
            SendB[nPlace] = CGlobal.GetCS(SendB, 4, nPlace - 4);
            if (ErrCs == true)
            {
                SendB[nPlace] = Convert.ToByte(SendB[nPlace]+1);
            }
            nPlace++; 
            SendB[nPlace] = 0x16;
            if (ErrEnd == true)
            {
                SendB[nPlace] = 0; ;
            }
            nPlace++;
            return nPlace;
        }

        private int ExpSend_Sync_Time(byte[] SendB, bool ErrCot, bool ErrLen,bool ErrYear,bool ErrMon,bool ErrDay,bool ErrHour,bool ErrMin,bool ErrSec)
        {
            int m_Cot = 6;
            if (ErrCot == true)
            {
                m_Cot = 0;
            }

            int nPlace = 0;
            SendB[nPlace++] = 103; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, m_Cot);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            //放时间
            DateTime dt = DateTime.Now;
            int t = dt.Millisecond;
            t = t + dt.Second * 1000;
            int pWeek = 0;
            if (dt.DayOfWeek == 0)
                pWeek = 7;
            else
                pWeek = Convert.ToByte(dt.DayOfWeek);
            pWeek = pWeek << 5;

            SendB[nPlace] = Convert.ToByte(t % 256);
            nPlace++;
            SendB[nPlace] = Convert.ToByte(t / 256);
            if (ErrSec == true)
            {
                SendB[nPlace] = (byte)(SendB[nPlace] + 1);
            }
            nPlace++;
            SendB[nPlace] = Convert.ToByte(dt.Minute);
            if (ErrMin == true)
            {
                SendB[nPlace] =70;
            }
            nPlace++;
            SendB[nPlace] = Convert.ToByte(dt.Hour);
            if (ErrHour == true)
            {
                SendB[nPlace] = 26;
            }
            nPlace++;
            SendB[nPlace] = Convert.ToByte(dt.Day + pWeek);
            if (ErrDay == true)
            {
                SendB[nPlace] = 33;
            }
            nPlace++;
            SendB[nPlace] = Convert.ToByte(dt.Month);
            if (ErrMon == true)
            {
                SendB[nPlace] = 15;
            }
            nPlace++;
            
            SendB[nPlace] = Convert.ToByte((dt.Year - 2000) % 100);
            if (ErrYear == true)
            {
                SendB[nPlace] =255;
            }
            nPlace++;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int ExpSend_YK_Cmd(byte[] SendB, bool ErrCot, bool ErrLen)
        {
            int m_Cot = 6;
           


            int nPlace = 0;
            Byte pSelect = 0;
            if (pYkMsg == null)
            {
                MessageBox.Show("遥控数据丢失!");
                return 0;
            }
            if (theDevice == null)
            {
                MessageBox.Show("装置对象丢失!");
                return 0;
            }
            if (SendB == null)
            {
                MessageBox.Show("数组对象丢失!");
                return 0;
            }
            SendB[nPlace++] = (Byte)(45 + pYkMsg.m_YkType);
            SendB[nPlace++] = 0x01;

            if (pYkMsg.m_YkStatus == 2)
            {
                if (ErrCot == true)
                {
                    m_Cot = 0;
                }
                else
                {
                    m_Cot = 8;
                }
                nPlace += SetCot(SendB, nPlace, m_Cot);
            }
            else
            {
                if (ErrCot == true)
                {
                    m_Cot = 0;
                }
                else
                {
                    m_Cot = 6;
                }
                nPlace += SetCot(SendB, nPlace, m_Cot);
            }

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            //int tL = pYkMsg.m_YkNo + m_YkSrartAddress - 1;
            int tL = pYkMsg.m_YkAddress;
            SendB[nPlace++] = (Byte)(tL % 256);
            SendB[nPlace++] = (Byte)(tL / 256);

            if (pYkMsg.m_YkStatus == 0)
                pSelect = 0x80;
            else
                pSelect = 0;
            SendB[nPlace++] = (Byte)(pSelect + pYkMsg.m_YkType + pYkMsg.m_YkValue);

            return ExpSend_Stardard_101Report(SendB, nPlace, false, false, ErrCot, ErrLen, false, false);
        }
        private int GetTaskBuffer_TestCase101(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;          
            pYkMsg = new CYkOption();          
            switch (nTaskNo)
            {
                case 1000://发送连接报文               
                    InitPara();
                    nSendLeng = Send_Short_Report(0x49, pSendBuffer);
                    break;                
                case 1001:
                    nSendLeng = Send_Short_Report(0xFF, pSendBuffer);
                    break;
                case 1002:
                    nSendLeng = ExpSend_Short_Report(0x49, pSendBuffer, true, false, false);
                    break;
                case 1003:
                    nSendLeng = ExpSend_Short_Report(0x49, pSendBuffer, false, true, false);
                    break;
                case 1004:
                    nSendLeng = ExpSend_Short_Report(0x49, pSendBuffer, false, false, true);
                    break;
                case 2000:
                    nSendLeng = Send_Short_Report(0x40, pSendBuffer);
                    break;
                case 2001:
                    nSendLeng = Send_Short_Report(0xFF, pSendBuffer);
                    break;
                case 3000:
                    nSendLeng = SendCallTotalData(pSendBuffer);
                    break;
                case 3001:
                    nSendLeng = ExpSendCallTotalData(pSendBuffer,true,false,false,false,false,false);  
                    break;
                case 3002:
                    nSendLeng = ExpSendCallTotalData(pSendBuffer, false, true, false, false, false, false);  
                    break;
                case 3003:
                    nSendLeng = ExpSendCallTotalData(pSendBuffer, false, false, true, false, false, false);  
                    break;
                case 3004:
                    nSendLeng = ExpSendCallTotalData(pSendBuffer, false, false, false, true, false, false);  
                    break;
                case 3005:
                    nSendLeng = ExpSendCallTotalData(pSendBuffer, false, false, false, false, true, false);
                    break;
                case 3006:
                    nSendLeng = ExpSendCallTotalData(pSendBuffer, false, false, false, false, false, true);
                    break;
                case 4000:
                    nSendLeng = Send_Sync_Time(pSendBuffer);
                    break;
                case 4001:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, true, false, false, false, false, false, false, false);
                    break;
                case 4002:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, true, false, false, false, false, false, false);
                    break;
                case 4003:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, false, true, false, false, false, false, false);
                    break;
                case 4004:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, false, false, true, false, false, false, false);
                    break;
                case 4005:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, false, false, false, true, false, false, false);
                    break;
                case 4006:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, false, false, false, false, true, false, false);
                    break;
                case 4007:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, false, false, false, false, false, true, false);
                    break;
                case 4008:
                    nSendLeng = ExpSend_Sync_Time(pSendBuffer, false, false, false, false, false, false, false, true);
                    break;
                case 4009:
                    nSendLeng = Send_CallSync_Time(pSendBuffer);
                    break;
                case 5000:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = ExpSend_YK_Cmd(pSendBuffer,true,false);
                    break;
                case 5001:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = ExpSend_YK_Cmd(pSendBuffer,false,true);
                    break;
                case 5002:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkStatus = 0;
                    pYkMsg.m_YkType = 0;                     
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5003:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5004:
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 2;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5005:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5006:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5007:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 2;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5008:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5009:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5010:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 2;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5011:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5012:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 5013:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkStatus = 2;
                    pYkMsg.m_YkType = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;

                case 5014:
                    pYkMsg.m_YkAddress = 100000;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);                  
                    break;
                case 5015:
                    pYkMsg.m_YkAddress = 100000;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);                  
                    break;
                case 5016:
                    pYkMsg.m_YkAddress = 100000;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 0;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);                  
                    break;
                case 5017:
                    pYkMsg.m_YkAddress = 100000;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 1;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);                   
                    break;
                case 5018:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 1;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);               
                    break;
                case 5019:
                    pYkMsg.m_YkAddress = 1;
                    pYkMsg.m_YkValue = 0;
                    pYkMsg.m_YkType = 0;
                    pYkMsg.m_YkStatus = 1;
                    nSendLeng = Send_YK_Cmd(pSendBuffer);                   
                    break;

                case 6000:
                    break;

                 
            }
            return nSendLeng;




        }
        private int GetTaskBuffer_Encode_testCase(Byte[] pSendBuffer, int nTaskNo, int nSendLeng)
        {
            byte[] appdata = new byte[nSendLeng + 1];
            appdata[0] = Convert.ToByte(nSendLeng);
            for (int i = 1; i < appdata.Length; i++)
            {
                appdata[i] = pSendBuffer[i - 1];
            }
            switch (nTaskNo)
            {
                case 1000: //发送连接报文
                case 1001:
                case 1002:
                case 1003:
                case 1004:
                case 2000: 
                case 2001:
                    byte[] Infosave = new byte[2] { 0, 0 };
                    nSendLeng = MakeMingWenFrame(0x01, 0x00, appdata, nSendLeng + 1, Infosave, 2, pSendBuffer);
                    break;
                case 3000://发送总召报文
                     case 3001:
                     case 3002:
                     case 3003:
                     case 3004:
                     case 3005:
                     case 3006:

                case 4000://发送对时报文
                case 4001:
                case 4002:
                case 4003:
                case 4004:
                case 4005:
                case 4006:
                case 4007:
                case 4008:
                case 4009:
                    nSendLeng = MakeFrame_AppType_00(nSendLeng, pSendBuffer, appdata);
                    break;
                case 5000://遥控
                case 5001:
                case 5002:
                case 5003:
                case 5004:
                case 5005:
                case 5006:
                case 5007:
                case 5008:
                case 5009:
                case 5010:
                case 5011:
                case 5012:
                case 5013:
                case 5014:
                case 5015:
                case 5016:
                case 5017:
                case 5018:
                case 5019:
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
            }
            return nSendLeng;
        }
    }
}
