using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmProgress : Form
    {
        public frmProgress(BackgroundWorker worker)
        {
            InitializeComponent();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            this.ControlBox = false; 
        }
        public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
