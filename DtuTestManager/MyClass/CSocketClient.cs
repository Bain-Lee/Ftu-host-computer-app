using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace DtuTestManager
{
    public class CSocketChannel
    {
        private object m_Lock = new object();  
        protected Socket SocketClient;                   //socket连接对象
        protected bool m_OnlineFlag = false;
        protected bool bReciveFlag;                    //接收线程标志
        protected bool bListenFlag;    
        protected ManualResetEvent TimeoutObject;             //超时等待
        protected Thread RecvThread;
        protected Thread AcceptThread;
        protected AutoResetEvent exitEvent;
        protected object m_SocketLock = new object();
        protected Socket m_ListenSocket = null;
        public delegate void Delegate_ShowBuff(string strName, Byte[] pBuff, int nLeng);


        public CSocketChannel()
        {
            TimeoutObject = new ManualResetEvent(false);
            bReciveFlag = false;
            exitEvent = new AutoResetEvent(false);
            RecvThread = null;
            SocketClient = null;
        }
        protected void CloseSocket()
        {
            if (SocketClient != null)
            {
                SocketClient.Close();
                SocketClient = null;
            }
            m_OnlineFlag = false;
        }
      
        public bool SendToSocket(Byte[] pSendBuff, int nSendLeng)
        {
            bool SendFlags = false;
            Monitor.Enter(m_Lock);
            try 
            {
                if (SocketClient == null) return false;
               
                if (SocketClient != null && SocketClient.Connected)
                {
                    if (SocketClient.Poll(100, SelectMode.SelectWrite))
                    {
                        SocketClient.Send(pSendBuff, nSendLeng, 0);
                        SendFlags = true;
                    }
                    else
                    {
                        CloseSocket();                      
                        SendFlags = false;
                    }
                }
                
            } 
            catch (SocketException se) 
            {
                CloseSocket();
                SendFlags = false;
            }
            Monitor.Exit(m_Lock);
            return SendFlags;
        }
    }
    public class CSocketConnect : CSocketChannel
    {
        public CSocketConnect()
        {

        }
        private void ExceptionSolver(SocketException sep)
        {
            switch (sep.SocketErrorCode)
            {
                case SocketError.NotConnected:
                    //捕获ip地址输入错误的情况;
                    //Program.postLog("不存在网络连接");
                    break;

                case SocketError.ConnectionAborted:
                    //在这里处理频繁出现的错误，
                    //比如IP不对，网线没插
                    //Program.postLog("连接中止");
                    break;
                case SocketError.ConnectionRefused:
                    //远程主机正在主动拒绝连接;可能是连接的时候ip或port写错了;
                    //Program.postLog("对方不接受连接,更可能是port的原因");
                    break;
                case SocketError.HostUnreachable:
                    //Program.postLog("连接目标不可达");
                    break;
                case SocketError.TimedOut:
                    //尝试连接ip超时;
                    //Program.postLog("尝试连接ip超时,更可能是ip的原因");
                    break;
                default:
                    //Program.postLog("捕获到" + sep.SocketErrorCode);
                    //这里直接报错，如果调试的时候出现这里的错误比较多，就移到上面解决，一般问题都是从来不出的
                    break;

            }
        }
        private void ConnCallBack(IAsyncResult AsyncResult)
        {
            Socket sck = (Socket)AsyncResult.AsyncState;
            try
            {
                sck.EndConnect(AsyncResult);
            }
            catch (Exception ex)
            {
                // m_bOnlineStatus = false;
                //发生错误            
                return;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }
        public virtual bool SocketConnect(string strServerIP, int nServerPort, int nTimeOut, int nCount)
        {
            if (m_OnlineFlag) return true;
            if (nCount <= 0) return m_OnlineFlag;
            TimeoutObject.Reset();
            SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint pConnSocket = new IPEndPoint(IPAddress.Parse(strServerIP), nServerPort);
            try
            {
                //开始连接       
                SocketClient.Blocking = false;
                SocketClient.BeginConnect(pConnSocket, new AsyncCallback(ConnCallBack), SocketClient);
                TimeoutObject.WaitOne(nTimeOut, false);
                m_OnlineFlag = SocketClient.Connected;
                if (SocketClient.Connected == false)
                {
                    //SocketClient.EndConnect(pResult);
                    nCount--;
                    m_OnlineFlag = SocketConnect(strServerIP, nServerPort, nTimeOut, nCount);
                    return m_OnlineFlag;
                }
                return m_OnlineFlag;
            }
            catch (SocketException ex)
            {                //连接失败                
                CloseSocket();
                ExceptionSolver(ex);
            }
            catch (Exception ex)
            {                //连接失败            
                CloseSocket();
            }
            return false;
        }
        public virtual bool SocketConnect(int nListenPort)
        {
            return false;
        }
    }
    public class CSocketDevice : CSocketConnect
    {
        public CDeviceClass theDevice = null;              //对应装置对象
        public void CloseThread()
        {
            bListenFlag = false;
            if (m_ListenSocket != null)
            {
                m_ListenSocket.Close();
                //AcceptThread.Join();
                m_ListenSocket = null;
            }
            if (bReciveFlag)
            {
                bReciveFlag = false;
                exitEvent.Set();
                Thread.Sleep(100);
                //RecvThread.Join();
            }
            theDevice.m_bOnlineStatus = false;
            RecvThread = null;
            CloseSocket();
        }
        private void ByteGroupMove(Byte[] pSourceBuff, int pMovePlace, int pMoveLeng)
        {
            Byte[] pNewBuff = new Byte[pSourceBuff.Length];
            Array.Copy(pSourceBuff, pMovePlace, pNewBuff, 0, pMoveLeng);
            Array.Copy(pNewBuff, 0, pSourceBuff, 0, pMoveLeng);
        }
        public void CloseReadSoketThread()
        {
            if (bReciveFlag)
            {
                bReciveFlag = false;
                exitEvent.Set();
                Thread.Sleep(100);
               // RecvThread.Join();
                CloseSocket();
            }
        }
        public void StartListenSocket()
        {          
            Socket pClient;
            while (bListenFlag)
            {
                try
                {                 
                    pClient = m_ListenSocket.Accept();

                    if (pClient == null) continue;
                    CloseReadSoketThread();
                    SocketClient = pClient;

                    theDevice.m_bOnlineStatus = true;
                    if (bReciveFlag == false)
                    {
                        bReciveFlag = true;
                        theDevice.m_Ptr.InitPara();
                        RecvThread = new Thread(new ThreadStart(this.RtuRecThread));
                        RecvThread.IsBackground = true;
                        RecvThread.Start();

                        if (CGlobal.m_RunSendMode)
                        {
                            if (CGlobal.m_Device.m_Connection.IsEncode == true)
                            {
                                CGlobal.SendTaskOrder((int)EncodeStatus.Auto_Master_Indentity);
                            }
                            else
                            {
                                CGlobal.SendTaskOrder(1);
                            }
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        protected int ReceiveBuff(Byte[] pReceiveBuff)
        {
            int nReadLeng = 0;
            try
            {
                if (SocketClient != null && SocketClient.Connected && SocketClient.Available > 0)
                {
                    if (SocketClient.Poll(100, SelectMode.SelectRead))
                    {
                        nReadLeng = SocketClient.Receive(pReceiveBuff);
                        if (nReadLeng == 0)
                        {
                            CloseSocket();
                            theDevice.m_bOnlineStatus = false;
                        }
                    }
                    else
                    {
                        CloseSocket();
                        theDevice.m_bOnlineStatus = false;
                    }
                }
            }
            catch (SocketException ex)
            {                
                    CloseSocket();
                    theDevice.m_bOnlineStatus = false;                
            }
            return nReadLeng;
        }   
        public void RtuRecThread()
        {
           
            int nNowRecLeng = 0;
            byte[] pNowReadBuff = new byte[4096];

            exitEvent.Reset();
            m_OnlineFlag = true;
            theDevice.m_ReceiveFlag = true;
            while (bReciveFlag)
            {
                if (exitEvent.WaitOne(5))  break;
                nNowRecLeng = ReceiveBuff(pNowReadBuff);              
                theDevice.AddProcessBuff(pNowReadBuff, nNowRecLeng);               
                if (m_OnlineFlag == false) break;
            }
            theDevice.m_bOnlineStatus = false;
            CloseSocket();
            bReciveFlag = false;
            theDevice.m_ReceiveFlag = true;
        }
        public override bool SocketConnect(string strServerIP, int nServerPort, int nTimeOut, int nCount)
        {
            if (base.SocketConnect(strServerIP, nServerPort, nTimeOut, nCount))
            {
                theDevice.m_bOnlineStatus = true;
                if (bReciveFlag == false)
                {
                    bReciveFlag = true;
                    theDevice.m_Ptr.InitPara();
                    RecvThread = new Thread(new ThreadStart(this.RtuRecThread));
                    RecvThread.IsBackground = true;
                    RecvThread.Start();                  
                }
            }
            return m_OnlineFlag;
        }
        public override bool SocketConnect(int nListenPort)
        {          
            if (m_ListenSocket != null) return true;
            m_ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostIP = IPAddress.Any;
            IPEndPoint ep = new IPEndPoint(hostIP, nListenPort);
            try
            {
                m_ListenSocket.Bind(ep);
                m_ListenSocket.Listen(5);
            }
            catch
            {
                MessageBox.Show("监听失败!");
                m_ListenSocket = null;
                return false;
            }
            bListenFlag = true;
            AcceptThread = new Thread(new ThreadStart(this.StartListenSocket));
            AcceptThread.IsBackground = true;
            AcceptThread.Start();

            return true;
        }
    }
}
