using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace AutoSend
{
    public partial class frmUpdatePass : Form
    {
        private string _login_name;
        private string _login_pass;

        public string _newPass;

        public frmUpdatePass(string name, string pass)
        {
            this._login_name = name;
            this._login_pass = pass;

            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string oldpass = this.txt_oldpass.Text;
            string newpass = this.txt_newpass.Text;
            string repass = this.txtrepass.Text;

            //if (oldpass != _login_pass)
            //{
            //    MessageBox.Show("旧密码不正确！");
            //    return;
            //}
            if (newpass == repass)
            {
                string key = "AutoSend";
                string iv = "weiwei@1";

                string strPass = this._login_name + "|" + oldpass + "|" + newpass + "|" + Myinfo.sid + "|" + DateTime.Now;

                string md5strPass = Tools.Encode(strPass, key, iv);

                WebReference.Heart hwm = new WebReference.Heart();

                string re = hwm.UpdatePassWord(md5strPass);

                string[] rs = Tools.Decode(re, key, iv).Split('|');

                if (rs.Length == 2)
                {
                    string result = rs[0];

                    if (result == "1")
                    {
                        MessageBox.Show("修改密码成功!");

                        this._newPass = newpass;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Hide();
                    }
                    if (result == "0")
                    {
                        MessageBox.Show("修改密码失败!");
                    }
                    if (result == "-1")
                    {
                        MessageBox.Show("旧密码错误!");
                    }
                }
            }
            else
            {
                MessageBox.Show("两次密码输入不一致!");
            }
        }
    }
}
