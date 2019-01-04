using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmgetpic : Form
    {
        public frmMain f;
        int page = 1;
        public frmgetpic(frmMain myfrm)
        {
            f = myfrm;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.label1.Text = string.Format("共{0}行", textBox1.Lines.Length);
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Lines.Length > 0)
            {
                string path = "";
                int index = textBox1.GetFirstCharIndexOfCurrentLine();//得到当前行第一个字符的索引
                int line = textBox1.GetLineFromCharIndex(index);//得到当前行的行号,从0开始，习惯是从1开始，所以+1.
                path = textBox1.Lines[line];
                if (path != "" && path.StartsWith("http://"))
                {
                    lblpicpath.Text = string.Format("{0}", path);
                    pictureBox1.ImageLocation = path;
                }
            }

        }

        private void frmgetpic_Load(object sender, EventArgs e)
        {
            var url = "http://my.chemcp.com/office/myweb/addclass.asp";
            var r = NetHelper.HttpGet(url, "",Encoding.Default);
            GetValueByName(r, "bigclassname");
        }
        /// <summary>
        /// 根据Name匹配Value对应的内容
        /// </summary>
        /// <param name="src"></param>
        /// <param name="sty"></param>
        /// <param name="tagerName"></param>
        /// <param name="typ"></param>
        public void GetValueByName(string Html, string element)
        {
            List<Category> list = new List<Category>();
            Regex title = new Regex(@"name=\" + '"' + "" + element + "+(.|\n)*?>", RegexOptions.IgnoreCase);
            MatchCollection matches = title.Matches(Html);
            int i = 0;
            foreach (Match match in matches)
            {
                title = new Regex(@"value=[" + '"' + "\']+(.|\n)*?[" + '"' + "\']", RegexOptions.IgnoreCase);
                MatchCollection matchereg = title.Matches(match.ToString());
                foreach (Match matchreg in matchereg)
                {
                    title = new Regex(@"[" + '"' + "\']+(.|\n)*?[" + '"' + "\']", RegexOptions.IgnoreCase);
                    MatchCollection matche = title.Matches(matchreg.ToString());
                    Regex id = new Regex(@"cmd=up&bigclassid=.*?>", RegexOptions.IgnoreCase);
                    MatchCollection idreg = id.Matches(Html);
                    foreach (Match val in matche)
                    {
                        var idval = idreg[i].Value.Replace("cmd=up&bigclassid=", "").Replace(">", "");
                        list.Add(new Category { ChsName = idval, EngName = val.ToString().Replace("\"", "") });
                        ++i;
                    }
                }
            }
            //this.TypeBox.ValueMember = "ChsName";
            //this.TypeBox.DisplayMember = "EngName";
            //this.TypeBox.DataSource = list;
            //this.TypeBox.SelectedIndex = 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (f.lispic.Text.EndsWith("\r\n") || f.lispic.Text == "")
                f.lispic.Text += textBox1.Text;
            else
                f.lispic.Text += "\r\n" + textBox1.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string add = textBox1.Text.Replace("s_", "");
            if (f.randpic.Text.EndsWith("\r\n") || f.randpic.Text == "")
                f.randpic.Text += add;
            else
                f.randpic.Text += "\r\n" + add;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<CsharpHttpHelper.Item.ImgItem> list = myhttp.GetImagshg(myhttp.host + "/office/pic.asp?typeid=" + this.TypeBox.SelectedValue);
            settext(list);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<CsharpHttpHelper.Item.ImgItem> list = myhttp.GetImagshg(myhttp.host + "/office/pic.asp?typeid=" + this.TypeBox.SelectedValue.ToString() + "&Page=" + page);
            page++;
            settext(list);
        }
        private void settext(List<CsharpHttpHelper.Item.ImgItem> list)
        {
            if (list != null)
            {
                //List<string> l = new List<string>();
                foreach (CsharpHttpHelper.Item.ImgItem item in list)
                {
                    if (!item.Src.Contains("img.chemcp.com")) continue;

                    if (textBox1.Text != "")
                        textBox1.Text += "\r\n" + item.Src.Replace("/small", "");
                    else
                    {
                        textBox1.Text = item.Src.Replace("/small", "");
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Lines = AShelp.delreStrings(textBox1.Lines);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] s = textBox1.Lines;
            textBox1.Text = "";
            List<string> list = new List<string>();
            foreach (string t in s)
            {
                if (t != lblpicpath.Text && t != "")
                {
                    list.Add(t);
                }
            }
            textBox1.Lines = list.ToArray();

        }
    }
}
