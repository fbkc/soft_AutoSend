using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmDig : Form
    {
        frmMain f;
        public frmDig(frmMain myf)
        {
            InitializeComponent();
            f = myf;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtKey.Text.Trim().Length < 2)
            {
                MessageBox.Show("输入关键字长度必须大于1");
                txtKey.Text = "";
            }
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                checkedListBox1.Items.Clear();
                btnSearch.Enabled = false;
                btnSearch.Text = "正在检索...";
                try
                {
                    string key = NetHelper.GetMD5(Myinfo.username + "fangyuan888");
                    var f = new StringBuilder();
                    f.AppendFormat("username={0}&", Myinfo.username);
                    f.AppendFormat("key={0}&", key);
                    f.AppendFormat("word={0}&", txtKey.Text.Trim());
                    string main1 = NetHelper.HttpPost("http://vip.hsoow.com/index.php?m=member&c=index&a=caiji", f.ToString(), "");
                    if (main1 == "")
                    { MessageBox.Show("暂未搜到相关词"); return; }
                    JObject jo = (JObject)JsonConvert.DeserializeObject(main1);
                    string code = jo["code"].ToString();
                    string count = jo["count"].ToString();
                    string data = jo["data"].ToString();
                    if (code == "0")//失败
                    { MessageBox.Show(data); return; }
                    else if (code == "1")//成功
                    {
                        label2.Text = "共搜索到" + count + "个词";
                        foreach (var w in jo["data"])
                            checkedListBox1.Items.Add(w["word"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("暂未搜到相关数据");
                }
                btnSearch.Text = "搜索";
                //label2.Text = "共" + listBox1.Items.Count + "行";
                btnSearch.Enabled = true;

            }
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
            if (f.txtbl1.Text.EndsWith("\r\n") || f.txtbl1.Text == "")
                f.txtbl1.Text += s.ToString();
            else
                f.txtbl1.Text += "\r\n" + s.ToString();
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
    }
}
