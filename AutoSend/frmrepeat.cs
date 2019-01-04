using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmrepeat : Form
    {
        public frmrepeat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] s = textBox1.Lines;
            s=AShelp.delreStrings(s);
            textBox2.Lines = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox2.Text);
        }
    }
}
