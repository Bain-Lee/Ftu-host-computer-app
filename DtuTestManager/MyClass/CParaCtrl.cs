using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtuTestManager
{
    public  class CParaCtrl
    {
        public int m_ParaListType;      //参数表类型
        public int m_ParaNo;            //参数序号
        public int m_ParaType;          //参数类型
        public int m_ParaAddress;       //参数地址
        public string m_ParaName;       //参数名称
        public int m_ParaLeng;          //参数长度
        public string m_ParaUnit;       //参数单位
        public string m_ParaLimit;      //上下限描述
        public string m_ParaTableName;   //参数表名
        public bool m_Select = false;   //是否选择

        private object pValue;
        public object m_ParaValue       //参数值
        {
            set
            {
                switch (m_ParaType)
                {
                    case 1:         //bool，转为Byte
                        pValue = Convert.ChangeType(value,typeof(Byte));
                        m_ParaLeng = 1;
                        break;
                    case 2:         //int
                        pValue = Convert.ChangeType(value, typeof(int));
                        m_ParaLeng = 4;
                        break;
                    case 4:         //string
                        pValue = Convert.ChangeType(value, typeof(string));
                        m_ParaLeng = ((string)pValue).Length;
                        break;
                    case 32:        //Byte
                        pValue = Convert.ChangeType(value, typeof(Byte));
                        m_ParaLeng = 1;
                        break;
                    case 33:        //short
                        pValue = Convert.ChangeType(value, typeof(short));
                        m_ParaLeng = 2;
                        break;
                    case 43:        //sbyte
                        pValue = Convert.ChangeType(value, typeof(sbyte));
                        m_ParaLeng = 1;
                        break;
                    case 45:        //ushort
                        pValue = Convert.ChangeType(value, typeof(ushort));
                        m_ParaLeng = 2;
                        break;
                    case 35:        //uint
                        pValue = Convert.ChangeType(value, typeof(uint));
                        m_ParaLeng = 4;
                        break;
                    case 36:        //long
                        pValue = Convert.ChangeType(value, typeof(long));
                        m_ParaLeng = 8;
                        break;
                    case 37:        //ulong
                        pValue = Convert.ChangeType(value, typeof(ulong));
                        m_ParaLeng = 8;
                        break;
                    case 38:        //float
                        pValue = Convert.ChangeType(value, typeof(float));
                        m_ParaLeng = 4;
                        break;
                    case 39:        //double
                        pValue = Convert.ChangeType(value, typeof(double));
                        m_ParaLeng = 8;
                        break;
                    default:
                        pValue = Convert.ChangeType(value, typeof(string));
                        m_ParaLeng = ((string)pValue).Length;
                        break;
                }
            }
            get
            {
                switch (m_ParaType)
                {
                    case 1:         //bool，转为Byte
                        return (Byte)pValue;
                    case 2:         //int
                        return (int)pValue;
                    case 4:         //string
                        return (string)pValue;
                    case 32:        //Byte
                        return (Byte)pValue;
                    case 33:        //short
                        return (short)pValue;
                    case 43:        //sbyte
                        return (sbyte)pValue;
                    case 45:        //ushort
                        return (ushort)pValue;
                    case 35:        //uint
                        return (uint)pValue;
                    case 36:        //long
                        return (long)pValue;
                    case 37:        //ulong
                        return (ulong)pValue;
                    case 38:        //float
                        return (float)pValue;
                    case 39:        //double
                        return (double)pValue;
                    default:
                        return (string)pValue;
                }
            }
        }
        public string  GetValueTypeName()
        {
            switch (m_ParaType)
            {
                case 1:         //bool，转为Byte
                    return "bool";
                case 2:         //int
                    return "int";
                case 4:         //string
                    return "string";
                case 32:        //Byte
                    return "Byte";
                case 33:        //short
                    return "short";
                case 43:        //sbyte
                    return "sbyte";
                case 45:        //ushort
                    return "ushort";
                case 35:        //uint
                    return "uint";
                case 36:        //long
                    return "long";
                case 37:        //ulong
                    return "ulong";
                case 38:        //float
                    return "float";
                case 39:        //double
                    return "double";
                default:
                    return "string";
            }
        }
        public int GetValueBuff(ref Byte[] pBuff)
        {
            switch (m_ParaType)
            {
                case 1:         //bool，转为Byte
                    pBuff = System.BitConverter.GetBytes((Byte)m_ParaValue);
                    break;
                case 2:         //int
                    pBuff = System.BitConverter.GetBytes((int)m_ParaValue);
                    break;
                case 4:         //string
                    pBuff = Encoding.Default.GetBytes((string)m_ParaValue);
                    break;
                case 32:        //Byte
                    pBuff = System.BitConverter.GetBytes((Byte)m_ParaValue);
                    break;
                case 33:        //short
                    pBuff = System.BitConverter.GetBytes((short)m_ParaValue);
                    break;
                case 43:        //sbyte
                    pBuff = System.BitConverter.GetBytes((sbyte)m_ParaValue);
                    break;
                case 45:        //ushort
                    pBuff = System.BitConverter.GetBytes((ushort)m_ParaValue);
                    break;
                case 35:        //uint
                    pBuff = System.BitConverter.GetBytes((uint)m_ParaValue);
                    break;
                case 36:        //long
                    pBuff = System.BitConverter.GetBytes((long)m_ParaValue);
                    break;
                case 37:        //ulong
                    pBuff = System.BitConverter.GetBytes((ulong)m_ParaValue);
                    break;
                case 38:        //float
                    pBuff = System.BitConverter.GetBytes((float)m_ParaValue);
                    break;
                case 39:        //double
                    pBuff = System.BitConverter.GetBytes((double)m_ParaValue);
                    break;
                default:
                    pBuff = Encoding.Default.GetBytes((string)m_ParaValue);
                    break;
            }
            return m_ParaLeng;
        }

    }
}
