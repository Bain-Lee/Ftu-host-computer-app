using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Windows.Forms;
    using System.Text.RegularExpressions;
    public partial class IpBox : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private string _text = "";

      

        public IpBox()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。 
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化 

        }
        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //System.Text.Encoding.GetEncoding(0).GetString(); 
        }

        private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            MaskIpAddr(textBox1, e);
        }

        private void textBox2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            MaskIpAddr(textBox2, e);
        }

        private void textBox3_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            MaskIpAddr(textBox3, e);
        }

        private void textBox4_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            MaskIpAddr(textBox4, e);
        }

        private void MaskIpAddr(System.Windows.Forms.TextBox textBox, KeyPressEventArgs e)
        {
            int len = textBox.Text.Length;

            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            {
                if (e.KeyChar != 8)
                {
                    if (len == 2 && e.KeyChar != '.')
                    {
                        string tmp = textBox.Text + e.KeyChar;
                        if (textBox.Name == "textBox1")
                        {
                            if (Int32.Parse(tmp) > 223) // 进行验证 
                            {
                                MessageBox.Show(tmp + " 不是一个有效项目。请指定一个介于 1 和 223 之间的数值。");
                                textBox.Text = "223";
                                textBox.Focus();
                                return;
                            }

                            textBox2.Focus();
                            textBox2.SelectAll();

                        }
                        else if (textBox.Name == "textBox2")
                        {
                            if (Int32.Parse(tmp) > 255)
                            {
                                MessageBox.Show(tmp + " 不是一个有效项目。请指定一个介于 1 和 255 之间的数值。");
                                textBox.Text = "255";
                                textBox.Focus();
                                return;
                            }
                            textBox3.Focus();
                            textBox3.SelectAll();
                        }
                        else if (textBox.Name == "textBox3")
                        {
                            if (Int32.Parse(tmp) > 255)
                            {
                                MessageBox.Show(tmp + " 不是一个有效项目。请指定一个介于 1 和 255 之间的数值。");
                                textBox.Text = "255";
                                textBox.Focus();
                                return;
                            }
                            textBox4.Focus();
                            textBox4.SelectAll();
                        }
                        else if (textBox.Name == "textBox4")
                        {
                            if (Int32.Parse(tmp) > 255)
                            {
                                MessageBox.Show(tmp + " 不是一个有效项目。请指定一个介于 1 和 255 之间的数值。");
                                textBox.Text = "255";
                                textBox.Focus();
                                return;
                            }

                        }

                    }
                    if (e.KeyChar == '.')
                    {
                        if (textBox.Name == "textBox1" && textBox.Text != "")
                        {
                            textBox2.Focus();
                            textBox2.SelectAll();
                        }
                        if (textBox.Name == "textBox2" && textBox.Text != "")
                        {
                            textBox3.Focus();
                            textBox3.SelectAll();
                        }
                        if (textBox.Name == "textBox3" && textBox.Text != "")
                        {
                            textBox4.Focus();
                            textBox4.SelectAll();
                        }
                        if (textBox.Name == "textBox4" && textBox.Text != "")
                        {

                        }
                        e.Handled = true;
                    }
                }
                else
                {
                    if (textBox.Name == "textBox1" && textBox.Text == "")
                    {

                    }
                    if (textBox.Name == "textBox2" && textBox.Text == "")
                    {
                        textBox1.Focus();
                        textBox1.SelectionStart = textBox1.Text.Length;
                    }
                    if (textBox.Name == "textBox3" && textBox.Text == "")
                    {
                        textBox2.Focus();
                        textBox2.SelectionStart = textBox2.Text.Length;
                    }
                    if (textBox.Name == "textBox4" && textBox.Text == "")
                    {
                        textBox3.Focus();
                        textBox3.SelectionStart = textBox3.Text.Length;
                    }
                    e.Handled = false;
                }
            }
            else
                e.Handled = true;
        }
        [Browsable(true)]

        // 获取 IpBox 的文本。 

        public new string Text
        {
            get
            {
                if (textBox1.Text == ""
                 || textBox2.Text == ""
                 || textBox3.Text == ""
                 || textBox4.Text == "")
                {
                    _text = "";
                    return _text;
                }
                else
                {
                    _text = Convert.ToInt32(textBox1.Text).ToString() + "." + Convert.ToInt32(textBox2.Text).ToString() + "." + Convert.ToInt32(textBox3.Text).ToString() + "." + Convert.ToInt32(textBox4.Text).ToString();
                    return _text;
                }

            }
            set
            {
                string[] tr = value.Split('.');
                if ((Convert.ToInt32(tr[0]) >= 0 && Convert.ToInt32(tr[0]) <= 255) && (Convert.ToInt32(tr[1]) >= 0 && Convert.ToInt32(tr[1]) <= 255) && (Convert.ToInt32(tr[2]) >= 0 && Convert.ToInt32(tr[2]) <= 255) && (Convert.ToInt32(tr[3]) >= 0 && Convert.ToInt32(tr[3]) <= 255))
                {
                    _text = value;
                    textBox1.Text = tr[0];
                    textBox2.Text = tr[1];
                    textBox3.Text = tr[2];
                    textBox4.Text = tr[3];
                }

            }



        }

    }
}