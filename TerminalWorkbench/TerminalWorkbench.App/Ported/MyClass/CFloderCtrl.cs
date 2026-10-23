using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Generic;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public class CFloderCtrl
    {
        public string m_FloderKey;              //文件夹目录关键字
        public string m_FloderName;             //文件夹名字
        public string m_FloderPath;             //文件夹路径
        public string m_FileType;               //文件后缀类型
        public int m_FloaderType;               //文件夹存储的文件类型
        public TreeNode m_FloderNode = null;
        public List<CFileCtrl> m_FileList = new List<CFileCtrl>();      //文件夹下文件

        public int m_NowReadPalce = 0;                                   //当前读取文件
        public List<CFileCtrl> m_TempFileList = new List<CFileCtrl>();   //临时文件名列表

        public void ClearTempFileList()
        {
            m_TempFileList.Clear();
        }
        public void ClearFileList()
        {
            m_FileList.Clear();
        }
        public void AddNewWaitReadFile(CFileCtrl pFile)
        {
            pFile.m_ReadResult = "未读取";
            m_TempFileList.Add(pFile);
        }
        public void AddNewFile(string strFileName)
        {
            CFileCtrl pNewFile = new CFileCtrl();
            pNewFile.m_FileTypeName = strFileName;
            pNewFile.m_FileName = pNewFile.m_FileTypeName.Remove(pNewFile.m_FileTypeName.LastIndexOf("."));
            pNewFile.m_FilePath = m_FloderPath + "\\" + pNewFile.m_FileTypeName;
            pNewFile.m_IsFileExist = pNewFile.CheckFileExist();
            pNewFile.m_ParentFloder = this;
            pNewFile.m_FileType = m_FloaderType;
            m_FileList.Add(pNewFile);
        }
        public void IniFileTreeView(bool bExpand = false)
        {
            int i;
            CFileCtrl pFile;
            if (m_FloderNode == null)
                return;
            m_FloderNode.Nodes.Clear();
            for (i = 0; i < m_FileList.Count; i++)
            {
                pFile = m_FileList[i];
                pFile.m_FileNode = new TreeNode();
                pFile.m_FileNode.Text = pFile.m_FileName;
                pFile.m_FileNode.Tag = pFile;
                m_FloderNode.Nodes.Add(pFile.m_FileNode);
            }
            if (bExpand)
                m_FloderNode.Expand();
        }
        public void IniReadFloderFile()
        {
            try
            {
                ClearFileList();
                string[] files = Directory.GetFiles(m_FloderPath);//得到文件列表
                foreach (string FileName in files)//循环文件
                {
                    if (FileName.LastIndexOf(".") != -1)
                    {
                        if (m_FileType.IndexOf(FileName.Substring(FileName.LastIndexOf("."))) != -1)
                        {
                            string strName = FileName.Substring(FileName.LastIndexOf("\\") + 1);
                            AddNewFile(strName);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        public bool CheckFileExits(string strFileName)
        {
            string strName = strFileName.Substring(0, strFileName.LastIndexOf("."));
            for (int i = 0; i < m_FileList.Count; i++)
            {
                string strOldName = m_FileList[i].m_FileTypeName.Substring(0, m_FileList[i].m_FileTypeName.LastIndexOf("."));
                if (strOldName == strName)
                    return true;
            }
            return false;
        }
        public void CopyFile(string NewFloder)
        {
            try
            {
                string[] files = Directory.GetFiles(m_FloderPath);//得到文件列表
                foreach (string FileName in files)//循环文件
                {
                    string strName = FileName.Substring(FileName.LastIndexOf("\\") + 1);
                    System.IO.File.Copy(FileName, NewFloder + "\\" + strName, true);
                }
            }
            catch
            {
                return;
            }
        }
    }
}
