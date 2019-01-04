using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmjz : Form
    {
        frmMain myf;
        public frmjz(frmMain f)
        {
            myf = f;
            InitializeComponent();
        }

        private void frmjz_Load(object sender, EventArgs e)
        {
            this.textBox2.Text = "。---换行符\r\n，---换行符\r\n；---换行符\r\n！---换行符\r\n!---换行符\r\n&gt;---换行符\r\n&lt;---换行符\r\n,---换行符\r\n.---换行符\r\n;---换行符\r\n?---换行符\r\n@---空\r\ncn---空\r\ncom---空\r\nnet---空\r\norg---空\r\nwww---空\r\n“---空\r\n”---空\r\n…---换行符\r\n《---换行符\r\n》---换行符\r\n	---空\r\n ---空";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = string.Format("共有{0}行", textBox1.Lines.Length); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isdel = checkBox1.Checked;
            string t = textBox1.Text;
            string[] sp = textBox2.Lines;            
            foreach (string s in sp)
            {
                t = t.Replace(s.Split('-')[0],"⊙");
            }
            t = t.Replace("\r\n", "");
            sp = t.Split('⊙');
            List<string> tt = new List<string>(sp);
            List<string> newt = new List<string>();
            if (isdel)
            {
                int snum = 0;
                int.TryParse(textBox3.Text, out snum);
                int dnum = 0;
                int.TryParse(textBox4.Text, out dnum);
                for (int i = 0; i < tt.Count; i++)
                {
                    if (tt[i].Length >= snum && tt[i].Length <= dnum)
                        newt.Add(tt[i]);
                }
                sp = newt.ToArray();
            }
            else
            {
                sp=AShelp.delspaceStrings(sp);
            }
            
            textBox1.Lines = sp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool isdel = checkBox2.Checked;            
            string[] sp = textBox7.Lines;           
            List<string> tt = new List<string>(sp);
            List<string> newt = new List<string>();
            if (isdel)
            {
                int snum = 0;
                int.TryParse(textBox6.Text, out snum);
                int dnum = 0;
                int.TryParse(textBox5.Text, out dnum);
                for (int i = 0; i < tt.Count; i++)
                {
                    if (tt[i].Length >= snum && tt[i].Length <= dnum)
                        newt.Add(tt[i]);
                }
                sp = newt.ToArray();
            }
            else
            {
                sp = AShelp.delspaceStrings(sp);
            }
            textBox7.Lines = sp;

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label7.Text = string.Format("共有{0}行", textBox7.Lines.Length); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myf.txtJuZi.Text += textBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myf.txtJuZi.Text += textBox7.Text;
        }
    }
}
