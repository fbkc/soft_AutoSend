using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmTH : Form
    {
        public string nr;
        public Form ff;
        public frmTH(string innr,Form f)
        {
            nr = innr;
            ff = f;
            InitializeComponent();
        }

        private void frmTH_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fnr = textBox1.Text.Trim();
            string tnr = textBox2.Text.Trim();
            if (fnr != "")
            {
                nr = nr.Replace(fnr, tnr);
            }
            frmMain f = ff as frmMain;
            if (f != null)
                f.richTextBox1.Text = nr;
            this.DialogResult = DialogResult.OK;
        }
    }
}
