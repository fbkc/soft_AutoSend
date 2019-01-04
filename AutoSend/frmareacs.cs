using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace AutoSend
{
    public partial class frmareacs : Form
    {
        XmlDocument doc = new XmlDocument();
        public frmMain myf;
        public frmareacs(frmMain f)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            myf = f;
            InitializeComponent();

        }

        private void frmareacs_Load(object sender, EventArgs e)
        {
            string[] ps=PCA.getprovince();
            checkedListBox1.Items.AddRange(ps);
            //button1_Click(null,null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
                else
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            new Thread(() =>
            {
                button15.Enabled = false;
                //DoWork;                
                textBox1.Text = "";
                string province = "";
                string mycity = "";
                string myarea = "";
                string txt = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        province = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        int index = PCA.GetIndex(province);
                        string[] citys = PCA.getcity(index);
                        if (citys != null)
                            for (int j = 0; j < citys.Length; j++)
                            {
                                mycity = citys[j];
                                string[] areas = PCA.getarea(index, j);
                                if (areas != null)
                                {
                                    foreach (string s in areas)
                                    {
                                        txt += province + mycity + s + "\r\n";
                                    }
                                }
                                else
                                {
                                    txt += province + mycity + "\r\n";

                                }

                            }
                    }
                }
                if (txt != "") txt = txt.Substring(0, txt.LastIndexOf("\r\n"));
                textBox1.Text = txt;
                groupBox1.Text = string.Format("地名，共有{0}行", textBox1.Lines.Length);
                button15.Enabled = true;
            }).Start();
            
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                button14.Enabled = false;
                //DoWork;
                textBox1.Text = "";
                string province = "";
                string mycity = "";
                string myarea = "";
                string txt = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        province = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        int index = PCA.GetIndex(province);
                        string[] citys = PCA.getcity(index);
                        if (citys != null)
                            for (int j = 0; j < citys.Length; j++)
                            {
                                mycity = citys[j];                                
                                txt += province + mycity + "\r\n";
                            }
                    }
                }
                if (txt != "") txt = txt.Substring(0, txt.LastIndexOf("\r\n"));
                textBox1.Text = txt;
                groupBox1.Text = string.Format("地名，共有{0}行", textBox1.Lines.Length);
                button14.Enabled = true;
            }).Start();
            

        }

        private void button16_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                button16.Enabled = false;
                //DoWork;
                textBox1.Text = "";
                string province = "";
                string mycity = "";
                string myarea = "";
                string txt = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        province = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        int index = PCA.GetIndex(province);
                        string[] citys = PCA.getcity(index);
                        if (citys != null)
                            for (int j = 0; j < citys.Length; j++)
                            {
                                mycity = citys[j];
                                string[] areas = PCA.getarea(index, j);
                                if (areas != null)
                                {
                                    foreach (string s in areas)
                                    {
                                        txt +=  mycity + s + "\r\n";
                                    }
                                }
                                else
                                {
                                    txt += mycity + "\r\n";
                                }

                            }
                    }
                }
                if (txt != "") txt = txt.Substring(0, txt.LastIndexOf("\r\n"));
                textBox1.Text = txt;
                groupBox1.Text = string.Format("地名，共有{0}行", textBox1.Lines.Length);
                button16.Enabled = true;
            }).Start();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                button7.Enabled = false;
                //DoWork;
                textBox1.Text = "";
                string province = "";
                string mycity = "";
                string myarea = "";
                string txt = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        province = checkedListBox1.GetItemText(checkedListBox1.Items[i]);                        
                        txt += province + mycity + "\r\n";
                    }
                }
                if (txt != "") txt = txt.Substring(0, txt.LastIndexOf("\r\n"));
                textBox1.Text = txt;
                groupBox1.Text = string.Format("地名，共有{0}行", textBox1.Lines.Length);
                button7.Enabled = true;
            }).Start();
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                button17.Enabled = false;
                //DoWork;
                textBox1.Text = "";
                string province = "";
                string mycity = "";
                string myarea = "";
                string txt = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        province = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        int index = PCA.GetIndex(province);
                        string[] citys = PCA.getcity(index);
                        if (citys != null)
                            for (int j = 0; j < citys.Length; j++)
                            {
                                mycity = citys[j];
                                if (mycity.Trim() != "")
                                    txt += mycity + "\r\n";

                            }
                    }
                }
                if (txt != "") txt = txt.Substring(0, txt.LastIndexOf("\r\n"));
                textBox1.Text = txt;
                groupBox1.Text = string.Format("地名，共有{0}行", textBox1.Lines.Length);
                button17.Enabled = true;
            }).Start();
            
        }

        private void button18_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                button18.Enabled = false;
                //DoWork;               
                textBox1.Text = "";
                string province = "";
                string mycity = "";
                string myarea = "";
                string txt = "";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        province = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        int index = PCA.GetIndex(province);
                        string[] citys = PCA.getcity(index);
                        if (citys != null)
                            for (int j = 0; j < citys.Length; j++)
                            {
                                mycity = citys[j];
                                string[] areas = PCA.getarea(index, j);
                                if (areas != null)
                                {
                                    foreach (string s in areas)
                                    {
                                        txt += s + "\r\n";
                                    }
                                }
                                else
                                {
                                    txt += mycity + "\r\n";

                                }

                            }
                    }
                }
                if (txt != "") txt = txt.Substring(0, txt.LastIndexOf("\r\n"));
                textBox1.Text = txt;                
                groupBox1.Text = string.Format("地名，共有{0}行", textBox1.Lines.Length);

                button18.Enabled = true;
            }).Start();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("省","");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("市", "");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("县", "");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("区", "");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string t = textBox2.Text.Trim();
            if(t!="")
            textBox1.Text = textBox1.Text.Replace(t, "");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (myf.txtCity.Text.EndsWith("\r\n") || myf.txtCity.Text == "")
                myf.txtCity.Text += textBox1.Text;
            else
                myf.txtCity.Text += "\r\n" + textBox1.Text;
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string[] s = textBox1.Lines;
            textBox1.Lines= AShelp.RandomStrings(s);
        }
    }
}
