using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmreplace : Form
    {
        public frmreplace()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            string t = textBox1.Text;
            string[] sp = textBox2.Lines;            
            string[] tc;
            foreach (string s in sp)
            {
                tc=s.Split(new string[]{"$$"},StringSplitOptions.RemoveEmptyEntries);
                if(tc.Length==2)
                {                
                t = t.Replace(tc[0], tc[1]);
                }
            }                   

            textBox3.Text =t;
        }
    }
}
