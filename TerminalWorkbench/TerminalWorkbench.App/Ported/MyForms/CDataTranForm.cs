using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;
using System.Collections;

namespace DtuTestManager
{
    public partial class CDataTranForm : Form
    {
        public CDataTranForm()
        {
            InitializeComponent();
        }
        private byte[] StringToBuffer(string strText)
        {
            if (strText.Length == 0)
                return null;
            string strValue = strText.Replace("\r", "");
            strValue = strValue.Replace("\n", "");
            strValue = strValue.Replace(" ", "");
            if (strValue.Length <= 2)
                return null;
            byte[] pBuffer = new byte[strValue.Length / 2];
            try
            {
                for (int i = 0; i < (strValue.Length / 2); i++)
                {
                    pBuffer[i] = (byte)Convert.ToInt32(strValue.Substring(i * 2, 2), 16);
                }
            }
            catch
            {
                return null;
            }
            return pBuffer;
        }
        private string BufferToString(byte[] pBuffer, int nLeng)
        {
            string strContent = "";
            for (int i = 0; i < nLeng; i++)
            {
                strContent += String.Format("{0:X2}", pBuffer[i]);
                strContent += " ";
            }
            return strContent;
        }
        private void BtTran1_Click(object sender, EventArgs e)
        {
            byte[] pBuff = StringToBuffer(SourceText.Text);
            if (pBuff == null)
                return;
            byte[] pDataBuff = new byte[4];
            int nCount = pBuff.Length / 4;
            ItemText.Text = "";
            for (int i = 0; i < nCount; i++)
            {
                Array.Copy(pBuff, i * 4, pDataBuff, 0, 4);
                ItemText.Text += (BitConverter.ToSingle(pDataBuff, 0)).ToString();
                ItemText.Text += "\r\n";
            }
        }

        private void BtTran2_Click(object sender, EventArgs e)
        {
            byte[] pBuff = StringToBuffer(SourceText.Text);
            if (pBuff == null)
                return;
            byte[] pDataBuff = new byte[4];
            int nCount = pBuff.Length / 4;
            ItemText.Text = "";
            for (int i = 0; i < nCount; i++)
            {
                Array.Copy(pBuff, i * 4, pDataBuff, 0, 4);
                ItemText.Text += (BitConverter.ToInt32(pDataBuff, 0)).ToString();
                ItemText.Text += "\r\n";
            }
        }

        private void BtTran3_Click(object sender, EventArgs e)
        {
            byte[] pBuff = StringToBuffer(SourceText.Text);
            if (pBuff == null)
                return;
            byte[] pDataBuff = new byte[4];
            int nCount = pBuff.Length / 4;
            ItemText.Text = "";
            for (int i = 0; i < nCount; i++)
            {
                Array.Copy(pBuff, i * 4, pDataBuff, 0, 4);
                ItemText.Text += (BitConverter.ToUInt32(pDataBuff, 0)).ToString();
                ItemText.Text += "\r\n";
            }
        }

        private void BtTran4_Click(object sender, EventArgs e)
        {
            byte[] pBuff = StringToBuffer(SourceText.Text);
            if (pBuff == null)
                return;
            ItemText.Text = TerminalWorkbench.Presentation.TextEncoding.Ansi.GetString(pBuff);
        }

        private void BtTran5_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] pBuff = System.BitConverter.GetBytes(Convert.ToSingle(SourceText.Text));
                ItemText.Text = BufferToString(pBuff, pBuff.Length);
            }
            catch
            {
                ItemText.Text = "";
            }
        }

        private void BtTran6_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] pBuff = System.BitConverter.GetBytes(Convert.ToInt32(SourceText.Text));
                ItemText.Text = BufferToString(pBuff, pBuff.Length);
            }
            catch
            {
                ItemText.Text = "";
            }
        }

        private void BtTran7_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] pBuff = System.BitConverter.GetBytes(Convert.ToUInt32(SourceText.Text));
                ItemText.Text = BufferToString(pBuff, pBuff.Length);
            }
            catch
            {
                ItemText.Text = "";
            }
        }

        private void BtTran8_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] pBuff = TerminalWorkbench.Presentation.TextEncoding.Ansi.GetBytes(SourceText.Text);
                ItemText.Text = BufferToString(pBuff, pBuff.Length);
            }
            catch
            {
                ItemText.Text = "";
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ItemText.Text = "";
            SourceText.Text = "";
        }
    }
}
