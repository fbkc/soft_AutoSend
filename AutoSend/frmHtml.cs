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
    public partial class frmHtml : Form
    {
        frmMain myf;
        public frmHtml(frmMain f)
        {
            InitializeComponent();
            myf = f;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void frmHtml_Load(object sender, EventArgs e)
        {

        }
        private void button46_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【标题】");
            this.htmlEditor1.Focus();
        }
        private void button47_Click(object sender, EventArgs e)
        {
            if (myf.txtzhubl.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【主变量】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("主变量没有数据");

        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (myf.txtbl1.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【变量1】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("变量1没有数据");

        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (myf.txtbl2.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【变量2】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("变量2没有数据");
        }

        private void button49_Click(object sender, EventArgs e)
        {

        }

        private void button51_Click(object sender, EventArgs e)
        {

        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (myf.dgvpracontent.Rows.Count < 1)
            {
                MessageBox.Show("段落库没有数据");
                return;
            }
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【段落】");
            this.htmlEditor1.Focus();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (myf.txtdl2.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【段落2】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("段落2没有数据");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (myf.txtdl3.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【段落3】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("段落3没有数据");
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (myf.txtdl4.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【段落4】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("段落4没有数据");
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (myf.txtrandpic.Lines.Length < 1)
            {
                MessageBox.Show("随机图片没有数据");
                return;
            }
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【随机图片】");
            this.htmlEditor1.Focus();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (myf.txtranda.Lines.Length > 0)
            {
                txtmbname.Focus();
                this.htmlEditor1.PasteIntoSelection("【随机链接】");
                this.htmlEditor1.Focus();
            }
            else
                MessageBox.Show("随机链接没有数据");
        }

        private void button58_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【年】");
            this.htmlEditor1.Focus();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【月】");
            this.htmlEditor1.Focus();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【日】");
            this.htmlEditor1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.htmlEditor1.BodyInnerHTML == null)
                return;
            Regex reb = new Regex("【标题】");
            MatchCollection mab = reb.Matches(this.htmlEditor1.BodyInnerHTML);
            if (mab.Count > 2)
            {
                MessageBox.Show("最多插入2个标题");
                return;
            }
            Regex red = new Regex("【段落】");
            MatchCollection mad = red.Matches(this.htmlEditor1.BodyInnerHTML);
            if (mad.Count < 3 || mad.Count > 5)
            {
                MessageBox.Show("必须插入3到5个段落");
                return;
            }
            Regex ret = new Regex("【随机图片】");
            MatchCollection mat = ret.Matches(this.htmlEditor1.BodyInnerHTML);
            if (mat.Count < 2 || mat.Count > 5)
            {
                MessageBox.Show("必须插入2到5张图片");
                return;
            }
            myf.htmlEditor1.BodyInnerHTML = htmlEditor1.BodyInnerHTML;
            string mb = txtmbname.Text.Trim();
            string html = htmlEditor1.BodyInnerHTML;
            Regex r = new Regex("style=\"(?<key>.*?)\"");
            html = r.Replace(html, "");
            AShelp.SaveHtml(html, mb);
            bool ishave = false;
            for (int i = 0; i < myf.ckbHtml.Items.Count; i++)
            {
                if (myf.ckbHtml.GetItemText(myf.ckbHtml.Items[i]) == mb)
                    ishave = true;
            }
            if (!ishave)
            {
                myf.ckbHtml.Items.Add(mb);
            }
            this.Close();
        }

        private void button117_Click(object sender, EventArgs e)
        {
            string html = htmlEditor1.BodyInnerHTML;
            frmTH f = new frmTH(html, this);
            f.Show();
        }

        private void btncjss_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            this.htmlEditor1.PasteIntoSelection("【实时采集】");
            this.htmlEditor1.Focus();
        }
    }
}
