using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmkeyword : Form
    {
        public frmMain myf;
        public frmkeyword(frmMain f)
        {
            myf = f;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtscgs.Text += "【关键词1】";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtscgs.Text += "【关键词2】";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtscgs.Text += "【关键词3】";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtscgs.Text += "【后缀】";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = string.Format("关键词1，共有{0}行", txtgjc1.Lines.Length);
        }

        private void txtgjc2_TextChanged(object sender, EventArgs e)
        {
            label2.Text = string.Format("关键词2，共有{0}行", txtgjc2.Lines.Length);
        }

        private void txtgjc3_TextChanged(object sender, EventArgs e)
        {
            label3.Text = string.Format("关键词3，共有{0}行", txtgjc3.Lines.Length);
        }

        private void txthz_TextChanged(object sender, EventArgs e)
        {
            label4.Text = string.Format("后缀，共有{0}行", txthz.Lines.Length);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label5.Text = string.Format("生成结果，共有{0}行", txtjg.Lines.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myf.txtCity.Text += txtjg.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int num = 0;
            int.TryParse(txtnum.Text, out num);
            string[] key1 = txtgjc1.Lines;
            string[] key2 = txtgjc2.Lines;
            string[] key3 = txtgjc3.Lines;
            string[] hz = txthz.Lines;
            Random r = new Random();
            List<string> jg = new List<string>();
            string temp = "";
            if (num != 0)
            {
                string formatstr = txtscgs.Text;
                //生成随机后替换【地名】
                for (int i = 0; i < num; i++)
                {
                    temp = formatstr;
                    if (key1.Length > 0)
                    {
                        temp = temp.Replace("【关键词1】", key1[r.Next(key1.Length)]);
                    }
                    if (key2.Length > 0)
                    {
                        temp = temp.Replace("【关键词2】", key2[r.Next(key2.Length)]);
                    }
                    if (key3.Length > 0)
                    {
                        temp = temp.Replace("【关键词3】", key3[r.Next(key3.Length)]);
                    }
                    if (hz.Length > 0)
                    {
                        temp = temp.Replace("【后缀】", hz[r.Next(hz.Length)]);
                    }
                    jg.Add(temp);
                }
               txtjg.Lines= AShelp.delreStrings(jg);
            }
            else
            {
                MessageBox.Show("请输入生成数量！");
            }
        }
    }
}
