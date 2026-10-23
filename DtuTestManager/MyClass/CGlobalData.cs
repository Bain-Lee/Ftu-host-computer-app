using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
namespace DtuTestManager
{
    class CGlobalData
    {
    }
    public struct ComOption
    {
        public int m_nCom_No;
        public int m_nCom_Baund;
        public int m_nCom_DataBit;
        public int m_nCom_StopBit;
        public int m_nCom_CheckBit;
    };
    public struct Ptr101Address
    {
        public int m_AsduAddress;
        public int m_YxStartAddress;
        public int m_YcStartAddress;
        public int m_YkSrartAddress;
        public int m_PowerStartAddress;
        public int m_AddressType;
        public int m_CotType;
    };
    public struct SystemConnection
    {
        public bool IsEncode;        //false  不加密   true加密       
        public int m_nConnType;                 //0表示串口 1表示网络
        public int m_n_ProType;                 //0表示101   1表示104
        public int m_SingleType;                //表示单点遥信，1表示双点遥信
        public string m_DevName;                //装置模板
        public string m_strIP_Address;
        public int m_nIP_Port;
        public int m_SocketMode;                //TCP/IP模式，0表示客户端模式，1表示服务器模式
        public ComOption m_strutComOption;
        public Ptr104Address m_104Address;
        public Ptr101Address m_101Address;
        public int m_DC1Index;
        public int m_DC2Index;
        public int m_AC1Index;
        public int m_AC2Index;
    };
    public class CYkOption
    {
        public int m_YkNo;          //遥控号
        public int m_YkValue;       //0分 1合
        public int m_YkType;        //0单点  1双点
        public int m_YkStatus;      //0 选择 1执行  2取消
        public string m_YkName;     //遥控名称
        public int m_YkAddress;     //遥控地址
    }
    public class CYxOption
    {
        public int m_YxNo;              //遥信号     
        public string m_YxName;         //遥信名称
        public int m_YxValue;           //遥信值
        public string m_YxChangeTime;   //遥信改变时间
        public int m_YxAddress;    //遥信地址


        //仅供芯唐芯片使用，即无ARM的FTU
        public int m_SrcNo;//原序号
        public int m_SingleType;  //0表示单点遥信，1表示双点遥信
        public int m_IsReverse;// 0表示取反  ，1表示不取反
        public bool m_SelectFlag;//选中标志  true选中
    }
    public class CSOEOption
    {
        public CYxOption m_YxOption = new CYxOption();
        public string m_StatusDesc = "";
        public string m_RecordTime = "";
        public CSOEOption()
        {

        }
        public CSOEOption(CSOEOption pObject)
        {
            m_YxOption.m_YxNo = pObject.m_YxOption.m_YxNo;
            m_YxOption.m_YxValue = pObject.m_YxOption.m_YxValue;
            m_YxOption.m_YxName = pObject.m_YxOption.m_YxName;
            m_YxOption.m_YxChangeTime = pObject.m_YxOption.m_YxChangeTime;
            m_StatusDesc = pObject.m_StatusDesc;
        }
    }
    public class CYcOption
    {
        public int m_YcNo;
        public float m_YcValue;
        public string m_YcName;
        public float m_YcRatio;			    /*系数：  =1 (默认)*/
        public float m_YcOffset;			/*修正值：=0 (默认)*/
        public string m_YcUnit;             //单位
        public int m_YcAddress;             //遥测地址


        //仅供芯唐芯片使用，即无ARM的FTU
        public int m_SrcNo;//原序号  
        public bool m_SelectFlag;//选中标志  true选中
    }
    public class CPowerOption
    {
        public int m_PowerNo;
        public float m_PowerValue;
        public string m_PowerName;
        public string m_PowerUnit;//单位
        public int m_PowerAddress;//电量地址
        public string m_PowerChangeTime;

