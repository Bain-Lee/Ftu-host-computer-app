using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DtuTestManager
{
    public class CFileBuff
    {
        public Byte[] m_DataBuff = null;
        public int m_DataLeng = 0;
        public int m_PacketNo = -1;
    }
    public class CFixDataStruct
	{
		public string m_DataAddress = "";
        public string m_DataValue = "";
	}
    public class CFixData
    {
        public static int m_MaxCount = 0;
        public string m_TimeFlag = "";
        public int m_DataCount = 0;
        public List<CFixDataStruct> m_DataList = new List<CFixDataStruct>();
    }
    public class CFileCtrl
    {
        public string ReadFileName;
        public string ReadFileVersion;
        public string TerminalID;


        public int ColumnCnt;     //专为定点数据显示的计数器
        public int ColumnGap;     //专为定点数据显示列表的间隔数  文件类型为2
        public int ColumnMax;     //最大组数

        public string m_FileName;               //不带文件后缀的文件名
        public string m_FileTypeName;           //带有文件后缀的完整文件名
        public string m_FilePath;               //文件路径
        public int m_FileType;                  //文件类型
        public string m_RecordTime;             //文件修改时间
        public bool m_IsFileExist;              //文件是否存在
        public bool m_ReadSelect = false;       //是否选择读取
        public TreeNode m_FileNode = null;
        public CFloderCtrl m_ParentFloder = null;

        public List<CFileBuff> m_FileBuff = new List<CFileBuff>();
        public int m_FilePointNo = 0;
        public int m_FileRII = 0;
        public int m_ReadFileLeng = 0;              //已接收文件长度
        public int m_FileSize = 0;                  //文件大小
        public bool m_EndFlag = false;
        public bool m_ReadSuccess = false;      //读取是否成功
        public bool m_StartFlag = false;        //读取文件开始
        public bool m_bHaveRead = false;        //是否已经读过
        public string m_ReadResult;             //读取文件结果描述
        public string[] m_StrHead = null;
        public int[] m_ColumnsWith = null;
        public List<string> FileContent = new List<string>();
        public bool m_bHaveReadFileCOntent = false;        //是否已经解析过文件内容
        public CWaveFileInfo m_CWaveInfo = null;   //录波文件专用
        public List<CFixData> m_FixDataList = new List<CFixData>();
        public List<float>[] G_fixpt_data = null;
        public List<float>[] G_frz_data = null;
        public List<float>[] LineLoss_G_frz_data = null;

        public CFileCtrl()
        {
            ColumnCnt = 0;
            ColumnGap =8;
        }
        public void GetRowContent(ref string[] strConteng)
        {

        }
        public void IniCloumnsHead()
        {
            switch (m_FileType % 100)
            {
                case 0:
                    break;
                case 1:
                    m_StrHead = new string[6];
                    m_ColumnsWith = new int[6];
                    m_StrHead[0] = "序号";
                    m_ColumnsWith[0] = 40;

                    m_StrHead[1] = "地址";
                    m_ColumnsWith[1] = 180;

                    m_StrHead[2] = "最大值";
                    m_ColumnsWith[2] = 150;

                    m_StrHead[3] = "最大值发生时间";
                    m_ColumnsWith[3] = 200;

                    m_StrHead[4] = "最小值";
                    m_ColumnsWith[4] = 150;

                    m_StrHead[5] = "最小值发生时间";
                    m_ColumnsWith[5] = 200;
                    break;
                case 2:
                    break;
                case 5:
                    m_StrHead = new string[5];
                    m_ColumnsWith = new int[5];
                    m_StrHead[0] = "序号";
                    m_ColumnsWith[0] = 40;

                    m_StrHead[1] = "类型";
                    m_ColumnsWith[1] = 40;

                    m_StrHead[2] = "时间";
                    m_ColumnsWith[2] = 150;

                    m_StrHead[3] = "内容";
                    m_ColumnsWith[3] = 200;

                    m_StrHead[4] = "值";
                    m_ColumnsWith[4] = 150;
                    break;
                case 6:
                    m_StrHead = new string[4];
                    m_ColumnsWith = new int[4];
                    m_StrHead[0] = "序号";
                    m_ColumnsWith[0] = 40;

                    m_StrHead[1] = "SOE发生时间";
                    m_ColumnsWith[1] = 150;

                    m_StrHead[2] = "地址";
                    m_ColumnsWith[2] = 200;

                    m_StrHead[3] = "值";
                    m_ColumnsWith[3] = 150;
                    break;
                case 7:
                    m_StrHead = new string[5];
                    m_ColumnsWith = new int[5];
                    m_StrHead[0] = "序号";
                    m_ColumnsWith[0] = 40;

                    m_StrHead[1] = "遥控发生时间";
                    m_ColumnsWith[1] = 150;

                    m_StrHead[2] = "名称(地址)";
                    m_ColumnsWith[2] = 200;

                    m_StrHead[3] = "操作类型";
                    m_ColumnsWith[3] = 150;

                    m_StrHead[4] = "操作命令";
                    m_ColumnsWith[4] = 150;
                    break;
                case 13:
                    m_StrHead = new string[4];
                    m_ColumnsWith = new int[4];
                    m_StrHead[0] = "序号";
                    m_ColumnsWith[0] = 40;

                    m_StrHead[1] = "类型";
                    m_ColumnsWith[1] = 40;

                    m_StrHead[2] = "时间";
                    m_ColumnsWith[2] = 150;

                    m_StrHead[3] = "内容";
                    m_ColumnsWith[3] = 800;                   
                    break;
            }

        }
        public void AddReadBuff(CFileBuff pDataBuff)
        {
            if (m_FileBuff.Count == 0)
            {
                m_FileBuff.Add(pDataBuff);
                m_ReadFileLeng += pDataBuff.m_DataLeng;
                return;
            }
            for (int i = m_FileBuff.Count; i > 0; i--)
            {
                CFileBuff temp = m_FileBuff[i - 1];
                if (temp.m_PacketNo > pDataBuff.m_PacketNo)
                {
                    continue;
                }
                else if (temp.m_PacketNo < pDataBuff.m_PacketNo)
                {
                    m_FileBuff.Insert(i, pDataBuff);
                    m_ReadFileLeng += pDataBuff.m_DataLeng;
                    return;
                }
                else
                {
                    return;
                }            
            } 
        }
        public void IniReadFile()
        {
            m_EndFlag = false;
            m_StartFlag = false;
            m_ReadSuccess = false;
            m_ReadResult = "等待...";
            m_ReadFileLeng = 0;
            m_FileBuff.Clear();
        }
        public bool CheckFileExist()
        {
            m_IsFileExist = false;
            if (File.Exists(m_FilePath)) m_IsFileExist = true;
            return m_IsFileExist;
        }
        public void SaveFile()
        {
            int OffSet = 0;
            m_FilePath = m_FilePath.Replace("/", "");
            int Length = m_FilePath.Length;

            BinaryWriter FileWrite = new BinaryWriter(new FileStream(m_FilePath, FileMode.Create));
            for (int i = 0; i < m_FileBuff.Count; i++)
            {
                FileWrite.Write(m_FileBuff[i].m_DataBuff);
                OffSet += m_FileBuff[i].m_DataLeng;
            }
            FileWrite.Close();
        }
        public void ReadFileContent()
        {
            string[] RowValue = null;
            string str = "";
            try
            {
                using (FileStream fsRead = new FileStream(m_FilePath, FileMode.Open))
                {
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);
                    str = CGlobal.BytesToString(heByte);
                
                string S = str.ToString().Trim();
                if (S == "")
                    return;
                S = S.Replace("\r","");
                RowValue = S.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                }
            }
            catch
            { }
            if (RowValue.Length <= 0)
                return;


            switch (m_FileType % 100)
            {
               
                case 1: 
                case 2:        
                case 3:       
                case 4:               
                case 6:       
                case 7:       
                case 8:
                case 9:
                case 10:
                case 11:
                case 12: 
                    ReadFileName = RowValue[0].Split(',')[0];
                    ReadFileVersion = RowValue[0].Split(',')[1];
                    TerminalID = RowValue[1].Split(',')[0];
                    if (RowValue.Length < 2)
                        return;
                    break;
                case 13:
                     ReadFileName = RowValue[0].Split(',')[0];
                    ReadFileVersion = RowValue[0].Split(',')[1];                   
                    if (RowValue.Length <1)
                        return;
                    break;
            }

            switch (m_FileType % 100)
            {
                case 0:         //录波文件
                    DowithWave(RowValue);
                    break;
                case 1:         //极值文件
                    DowithExv(RowValue);
                    break;
                case 2:         //定点记录文件
                    Dowithfixpt(RowValue);
                    break;
                case 3:         //日冻结能量记录文件
                    Dowithfrz(RowValue);
                    break;
                case 4:         //功率反向电能量冻结值
                    Dowithflowrev(RowValue);
                    break;
                case 5:         //日志文件
                    DowithLog(RowValue);
                    break;
                case 6:         //SOE记录文件
                    DowithSOE(RowValue);
                    break;
                case 7:         //遥控操作记录文件
                    DowithYKRecord(RowValue);
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    DowithLineLossFreeze(RowValue);//线损模块冻结文件
                    break;
                case 13://线损事件记录
                    DowithLineLossLog(RowValue);
                    break;
            }
        }
        //录波
        public void DowithWave(string[] RowValue)
        {
            string path = m_FilePath;
            path = path.Substring(0, path.Length - 3) + "dat";

            m_CWaveInfo = new CWaveFileInfo();
            m_CWaveInfo.DoWithLuboFile(RowValue, path);

            m_bHaveReadFileCOntent = m_CWaveInfo.m_Success;
        }
        public void ShowLuboForm()
        {
            if (m_CWaveInfo == null || m_CWaveInfo.m_Success == false) return;
            CSoePicForm pForm = new CSoePicForm();
            pForm.m_File = this;
            pForm.ShowDialog();
        }
        //极值
        public void DowithExv(string[] RowValue)
        {
            try
            {
                string[] maxV = new string[1000];
                string[] minV = new string[1000];
                string strAddr;
                maxV = RowValue[2].Split(','); //最大值
                minV = RowValue[3].Split(','); //最小值
                int MaxNum = Convert.ToInt32(maxV[0]); //最大值信息对象数目
                for (int i = 0; i < MaxNum; i++)
                {
                    strAddr = maxV[1 + i * 3];
                    int Addr = Convert.ToInt32(strAddr, 16);

                    strAddr = strAddr + "(" + GetName(Addr, 1) + ")";

                    FileContent.Add(strAddr);
                    FileContent.Add(maxV[2 + i * 3]); //最大值
                    FileContent.Add(maxV[3 + i * 3]);//最大值时间
                    FileContent.Add(minV[2 + i * 3]); //最小值
                    FileContent.Add(minV[3 + i * 3]);//最小值时间             
                }
                IniCloumnsHead();
                m_bHaveReadFileCOntent = true;
            }
            catch { }
        }
        //定点
        public void Dowithfixpt(string[] RowValue)
        {
            try
            {
                int n = 0;
                int i,j;
                m_FixDataList.Clear();
                string[] strhead = new string[2000];
                n = Convert.ToInt32(RowValue[1].Split(',')[2]);//条目数

                for(i=2;i<RowValue.Length;i++)
                {
                    CFixData pData = new CFixData();
                    m_FixDataList.Add(pData);
                    string[] pstrList = RowValue[i].Split(',');
                    pData.m_DataCount = Convert.ToInt32(pstrList[0]);
                    pData.m_TimeFlag = pstrList[1];
                    if (pData.m_DataCount > CFixData.m_MaxCount) CFixData.m_MaxCount = pData.m_DataCount;
                    for (j = 0; j < pData.m_DataCount; j++)
                    {
                        try
                        {
                            CFixDataStruct pStruct = new CFixDataStruct();
                            pStruct.m_DataAddress = pstrList[j * 2 + 2];
                            pStruct.m_DataValue = pstrList[j * 2 + 3];
                            pData.m_DataList.Add(pStruct);
                        }
                        catch
                        { }
                    }
                }

                

                ColumnMax = CFixData.m_MaxCount / ColumnGap;
                if ((CFixData.m_MaxCount % ColumnGap) > 0) ColumnMax++;
                m_bHaveReadFileCOntent = true;

                G_fixpt_data = new List<float>[CFixData.m_MaxCount];
                for (i = 0; i < G_fixpt_data.Length; i++)
                {
                    G_fixpt_data[i] = new List<float>();
                    for (j = 0; j < m_FixDataList.Count; j++)
                    {
                        if (i < m_FixDataList[j].m_DataList.Count)
                            G_fixpt_data[i].Add(Convert.ToSingle(m_FixDataList[j].m_DataList[i].m_DataValue));
                    }
                }
            }
            catch
            { }
        }              
        //线损模块冻结文件     
        public void DowithLineLossFreeze(string[] RowValue)
        {
            try
            {
                int n = 0;
                string[] strhead = new string[200];
                string[] ts = new string[1000];
                n = Convert.ToInt32(RowValue[2].Split(',')[0]);//数目
                LineLoss_G_frz_data = new List<float>[n];
                for (int i = 0; i < n; i++)
                {
                    LineLoss_G_frz_data[i] = new List<float>();
                }

                for (int i = 2; i < RowValue.Length; i++)
                {
                    ts = RowValue[i].Split(',');

                    FileContent.Add(ts[1].Trim()); //时间

                    for (int j = 0; j < n; j++)
                    {
                        if (i < 3)
                        {
                            // int Addr = Convert.ToInt32(ts[2 + j * 2], 16);

                            // strhead[j] = GetName(Addr - CGlobal.m_Device.m_Ptr.m_PowerStartAddress, 2);
                            strhead[j] = ts[2 + j * 2];

                        }
                        string Str = ts[j * 2 + 3];
                       string  str1=Str.Substring(0,1);
                       string str2 = Str.Substring(1, Str.Length-1);
                       if ((str1 == "+") || (str1 == "-"))
                       {
                           Str=str1 + str2.Trim();                       
                       }
                       FileContent.Add(Str.Trim());
                        //LineLoss_G_frz_data[j].Add(float.Parse(ts[j * 2 + 3]));

                    }
                }
                m_StrHead = new string[2 + n];
                m_ColumnsWith = new int[2 + n];
                m_StrHead[0] = "序号";
                m_ColumnsWith[0] = 40;

                m_StrHead[1] = "时间";
                m_ColumnsWith[1] = 160;
                for (int i = 0; i < n; i++)
                {
                    m_StrHead[2 + i] = strhead[i];
                    m_ColumnsWith[2 + i] = 80;
                }
                m_bHaveReadFileCOntent = true;
            }
            catch
            { }
        }
        //日冻结
        public void Dowithfrz(string[] RowValue)
        {
            try
            {
                int n = 0;
                string[] strhead = new string[200];
                string[] ts = new string[1000];   
                n = Convert.ToInt32(RowValue[2].Split(',')[0]);//数目
                G_frz_data = new List<float>[n];
                for (int i = 0; i < n; i++)
                {
                    G_frz_data[i] = new List<float>();
                }
                
                for (int i = 2; i < RowValue.Length; i++)
                {
                    ts = RowValue[i].Split(',');
                  
                    FileContent.Add(ts[1]); //时间

                    for (int j = 0; j < n; j++)
                    {
                        if (i < 3)
                        {
                           // int Addr = Convert.ToInt32(ts[2 + j * 2], 16);

                           // strhead[j] = GetName(Addr - CGlobal.m_Device.m_Ptr.m_PowerStartAddress, 2);
                            strhead[j] = ts[2 + j * 2];

                        }
                        FileContent.Add(ts[j * 2 + 3]);
                        G_frz_data[j].Add(float.Parse(ts[j * 2 + 3]));

                    }
                }
                m_StrHead = new string[2 + n];
                m_ColumnsWith = new int[2 + n];
                m_StrHead[0] = "序号";
                m_ColumnsWith[0] = 40;

                m_StrHead[1] = "时间";
                m_ColumnsWith[1] = 160;
                for (int i = 0; i < n; i++)
                {
                    m_StrHead[2 + i] = strhead[i];
                    m_ColumnsWith[2 + i] = 120;
                }
                m_bHaveReadFileCOntent = true;
            }
            catch
            { }
        }
        //功率反向电能量冻结值
        public void Dowithflowrev(string[] RowValue)
        {
            try
            {
                int n = 0;
                string[] strhead = new string[200];
                string[] ts = new string[1000];
                for (int i = 1; i < RowValue.Length; i++)
                {
                    ts = RowValue[i].Split(',');
                    n = Convert.ToInt32(ts[0]);//数目  
                    for (int j = 0; j < n; j++)
                    {
                        if (i < 1)
                        {
                            int Addr = Convert.ToInt32(ts[1 + j * 3], 16);
                            strhead[j] = GetName(Addr, 2);
                        }
                        FileContent.Add(ts[(j + 1) * 3] + "  " + ts[(j + 1) * 3 - 1]);
                    }
                }

                m_StrHead = new string[1 + n];
                m_ColumnsWith = new int[1 + n];
                m_StrHead[0] = "序号";
                m_ColumnsWith[0] = 40;
                for (int i = 0; i < n; i++)
                {
                    m_StrHead[1 + i] = strhead[i];
                    m_ColumnsWith[1 + i] = 120;
                }
                m_bHaveReadFileCOntent = true;
            }
            catch { }
        }

        public void DowithLineLossLog(string[] RowValue)
        {
            try
            {
                string[] ts = new string[3];
                for (int i = 1; i < RowValue.Length; i++)
                {
                    ts = RowValue[i].Split(',');
                    FileContent.Add(ts[0].Trim());
                    FileContent.Add(ts[1].Trim());
                    ts[2] = RowValue[i].Replace(ts[0] + "," + ts[1] + ",", string.Empty);
                    FileContent.Add(ts[2].Trim());                    
                }
                IniCloumnsHead();
                m_bHaveReadFileCOntent = true;
            }
            catch { }
        }        
        //日志
        public void DowithLog(string[] RowValue)
        {
            try
            {
                string[] ts = new string[4];
                for (int i = 1; i < RowValue.Length; i++)
                {
                    ts = RowValue[i].Split(',');
                    FileContent.Add(ts[0]);
                    FileContent.Add(ts[1]);
                    FileContent.Add(ts[2]);
                    FileContent.Add(ts[3]);
                }
                IniCloumnsHead();
                m_bHaveReadFileCOntent = true;
            }
            catch { }
        }
        //SOE
        public void DowithSOE(string[] RowValue)
        {
            try
            {
                string[] ts = new string[3];
                for (int i = 2; i < RowValue.Length; i++)
                {
                    ts = RowValue[i].Split(',');

                    int Addr = Convert.ToInt32(ts[0], 16);
                    ts[0] = ts[0] + "(" + GetName(Addr, 3) + ")";
                    FileContent.Add(ts[2]);//时间
                    FileContent.Add(ts[0]);//时间
                    FileContent.Add(ts[1]);//值             
                }
                IniCloumnsHead();
                m_bHaveReadFileCOntent = true;
            }
            catch { }
        }
        private string GetName(int nIndex, int nType)
        {
            string strName = "";
            int i;
            switch (nType)
            {
               
               
                case 1:
                    strName = "未知";
                    for (i = 0; i < CGlobal.m_Device.m_YcList.Count; i++)
                    {
                        if (nIndex == CGlobal.m_Device.m_YcList[i].m_YcAddress)
                        {
                            strName = CGlobal.m_Device.m_YcList[i].m_YcName;
                            break;
                        }
                    }
                    /*
                    if (nIndex < 0 || nIndex >= CGlobal.m_Device.m_YcList.Count)
                    {
                        strName = "未知";
                    }
                    else
                    {
                        strName = CGlobal.m_Device.m_YcList[nIndex].m_YcName;
                    }*/
                    break;
                case 2:
                    strName = "未知";
                    for (i = 0; i < CGlobal.m_Device.m_PowerList.Count; i++)
                    {
                        if (nIndex == CGlobal.m_Device.m_PowerList[i].m_PowerAddress)
                        {
                            strName = CGlobal.m_Device.m_PowerList[i].m_PowerName;
                            break;
                        }
                    }
                    /*
                    if (nIndex < 0 || nIndex >= CGlobal.m_Device.m_PowerList.Count)
                    {
                        strName = "未知";
                    }
                    else
                    {
                        strName = CGlobal.m_Device.m_PowerList[nIndex].m_PowerName;
                    }
                    */
                    break;
                case 3:
                    strName = "未知";
                    for (i = 0; i < CGlobal.m_Device.m_YxList.Count; i++)
                    {
                        if (nIndex == CGlobal.m_Device.m_YxList[i].m_YxAddress)
                        {
                            strName = CGlobal.m_Device.m_YxList[i].m_YxName;
                            break;
                        }
                    }
                    /*
                        if (nIndex < 0 || nIndex >= CGlobal.m_Device.m_YxList.Count)
                        {
                            strName = "未知";
                        }
                        else
                        {
                            strName = CGlobal.m_Device.m_YxList[nIndex].m_YxName;
                        }*/
                    break;
                case 4:
                    strName = "未知";
                    for (i = 0; i < CGlobal.m_Device.m_YkList.Count; i++)
                    {
                        if (nIndex == CGlobal.m_Device.m_YkList[i].m_YkAddress)
                        {
                            strName = CGlobal.m_Device.m_YkList[i].m_YkName;
                            break;
                        }
                    }
                    /*
                    if (nIndex < 0 || nIndex >= CGlobal.m_Device.m_YkList.Count)
                    {
                        strName = "未知";
                    }
                    else
                    {
                        strName = CGlobal.m_Device.m_YkList[nIndex].m_YkName;
                    }*/
                    break;

            }
            return strName;
          
        }
        //YK记录
        public void DowithYKRecord(string[] RowValue)
        {
            try
            {
                string[] ts = new string[4];
                for (int i = 2; i < RowValue.Length; i++)
                {

                    ts = RowValue[i].Split(',');
                    FileContent.Add(ts[3]);//发生时间

                    int Addr = Convert.ToInt32(ts[0], 16);
                    ts[0] = ts[0] + "(" + GetName(Addr, 4) + ")";
                    FileContent.Add(ts[0]);
                    FileContent.Add(ts[1]);  //操作类型                  
                    FileContent.Add(ts[2]);  //操作命令
                }
                IniCloumnsHead();
                m_bHaveReadFileCOntent = true;
            }
            catch
            { }
        }
    }
}
