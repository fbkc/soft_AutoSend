using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmwordtopic : Form
    {
        public frmwordtopic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                textBox1.BackColor = colorDialog1.Color;
            }
        }

        private void frmwordtopic_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 把文字转换才Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        private Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                int width = (int)(sizef.Width + 1);
                int height = (int)(sizef.Height + 1);
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            }
            else
            {
                bmp = new Bitmap(rect.Width, rect.Height);
            }

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            SolidBrush brush = new SolidBrush(fontcolor);
            //Brush brush = new SolidColorBrush(fontcolor);
            g.DrawString(text, font, brush, rect, format);
            return bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //获取文本636 468 277, 224

            string text = this.textBox1.Text;
            int h = 100, w = 100;
            int.TryParse(textBox3.Text, out h);
            int.TryParse(textBox4.Text, out w);
            //得到Bitmap(传入Rectangle.Empty自动计算宽高)
            Bitmap bmp = TextToBitmap(text, this.textBox1.Font, new Rectangle(0,0,w,h), this.textBox1.ForeColor, this.textBox1.BackColor);
            this.pictureBox1.Height = h;
            this.pictureBox1.Width = w;
            if (w > 337)
            {
                this.Width = 636 + w - 337;
            }
            else
            {
                this.Width = 636;
            }
            if (h > 224)
            {
                this.Height = 468 +h - 224;
            }
            else
            {
                this.Height = 468;
            }
            //用PictureBox显示
            this.pictureBox1.Image = bmp;

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = colorDialog1.Color;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.png|*.png|*.jpeg|*.jpg|*.bmp|*.bmp";            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageFormat f = ImageFormat.Jpeg;
                switch (comboBox1.Text)
                {
                    case ".jpg":
                        f = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        f = ImageFormat.Bmp;                        
                        break;
                    case ".png":
                        f = ImageFormat.Png;
                        break;
                }
                Image bmp = this.pictureBox1.Image;
                //保存到桌面save.jpg
                string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);


                bmp.Save(saveFileDialog1.FileName, f);
            }
            
        }
    }
}
