using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtuTestManager
{
    public class CCheckdata
    {

        public int m_LineNo; //线路号
        public string m_Name;//名称
        public int m_Index;//索引
        public float m_Default;//标准值
        public ushort m_Ratio;//系数
        public float m_Real;//实测值    
        public byte Is_Check;//是否校验
        public byte Is_Down;//是否下装
        public float m_Mul_A;   //系数A
        public float m_Mul_B;   //系数B
    }

    public class CDCCheckdata
    {
        public int m_Index;//索引
        public string m_Name;//名称        
        public float m_Default;//标准值
        public float m_Real;//实测值    
        public byte Is_Check;//是否校验
    }

    public class CPhasedata
    {
        public int m_LineNo; //线路号
        public string m_Name;//相角名称
        public float m_Value;//绝对相角度数  
    }

}
