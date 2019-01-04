using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmhzc : Form
    {
        frmMain f;
        public frmhzc(frmMain myf)
        {
            InitializeComponent();
            f = myf;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (s.ToString().Length > 0)
                        s.Append("\r\n" + checkedListBox1.Items[i].ToString());
                    else
                        s.Append(checkedListBox1.Items[i].ToString());
                }
            }
            if (f.txtbl2.Text.EndsWith("\r\n") || f.txtbl2.Text == "")
                f.txtbl2.Text += s.ToString();
            else
                f.txtbl2.Text += "\r\n" + s.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkBox1.Checked)
                    this.checkedListBox1.SetItemChecked(i, true);//true就是全选
                else
                    this.checkedListBox1.SetItemChecked(i, false);//true就是全选
            }
        }

        private void frmhzc_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\cthzc.bin";
            string hzc= AShelp.LoadTXT(path);
            if (hzc == "")
            {
                MessageBox.Show("文件不存在，请联系客服");
                return;
            }
            string[] strh = Regex.Split(hzc, "\r\n", RegexOptions.IgnoreCase);
            foreach (string s in strh)
            {
                checkedListBox1.Items.Add(s);
            }
        }
    }
}
