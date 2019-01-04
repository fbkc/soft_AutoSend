using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmGG : Form
    {
        private string gginfo;
        public frmGG(string gg)
        {
            InitializeComponent();
            gginfo = gg;
        }

        private void frmGG_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = gginfo;
	    this.textBox1.Select(0, 0);
        }
    }
}
