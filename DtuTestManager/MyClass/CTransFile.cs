using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DtuTestManager
{
    public class CFileData
    {
        public int PackNo;
        public int DataLength;
        public int Offset;
        public byte[] DataBuff;
    }
    public class CTransFile
    {
        public string m_FileName;       //文件名
        public int m_FileNameLength;//文件名长度
        public int m_FileID;//文件ID
        public long m_FileSize;//文件大小
        public int CurrentIndex;//当前传第几包
        public int SumFrames;//总包数
        public List<CFileData> m_data = new List<CFileData>();

    }
}
