using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmRefresh : Form
    {
        public string url = "http://wp2.qihuiwang.com/product/productlist.aspx";
        public frmRefresh()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                url = "http://wp2.qihuiwang.com/purchase/purchaselist.aspx";
            }
            else if (radioButton2.Checked)
            {
                url = "http://wp2.qihuiwang.com/product/productlist.aspx";
            }
            else if (radioButton3.Checked)
            {
                url = "http://www.uggd.com/e/DoInfo/ListInfo.php?mid=10";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThreadStart start = null;
            Thread t = null;
            if (start == null)
            {
                start = pubtitle;
            }
            
                t = new Thread(start);
                t.IsBackground = true;
                t.Start();
                

            
        }
        private void pubtitle()
        {
            int st = 1, ed = 1;
            int.TryParse(textBox2.Text, out st);
            int.TryParse(textBox3.Text, out ed);
            for (int i = st; i <= ed; i++)
            {
                string s = "";
                if (radioButton2.Checked)
               s = myhttp.Refreshtitle(url,i, "http://wp2.qihuiwang.com/Product/ajax/OperateHandler.ashx");
                else
                    s = myhttp.Refreshtitlep(url, i, "http://wp2.qihuiwang.com/purchase/purchaselist.aspx");
                textBox1.Text += "第" + i + "页：" + s+"\r\n";
                
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.Select(this.textBox1.TextLength, 0);//光标定位到文本最后
            this.textBox1.ScrollToCaret();//滚动到光标处
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = myhttp.GetPage(url);
            textBox3.Text = i.ToString();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
