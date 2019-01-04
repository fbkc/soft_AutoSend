using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmInput : Form
    {
        public string inputName;
        public frmInput()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            inputName = txtName.Text.Trim();
            this.DialogResult = DialogResult.OK;

        }
    }
}
