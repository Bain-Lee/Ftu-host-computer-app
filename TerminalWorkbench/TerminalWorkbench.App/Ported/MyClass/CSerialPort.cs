using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using TerminalWorkbench.Presentation;
using System.IO.Ports;
using System.Threading;


namespace DtuTestManager
{
    public class CSerialPort
    {
        protected SerialPort m_SerialPort = new SerialPort();
        public bool m_OpenFlag = false;
        protected bool bReciveFlag;                    //接收线程标志
        protected Thread RecvThread;
        public AutoResetEvent exitEvent;
        public ComOption pComOptoin;
        private object m_RWLock = new object();
        public delegate void Delegate_ShowBuff(string strName, Byte[] pBuff, int nLeng);
        public CSerialPort()
        {
            bReciveFlag = false;
            exitEvent = new AutoResetEvent(false);
            RecvThread = null;
        }
        public virtual bool OpenComPort(ComOption ComInfo)
        {
            pComOptoin = ComInfo;
            if (m_OpenFlag)
            {
                CloseComPort();
            }
            m_SerialPort.PortName = "COM" + ComInfo.m_nCom_No.ToString();
            m_SerialPort.BaudRate = ComInfo.m_nCom_Baund;
            m_SerialPort.DataBits = Convert.ToInt32(ComInfo.m_nCom_DataBit);
            m_SerialPort.WriteTimeout = 20;
            //   m_SerialPort.ReadBufferSize = 2048;
            //  m_SerialPort.WriteBufferSize = 2048;
            switch (ComInfo.m_nCom_CheckBit)
            {
                case 0:
                    m_SerialPort.Parity = Parity.None;
                    break;
                case 1:
                    m_SerialPort.Parity = Parity.Odd;
                    break;
                case 2:
                    m_SerialPort.Parity = Parity.Even;
                    break;
                default:
                    m_SerialPort.Parity = Parity.None;
                    break;
            }
            switch (ComInfo.m_nCom_StopBit)
            {
                case 1:
                    m_SerialPort.StopBits = StopBits.One;
                    break;
                case 2:
                    m_SerialPort.StopBits = StopBits.Two;
                    break;
                default:
                    m_SerialPort.StopBits = StopBits.One;
                    break;
            }
            try
            {
                m_SerialPort.Open();
                m_OpenFlag = m_SerialPort.IsOpen;
            }
            catch
            {
                m_SerialPort.Close();
            }
            return m_OpenFlag;
        }
        public virtual void CloseComPort()
        {
            m_OpenFlag = false;
            m_SerialPort.Close();
        }
        public virtual bool SendToComPort(byte[] pSendBuff, int nSendLeng)
        {
            try
            {
                if (m_OpenFlag == false)
                    return false;
                Monitor.Enter(m_RWLock);
                m_SerialPort.Write(pSendBuff, 0, nSendLeng);
                // Thread.Sleep(300);
                Monitor.Exit(m_RWLock);
                Thread.Sleep(nSendLeng);
            }
            catch (Exception)
            {
                m_SerialPort.DiscardOutBuffer();
                m_SerialPort.DiscardInBuffer();
                if (OpenComPort(pComOptoin) == false)
                {
                    m_OpenFlag = false;
                    CGlobal.m_Device.m_bOnlineStatus = false;
                }
                return false;
            }
            return true;
        }
        public virtual int ReadComPort(Byte[] pRecBuff, int nWait, int nMaxLeng)
        {
            int nNewRecLeng = 0;
            int nOldRecLeng = 0;
            if (m_SerialPort.IsOpen == false)
                return 0;
            while (true)
            {
                Thread.Sleep(50);
                if (m_SerialPort.IsOpen == false)
                    return 0;
                nNewRecLeng = m_SerialPort.BytesToRead;
                if (nNewRecLeng != nOldRecLeng)
                {
                    nOldRecLeng = nNewRecLeng;
                    nWait = 0;
                    continue;
                }
                else
                {
                    if (nOldRecLeng > 0)
                    {
                        if (nNewRecLeng > nMaxLeng)
                            nNewRecLeng = nMaxLeng;
                        Monitor.Enter(m_RWLock);
                        nOldRecLeng = m_SerialPort.Read(pRecBuff, 0, nNewRecLeng);
                        Monitor.Exit(m_RWLock);
                        break;
                    }
                    if (nWait <= 0)
                        break;
                    nWait--;
                }
            }
            return nOldRecLeng;
        }
    }
    public class CComDevice : CSerialPort
    {
        public CDeviceClass theDevice = null;              //对应装置对象
        public void CloseThread()
        {
            if (bReciveFlag)
            {
                exitEvent.Set();
                bReciveFlag = false;
                Thread.Sleep(100);
            }
            theDevice.m_bOnlineStatus = false;
            RecvThread = null;
            CloseComPort();
        }
        private void ByteGroupMove(Byte[] pSourceBuff, int pMovePlace, int pMoveLeng)
        {
            Byte[] pNewBuff = new Byte[pSourceBuff.Length];
            Array.Copy(pSourceBuff, pMovePlace, pNewBuff, 0, pMoveLeng);
            Array.Copy(pNewBuff, 0, pSourceBuff, 0, pMoveLeng);
        }
        public void RtuRecThread()
        {
            int nNowRecLeng = 0;
            byte[] pNowReadBuff = new byte[4096];

            theDevice.m_Ptr.InitPara();
            exitEvent.Reset();

            while (bReciveFlag)
            {
                if (exitEvent.WaitOne(50))
                {
                    break;
                }
                nNowRecLeng = ReadComPort(pNowReadBuff, 1, 4096);
                theDevice.AddProcessBuff(pNowReadBuff, nNowRecLeng);
                if (theDevice.m_bOnlineStatus == false)
                {
                    break;
                }
            }
            theDevice.m_bOnlineStatus = false;
            bReciveFlag = false;
        }
        public override bool OpenComPort(ComOption ComInfo)
        {
            if (base.OpenComPort(ComInfo))
            {
                if (bReciveFlag == false)
                {
                    bReciveFlag = true;
                    RecvThread = new Thread(new ThreadStart(this.RtuRecThread));
                    RecvThread.IsBackground = true;
                    RecvThread.Start();
                }
                return true;
            }
            return false;
        }
    }
}
