using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace AutoSend
{
    public partial class frmimg : Form
    {
        public string isstart = "OK";
        public frmMain f;
        public frmimg(frmMain myfrm)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
            f = myfrm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = this.folderBrowserDialog1.SelectedPath;
                DirectoryInfo dirinfo = new DirectoryInfo(this.textBox1.Text);
                FileInfo[] allfile = dirinfo.GetFiles();
                string fileName;
                this.listView1.BeginUpdate();
                foreach (FileInfo fi in allfile)
                {
                    fileName = fi.Name.ToLower();
                    if (fileName.ToLower().EndsWith(".gif") || fileName.ToLower().EndsWith(".jpg") || fileName.ToLower().EndsWith(".bmp") || fileName.ToLower().EndsWith(".png"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = fi.FullName;
                        lvi.SubItems.Add("等待上传");
                        this.listView1.Items.Add(lvi);
                    }
                }
                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //
            MessageBox.Show("因百度新出‘细雨算法’，新上传图片不得包含电话及手机号码。一经发现，立即封号！！！","警告");

            //if (myhttp.islogin == "OK")
            //{
                this.Text = "正在上传图片，请耐心等待，不要离开！";
                ThreadStart start = null;
                isstart = "OK";
                if (start == null)
                {
                    start = uppic;
                }
                Thread t = new Thread(start);
                t.IsBackground = true;
                t.Start();
            //}
            //else
            //{
            //    MessageBox.Show("请登录后再操作！");
            //}

        }

        private void uppic()
        {
            string u;
            FileInfo f;
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (isstart == "OK")
                {
                    if (this.listView1.Items[i].Checked && this.listView1.Items[i].SubItems[1].Text.ToString() == "等待上传")
                    {

                        f = new FileInfo(this.listView1.Items[i].Text);
                        if (f.Length > 200 * 1024)
                        {
                            this.listView1.Items[i].SubItems[1].Text = "图片超过200K";
                        }
                        else
                        { 
                        u = myhttp.Upload(this.listView1.Items[i].Text);
                            Thread.Sleep(5000);
                        if (u !="")
                            this.listView1.Items[i].SubItems[1].Text = u;
                        }
                        listView1.Refresh();
                    }
                }
                else
                {
                    Thread.CurrentThread.Abort();
                }
            }
            this.Text = "所有图片上传完成！";
            Thread.CurrentThread.Abort();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                this.listView1.Items[i].Checked = true;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                this.listView1.Items[i].Checked = !this.listView1.Items[i].Checked;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.CheckedItems)  //选中项遍历
            {
                listView1.Items.RemoveAt(lvi.Index); // 按索引移除                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isstart = "NO";
            this.Text = "图片上传已停止";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string httpurl;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                httpurl = this.listView1.Items[i].SubItems[1].Text;
                if (httpurl.StartsWith("http"))
                {
                    if (s.ToString().Length > 0)
                        s.Append("\r\n" + httpurl);
                    else
                        s.Append(httpurl);
                }
            }
            //s.Replace("s_", "");
            if (f.lispic.Text.EndsWith("\r\n") || f.lispic.Text == "")
                f.lispic.Text += s.ToString();
            else
                f.lispic.Text += "\r\n" + s.ToString();
        }

        private void frmimg_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            string httpurl;
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                httpurl = this.listView1.Items[i].SubItems[1].Text;
                if (httpurl.StartsWith("http"))
                {
                    if (s.ToString().Length > 0)
                        s.Append("\r\n" + httpurl);
                    else
                        s.Append(httpurl);
                }
            }
            s=s.Replace("s_", "");
            if (f.randpic.Text.EndsWith("\r\n") || f.randpic.Text == "")
                f.randpic.Text += s.ToString();
            else
                f.randpic.Text += "\r\n" + s.ToString();
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            String[] str_Drop = (String[])e.Data.GetData(DataFormats.FileDrop, true);//必须用字符串数组  
            Data_List(listView1, str_Drop[0]);  
        }
        public void Data_List(ListView LV, string F)
        {            
            if (F.LastIndexOf(".") == F.Length - 4)
            {
                if (F.ToLower().EndsWith(".gif") || F.ToLower().EndsWith(".jpg") || F.ToLower().EndsWith(".bmp") || F.ToLower().EndsWith(".png"))
                {

                    ListViewItem item = new ListViewItem(F);
                    item.SubItems.Add("等待上传");
                    LV.Items.Add(item);
                }
            }
        }  
    }
}
