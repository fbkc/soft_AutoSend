using System;
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
    public partial class frmcj : Form
    {
        public frmMain f;
        public frmcj(frmMain myfrm)
        {
            InitializeComponent();
            f = myfrm;
        }
        AccessHelper achelp = new AccessHelper();
        //一键采集
        private string urltxt = "";
        private void btnonekeygather_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txturlgather.Text.Trim().Length < 1)
                {
                    MessageBox.Show("网址不能为空！");
                    this.txturlgather.Focus();
                }
                else
                {
                    string pattern = "(http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?";
                    Match match = Regex.Match(this.txturlgather.Text, pattern);
                    if (!match.Success)
                    {
                        MessageBox.Show("请输入正确的网址");
                        this.txturlgather.Focus();
                    }
                    else
                    {
                        this.urltxt = this.txturlgather.Text.Trim();
                        if (!this.urltxt.EndsWith("/"))
                        {
                            this.urltxt += "/";
                        }
                        string html = "";
                        try
                        {
                            html = NetHelper.HttpGet(this.urltxt, "", Encoding.GetEncoding("UTF-8"));
                        }
                        catch (Exception ex)
                        {
                            string pattern2 = "(http|https)://(?<domain>[^(:|/]*)";
                            Match match2 = Regex.Match(this.txturlgather.Text, pattern2);
                            this.urltxt = match2.Groups[0].ToString();
                            if (!this.urltxt.EndsWith("/"))
                            {
                                this.urltxt += "/";
                            }
                            html = NetHelper.HttpGet(this.urltxt, "", Encoding.GetEncoding("UTF-8"));
                        }
                        if (this.isLuan(html))
                        {
                            html = NetHelper.HttpGet(this.urltxt, "", Encoding.GetEncoding("gb2312"));
                        }
                        List<gather> list = new List<gather>();
                        string pattern3 = "(?is)<a(?:(?!href=).)*href=(['\"]?)(?<url>[^\"\\s>]*)\\1[^>]*>(?<text>(?:(?!</?a\\b).)*)</a>";
                        MatchCollection matchCollection = Regex.Matches(html, pattern3, RegexOptions.Multiline);
                        foreach (Match match3 in matchCollection)
                        {
                            string input = Regex.Replace(match3.Groups["text"].Value, "<[^>]*>", string.Empty);
                            string title = Regex.Replace(input, "\\s", "").Replace("&nbsp;", "").Replace("&quot", "").Replace("&raquo", "");
                            if (Encoding.Default.GetByteCount(title) > 17)
                            {
                                gather gather = new gather();
                                gather.title = title;
                                string url = match3.Groups["url"].Value;
                                if (!string.IsNullOrEmpty(url))
                                {
                                    if (url.StartsWith("http://") || url.StartsWith("https://"))
                                    {
                                        gather.url = url;
                                    }
                                    else
                                    {
                                        string pattern2 = "(http|https)://(?<domain>[^(:|/]*)";
                                        Match match2 = Regex.Match(this.txturlgather.Text, pattern2);
                                        this.urltxt = match2.Groups[0].ToString();
                                        if (!this.urltxt.EndsWith("/"))
                                        {
                                            this.urltxt += "/";
                                        }
                                        gather.url = this.urltxt + url;
                                    }
                                    gather.isdeal = "---";
                                    list.Add(gather);
                                }
                            }
                        }
                        this.dgvtitlegather.AutoGenerateColumns = false;
                        this.dgvtitlegather.DataSource = list;
                        MessageBox.Show("抓取文章共" + this.dgvtitlegather.Rows.Count + "篇");
                        this.label60.Text = "文章数量：" + this.dgvtitlegather.Rows.Count + "篇";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("采集失败，请更换网址" + ex);
            }
        }
        private int page = 0;
        private string content = "";
        //编辑段落，将采集到的段落全部加入段落处理
        private void btnAdddeal_Click(object sender, EventArgs e)
        {
            this.content = "";
            this.page = 0;
            this.page = this.dgvtitlegather.Rows.Count;
            if (this.page > 30)
            {
                this.page = 30;
            }
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.CheckUpdate1);
            backgroundWorker.RunWorkerAsync();
            new frmProgress(backgroundWorker)
            {
                Text = "正在处理采集数据，请耐心等待..."
            }.ShowDialog(this);
            new frmparagraphdeal(f)
            {
                isAdd = true,
                par = this.content
            }.ShowDialog();
            f.databind1();
            //this.tabControl4.SelectedTab = this.tabPage10;
        }
        public void CheckUpdate1(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while (i < this.page)
            {
                string url = this.dgvtitlegather.Rows[i].Cells["urls"].Value.ToString();
                string text = "";
                try
                {
                    text = NetHelper.HttpGet(url, "", Encoding.GetEncoding("UTF-8"));
                }
                catch (Exception ex)
                {
                    goto IL_A4;
                }
                goto IL_58;
            IL_A4:
                i++;
                continue;
            IL_58:
                if (this.isLuan(text))
                {
                    text = NetHelper.HttpGet(url, "", Encoding.GetEncoding("gb2312"));
                }
                string htmlContent = this.GetContent(text);
                this.content += this.formatHTML(htmlContent);
                goto IL_A4;
            }
        }
        /// <summary>
        /// 描述:格式化网页源码
        /// 
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        public string formatHTML(string htmlContent)
        {
            string result = "";

            result = htmlContent.Replace("&raquo;", "").Replace("&nbsp;", "")
                    .Replace("&copy;", "").Replace("/r", "").Replace("/t", "")
                    .Replace("/n", "").Replace("&amp;", "&").Replace("&rdquo;", "")
                    .Replace("&ldquo;", "").Replace("&quot;", "").Replace("-", "")
                    .Replace("原文链接", "").Replace("推荐帖子", "").Replace("注册", "")
                    .Replace("登陆", "").Replace("登录", "").Replace("&hellip;", "")
                    .Replace("相关阅读", "").Replace("特价影票4折起在线选座", "")
                    .Replace("忘记密码？", "").Replace("&rarr", "").Replace("&", "");
            return result;
        }
        //预览
        private void dgvtitlegather_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string url = dgvtitlegather.Rows[e.RowIndex].Cells["urls"].Value.ToString();
                string html = NetHelper.HttpGet(url, "", Encoding.GetEncoding("UTF-8"));
                if (isLuan(html))//判断是否有乱码
                {
                    html = NetHelper.HttpGet(url, "", Encoding.GetEncoding("gb2312"));//有乱码，用gbk编码再查一次
                }
                string p = GetContent(html);//获取p标签内容
                //p = HttpUtility.UrlDecode(p, System.Text.Encoding.Unicode);
                if (p == "")
                {
                    MessageBox.Show("获取段落失败，跳过");
                    dgvtitlegather.Rows[e.RowIndex].Cells["IsState"].Value = "跳过";
                }
                else
                {
                    frmparagraphdeal frmpar = new frmparagraphdeal(f);
                    frmpar.par = formatHTML(p);
                    frmpar.isAdd = true;
                    frmpar.ShowDialog();
                    f.databind1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取段落失败，跳过");
                dgvtitlegather.Rows[e.RowIndex].Cells["IsState"].Value = "跳过";
            }
        }

        //根据链接采集文章
        public string GetContent(string html)
        {
            string content = string.Empty;
            string reg = @"(?i)<p[^>]*?>([\s\S]*?)</p>";
            string pp = "";
            MatchCollection mcTable = Regex.Matches(html, reg);
            foreach (Match mTable in mcTable)
            {
                pp = Regex.Replace(mTable.Groups[1].Value, "<[^>]*>", string.Empty, RegexOptions.Singleline);
                string p1 = Regex.Replace(pp, @"\s", "");
                if (System.Text.Encoding.Default.GetByteCount(p1) > 150)
                {
                    if (p1.Contains("Copyright") || p1.Contains("copyright") || p1.Contains("版权所有") || p1.Contains("技术支持："))
                        continue;
                    if (p1.Contains("新浪") || p1.Contains("关于我们") || p1.Contains("ICP") || p1.Contains("法律顾问："))
                        continue;
                    if (p1.Contains("上一篇：") || p1.Contains("下一篇：") || p1.Contains("微信扫描二维码") || p1.Contains("微信扫码二维码")
                        || p1.Contains("分享至好友和朋友圈"))
                        continue;
                    if (p1.Contains("来源：") || p1.Contains("【") || p1.Contains("s后自动返回") || p1.Contains("用户名")
                        || p1.Contains("地址：") || p1.Contains("客户端"))
                        continue;
                    if (p1.Contains("|退出") || p1.Contains("◎文/图")
                        || p1.Contains("评论()"))
                        continue;
                    content += "    " + p1 + "\r\n";
                }
            }
            //if (content.EndsWith("\r\n") && content.Length > 2)
            //    return content.Substring(0, content.Length - 2);
            //else
            return content;
        }

        /// <summary>
        /// 判断网页是否有乱码
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        bool isLuan(string txt)
        {
            var bytes = Encoding.UTF8.GetBytes(txt);
            //239 191 189
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }
        bool isLuan1(string txt)
        {
            var bytes = Encoding.Default.GetBytes(txt);
            //239 191 189
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }

        private void dgvtitlegather_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //自动编号，与数据无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               dgvtitlegather.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   dgvtitlegather.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   dgvtitlegather.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void txturlgather_Click(object sender, EventArgs e)
        {
            txturlgather.Text = "";
        }
    }
}
