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
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

namespace DtuTestManager
{
    public partial class CFTUFastUpdate : Form
    {
        public byte[] SendBuffer = new byte[2000];
        byte[] head = new byte[160];
        public int flag_Cancel = 0;
        int count;//用于定时器计数
        public Socket TFRSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public CFTUFastUpdate()
        {
            InitializeComponent();
        }
        private void CFTUFastUpdate_Load(object sender, EventArgs e)
        {

        }
        byte GetSignMark(int i)
        {
            string str = "+<这是_序列号>-";
            byte[] bt = TerminalWorkbench.Presentation.TextEncoding.Ansi.GetBytes(str);
            return bt[i];
        }
        bool ScanSignMark(byte[] buffer, long length, out int psm)
        {
            int i, j;
            int temp = 0;
            for (i = 0; i < length - 32; i++)
            {
                for (j = 0; j < 15; j++)
                {
                    if (buffer[i + j] == GetSignMark(j))
                    {
                        temp++;
                    }
                    if (buffer[i + j] != GetSignMark(j))
                        break;
                }
                if (j == 15)
                {
                    psm = i;
                    return true;
                }
            }
            psm = -1;
            return false;
        }
        public bool DoWithLoadFile_FTU(string strPath)
        {
            FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (file.Length == 0)
                return false;

            byte[] data = new byte[file.Length];
            file.ReadExactly(data);
            file.Close();

            FileInfo myFile = new FileInfo(strPath);
            CGlobal.m_Device.m_TransFile.m_FileName = myFile.Name;
            CGlobal.m_Device.m_TransFile.m_FileNameLength = myFile.Name.Length;
            CGlobal.m_Device.m_TransFile.m_FileID = 0;
            CGlobal.m_Device.m_TransFile.m_FileSize = myFile.Length;//获得文件的字节数

            int i = 0;
            int nCount = 0;
            nCount = (int)(myFile.Length / 1024);   //下发文件包数 max
            CGlobal.m_Device.m_TransFile.SumFrames = nCount;
            int psm = 0;
            ScanSignMark(data, myFile.Length, out psm);
            if (psm < 0)
            {
                MessageBox.Show("未知的装置类型，无法升级", "提示");
            }
            i = 0;
            psm = psm + 16;
            while (data[psm] != 0)
            {
                head[i++] = data[psm++];
            }
            string str_temp = BytesToString(head);
            label3.Text = "Device number：" + str_temp;

            for (i = 0; i < nCount; i++)
            {
                CFileData temp = new CFileData();
                temp.PackNo = i;
                temp.DataLength = 1024;
                temp.Offset = i * 1024;
                temp.DataBuff = new byte[temp.DataLength];
                Array.Copy(data, temp.Offset, temp.DataBuff, 0, temp.DataLength);
                CGlobal.m_Device.m_TransFile.m_data.Add(temp);
            }
            if ((myFile.Length % 1024) > 0)
            {
                CFileData temp = new CFileData();
                temp.PackNo = nCount;
                temp.DataLength = (int)(myFile.Length % 1024);
                temp.DataBuff = new byte[temp.DataLength];
                temp.Offset = nCount * 1024;
                Array.Copy(data, temp.Offset, temp.DataBuff, 0, temp.DataLength);
                CGlobal.m_Device.m_TransFile.m_data.Add(temp);
                nCount++;
            }
            CGlobal.m_Device.m_TransFile.SumFrames = nCount;
            CGlobal.m_Device.m_TransFile.CurrentIndex = 0;
            return true;
        }
        private string BytesToString(byte[] Bytes)
        {
            string Mystring;
            Encoding FromEcoding = Encoding.GetEncoding("gb2312");
            Encoding ToEcoding = Encoding.GetEncoding("UTF-8");
            byte[] Tobytes = Encoding.Convert(FromEcoding, ToEcoding, Bytes);
            Mystring = ToEcoding.GetString(Tobytes);                                            //得到的是UTF8字节码序列，需要转换为UTF8字符串
            return Mystring;                                                                    //转换
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CGlobal.m_Device.m_TransFile.m_data.Clear();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "BIN文件(*.bin)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath.Text = fileDialog.FileName;
                try
                {
                    if (DoWithLoadFile_FTU(fileDialog.FileName) == false)
                        return;
                    FileDownMsg.Value = 0;
                    FileDownMsg.Minimum = 0;
                    FileDownMsg.Maximum = CGlobal.m_Device.m_TransFile.SumFrames + 1;
                    FileDownMsg.Step = 1;
                    FpbValue.Text = "进度0%";
                    BtnStartDown.Enabled = true;
                    BtnEndDown.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("文件打开出错", "提示");
                }
            }
        }
        private void BtnStartDown_Click(object sender, System.EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(createThreadUpDate));
            thread.IsBackground = true;
            thread.Start();//启动线程
        }
        private int SendEndReport()
        {
            int offset = 17;
            int i = 0;
            SendBuffer[offset] = 0xff;
            offset++;
            SendBuffer[offset] = 0xff;
            offset++;
            int Len = Convert.ToInt32(CGlobal.m_Device.m_TransFile.m_FileSize);
            byte[] byData = new byte[CGlobal.m_Device.m_TransFile.m_FileSize];
            int nIndex = 0;
            for (i = 0; i < CGlobal.m_Device.m_TransFile.SumFrames; i++)
            {

                Array.Copy(CGlobal.m_Device.m_TransFile.m_data[i].DataBuff, 0, byData, nIndex, CGlobal.m_Device.m_TransFile.m_data[i].DataBuff.Length);
                nIndex += CGlobal.m_Device.m_TransFile.m_data[i].DataBuff.Length;
            }
            int crcTemp = CRC16(byData, Len);
            SendBuffer[offset] = Convert.ToByte(crcTemp & 255);
            offset++;
            SendBuffer[offset] = Convert.ToByte(crcTemp >> 8);
            offset++;
            if (MessageBox.Show("确定升级吗？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string ss = "StartProgram";

                for (i = 0; i < 12; i++)
                {
                    SendBuffer[21 + i] = Convert.ToByte(ss[i]);

                }
                SendBuffer[21 + i] = 0x00;
            }
            else
            {
                string ss = "CancelProgram";
                for (i = 0; i < 13; i++)
                {
                    SendBuffer[21 + i] = Convert.ToByte(ss[i]);

                }
                SendBuffer[21 + i] = 0x00;
                flag_Cancel = 1;
            }
            return (21 + i + 1);
        }
        private void makeframe(int Len)
        {
            int offset = 0;
            SendBuffer[offset] = 0xEB;
            offset = offset + 1;
            SendBuffer[offset] = 0x90;
            offset = offset + 1;
            SendBuffer[offset] = 0xEB;
            offset = offset + 1;
            SendBuffer[offset] = 0x90;
            offset = offset + 1;
            //长度暂定为0
            SendBuffer[offset] = 00;
            offset = offset + 1;
            SendBuffer[offset] = 00;
            offset = offset + 1;
            //功能码为0
            SendBuffer[offset] = 00;
            offset = offset + 1;
            //子类型为0
            SendBuffer[offset] = 00;
            offset = offset + 1;
            //装置类型号
            SendBuffer[offset] = head[0];
            offset = offset + 1;
            SendBuffer[offset] = head[1];
            offset = offset + 1;
            SendBuffer[offset] = head[2];
            offset = offset + 1;
            //升级文件总字节长度
            long size1 = CGlobal.m_Device.m_TransFile.m_FileSize;
            for (int i = offset; i < offset + 4; i++)
            {
                SendBuffer[i] = Convert.ToByte(size1 % 256);
                size1 = size1 - size1 % 256;
                size1 = size1 / 256;
            }
            offset = offset + 4;
            //升级文件ID
            SendBuffer[offset++] = Convert.ToByte(CGlobal.m_Device.m_TransFile.m_FileID % 256);
            SendBuffer[offset++] = Convert.ToByte(CGlobal.m_Device.m_TransFile.m_FileID / 256);



            //长度
            int temp = Len - 6;
            SendBuffer[4] = Convert.ToByte(temp % 256);
            temp = temp - temp % 256;
            temp = temp / 256;
            offset = offset + 1;
            SendBuffer[5] = Convert.ToByte(temp % 256);
            //最后的CRC
            //去掉包头再校验
            byte[] tempdata = new byte[Len - 6 - 2];
            for (int i = 6; i < Len - 2; i++)
            {
                tempdata[i - 6] = SendBuffer[i];
            }
            int crcTemp = CRC16(tempdata, Len - 6 - 2);

            SendBuffer[Len - 2] = Convert.ToByte(crcTemp & 255);
            SendBuffer[Len - 1] = Convert.ToByte(crcTemp >> 8);
        }
        void Sendreport()
        {

            // str = "当前帧：" + Convert.ToString(frameCnt + 1) + "\r\n";
            int offset = 17;
            int i = 0;
            //升级文件帧计数
            int frame = CGlobal.m_Device.m_TransFile.CurrentIndex;
            SendBuffer[offset++] = Convert.ToByte(frame % 256);
            SendBuffer[offset++] = Convert.ToByte(frame / 256);
            if (frame < CGlobal.m_Device.m_TransFile.SumFrames)
            {
                for (i = offset; i < offset + CGlobal.m_Device.m_TransFile.m_data[frame].DataBuff.Length; i++)
                {

                    SendBuffer[i] = CGlobal.m_Device.m_TransFile.m_data[frame].DataBuff[i - offset];
                }
            }
            else
            {
                i = SendEndReport();
            }
            int ZLen = i + 2;
            makeframe(ZLen);
            try
            {
                int kk = TFRSocket.Send(SendBuffer, ZLen, 0);
                timer1.Start();//开始计时
                if (flag_Cancel == 1)
                {
                    TFRSocket.Close();
                }
            }
            catch
            {
                MessageBox.Show("发送数据出错", "提示");
                try
                {
                    TFRSocket.Shutdown(SocketShutdown.Both);
                }
                catch
                {
                }
                TFRSocket.Close();
                timer1.Stop();
            }
            CGlobal.m_Device.m_TransFile.CurrentIndex++;
            FileDownMsg.Invoke(
                new MethodInvoker(
                    delegate
                    {
                        FileDownMsg.Value = CGlobal.m_Device.m_TransFile.CurrentIndex;
                        float fValue = (float)(((float)CGlobal.m_Device.m_TransFile.CurrentIndex) / ((float)CGlobal.m_Device.m_TransFile.SumFrames));
                        fValue *= 100;
                        FpbValue.Text = "进度" + fValue.ToString("F2") + "%";
                    }
                    )
                );
        }
        public void createThreadUpDate()
        {
            flag_Cancel = 0;
            int iBytes = 0;
            if (TFRSocket.Connected == false)
            {
                try
                {
                    TFRSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    TFRSocket.Connect(IPAddress.Parse(CGlobal.m_Device.m_Connection.m_strIP_Address), 9);
                    CGlobal.m_Device.m_TransFile.CurrentIndex = 0;
                }
                catch
                {
                    return;
                }
            }
            else
            {
                try
                {
                    TFRSocket.Close();
                    TFRSocket.Connect(IPAddress.Parse(CGlobal.m_Device.m_Connection.m_strIP_Address), 9);
                }
                catch
                {
                    return;
                }
            }
            Random ra = new Random();
            CGlobal.m_Device.m_TransFile.m_FileID = (ra.Next()) & 0xFF;
            Sendreport();
            try
            {
                while (true)
                {
                    try
                    {
                        //通过clientSocket接收数据
                        byte[] m_DataBuffer = new byte[500];
                        if (flag_Cancel == 1)
                        {
                            return;
                        }
                        try
                        {
                            if (TFRSocket.Connected == true)
                                iBytes = TFRSocket.Receive(m_DataBuffer);
                        }
                        catch
                        {
                            timer1.Enabled = false;
                            TFRSocket.Close();
                            return;
                        }
                        if (iBytes > 0)
                        {
                            if ((m_DataBuffer[0] == 0xAA) && (m_DataBuffer[7] == 0xAB))
                            {
                                int crcTemp = CRC16(m_DataBuffer, iBytes - 2);
                                if ((m_DataBuffer[16] == Convert.ToByte(crcTemp & 255)) && (m_DataBuffer[17] == Convert.ToByte(crcTemp >> 8)))
                                {
                                    try
                                    {
                                        Sendreport();
                                    }
                                    catch
                                    {
                                        TFRSocket.Close();
                                        return;
                                    }
                                    count = 0;
                                }
                            }
                            else
                            {
                                label3.Invoke(
              new MethodInvoker(
                  delegate
                  {
                      label3.Text = BytesToString(m_DataBuffer);
                  }
                  )
              );

                                count = 0;
                                TFRSocket.Close();
                                timer1.Enabled = false;
                                return;
                            }
                            iBytes = 0;
                        }
                        else
                        {
                            if (count > 3)
                            {
                                //超过10秒，没收到报文
                                MessageBox.Show("超时没收到报文", "提示信息");
                                TFRSocket.Close();
                                return;
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("服务器中断", "提示");
                return;
            }

        }
        //包头不参与校验
        public int CRC16(byte[] dat, int count)
        {
            int crc = 0xFFFF;
            Byte crctemp;
            int i = 0;
            while (count > 0)
            {
                count -= 1;
                crctemp = Convert.ToByte(crc >> 8);
                crc = (crc << 8) & 0xFFFF;
                crc = (CRC16Table[crctemp ^ dat[i]] ^ crc) & 0xFFFF;
                i += 1;
            }
            return crc;
        }
        public ushort[] CRC16Table = {
           0xFFFF, 0x1021, 0x2042, 0x3063, 0x4084, 0x50A5, 0x60C6, 0x70E7,
           0x8108, 0x9129, 0xA14A, 0xB16B, 0xC18C, 0xD1AD, 0xE1CE, 0xF1EF,
           0x1231, 0x210, 0x3273, 0x2252, 0x52B5, 0x4294, 0x72F7, 0x62D6,
           0x9339, 0x8318, 0xB37B, 0xA35A, 0xD3BD, 0xC39C, 0xF3FF, 0xE3DE,
           0x2462, 0x3443, 0x420, 0x1401, 0x64E6, 0x74C7, 0x44A4, 0x5485,
           0xA56A, 0xB54B, 0x8528, 0x9509, 0xE5EE, 0xF5CF, 0xC5AC, 0xD58D,
           0x3653, 0x2672, 0x1611, 0x630, 0x76D7, 0x66F6, 0x5695, 0x46B4,
           0xB75B, 0xA77A, 0x9719, 0x8738, 0xF7DF, 0xE7FE, 0xD79D, 0xC7BC,
           0x48C4, 0x58E5, 0x6886, 0x78A7, 0x840, 0x1861, 0x2802, 0x3823,
           0xC9CC, 0xD9ED, 0xE98E, 0xF9AF, 0x8948, 0x9969, 0xA90A, 0xB92B,
           0x5AF5, 0x4AD4, 0x7AB7, 0x6A96, 0x1A71, 0xA50, 0x3A33, 0x2A12,
           0xDBFD, 0xCBDC, 0xFBBF, 0xEB9E, 0x9B79, 0x8B58, 0xBB3B, 0xAB1A,
           0x6CA6, 0x7C87, 0x4CE4, 0x5CC5, 0x2C22, 0x3C03, 0xC60, 0x1C41,
           0xEDAE, 0xFD8F, 0xCDEC, 0xDDCD, 0xAD2A, 0xBD0B, 0x8D68, 0x9D49,
           0x7E97, 0x6EB6, 0x5ED5, 0x4EF4, 0x3E13, 0x2E32, 0x1E51, 0xE70,
           0xFF9F, 0xEFBE, 0xDFDD, 0xCFFC, 0xBF1B, 0xAF3A, 0x9F59, 0x8F78,
           0x9188, 0x81A9, 0xB1CA, 0xA1EB, 0xD10C, 0xC12D, 0xF14E, 0xE16F,
           0x1080, 0xA1, 0x30C2, 0x20E3, 0x5004, 0x4025, 0x7046, 0x6067,
           0x83B9, 0x9398, 0xA3FB, 0xB3DA, 0xC33D, 0xD31C, 0xE37F, 0xF35E,
           0x2B1, 0x1290, 0x22F3, 0x32D2, 0x4235, 0x5214, 0x6277, 0x7256,
           0xB5EA, 0xA5CB, 0x95A8, 0x8589, 0xF56E, 0xE54F, 0xD52C, 0xC50D,
           0x34E2, 0x24C3, 0x14A0, 0x481, 0x7466, 0x6447, 0x5424, 0x4405,
           0xA7DB, 0xB7FA, 0x8799, 0x97B8, 0xE75F, 0xF77E, 0xC71D, 0xD73C,
           0x26D3, 0x36F2, 0x691, 0x16B0, 0x6657, 0x7676, 0x4615, 0x5634,
           0xD94C, 0xC96D, 0xF90E, 0xE92F, 0x99C8, 0x89E9, 0xB98A, 0xA9AB,
           0x5844, 0x4865, 0x7806, 0x6827, 0x18C0, 0x8E1, 0x3882, 0x28A3,
           0xCB7D, 0xDB5C, 0xEB3F, 0xFB1E, 0x8BF9, 0x9BD8, 0xABBB, 0xBB9A,
           0x4A75, 0x5A54, 0x6A37, 0x7A16, 0xAF1, 0x1AD0, 0x2AB3, 0x3A92,
           0xFD2E, 0xED0F, 0xDD6C, 0xCD4D, 0xBDAA, 0xAD8B, 0x9DE8, 0x8DC9,
           0x7C26, 0x6C07, 0x5C64, 0x4C45, 0x3CA2, 0x2C83, 0x1CE0, 0xCC1,
           0xEF1F, 0xFF3E, 0xCF5D, 0xDF7C, 0xAF9B, 0xBFBA, 0x8FD9, 0x9FF8,
           0x6E17, 0x7E36, 0x4E55, 0x5E74, 0x2E93, 0x3EB2, 0xED1, 0x1EF0
            };
        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;//记录当前秒
        }

        private void CFTUFastUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
