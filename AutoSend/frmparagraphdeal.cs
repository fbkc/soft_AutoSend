using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using yxdain;

namespace AutoSend
{
    public partial class frmparagraphdeal : Form
    {
        public frmMain f;

        public AccessHelper achelp;

        public string par = "";

        public bool isAdd = false;

        public string ID = "";

        private List<string> strList = null;
        public frmparagraphdeal(frmMain myfrm)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            f = myfrm;
        }

        private void frmparagraphdeal_Load(object sender, EventArgs e)
        {
            richtxtpardeal.Text = par;
        }
        //一键处理
        private void btnonekeydeal_Click(object sender, EventArgs e)
        {
            this.strList = new List<string>();
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "0?(13|14|15|16|17|18)[0-9]{9}", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "[0-9-()（）]{7,18}", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "^((https|http|ftp|rtsp|mms)?:\\/\\/)[^\\s]+", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "^(http|https|ftp)\\://[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&$%\\$#\\=~])*$", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "((http|ftp|https)://)(([a-zA-Z0-9\\._-]+\\.[a-zA-Z]{2,6})|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\\&%_\\./-~-]*)?", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "(([a-zA-Z0-9\\._-]+\\.[a-zA-Z]{2,6})|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\\&%_\\./-~-]*)?", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "\\w[-\\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\\.)+[A-Za-z]{2,14}", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, "[1-9]([0-9]{5,11})", " ");
            //this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, @"([零一二三四五六七八九十百千万壹贰叁肆伍陆柒捌玖拾佰仟亿]+亿)?零?([一二三四五六七八九十百千壹贰叁肆伍陆柒捌玖拾佰仟]+万)?零?([一二三四五六七八九十百壹贰叁肆伍陆柒捌玖拾佰][千仟])?零?([一二三四五六七八九十壹贰叁肆伍陆柒捌玖拾][百佰])?零?([一二三四五六七八九壹贰叁肆伍陆柒捌玖]?[十拾])?零?([一二三四五六七八九壹贰叁肆伍陆柒捌玖])?", " ");
            this.richtxtpardeal.Text = Regex.Replace(this.richtxtpardeal.Text, @"\d", " ").Replace("、", " ").Replace(".", " ").Replace("：", " ").Replace("①", " ")
                .Replace("②", " ").Replace("③", " ").Replace("④", " ").Replace("⑤", " ").Replace("⑥", " ").Replace("⑦", " ").Replace("⑧", " ").Replace("⑨", " ")
                .Replace("⑩", " ").Replace("⑪", " ").Replace("⑫", " ").Replace("⑬", " ").Replace("（", " ").Replace("）", " ").Replace("(", " ").Replace("）", " ")
                .Replace("⑴", " ").Replace("⑵", " ").Replace("⑶", " ").Replace("⑷", " ").Replace("⑸", " ").Replace("⑹", " ").Replace("⑺", " ").Replace("⑻", " ").Replace("⑼", " ")
                .Replace("⒈", " ").Replace("⒉", " ").Replace("⒊", " ").Replace("⒋", " ").Replace("⒌", " ").Replace("⒍", " ").Replace("⒎", " ").Replace("⒏", " ").Replace("⒐", " ").Replace("⒑", " ");
            string text = this.richtxtpardeal.Text.Replace("\r\n", "").Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\u3000", "");
            string[] array = text.Split(new char[]
			{
				'。',
				'？',
				'！'
			});
            if (array.Length > 1)
            {
                string text2 = "";
                for (int i = 0; i < array.Length; i++)
                {
                    string text3 = array[i];
                    if (text3.Length <= 200)
                    {
                        text2 = text2 + text3 + "。";
                        if (text2.Length > 200)
                        {
                            if (text3.StartsWith("\u3000\u3000"))
                            {
                                this.strList.Add(text2);
                            }
                            else
                            {
                                this.strList.Add("\u3000\u3000" + text2);
                            }
                            text2 = "";
                        }
                    }
                    else if (text3.Length > 200 && text3.Length < 250)
                    {
                        if (text3.StartsWith("\u3000\u3000"))
                        {
                            this.strList.Add(text3 + "。");
                        }
                        else
                        {
                            this.strList.Add("\u3000\u3000" + text3 + "。");
                        }
                    }
                    else if (text3.Length >= 250)
                    {
                        text3 = text3.Substring(0, 250);
                        if (text3.StartsWith("\u3000\u3000"))
                        {
                            this.strList.Add(text3 + "。");
                        }
                        else
                        {
                            this.strList.Add("\u3000\u3000" + text3 + "。");
                        }
                    }
                }
            }
            else if (array.Length < 2 && text.Length > 220)
            {
                for (int i = 0; i < text.Length - 220; i += 220)
                {
                    string text3 = text.Substring(i, 220);
                    if (text3.StartsWith("\u3000\u3000"))
                    {
                        this.strList.Add(text3 + "。");
                    }
                    else
                    {
                        this.strList.Add("\u3000\u3000" + text3 + "。");
                    }
                }
            }
            this.richtxtpardeal.Text = string.Join("\r\n", this.strList.ToArray());
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            richtxtpardeal.Text = "";
        }

        private void btnsavetopar_Click(object sender, EventArgs e)
        {
            if (this.richtxtpardeal.Text.Trim() == "")
            {
                MessageBox.Show("段落不能为空！");
                return;
            }
            if (isAdd)
            {
                if (this.richtxtpardeal.Text != "" && this.strList == null)
                {
                    MessageBox.Show("请先处理段落！");
                }
                else
                {
                    BackgroundWorker backgroundWorker = new BackgroundWorker();
                    backgroundWorker.WorkerSupportsCancellation = true;
                    backgroundWorker.DoWork += new DoWorkEventHandler(this.CheckUpdate);
                    backgroundWorker.RunWorkerAsync();
                    new frmProgress(backgroundWorker)
                    {
                        Text = "正在保存，请耐心等待..."
                    }.ShowDialog(this);
                    MessageBox.Show("保存成功");
                    base.Close();
                }
            }
            else
            {
                try
                {
                    this.achelp = new AccessHelper();
                    string strSql = string.Concat(new string[]
					{
						"update paragraph set PraContent='",
						this.richtxtpardeal.Text.Replace("'",""),
						"',AddTime='",
						DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
						"' where ID=",
						this.ID
					});
                    int num = this.achelp.ExcuteSql(strSql);
                    if (num > -1)
                    {
                        base.Close();
                        MessageBox.Show("更新成功");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CheckUpdate(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.strList != null && this.strList.Count > 0)
                {
                    this.achelp = new AccessHelper();
                    string a = this.achelp.GetDataTableFromDB("SELECT count(*) FROM paragraph").Rows[0][0].ToString();
                    int num;
                    if (a == "0")
                    {
                        num = 1000001;
                    }
                    else
                    {
                        DataRow dataRow = this.achelp.GetDataTableFromDB("SELECT TOP 1 ID FROM paragraph ORDER BY paragraph.ID DESC").Rows[0];
                        num = 1000001 + Convert.ToInt32(dataRow[0]);
                    }
                    for (int i = 0; i < this.strList.Count; i++)
                    {
                        string strSql = string.Concat(new object[]
						{
							"insert into paragraph (PraNo,PraContent,AddTime,QualityReport,UsedCount) values ('N",
							num,
							"','",
							this.strList[i].Replace("'",""),
							"','",
							DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
							"','优质','0')"
						});
                        int num2 = this.achelp.ExcuteSql(strSql);
                        num++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现错误" + ex.ToString());
            }
        }
        private void richtxtpardeal_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "文本字数：" + richtxtpardeal.Text.Trim().Length + "个";
        }

        private void btnchange_Click(object sender, EventArgs e)
        {
            if (txtoldword.Text.Trim() == "")
                return;
            string s = richtxtpardeal.Text;
            s = s.Replace(txtoldword.Text, txtnewword.Text);
            richtxtpardeal.Text = s;
        }

        private void richtxtpardeal_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (richtxtpardeal.SelectionLength > 0)
                {
                    toolStripMenuItem3.Enabled = true;
                    toolStripMenuItem2.Enabled = true;
                }
                else
                {
                    toolStripMenuItem3.Enabled = false;
                    toolStripMenuItem2.Enabled = false;
                }
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                    toolStripMenuItem4.Enabled = true;
                else
                    toolStripMenuItem4.Enabled = false;
                contextMenuStrip1.Show(richtxtpardeal, new Point(e.X, e.Y));
            }
        }
        //全选
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richtxtpardeal.SelectAll();
        }
        //剪切
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richtxtpardeal.Cut();
        }
        //复制
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richtxtpardeal.Copy();
        }
        //粘贴
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if ((richtxtpardeal.SelectionLength > 0) && (MessageBox.Show("是否覆盖选中的文本?", "覆盖", MessageBoxButtons.YesNo) == DialogResult.No))
                richtxtpardeal.SelectionStart = richtxtpardeal.SelectionStart + richtxtpardeal.SelectionLength;
            richtxtpardeal.Paste();
        }

        private void richtxtpardeal_MouseCaptureChanged(object sender, EventArgs e)
        {
            this.strList = null;
        }

    }
}
