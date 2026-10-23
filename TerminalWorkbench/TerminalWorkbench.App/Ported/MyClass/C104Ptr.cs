using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerminalWorkbench.Presentation;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
namespace DtuTestManager
{
    public class UseMessage
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
        private const int WM_USER_Encode = 0x0500;
        public const int MY_MSG_1_Encode = WM_USER_Encode + 1;
        public const int MY_MSG_2_Encode = WM_USER_Encode + 2;
        public const int MY_MSG_3_Encode = WM_USER_Encode + 3;
        public const int MY_MSG_4_Encode = WM_USER_Encode + 4;
        public const int MY_MSG_5_Encode = WM_USER_Encode + 5;
        public const int MY_MSG_6_Encode = WM_USER_Encode + 6;
        public const int MY_MSG_7_Encode = WM_USER_Encode + 7;
        public const int MY_MSG_8_Encode = WM_USER_Encode + 8;
        public const int MY_MSG_9_Encode = WM_USER_Encode + 9;

    }
    public struct Ptr104Address
    {
        public int m_AsduAddress;
        public int m_YxStartAddress;
        public int m_YcStartAddress;
        public int m_YkSrartAddress;
        public int m_PowerStartAddress;
    };
    public class C104Ptr : CPtrInterface
    {
        public ushort usSendNo = 0;
        public ushort usRecvNo = 0;

        private int m_SCount = 0;
        private DateTime m_STime = DateTime.Now;

        public C104Ptr()
        {
            InitPara();
        }
        private int Send_U_Report(byte iType, byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = 0x04;
            SendB[nPlace++] = iType;
            SendB[nPlace++] = 0x00;
            SendB[nPlace++] = 0x00;
            SendB[nPlace++] = 0x00;
            return nPlace;
        }
        private int Send_S_Report(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = 0x04;
            SendB[nPlace++] = 0x01;
            SendB[nPlace++] = 0x00;
            SendB[nPlace++] = Convert.ToByte(usRecvNo * 2 % 256);
            SendB[nPlace++] = Convert.ToByte(usRecvNo * 2 / 256);
            return nPlace;
        }
        private void Send_I_Report(byte[] SendB, int nLen)
        {
            if (nLen > 255)
            {
                MessageBox.Show("发送长度超过255，请重新选择");
                nLen = 255;
            }
            int nPlace = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = Convert.ToByte(nLen);
            SendB[nPlace++] = Convert.ToByte(usSendNo * 2 % 256);
            SendB[nPlace++] = Convert.ToByte(usSendNo * 2 / 256);
            usSendNo++;
            SendB[nPlace++] = Convert.ToByte(usRecvNo * 2 % 256);
            SendB[nPlace++] = Convert.ToByte(usRecvNo * 2 / 256);
        }
        private int SendCallPowerData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x65; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x5;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }

        private int SendGetFrzData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x65; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x45;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int SendCallTotalData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x64; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x14;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_CallSync_Time(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 103; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x05;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            //放时间
            DateTime dt = DateTime.Now;
            int t = dt.Millisecond;
            int pWeek = 0;
            if (dt.DayOfWeek == 0)
                pWeek = 7;
            else
                pWeek = Convert.ToByte(dt.DayOfWeek);
            pWeek = pWeek << 5;

            t = t + dt.Second * 1000;
            /*    SendB[nPlace++] = Convert.ToByte(t % 256);
                SendB[nPlace++] = Convert.ToByte(t / 256);
                SendB[nPlace++] = Convert.ToByte(dt.Minute);
                SendB[nPlace++] = Convert.ToByte(dt.Hour);
                SendB[nPlace++] = Convert.ToByte(dt.Day + pWeek);
                SendB[nPlace++] = Convert.ToByte(dt.Month);
                SendB[nPlace++] = Convert.ToByte((dt.Year - 2000) % 100);
                */
            nPlace += 7;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Sync_Time(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 103; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;


            //放时间
            DateTime dt = DateTime.Now;
            int t = dt.Millisecond;
            int pWeek = 0;
            if (dt.DayOfWeek == 0)
                pWeek = 7;
            else
                pWeek = Convert.ToByte(dt.DayOfWeek);
            pWeek = pWeek << 5;

            t = t + dt.Second * 1000;
            SendB[nPlace++] = Convert.ToByte(t % 256);
            SendB[nPlace++] = Convert.ToByte(t / 256);
            SendB[nPlace++] = Convert.ToByte(dt.Minute);
            SendB[nPlace++] = Convert.ToByte(dt.Hour);
            SendB[nPlace++] = Convert.ToByte(dt.Day + pWeek);
            SendB[nPlace++] = Convert.ToByte(dt.Month);
            SendB[nPlace++] = Convert.ToByte((dt.Year - 2000) % 100);
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_YK_Cmd(Byte[] SendB)
        {
            int nPlace = 6;
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
                SendB[nPlace++] = 0x08; //取消
            }
            else
            {
                SendB[nPlace++] = 0x06;//选择或执行
            }
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            //int tL = pYkMsg.m_YkNo + m_YkSrartAddress - 1;
            int tL = pYkMsg.m_YkAddress;
            SendB[nPlace++] = (Byte)(tL % 256);
            SendB[nPlace++] = (Byte)(tL / 256);
            SendB[nPlace++] = 0;

            if (pYkMsg.m_YkStatus == 0)
                pSelect = 0x80;
            else
                pSelect = 0;
            SendB[nPlace++] = (Byte)(pSelect + pYkMsg.m_YkType + pYkMsg.m_YkValue);

            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_ReadAll_Para(byte[] SendB)
        {

            int nPlace = 6;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCA; //类型202
            }
            else
            {
                SendB[nPlace++] = 0x6F; //类型
            }

            SendB[nPlace++] = 0;


            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Read_Para(byte[] SendB)
        {
            if (m_NowParaTask == null)
                return 0;

            int nPlace = 6;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCA; //类型202
            }
            else
            {
                SendB[nPlace++] = 0x6F; //类型
            }

            SendB[nPlace++] = Convert.ToByte(m_NowParaTask.m_ParaCount);

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);

            for (int i = 0; i < m_NowParaTask.m_ParaCount; i++)
            {
                int nAddress = theDevice.m_SelectParaList[i + m_NowParaTask.m_StartPalce].m_ParaAddress;
                // if (CheckParaStatus(Is_PriPara, nAddress)) break;
                SendB[nPlace++] = Convert.ToByte(nAddress % 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 65536);
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Preset_Para(byte[] SendB)
        {
            if (m_NowParaTask == null)
                return 0;
            int nPlace = 6;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCB; //类型
            }
            else
            {
                SendB[nPlace++] = 0x70; //类型
            }

            SendB[nPlace++] = Convert.ToByte(m_NowParaTask.m_ParaCount);

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);


            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0x80;//参数特征标识,无后续
            else
                SendB[nPlace++] = 0x81;//参数特征标识，有后续
            byte[] data = new byte[10];
            for (int i = 0; i < m_NowParaTask.m_ParaCount; i++)
            {
                CParaCtrl pPara = theDevice.m_SelectParaList[i + m_NowParaTask.m_StartPalce];
                int nAddress = pPara.m_ParaAddress;
                // if (CheckParaStatus(Is_PriPara, nAddress)) break;
                SendB[nPlace++] = Convert.ToByte(nAddress % 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 65536);

                SendB[nPlace++] = Convert.ToByte(pPara.m_ParaType);//tag类型
                SendB[nPlace++] = Convert.ToByte(pPara.m_ParaLeng);//数据长度

                Byte[] pValueByte = null;
                int nLength = pPara.GetValueBuff(ref pValueByte);
                for (int j = 0; j < nLength; j++)
                    SendB[nPlace++] = pValueByte[j];
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Write_Para(byte[] SendB)
        {
            int nPlace = 6;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCB; //类型
            }
            else
            {
                SendB[nPlace++] = 0x70; //类型
            }

            SendB[nPlace++] = 0x0;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);


            SendB[nPlace++] = 0;//特征标识
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }

        private int Send_CancelWrite_Para(byte[] SendB)
        {
            int nPlace = 6;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCB; //类型
            }
            else
            {
                SendB[nPlace++] = 0x70; //类型
            }

            SendB[nPlace++] = 0x0;

            SendB[nPlace++] = 0x08;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);


            SendB[nPlace++] = 0;//特征标识
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_Dir(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0xD2; //类型
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = 0x05;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x2;//附加数据包类型
            SendB[nPlace++] = 0x1;//读目录

            SendB[nPlace++] = Convert.ToByte(theDevice.m_SelectFloder.m_FloaderType);//目录标识
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_SelectFloder.m_FloderKey.Length);//目录名长度

            //目录名
            for (int t = 0; t < theDevice.m_SelectFloder.m_FloderKey.Length; t++)
            {
                SendB[nPlace++] = Convert.ToByte(theDevice.m_SelectFloder.m_FloderKey[t]);
            }

            SendB[nPlace++] = theDevice.m_CallFileTime.m_CallType;  //召唤标志
            for (int i = 0; i < 7; i++)
            {
                SendB[nPlace++] = theDevice.m_CallFileTime.m_StartTime[i];
            }
            for (int i = 0; i < 7; i++)
            {
                SendB[nPlace++] = theDevice.m_CallFileTime.m_EndTime[i];
            }

            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_File_Active(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0xD2; //类型
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x2;//附加数据包类型
            SendB[nPlace++] = 0x3;//读文件激活

            theDevice.m_NowReadFile = theDevice.m_SelectFloder.m_TempFileList[theDevice.m_SelectFloder.m_NowReadPalce];

            SendB[nPlace++] = Convert.ToByte(theDevice.m_NowReadFile.m_FileTypeName.Length);//文件名长度

            for (int t = 0; t < theDevice.m_NowReadFile.m_FileTypeName.Length; t++)
            {
                SendB[nPlace++] = Convert.ToByte(theDevice.m_NowReadFile.m_FileTypeName[t]);
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_FileData_Conform(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0xD2; //类型
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = 0x05;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x2;//附加数据包类型
            SendB[nPlace++] = 0x6;

            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FileRII);
            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FileRII >> 8);
            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FileRII >> 16);
            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FileRII >> 24);

            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FilePointNo);
            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FilePointNo >> 8);
            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FilePointNo >> 16);
            SendB[nPlace++] = (byte)(theDevice.m_NowReadFile.m_FilePointNo >> 24);

            SendB[nPlace++] = 0;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_DC_CheckData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x71; //类型
            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.m_DC_Checkdata.Count);

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0xFF; //线路编号
            for (int i = 0; i < CGlobal.m_Device.m_DC_Checkdata.Count; i++)
            {
                SendB[nPlace++] = CGlobal.m_Device.m_DC_Checkdata[i].Is_Check;   //校验标识
                byte[] temp = BitConverter.GetBytes(CGlobal.m_Device.m_DC_Checkdata[i].m_Default);//基准值
                SendB[nPlace++] = temp[0];
                SendB[nPlace++] = temp[1];
                SendB[nPlace++] = temp[2];
                SendB[nPlace++] = temp[3];
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_CheckData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x71; //类型
            SendB[nPlace++] = 0x12;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.SelLineNo); //线路编号
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                SendB[nPlace++] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check;   //校验标识
                byte[] temp = BitConverter.GetBytes(CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Default);//基准值
                SendB[nPlace++] = temp[0];
                SendB[nPlace++] = temp[1];
                SendB[nPlace++] = temp[2];
                SendB[nPlace++] = temp[3];
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_RatioData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x72; //类型
            SendB[nPlace++] = 0x12;

            SendB[nPlace++] = 0x05;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.SelLineNo); //线路编号
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                SendB[nPlace++] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check;  //下装标识
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
                SendB[nPlace++] = 0;
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Down_RatioData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x72; //类型
            SendB[nPlace++] = 0x12;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.SelLineNo); //线路编号
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                SendB[nPlace++] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Down;   //下装标识
                byte[] temp = BitConverter.GetBytes(CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Ratio);//系数
                SendB[nPlace++] = temp[0];
                SendB[nPlace++] = temp[1];
                temp = BitConverter.GetBytes(CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_A);
                SendB[nPlace++] = temp[0];
                SendB[nPlace++] = temp[1];
                SendB[nPlace++] = temp[2];
                SendB[nPlace++] = temp[3];
                temp = BitConverter.GetBytes(CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_B);
                SendB[nPlace++] = temp[0];
                SendB[nPlace++] = temp[1];
                SendB[nPlace++] = temp[2];
                SendB[nPlace++] = temp[3];
            }
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Call_Phase(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 0x73; //类型
            SendB[nPlace++] = 0x06;

            SendB[nPlace++] = 0x05;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.SelLineNo); //线路编号
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_WriteFileActive(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 210; //类型
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 2; //文件传输

            SendB[nPlace++] = 7;//操作标识
            SendB[nPlace++] = Convert.ToByte(theDevice.m_TransFile.m_FileNameLength);
            for (int i = 0; i < theDevice.m_TransFile.m_FileNameLength; i++)
            {
                SendB[nPlace++] = Convert.ToByte(theDevice.m_TransFile.m_FileName[i]);
            }
            byte[] temp = BitConverter.GetBytes(theDevice.m_TransFile.m_FileID);          //文件ID
            SendB[nPlace++] = temp[0];
            SendB[nPlace++] = temp[1];
            SendB[nPlace++] = temp[2];
            SendB[nPlace++] = temp[3];
            temp = BitConverter.GetBytes(theDevice.m_TransFile.m_FileSize);
            SendB[nPlace++] = temp[0];
            SendB[nPlace++] = temp[1];
            SendB[nPlace++] = temp[2];
            SendB[nPlace++] = temp[3];
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_WriteFileData(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 210; //类型
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x05;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 2; //文件传输

            SendB[nPlace++] = 9;//操作标识
            byte[] temp = BitConverter.GetBytes(theDevice.m_TransFile.m_FileID);   //文件ID
            SendB[nPlace++] = temp[0];
            SendB[nPlace++] = temp[1];
            SendB[nPlace++] = temp[2];
            SendB[nPlace++] = temp[3];
            //数据段号
            temp = System.BitConverter.GetBytes(theDevice.m_TransFile.m_data[theDevice.m_TransFile.CurrentIndex].Offset);
            SendB[nPlace++] = temp[0];
            SendB[nPlace++] = temp[1];
            SendB[nPlace++] = temp[2];
            SendB[nPlace++] = temp[3];

            if (theDevice.m_TransFile.CurrentIndex < (theDevice.m_TransFile.SumFrames - 1))
            {
                SendB[nPlace++] = 1;//有后续
            }
            else
            {
                SendB[nPlace++] = 0;//无后续
            }
            for (int i = 0; i < theDevice.m_TransFile.m_data[theDevice.m_TransFile.CurrentIndex].DataLength; i++)    //文件内容
            {
                SendB[nPlace++] = theDevice.m_TransFile.m_data[theDevice.m_TransFile.CurrentIndex].DataBuff[i];
            }
            SendB[nPlace++] = CGlobal.GetCS(theDevice.m_TransFile.m_data[theDevice.m_TransFile.CurrentIndex].DataBuff, 0,
                                                    theDevice.m_TransFile.m_data[theDevice.m_TransFile.CurrentIndex].DataLength);
            Send_I_Report(SendB, nPlace - 2);
            theDevice.m_TransFile.CurrentIndex++;
            return nPlace;
        }
        private bool CheckRecBuff(ref byte[] pRecBuff, ref int nRecLeng)
        {
            int i;
            for (i = 0; i <= (nRecLeng - 6); i++)
            {
                if (pRecBuff[i] == 0x68)
                {
                    if (i > 0)
                    {
                        nRecLeng = nRecLeng - i;
                        for (int j = 0; j < nRecLeng; j++)
                        {
                            pRecBuff[j] = pRecBuff[i++];
                        }
                        break;
                    }
                    else
                        break;
                }
            }
            if (nRecLeng < 6)
                return false;               ///接收报文长度不会小于6;
            if (pRecBuff[0] != 0x68)
                return false;     ///报文头不正确;
            UInt32 nRealLeng = Convert.ToUInt32(pRecBuff[1] + 2);
            if (nRealLeng > nRecLeng)
                return false;       ///接收报文小于实际有效报文长度
            return true;
        }
        private void ASDU_YX(byte[] TB, bool bSoeFlag)
        {
            bool bIsSpont = false;
            bool IsContinue = false;
            bool bIsDouble = false;
            int DataCount;
            int StartAddr, Offset;
            int i, nValue;
            string strTimeMsg;

            if (TB[8] == 3)
                bIsSpont = true;        //突发上送
            if ((TB[7] & 0x80) == 0x80)
                IsContinue = true;  //连续状态
            DataCount = TB[7] & 0x7F;
            if (TB[6] == 3 || TB[6] == 0x1F)
                bIsDouble = true;           //双点

            if (bSoeFlag)
            {
                if (IsContinue == true)
                {
                    StartAddr = TB[12] + TB[13] * 256;
                    Offset = 15;
                    for (i = 0; i < DataCount; i++)
                    {
                        //  NewAddr = StartAddr - m_YxStartAddress + i;
                        nValue = Convert.ToByte(TB[Offset] & 0x03);
                        Offset++;
                        Byte[] pSoeTime = new Byte[7];
                        Array.Copy(TB, Offset, pSoeTime, 0, 7);
                        strTimeMsg = GetSOETimeString(pSoeTime);
                        Offset = Offset + 7;
                        SetYxNowStatus_SOE(StartAddr + i, nValue, strTimeMsg, bIsDouble, bIsSpont, bSoeFlag);
                    }
                }
                else
                {
                    Offset = 12;
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        //   NewAddr = StartAddr - m_YxStartAddress;
                        Offset = Offset + 3;
                        nValue = Convert.ToByte(TB[Offset] & 0x03);
                        Offset++;
                        Byte[] pSoeTime = new Byte[7];
                        Array.Copy(TB, Offset, pSoeTime, 0, 7);
                        strTimeMsg = GetSOETimeString(pSoeTime);
                        Offset = Offset + 7;
                        SetYxNowStatus_SOE(StartAddr, nValue, strTimeMsg, bIsDouble, bIsSpont, bSoeFlag);
                    }
                }
                CGlobal.PostMessage(CGlobal.m_Device.m_MainFormHandle, UseMessage.MY_MSG_3, 0, 0);
            }
            else
            {
                if (IsContinue == true)
                {
                    StartAddr = TB[12] + TB[13] * 256;
                    Offset = 15;
                    for (i = 0; i < DataCount; i++)
                    {
                        // NewAddr = StartAddr - m_YxStartAddress + i;
                        strTimeMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        SetYxNowStatus(StartAddr + i, Convert.ToByte(TB[Offset] & 0x03));
                        Offset++;
                    }
                }
                else
                {
                    Offset = 12;
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        //NewAddr = StartAddr - m_YxStartAddress;
                        Offset = Offset + 3;
                        strTimeMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        SetYxNowStatus(StartAddr, Convert.ToByte(TB[Offset] & 0x03));
                        Offset++;
                    }
                }
            }
            theDevice.m_bYxFresh = true;
        }
        private void DoWithYC(byte[] TB, int flag)
        {
            try
            {
                int StartAddr;
                float fValue = 0;
                int i;
                byte[] fValueBuff = new byte[4];
                bool IsContinue = false;
                int Offset;

                if ((TB[7] & 0x80) == 0x80)
                    IsContinue = true;
                int DataCount = TB[7] & 0x7F;
                int nDataType = TB[6];
                int nValue = 0;
                if (IsContinue == true)
                {
                    StartAddr = TB[12] + TB[13] * 256;
                    //NewAddr = StartAddr - m_YcStartAddress;
                    Offset = 15;
                    switch (nDataType)
                    {
                        case 0x09:      //归一化
                        case 0x0B:      //标度化
                            for (i = 0; i < DataCount; i++)
                            {
                                nValue = TB[Offset] + TB[Offset + 1] * 256;
                                Offset += 2;
                                SetYcValue(StartAddr + i, (float)nValue, TB[Offset]);
                                Offset += 1;
                            }
                            break;
                        case 0x0D:      //浮点
                            for (i = 0; i < DataCount; i++)
                            {
                                Array.Copy(TB, Offset, fValueBuff, 0, 4);
                                fValue = BitConverter.ToSingle(fValueBuff, 0);
                                Offset += 4;
                                SetYcValue(StartAddr + i, fValue, TB[Offset]);
                                Offset += 1;
                            }
                            break;
                    }
                }
                else
                {
                    Offset = 12;
                    switch (nDataType)
                    {
                        case 0x09:      //归一化
                        case 0x0B:      //表度化
                            for (i = 0; i < DataCount; i++)
                            {
                                StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                                ///NewAddr = StartAddr - m_YcStartAddress;
                                Offset += 3;
                                nValue = TB[Offset] + TB[Offset + 1] * 256;
                                Offset += 2;
                                SetYcValue(StartAddr, (float)nValue, TB[Offset]);
                                Offset += 1;
                            }
                            break;
                        case 0x0D:
                            for (i = 0; i < DataCount; i++)
                            {
                                StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                                //NewAddr = StartAddr - m_YcStartAddress;
                                Offset += 3;
                                Array.Copy(TB, Offset, fValueBuff, 0, 4);
                                fValue = BitConverter.ToSingle(fValueBuff, 0);
                                Offset += 4;
                                SetYcValue(StartAddr, fValue, TB[Offset]);
                                Offset += 1;
                            }
                            break;
                    }
                }
                theDevice.m_bYcFresh = true;
            }
            catch
            {
            }
        }
        private void DoWithYM(byte[] TB, int tLen, bool bSoeFlag)
        {
            int i = 0;
            int NewAddr;
            bool IsContinue = false;
            int Offset;
            if ((TB[7] & 0x80) == 0x80)
                IsContinue = true;
            int DataCount = TB[7] & 0x7F;
            int StartAddr;
            byte[] fValueBuff = new byte[4];
            float fValue;
            string strTimeMsg;
            Byte bEnable;
            if (bSoeFlag)
            {
                if (IsContinue == true)
                {
                    StartAddr = TB[12] + TB[13] * 256;
                    Offset = 15;
                    for (i = 0; i < DataCount; i++)
                    {
                        NewAddr = StartAddr - m_PowerStartAddress;
                        Array.Copy(TB, Offset, fValueBuff, 0, 4);
                        fValue = BitConverter.ToSingle(fValueBuff, 0);
                        Offset += 4;
                        bEnable = TB[Offset];
                        Offset += 1;
                        Byte[] pSoeTime = new Byte[7];
                        Array.Copy(TB, Offset, pSoeTime, 0, 7);
                        strTimeMsg = GetSOETimeString(pSoeTime);
                        Offset = Offset + 7;
                        SetPowerValue(NewAddr + i, fValue, bEnable, strTimeMsg);
                    }
                }
                else
                {
                    Offset = 12;
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        NewAddr = StartAddr - m_PowerStartAddress;
                        Offset = Offset + 3;
                        Array.Copy(TB, Offset, fValueBuff, 0, 4);
                        fValue = BitConverter.ToSingle(fValueBuff, 0);
                        Offset += 4;
                        bEnable = TB[Offset];
                        Offset += 1;
                        Byte[] pSoeTime = new Byte[7];
                        Array.Copy(TB, Offset, pSoeTime, 0, 7);
                        strTimeMsg = GetSOETimeString(pSoeTime);
                        Offset = Offset + 7;
                        SetPowerValue(NewAddr + i, fValue, bEnable, strTimeMsg);
                    }
                }
            }
            else
            {
                if (IsContinue == true)
                {
                    StartAddr = TB[12] + TB[13] * 256;
                    Offset = 15;
                    for (i = 0; i < DataCount; i++)
                    {
                        NewAddr = StartAddr - m_PowerStartAddress;
                        Array.Copy(TB, Offset, fValueBuff, 0, 4);
                        fValue = BitConverter.ToSingle(fValueBuff, 0);
                        Offset += 4;
                        bEnable = TB[Offset];
                        Offset += 1;
                        strTimeMsg = "";
                        SetPowerValue(NewAddr + i, fValue, bEnable, strTimeMsg);
                    }
                }
                else
                {
                    Offset = 12;
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        NewAddr = StartAddr - m_PowerStartAddress;
                        Offset = Offset + 3;
                        Array.Copy(TB, Offset, fValueBuff, 0, 4);
                        fValue = BitConverter.ToSingle(fValueBuff, 0);
                        Offset += 4;
                        bEnable = TB[Offset];
                        Offset += 1;
                        strTimeMsg = "";
                        SetPowerValue(NewAddr + i, fValue, bEnable, strTimeMsg);
                    }
                }
            }
            theDevice.m_bPwFresh = true;
        }


        public int BinarySearch(int low, int high, int key)
        {
            int mid = (low + high) / 2;
            if (low > high)
                return -1;
            else
            {
                if (CGlobal.m_Device.m_TotalParaDataList[mid].m_ParaAddress == key)
                    return mid;
                else if (CGlobal.m_Device.m_TotalParaDataList[mid].m_ParaAddress > key)
                    return BinarySearch(low, mid - 1, key);
                else
                    return BinarySearch(mid + 1, high, key);
            }
        }
        public string ChangeData(byte[] data, int m_ParaType)
        {

            string str = "";
            byte[] data1 = new byte[2];
            switch (m_ParaType)
            {
                case 32:
                    str = data[0].ToString();
                    break;
                case 33:
                    Array.Copy(data, 0, data1, 0, 2);
                    str = (BitConverter.ToInt16(data1, 0)).ToString();//有符号
                    break;
                case 43:
                    str = Convert.ToInt16(data[0]).ToString();
                    break;
                case 45:
                    Array.Copy(data, 0, data1, 0, 2);
                    str = (BitConverter.ToUInt16(data1, 0)).ToString();//有符号
                    break;
            }
            return str;
        }
        private void DoWithreadDZ(byte[] TB)
        {
            int L = TB[7] & 0x7F;
            if (L <= 0)
                return;
            int offset = 14;
            byte type = TB[offset];//参数特征标示
            offset++;
            string str = "";
            int Addr;
            byte tag, Len;
            for (int j = 0; j < L; j++)
            {
                Addr = TB[offset] + TB[offset + 1] * 256;//地址
                int nIndex = BinarySearch(0, CGlobal.m_Device.m_TotalParaDataList.Count, Addr);
                int m_Real_ParaType = CGlobal.m_Device.m_TotalParaDataList[nIndex].m_ParaType;
                offset += 3;
                tag = TB[offset];//tag类型
                offset++;
                Len = TB[offset];//数据长度
                offset++;
                if ((tag == 1) && (Len == 1))//bool
                {
                    if (TB[offset] == 1)
                        str = "1";
                    else
                        str = "0";
                }
                else if ((tag == 43) && (Len == 1))//小整形
                {
                    str = (Convert.ToInt16(TB[offset])).ToString();

                }
                else if ((tag == 32) && (Len == 1))//无符号小整形
                {
                    str = TB[offset].ToString();

                }
                else if ((tag == 33) && (Len == 2))//小整形
                {
                    byte[] data = new byte[2];
                    Array.Copy(TB, offset, data, 0, 2);
                    str = (BitConverter.ToInt16(data, 0)).ToString();//有符号

                }
                else if ((tag == 45) && (Len == 2))
                {
                    byte[] data = new byte[2];
                    Array.Copy(TB, offset, data, 0, 2);
                    str = (BitConverter.ToUInt16(data, 0)).ToString();//无符号

                }
                else if ((tag == 2) && (Len == 4))//整形
                {
                    byte[] data = new byte[4];
                    Array.Copy(TB, offset, data, 0, 4);
                    if (m_Real_ParaType == tag)
                    {
                        str = (BitConverter.ToInt32(data, 0)).ToString();
                    }
                    else
                    {
                        str = ChangeData(data, m_Real_ParaType);
                    }

                }
                else if ((tag == 35) && (Len == 4))//无符号整形
                {
                    byte[] data = new byte[4];
                    Array.Copy(TB, offset, data, 0, 4);
                    if (m_Real_ParaType == tag)
                    {
                        str = (BitConverter.ToUInt32(data, 0)).ToString();
                    }
                    else
                    {
                        str = ChangeData(data, m_Real_ParaType);
                    }

                }
                else if ((tag == 36) && (Len == 8))//长整形
                {
                    byte[] data = new byte[8];
                    Array.Copy(TB, offset, data, 0, 8);
                    str = (BitConverter.ToInt64(data, 0)).ToString();

                }
                else if ((tag == 37) && (Len == 8))//无符号长整形
                {
                    byte[] data = new byte[8];
                    Array.Copy(TB, offset, data, 0, 8);
                    str = (BitConverter.ToUInt64(data, 0)).ToString();

                }
                else if ((tag == 38) && (Len == 4))//单精度浮点
                {
                    byte[] data = new byte[4];
                    Array.Copy(TB, offset, data, 0, 4);
                    str = (BitConverter.ToSingle(data, 0)).ToString();

                }
                else if ((tag == 39) && (Len == 8))//双精度浮点
                {
                    byte[] data = new byte[8];
                    Array.Copy(TB, offset, data, 0, 8);
                    str = (BitConverter.ToSingle(data, 0)).ToString();

                }
                else if (tag == 4)
                {
                    byte[] data = new byte[Len];
                    Array.Copy(TB, offset, data, 0, Len);
                    str = CGlobal.BytesToString(data);
                }
                offset += Len;
                theDevice.SetParaValue(Addr, str);
            }
            if (TB[8] == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值异常", "提示");
                return;
            }
            else if (TB[8] == 7)
            {
                //终端激活确认
                //不处理
            }
            int bs0 = 0x01 & type;
            if (bs0 == 0)
                theDevice.m_bParaFresh = true;
        }

        private void DoWithwriteDZ(byte[] TB)
        {
            try
            {
                int offset = 8;//传送原因
                int YY = TB[offset];
                offset = 14;
                byte type = TB[offset];//参数类型标示
                int se = 0x80 & type;
                int CR = 0x40 & type;
                if ((se != 0) && (CR == 0) && (YY == 7))
                {
                    if (theDevice.m_ParaHandle >= 0)
                        CGlobal.SendMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_1, 0, 0);
                }
                else if ((se == 0) && (CR == 0) && (YY == 7))
                {
                    if (m_NowParaTask != null)
                        m_NowParaTask.m_AnswerFlag = true;
                    if (theDevice.m_ParaHandle >= 0)
                        CGlobal.SendMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_2, 0, 0);

                }
            }
            catch
            {
            }
        }
        private int DoWith_S_Recv(Byte[] pRecBuff)
        {
            return 21;
        }
        private int DoWith_U_Recv(int iByte)
        {
            try
            {
                switch (iByte)
                {
                    case 0x0B:   //收到0x0B
                        m_StartFlag = true;
                        m_HeartTime = DateTime.Now;
                        m_STime = DateTime.Now;
                        //              PostMessage(m_hWnd, MY_MSG_END,0,0);
                        break;
                    case 0x23:    //收到0x23
                        m_StartFlag = false;
                        break;
                    case 0x83:    //收到0x83
                        m_bHeartOnlie = false;
                        break;
                    case 0x43:
                        return 20;
                    default:
                        break;
                }
            }
            catch
            {
            }
            return 0;
        }
        private void DoWithYK(byte[] TB, int tLen, bool IsDD = false)
        {
            try
            {
                int YkResult = TB[8];
                if (YkResult == 7 || YkResult == 9 || YkResult == 10)
                    CGlobal.PostMessage(theDevice.m_YkFormHandle, UseMessage.MY_MSG_7, 0, 0);
                else
                    CGlobal.PostMessage(theDevice.m_YkFormHandle, UseMessage.MY_MSG_8, 0, 0);
                if ((CYKForm.YKStatus == true) && (YkResult == 7) && (CYKForm.YKTest = true))
                {
                    CGlobal.PostMessage(theDevice.m_YkFormHandle, UseMessage.MY_MSG_9, 0, 0);
                }
            }
            catch
            {
            }

        }
        private void DoWithreadDir(byte[] TB)
        {
            try
            {
                string nYear, nMonth, nDay, nHour, nMinute, nSecond, nMilliSecond;
                int offset = 16;  //操作标识
                offset++;//结果描述子
                if (TB[offset] == 0)//成功
                {

                    if (TB[offset + 6] == 0)//无目录
                    {
                        CGlobal.PostMessage(theDevice.m_FileFormHandle, UseMessage.MY_MSG_2, 0, 0);
                        return;
                    }
                }
                else
                {
                    if (TB[offset + 6] == 0)//无目录
                    {
                        CGlobal.PostMessage(theDevice.m_FileFormHandle, UseMessage.MY_MSG_3, 0, 0);
                    }
                    return;
                }

                int hxbz = TB[offset + 5];  //后续标志
                int f_num = TB[offset + 6];  //文件数量

                offset = offset + 7;
                for (int k = 0; k < f_num; k++)
                {
                    CFileCtrl pNewFile = new CFileCtrl();
                    int file_Len = TB[offset];  //文件1名称长度
                    offset++;
                    byte[] tempB = new byte[file_Len];
                    for (int i = 0; i < file_Len; i++)
                    {
                        tempB[i] = TB[offset++];
                    }
                    pNewFile.m_FileTypeName = CGlobal.BytesToString(tempB);  //文件名称
                    if (pNewFile.m_FileTypeName.LastIndexOf(".") == -1)
                    {
                        pNewFile.m_FileTypeName += ".msg";
                    }

                    int shuxing = TB[offset];//属性
                    offset = offset + 1;//大小
                    byte[] tb = new byte[4];
                    for (int i = 0; i < 4; i++)
                    {
                        tb[i] = TB[offset++];
                    }

                    pNewFile.m_FileSize = System.BitConverter.ToInt32(tb, 0);  //文件大小

                    int t = TB[offset] + TB[offset + 1] * 256;
                    nMilliSecond = Convert.ToString(t % 1000);
                    t = t - t % 1000;
                    t = t / 1000;
                    if (t >= 60)
                    {
                        t = 0;
                    }
                    nSecond = t.ToString("#00");
                    nMinute = TB[offset + 2].ToString("#00");
                    nHour = (TB[offset + 3]).ToString("#00");
                    nDay = (TB[offset + 4] & 0x1F).ToString("#00");
                    nMonth = (TB[offset + 5]).ToString("#00");
                    nYear = Convert.ToString(TB[offset + 6] + 2000);

                    pNewFile.m_RecordTime = nYear + "-" + nMonth + "-" + nDay + "  " + nHour + ":" + nMinute + ":" + nSecond;// +nMilliSecond; //文件时间
                    if (theDevice.m_SelectFloder != null)
                    {
                        pNewFile.m_FilePath = theDevice.m_SelectFloder.m_FloderPath + "\\" + pNewFile.m_FileTypeName;
                        theDevice.m_SelectFloder.AddNewWaitReadFile(pNewFile);
                    }
                    offset = offset + 7;
                }
                if (hxbz == 0)
                {
                    CGlobal.PostMessage(theDevice.m_FileFormHandle, UseMessage.MY_MSG_4, 0, 0);
                }
            }
            catch
            {
            }
        }
        private void Dowithreadfile(byte[] TB)
        {
            try
            {
                int offset = 16;  //操作标识
                offset++;//结果描述子
                if (TB[offset] == 0)
                {
                    //成功
                }
                else
                {
                    theDevice.m_NowReadFile.m_ReadResult = "读取失败";
                    theDevice.m_NowReadFile.m_EndFlag = true;
                    return;
                }
                offset++;
                int f_L = TB[offset];  //文件1名称长度
                offset++;
                byte[] tempB = new byte[f_L];
                for (int i = 0; i < f_L; i++)
                {
                    tempB[i] = TB[i + offset];
                }
                /*string strtemp = CGlobal.BytesToString(tempB);  //文件名称*/
                offset = offset + f_L;//文件标识
                byte[] data = new byte[4];
                for (int i = 0; i < 4; i++)
                    data[i] = TB[offset + i];
                theDevice.m_NowReadFile.m_FileRII = System.BitConverter.ToInt32(data, 0); //文件标识
                offset = offset + 4;
                for (int i = 0; i < 4; i++)
                    data[i] = TB[offset + i];
                theDevice.m_NowReadFile.m_FileSize = System.BitConverter.ToInt32(data, 0); //文件大小
            }
            catch
            {
            }
        }
        private int Dowithreadfiledata(byte[] TB, int nLen)
        {
            try
            {
                int i = 0;
                int offset = 16;  //操作标识
                offset = offset + 1;
                byte[] data = new byte[4];
                for (i = 0; i < 4; i++)
                    data[i] = TB[offset + i];//文件ID
                int fileBS = System.BitConverter.ToInt32(data, 0); //文件标识
                if (theDevice.m_NowReadFile != null)
                {
                    if (fileBS != theDevice.m_NowReadFile.m_FileRII)
                    {

                        theDevice.m_NowReadFile.m_ReadResult = "读取失败";
                        theDevice.m_NowReadFile.m_EndFlag = true;
                        return 0;
                    }
                }
                offset = offset + 4;

                for (i = 0; i < 4; i++)
                    data[i] = TB[offset + i];
                if (theDevice.m_NowReadFile != null)
                    theDevice.m_NowReadFile.m_FilePointNo = System.BitConverter.ToInt32(data, 0); //数据段号
                offset = offset + 4;
                byte HouXu = TB[offset];
                offset++;
                CFileBuff pNewBuff = new CFileBuff();
                pNewBuff.m_PacketNo = theDevice.m_NowReadFile.m_FilePointNo;
                pNewBuff.m_DataLeng = nLen - offset - 1;
                if (pNewBuff.m_DataLeng > 0)
                {
                    pNewBuff.m_DataBuff = new Byte[pNewBuff.m_DataLeng];
                    Array.Copy(TB, offset, pNewBuff.m_DataBuff, 0, pNewBuff.m_DataLeng);
                    if (TB[nLen - 1] != CGlobal.GetCS(pNewBuff.m_DataBuff, 0, pNewBuff.m_DataLeng))
                    {
                        return 0;
                    }
                    if (theDevice.m_NowReadFile != null)
                        theDevice.m_NowReadFile.AddReadBuff(pNewBuff);
                }
                if (HouXu == 0)//无后续
                {
                    if (theDevice.m_NowReadFile != null)
                    {
                        if (theDevice.m_NowReadFile.m_FileSize == theDevice.m_NowReadFile.m_ReadFileLeng)
                        {
                            theDevice.m_NowReadFile.m_ReadResult = "读取结束";
                            theDevice.m_NowReadFile.m_EndFlag = true;
                            theDevice.m_NowReadFile.m_ReadSuccess = true;
                        }
                        else
                        {
                            theDevice.m_NowReadFile.m_ReadResult = "结束,长度不一致";
                        }
                    }
                    return 24;
                }
            }
            catch
            {
            }
            return 0;
        }
        private int DowithwritefileConform(byte[] TB)
        {
            int offset = 17;  //结果描述子
            if (TB[offset] == 0)
            {
                //成功

                return 0;
            }
            else
            {
                //失败
                if (theDevice.m_SelectDownFileType == 0)
                {
                    CGlobal.PostMessage(theDevice.m_DownFileFormHandle, UseMessage.MY_MSG_1, 0, 0);
                }
                else
                {
                    CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_1, 0, 0);
                }
                return 0;
            }

        }
        private void DowithwritefileDataConform(byte[] TB)
        {
            int offset = 25;//结果描述字25
            if (TB[offset] == 0)
            {
                //成功
                if (theDevice.m_SelectDownFileType == 0)
                {
                    CGlobal.PostMessage(theDevice.m_DownFileFormHandle, UseMessage.MY_MSG_2, 0, 0);
                }
                else
                {
                    CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_2, 0, 0);
                }
                return;
            }
            else
            {
                //失败
                if (theDevice.m_SelectDownFileType == 0)
                {
                    CGlobal.PostMessage(theDevice.m_DownFileFormHandle, UseMessage.MY_MSG_3, 0, 0);
                }
                else
                {
                    CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_3, 0, 0);
                }
                return;
            }
        }
        private void DowWithProVersion(byte[] pRecBuff)
        {
            int nPlace = 12;
            float fValue;
            fValue = CGlobal.GetBuffToFloat(pRecBuff[nPlace], pRecBuff[nPlace + 1], 100);
            theDevice.m_Version.m_ADSoftVersion = "V" + fValue.ToString("F2");
            nPlace += 2;
            fValue = CGlobal.GetBuffToFloat(pRecBuff[nPlace], pRecBuff[nPlace + 1], 100);
            theDevice.m_Version.m_ConSoftVersion = "V" + fValue.ToString("F2");
            nPlace += 2;
            theDevice.m_Version.m_CRC = pRecBuff[nPlace] + pRecBuff[nPlace + 1] * 256;
            nPlace += 2;
            theDevice.m_Version.m_TotalRamSize = CGlobal.GetBuffToInt(pRecBuff[nPlace], pRecBuff[nPlace + 1], pRecBuff[nPlace + 2], pRecBuff[nPlace + 3]);
            nPlace += 4;
            theDevice.m_Version.m_SurplusRam = CGlobal.GetBuffToInt(pRecBuff[nPlace], pRecBuff[nPlace + 1], pRecBuff[nPlace + 2], pRecBuff[nPlace + 3]);
            nPlace += 4;
            theDevice.m_Version.m_RamRate = CGlobal.GetBuffToShort(pRecBuff[nPlace], pRecBuff[nPlace + 1]);
            nPlace += 2;
            theDevice.m_Version.m_TotalDiskSize = CGlobal.GetBuffToInt(pRecBuff[nPlace], pRecBuff[nPlace + 1], pRecBuff[nPlace + 2], pRecBuff[nPlace + 3]);
            nPlace += 4;
            theDevice.m_Version.m_SurplusDisk = CGlobal.GetBuffToInt(pRecBuff[nPlace], pRecBuff[nPlace + 1], pRecBuff[nPlace + 2], pRecBuff[nPlace + 3]);
            nPlace += 4;
            theDevice.m_Version.m_DiskRate = CGlobal.GetBuffToShort(pRecBuff[nPlace], pRecBuff[nPlace + 1]);
            nPlace += 2;
            theDevice.m_Version.m_Temperature = CGlobal.GetBuffToFloat(pRecBuff[nPlace], pRecBuff[nPlace + 1], 100);
            nPlace += 2;
        }
        private void DowithCheckData(byte[] pRecBuff)
        {
            int nPlace = 14;
            if (pRecBuff[12] == 0xff)
            {
                for (int i = 0; i < 2; i++)
                {
                    CGlobal.m_Device.m_DC_Checkdata[i].m_Real = BitConverter.ToSingle(pRecBuff, nPlace);//实测值
                    nPlace = nPlace + 5;
                }
            }
            else
            {
                for (int i = 0; i < 18; i++)
                {
                    CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Real = BitConverter.ToSingle(pRecBuff, nPlace);//实测值
                    nPlace = nPlace + 5;
                }
            }
            theDevice.m_bCheckFresh = true;

        }
        private void DowithPhaseData(byte[] pRecBuff)
        {
            int nPlace = 13;
            for (int i = 0; i < 6; i++)
            {
                CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo][i].m_Value = BitConverter.ToSingle(pRecBuff, nPlace);//实测值
                nPlace = nPlace + 4;
            }
            theDevice.m_bPhaseFresh = true;

        }
        private void DowithReadRatioData(byte[] pRecBuff)
        {
            int nPlace = 13;
            for (int i = 0; i < 18; i++)
            {
                nPlace++;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Ratio = BitConverter.ToUInt16(pRecBuff, nPlace);//实测值
                nPlace = nPlace + 2;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_A = BitConverter.ToSingle(pRecBuff, nPlace);//实测值
                nPlace = nPlace + 4;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_B = BitConverter.ToSingle(pRecBuff, nPlace);//实测值
                nPlace = nPlace + 4;

            }
            theDevice.m_bRatioFresh = true;
        }
        private void DowithDownRatioData(byte[] pRecBuff)
        {

        }
        private void DoWithFaultEvent(byte[] TB)
        {
            int i = 0;
            bool bIsDouble = false;
            string strInfo = "";
            byte[] fValueBuff = new byte[4];
            float fValue = 0;
            int StartAddr, NewAddr, nValue;
            string strTimeMsg;
            int offset = 12;
            int YxCnt = TB[offset];//遥信个数
            offset++;
            int YxType = TB[offset];
            offset++;
            if (YxType == 3 || YxType == 0x1F)
                bIsDouble = true;           //双点
            for (i = 0; i < YxCnt; i++)
            {
                StartAddr = TB[offset] + TB[offset + 1] * 256;
                NewAddr = StartAddr - m_YxStartAddress;
                offset = offset + 3;
                nValue = Convert.ToByte(TB[offset] & 0x03);
                offset++;
                Byte[] pTime = new Byte[7];
                Array.Copy(TB, offset, pTime, 0, 7);
                strTimeMsg = GetSOETimeString(pTime);
                offset = offset + 7;
                strInfo = strInfo + strTimeMsg + " " + theDevice.m_YxList[NewAddr].m_YxName + " " + GetYxStatus(nValue, bIsDouble);
            }
            strInfo = strInfo + "\n";
            int YcCnt = TB[offset];//遥测个数
            offset++;
            int nDataType = TB[offset];//遥测类型
            offset++;
            switch (nDataType)
            {
                case 0x09:      //归一化
                case 0x0B:      //表度化
                    for (i = 0; i < YcCnt; i++)
                    {
                        StartAddr = TB[offset] + TB[offset + 1] * 256;
                        NewAddr = StartAddr - m_YcStartAddress;
                        offset += 3;
                        nValue = TB[offset] + TB[offset + 1] * 256;
                        offset += 2;
                        //SetYcValue(NewAddr, (float)nValue, TB[offset]);暂时先不计算
                        strInfo = strInfo + theDevice.m_YcList[NewAddr].m_YcName + "=" + nValue.ToString() + "  ";
                    }
                    break;
                case 0x0D:
                    for (i = 0; i < YcCnt; i++)
                    {
                        StartAddr = TB[offset] + TB[offset + 1] * 256;
                        NewAddr = StartAddr - m_YcStartAddress;
                        offset += 3;
                        Array.Copy(TB, offset, fValueBuff, 0, 4);
                        fValue = BitConverter.ToSingle(fValueBuff, 0);
                        offset += 4;
                        // SetYcValue(NewAddr, fValue, TB[offset]);;暂时先不计算
                        strInfo = strInfo + theDevice.m_YcList[NewAddr].m_YcName + "=" + fValue.ToString() + "  ";
                    }
                    break;
            }
            theDevice.m_EventClass.AddRealMsg(strInfo);
            CGlobal.PostMessage(CGlobal.m_Device.m_MainFormHandle, UseMessage.MY_MSG_2, 0, 0);
        }


        public void Dowith_Read_AeroNo(byte[] TB)
        {

            if (TB[8] == 7)
            {
                m_ParaAreaNo = TB[15] + TB[16] * 256;
                CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_4, 0, 0);
            }
            else if (TB[8] == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值区号异常", "提示");
            }

        }
        public void Dowith_Switch_AeroNo(byte[] TB)
        {
            if (TB[8] == 47)
            {
                // MessageBox.Show("切换定值区号异常", "提示");
            }

        }
        private void Dowith_TerminalTime(byte[] pRecBuff)
        {
            if (pRecBuff[8] == 5)
            {
                string[] strWeek = { "一", "二", "三", "四", "五", "六", "日", "(未知)" };
                int nPlace = 15;
                int nYear = 2000 + pRecBuff[nPlace + 6];
                int nMonth = pRecBuff[nPlace + 5];
                int nDay = pRecBuff[nPlace + 4] & 0x1F;
                int nHour = pRecBuff[nPlace + 3];
                int nMinute = pRecBuff[nPlace + 2];

                int nMillSec = pRecBuff[nPlace + 1] * 256 + pRecBuff[nPlace];

                int nSecond = Convert.ToInt32(nMillSec / 1000);
                nMillSec = nMillSec % 1000;

                int nWeek = pRecBuff[nPlace + 4] >> 5;
                if (nWeek == 0)
                    nWeek = 7;

                string strTerTime = "终端当前时间：";
                strTerTime += nYear.ToString() + "-";
                strTerTime += nMonth.ToString() + "-";
                strTerTime += nDay.ToString() + " ";
                strTerTime += nHour.ToString() + ":";
                strTerTime += nMinute.ToString() + ":";
                strTerTime += nSecond.ToString() + ".";
                strTerTime += nMillSec.ToString();
                if (nWeek >= 1 && nWeek <= 7)
                {
                    nWeek--;
                    strTerTime += " 星期" + strWeek[nWeek];
                }
                else
                {
                    strTerTime += " 星期" + strWeek[7];
                }
                strTerTime += "\r\n";
                CGlobal.ShowTipsInfo(strTerTime);
            }
        }
        private int DoWith_I_Recv(byte[] pRecBuff, int nRecLeng)
        {
            try
            {
                usRecvNo++;
                m_SCount++;
                int YY;
                int ind;
                switch (pRecBuff[6])
                {
                    case 70:  //初始化结束
                        return 4;
                    case 0x64://总召应答
                        m_bCheckTimeOut = false;
                        if (pRecBuff[8] == 7)
                        {
                            //激活确认
                        }
                        else if (pRecBuff[8] == 10)
                        {
                            //激活终止  开始发起对时
                            // CGlobal.SendTaskOrder(5);  //发送对时
                        }
                        break;
                    case 0x67:
                        Dowith_TerminalTime(pRecBuff);
                        break;
                    case 1://单点遥信
                    case 3: //双点遥信
                        ASDU_YX(pRecBuff, false);
                        break;
                    case 0x1E: //单点SOE
                    case 0x1F: //双点SOE
                        ASDU_YX(pRecBuff, true);
                        break;
                    case 9:
                    case 11:
                    case 13: //遥测
                        DoWithYC(pRecBuff, ((pRecBuff[6] - 9) / 2));
                        break;
                    case 42://故障事件
                        DoWithFaultEvent(pRecBuff);
                        break;
                    case 45: //单点遥控返校
                        m_bCheckTimeOut = false;
                        DoWithYK(pRecBuff, nRecLeng, true);
                        break;
                    case 46: //双点遥控返校
                        m_bCheckTimeOut = false;
                        DoWithYK(pRecBuff, nRecLeng, false);
                        break;
                    case 206://摇脉
                        DoWithYM(pRecBuff, nRecLeng, false);
                        break;
                    case 207://摇脉
                        DoWithYM(pRecBuff, nRecLeng, true);
                        break;
                    case 0x6E:
                        DowWithProVersion(pRecBuff);
                        break;
                    case 0x6F:  //读定值参数
                        m_bCheckTimeOut = false;
                        if (m_NowParaTask != null)
                            m_NowParaTask.m_AnswerFlag = true;
                        DoWithreadDZ(pRecBuff);
                        break;
                    case 0x70:  //写定值 预置 固化
                        m_bCheckTimeOut = false;
                        //if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;
                        if (pRecBuff[8] == 9)
                        {
                            return 0;
                        }
                        if (pRecBuff[8] == 7)
                        {
                            DoWithwriteDZ(pRecBuff);
                        }
                        else if (pRecBuff[8] == 0x47)
                        {
                            //激活终止  预置失败
                            if (theDevice.m_ParaHandle >= 0)
                                CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_3, 0, 0);
                        }
                        break;
                    case 200://切换定值区
                        Dowith_Switch_AeroNo(pRecBuff);
                        break;
                    case 201://定值区号读取
                        Dowith_Read_AeroNo(pRecBuff);
                        break;
                    case 202:  //读定值参数 202
                        m_bCheckTimeOut = false;
                        if (m_NowParaTask != null)
                            m_NowParaTask.m_AnswerFlag = true;
                        DoWithreadDZ(pRecBuff);
                        break;
                    case 203:  //写定值 预置 固化
                        m_bCheckTimeOut = false;
                        if (pRecBuff[8] == 9)
                        {
                            return 0;
                        }
                        if (pRecBuff[8] == 7)
                        {
                            DoWithwriteDZ(pRecBuff);
                        }
                        else if (pRecBuff[8] == 0x47)
                        {
                            //激活终止
                            CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_3, 0, 0);
                        }
                        break;
                    case 0xD2:  //文件
                        m_bCheckTimeOut = false;
                        ind = pRecBuff[16];//操作标识
                        YY = pRecBuff[8];
                        if (m_NowDownFileTask != null)
                            m_NowDownFileTask.m_AnswerFlag = true;
                        if ((ind == 2) && (YY == 5))
                        {
                            CWaitForm.m_bCose = true;
                            DoWithreadDir(pRecBuff);//文件目录召唤确认
                        }
                        else if ((YY == 7) && (ind == 4))
                        {
                            Dowithreadfile(pRecBuff);   //读文件激活确认
                        }
                        else if ((ind == 5) && (YY == 5))
                        {
                            return Dowithreadfiledata(pRecBuff, nRecLeng);//读文件数据传输
                        }
                        else if ((ind == 8) && (YY == 7))
                        {

                            DowithwritefileConform(pRecBuff);//写文件激活确认
                        }
                        else if ((ind == 10) && (YY == 5))
                        {
                            DowithwritefileDataConform(pRecBuff);//写文件传输数据确认
                        }
                        break;
                    case 0xD3://远程软件升级
                        m_bCheckTimeOut = false;
                        ind = pRecBuff[15];
                        YY = pRecBuff[8];
                        if (m_NowDownFileTask != null)
                            m_NowDownFileTask.m_AnswerFlag = true;
                        if ((YY == 7) && ((ind & 0x80) != 0))  //升级启动确认
                        {

                        }
                        else if ((YY == 7) && ((ind & 0x80) == 0))  //升级结束确认
                        {
                            CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_4, 0, 0);
                        }
                        else if ((YY == 9) && ((ind & 0x80) == 0))  //升级结束确认
                        {
                            CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_5, 0, 0);
                        }
                        break;
                    case 0x71://校验
                        m_bCheckTimeOut = false;
                        if (m_NowDownFileTask != null)
                            m_NowDownFileTask.m_AnswerFlag = true;
                        YY = pRecBuff[8];
                        if (YY == 7)
                        {
                            DowithCheckData(pRecBuff);
                        }
                        break;
                    case 0x72://系数
                        m_bCheckTimeOut = false;
                        if (m_NowDownFileTask != null)
                            m_NowDownFileTask.m_AnswerFlag = true;
                        YY = pRecBuff[8];
                        if (YY == 0x05)
                        {
                            DowithReadRatioData(pRecBuff);
                        }
                        else if (YY == 0x07)
                        {
                            DowithDownRatioData(pRecBuff);
                        }
                        break;
                    case 0x73://绝对相角
                        if (m_NowDownFileTask != null)
                            m_NowDownFileTask.m_AnswerFlag = true;
                        m_bCheckTimeOut = false;
                        DowithPhaseData(pRecBuff);
                        break;
                }
            }
            catch
            {

            }
            return 0;
        }
        private void CheckHeartTimeOut()
        {
            if (m_bHeartOnlie)
            {
                TimeSpan spanTime = DateTime.Now - m_HeartTime;
                if (spanTime.Seconds >= 50)
                {
                    CGlobal.PostMessage(theDevice.m_MainFormHandle, UseMessage.MY_MSG_5, 0, 0);
                    m_bHeartOnlie = false;
                }
            }
        }
        private int Send_UpdateOrder(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 220; //类型
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 2; //文件传输

            SendB[nPlace++] = 7;//操作标识
            SendB[nPlace++] = 0;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Pro_UpdateOrder(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 211; //类型
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x80; //命令类型
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Pro_UpdateEndOrder(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 211; //类型
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x00; //命令类型
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_RestartDev(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x91; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Pro_UpdateCancelOrder(byte[] SendB)
        {
            int nPlace = 6;
            SendB[nPlace++] = 211; //类型
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x08;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x00; //命令类型
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Read_AeroNo(byte[] SendB)
        {

            int nPlace = 6;
            SendB[nPlace++] = 201; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_Change_AeroNo(byte[] SendB)
        {

            int nPlace = 6;
            SendB[nPlace++] = 200; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        public override void InitPara()
        {
            base.InitPara();
            m_SCount = 0;
            usSendNo = 0;
            usRecvNo = 0;
            m_STime = DateTime.Now;
        }
        public override int GetTheRightBuffContent(byte[] pRecBuff, int nBuffLeng, Byte[] pGetBuff, ref int nGetLeng)
        {
            int nRetLeng = nBuffLeng;
            if (theDevice.m_Connection.IsEncode)
            {
                nRetLeng = base.GetTheRightBuffContent(pRecBuff, nBuffLeng, pGetBuff, ref nGetLeng);
            }
            else
            {
                if (nBuffLeng < 6)
                    return 0;    ///最小长度为6

                int nDataLeng = 0;
                for (int i = 0; i < nBuffLeng; i++)
                {
                    if (pRecBuff[i] == 0x68)
                    {
                        if ((nBuffLeng - i) < 2)
                            return i;
                        nDataLeng = pRecBuff[i + 1] + 2;
                        if ((nDataLeng + i) > nBuffLeng)
                            return i;
                        nGetLeng = nDataLeng;
                        Array.Copy(pRecBuff, i, pGetBuff, 0, nGetLeng);
                        nRetLeng = nGetLeng + i;
                        break;
                    }
                    else
                    {
                        nRetLeng = i + 1;
                    }
                }
            }
            return nRetLeng;
        }
        public override void GetTestOrSBuff(Byte[] pSendBuff, ref int pSendLeng)
        {
            if (m_SCount >= 4)
            {
                if (m_StartFlag)
                {
                    pSendLeng = GetTaskBuffer(pSendBuff, 21);
                    m_STime = DateTime.Now;
                    m_SCount = 0;
                }
            }
            else
            {
                if (m_StartFlag)
                {
                    TimeSpan spanTime = DateTime.Now - m_STime;
                    if (spanTime.TotalSeconds >= 15)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, 21);
                        m_STime = DateTime.Now;
                        m_SCount = 0;
                    }
                }
            }
            if (pSendLeng == 0)
            {
                if (m_StartFlag)
                {
                    TimeSpan spanTime = DateTime.Now - m_HeartTime;
                    if (spanTime.TotalSeconds >= 180)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, 2);
                        m_HeartTime = DateTime.Now;
                        m_bHeartOnlie = true;
                    }
                }
            }
        }
        public override void Process(Byte[] pRecvBuff, ref int pRecvLeng, Byte[] pSendBuff, ref int pSendLeng)
        {
            pSendLeng = 0;
            if (CGlobal.m_Device.m_Connection.IsEncode == true)
            {
                base.Process(pRecvBuff, ref pRecvLeng, pSendBuff, ref pSendLeng);
            }
            if (pRecvLeng > 0)
            {
                m_HeartTime = DateTime.Now;
                if (!CheckRecBuff(ref pRecvBuff, ref pRecvLeng))
                {
                    return;
                }
                int nRealLeng = pRecvBuff[1] + 2;
                if ((pRecvBuff[2] & 0x03) == 3 && nRealLeng == 6)                   ///U格式报文,用来启动\停止\链路测试三项功能
                {
                    int nTaskNo = DoWith_U_Recv(pRecvBuff[2]);
                    if (nTaskNo > 0)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                    }
                }
                else if ((pRecvBuff[2] & 0x03) == 1 && nRealLeng == 6)         ///S格式报文
                {
                    int nTaskNo = DoWith_S_Recv(pRecvBuff);
                    if (nTaskNo > 0)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                    }
                }
                else if ((pRecvBuff[2] & 0x01) == 0 && nRealLeng > 6)  ///I格式报文
                {
                    int nTaskNo = DoWith_I_Recv(pRecvBuff, nRealLeng);
                    if (nTaskNo > 0)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                    }
                }
                else
                {
                    pRecvLeng = 0;
                }
            }
            else
            {
                CheckTimeOut();
                CheckHeartTimeOut();
            }
        }
        public override string GetBuffContent(Byte[] pBuffer)
        {
            #region 校验参数名称
            string[] s_name = new string[18];
            int nT = 0;
            s_name[nT++] = "U1";
            s_name[nT++] = "U2";
            s_name[nT++] = "U3";
            s_name[nT++] = "U0";
            s_name[nT++] = "CIA";
            s_name[nT++] = "CIB";
            s_name[nT++] = "CIC";
            s_name[nT++] = "BIA";
            s_name[nT++] = "BIB";
            s_name[nT++] = "BIC";
            s_name[nT++] = "I0";

            s_name[nT++] = "ΦA";
            s_name[nT++] = "ΦB";
            s_name[nT++] = "ΦC";
            s_name[nT++] = "Freq";
            s_name[nT++] = "COS";
            s_name[nT++] = "P";
            s_name[nT++] = "Q";
            #endregion
            #region 相角名称
            string[] A_name = new string[6];
            A_name[0] = "UA";
            A_name[1] = "UB";
            A_name[2] = "UC";
            A_name[3] = "IA";
            A_name[4] = "IB";
            A_name[5] = "IC";
            #endregion
            string strOut = "";
            string SendYY = "";
            string temp = "";
            int nPlace = 0;
            byte[] data = new byte[4];

            try
            {
                #region //头部
                strOut += String.Format("{0:X2}", pBuffer[0]) + "   " + "启动符" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[1]) + "   " + "数据长度----控制域+运用服务长度" + pBuffer[1].ToString() + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[2]) + "   " + "控制域1" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[3]) + "   " + "控制域2" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[4]) + "   " + "控制域3" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[5]) + "   " + "控制域4" + "\r\n";
                #endregion
                #region //类型
                switch (pBuffer[8])
                {
                    case 1:
                        temp = "单点遥信";
                        break;
                    case 3:
                        temp = "双点遥信";
                        break;
                    case 9:
                        temp = "遥测值，归一化";
                        break;
                    case 11:
                        temp = "遥测值，标度化";
                        break;
                    case 13:
                        temp = "遥测值，短浮点数";
                        break;
                    case 30:
                        temp = "带时标的单遥信";
                        break;
                    case 31:
                        temp = "带时标的双遥信";
                        break;
                    case 42:
                        temp = "故障事件信息";
                        break;

                    case 70:
                        temp = "初始化结束";
                        break;
                    case 100:
                        temp = "总召唤";
                        break;
                    case 45:
                        temp = "单点遥控";
                        break;
                    case 46:
                        temp = "双点遥控";
                        break;
                    case 110:
                        temp = "读装置状态";
                        break;
                    case 111:
                        temp = "私有数据召唤";
                        break;
                }

                #endregion
                #region //限定词
                strOut += String.Format("{0:X2}", pBuffer[6]) + "   " + "类型标识：" + pBuffer[6].ToString() + temp + "\r\n";
                int Infonum = pBuffer[7] % 0x80;
                int SQ = pBuffer[7] / 0x80;
                if (SQ == 0)
                {
                    temp = "非顺序";
                }
                else
                {
                    temp = "顺序";
                }
                strOut += String.Format("{0:X2}", pBuffer[7]) + "   " + "可变结构限定词VSQ---信息数目=" + (Infonum).ToString() + "SQ:" + (SQ).ToString() + temp + "\r\n";
                #endregion
                #region //传送原因
                switch (pBuffer[8])
                {
                    case 1:
                        SendYY = "周期/循环";
                        break;
                    case 2:
                        SendYY = "背景扫描";
                        break;
                    case 3:
                        SendYY = "突发";
                        break;
                    case 5:
                        SendYY = "被请求";
                        break;
                    case 7:
                        SendYY = "激活确认";
                        break;
                    case 8:
                        SendYY = "停止激活";
                        break;
                    case 9:
                        SendYY = "停止激活确认";
                        break;
                    case 10:
                        SendYY = "激活结束";
                        break;
                    case 20:
                        SendYY = "响应站召唤";
                        break;
                    case 47:
                        SendYY = "未知的信息对象地址";
                        break;

                }
                #endregion
                strOut += String.Format("{0:X2}", pBuffer[8]) + " " + String.Format("{0:X2}", pBuffer[9]) + "   " + "传送原因:" + SendYY + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[10]) + " " + String.Format("{0:X2}", pBuffer[11]) + "   " + "公共地址" + "低前，高后" + "\r\n";
                nPlace = 12;
                #region //具体类型的解析
                switch (pBuffer[6])
                {
                    case 70:
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息体地址" + "\r\n";
                        nPlace = nPlace + 3;
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "初始化原因" + "" + "\r\n";
                        break;
                    case 100:
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息体地址" + "\r\n";
                        nPlace = nPlace + 3;
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "召唤限定词" + "" + "\r\n";
                        break;
                    case 1:
                        if (SQ == 0)
                        {
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息对象首地址:" + temp + "\r\n";
                                nPlace = nPlace + 3;
                                if ((pBuffer[nPlace] & 0x01) != 0)
                                {
                                    temp = "SPI:1(合)";
                                }
                                else
                                {
                                    temp = "SPI:0(分)";
                                }
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + "值描述:---" + temp + "\r\n";
                                nPlace = nPlace + 1;
                            }
                        }
                        else
                        {
                            temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息对象首地址:" + temp + "\r\n";
                            nPlace = nPlace + 3;
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                if ((pBuffer[nPlace] & 0x01) != 0)
                                {
                                    temp = "SPI:1(合)";
                                }
                                else
                                {
                                    temp = "SPI:0(分)";
                                }
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + "值描述:---" + temp + "\r\n";
                                nPlace = nPlace + 1;
                            }
                        }
                        break;
                    case 3:
                        if (SQ == 0)
                        {
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息对象首地址:" + temp + "\r\n";
                                nPlace = nPlace + 3;
                                if ((pBuffer[nPlace] & 0x03) == 0)
                                {
                                    temp = "DPI:0(不确定状态)";
                                }
                                else if ((pBuffer[nPlace] & 0x03) == 1)
                                {
                                    temp = "SPI:1(确定状态开)";
                                }
                                else if ((pBuffer[nPlace] & 0x03) == 2)
                                {
                                    temp = "SPI:2(确定状态合)";
                                }
                                else
                                {
                                    temp = "DPI:3(不确定状态)";
                                }
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + "值描述:---" + temp + "\r\n";
                                nPlace = nPlace + 1;
                            }
                        }
                        else
                        {
                            temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息对象首地址:" + temp + "\r\n";
                            nPlace = nPlace + 3;
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                if ((pBuffer[nPlace] & 0x03) == 0)
                                {
                                    temp = "DPI:0(不确定状态)";
                                }
                                else if ((pBuffer[nPlace] & 0x03) == 1)
                                {
                                    temp = "SPI:1(确定状态开)";
                                }
                                else if ((pBuffer[nPlace] & 0x03) == 2)
                                {
                                    temp = "SPI:2(确定状态合)";
                                }
                                else
                                {
                                    temp = "DPI:3(不确定状态)";
                                }
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + "值描述:---" + temp + "\r\n";
                                nPlace = nPlace + 1;
                            }
                        }
                        break;
                    case 13:
                        if (SQ == 0)
                        {
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256 + pBuffer[nPlace + 2] * 256 * 256).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息体地址：" + temp + "\r\n";
                                nPlace = nPlace + 3;
                                Array.Copy(pBuffer, nPlace, data, 0, 4);
                                temp = BitConverter.ToSingle(data, 0).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 3]) + "遥测值:" + temp + "\r\n";
                                nPlace = nPlace + 5;
                            }
                        }
                        else
                        {
                            temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256 + pBuffer[nPlace + 2] * 256 * 256).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息对象首地址:" + temp + "\r\n";
                            nPlace = nPlace + 3;
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                Array.Copy(pBuffer, nPlace, data, 0, 4);
                                temp = BitConverter.ToSingle(data, 0).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 3]) + "遥测值:" + temp + "\r\n";
                                nPlace = nPlace + 5;
                            }

                        }
                        break;
                    case 202:
                    case 0x6F:
                        temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "定值区号SN:" + temp + "\r\n";
                        nPlace = nPlace + 2;
                        temp = (pBuffer[nPlace]).ToString();
                        if ((pBuffer[nPlace] & 0x01) == 0)
                        {
                            temp = temp + " " + "无后续";
                        }
                        else
                        {
                            temp = temp + " " + "有后续";
                        }
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "参数特征标识:" + temp + "\r\n";
                        nPlace = nPlace + 1;
                        for (int i = 0; i < Infonum; i++)
                        {
                            strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                            temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256 + pBuffer[nPlace + 2] * 256 * 256).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + "信息体地址：" + temp + "\r\n";
                            nPlace = nPlace + 3;

                            #region  //类型
                            switch (pBuffer[nPlace])
                            {
                                case 1:
                                    temp = "bool";
                                    break;
                                case 2:
                                    temp = "int";
                                    break;
                                case 4:
                                    temp = "string";
                                    break;
                                case 32:
                                    temp = "Byte";
                                    break;
                                case 33:
                                    temp = "short";
                                    break;
                                case 43:
                                    temp = "sbyte";
                                    break;
                                case 45:
                                    temp = "ushort";
                                    break;
                                case 35:
                                    temp = "uint";
                                    break;
                                case 36:
                                    temp = "long";
                                    break;
                                case 37:
                                    temp = "ulong";
                                    break;
                                case 38:
                                    temp = "float";
                                    break;
                                case 39:
                                    temp = "double";
                                    break;

                            }

                            #endregion
                            int tag = pBuffer[nPlace];
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "Tag类型:" + temp + "\r\n";
                            nPlace = nPlace + 1;
                            temp = pBuffer[nPlace].ToString();
                            int Len = pBuffer[nPlace];
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "数据长度:" + temp + "\r\n";
                            nPlace = nPlace + 1;
                            #region //值
                            if ((tag == 1) && (Len == 1))//bool
                            {
                                if (pBuffer[nPlace] == 1)
                                    temp = "1";
                                else
                                    temp = "0";
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "值:" + temp + "\r\n";
                            }
                            else if ((tag == 43) && (Len == 1))//小整形
                            {
                                temp = (Convert.ToInt16(pBuffer[nPlace])).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "值:" + temp + "\r\n";
                            }
                            else if ((tag == 32) && (Len == 1))//无符号小整形
                            {
                                temp = pBuffer[nPlace].ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "值:" + temp + "\r\n";
                            }
                            else if ((tag == 33) && (Len == 2))//小整形
                            {
                                byte[] data1 = new byte[2];
                                Array.Copy(pBuffer, nPlace, data1, 0, 2);
                                temp = (BitConverter.ToInt16(data1, 0)).ToString();//有符号
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "值:" + temp + "\r\n";

                            }
                            else if ((tag == 45) && (Len == 2))
                            {
                                byte[] data1 = new byte[2];
                                Array.Copy(pBuffer, nPlace, data1, 0, 2);
                                temp = (BitConverter.ToUInt16(data1, 0)).ToString();//无符号
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "值:" + temp + "\r\n";

                            }
                            else if ((tag == 2) && (Len == 4))//整形
                            {
                                Array.Copy(pBuffer, nPlace, data, 0, 4);
                                temp = (BitConverter.ToInt32(data, 0)).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + String.Format("{0:X2}", pBuffer[nPlace + 2]) + String.Format("{0:X2}", pBuffer[nPlace + 3]) + "值:" + temp + "\r\n";
                            }
                            else if ((tag == 35) && (Len == 4))//无符号整形
                            {
                                Array.Copy(pBuffer, nPlace, data, 0, 4);
                                temp = (BitConverter.ToUInt32(data, 0)).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + String.Format("{0:X2}", pBuffer[nPlace + 2]) + String.Format("{0:X2}", pBuffer[nPlace + 3]) + "值:" + temp + "\r\n";

                            }
                            else if ((tag == 36) && (Len == 8))//长整形
                            {
                                byte[] data2 = new byte[8];
                                Array.Copy(pBuffer, nPlace, data2, 0, 8);
                                temp = (BitConverter.ToInt64(data2, 0)).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + String.Format("{0:X2}", pBuffer[nPlace + 2]) + String.Format("{0:X2}", pBuffer[nPlace + 3]);
                                strOut += String.Format("{0:X2}", pBuffer[nPlace + 4]) + String.Format("{0:X2}", pBuffer[nPlace + 5]) + String.Format("{0:X2}", pBuffer[nPlace + 6]) + String.Format("{0:X2}", pBuffer[nPlace + 7]);
                                strOut += "值:" + temp + "\r\n";

                            }
                            else if ((tag == 37) && (Len == 8))//无符号长整形
                            {
                                byte[] data2 = new byte[8];
                                Array.Copy(pBuffer, nPlace, data2, 0, 8);
                                temp = (BitConverter.ToUInt64(data2, 0)).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + String.Format("{0:X2}", pBuffer[nPlace + 2]) + String.Format("{0:X2}", pBuffer[nPlace + 3]);
                                strOut += String.Format("{0:X2}", pBuffer[nPlace + 4]) + String.Format("{0:X2}", pBuffer[nPlace + 5]) + String.Format("{0:X2}", pBuffer[nPlace + 6]) + String.Format("{0:X2}", pBuffer[nPlace + 7]);
                                strOut += "值:" + temp + "\r\n";
                                ;
                            }
                            else if ((tag == 38) && (Len == 4))//单精度浮点
                            {
                                Array.Copy(pBuffer, nPlace, data, 0, 4);
                                temp = (BitConverter.ToSingle(data, 0)).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + String.Format("{0:X2}", pBuffer[nPlace + 2]) + String.Format("{0:X2}", pBuffer[nPlace + 3]) + "值:" + temp + "\r\n";

                            }
                            else if ((tag == 39) && (Len == 8))//双精度浮点
                            {
                                byte[] data2 = new byte[8];
                                Array.Copy(pBuffer, nPlace, data2, 0, 8);
                                temp = (BitConverter.ToSingle(data2, 0)).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + String.Format("{0:X2}", pBuffer[nPlace + 1]) + String.Format("{0:X2}", pBuffer[nPlace + 2]) + String.Format("{0:X2}", pBuffer[nPlace + 3]);
                                strOut += String.Format("{0:X2}", pBuffer[nPlace + 4]) + String.Format("{0:X2}", pBuffer[nPlace + 5]) + String.Format("{0:X2}", pBuffer[nPlace + 6]) + String.Format("{0:X2}", pBuffer[nPlace + 7]);
                                strOut += "值:" + temp + "\r\n";
                            }
                            else if (tag == 4)
                            {
                                byte[] data3 = new byte[Len];
                                Array.Copy(pBuffer, nPlace, data3, 0, Len);
                                temp = CGlobal.BytesToString(data3);
                                for (int j = 0; j < Len; j++)
                                {
                                    strOut += String.Format("{0:X2}", pBuffer[nPlace + j]);
                                }
                                strOut += "值:" + temp + "\r\n";
                            }
                            #endregion
                            nPlace = nPlace + Len;
                        }
                        break;
                    case 203:
                    case 0x70:
                        temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "定值区号SN:" + temp + "\r\n";
                        nPlace = nPlace + 2;
                        temp = (pBuffer[nPlace]).ToString();
                        if ((pBuffer[nPlace] & 0x80) == 0)
                        {
                            temp = temp + " " + "固化";
                        }
                        else
                        {
                            temp = temp + " " + "预置";
                        }
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "参数特征标识:" + temp + "\r\n";
                        break;
                    case 0x71:
                        temp = (pBuffer[nPlace]).ToString();
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "线路编号:" + temp + "\r\n";
                        nPlace = nPlace + 1;

                        for (int i = 0; i < Infonum; i++)
                        {
                            temp = (pBuffer[nPlace]).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + s_name[i] + "校验标识:" + temp + "\r\n";
                            nPlace = nPlace + 1;
                            Array.Copy(pBuffer, nPlace, data, 0, 4);
                            temp = BitConverter.ToSingle(data, 0).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 3]) + s_name[i] + "校验后测量值:" + temp + "\r\n";
                            nPlace = nPlace + 4;
                        }
                        break;
                    case 0x72:
                        for (int i = 0; i < Infonum; i++)
                        {
                            temp = (pBuffer[nPlace]).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + s_name[i] + "下装标识:" + temp + "\r\n";
                            nPlace = nPlace + 1;
                            temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + s_name[i] + "系数:" + temp + "\r\n";
                            nPlace = nPlace + 2;
                        }
                        break;
                    case 0x73:
                        temp = (pBuffer[nPlace]).ToString();
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "回线号:" + temp + "\r\n";
                        nPlace = nPlace + 1;
                        for (int i = 0; i < Infonum; i++)
                        {
                            Array.Copy(pBuffer, nPlace, data, 0, 4);
                            temp = BitConverter.ToSingle(data, 0).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 3]) + A_name[i] + "相角:" + temp + "\r\n";
                            nPlace = nPlace + 4;
                        }
                        break;
                }
                #endregion
            }
            catch { }

            return "";
        }

        private int GetTaskBuffer_104(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;

            switch (nTaskNo)
            {
                case 1: //发送连接报文
                    InitPara();
                    SetTimeOut();
                    nSendLeng = Send_U_Report(0x07, pSendBuffer);
                    break;
                case 2:  //发送测试报文
                    nSendLeng = Send_U_Report(0x43, pSendBuffer);
                    break;
                case 3:  //发送停止报文
                    nSendLeng = Send_U_Report(0x13, pSendBuffer);
                    break;
                case 4://发送总召报文
                    SetTimeOut();
                    nSendLeng = SendCallTotalData(pSendBuffer);
                    break;
                case 5://发送对时报文
                    nSendLeng = Send_Sync_Time(pSendBuffer);
                    break;
                case 6://遥控
                    SetTimeOut();
                    nSendLeng = Send_YK_Cmd(pSendBuffer);
                    break;
                case 7://召电度
                    SetTimeOut();
                    nSendLeng = SendCallPowerData(pSendBuffer);
                    break;

                case 8://读定值参数
                    SetTimeOut();
                    nSendLeng = Send_Read_Para(pSendBuffer);
                    break;
                case 9://定值参数预置
                    SetTimeOut();
                    nSendLeng = Send_Preset_Para(pSendBuffer);
                    break;
                case 10://定值参数固化
                    SetTimeOut();
                    nSendLeng = Send_Write_Para(pSendBuffer);
                    break;
                case 42://定值撤销
                    SetTimeOut();
                    nSendLeng = Send_CancelWrite_Para(pSendBuffer);
                    break;
                case 20://通讯测试应答
                    m_HeartTime = DateTime.Now;
                    nSendLeng = Send_U_Report(0x83, pSendBuffer);
                    break;
                case 21://S格式
                    nSendLeng = Send_S_Report(pSendBuffer);
                    break;
                case 22://读目录
                    SetTimeOut();
                    nSendLeng = Send_Call_Dir(pSendBuffer);
                    break;
                case 23://读文件激活
                    SetTimeOut();
                    nSendLeng = Send_Call_File_Active(pSendBuffer);
                    break;
                case 24://读文件数据传输确认
                    nSendLeng = Send_Call_FileData_Conform(pSendBuffer);
                    break;
                case 25://发送校验报文
                    SetTimeOut();
                    nSendLeng = Send_Call_CheckData(pSendBuffer);
                    break;
                case 26://发送召唤系数
                    SetTimeOut();
                    nSendLeng = Send_Call_RatioData(pSendBuffer);
                    break;
                case 27://发送下装系数
                    SetTimeOut();
                    nSendLeng = Send_Down_RatioData(pSendBuffer);
                    break;
                case 28: //读取所有的参数
                    SetTimeOut();
                    nSendLeng = Send_ReadAll_Para(pSendBuffer);
                    break;
                case 29://发送写文件激活命令
                    SetTimeOut();
                    nSendLeng = Send_WriteFileActive(pSendBuffer);
                    break;
                case 30://发送写文件数据传输命令
                    SetTimeOut();
                    nSendLeng = Send_WriteFileData(pSendBuffer);
                    break;
                case 31://发送召唤绝对相角
                    SetTimeOut();
                    nSendLeng = Send_Call_Phase(pSendBuffer);
                    break;
                case 32://发送直流校验
                    SetTimeOut();
                    nSendLeng = Send_Call_DC_CheckData(pSendBuffer);
                    break;
                case 33:
                    nSendLeng = Send_UpdateOrder(pSendBuffer);
                    break;
                case 34:  //国网升级启动
                    nSendLeng = Send_Pro_UpdateOrder(pSendBuffer);
                    break;
                case 35:  //国网升级结束
                    nSendLeng = Send_Pro_UpdateEndOrder(pSendBuffer);
                    break;
                case 36://国网升级撤销
                    nSendLeng = Send_Pro_UpdateCancelOrder(pSendBuffer);
                    break;
                case 37://读取定值区号
                    nSendLeng = Send_Read_AeroNo(pSendBuffer);
                    break;
                case 38://切换定值区号
                    nSendLeng = Send_Change_AeroNo(pSendBuffer);
                    break;
                case 39://瞬时冻结数据
                    nSendLeng = SendGetFrzData(pSendBuffer);
                    break;
                case 41://读时钟
                    nSendLeng = Send_CallSync_Time(pSendBuffer);
                    break;
                case 60://重启Arm
                    nSendLeng = Send_RestartDev(pSendBuffer);
                    break;
                case 61://遥信置数
                    nSendLeng = Send_YX_Lock(pSendBuffer);
                    break;
                case 62://遥信解锁
                    nSendLeng = Send_YX_UnLock(pSendBuffer);
                    break;
                case 63://遥测置数
                    nSendLeng = Send_YC_Lock(pSendBuffer);
                    break;
                case 64://遥测解锁
                    nSendLeng = Send_YC_UnLock(pSendBuffer);
                    break;
                default:
                    nSendLeng = base.GetTaskBuffer(pSendBuffer, nTaskNo);
                    break;
            }
            return nSendLeng;
        }
        private int Send_YX_Lock(byte[] SendB)
        {



            int nPlace = 0;
            SendB[nPlace++] = 0xAA; //类型
            SendB[nPlace++] = 0x01;
            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo % 256);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo / 256);
            SendB[nPlace++] = 0x00;
            SendB[nPlace++] = CGlobal.m_YxValue;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_YX_UnLock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAB; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo % 256);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo / 256);
            SendB[nPlace++] = 0x00;
            SendB[nPlace++] = 0;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_YC_Lock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAC; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);

            int addr = CGlobal.m_YcNo + 0x4001 - 1;
            SendB[nPlace++] = (Byte)(addr % 256);
            SendB[nPlace++] = (Byte)(addr / 256);
            SendB[nPlace++] = 0x00;
            byte[] temp = BitConverter.GetBytes(CGlobal.m_YcValue);
            SendB[nPlace++] = temp[0];
            SendB[nPlace++] = temp[1];
            SendB[nPlace++] = temp[2];
            SendB[nPlace++] = temp[3];

            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        private int Send_YC_UnLock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAD; //类型
            SendB[nPlace++] = 0x01;

            SendB[nPlace++] = 0x06;
            SendB[nPlace++] = 0x00;

            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr % 256);
            SendB[nPlace++] = Convert.ToByte(theDevice.m_ASDUAddr / 256);
            int addr = CGlobal.m_YcNo + 0x4001 - 1;
            SendB[nPlace++] = (Byte)(addr % 256);
            SendB[nPlace++] = (Byte)(addr / 256);
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            Send_I_Report(SendB, nPlace - 2);
            return nPlace;
        }
        public override int GetTaskBuffer(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;
            if ((nTaskNo > 0) && (nTaskNo < 0x85))
            {
                nSendLeng = GetTaskBuffer_104(pSendBuffer, nTaskNo);
            }
            else if (nTaskNo >= 1000000)
            {
                nSendLeng = base.GetTaskBuffer(pSendBuffer, nTaskNo);
            }

            if ((CGlobal.m_Device.m_Connection.IsEncode == true) && (nTaskNo < 1000000))
            {
                nSendLeng = GetTaskBuffer_Encode(pSendBuffer, nTaskNo, nSendLeng);
            }
            return nSendLeng;
        }
    }
}
