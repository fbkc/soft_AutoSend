using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using CsharpHttpHelper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AutoSend
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            string mFilePath = Application.StartupPath + "\\AutoUpdate.exe";
            if (File.Exists(mFilePath))
                Process.Start(mFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Myinfo.sname;
            this.notifyIcon1.Text = Myinfo.sname;
            List<string> source = AShelp.Load("user");
            if (source != null)
            {
                this.txtName.Text = source[0];
                this.txtPwd.Text = source[1];
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //foreach (Process p in Process.GetProcessesByName("AutoUpdate"))
            //{
            //    MessageBox.Show("请先升级软件");
            //    return;
            //}
            string name = this.txtName.Text;
            string pass = this.txtPwd.Text;
            LoginServer(name, pass);

        }

        private void LoginServer(string name, string pass)
        {
            string realmList = "";//目前所有站
            string realmNameInfo = "";//权限站
            string expirationTime = "";//到期时间
            DateTime s, n;
            s = DateTime.Now;
            n = DateTime.Now;

            string key = NetHelper.GetMD5(name + "100dh888");
            string dosubmit = "1";
            //var obj = new
            //{
            //    username = name,
            //    password = pass,
            //    dosubmit,
            //    key
            //};
            //string postDataStr = JsonConvert.SerializeObject(obj);
            StringBuilder strpost = new StringBuilder();
            strpost.AppendFormat("username={0}&", name);
            strpost.AppendFormat("password={0}&", pass);
            strpost.AppendFormat("dosubmit={0}&", dosubmit);
            strpost.AppendFormat("key={0}&", key);
            try
            {
                string login_json = NetHelper.HttpPost("http://tool.100dh.cn/LoginHandler.ashx?action=Login", strpost.ToString());
                if (login_json != "")
                {
                    // list = (List<CategoryJson>)HttpHelper.JsonToObject<List<CategoryJson>>(main1);
                    JObject jo = (JObject)JsonConvert.DeserializeObject(login_json);
                    string code = jo["code"].ToString();
                    string msg = jo["msg"].ToString();
                    if (code == "0")//失败
                    { MessageBox.Show("登录失败，" + msg); return; }
                    else if (code == "1")//成功
                    {
                        string data = jo["detail"].ToString();
                        expirationTime = jo["detail"]["cmUser"]["expirationTime"].ToString();//到期时间
                        DateTime.TryParse(expirationTime, out s);
                        realmNameInfo = jo["detail"]["cmUser"]["realmNameInfo"].ToString();//此账号下绑定的域名
                        realmList = jo["detail"]["realmList"].ToString();
                        //rjlist = (List<ReleaseJson>)HttpHelper.JsonToObject<List<ReleaseJson>>(release);
                        List<realmNameInfo> rjlist = (List<realmNameInfo>)HttpHelper.JsonToObject<List<realmNameInfo>>(realmList);
                        if (rjlist.Count > 0)
                        {
                            List<realmNameInfo> rList = new List<realmNameInfo>();
                            foreach (realmNameInfo rj in rjlist)
                                //rj.path = joo[item.Key]["path"].ToString();路径，暂时未加
                                if (realmNameInfo.Contains(rj.Id) && rj.isUseing == true)
                                    rList.Add(rj);
                            Myinfo.rjlist = rList;
                        }
                        if (s <= n)
                        {
                            MessageBox.Show("授权已到期！");
                        }
                        else
                        {
                            Myinfo.softtime = s - n;
                            Myinfo.realmNameInfo = realmNameInfo;
                            Myinfo.username = name;
                            Myinfo.password = pass;
                            if (ckbremenber.Checked)
                            {
                                AShelp.Save(this.txtName.Text, this.txtPwd.Text, "", "", "user");
                                Myinfo.username = this.txtName.Text;
                            }
                            frmMain f = new frmMain(name, pass);
                            f.Show();
                            Myinfo.myfmain = f;
                            Myinfo.mylogin = this;
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现错误，请联系作者！" + ex);
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
            }


        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (Myinfo.myfmain != null)
            {
                Myinfo.myfmain.Show();
                Myinfo.myfmain.WindowState = FormWindowState.Normal;

                Myinfo.myfmain.Activate();
            }

        }

        private void 打开软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Myinfo.myfmain != null)
            {
                Myinfo.myfmain.Show();
                Myinfo.myfmain.WindowState = FormWindowState.Normal;

                Myinfo.myfmain.Activate();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Myinfo.myfmain != null)
            {
                Myinfo.myfmain.Close();
            }
            Application.ExitThread();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