        //仅供芯唐芯片使用，即无ARM的FTU
        public int m_SrcNo;//原序号  
        public bool m_SelectFlag;//选中标志  true选中
    }
    public class CDevVersion
    {
        public string m_ADSoftVersion;          //采样程序版本
        public string m_ConSoftVersion;         //通信程序版本
        public int m_CRC;                       //通信程序CRC码
        public int m_TotalRamSize;              //总内存大小
        public int m_SurplusRam;                //剩余内存大小
        public int m_RamRate;                   //内存使用百分比
        public int m_TotalDiskSize;             //总存储空间大小
        public int m_SurplusDisk;               //剩余存储空间大小
        public int m_DiskRate;                  //剩余空间百分比
        public float m_Temperature;             //装置温度
    }
    public class ParaTaskMsg
    {
        public int m_TaskNo;
        public int m_StartPalce;
        public int m_ParaCount;
        public bool m_AnswerFlag;
        public bool m_EndFlag;
    }
    public class CDownFileTaskMsg
    {
        public int m_TaskNo;
        public bool m_AnswerFlag;  //false 需要应答  true 不需要应答    
    }
    public class CerTaskMsg
    {
        public int m_TaskNo;
        public bool m_AnswerFlag;
    }
    public class CIPModule
    {
        public string m_IPString;
        public byte[] m_IP_byte = new byte[4];

        public string m_SubnetMaskString;
        public byte[] m_SubnetMask_byte = new byte[4];

        public int NetPort;
    }
     public class CMacModule
    {
        public int Port;

        public string m_MacString;
        public   byte[] m_Mac_byte = new byte[6]; 
    }
    
    public class CGateWayModule
    {
        public string m_NetSegmentString;
        public byte[] m_NetSegment_byte = new byte[4];

        public string m_SubnetMaskString;
        public byte[] m_SubnetMask_byte = new byte[4];

        public string m_GateWayString;
        public byte[] m_GateWay_byte = new byte[4];

        public int NetPort;
    }

    public class CComModule
    {
        public int ComPort;
        public int ComBaud;
    }

    public class CHostModule
    {
        public byte m_HostIndex;        //主站号
        public byte m_address;          //通讯地址
        //public byte m_HostType;         //0-串口,1-网口
        public byte m_ChanType;         //通道类型
        public byte m_PrtclType;        //规约类型

        public int LocalPort;           //本地端口
        public int RemotePort;          //远方端口

        public string m_LocalIPString1;      //本地IP
        public byte[] m_LocalIP_byte1 = new byte[4];

        public string m_LocalIPString2;      //本地IP
        public byte[] m_LocalIP_byte2 = new byte[4];

        public string m_LocalIPString3;      //本地IP
        public byte[] m_LocalIP_byte3 = new byte[4];

        public string m_RemoteIPString1;     //远方IP
        public byte[] m_RemoteIP_byte1 = new byte[4];

        public string m_RemoteIPString2;     //远方IP
        public byte[] m_RemoteIP_byte2 = new byte[4];

        public string m_RemoteIPString3;     //远方IP
        public byte[] m_RemoteIP_byte3 = new byte[4];
    }

    public enum ParaType
    {
        Boolen = 1,
        Tiny = 43,
        UTiny = 32,
        Short =33,
        UShort=45,
        Int=2,
        Uint=35,
        Long=36,
        Ulong=37,
        Float=38,
        Double=39,
        String=4,
        IP=100,
    };


    public enum TI_Type
    {
        R_4G_PA_1      = 0x85,//4G拨号参数召唤
        W_4G_PA_1      = 0x86,//4G拨号参数修改
        R_NT_PA_1      = 0x87,//网口/串口参数召唤
        W_NT_PA_1      = 0x88,//网口/串口参数修改
        R_GW_PA_1      = 0x89,//网关参数召唤
        W_GW_PA_1      = 0x8A,//网关参数修改
        R_SR_PA_1      = 0x8B,//串口参数召唤
        W_SR_PA_1      = 0x8C,//串口参数修改
        R_PI_PA_1      = 0x8D,//主站参数召唤
        W_PI_PA_1      = 0x8E,//主站参数修改
        R_TI_PA_1      = 0x8F,//终端参数召唤
        W_TI_PA_1      = 0x90,//终端参数修改
        Restart_D      = 0X91, //重启装置
        R_MC_PA_1      = 0x92,//MAC参数召唤
        W_MC_PA_1      = 0x93,//MAC参数修改
    };
    public class CParaCtrl4G
    {
        public bool m_Select = false;   //是否选择
        public ParaType m_ParaType;          //参数类型
        public ParaType m_ProParaType;       //规约参数类型
        public int m_ParaAddress;       //参数地址
        private object pValue;
        public int m_ParaLeng;          //参数长度

