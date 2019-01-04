using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmpicadd : Form
    {
        public frmpicadd()
        {
            InitializeComponent();
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
                    if (fileName.EndsWith(".gif") || fileName.EndsWith(".jpg") || fileName.EndsWith(".bmp") || fileName.EndsWith(".png"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = fi.FullName;
                        lvi.SubItems.Add("等待处理");
                        this.listView1.Items.Add(lvi);
                    }
                }
                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox5.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = this.openFileDialog1.FileName;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "";
            string outpath = "";
            outpath = this.textBox5.Text;
            if (!outpath.EndsWith("\\")) outpath += "\\";
            FileInfo f;
            string outfile = "";
            string add = textBox2.Text.Trim();
            bool ispic = true;
            if (radioButton2.Checked) ispic = false;
            ImagePosition m=ImagePosition.LeftTop;
            if (comboBox1.Text == "左上")
                m = ImagePosition.LeftTop;
            else if (comboBox1.Text == "左下")
                m = ImagePosition.LeftBottom;
            else if (comboBox1.Text == "右上")
                m = ImagePosition.RightTop;
            else if (comboBox1.Text == "右下")
                m = ImagePosition.RigthBottom;
            else if (comboBox1.Text == "顶部居中")
                m = ImagePosition.TopMiddle;
            else if (comboBox1.Text == "底部居中")
                m = ImagePosition.BottomMiddle;
            else if (comboBox1.Text == "中心")
                m = ImagePosition.Center;
            WaterImageManage wim = new WaterImageManage();
            float alpha = 1.0f;
            float.TryParse(textBox3.Text, out alpha);
            string jg = "";
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                //if (this.listView1.Items[i].Checked)
                {
                    path = this.listView1.Items[i].Text;
                    f = new FileInfo(path);
                    outfile = outpath + f.Name;
                    if (ispic)
                    {
                        jg = wim.DrawImage(path, add, alpha, m, outfile);
                    }
                    else
                    {
                        jg = wim.DrawWords(path, add, alpha, m, outfile);
                    }
                   
                        this.listView1.Items[i].SubItems[1].Text = jg;
                   


                    listView1.Refresh();
                }
            }
            MessageBox.Show("所有图片处理完成！");
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label2.Text = "水印文本：";
                button4.Visible = false;
            }
            else
            {
                label2.Text = "请选择水印图片：";
                button4.Visible = true;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label2.Text = "水印文本：";
                button4.Visible = false;
            }
            else
            {
                label2.Text = "请选择水印图片：";
                button4.Visible = true;
            }

        }

        private void frmpicadd_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
