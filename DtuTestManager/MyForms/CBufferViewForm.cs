using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;     //浮动窗口
using System.IO;

namespace DtuTestManager
{
    public partial class CBufferViewForm : DockContent
    {
        public  bool m_FreshBuff = true;       
        public CBufferViewForm()
        {
            InitializeComponent();
        }
        public void SaveNowBuff()
        {
            try
            {
                string SavePath;

                SaveFileDialog SaveForm = new SaveFileDialog();
                SaveForm.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                SaveForm.Filter = "txt文件 (*.txt)|*.txt";
                SaveForm.FileName = "实时报文.txt";
                if(SaveForm.ShowDialog() == DialogResult.OK)
                {
                    SavePath = SaveForm.FileName;
                }
                else 
                    return;
                FileStream SaveFile = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(SaveFile, Encoding.Default);
                SaveFile.SetLength(0);
                streamWriter.Write(BufferText.Text);
                streamWriter.Flush();
                streamWriter.Close();
                SaveFile.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void ShowBuffer(string strBuffName, Byte[] pBuffer, int nLeng)
        {
            if (m_FreshBuff == false || nLeng <= 0) return;
            string strOut;
            int i;
            strOut = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strOut += "  " + strBuffName + "  长度：" + nLeng.ToString();
            strOut += "\r\n";
            for (i = 0; i < nLeng; i++)
            {
                strOut += String.Format("{0:X2}", pBuffer[i]);
                strOut += " ";
            }           
            BufferText.Invoke
            (
                  new MethodInvoker
                  (
                      delegate
                      {                          
                          BufferText.AppendText(strOut + "\r\n");                         
                      }
                  )
            );
        }
        public void ShowTips(string strInfoTips)
        {
            if (m_FreshBuff == false) return;
            BufferText.Invoke
            (
                  new MethodInvoker
                  (
                      delegate
                      {
                          BufferText.AppendText(strInfoTips + "\r\n");
                      }
                  )
            );

        }
        private void BtnStartFreshBuff_Click(object sender, EventArgs e)
        {
            m_FreshBuff = true;
            BufferText.AppendText("开始刷新\r\n");
        }
        private void BtnStopFreshBuff_Click(object sender, EventArgs e)
        {
            m_FreshBuff = false;
            BufferText.AppendText("停止刷新\r\n");
        }
        private void BtnClearBuff_Click(object sender, EventArgs e)
        {
            BufferText.Text = "";
        }   
        private void CBufferViewForm_Load(object sender, EventArgs e)
        {

        }
        private void CBufferViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CGlobal.m_BufferView = null;
            bool flag2 = (CGlobal.m_SOEView == null || CGlobal.m_SOEView.IsDisposed == true);
            bool flag3 = (CGlobal.m_EventView == null || CGlobal.m_EventView.IsDisposed == true);
            if (flag2 && flag3)
            {
                CGlobal.PostMessage(CGlobal.m_Device.m_MainFormHandle, UseMessage.MY_MSG_1, 0, 0);
            }
        }  
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveNowBuff();
        }    
    }
}