        public static bool IsIP(string ip)
        {
            //判断是否为IP
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public object m_ParaValue       //参数值
        {
            set
            {
                try
                {
                    switch (m_ParaType)
                    {
                        case ParaType.Boolen:       //bool，转为Byte
                            pValue = Convert.ChangeType(value, typeof(Byte));
                            m_ParaLeng = 1;
                            break;
                        case ParaType.Int:          //int
                            pValue = Convert.ChangeType(value, typeof(int));
                            m_ParaLeng = 4;
                            break;
                        case ParaType.String:      //string
                            pValue = Convert.ChangeType(value, typeof(string));
                            m_ParaLeng = ((string)pValue).Length;
                            break;
                        case ParaType.UTiny:       //Byte
                            pValue = Convert.ChangeType(value, typeof(Byte));
                            m_ParaLeng = 1;
                            break;
                        case ParaType.Short:       //short
                            pValue = Convert.ChangeType(value, typeof(short));
                            m_ParaLeng = 2;
                            break;
                        case ParaType.Tiny:        //sbyte
                            pValue = Convert.ChangeType(value, typeof(sbyte));
                            m_ParaLeng = 1;
                            break;
                        case ParaType.UShort:      //ushort
                            pValue = Convert.ChangeType(value, typeof(ushort));
                            m_ParaLeng = 2;
                            break;
                        case ParaType.Uint:        //uint
                            pValue = Convert.ChangeType(value, typeof(uint));
                            m_ParaLeng = 4;
                            break;
                        case ParaType.Long:        //long
                            pValue = Convert.ChangeType(value, typeof(long));
                            m_ParaLeng = 8;
                            break;
                        case ParaType.Ulong:       //ulong
                            pValue = Convert.ChangeType(value, typeof(ulong));
                            m_ParaLeng = 8;
                            break;
                        case ParaType.Float:       //float
                            pValue = Convert.ChangeType(value, typeof(float));
                            m_ParaLeng = 4;
                            break;
                        case ParaType.Double:      //double
                            pValue = Convert.ChangeType(value, typeof(double));
                            m_ParaLeng = 8;
                            break;
                        case ParaType.IP:        //IP地址输入形式
                            string strIP = (string)value;

                            if (IsIP(strIP.Trim()))
                            {
                                uint myIp;
                                string[] eachIP = strIP.Split('.');
                                myIp = Convert.ToByte(eachIP[3]);
                                myIp = (myIp << 8) + Convert.ToByte(eachIP[2]);
                                myIp = (myIp << 8) + Convert.ToByte(eachIP[1]);
                                myIp = (myIp << 8) + Convert.ToByte(eachIP[0]);

                                pValue = myIp;
                            }
                            else
                                pValue =null;
                           
                            m_ParaLeng = 4;
                            break;
                        default:
                            pValue = Convert.ChangeType(value, typeof(string));
                            m_ParaLeng = ((string)pValue).Length;
                            break;
                    }
                }
                catch { }
            }
            get
            {
                try
                {
                    switch (m_ParaType)
                    {
                        case ParaType.Boolen:         //bool，转为Byte
                            return (Byte)pValue;
                        case ParaType.Int:            //int
                            return (int)pValue;
                        case ParaType.String:         //string
                            return (string)pValue;
                        case ParaType.UTiny:          //Byte
                            return (Byte)pValue;
                        case ParaType.Short:          //short
                            return (short)pValue;
                        case ParaType.Tiny:           //sbyte
                            return (sbyte)pValue;
                        case ParaType.UShort:         //ushort
                            return (ushort)pValue;
                        case ParaType.Uint:           //uint
                            return (uint)pValue;
                        case ParaType.Long:          //long
                            return (long)pValue;
                        case ParaType.Ulong:         //ulong
                            return (ulong)pValue;
                        case ParaType.Float:         //float
                            return (float)pValue;
                        case ParaType.Double:        //double
                            return (double)pValue;
                        case ParaType.IP:
                            byte[] ipGroup =new byte[4];
                            uint ipAddress = (uint)pValue;
                            string m_ip;
                            ipGroup[0] = Convert.ToByte(ipAddress & 0xff);
                            ipAddress = ipAddress >> 8;
                            ipGroup[1] = Convert.ToByte(ipAddress & 0xff);
                            ipAddress = ipAddress >> 8;
                            ipGroup[2] = Convert.ToByte(ipAddress & 0xff);
                            ipAddress = ipAddress >> 8;
                            ipGroup[3] = Convert.ToByte(ipAddress & 0xff);

                            m_ip = ipGroup[0].ToString() + "." + ipGroup[1].ToString() + "." + ipGroup[2].ToString() + "." + ipGroup[3].ToString();
       
                            return m_ip;
                        default:
                            return (string)pValue;
                    }
                }
                catch { return (string)pValue; }
            }
        }


        public int GetValueBuff(ref Byte[] pBuff)
        {
            switch (m_ParaType)
            {
                case ParaType.Boolen:         //bool，转为Byte
                    pBuff = System.BitConverter.GetBytes((Byte)m_ParaValue);
                    break;
                case ParaType.Int:            //int
                    pBuff = System.BitConverter.GetBytes((int)m_ParaValue);
                    break;
                case ParaType.String:         //string
                    pBuff = Encoding.Default.GetBytes((string)m_ParaValue);
                    break;
                case ParaType.UTiny:         //Byte
                    pBuff = System.BitConverter.GetBytes((Byte)m_ParaValue);
                    break;
                case ParaType.Short:         //short
                    pBuff = System.BitConverter.GetBytes((short)m_ParaValue);
                    break;
                case ParaType.Tiny:         //sbyte
                    pBuff = System.BitConverter.GetBytes((sbyte)m_ParaValue);
                    break;
                case ParaType.UShort:       //ushort
                    pBuff = System.BitConverter.GetBytes((ushort)m_ParaValue);
                    break;
                case ParaType.Uint:        //uint
                    pBuff = System.BitConverter.GetBytes((uint)m_ParaValue);
                    break;
                case ParaType.Long:        //long
                    pBuff = System.BitConverter.GetBytes((long)m_ParaValue);
                    break;
                case ParaType.Ulong:       //ulong
                    pBuff = System.BitConverter.GetBytes((ulong)m_ParaValue);
                    break;
                case ParaType.Float:       //float
                    pBuff = System.BitConverter.GetBytes((float)m_ParaValue);
                    break;
                case ParaType.Double:     //double
                    pBuff = System.BitConverter.GetBytes((double)m_ParaValue);
                    break;
                case ParaType.IP:         //IP转换成四字节的uint
                    /*
                    uint myIp;
                    string str_IP= (string)m_ParaValue;
                    string[] eachIP = str_IP.Split('.');
                    myIp = Convert.ToByte(eachIP[0]);
                    myIp = (myIp << 8) + Convert.ToByte(eachIP[1]);
                    myIp = (myIp << 8) + Convert.ToByte(eachIP[2]);
                    myIp = (myIp << 8) + Convert.ToByte(eachIP[3]);
                    pBuff = System.BitConverter.GetBytes((uint)myIp);
                     * */
                    //感觉这样就可以了，不需要上面那么复杂
                    pBuff = System.BitConverter.GetBytes((uint)pValue);
                    break;
                default:
                    pBuff = Encoding.Default.GetBytes((string)m_ParaValue);
                    break;
            }
            return m_ParaLeng;
        }
    }
}

   

