using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmdlhtml : Form
    {
        public string HTML = "";
        public frmdlhtml()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HTML= htmlEditor1.BodyInnerHTML;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
