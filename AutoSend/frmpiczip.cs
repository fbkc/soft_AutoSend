using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace AutoSend
{
    public partial class frmpiczip : Form
    {
        public frmpiczip()
        {
            InitializeComponent();
        }

        private void frmpiczip_Load(object sender, EventArgs e)
        {

        }

        #region GetPicThumbnail
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth"></param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public bool GetPicThumbnail(string sFile, string dFile, int dHeight, int dWidth, int flag,bool isscal)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (isscal)
            {
                if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号
                {
                    if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                    {
                        sW = dWidth;
                        sH = (dWidth * tem_size.Height) / tem_size.Width;
                    }
                    else
                    {
                        sH = dHeight;
                        sW = (tem_size.Width * dHeight) / tem_size.Height;
                    }
                }
                else
                {
                    sW = tem_size.Width;
                    sH = tem_size.Height;
                }
            }
            else
            {
                sW = dWidth;
                sH = dHeight;
            }
            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }

        }
        #endregion

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

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "";
            string outpath="";
            outpath=this.textBox5.Text;
            if(!outpath.EndsWith("\\"))outpath+="\\";
            FileInfo f;
            string outfile="";
            int h=300,w=300,flag=100;
            int.TryParse(textBox3.Text,out h);
            int.TryParse(textBox4.Text,out w);
            int.TryParse(textBox2.Text,out flag);
            if(h==0)h=300;
            if(w==0)w=300;
            if(flag<=0 || flag>100)flag=100;
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {                
                    //if (this.listView1.Items[i].Checked)
                    {
                        path = this.listView1.Items[i].Text;
                        f=new FileInfo(path);                        
                        outfile=outpath+f.Name;
                        if (GetPicThumbnail(path, outfile, h, w, flag, checkBox1.Checked))
                            this.listView1.Items[i].SubItems[1].Text = "处理完成";
                        else
                        {
                            this.listView1.Items[i].SubItems[1].Text = "处理出错";

                        }
                        
                        listView1.Refresh();
                    }                
            }
            MessageBox.Show("所有图片处理完成！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox5.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

    }
}
