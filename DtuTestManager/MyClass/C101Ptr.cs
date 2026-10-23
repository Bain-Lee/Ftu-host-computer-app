using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace DtuTestManager
{
    public partial class C101Ptr : CPtrInterface
    {

        public bool IsShortFrame = false;
        // public int LAddr = 0x01;
        public bool NeedResend;
        public byte[] ReSendB = new byte[2000];
        public int ReSendBLen;

        bool bIsSpont = false;
        bool IsContinue = false;
        bool bIsDouble = false;
        byte btASDU = 0, btLimit = 0, btCot = 0;
        int nDataPlace = 0;
        int Offset = 0;
        int DataCount = 0;
      
       
        private Byte m_FCB = 0x53;
        private Byte GetFCB()
        {
            Byte pFCb = m_FCB;
            if (m_FCB == 0x73)
                m_FCB = 0x53;
            else
                m_FCB = 0x73;
            return pFCb;
        }
        private int GetAddressLeng()
        {
            return m_AddressType + 1;
        }
        private int GetCotLeng()
        {
            return m_CotType + 1;
        }
        private int GetCtrlStatus(byte[] pBuff, ref byte byteASDU, ref byte byteLimit, ref byte byteCot)
        {
            bIsSpont = false;
            IsContinue = false;
            bIsDouble = false;

            int nASDUPlace = 5 + GetAddressLeng();
            int nLimitPlace = nASDUPlace + 1;
            int nCotPlace = nLimitPlace + 1;
            byteASDU = pBuff[nASDUPlace];
            byteLimit = pBuff[nLimitPlace];
            byteCot = pBuff[nCotPlace];
            return nCotPlace + GetAddressLeng() + GetCotLeng();
        }
        private void ASDU_YX(byte[] TB, bool bSoeFlag)
        {
            int StartAddr;
            int i, nValue;
            string strTimeMsg;

            if (btASDU == 3 || btASDU == 0x1F) bIsDouble = true;            //双点
            if ((btLimit & 0x80) == 0x80) IsContinue = true;                //连续状态
            if (btCot == 3) bIsSpont = true;                                //突发上送
            DataCount = btLimit & 0x7F;                                     //数量

            if (bSoeFlag)
            {
                if (IsContinue == true)
                {
                    StartAddr = TB[nDataPlace] + TB[nDataPlace + 1] * 256;
                    Offset = nDataPlace + 2;
                    for (i = 0; i < DataCount; i++)
                    {
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
                    Offset = nDataPlace;
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        Offset = Offset + 2;
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
                    StartAddr = TB[nDataPlace] + TB[nDataPlace + 1] * 256;
                    Offset = nDataPlace + 2;
                    for (i = 0; i < DataCount; i++)
                    {
                        strTimeMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        SetYxNowStatus(StartAddr + i, Convert.ToByte(TB[Offset] & 0x03));
                        Offset++;
                    }
                }
                else
                {
                    Offset = nDataPlace;
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        Offset = Offset + 2;
                        strTimeMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        SetYxNowStatus(StartAddr, Convert.ToByte(TB[Offset] & 0x03));
                        Offset++;
                    }
                }
            }
            theDevice.m_bYxFresh = true;
        }
        private void DoWithYC(byte[] TB)
        {
            try
            {
                int StartAddr;
                float fValue = 0;
                int i;
                byte[] fValueBuff = new byte[4];
                Int16 nValue = 0;

                if ((btLimit & 0x80) == 0x80) IsContinue = true;                //连续状态
                DataCount = btLimit & 0x7F;                                     //数量

                if (IsContinue == true)
                {
                    StartAddr = TB[nDataPlace] + TB[nDataPlace + 1] * 256;
                    Offset = nDataPlace + 2;
                    switch (btASDU)
                    {
                        case 0x09:      //归一化
                        case 0x0B:      //标度化
                            for (i = 0; i < DataCount; i++)
                            {
                                nValue = (Int16)(TB[Offset] + TB[Offset + 1] * 256);
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
                    Offset = nDataPlace;
                    switch (btASDU)
                    {
                        case 0x09:      //归一化
                        case 0x0B:      //表度化
                            for (i = 0; i < DataCount; i++)
                            {
                                StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                                Offset += 2;
                                nValue = (Int16)(TB[Offset] + TB[Offset + 1] * 256);
                                Offset += 2;
                                SetYcValue(StartAddr, (float)nValue, TB[Offset]);
                                Offset += 1;
                            }
                            break;
                        case 0x0D:
                            for (i = 0; i < DataCount; i++)
                            {
                                StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                                Offset += 2;
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
            { }
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
            try
            {
                string str = "";
                int Addr;
                byte tag, Len, nType;

                DataCount = btLimit & 0x7F;                                     //数量
                nType = TB[nDataPlace + 2];
                Offset = nDataPlace + 3;

                for (int j = 0; j < DataCount; j++)
                {
                    try
                    {
                        Addr = TB[Offset] + TB[Offset + 1] * 256;//地址
                        int nIndex = BinarySearch(0, CGlobal.m_Device.m_TotalParaDataList.Count, Addr);
                        int m_Real_ParaType = CGlobal.m_Device.m_TotalParaDataList[nIndex].m_ParaType;
                        Offset += 2;
                        tag = TB[Offset++];//tag类型
                        Len = TB[Offset++];//数据长度;
                        if ((tag == 1) && (Len == 1))//bool
                        {
                            if (TB[Offset] == 1)
                                str = "1";
                            else
                                str = "0";
                        }
                        else if ((tag == 43) && (Len == 1))//小整形
                        {
                            str = (Convert.ToInt16(TB[Offset])).ToString();

                        }
                        else if ((tag == 32) && (Len == 1))//无符号小整形
                        {
                            str = TB[Offset].ToString();

                        }
                        else if ((tag == 33) && (Len == 2))//小整形
                        {
                            byte[] data = new byte[2];
                            Array.Copy(TB, Offset, data, 0, 2);
                            str = (BitConverter.ToInt16(data, 0)).ToString();//有符号

                        }
                        else if ((tag == 45) && (Len == 2))
                        {
                            byte[] data = new byte[2];
                            Array.Copy(TB, Offset, data, 0, 2);
                            str = (BitConverter.ToUInt16(data, 0)).ToString();//无符号

                        }
                        else if ((tag == 2) && (Len == 4))//整形
                        {
                            byte[] data = new byte[4];
                            Array.Copy(TB, Offset, data, 0, 4);
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
                            Array.Copy(TB, Offset, data, 0, 4);
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
                            Array.Copy(TB, Offset, data, 0, 8);
                            str = (BitConverter.ToInt64(data, 0)).ToString();

                        }
                        else if ((tag == 37) && (Len == 8))//无符号长整形
                        {
                            byte[] data = new byte[8];
                            Array.Copy(TB, Offset, data, 0, 8);
                            str = (BitConverter.ToUInt64(data, 0)).ToString();

                        }
                        else if ((tag == 38) && (Len == 4))//单精度浮点
                        {
                            byte[] data = new byte[4];
                            Array.Copy(TB, Offset, data, 0, 4);
                            str = (BitConverter.ToSingle(data, 0)).ToString();

                        }
                        else if ((tag == 39) && (Len == 8))//双精度浮点
                        {
                            byte[] data = new byte[8];
                            Array.Copy(TB, Offset, data, 0, 8);
                            str = (BitConverter.ToSingle(data, 0)).ToString();

                        }
                        else if (tag == 4)
                        {
                            byte[] data = new byte[Len];
                            Array.Copy(TB, Offset, data, 0, Len);
                            str = CGlobal.BytesToString(data);
                        }
                        Offset += Len;
                        theDevice.SetParaValue(Addr, str);
                    }
                    catch
                    {

                    }
                }
                if (btCot == 47)
                {
                    //终端激活终止  开始显示
                    // MessageBox.Show("读定值异常", "提示");
                    return;
                }
                else if (btCot == 7)
                {
                    //终端激活确认
                    //不处理
                }
                if ((0x01 & nType) == 0) theDevice.m_bParaFresh = true;
            }
            catch
            { }
        }
        private void DoWithwriteDZ(byte[] TB)
        {
            try
            {
                byte nType = 0;
                nType = TB[nDataPlace + 2];

                int se = 0x80 & nType;
                int CR = 0x40 & nType;
                if ((se != 0) && (CR == 0) && (btCot == 7))  //预置成功
                {
                    if (theDevice.m_ParaHandle >= 0) CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_1, 0, 0);
                   
                }
                else if ((se == 0) && (CR == 0) && (btCot == 7))  //固化成功
                {
                    if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;          
                    if (theDevice.m_ParaHandle >= 0) CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_2, 0, 0);
                                     
                }
            }
            catch
            { }
        }
        private void DoWithYK(byte[] TB)
        {
            ;
            try
            {

                if (btCot == 7 || btCot == 9 || btCot == 10)
                    CGlobal.PostMessage(theDevice.m_YkFormHandle, UseMessage.MY_MSG_7, 0, 0);
                else
                    CGlobal.PostMessage(theDevice.m_YkFormHandle, UseMessage.MY_MSG_8, 0, 0);

                if ((CYKForm.YKStatus == true) && (btCot == 7) && (CYKForm.YKTest = true))
                {
                    CGlobal.PostMessage(theDevice.m_YkFormHandle, UseMessage.MY_MSG_9, 0, 0);
                }
            }
            catch
            { }

        }
        private void DoWithreadDir(byte[] TB)
        {
            try
            {
                string nYear, nMonth, nDay, nHour, nMinute, nSecond, nMilliSecond;
                Offset = nDataPlace;
                Offset += 4;
                if (TB[Offset] == 0)//成功
                {

                    if (TB[Offset + 6] == 0)//无目录
                    {
                        CGlobal.PostMessage(theDevice.m_FileFormHandle, UseMessage.MY_MSG_2, 0, 0);
                        return;
                    }
                }
                else
                {
                    if (TB[Offset + 6] == 0)//无目录
                    {
                        CGlobal.PostMessage(theDevice.m_FileFormHandle, UseMessage.MY_MSG_3, 0, 0);
                    }
                    return;
                }

                int hxbz = TB[Offset + 5];  //后续标志
                int f_num = TB[Offset + 6];  //文件数量

                Offset = Offset + 7;
                for (int k = 0; k < f_num; k++)
                {
                    CFileCtrl pNewFile = new CFileCtrl();
                    int file_Len = TB[Offset];  //文件1名称长度
                    Offset++;
                    byte[] tempB = new byte[file_Len];
                    for (int i = 0; i < file_Len; i++)
                    {
                        tempB[i] = TB[Offset++];
                    }
                    pNewFile.m_FileTypeName = CGlobal.BytesToString(tempB);  //文件名称
                    if (pNewFile.m_FileTypeName.LastIndexOf(".") == -1)
                    {
                        pNewFile.m_FileTypeName += ".msg";
                    }

                    int shuxing = TB[Offset];//属性
                    Offset = Offset + 1;//大小
                    byte[] tb = new byte[4];
                    for (int i = 0; i < 4; i++)
                    {
                        tb[i] = TB[Offset++];
                    }

                    pNewFile.m_FileSize = System.BitConverter.ToInt32(tb, 0);  //文件大小         

                    int t = TB[Offset] + TB[Offset + 1] * 256;
                    nMilliSecond = Convert.ToString(t % 1000);
                    t = t - t % 1000;
                    t = t / 1000;
                    if (t >= 60)
                    { t = 0; }
                    nSecond = t.ToString("#00");
                    nMinute = TB[Offset + 2].ToString("#00");
                    nHour = (TB[Offset + 3]).ToString("#00");
                    nDay = (TB[Offset + 4] & 0x1F).ToString("#00");
                    nMonth = (TB[Offset + 5]).ToString("#00");
                    nYear = Convert.ToString(TB[Offset + 6] + 2000);

                    pNewFile.m_RecordTime = nYear + "-" + nMonth + "-" + nDay + "  " + nHour + ":" + nMinute + ":" + nSecond;// +nMilliSecond; //文件时间
                    if (theDevice.m_SelectFloder != null)
                    {
                        pNewFile.m_FilePath = theDevice.m_SelectFloder.m_FloderPath + "\\" + pNewFile.m_FileTypeName;
                        theDevice.m_SelectFloder.AddNewWaitReadFile(pNewFile);
                    }
                    Offset = Offset + 7;
                }
                if (hxbz == 0)
                {
                    CGlobal.PostMessage(theDevice.m_FileFormHandle, UseMessage.MY_MSG_4, 0, 0);
                }
            }
            catch
            { }
        }
        private void Dowithreadfile(byte[] TB)
        {
            try
            {
                Offset = nDataPlace;
                Offset += 4;
                if (TB[Offset] == 0)
                {
                    //成功
                }
                else
                {
                    theDevice.m_NowReadFile.m_ReadResult = "读取失败";
                    theDevice.m_NowReadFile.m_EndFlag = true;
                    return;
                }
                Offset++;
                int f_L = TB[Offset++];  //文件1名称长度
                byte[] tempB = new byte[f_L];
                for (int i = 0; i < f_L; i++)
                {
                    tempB[i] = TB[i + Offset];
                }
                /*string strtemp = CGlobal.BytesToString(tempB);  //文件名称*/
                Offset = Offset + f_L;//文件标识
                byte[] data = new byte[4];
                for (int i = 0; i < 4; i++)
                    data[i] = TB[Offset + i];
                theDevice.m_NowReadFile.m_FileRII = System.BitConverter.ToInt32(data, 0); //文件标识
                Offset = Offset + 4;
                for (int i = 0; i < 4; i++)
                    data[i] = TB[Offset + i];
                theDevice.m_NowReadFile.m_FileSize = System.BitConverter.ToInt32(data, 0); //文件大小
            }
            catch
            { }
        }
        private int Dowithreadfiledata(byte[] TB, int nLen)
        {
            try
            {
                int i = 0;
                Offset = nDataPlace;
                Offset += 4;
                byte[] data = new byte[4];
                for (i = 0; i < 4; i++)
                    data[i] = TB[Offset + i];//文件ID
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
                Offset = Offset + 4;

                for (i = 0; i < 4; i++)
                    data[i] = TB[Offset + i];
                if (theDevice.m_NowReadFile != null)
                    theDevice.m_NowReadFile.m_FilePointNo = System.BitConverter.ToInt32(data, 0); //数据段号
                Offset = Offset + 4;
                byte HouXu = TB[Offset++];
                CFileBuff pNewBuff = new CFileBuff();
                pNewBuff.m_PacketNo = theDevice.m_NowReadFile.m_FilePointNo;
                pNewBuff.m_DataLeng = nLen - Offset - 3;
                if (pNewBuff.m_DataLeng > 0)
                {
                    pNewBuff.m_DataBuff = new Byte[pNewBuff.m_DataLeng];
                    Array.Copy(TB, Offset, pNewBuff.m_DataBuff, 0, pNewBuff.m_DataLeng);
                    if (TB[nLen - 3] != CGlobal.GetCS(pNewBuff.m_DataBuff, 0, pNewBuff.m_DataLeng))
                    {
                        return 0;
                    }
                    if (theDevice.m_NowReadFile != null)
                        theDevice.m_NowReadFile.AddReadBuff(pNewBuff);
                }
                if (HouXu == 0)//无后续
                {
                    //SaveFile();//存文件 到TempFile     
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
            { }
            return 0;
        }
        private int DowithwritefileConform(byte[] TB)
        {
            Offset = nDataPlace;
            Offset += 4;
            if (TB[Offset] == 0)
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
            Offset = nDataPlace;
            Offset += 12;
            if (TB[Offset] == 0)
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
        public void Dowith_Read_AeroNo(byte[] TB)
        {
            Offset = nDataPlace + 2;
            if (btCot == 7)
            {
                m_ParaAreaNo = TB[Offset] + TB[Offset + 1] * 256;
                CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_4, 0, 0);
            }
            else if (btCot == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值区号异常", "提示");
            }

        }
        public void Dowith_Switch_AeroNo(byte[] TB)
        {
            if (btCot == 47)
            {
                // MessageBox.Show("切换定值区号异常", "提示");               
            }
        }
        private void DowWithProVersion(byte[] pRecBuff)
        {
            int nPlace = nDataPlace;
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
            int nPlace = nDataPlace + 2;
            if (pRecBuff[nDataPlace] == 0xff)
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
            int nPlace = nDataPlace + 1;
            for (int i = 0; i < 6; i++)
            {
                CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo][i].m_Value = BitConverter.ToSingle(pRecBuff, nPlace);//实测值
                nPlace = nPlace + 4;
            }
            theDevice.m_bPhaseFresh = true;

        }
        private void DowithReadRatioData(byte[] pRecBuff)
        {
            int nPlace = nDataPlace + 1;
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

            string strInfo = "";
            byte[] fValueBuff = new byte[4];
            float fValue = 0;
            int StartAddr, NewAddr, nValue;
            string strTimeMsg;
            Offset = nDataPlace;
            int YxCnt = TB[Offset++];//遥信个数 

            int YxType = TB[Offset++];
            if (YxType == 3 || YxType == 0x1F) bIsDouble = true;           //双点                
            for (i = 0; i < YxCnt; i++)
            {
                StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                NewAddr = StartAddr - m_YxStartAddress;
                Offset = Offset + 2;
                nValue = Convert.ToByte(TB[Offset] & 0x03);
                Offset++;
                Byte[] pTime = new Byte[7];
                Array.Copy(TB, Offset, pTime, 0, 7);
                strTimeMsg = GetSOETimeString(pTime);
                Offset = Offset + 7;
                strInfo = strInfo + strTimeMsg + " " + theDevice.m_YxList[NewAddr].m_YxName + " " + GetYxStatus(nValue, bIsDouble);
            }
            strInfo = strInfo + "\n";
            int YcCnt = TB[Offset++];//遥测个数
            int nDataType = TB[Offset++];//遥测类型
            switch (nDataType)
            {
                case 0x09:      //归一化
                case 0x0B:      //表度化
                    for (i = 0; i < YcCnt; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        NewAddr = StartAddr - m_YcStartAddress;
                        Offset += 2;
                        nValue = TB[Offset] + TB[Offset + 1] * 256;
                        Offset += 2;
                        //SetYcValue(NewAddr, (float)nValue, TB[offset]);暂时先不计算    
                        strInfo = strInfo + theDevice.m_YcList[NewAddr].m_YcName + "=" + nValue.ToString() + "  ";
                    }
                    break;
                case 0x0D:
                    for (i = 0; i < YcCnt; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        NewAddr = StartAddr - m_YcStartAddress;
                        Offset += 2;
                        Array.Copy(TB, Offset, fValueBuff, 0, 4);
                        fValue = BitConverter.ToSingle(fValueBuff, 0);
                        Offset += 4;
                        // SetYcValue(NewAddr, fValue, TB[offset]);;暂时先不计算    
                        strInfo = strInfo + theDevice.m_YcList[NewAddr].m_YcName + "=" + fValue.ToString() + "  ";
                    }
                    break;
            }
            theDevice.m_EventClass.AddRealMsg(strInfo);
            CGlobal.PostMessage(CGlobal.m_Device.m_MainFormHandle, UseMessage.MY_MSG_2, 0, 0);
        }
        private void Dowith_TerminalTime(byte[] pRecBuff)
        {
            if (btCot == 5)
            {
                string[] strWeek = { "一", "二", "三", "四", "五", "六", "日", "(未知)" };
                int nPlace = nDataPlace + 2;
                int nYear = 2000 + pRecBuff[nPlace + 6];
                int nMonth = pRecBuff[nPlace + 5];
                int nDay = pRecBuff[nPlace + 4] & 0x1F;
                int nHour = pRecBuff[nPlace + 3];
                int nMinute = pRecBuff[nPlace + 2];

                int nMillSec = pRecBuff[nPlace + 1] * 256 + pRecBuff[nPlace];

                int nSecond = Convert.ToInt32(nMillSec / 1000);
                nMillSec = nMillSec % 1000;

                int nWeek = pRecBuff[nPlace + 4] >> 5;
                if (nWeek == 0) nWeek = 7;

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
                CGlobal.ShowTipsInfo(strTerTime);
            }
        }
        private int DoWithData(byte[] pRecBuff, int nRecLeng)
        {
            int nIndPlace;
            int ind;
            nDataPlace = GetCtrlStatus(pRecBuff, ref btASDU, ref btLimit, ref btCot);
            Offset = nDataPlace;
            switch (btASDU)
            {
                case 70:  //初始化结束
                    return 4;
                case 0x64://总召应答
                    m_bCheckTimeOut = false;
                    if (btCot == 7)
                    {
                        //激活确认
                    }
                    else if (btCot == 10)
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
                    DoWithYC(pRecBuff);
                    break;
                case 42://故障事件
                    DoWithFaultEvent(pRecBuff);
                    break;
                case 45: //单点遥控返校
                case 46: //双点遥控返校
                    m_bCheckTimeOut = false;
                    DoWithYK(pRecBuff);
                    break;
                case 206://摇脉
                    DoWithYM(pRecBuff, nRecLeng, false); break;
                case 207://摇脉
                    DoWithYM(pRecBuff, nRecLeng, true); break;
                case 0x6E:
                    DowWithProVersion(pRecBuff);
                    break;
                case 200://切换定值区
                    Dowith_Switch_AeroNo(pRecBuff);
                    break;
                case 201://定值区号读取
                    Dowith_Read_AeroNo(pRecBuff);
                    break;
                case 0x6F:  //读定值参数 
                    m_bCheckTimeOut = false;
                    if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;                   
                    DoWithreadDZ(pRecBuff);
                    break;
                case 0x70:  //写定值 预置 固化
                    m_bCheckTimeOut = false;

                    if (btCot == 9)
                    {
                        return 0;
                    }
                    if (btCot == 7)
                    {
                        DoWithwriteDZ(pRecBuff);
                    }
                    else if (btCot == 0x47)
                    {
                        //激活终止  预置失败
                        if (theDevice.m_ParaHandle >= 0) CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_3, 0, 0);                     
                    }
                    break;
                case 202:  //读定值参数 202
                    m_bCheckTimeOut = false;
                    if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;
                    DoWithreadDZ(pRecBuff);
                    break;
                case 203:  //写定值 预置 固化
                    m_bCheckTimeOut = false;
                    if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;
                    if (btCot == 9)
                    {
                        return 0;
                    }
                    if (btCot == 7)
                    {
                        DoWithwriteDZ(pRecBuff);
                    }
                    else if (btCot == 0x47)
                    {
                        //激活终止 
                        CGlobal.PostMessage(theDevice.m_ParaHandle, UseMessage.MY_MSG_3, 0, 0);
                    }
                    break;
                case 0xD2:  //文件
                    m_bCheckTimeOut = false;
                    nIndPlace = nDataPlace + 3;
                    if (m_NowDownFileTask != null) m_NowDownFileTask.m_AnswerFlag = true;
                    ind = pRecBuff[nIndPlace];//操作标识

                    if ((ind == 2) && (btCot == 5))
                    {
                        DoWithreadDir(pRecBuff);//文件目录召唤确认
                    }
                    else if ((btCot == 7) && (ind == 4))
                    {
                        Dowithreadfile(pRecBuff);   //读文件激活确认
                    }
                    else if ((ind == 5) && (btCot == 5))
                    {
                        return Dowithreadfiledata(pRecBuff, nRecLeng);//读文件数据传输
                    }
                    else if ((ind == 8) && (btCot == 7))
                    {
                        DowithwritefileConform(pRecBuff);//写文件激活确认
                    }
                    else if ((ind == 10) && (btCot == 5))
                    {
                        DowithwritefileDataConform(pRecBuff);//写文件传输数据确认
                    }
                    break;
                case 0xD3://远程软件升级
                    m_bCheckTimeOut = false;
                    nIndPlace = nDataPlace + 2;
                    ind = pRecBuff[nIndPlace];//操作标识
                    if (m_NowDownFileTask != null) m_NowDownFileTask.m_AnswerFlag = true;
                    if ((btCot == 7) && ((ind & 0x80) != 0))  //升级启动确认
                    {

                    }
                    else if ((btCot == 7) && ((ind & 0x80) == 0))  //升级结束确认
                    {

                        CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_4, 0, 0);
                    }
                    else if ((btCot == 9) && ((ind & 0x80) == 0))
                    {

                        CGlobal.PostMessage(theDevice.m_UpdataFormHandle, UseMessage.MY_MSG_5, 0, 0);
                    }
                    break;
                case 0x71://校验
                    if (m_NowDownFileTask != null) m_NowDownFileTask.m_AnswerFlag = true;
                    m_bCheckTimeOut = false;
                    if (btCot == 7)
                    {
                        DowithCheckData(pRecBuff);
                    }
                    break;
                case 0x72://系数
                    if (m_NowDownFileTask != null) m_NowDownFileTask.m_AnswerFlag = true;
                    m_bCheckTimeOut = false;
                    if (btCot == 0x05)
                    {
                        DowithReadRatioData(pRecBuff);
                    }
                    else if (btCot == 0x07)
                    {
                        DowithDownRatioData(pRecBuff);
                    }
                    break;
                case 0x73://绝对相角
                    if (m_NowDownFileTask != null) m_NowDownFileTask.m_AnswerFlag = true;
                    m_bCheckTimeOut = false;
                    DowithPhaseData(pRecBuff);
                    break;


                case 0x85:
                    m_bCheckTimeOut = false;
                    DoWithRead4G_Para(pRecBuff);
                    break;              
                case 0x87:
                    m_bCheckTimeOut = false;
                    DoWithReadIP_Para(pRecBuff);
                    break;
                case (int)TI_Type.R_MC_PA_1:
                    m_bCheckTimeOut = false;
                    DoWithReadMac_Para(pRecBuff);
                    break;
                case 0x88:  //写IP参数回复,不需要发送重启
                case 0x8a:  //写网关回复
                case 0x8c:  //写串口回复
                case (int)TI_Type.W_MC_PA_1:
                
                    m_bCheckTimeOut = false;
                    if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;
                    if (pRecBuff[9] == 7) //参数设置成功
                    {
                        if (m_NowParaTask.m_EndFlag == true)
                            CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_9, 0, 0);
                    }
                    else if (pRecBuff[9] == 0x47)
                    {
                        CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_7, 0, 0);
                    }
                    break;
                case 0x86://4G参数设置回复
                case 0x8e://主站参数设置回复
				case (int)TI_Type.W_TI_PA_1:
                    m_bCheckTimeOut = false;
                    if (m_NowParaTask != null) m_NowParaTask.m_AnswerFlag = true;

                    if (pRecBuff[9] == 7) //参数设置成功
                    {
                        if (theDevice.m_SpeParaFormHandle > 0)
                        {
                            CGlobal.PostMessage(theDevice.m_SpeParaFormHandle, UseMessage.MY_MSG_2, 0, 0);
                        }
                        if (m_NowParaTask != null)
                        {
                            if (m_NowParaTask.m_EndFlag == true)
                                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_8, 0, 0);
                        }
                    }
                    else if (pRecBuff[9] == 0x47)
                    {
                        CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_7, 0, 0);
                    }
                    break;
                case 0x89:
                    m_bCheckTimeOut = false;
                    DoWithReadGW_Para(pRecBuff);
                    break;
                case 0x8b:
                    m_bCheckTimeOut = false;
                    DoWithReadCom_Para(pRecBuff);
                    break;
                case 0x8d:
                    m_bCheckTimeOut = false;
                    DoWithReadHost_Para(pRecBuff);
                    break;
                case (int)TI_Type.R_TI_PA_1:
                    m_bCheckTimeOut = false;
                    DoWithReadDev_Para(pRecBuff);
                    break;

            }

            return 0;
        }
        private void DoWithYM(byte[] TB, int tLen, bool bSoeFlag)
        {
            int i = 0;
            int NewAddr;
            int StartAddr;
            byte[] fValueBuff = new byte[4];
            float fValue;
            string strTimeMsg;
            Byte bEnable;

            if ((btLimit & 0x80) == 0x80) IsContinue = true;
            DataCount = btLimit & 0x7F;
            Offset = nDataPlace;

            if (bSoeFlag)
            {
                if (IsContinue == true)
                {
                    StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                    Offset = Offset + 2;
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
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        NewAddr = StartAddr - m_PowerStartAddress;
                        Offset = Offset + 2;
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
                    StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                    Offset = Offset + 2;
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
                    for (i = 0; i < DataCount; i++)
                    {
                        StartAddr = TB[Offset] + TB[Offset + 1] * 256;
                        NewAddr = StartAddr - m_PowerStartAddress;
                        Offset = Offset + 2;
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
        private void DoWithReadCom_Para(byte[] TB)
        {
            int i = 0;

            byte[] intbuf = new byte[4];
            byte con_byte;
            con_byte = TB[Offset];
            Offset++;

            DataCount = btLimit & 0x7F;                                     //数量

            for (i = 0; i < DataCount; i++)
            {
                CComModule RevCom = new CComModule();

                RevCom.ComPort = TB[Offset];
                Offset++;

                Array.Copy(TB, Offset, intbuf, 0, 4);
                RevCom.ComBaud = BitConverter.ToInt32(intbuf, 0);


                Offset = Offset + 4;
                CGlobal.m_GComList.Add(RevCom);
            }

            if (con_byte == 0)        //无后续帧
            {
                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_4, 0, 0);
                return;
            }


            if (btCot == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值异常", "提示");
                return;
            }
            else if (btCot == 7)
            {
                //终端激活确认
                //不处理
            }
        }
        private void DoWithReadHost_Para(byte[] TB)
        {
            int i = 0;
            byte con_byte;
            byte[] intbuf = new byte[4];

            DataCount = btLimit & 0x7F;                                     //数量

            con_byte = TB[Offset];
            Offset++;

            for (i = 0; i < DataCount; i++)
            {
                CHostModule RevHost = new CHostModule();

                RevHost.m_HostIndex = TB[Offset];
                Offset++;

                RevHost.m_address = TB[Offset];
                Offset++;

                //RevHost.m_HostType = TB[Offset];
                //Offset++;

                RevHost.m_ChanType = TB[Offset];
                Offset++;

                RevHost.m_PrtclType = TB[Offset];
                Offset++;

                Array.Copy(TB, Offset, intbuf, 0, 4);
                RevHost.LocalPort = BitConverter.ToInt32(intbuf, 0);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, intbuf, 0, 4);
                RevHost.RemotePort = BitConverter.ToInt32(intbuf, 0);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_LocalIP_byte1, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_LocalIP_byte2, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_LocalIP_byte3, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_RemoteIP_byte1, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_RemoteIP_byte2, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_RemoteIP_byte3, 0, 4);
                Offset = Offset + 4;

                CGlobal.m_GHostList.Add(RevHost);
            }

            if (con_byte == 0)        //无后续帧
            {
                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_5, 0, 0);
                return;
            }

            if (btCot == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值异常", "提示");
                return;
            }
            else if (btCot == 7)
            {
                //终端激活确认
                //不处理
            }
        }
        private void DoWithReadDev_Para(byte[] TB)
        {
            int i = 0;
            byte con_byte;
            byte[] intbuf = new byte[4];

            DataCount = btLimit & 0x7F;                                     //数量

            con_byte = TB[Offset];
            Offset++;

            for (i = 0; i < DataCount; i++)
            {
                CHostModule RevHost = new CHostModule();

                RevHost.m_HostIndex = TB[Offset];
                Offset++;

                RevHost.m_address = TB[Offset];
                Offset++;

                //RevHost.m_HostType = TB[Offset];
                //Offset++;

                RevHost.m_ChanType = TB[Offset];
                Offset++;

                RevHost.m_PrtclType = TB[Offset];
                Offset++;

                Array.Copy(TB, Offset, intbuf, 0, 4);
                RevHost.LocalPort = BitConverter.ToInt32(intbuf, 0);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, intbuf, 0, 4);
                RevHost.RemotePort = BitConverter.ToInt32(intbuf, 0);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_LocalIP_byte1, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_LocalIP_byte2, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_LocalIP_byte3, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_RemoteIP_byte1, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_RemoteIP_byte2, 0, 4);
                Offset = Offset + 4;

                Array.Copy(TB, Offset, RevHost.m_RemoteIP_byte3, 0, 4);
                Offset = Offset + 4;

                CGlobal.m_GDevList.Add(RevHost);
            }

            if (con_byte == 0)        //无后续帧
            {
                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_10, 0, 0);
                return;
            }

            if (btCot == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值异常", "提示");
                return;
            }
            else if (btCot == 7)
            {
                //终端激活确认
                //不处理
            }
        }
        private void DoWithReadIP_Para(byte[] TB)
        {
            int i = 0;

            byte con_byte;
            con_byte = TB[Offset];
            Offset++;

            DataCount = btLimit & 0x7F;                                     //数量

            for (i = 0; i < DataCount; i++)
            {
                CIPModule RevIP = new CIPModule();
                RevIP.NetPort = TB[Offset];
                Offset = Offset + 1;
                Array.Copy(TB, Offset, RevIP.m_IP_byte, 0, 4);
                Offset = Offset + 4;
                Array.Copy(TB, Offset, RevIP.m_SubnetMask_byte, 0, 4);
                Offset = Offset + 4;
                CGlobal.m_IPGroup.Add(RevIP);

            }

            if (con_byte == 0)        //无后续帧
            {
                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_2, 0, 0);
                return;
            }


            if (btCot == 47)
            {
                //终端激活终止  开始显示
                // MessageBox.Show("读定值异常", "提示");
                return;
            }
            else if (btCot == 7)
            {
                //终端激活确认
                //不处理
            }

        }
        private void DoWithReadMac_Para(byte[] TB)
        {
            int i = 0;
            byte con_byte;
            con_byte = TB[Offset];
            Offset++;


            DataCount = btLimit & 0x7F;         //数量

            for (i = 0; i < DataCount; i++)
            {
                CMacModule RevMac = new CMacModule();

                RevMac.Port = TB[Offset];
                Offset = Offset + 1;
                Array.Copy(TB, Offset, RevMac.m_Mac_byte, 0, 6);
                Offset = Offset + 6;
                //String.Format("{0:X2}", pBuffer[i]);
                //RevMac.m_MacString = Convert.ToString(RevMac.m_Mac_byte[0], 16) + ":" + Convert.ToString(RevMac.m_Mac_byte[1], 16) + ":" + Convert.ToString(RevMac.m_Mac_byte[2], 16) + ":" + Convert.ToString(RevMac.m_Mac_byte[3], 16) + ":" + Convert.ToString(RevMac.m_Mac_byte[4], 16) + ":" + Convert.ToString(RevMac.m_Mac_byte[5], 16);
                RevMac.m_MacString = String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}", RevMac.m_Mac_byte[0], RevMac.m_Mac_byte[1], RevMac.m_Mac_byte[2], RevMac.m_Mac_byte[3], RevMac.m_Mac_byte[4], RevMac.m_Mac_byte[5]);
                CGlobal.m_MacGroup.Add(RevMac);

            }

            if (con_byte == 0) //无后续帧
            {
                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_11, 0, 0);
                return;
            }


            if (btCot == 47)
            {
                //终端激活终止  开始显示
                MessageBox.Show("读定值异常", "提示");
                return;
            }

        }
        private void DoWithReadGW_Para(byte[] TB)
        {
            int i = 0;
            byte con_byte;
            con_byte = TB[Offset];
            Offset++;


            DataCount = btLimit & 0x7F;         //数量

            for (i = 0; i < DataCount; i++)
            {
                CGateWayModule RevGW = new CGateWayModule();

                RevGW.NetPort = TB[Offset];
                Offset = Offset + 1;
                Array.Copy(TB, Offset, RevGW.m_NetSegment_byte, 0, 4);
                Offset = Offset + 4;
                Array.Copy(TB, Offset, RevGW.m_SubnetMask_byte, 0, 4);
                Offset = Offset + 4;
                Array.Copy(TB, Offset, RevGW.m_GateWay_byte, 0, 4);
                Offset = Offset + 4;
                CGlobal.m_GateWayGroup.Add(RevGW);

            }

            if (con_byte == 0)        //无后续帧
            {
                CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_3, 0, 0);
                return;
            }


            if (btCot == 47)
            {
                //终端激活终止  开始显示
                MessageBox.Show("读定值异常", "提示");
                return;
            }

        }
        private void DoWithRead4G_Para(byte[] TB)
        {            
            try
            {
                string str = "";
                int Addr;
                byte tag, Len, con_byte;

                DataCount = btLimit & 0x7F;         //数量

                con_byte = TB[Offset]; //参数特征标识(1)
                Offset = Offset + 1;    //第一个信息体地址offset

                for (int j = 0; j < DataCount; j++)
                {
                    try
                    {
                        Addr = TB[Offset] + (TB[Offset + 1] << 8);//地址
                        Offset += 2;
                        tag = TB[Offset++];//tag类型
                        Len = TB[Offset++];//数据长度;
                        if ((tag == 1) && (Len == 1))//bool
                        {
                            byte data = new byte();
                            data = TB[Offset];

                            if (TB[Offset] == 1)
                                str = "1";
                            else
                                str = "0";
                        }
                        else if ((tag == 43) && (Len == 1))//小整形 char
                        {
                            byte data = new byte();
                            data = TB[Offset];

                            str = (Convert.ToInt16(TB[Offset])).ToString();
                        }
                        else if ((tag == 32) && (Len == 1))//无符号小整形   unsigned char
                        {
                            byte data = new byte();
                            data = TB[Offset];
                            str = TB[Offset].ToString();

                        }
                        else if ((tag == 33) && (Len == 2))//short
                        {
                            byte[] data = new byte[2];
                            Array.Copy(TB, Offset, data, 0, 2);
                            str = (BitConverter.ToInt16(data, 0)).ToString();//有符号

                        }
                        else if ((tag == 45) && (Len == 2))//ushort
                        {
                            byte[] data = new byte[2];
                            Array.Copy(TB, Offset, data, 0, 2);
                            str = (BitConverter.ToUInt16(data, 0)).ToString();//无符号

                        }
                        else if ((tag == 2) && (Len == 4))//整形
                        {
                            byte[] data = new byte[4];
                            Array.Copy(TB, Offset, data, 0, 4);
                            str = (BitConverter.ToInt32(data, 0)).ToString();

                        }
                        else if ((tag == 35) && (Len == 4))//无符号整形
                        {
                            byte[] data = new byte[4];
                            Array.Copy(TB, Offset, data, 0, 4);
                            str = (BitConverter.ToUInt32(data, 0)).ToString();

                        }
                        else if ((tag == 36) && (Len == 8))//长整形
                        {
                            byte[] data = new byte[8];
                            Array.Copy(TB, Offset, data, 0, 8);
                            str = (BitConverter.ToInt64(data, 0)).ToString();

                        }
                        else if ((tag == 37) && (Len == 8))//无符号长整形
                        {
                            byte[] data = new byte[8];
                            Array.Copy(TB, Offset, data, 0, 8);
                            str = (BitConverter.ToUInt64(data, 0)).ToString();

                        }
                        else if ((tag == 38) && (Len == 4))//单精度浮点
                        {
                            byte[] data = new byte[4];
                            Array.Copy(TB, Offset, data, 0, 4);
                            str = (BitConverter.ToSingle(data, 0)).ToString();

                        }
                        else if ((tag == 39) && (Len == 8))//双精度浮点
                        {
                            byte[] data = new byte[8];
                            Array.Copy(TB, Offset, data, 0, 8);
                            str = (BitConverter.ToSingle(data, 0)).ToString();

                        }
                        else if (tag == 4)
                        {
                            byte[] data = new byte[Len];
                            Array.Copy(TB, Offset, data, 0, Len);
                            str = CGlobal.BytesToString(data);
                        }
                        Offset += Len;

                        if (Addr == 0x004)
                        {
                            theDevice.m_DevID = str;
                            CGlobal.PostMessage(theDevice.m_SpeParaFormHandle, UseMessage.MY_MSG_1, 0, 0);
                        }
                        theDevice.Set4GParaValue(Addr, str);

                    }
                    catch
                    {

                    }
                }



                if (con_byte == 0)
                {
                    CGlobal.PostMessage(theDevice.m_CommuCFGFormHandle, UseMessage4G.MY_MSG_1, 0, 0);
                    return;
                }


                if (btCot == 47)
                {
                    //终端激活终止  开始显示
                    // MessageBox.Show("读定值异常", "提示");
                    return;
                }
                else if (btCot == 7)
                {
                    //终端激活确认
                    //不处理
                }
                // if ((0x01 & nType) == 0) theDevice.m_b4GParaFresh = true;       //无后续的时候 刷新

            }
            catch
            { }
        }
        private int SetAddress(byte[] SendB, int nPlace, int nValue)
        {
            if (m_AddressType == 0)
            {
                SendB[nPlace] = Convert.ToByte(nValue);
            }
            else
            {
                SendB[nPlace++] = Convert.ToByte(nValue % 256);
                SendB[nPlace++] = Convert.ToByte(nValue / 256);
            }
            return GetAddressLeng();
        }
        private int SetCot(byte[] SendB, int nPlace, int nValue)
        {
            if (m_AddressType == 0)
            {
                SendB[nPlace] = Convert.ToByte(nValue);
            }
            else
            {
                SendB[nPlace++] = Convert.ToByte(nValue % 256);
                SendB[nPlace++] = Convert.ToByte(nValue / 256);
            }
            return GetCotLeng();
        }
        public int Send_Stardard_101Report(byte[] SendB, int FrameLen)
        {
            int nPlace = 0;
            nPlace = GetAddressLeng() + 5;
            Array.Copy(SendB, 0, SendB, nPlace, FrameLen);

            nPlace = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x68;
            SendB[nPlace++] = GetFCB();
            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            nPlace += FrameLen;
            SendB[1] = (byte)(nPlace - 4);
            SendB[2] = SendB[1];
            SendB[nPlace] = CGlobal.GetCS(SendB, 4, nPlace - 4);
            nPlace++;
            SendB[nPlace++] = 0x16;
            return nPlace;
        }
        private int Send_Short_Report(byte iType, byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x10;
            SendB[nPlace++] = iType;
            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            SendB[nPlace] = CGlobal.GetCS(SendB, 1, nPlace - 1);
            nPlace++;
            SendB[nPlace++] = 0x16;
            return nPlace;
        }
        private int Send_UpdateOrder(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 220; //类型
            SendB[nPlace++] = 0;

            nPlace += SetCot(SendB, nPlace, 6);
            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 2; //文件传输

            SendB[nPlace++] = 7;//操作标识
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int SendCallPowerData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x65; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x5;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int SendGetFrzData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x65; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x45;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int SendCallTotalData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x64; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x14;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_CallSync_Time(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 103; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 5);

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

            SendB[nPlace++] = Convert.ToByte(t % 256);
            SendB[nPlace++] = Convert.ToByte(t / 256);
            SendB[nPlace++] = Convert.ToByte(dt.Minute);
            SendB[nPlace++] = Convert.ToByte(dt.Hour);
            SendB[nPlace++] = Convert.ToByte(dt.Day + pWeek);
            SendB[nPlace++] = Convert.ToByte(dt.Month);
            SendB[nPlace++] = Convert.ToByte((dt.Year - 2000) % 100);
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Sync_Time(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 103; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

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

            SendB[nPlace++] = Convert.ToByte(t % 256);
            SendB[nPlace++] = Convert.ToByte(t / 256);
            SendB[nPlace++] = Convert.ToByte(dt.Minute);
            SendB[nPlace++] = Convert.ToByte(dt.Hour);
            SendB[nPlace++] = Convert.ToByte(dt.Day + pWeek);
            SendB[nPlace++] = Convert.ToByte(dt.Month);
            SendB[nPlace++] = Convert.ToByte((dt.Year - 2000) % 100);
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_YK_Cmd(byte[] SendB)
        {
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
                nPlace += SetCot(SendB, nPlace, 8);
            }
            else
            {
                nPlace += SetCot(SendB, nPlace, 6);
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

            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_ReadAll_Para(byte[] SendB)
        {

            int nPlace = 0;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCA; //类型202
            }
            else
            {
                SendB[nPlace++] = 0x6F; //类型
            }

            SendB[nPlace++] = 0;


            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_Para(byte[] SendB)
        {
            if (m_NowParaTask == null) return 0;

            int nPlace = 0;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCA; //类型202
            }
            else
            {
                SendB[nPlace++] = 0x6F; //类型
            }

            SendB[nPlace++] = Convert.ToByte(m_NowParaTask.m_ParaCount);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);

            for (int i = 0; i < m_NowParaTask.m_ParaCount; i++)
            {
                int nAddress = theDevice.m_SelectParaList[i + m_NowParaTask.m_StartPalce].m_ParaAddress;
                //if (CheckParaStatus(Is_PriPara, nAddress)) break;
                SendB[nPlace++] = Convert.ToByte(nAddress % 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 256);
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Preset_Para(byte[] SendB)
        {
            if (m_NowParaTask == null) return 0;
            int nPlace = 0;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCB; //类型
            }
            else
            {
                SendB[nPlace++] = 0x70; //类型
            }

            SendB[nPlace++] = Convert.ToByte(m_NowParaTask.m_ParaCount);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);


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

                SendB[nPlace++] = Convert.ToByte(pPara.m_ParaType);//tag类型
                SendB[nPlace++] = Convert.ToByte(pPara.m_ParaLeng);//数据长度

                Byte[] pValueByte = null;
                int nLength = pPara.GetValueBuff(ref pValueByte);
                for (int j = 0; j < nLength; j++) SendB[nPlace++] = pValueByte[j];
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_Para(byte[] SendB)
        {
            int nPlace = 0;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCB; //类型
            }
            else
            {
                SendB[nPlace++] = 0x70; //类型
            }

            SendB[nPlace++] = 0x0;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);


            SendB[nPlace++] = 0;//特征标识
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_CancelWrite_Para(byte[] SendB)
        {
            int nPlace = 0;
            if (Is_PriPara == false)
            {
                SendB[nPlace++] = 0xCB; //类型
            }
            else
            {
                SendB[nPlace++] = 0x70; //类型
            }

            SendB[nPlace++] = 0x0;

            nPlace += SetCot(SendB, nPlace, 8);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);


            SendB[nPlace++] = 0;//特征标识
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_Dir(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xD2; //类型
            SendB[nPlace++] = 0x00;

            nPlace += SetCot(SendB, nPlace, 5);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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

            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_File_Active(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xD2; //类型
            SendB[nPlace++] = 0x00;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_FileData_Conform(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xD2; //类型
            SendB[nPlace++] = 0x00;

            nPlace += SetCot(SendB, nPlace, 5);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_DC_CheckData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x71; //类型
            SendB[nPlace++] = 0x2;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_CheckData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x71; //类型
            SendB[nPlace++] = 0x12;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_RatioData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x72; //类型
            SendB[nPlace++] = 0x12;

            nPlace += SetCot(SendB, nPlace, 5);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Down_RatioData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x72; //类型
            SendB[nPlace++] = 0x12;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Call_Phase(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x73; //类型
            SendB[nPlace++] = 0x06;

            nPlace += SetCot(SendB, nPlace, 5);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.SelLineNo); //线路编号           
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_WriteFileActive(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 210; //类型
            SendB[nPlace++] = 0;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_WriteFileData(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 210; //类型
            SendB[nPlace++] = 0;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

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

            theDevice.m_TransFile.CurrentIndex++;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Pro_UpdateOrder(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 211; //类型
            SendB[nPlace++] = 0;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0x80; //命令类型        
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Pro_UpdateEndOrder(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 211; //类型
            SendB[nPlace++] = 0;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x00; //命令类型        
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Pro_UpdateCancelOrder(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 211; //类型
            SendB[nPlace++] = 0;

            nPlace += SetCot(SendB, nPlace, 8);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;

            SendB[nPlace++] = 0x00; //命令类型        
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_AeroNo(byte[] SendB)
        {

            int nPlace = 0;
            SendB[nPlace++] = 201; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Change_AeroNo(byte[] SendB)
        {

            int nPlace = 0;
            SendB[nPlace++] = 200; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo % 256);
            SendB[nPlace++] = Convert.ToByte(m_ParaAreaNo / 256);
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private bool CheckRecBuff(ref byte[] pRecBuff, ref int nRecLeng)
        {
            if (pRecBuff[0] == 0x68)
            {
                IsShortFrame = false;
            }
            else
            {
                IsShortFrame = true;
            }
            if (IsShortFrame == false)
            {
                if (pRecBuff[1] != pRecBuff[2]) return false;
                if (pRecBuff[3] != 0x68) return false;
                UInt32 nRealLeng = Convert.ToUInt32(pRecBuff[1] + 6);
                if (nRealLeng > nRecLeng) return false;
                if (pRecBuff[nRealLeng - 1] != 0x16) return false;
                if (pRecBuff[nRealLeng - 2] != CGlobal.GetCS(pRecBuff, 4, pRecBuff[1])) return false;
            }
            else
            {
                int nRealLeng = 4 + GetAddressLeng();
                if (nRealLeng > nRecLeng) return false;
                if (pRecBuff[nRealLeng - 1] != 0x16) return false;

                if (pRecBuff[nRealLeng - 2] != CGlobal.GetCS(pRecBuff, 1, nRealLeng - 3)) return false;
            }
            return true;
        }
        private int SendBackByCtl(byte CtlCode)
        {
            switch (CtlCode)
            {
                case 0xC9:
                    return 40;
                case 0xC0:
                case 0xC2:
                    return 20;
            }
            int GNCode = 0x0f & CtlCode;
            switch (GNCode)
            {
                case 0://确认
                    NeedResend = false;
                    m_StartFlag = true;
                    if (CGlobal.m_Device.m_Ptr.m_IsUpdateFlag)
                    {
                        m_bCheckTimeOut = false;
                        if (m_NowDownFileTask != null) m_NowDownFileTask.m_AnswerFlag = true;
                    }
                    break;
                case 1: //链路忙
                    // if (NeedResend)
                    // SendReport(ReSendB,ReSendBLen);
                    break;
                case 3:
                case 4://传送数据确认或否认
                    return 20;
                case 8://有数据
                    NeedResend = false;
                    return 20;
                case 9://无数据
                    NeedResend = false;
                    return 20;
                case 11://链路状态
                    NeedResend = false;
                    return 3;
                default:
                    break;
            }
            return 0;
        }
        private void CheckHeartTimeOut()
        {
            if (m_bHeartOnlie)
            {
                TimeSpan spanTime = DateTime.Now - m_HeartTime;
                if (spanTime.Seconds >= 30)
                {
                    CGlobal.PostMessage(theDevice.m_MainFormHandle, UseMessage.MY_MSG_5, 0, 0);
                    m_bHeartOnlie = false;
                }
            }
        }
        public override void InitPara()
        {
            base.InitPara();
        }
        public override int GetTheRightBuffContent(Byte[] pRecBuff, int nBuffLeng, Byte[] pGetBuff, ref int nGetLeng)
        {

            int nRetLeng = nBuffLeng;
            if (theDevice.m_Connection.IsEncode)
            {
                nRetLeng = base.GetTheRightBuffContent(pRecBuff, nBuffLeng, pGetBuff, ref nGetLeng);
            }
            else
            {
                int nMinLeng = 0;
                if (m_AddressType == 0)
                {
                    nMinLeng = 5;
                }
                else
                {
                    nMinLeng = 6;
                }
                if (nBuffLeng < nMinLeng) return 0;    ///最小长度为

                int nDataLeng = 0;
                for (int i = 0; i < (nBuffLeng - 1); i++)
                {
                    if (pRecBuff[i] == 0x68)
                    {
                        if ((nBuffLeng - i) < 2) return i;
                        IsShortFrame = false;
                        nDataLeng = pRecBuff[i + 1] + 6;
                        if ((nDataLeng + i) > nBuffLeng) return i;

                        if (pRecBuff[i + nDataLeng - 1] == 0x16)
                        {
                            nGetLeng = nDataLeng;
                            Array.Copy(pRecBuff, i, pGetBuff, 0, nGetLeng);
                            nRetLeng = nGetLeng + i;
                        }
                        else
                        {
                            nRetLeng = i + 1;
                        }
                        break;
                    }
                    else if (pRecBuff[i] == 0x10)
                    {
                        if ((nBuffLeng - i) < 2) return i;
                        IsShortFrame = true;
                        nDataLeng = nMinLeng;
                        if ((nDataLeng + i) > nBuffLeng) return i;

                        if (pRecBuff[i + nMinLeng - 1] == 0x16)
                        {
                            nGetLeng = nDataLeng;
                            Array.Copy(pRecBuff, i, pGetBuff, 0, nGetLeng);
                            nRetLeng = nGetLeng + i;
                        }
                        else
                        {
                            nRetLeng = i + 1;
                        }
                        break;
                    }
                }
            }
            return nRetLeng;
        }
        public override void GetTestOrSBuff(Byte[] pSendBuff, ref int pSendLeng)  //心跳
        {
            if (m_StartFlag == false) return;

            if (pSendLeng == 0)
            {
                TimeSpan spanTime = DateTime.Now - m_HeartTime;
                int m_Sec = spanTime.Minutes * 60 + spanTime.Seconds;
                if (m_Sec >= 180)
                {
                    pSendLeng = GetTaskBuffer(pSendBuff, 2);
                    m_HeartTime = DateTime.Now;
                }
            }

        }
        public override void Process(byte[] pRecvBuff, ref int pRecvLeng, byte[] pSendBuff, ref int pSendLeng)
        {
            pSendLeng = 0;
            if (CGlobal.m_Device.m_Connection.IsEncode == true)
            {
                base.Process(pRecvBuff, ref pRecvLeng, pSendBuff, ref pSendLeng);
            }
            int nRealLeng;
            if (pRecvLeng > 0)
            {
                m_HeartTime = DateTime.Now;
                if (!CheckRecBuff(ref pRecvBuff, ref  pRecvLeng)) return;
                if (IsShortFrame == false)
                {
                    nRealLeng = pRecvBuff[1] + 6;
                    int nTaskNo = SendBackByCtl(pRecvBuff[4]);
                    if (nTaskNo > 0)
                    {
                        CGlobal.SendTaskOrder(nTaskNo);
                        // pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                    }
                    nTaskNo = DoWithData(pRecvBuff, nRealLeng);
                    if (nTaskNo > 0)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                    }
                }
                else
                {
                    nRealLeng = 6;
                    int nTaskNo = SendBackByCtl(pRecvBuff[1]);
                    if (nTaskNo > 0)
                    {
                        pSendLeng = GetTaskBuffer(pSendBuff, nTaskNo);
                    }
                }
            }
            else
            {
                CheckTimeOut();
                CheckHeartTimeOut();
            }
        }
        public override string GetBuffContent(byte[] pBuffer)
        {
            string strOut = "";
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
            string SendYY = "";
            string temp = "";
            int nPlace = 0;
            byte[] data = new byte[4];
            if (pBuffer[0] == 0x10)
            {
                strOut += String.Format("{0:X2}", pBuffer[0]) + "   " + "启动符" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[1]) + "   " + "控制域" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[2]) + " " + String.Format("{0:X2}", pBuffer[3]) + "   " + "链路地址" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[4]) + "   " + "校验和" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[5]) + "   " + "结束符" + "\r\n";
            }
            else
            {

                strOut += String.Format("{0:X2}", pBuffer[0]) + " " + String.Format("{0:X2}", pBuffer[1]) + " " + String.Format("{0:X2}", pBuffer[2]) + " " + String.Format("{0:X2}", pBuffer[3]) + "   " + "启动符，长度:" + pBuffer[1].ToString() + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[4]) + "   " + "控制域" + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[5]) + " " + String.Format("{0:X2}", pBuffer[6]) + "   " + "链路地址" + "\r\n";
                #region //类型
                switch (pBuffer[7])
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
                strOut += String.Format("{0:X2}", pBuffer[7]) + "   " + "类型标识：" + pBuffer[7].ToString() + temp + "\r\n";
                int Infonum = pBuffer[8] % 0x80;
                int SQ = pBuffer[8] / 0x80;
                if (SQ == 0)
                {
                    temp = "非顺序";
                }
                else
                {
                    temp = "顺序";
                }
                strOut += String.Format("{0:X2}", pBuffer[8]) + "   " + "可变结构限定词VSQ---信息数目=" + (Infonum).ToString() + "SQ:" + (SQ).ToString() + temp + "\r\n";
                #endregion
                #region //传送原因
                switch (pBuffer[9])
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
                strOut += String.Format("{0:X2}", pBuffer[9]) + " " + String.Format("{0:X2}", pBuffer[10]) + "   " + "传送原因:" + SendYY + "\r\n";
                strOut += String.Format("{0:X2}", pBuffer[11]) + " " + String.Format("{0:X2}", pBuffer[12]) + "   " + "公共地址" + "低前，高后" + "\r\n";
                nPlace = 13;
                #region //具体类型的解析
                switch (pBuffer[7])
                {
                    case 70:
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息体地址" + "\r\n";
                        nPlace = nPlace + 2;
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "初始化原因" + "" + "\r\n";
                        break;
                    case 100:
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息体地址" + "\r\n";
                        nPlace = nPlace + 2;
                        strOut += String.Format("{0:X2}", pBuffer[nPlace]) + "召唤限定词" + "" + "\r\n";
                        break;
                    case 1:
                        if (SQ == 0)
                        {
                            for (int i = 0; i < Infonum; i++)
                            {
                                strOut += "------数据" + (i + 1).ToString() + "------" + "\r\n";
                                temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息对象首地址:" + temp + "\r\n";
                                nPlace = nPlace + 2;
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
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息对象首地址:" + temp + "\r\n";
                            nPlace = nPlace + 2;
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
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息对象首地址:" + temp + "\r\n";
                                nPlace = nPlace + 2;
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
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息对象首地址:" + temp + "\r\n";
                            nPlace = nPlace + 2;
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
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息体地址：" + temp + "\r\n";
                                nPlace = nPlace + 2;
                                Array.Copy(pBuffer, nPlace, data, 0, 4);
                                temp = BitConverter.ToSingle(data, 0).ToString();
                                strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 2]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 3]) + "遥测值:" + temp + "\r\n";
                                nPlace = nPlace + 5;
                            }
                        }
                        else
                        {
                            temp = (pBuffer[nPlace] + pBuffer[nPlace + 1] * 256 + pBuffer[nPlace + 2] * 256 * 256).ToString();
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息对象首地址:" + temp + "\r\n";
                            nPlace = nPlace + 2;
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
                            strOut += String.Format("{0:X2}", pBuffer[nPlace]) + " " + String.Format("{0:X2}", pBuffer[nPlace + 1]) + "信息体地址：" + temp + "\r\n";
                            nPlace = nPlace + 2;

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
                                strOut += "值:" + temp + "\r\n"; ;
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
            return "";
        }
        private int Send_Write_4GPara(byte[] SendB)
        {
            if (m_NowParaTask == null) return 0;

            int nPlace = 0;

            SendB[nPlace++] = (int)TI_Type.W_4G_PA_1; //类型

            SendB[nPlace++] = Convert.ToByte(m_NowParaTask.m_ParaCount);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            byte[] data = new byte[10];
            try
            {
                for (int i = 0; i < m_NowParaTask.m_ParaCount; i++)
                {
                    CParaCtrl4G pPara = theDevice.m_SelectParaList4G[i + m_NowParaTask.m_StartPalce];
                    int nAddress = pPara.m_ParaAddress;
                    //if (CheckParaStatus(Is_PriPara, nAddress)) break;
                    SendB[nPlace++] = Convert.ToByte(nAddress % 256);
                    SendB[nPlace++] = Convert.ToByte(nAddress / 256);

                    SendB[nPlace++] = Convert.ToByte(pPara.m_ProParaType);//tag类型
                    SendB[nPlace++] = Convert.ToByte(pPara.m_ParaLeng);//数据长度

                    Byte[] pValueByte = null;
                    int nLength = pPara.GetValueBuff(ref pValueByte);
                    for (int j = 0; j < nLength; j++) SendB[nPlace++] = pValueByte[j];
                }
                return Send_Stardard_101Report(SendB, nPlace);
            }
            catch
            {
                MessageBox.Show("参数输入异常", "提示");
                return 0;
            }
        }
        private int Send_Read_4GPara(byte[] SendB)
        {
            if (m_NowParaTask == null) return 0;

            int nPlace = 0;
            SendB[nPlace++] = (byte)TI_Type.R_4G_PA_1; //类型
            SendB[nPlace++] = Convert.ToByte(m_NowParaTask.m_ParaCount);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            for (int i = 0; i < m_NowParaTask.m_ParaCount; i++)
            {
                int nAddress = theDevice.m_SelectParaList4G[i + m_NowParaTask.m_StartPalce].m_ParaAddress;
                SendB[nPlace++] = Convert.ToByte(nAddress % 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 256);
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_IPPara(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = (int)TI_Type.R_NT_PA_1; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_MacPara(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = (int)TI_Type.R_MC_PA_1; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_MacPara(byte[] SendB)
        {
            int i = 0;
            int j = 0;
            int nPlace = 0;
            int startAddr = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_StartPalce;
            int count = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount;

            SendB[nPlace++] = (byte)TI_Type.W_MC_PA_1; //类型

            SendB[nPlace++] = (byte)(count);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            for (i = 0; i < count; i++)
            {
                j = startAddr + i;

                SendB[nPlace++] = (byte)CGlobal.m_MacGroup[j].Port;
                Array.Copy(CGlobal.m_MacGroup[j].m_Mac_byte, 0, SendB, nPlace, 6);
                nPlace = nPlace + 6;
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_IPPara(byte[] SendB)
        {
            int i = 0;
            int j = 0;
            int nPlace = 0;
            int startAddr = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_StartPalce;
            int count = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount;

            SendB[nPlace++] = (byte)TI_Type.W_NT_PA_1; //类型

            SendB[nPlace++] = (byte)(count);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            //SendB[nPlace++] = Convert.ToByte(CGlobal.m_IPGroup0.Count);
            for (i = 0; i < count; i++)
            {
                j = i + startAddr;
                SendB[nPlace++] = (byte)CGlobal.m_IPGroup[j].NetPort;
                Array.Copy(CGlobal.m_IPGroup[j].m_IP_byte, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_IPGroup[j].m_SubnetMask_byte, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
            }



            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_GateWayPara(byte[] SendB)
        {

            int i = 0;
            int j = 0;
            int nPlace = 0;

            int startAddr = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_StartPalce;
            int count = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount;

            SendB[nPlace++] = (byte)TI_Type.W_GW_PA_1; ; //类型
            SendB[nPlace++] = (byte)(count);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            for (i = 0; i < count; i++)
            {
                j = startAddr + i;
                SendB[nPlace++] = (byte)CGlobal.m_GateWayGroup[j].NetPort;
                Array.Copy(CGlobal.m_GateWayGroup[j].m_NetSegment_byte, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GateWayGroup[j].m_SubnetMask_byte, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GateWayGroup[j].m_GateWay_byte, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
            }

            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_DevPara(byte[] SendB)
        {
            byte[] intBuff;
            int i = 0;
            int startAddr = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_StartPalce;
            int j = 0;

            int nPlace = 0;
            SendB[nPlace++] = Convert.ToByte(TI_Type.W_TI_PA_1); //类型
            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);


            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            for (i = 0; i < CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount; i++)
            {
                j = startAddr + i;

                SendB[nPlace++] = CGlobal.m_GDevList[j].m_HostIndex;
                SendB[nPlace++] = CGlobal.m_GDevList[j].m_address;
                //SendB[nPlace++] = CGlobal.m_GDevList[j].m_HostType;
                SendB[nPlace++] = CGlobal.m_GDevList[j].m_ChanType;
                SendB[nPlace++] = CGlobal.m_GDevList[j].m_PrtclType;

                intBuff = BitConverter.GetBytes(CGlobal.m_GDevList[j].LocalPort);
                Array.Copy(intBuff, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;

                intBuff = BitConverter.GetBytes(CGlobal.m_GDevList[j].RemotePort);
                Array.Copy(intBuff, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;

                Array.Copy(CGlobal.m_GDevList[j].m_LocalIP_byte1, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GDevList[j].m_LocalIP_byte2, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GDevList[j].m_LocalIP_byte3, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;

                Array.Copy(CGlobal.m_GDevList[j].m_RemoteIP_byte1, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GDevList[j].m_RemoteIP_byte2, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GDevList[j].m_RemoteIP_byte3, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_HostPara(byte[] SendB)
        {
            byte[] intBuff;
            int i = 0;
            int startAddr = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_StartPalce;
            int j = 0;

            int nPlace = 0;
            SendB[nPlace++] = Convert.ToByte(TI_Type.W_PI_PA_1); //类型
            SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);


            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            for (i = 0; i < CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount; i++)
            {
                j = startAddr + i;

                SendB[nPlace++] = CGlobal.m_GHostList[j].m_HostIndex;
                SendB[nPlace++] = CGlobal.m_GHostList[j].m_address;
                //SendB[nPlace++] = CGlobal.m_GHostList[j].m_HostType;

                SendB[nPlace++] = CGlobal.m_GHostList[j].m_ChanType;
                SendB[nPlace++] = CGlobal.m_GHostList[j].m_PrtclType;

                intBuff = BitConverter.GetBytes(CGlobal.m_GHostList[j].LocalPort);
                Array.Copy(intBuff, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;

                intBuff = BitConverter.GetBytes(CGlobal.m_GHostList[j].RemotePort);
                Array.Copy(intBuff, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;

                Array.Copy(CGlobal.m_GHostList[j].m_LocalIP_byte1, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GHostList[j].m_LocalIP_byte2, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GHostList[j].m_LocalIP_byte3, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;

                Array.Copy(CGlobal.m_GHostList[j].m_RemoteIP_byte1, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GHostList[j].m_RemoteIP_byte2, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
                Array.Copy(CGlobal.m_GHostList[j].m_RemoteIP_byte3, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_ComPara(byte[] SendB)
        {
            byte[] intBuff;
            int i = 0;
            int j = 0;
            int nPlace = 0;
            int startAddr = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_StartPalce;
            int count = CGlobal.m_Device.m_Ptr.m_NowParaTask.m_ParaCount;

            SendB[nPlace++] = (int)TI_Type.W_SR_PA_1; //类型
            SendB[nPlace++] = Convert.ToByte(count);

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            if (m_NowParaTask.m_EndFlag == true)
                SendB[nPlace++] = 0;//参数特征标识,无后续
            else
                SendB[nPlace++] = 1;//参数特征标识，有后续

            for (i = 0; i < count; i++)
            {
                j = startAddr + i;
                intBuff = BitConverter.GetBytes(CGlobal.m_GComList[j].ComPort);
                Array.Copy(intBuff, 0, SendB, nPlace, 1);
                nPlace = nPlace + 1;

                intBuff = BitConverter.GetBytes(CGlobal.m_GComList[j].ComBaud);
                Array.Copy(intBuff, 0, SendB, nPlace, 4);
                nPlace = nPlace + 4;
            }
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_GateWayPara(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = (byte)TI_Type.R_GW_PA_1; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_ComPara(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = (byte)TI_Type.R_SR_PA_1; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_HostPara(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = (byte)TI_Type.R_PI_PA_1;//类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Read_DevPara(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = (byte)TI_Type.R_TI_PA_1;//类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_RestartDev(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0x91; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            SendB[nPlace++] = 0;
            SendB[nPlace++] = 0;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int GetTaskBuffer_101(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;
            switch (nTaskNo)
            {
                case 1: //发送连接报文
                    InitPara();
                    nSendLeng = Send_Short_Report(0x49, pSendBuffer);
                    break;
                case 2:  //发送心跳测试报文
                    nSendLeng = Send_Short_Report(0x42, pSendBuffer);
                    break;
                case 3:  //发送复位报文
                    nSendLeng = Send_Short_Report(0x40, pSendBuffer);
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
                    nSendLeng = Send_Short_Report(0x00, pSendBuffer);
                    break;
                case 21:  //发送心跳测试报文
                    nSendLeng = Send_Short_Report(0x42, pSendBuffer);
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
                    SetTimeOut();
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
                case 33: //发送启动升级报文
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
                case 40:
                    nSendLeng = Send_Short_Report(0x0B, pSendBuffer);
                    break;
                case 41:
                    nSendLeng = Send_CallSync_Time(pSendBuffer);
                    break;
                case 50:
                    nSendLeng = Send_Read_IDPara(pSendBuffer);
                    break;
                case 51:
                    nSendLeng = Send_Write_IDPara(pSendBuffer);
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
            }
            return nSendLeng;
        }

        private int Send_YX_Lock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAA; //类型
            SendB[nPlace++] = 0x01;
            nPlace += SetCot(SendB, nPlace, 6);
            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo % 256);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo / 256);
            SendB[nPlace++] = CGlobal.m_YxValue;
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_YX_UnLock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAB; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo % 256);
            SendB[nPlace++] = (Byte)(CGlobal.m_YxNo / 256);
            SendB[nPlace++] = 0;            
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_YC_Lock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAC; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);

            int addr = CGlobal.m_YcNo + 0x4001 - 1;
            SendB[nPlace++] = (Byte)(addr % 256);
            SendB[nPlace++] = (Byte)(addr / 256);
            byte[] temp = BitConverter.GetBytes(CGlobal.m_YcValue);
            SendB[nPlace++] = temp[0];
            SendB[nPlace++] = temp[1];
            SendB[nPlace++] = temp[2];
            SendB[nPlace++] = temp[3];

            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_YC_UnLock(byte[] SendB)
        {
            int nPlace = 0;
            SendB[nPlace++] = 0xAD; //类型
            SendB[nPlace++] = 0x01;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            int addr = CGlobal.m_YcNo + 0x4001 - 1;
            SendB[nPlace++] = (Byte)(addr % 256);
            SendB[nPlace++] = (Byte)(addr / 256);
            SendB[nPlace++] = 0;          
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int GetTaskBuffer_4G(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;           
            switch (nTaskNo)
            {
                case (int)TI_Type.R_MC_PA_1:          //读4G参数
                    SetTimeOut();
                    nSendLeng = Send_Read_MacPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_MC_PA_1:
                    SetTimeOut();
                    nSendLeng = Send_Write_MacPara(pSendBuffer);
                    break;
                case (int)TI_Type.R_4G_PA_1:          //读4G参数
                    SetTimeOut();
                    nSendLeng = Send_Read_4GPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_4G_PA_1:
                    SetTimeOut();
                    nSendLeng = Send_Write_4GPara(pSendBuffer);
                    break;
                case (int)TI_Type.R_NT_PA_1:         //读IP地址
                    SetTimeOut();
                    nSendLeng = Send_Read_IPPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_NT_PA_1:          //写IP地址
                    SetTimeOut();
                    nSendLeng = Send_Write_IPPara(pSendBuffer);
                    break;
                case (int)TI_Type.R_GW_PA_1:         //读网关地址
                    SetTimeOut();
                    nSendLeng = Send_Read_GateWayPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_GW_PA_1:          //写网关地址
                    SetTimeOut();
                    nSendLeng = Send_Write_GateWayPara(pSendBuffer);
                    break;
                case (int)TI_Type.R_SR_PA_1:          //读串口地址
                    SetTimeOut();
                    nSendLeng = Send_Read_ComPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_SR_PA_1:          //写串口地址
                    SetTimeOut();
                    nSendLeng = Send_Write_ComPara(pSendBuffer);
                    break;
                case (int)TI_Type.R_PI_PA_1:          //读主站参数
                    SetTimeOut();
                    nSendLeng = Send_Read_HostPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_PI_PA_1:          //写主站参数 0x8e
                    SetTimeOut();
                    nSendLeng = Send_Write_HostPara(pSendBuffer);
                    break;

                case (int)TI_Type.R_TI_PA_1:          //读终端配置
                    SetTimeOut();
                    nSendLeng = Send_Read_DevPara(pSendBuffer);
                    break;
                case (int)TI_Type.W_TI_PA_1:          //写终端配置
                    SetTimeOut();
                    nSendLeng = Send_Write_DevPara(pSendBuffer);
                    break;

                case (int)TI_Type.Restart_D:
                    nSendLeng = Send_RestartDev(pSendBuffer);
                    break;
            }
            return nSendLeng;
        }
        public override int GetTaskBuffer(Byte[] pSendBuffer, int nTaskNo)
        {
            int nSendLeng = 0;          
            if ((nTaskNo > 0) && (nTaskNo < 0x85))
            {
                nSendLeng = GetTaskBuffer_101(pSendBuffer, nTaskNo);
            }
            else if ((nTaskNo >= 0x85) && (nTaskNo <= 0x93))
            {
                nSendLeng = GetTaskBuffer_4G(pSendBuffer, nTaskNo);
            }
            else if ((nTaskNo >= 1000) && (nTaskNo < 1000000))
            {
                nSendLeng = GetTaskBuffer_TestCase101(pSendBuffer, nTaskNo);
            }
            else if (nTaskNo >= 1000000)
            {
                nSendLeng = base.GetTaskBuffer(pSendBuffer, nTaskNo);
            }

            if ((CGlobal.m_Device.m_Connection.IsEncode == true) && (nTaskNo < 1000000))
            {

                if ((nTaskNo >= 1000) && (nTaskNo < 1000000))
                {
                    nSendLeng = GetTaskBuffer_Encode_testCase(pSendBuffer, nTaskNo, nSendLeng);
                }
                else
                {
                    nSendLeng = GetTaskBuffer_Encode(pSendBuffer, nTaskNo, nSendLeng);
                }
            }
            return nSendLeng;
        }

        private int Send_Read_IDPara(byte[] SendB)
        {
            

            int nPlace = 0;
            SendB[nPlace++] = (byte)TI_Type.R_4G_PA_1; //类型
            SendB[nPlace++] = 1;
            nPlace += SetCot(SendB, nPlace, 6);
            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);
            SendB[nPlace++] = 0;//参数特征标识,无后续
            int nAddress = 0x04;
            SendB[nPlace++] = Convert.ToByte(nAddress % 256);
            SendB[nPlace++] = Convert.ToByte(nAddress / 256);
            return Send_Stardard_101Report(SendB, nPlace);
        }
        private int Send_Write_IDPara(byte[] SendB)
        {
           

            int nPlace = 0;

            SendB[nPlace++] = (int)TI_Type.W_4G_PA_1; //类型

            SendB[nPlace++] = 1;

            nPlace += SetCot(SendB, nPlace, 6);

            nPlace += SetAddress(SendB, nPlace, theDevice.m_ASDUAddr);


            SendB[nPlace++] = 0;//参数特征标识,无后续


            byte[] data = new byte[10];
            try
            {
                int nAddress = 0x004;
                //if (CheckParaStatus(Is_PriPara, nAddress)) break;
                SendB[nPlace++] = Convert.ToByte(nAddress % 256);
                SendB[nPlace++] = Convert.ToByte(nAddress / 256);

                SendB[nPlace++] = 4;//tag类型
                SendB[nPlace++] = Convert.ToByte(CGlobal.m_Device.m_DevID.Length);//数据长度

                Byte[] pValueByte = Encoding.Default.GetBytes(CGlobal.m_Device.m_DevID);
                int nLength = pValueByte.Length;
                for (int j = 0; j < nLength; j++) SendB[nPlace++] = pValueByte[j];

                return Send_Stardard_101Report(SendB, nPlace);
            }
            catch
            {
                MessageBox.Show("参数输入异常", "提示");
                return 0;
            }
        }
    }
}


