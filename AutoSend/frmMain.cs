using CsharpHttpHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Web;
using System.Runtime.InteropServices;
using Data;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using yxdain;
using mshtml;

namespace AutoSend
{
    public partial class frmMain : Form
    {
        DateTime shutstart;
        DateTime softdt;
        public TextBox lispic;
        public TextBox txtCity;
        public TextBox txtJuZi;
        public TextBox randpic;
        public DateTime dts;
        public DateTime dte;
        public bool isstoppub = true;
        public bool ispics = true;//选中的是图片还是随机图片，用于移除图片地址功能。
        public int pubnum = 0;
        public bool ispausd = false;
        public int waitsecond = 0;
        public bool isstarttime = false;
        public string titleurl = "";
        private string _login_name;
        private string _login_pass;
        Thread t = null;
        TimeSpan regtime;
        string[] mgcs;
        public int cgnum = 0;
        public int sbnum = 0;
        public int paintX = 0;
        public int numjs = 0;
        private int page = 0;
        private string content = "";
        //关机功能
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
         ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }
        private void SaveConfigEasy()
        {
            List<infos> daifa = new List<infos>();
            foreach (ListViewItem i in this.lsvdaifa.Items)
            {
                infos info = new infos();
                info.title = i.Text;
                info.info = i.SubItems[1].Text;
                daifa.Add(info);
            }
            AShelp.SaveTitle(daifa, "df");
            AShelp.SavebakTitle(daifa, "df");
            List<infos> yifa = new List<infos>();
            foreach (ListViewItem i in this.lsvchenggong.Items)
            {
                infos info = new infos();
                info.title = i.Text;
                info.info = i.SubItems[1].Text;
                if (i.Tag != null)
                    info.stitle = i.Tag.ToString();
                else
                    info.stitle = "";
                yifa.Add(info);
            }
            AShelp.SaveTitle(yifa, "cg");
            AShelp.SavebakTitle(yifa, "cg");
            List<infos> shibai = new List<infos>();
            foreach (ListViewItem i in this.lsvshibai.Items)
            {
                infos info = new infos();
                info.title = i.Text;
                info.info = i.SubItems[1].Text;
                shibai.Add(info);
            }
            AShelp.SaveTitle(shibai, "sb");
            AShelp.SavebakTitle(shibai, "sb");
        }
        public int numsv = 0;
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (numsv < 300)
                numsv++;
            else
            {
                numsv = 0;
                try
                {
                    SaveConfigEasy();
                }
                catch (Exception ex)
                {

                }
            }
            if (numjs < 20)
                numjs++;
            else
            {
                paintX = paintX + 1;
                if (paintX > Myinfo.gginfo.Length - 1) paintX = 0;
                toolStripStatusLabel1.Text = Myinfo.gginfo.Substring(paintX);
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            frmGG f = new frmGG(Myinfo.gginfo);
            f.ShowDialog();
        }
        public frmMain(string name, string pass)
        {
            this._login_name = name;
            this._login_pass = pass;

            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void 官方网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Myinfo.ip);
        }
        public static void InitMenu(ToolStripMenuItem tsMi, EventHandler e)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(Application.StartupPath + @"\skins");
            string str, filename;
            ToolStripMenuItem temp = new ToolStripMenuItem();
            temp.Text = "默认";
            temp.Tag = "no";
            temp.Click += new EventHandler(e);
            tsMi.DropDownItems.Add(temp);
            FileInfo[] allfile = null;
            if (Directory.Exists(Application.StartupPath + @"\skins"))
                allfile = dirinfo.GetFiles("*.ssk");
            if (allfile != null)
                foreach (FileInfo fi in allfile)
                {
                    temp = new ToolStripMenuItem();
                    str = fi.Name;
                    filename = str.Substring(0, str.IndexOf('.'));
                    temp.Text = filename;
                    temp.Tag = fi.FullName;
                    temp.Click += new EventHandler(e);
                    tsMi.DropDownItems.Add(temp);
                }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //debugseo();
            txtName.Text = this._login_name;
            toolStripStatusLabel1.Text = Myinfo.gginfo;
            lispic = this.txtPics;
            txtCity = this.txtzhubl;
            randpic = this.txtrandpic;
            lispic = this.txtPics;
            regtime = Myinfo.softtime;
            this.tsslName.Text = "用户：" + Myinfo.username;
            this.Text = Myinfo.sname + Myinfo.comname;
            cbbone.DataSource = GetListData();
            cbbone.DisplayMember = "EngName";
            cbbone.ValueMember = "ChsName";
            comboBox3.DataSource = GetListDataXW();
            comboBox3.DisplayMember = "EngName";
            comboBox3.ValueMember = "ChsName";
            List<string> source = AShelp.Load(Myinfo.username + "\\usernet");
            if (source != null)
            {
                this.txtName.Text = source[0];
                this.txtPwd.Text = source[1];
            }
            #region 进度条
            //Form1_Load
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;

            //CheckUpdate为窗体显示过程中需要处理算法的方法
            worker.DoWork += new DoWorkEventHandler(CheckUpdate);
            worker.RunWorkerAsync();

            //显示进度窗体
            frmProgress f = new frmProgress(worker);
            f.Text = "正在加载，请耐心等待...";
            f.ShowDialog(this);
            #endregion
            dts = DateTime.Now;
        }
        //处理算法CheckUpdate，注意参数一致
        public void CheckUpdate(object sender, DoWorkEventArgs e)
        {
            initData();
        }
        //参数2第一参数
        private static List<Category> GetListData()
        {
            List<Category> list = new List<Category>();
            Category c = new Category();
            c.EngName = "机械模具";
            c.ChsName = "1";
            list.Add(c);
            c = new Category();
            c.EngName = "化工材料";
            c.ChsName = "2";
            list.Add(c);
            c = new Category();
            c.EngName = "通信产品";
            c.ChsName = "3";
            list.Add(c);
            c = new Category();
            c.EngName = "工具量具";
            c.ChsName = "4";
            list.Add(c);
            c = new Category();
            c.EngName = "五金电工";
            c.ChsName = "5";
            list.Add(c);
            c = new Category();
            c.EngName = "皮革纺织";
            c.ChsName = "6";
            list.Add(c);
            c = new Category();
            c.EngName = "印刷包装";
            c.ChsName = "7";
            list.Add(c);
            c = new Category();
            c.EngName = "汽车保养";
            c.ChsName = "8";
            list.Add(c);
            c = new Category();
            c.EngName = "矿山能源";
            c.ChsName = "9";
            list.Add(c);
            c = new Category();
            c.EngName = "建筑建材";
            c.ChsName = "10";
            list.Add(c);
            c = new Category();
            c.EngName = "农机配件";
            c.ChsName = "11";
            list.Add(c);
            c = new Category();
            c.EngName = "食品饮料";
            c.ChsName = "12";
            list.Add(c);
            c = new Category();
            c.EngName = "养殖种植";
            c.ChsName = "13";
            list.Add(c);
            c = new Category();
            c.EngName = "文教数码";
            c.ChsName = "14";
            list.Add(c);
            c = new Category();
            c.EngName = "办公日用";
            c.ChsName = "15";
            list.Add(c);
            c = new Category();
            c.EngName = "运动休闲";
            c.ChsName = "16";
            list.Add(c);
            c = new Category();
            c.EngName = "二手回收";
            c.ChsName = "17";
            list.Add(c);
            c = new Category();
            c.EngName = "物流维修";
            c.ChsName = "18";
            list.Add(c);
            c = new Category();
            c.EngName = "饰品礼品";
            c.ChsName = "19";
            list.Add(c);
            return list;
        }
        //参数2供应类型
        private static List<Category> GetListDataXW()
        {
            List<Category> list = new List<Category>();
            Category c = new Category();
            c.EngName = "行业新闻";
            c.ChsName = "22";
            list.Add(c);
            return list;
        }
        private void initData()
        {
            string[] configs = AShelp.GetConfigs();//得到所有配置
            foreach (string s in configs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = s;
                lvi.ImageIndex = 0;
                this.lsvConfig.Items.Add(lvi);
                //复制Database1.dll
                AShelp.DBcheck(s);
            }
            List<string> source = AShelp.LoadConfig("use");
            if (source != null && source.Count > 0)
            {
                Myinfo.configname = source[0];
                lblconfig.Text = "当前配置是：" + Myinfo.configname;
                label65.Text = "当前配置是：" + Myinfo.configname;//lhc1

            }
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\bak";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            LoadConfig();
            UpdateTabNum();

        }
        #region 加载configName lhc1
        private void LoadConfigName()
        {
            List<string> con = AShelp.LoadCategoryConfig("configName");
            if (con != null && con.Count > 0)
            {
                for (int i = 0; i < con.Count; i++)
                {
                    if (!configlistbox.Items.Contains(con[i]))
                        this.configlistbox.Items.Add(con[i]);
                }
            }
        }
        #endregion
        private void LoadConfig()
        {
            #region lhc1
            List<string> pz;
            try
            {
                pz = AShelp.LoadCategoryConfig("pz");
            }
            catch (Exception e)
            {
                pz = null;
            }
            if (pz != null && pz.Count == 1)
            {
                if (pz[0] == "1")
                {
                    radioButton27.Checked = true;
                    radioButton26.Checked = false;
                }
                else
                {
                    radioButton27.Checked = false;
                    radioButton26.Checked = true;
                }
            }
            LoadConfigName();
            #endregion
            List<string> dl;
            try
            {
                dl = AShelp.LoadWZ("adddl");
            }
            catch (Exception e)
            {
                dl = AShelp.LoadWZ("bak\\adddl");
            }
            if (dl != null && dl.Count == 16)
            {
                if (dl[0] == "1")
                    radioButton4.Checked = true;
                else
                    radioButton6.Checked = true;
                textBox10.Text = dl[1];
                if (dl[2] == "1")
                    radioButton9.Checked = true;
                else
                    radioButton8.Checked = true;
                textBox11.Text = dl[3];

                if (dl[4] == "1")
                    radioButton11.Checked = true;
                else
                    radioButton10.Checked = true;
                textBox12.Text = dl[5];
                if (dl[6] == "1")
                    radioButton13.Checked = true;
                else
                    radioButton12.Checked = true;
                textBox13.Text = dl[7];
                if (dl[8] == "1")
                    radioButton15.Checked = true;
                else
                    radioButton14.Checked = true;
                textBox14.Text = dl[9];

                if (dl[10] == "1")
                    radioButton17.Checked = true;
                else
                    radioButton16.Checked = true;
                try
                {
                    numericUpDown1.Value = Convert.ToDecimal(dl[11]);//自动发布，时
                    if (dl[12] == "1")
                        radioButton21.Checked = true;
                    else
                        radioButton20.Checked = true;
                    numericUpDown2.Value = Convert.ToDecimal(dl[13]);//自动发布，分
                }
                catch (Exception ex)
                {

                }
                label30.Text = dl[14];
                richtxtparsum.Text = dl[15];
            }
            //修改当前使用配置的图标状态
            for (int i = 0; i < this.lsvConfig.Items.Count; i++)
            {
                if (Myinfo.configname == this.lsvConfig.Items[i].Text)
                {
                    this.lsvConfig.Items[i].ImageIndex = 1;
                    break;
                }
            }
            //修改HTML视图
            string[] mbs = AShelp.GetHtmls();
            ckbHtml.Items.Clear();
            if (mbs != null)
            {
                foreach (string mb in mbs)
                {
                    ckbHtml.Items.Add(mb);
                }
            }
            List<string> ckdhtml = null;
            try
            {
                ckdhtml = AShelp.LoadWZ("ckdhtml");
            }
            catch (Exception e)
            {
                ckdhtml = AShelp.LoadWZ("bak\\ckdhtml");
            }
            if (ckdhtml != null)
            {
                foreach (string ckdh in ckdhtml)
                {
                    for (int i = 0; i < this.ckbHtml.Items.Count; i++)
                    {
                        if (ckbHtml.GetItemText(ckbHtml.Items[i]) == ckdh)
                            this.ckbHtml.SetItemChecked(i, true);
                    }
                }
            }

            List<string> wz = null;
            try
            {
                wz = AShelp.LoadWZ("WZ");
            }
            catch (Exception e)
            {

                wz = AShelp.LoadWZ("bak\\WZ");
            }
            if (wz != null && wz.Count == 26)
            {
                txtzhubl.Text = wz[0];
                txtbl1.Text = wz[1];
                if (wz[2] == "1")
                    ckbbl1.Checked = true;
                txtbl2.Text = wz[3];
                if (wz[4] == "1")
                    ckbbl2.Checked = true;
                txtbl3.Text = wz[5];
                if (wz[6] == "1")
                    ckbbl3.Checked = true;
                txtrandpic.Text = wz[7];
                txtdl1.Text = wz[8];
                if (wz[9] == "0")
                    rbtdl1r.Checked = false;
                txtdl1row.Text = wz[10];
                txtdl2.Text = wz[11];
                if (wz[12] == "0")
                    rbtdl2r.Checked = false;
                txtdl2row.Text = wz[13];
                txtdl3.Text = wz[14];
                if (wz[15] == "0")
                    rbtdl3r.Checked = false;
                txtdl3row.Text = wz[16];
                txtdl4.Text = wz[17];
                if (wz[18] == "0")
                    rbtdl4r.Checked = false;
                txtdl4row.Text = wz[19];

                txtTitle.Text = wz[20];
                if (wz[21] == "0")
                    ckbSCDL.Checked = false;
                txtkeyword1.Text = wz[22];
                txtkeyword2.Text = wz[23];
                txtkeyword3.Text = wz[24];
                txttishi.Text = wz[25];
            }
            List<string> add = null;
            try
            {
                add = AShelp.LoadWZ("add");
            }
            catch (Exception e)
            {

                add = AShelp.LoadWZ("bak\\add");
            }
            if (add != null)
            {
                textBox31.Text = add[0];
                if (add.Count > 1)
                {
                    if (add[1] == "1")
                        ckbmygj.Checked = true;
                }
                if (add.Count > 2)
                {
                    if (add[2] == "1")
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;
                }
                if (add.Count > 3)
                {
                    txtjgmgc.Text = add[3];
                }
            }

            List<infos> df = null;
            try
            {
                df = AShelp.LoadTitle("df");
            }
            catch (Exception e)
            {

                df = AShelp.LoadTitle("bak\\df");
            }
            lsvdaifa.Items.Clear();
            if (df != null)
            {

                foreach (infos minfo in df)
                {
                    ListViewItem lsv = new ListViewItem();
                    lsv.Text = minfo.title;
                    lsv.SubItems.Add(minfo.info);
                    lsv.Tag = minfo.stitle;
                    this.lsvdaifa.Items.Add(lsv);
                }
                label45.Text = string.Format("共有{0}个信息待发", df.Count);
            }
            List<infos> cg = null;
            try
            {
                cg = AShelp.LoadTitle("cg");
            }
            catch (Exception e)
            {

                cg = AShelp.LoadTitle("bak\\cg");
            }
            lsvchenggong.Items.Clear();
            if (cg != null)
            {

                foreach (infos minfo in cg)
                {
                    ListViewItem lsv = new ListViewItem();
                    lsv.Text = minfo.title;
                    lsv.SubItems.Add(minfo.info);
                    this.lsvchenggong.Items.Add(lsv);
                }
            }
            List<infos> sb = null;
            try
            {
                sb = AShelp.LoadTitle("sb");
            }
            catch (Exception e)
            {

                sb = AShelp.LoadTitle("bak\\sb");
            }
            lsvshibai.Items.Clear();
            if (sb != null)
            {

                foreach (infos minfo in sb)
                {
                    ListViewItem lsv = new ListViewItem();
                    lsv.Text = minfo.title;
                    lsv.SubItems.Add(minfo.info);
                    this.lsvshibai.Items.Add(lsv);
                }
            }
            UpdateTabNum();
            List<string> ts = null;
            try
            {
                ts = AShelp.LoadWZ("ts");
            }
            catch (Exception e)
            {
                ts = AShelp.LoadWZ("bak\\ts");
            }
            if (ts != null && ts.Count == 24)
            {
                txtSinterval.Text = ts[0];
                txtEinterval.Text = ts[1];
                if (ts[2] == "0")
                {
                    rbpubtbig.Checked = false;
                    rbpubtnum.Checked = true;
                }
                txtPubnum.Text = ts[3];
                if (ts[4] == "1")
                    ckbfb1.Checked = true;
                else
                    ckbfb1.Checked = false;
                if (ts[5] == "1")
                    ckbfb5.Checked = true;
                else
                    ckbfb5.Checked = false;
                if (ts[6] == "1")
                    ckbfb3.Checked = true;
                if (ts[7] == "1")
                    ckbfb4.Checked = true;
                if (ts[8] == "1")
                    ckblinkpre.Checked = true;
                if (ts[9] == "1")
                    ckbshut.Checked = true;
                if (ts[10] == "0")
                    radioButton2.Checked = true;
                txtmini.Text = ts[11];
                if (ts[12] == "1")
                    checkBox3.Checked = true;
                else
                    checkBox3.Checked = false;
                txtrandpic.Text = ts[13];
                cbbone.Text = ts[14];
                comboBox3.Text = ts[15];
                txt_pinpai.Text = ts[16];
                txt_xinghao.Text = ts[17];
                txt_city.Text = ts[18];
                txt_ghzl.Text = ts[19];
                txt_qdl.Text = ts[20];
                txt_price.Text = ts[21];
                txt_unit.Text = ts[22];
                txtFindcode.Text = ts[23];
            }
            try
            {
                //dealrichpar();//提前处理不规则段落
                //if (this.richtxtparsum.Text != "")
                //{
                //    string[] lines = this.richtxtparsum.Lines;
                //    this.achelp = new AccessHelper();
                //    string a = this.achelp.GetDataTableFromDB("SELECT count(*) FROM paragraph").Rows[0][0].ToString();
                //    int pNo;
                //    if (a == "0")
                //    {
                //        pNo = 1000001;
                //    }
                //    else
                //    {
                //        DataRow dataRow = this.achelp.GetDataTableFromDB("SELECT TOP 1 ID FROM paragraph ORDER BY paragraph.ID DESC").Rows[0];
                //        pNo = 1000001 + Convert.ToInt32(dataRow[0]);
                //    }
                //    string[] array = lines;
                //    for (int j = 0; j < array.Length; j++)
                //    {
                //        string pContent = array[j];
                //        string strSql = string.Concat(new object[]
                //        {
                //            "insert into paragraph (PraNo,PraContent,AddTime,QualityReport,UsedCount) values ('N",
                //            pNo,
                //            "','",
                //            pContent.Replace("'",""),
                //            "','",
                //            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                //            "','优质','0')"
                //        });
                //        int count = this.achelp.ExcuteSql(strSql);
                //        pNo++;
                //    }
                //    this.richtxtparsum.Text = "";
                //    this.SaveConfig();
                //}
                this.databind1();
            }
            catch (Exception ex)
            {
            }
        }
        public void databind1()
        {
            this.achelp = new AccessHelper();
            string strSql = "select * from paragraph";
            DataTable dataSource = new DataTable();
            dataSource = this.achelp.GetDataTableFromDB(strSql);
            this.dgvpracontent.AutoGenerateColumns = false;
            this.dgvpracontent.DataSource = dataSource;
            this.label30.Text = "段落库：共 " + this.dgvpracontent.Rows.Count + " 行";
        }
        private void dealrichpar()
        {
            if (richtxtparsum.Text.Trim() == "")
                return;
            //手机号码
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, "0?(13|14|15|16|17|18)[0-9]{9}", " ");
            //电话号码
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, "[0-9-()（）]{7,18}", " ");
            //url
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, @"^((https|http|ftp|rtsp|mms)?:\/\/)[^\s]+", " ");
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$", " ");
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?", " ");
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, @"(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?", " ");
            //Email
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}", " ");
            //QQ
            richtxtparsum.Text = Regex.Replace(richtxtparsum.Text, "[1-9]([0-9]{5,11})", " ");


            List<string> strList = new List<string>();
            string ss = richtxtparsum.Text.Replace("\r\n", "").Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("　", "");
            string[] sArray = ss.Split(new char[] { '。', '？', '！' });
            if (sArray.Length > 1)//有标点时
            {
                string str = "";
                string s = "";
                for (int i = 0; i < sArray.Length; i++)
                {
                    str = sArray[i];
                    if (str.Length <= 100)
                    {
                        s += str + "。";
                        if (s.Length > 100)
                        {
                            if (str.StartsWith("　　"))
                                strList.Add(s);
                            else
                                strList.Add("　　" + s);
                            s = "";
                        }
                    }
                    else if (str.Length > 100 && str.Length < 150)
                    {
                        if (str.StartsWith("　　"))
                            strList.Add(str + "。");
                        else
                            strList.Add("　　" + str + "。");
                    }
                    else if (str.Length >= 150)
                    {
                        str = str.Substring(0, 150);
                        if (str.StartsWith("　　"))
                            strList.Add(str + "。");
                        else
                            strList.Add("　　" + str + "。");
                    }
                }
            }
            //无标点时
            else if (sArray.Length < 2 && ss.Length > 120)
            {
                for (int i = 0; i < ss.Length - 120; i += 120)
                {
                    string str = "";
                    str = ss.Substring(i, 120);
                    if (str.StartsWith("　　"))
                        strList.Add(str + "。");
                    else
                        strList.Add("　　" + str + "。");
                }
            }
            richtxtparsum.Text = string.Join("\r\n", strList.ToArray());
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您真的要退出吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                SaveConfig();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string key = "AutoSend";
            string iv = "weiwei@1";

            string strPass = this._login_name + "|" + this._login_pass + "|" + Myinfo.sid + "|" + DateTime.Now;

            string md5strPass = Tools.Encode(strPass, key, iv);

            WebReference.Heart hwm = new WebReference.Heart();


            //result:0|时间，代表退出失败，1|时间，代表退出成功
            try
            {
                string result = hwm.SetIsUse(md5strPass);

                string re = Tools.Decode(result, key, iv);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("无法连接到远程服务器"))
                {
                    MessageBox.Show("无法连接到远程服务器,程序将退出！");
                }

            }
            finally
            {
                Myinfo.mylogin.notifyIcon1.Visible = false;
                Application.ExitThread();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (regtime.TotalSeconds > 0)
            {
                this.tsslTime.Text = "剩余时间：" + AShelp.DateDiff(ref regtime);
                this.tsslPhone.Text = "软件运行时间：" + AShelp.DateDiff(DateTime.Now, dts);
            }
            else
            {
                if (t != null)
                    t.Abort();
                timer1.Stop();
                MessageBox.Show("软件已到期,请联系软件公司！");
                Application.ExitThread();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void btnshow_Click(object sender, EventArgs e)
        {
            if (txtPwd.PasswordChar == '*')
            {
                txtPwd.PasswordChar = '\0';
                btnshow.Text = "隐";
            }
            else
            {
                txtPwd.PasswordChar = '*';
                btnshow.Text = "显";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lbllogininfo.Text = "正在登录，请耐心等待！";
            btnLogin.Enabled = false;
            lbllogininfo.Refresh();
            btnLogin.Refresh();
            string name = txtName.Text.Trim();
            string pwd = txtPwd.Text.Trim();
            string code = txtCode.Text.Trim();
            string objjson = myhttp.Login(name, pwd, code);
            if (objjson == "ok")
            {
                lbllogininfo.Text = "登录成功！";
                btnLogin.Enabled = false;
                myhttp.islogin = "OK";
                if (ckbremenberme.Checked)
                {
                    AShelp.Save(this.txtName.Text, this.txtPwd.Text, "", "", Myinfo.username + "\\usernet");
                }
            }
            else
            {
                lbllogininfo.Text = "登录不成功！";
                MessageBox.Show(objjson, "提示");
                btnLogin.Enabled = true;
                picCode_Click(null, null);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (myhttp.islogin == "OK")
            {
                string result = NetHelper.HttpGet("http://www.dh0315.com/new/?logout.html", "", Encoding.UTF8);
                btnLogin.Enabled = true;
                lbllogininfo.Text = "退出成功！";
                picCode.Image = myhttp.getCodeimage();
            }
        }

        private void picCode_Click(object sender, EventArgs e)
        {
            picCode.Image = myhttp.getCodeimage();
        }

        private void ckbremenberme_Click(object sender, EventArgs e)
        {
            if (ckbremenberme.Checked)
            {

            }
        }

        private void btnimgmanager_Click(object sender, EventArgs e)
        {
            frmimg f = new frmimg(this);
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            richTextBox1.SelectedText = "【标题】";
            this.richTextBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myhttp.islogin == "OK")
            {
                return;
            }
            else
            {
                MessageBox.Show("请先登录再操作！");
            }

        }

        private void cbbone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbtwo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPics_TextChanged(object sender, EventArgs e)
        {
            this.groupBox1.Text = string.Format("标题图片，共有{0}行", txtPics.Lines.Length);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            frmareacs f = new frmareacs(this);
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            txtzhubl.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string[] s = txtzhubl.Lines;
            txtzhubl.Lines = AShelp.RandomStrings(s);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            txtbl1.Text = "";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string[] s = txtbl1.Lines;
            txtbl1.Lines = AShelp.RandomStrings(s);
        }

        private void txtzhubl_TextChanged(object sender, EventArgs e)
        {
            label25.Text = string.Format("主变量，共有{0}行", txtzhubl.Lines.Length);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            txtbl2.Text = "";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string[] s = txtbl2.Lines;
            txtbl2.Lines = AShelp.RandomStrings(s);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            txtbl3.Text = "";
        }

        private void button27_Click(object sender, EventArgs e)
        {
            string[] s = txtbl3.Lines;
            txtbl3.Lines = AShelp.RandomStrings(s);
        }

        private void txtbl2_TextChanged(object sender, EventArgs e)
        {
            label27.Text = string.Format("变量2，共有{0}行", txtbl2.Lines.Length);

        }

        private void txtbl1_TextChanged(object sender, EventArgs e)
        {
            label26.Text = string.Format("变量1，共有{0}行", txtbl1.Lines.Length);
        }

        private void txtbl3_TextChanged(object sender, EventArgs e)
        {
            label28.Text = string.Format("变量3，共有{0}行", txtbl3.Lines.Length);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            frmkeyword f = new frmkeyword(this);
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtzhubl.Lines.Length > 0)
                txtTitle.SelectedText = "【主变量】";
            else
                MessageBox.Show("主变量没有数据");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtbl1.Lines.Length > 0)
                txtTitle.SelectedText = "【变量1】";
            else
                MessageBox.Show("变量1没有数据");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtbl2.Lines.Length > 0)
                txtTitle.SelectedText = "【变量2】";
            else
                MessageBox.Show("变量2没有数据");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtbl3.Lines.Length > 0)
                txtTitle.SelectedText = "【变量3】";
            else
                MessageBox.Show("变量3没有数据");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtzhubl.Lines.Length > 0)
                txtkeyword1.SelectedText = "【主变量】";
            else
                MessageBox.Show("主变量没有数据");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (txtbl1.Lines.Length > 0)
                txtkeyword1.SelectedText = "【变量1】";
            else
                MessageBox.Show("变量1没有数据");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtbl2.Lines.Length > 0)
                txtkeyword1.SelectedText = "【变量2】";
            else
                MessageBox.Show("变量2没有数据");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtbl3.Lines.Length > 0)
                txtkeyword1.SelectedText = "【变量3】";
            else
                MessageBox.Show("变量3没有数据");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (txtzhubl.Lines.Length > 0)
                txtkeyword2.SelectedText = "【主变量】";
            else
                MessageBox.Show("主变量没有数据");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtbl1.Lines.Length > 0)
                txtkeyword2.SelectedText = "【变量1】";
            else
                MessageBox.Show("变量1没有数据");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtbl2.Lines.Length > 0)
                txtkeyword2.SelectedText = "【变量2】";
            else
                MessageBox.Show("变量2没有数据");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtbl3.Lines.Length > 0)
                txtkeyword2.SelectedText = "【变量3】";
            else
                MessageBox.Show("变量3没有数据");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (txtzhubl.Lines.Length > 0)
                txtkeyword3.SelectedText = "【主变量】";
            else
                MessageBox.Show("主变量没有数据");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (txtbl1.Lines.Length > 0)
                txtkeyword3.SelectedText = "【变量1】";
            else
                MessageBox.Show("变量1没有数据");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (txtbl2.Lines.Length > 0)
                txtkeyword3.SelectedText = "【变量2】";
            else
                MessageBox.Show("变量2没有数据");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (txtbl3.Lines.Length > 0)
                txtkeyword3.SelectedText = "【变量3】";
            else
                MessageBox.Show("变量3没有数据");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ckbHtml.Items.Count; i++)
            {
                this.ckbHtml.SetItemChecked(i, true);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ckbHtml.Items.Count; i++)
            {
                if (ckbHtml.GetItemChecked(i))
                {
                    this.ckbHtml.SetItemChecked(i, false);
                }
                else
                {
                    this.ckbHtml.SetItemChecked(i, true);
                }

            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ckbHtml.Items.Count; i++)
            {
                this.ckbHtml.SetItemChecked(i, false);
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (txtzhubl.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.SelectedText += "【主变量】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("主变量没有数据");

        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (txtbl1.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.SelectedText += "【变量1】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("变量1没有数据");

        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (txtbl2.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.SelectedText = "【变量2】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("变量2没有数据");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (txtbl3.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.SelectedText = "【变量3】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("变量3没有数据");
        }

        private void button51_Click(object sender, EventArgs e)
        {

        }
        //lhccj
        private void button54_Click(object sender, EventArgs e)
        {
            if (dgvpracontent.Rows.Count < 1)
            {
                MessageBox.Show("段落库没有数据");
                return;
            }
            txtmbname.Focus();
            richTextBox1.SelectedText = "【段落】";
            this.richTextBox1.Focus();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (txtdl2.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.Text += "【段落2】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("段落2没有数据");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (txtdl3.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.Text += "【段落3】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("段落3没有数据");
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (txtdl4.Lines.Length > 0)
            {
                txtmbname.Focus();
                richTextBox1.Text += "【段落4】";
                this.richTextBox1.Focus();
            }
            else
                MessageBox.Show("段落4没有数据");
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (txtrandpic.Lines.Length < 1)
            {
                MessageBox.Show("随机图片没有数据");
                return;
            }
            txtmbname.Focus();
            richTextBox1.SelectedText = "【随机图片】";
            this.richTextBox1.Focus();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            richTextBox1.SelectedText = "【年】";
            this.richTextBox1.Focus();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            richTextBox1.SelectedText = "【月】";
            this.richTextBox1.Focus();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            txtmbname.Focus();
            richTextBox1.SelectedText = "【日】";
            this.richTextBox1.Focus();
        }

        private void button98_Click(object sender, EventArgs e)
        {
            Regex reb = new Regex("【标题】");
            MatchCollection mab = reb.Matches(richTextBox1.Text);
            if (mab.Count > 2)
            {
                MessageBox.Show("最多插入2个标题");
                return;
            }
            Regex red = new Regex("【段落】");
            MatchCollection mad = red.Matches(richTextBox1.Text);
            if (mad.Count < 3 || mad.Count > 5)
            {
                MessageBox.Show("必须插入3到5个段落");
                return;
            }
            Regex ret = new Regex("【随机图片】");
            MatchCollection mat = ret.Matches(richTextBox1.Text);
            if (mat.Count < 2 || mat.Count > 5)
            {
                MessageBox.Show("必须插入2到5张图片");
                return;
            }
            this.ckbHtml.Items.Add(txtmbname.Text);
        }

        private void button97_Click(object sender, EventArgs e)
        {
            //保存模版
            Regex reb = new Regex("【标题】");
            MatchCollection mab = reb.Matches(richTextBox1.Text);
            if (mab.Count > 2)
            {
                MessageBox.Show("最多插入2个标题");
                return;
            }
            //Regex red = new Regex("【段落】");
            //MatchCollection mad = red.Matches(richTextBox1.Text);
            //if (mad.Count < 3 || mad.Count > 4)
            //{
            //    MessageBox.Show("必须插入3到4个段落");
            //    return;
            //}
            //Regex ret = new Regex("【随机图片】");
            //MatchCollection mat = ret.Matches(richTextBox1.Text);
            //if (mat.Count < 2 || mat.Count > 5)
            //{
            //    MessageBox.Show("必须插入2到5张图片");
            //    return;
            //}
            string mb = txtmbname.Text.Trim();
            string html = richTextBox1.Text;
            Regex r = new Regex("style=\"(?<key>.*?)\"");
            html = r.Replace(html, "");
            AShelp.SaveHtml(html, mb);
            bool ishave = false;
            for (int i = 0; i < ckbHtml.Items.Count; i++)
            {
                if (ckbHtml.GetItemText(ckbHtml.Items[i]) == mb)
                    ishave = true;
            }
            if (!ishave)
            {
                ckbHtml.Items.Add(mb);
            }
            MessageBox.Show("保存成功！~~");
        }

        private void button45_Click(object sender, EventArgs e)
        {
            //显示第一个选择的模版  
            string temp = "";
            for (int i = 0; i < ckbHtml.Items.Count; i++)
            {
                if (ckbHtml.GetItemChecked(i))
                {
                    temp = ckbHtml.GetItemText(ckbHtml.Items[i]);
                    txtmbname.Text = temp;
                    string html = AShelp.LoadHtml(temp);
                    richTextBox1.Text = html;
                    return;

                }
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            frmjz f = new frmjz(this);
            f.TopMost = true;
            f.ShowDialog();
        }

        private void txtjz_TextChanged(object sender, EventArgs e)
        {
        }

        private void button30_Click(object sender, EventArgs e)
        {
        }

        private void button40_Click(object sender, EventArgs e)
        {
            txtdl4.Text = "";
        }

        private void button34_Click(object sender, EventArgs e)
        {
            txtdl1.Text = "";
        }

        private void button36_Click(object sender, EventArgs e)
        {
            txtdl2.Text = "";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            txtdl3.Text = "";
        }

        private void button33_Click(object sender, EventArgs e)
        {
            string[] s = txtdl1.Lines;
            txtdl1.Lines = AShelp.RandomStrings(s);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            string[] s = txtdl2.Lines;
            txtdl2.Lines = AShelp.RandomStrings(s);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            string[] s = txtdl3.Lines;
            txtdl3.Lines = AShelp.RandomStrings(s);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            string[] s = txtdl4.Lines;
            txtdl4.Lines = AShelp.RandomStrings(s);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            txttishi.Text = "";
        }

        private void btnSCtitle_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            List<string> sctitle = new List<string>();
            List<string> sctitle1 = new List<string>();
            string[] city = txtzhubl.Lines;

            int intcity = city.Length;
            string[] bl1 = txtbl1.Lines;
            int intbl1 = bl1.Length;
            string[] bl2 = txtbl2.Lines;
            int intbl2 = bl2.Length;
            string[] bl3 = txtbl3.Lines;
            int intbl3 = bl3.Length;
            sctitle.Add(title);
            bool ishvae = false;
            Random rd = new Random();
            do
            {
                ishvae = false;
                foreach (string st in sctitle)
                {
                    if (st.Contains("【主变量】") && intcity > 0)
                    {
                        Regex r = new Regex("【主变量】");
                        string t = "";
                        foreach (string s in city)
                        {
                            t = r.Replace(st, s);
                            sctitle1.Add(t + "【】" + s);
                        }
                        ishvae = true;
                    }
                }
                if (sctitle1.Count > 0)
                {
                    sctitle.Clear();
                    foreach (string s in sctitle1)
                        sctitle.Add(s);
                    sctitle1.Clear();
                }

            } while (ishvae);
            do
            {
                ishvae = false;
                foreach (string st in sctitle)
                {
                    if (st.Contains("【变量1】") && intbl1 > 0)
                    {
                        Regex r = new Regex("【变量1】");
                        string t = "";
                        if (!ckbbl1.Checked)
                            foreach (string s in bl1)
                            {
                                t = r.Replace(st, s, 1);
                                sctitle1.Add(t);
                            }
                        else
                        {

                            t = r.Replace(st, bl1[rd.Next(intbl1)], 1);
                            sctitle1.Add(t);
                        }
                        ishvae = true;
                    }
                }
                if (sctitle1.Count > 0)
                {
                    sctitle.Clear();
                    foreach (string s in sctitle1)
                        sctitle.Add(s);
                    sctitle1.Clear();
                }

            } while (ishvae);
            do
            {
                ishvae = false;
                foreach (string st in sctitle)
                {
                    if (st.Contains("【变量2】") && intbl2 > 0)
                    {
                        Regex r = new Regex("【变量2】");
                        string t = "";
                        if (!ckbbl2.Checked)
                            foreach (string s in bl2)
                            {
                                t = r.Replace(st, s, 1);
                                sctitle1.Add(t);
                            }
                        else
                        {
                            t = r.Replace(st, bl2[rd.Next(intbl2)], 1);
                            sctitle1.Add(t);
                        }
                        ishvae = true;
                    }
                }
                if (sctitle1.Count > 0)
                {
                    sctitle.Clear();
                    foreach (string s in sctitle1)
                        sctitle.Add(s);
                    sctitle1.Clear();
                }

            } while (ishvae);
            do
            {
                ishvae = false;
                foreach (string st in sctitle)
                {
                    if (st.Contains("【变量3】") && intbl3 > 0)
                    {
                        Regex r = new Regex("【变量3】");
                        string t = "";
                        if (!ckbbl3.Checked)
                            foreach (string s in bl3)
                            {
                                t = r.Replace(st, s, 1);
                                sctitle1.Add(t);
                            }
                        else
                        {
                            t = r.Replace(st, bl3[rd.Next(intbl3)], 1);
                            sctitle1.Add(t);
                        }
                        ishvae = true;
                    }
                }
                if (sctitle1.Count > 0)
                {
                    sctitle.Clear();
                    foreach (string s in sctitle1)
                        sctitle.Add(s.Trim());
                    sctitle1.Clear();
                }

            } while (ishvae);
            //sctitle为生成的数据
            if (MessageBox.Show("生成数据" + sctitle.Count + "行！您确定要生成这些数据，如果数据量太大，请选择随机复选框对数据进行重新组合！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string[] resultstr = null;
                if (ckbSCDL.Checked)
                {
                    resultstr = AShelp.RandomStrings(sctitle.ToArray());
                }
                else
                {
                    resultstr = sctitle.ToArray();
                }

                if (sctitle.Count > 10000)
                {
                    MessageBox.Show("数据量已超过10000，为了不影响速度系统只生成10000条数据！");
                    string[] resultstr1 = new string[10000];
                    Array.Copy(resultstr, 0, resultstr1, 0, 10000);
                    resultstr = resultstr1;
                }
                foreach (string stitle in resultstr)
                {
                    ListViewItem lvt = new ListViewItem();
                    string[] stitles = stitle.Split(new string[] { "【】" }, StringSplitOptions.None);
                    if (stitles.Length == 2)
                    {
                        lvt.Text = stitles[0];
                        lvt.Tag = stitles[1];
                    }
                    else
                    {
                        lvt.Text = stitle;
                        lvt.Tag = "";
                    }
                    lvt.SubItems.Add("等待发送");
                    lsvdaifa.Items.Add(lvt);
                }
                tabControl5.TabPages[0].Text = string.Format("待发列表({0})", lsvdaifa.Items.Count);
                MessageBox.Show("生成成功！~");

            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            txtrandpic.Text = "";
        }

        private void txtrandpic_TextChanged(object sender, EventArgs e)
        {
            groupBox11.Text = string.Format("内容图片，共有{0}行", txtrandpic.Lines.Length);
        }

        private void button95_Click(object sender, EventArgs e)
        {
            string checkedname = "";
            int lsindex = -1;
            for (int i = 0; i < this.lsvConfig.Items.Count; i++)
            {
                if (this.lsvConfig.Items[i].Selected)
                {
                    checkedname = this.lsvConfig.Items[i].Text;
                    lsindex = i;
                    break;
                }
            }
            if (checkedname != "")
            {
                if (Myinfo.configname != checkedname)
                {
                    string cname = "";
                    frmInput f = new frmInput();
                    f.TopMost = true;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        cname = f.inputName;
                        if (AShelp.ChangeConfigs(checkedname, cname))
                        {
                            this.lsvConfig.Items[lsindex].Text = cname;
                            #region 修改configName文件 lhc1
                            if (configlistbox.Items.Contains(checkedname))
                            {
                                for (int i = 0; i < configlistbox.Items.Count; i++)
                                {
                                    if (configlistbox.Items[i].ToString() == checkedname)
                                    {
                                        configlistbox.Items.RemoveAt(i);
                                        configlistbox.Items.Insert(i, cname);
                                    }
                                }
                                SaveConfigName();
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    MessageBox.Show("不能修改当前配置");

                }
            }
            else
            {
                MessageBox.Show("请选中一个配置");
            }

        }

        private void button96_Click(object sender, EventArgs e)
        {
            SaveConfig(); ;
            //另存当前配置
            string checkedname = "";
            for (int i = 0; i < this.lsvConfig.Items.Count; i++)
            {
                if (this.lsvConfig.Items[i].Selected)
                {
                    checkedname = this.lsvConfig.Items[i].Text;
                    break;
                }
            }
            if (checkedname != "")
            {
                string cname = "";
                frmInput f = new frmInput();
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    cname = f.inputName;
                    for (int i = 0; i < this.lsvConfig.Items.Count; i++)
                    {
                        if (this.lsvConfig.Items[i].Text == cname)
                        {
                            MessageBox.Show("已经有一个配置的名字叫" + cname + "，请更换一个新名字！");
                            return;
                        }
                    }
                    if (AShelp.CopyConfigs(checkedname, cname))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = cname;
                        lvi.ImageIndex = 0;
                        this.lsvConfig.Items.Add(lvi);

                    }
                }
            }
            else
            {
                MessageBox.Show("请选中一个配置");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttishi_TextChanged(null, null);
        }

        private void button94_Click(object sender, EventArgs e)
        {
            SaveConfig();
            SaveConfigName();//lhc1
            string checkedname = "";
            for (int i = 0; i < this.lsvConfig.Items.Count; i++)
            {
                if (this.lsvConfig.Items[i].Selected)
                {
                    checkedname = this.lsvConfig.Items[i].Text;
                    this.lsvConfig.Items[i].ImageIndex = 1;
                }
                else
                {
                    this.lsvConfig.Items[i].ImageIndex = 0;

                }
            }
            if (checkedname != "")
            {
                Myinfo.configname = checkedname;
                lblconfig.Text = "当前配置是：" + checkedname;
                label65.Text = "当前配置是：" + checkedname;//lhc1
                //修改配置文件
                AShelp.SaveConfig(checkedname, "", "", "");
                #region 进度条
                //Form1_Load
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;

                //CheckUpdate为窗体显示过程中需要处理算法的方法
                worker.DoWork += new DoWorkEventHandler(CheckUpdate2);
                worker.RunWorkerAsync();

                //显示进度窗体
                frmProgress f = new frmProgress(worker);
                f.Text = "正在加载，请耐心等待...";
                f.ShowDialog(this);
                #endregion
                MessageBox.Show("加载成功！");
            }
        }
        //处理算法CheckUpdate，注意参数一致
        public void CheckUpdate2(object sender, DoWorkEventArgs e)
        {
            LoadConfig();
        }

        #region 保存configName lhc1
        private void SaveConfigName()
        {
            List<string> con = new List<string>();
            for (int i = 0; i < configlistbox.Items.Count; i++)
            {
                string s = configlistbox.Items[i].ToString();
                con.Add(s);
            }
            AShelp.SaveCategoryConfig(con, "configName");
        }
        #endregion
        private void button6_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show("保存成功！");
        }

        private void SaveConfig()
        {
            #region lhc1
            List<string> pz = new List<string>();
            if (radioButton27.Checked)
                pz.Add("1");
            else
                pz.Add("0");
            AShelp.SaveCategoryConfig(pz, "pz");
            SaveConfigName();
            #endregion
            List<string> dl = new List<string>();
            if (radioButton4.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(textBox10.Text);
            if (radioButton9.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(textBox11.Text);
            if (radioButton11.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(textBox12.Text);
            if (radioButton13.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(textBox13.Text);
            if (radioButton15.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(textBox14.Text);
            if (radioButton17.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(numericUpDown1.Value.ToString());//自动发布，时
            if (radioButton21.Checked)
                dl.Add("1");
            else
                dl.Add("0");
            dl.Add(numericUpDown2.Value.ToString());//自动发布，分
            dl.Add(label30.Text);
            dl.Add(richtxtparsum.Text);
            AShelp.SaveWZ(dl, "adddl");
            AShelp.SaveWZbak(dl, "adddl");
            //分两大块保存1.整体文章管理内容 2.文章的特殊内容（不同的程序设置不一样）
            List<string> savewz = new List<string>();
            savewz.Add(txtzhubl.Text);
            savewz.Add(txtbl1.Text);
            if (ckbbl1.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtbl2.Text);
            if (ckbbl2.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtbl3.Text);
            if (ckbbl3.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtrandpic.Text);
            savewz.Add(txtdl1.Text);
            if (rbtdl1r.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtdl1row.Text);
            savewz.Add(txtdl2.Text);
            if (rbtdl2r.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtdl2row.Text);
            savewz.Add(txtdl3.Text);
            if (rbtdl3r.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtdl3row.Text);
            savewz.Add(txtdl4.Text);
            if (rbtdl4r.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtdl4row.Text);

            savewz.Add(txtTitle.Text);
            if (ckbSCDL.Checked)
                savewz.Add("1");
            else
                savewz.Add("0");
            savewz.Add(txtkeyword1.Text);
            savewz.Add(txtkeyword2.Text);
            savewz.Add(txtkeyword3.Text);
            savewz.Add(txttishi.Text);
            AShelp.SaveWZ(savewz, "WZ");
            AShelp.SaveWZbak(savewz, "WZ");
            List<string> saveadd = new List<string>();
            saveadd.Add(textBox31.Text);
            if (ckbmygj.Checked)
                saveadd.Add("1");
            else
                saveadd.Add("0");
            if (checkBox1.Checked)
                saveadd.Add("1");
            else
                saveadd.Add("0");
            saveadd.Add(txtjgmgc.Text);
            AShelp.SaveWZ(saveadd, "add");
            AShelp.SaveWZbak(saveadd, "add");
            SaveConfigEasy();

            List<string> checkedhtml = new List<string>();
            for (int i = 0; i < ckbHtml.Items.Count; i++)
            {
                if (ckbHtml.GetItemChecked(i))
                {
                    checkedhtml.Add(ckbHtml.GetItemText(ckbHtml.Items[i]));
                }
            }
            AShelp.SaveWZ(checkedhtml, "ckdhtml");
            AShelp.SaveWZbak(checkedhtml, "ckdhtml");

            List<string> teshu = new List<string>();
            teshu.Add(txtSinterval.Text);
            teshu.Add(txtEinterval.Text);
            if (rbpubtbig.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            teshu.Add(txtPubnum.Text);
            if (ckbfb1.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            if (ckbfb5.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            if (ckbfb3.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            if (ckbfb4.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            if (ckblinkpre.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            if (ckbshut.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            if (radioButton1.Checked)
                teshu.Add("1");
            else
                teshu.Add("0");
            teshu.Add(txtmini.Text);
            if (checkBox3.Checked)//广告词法敏感词
                teshu.Add("1");
            else
                teshu.Add("0");
            teshu.Add(txtrandpic.Text);//图片占用位置
            teshu.Add(cbbone.Text.ToString());
            teshu.Add(comboBox3.Text.ToString());
            teshu.Add(txt_pinpai.Text);
            teshu.Add(txt_xinghao.Text);
            teshu.Add(txt_city.Text);
            teshu.Add(txt_ghzl.Text);
            teshu.Add(txt_qdl.Text);
            teshu.Add(txt_price.Text);
            teshu.Add(txt_unit.Text);
            teshu.Add(txtFindcode.Text);
            AShelp.SaveWZ(teshu, "ts");
            AShelp.SaveWZbak(teshu, "ts");
        }

        private void button61_Click(object sender, EventArgs e)
        {
            ckbfb1.Checked = true;
            ckbfb2.Checked = true;
            ckbfb3.Checked = true;
            ckbfb4.Checked = true;

        }

        private void button66_Click(object sender, EventArgs e)
        {
            AShelp.selectall(lsvdaifa);
        }

        private void button67_Click(object sender, EventArgs e)
        {
            AShelp.reversselect(lsvdaifa);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            AShelp.delselect(lsvdaifa);
            UpdateTabNum();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            lsvdaifa.Items.Clear();
            UpdateTabNum();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            string tag = "";
            for (int i = 0; i < lsvdaifa.Items.Count; i++)
            {
                tag = "";
                if (lsvdaifa.Items[i].Tag != null) tag = lsvdaifa.Items[i].Tag.ToString();
                data.Add(lsvdaifa.Items[i].Text + "【】" + tag);
            }
            string[] newdata = AShelp.RandomStrings(data.ToArray());

            lsvdaifa.Items.Clear();
            foreach (string s in newdata)
            {
                ListViewItem lsv = new ListViewItem();
                string[] stitles = s.Split(new string[] { "【】" }, StringSplitOptions.None);
                if (stitles.Length == 2)
                {
                    lsv.Text = stitles[0];
                    lsv.Tag = stitles[1];
                }
                else
                {
                    lsv.Text = s;
                    lsv.Tag = "";
                }
                lsv.SubItems.Add("等待发送");
                lsvdaifa.Items.Add(lsv);
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            //导出数据
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                List<string> list = AShelp.getselecttext(lsvdaifa);
                //string txt = string.Join("||", list.ToArray());
                string txt = string.Join("\r\n", list.ToArray());
                AShelp.SaveTXT(txt, file, Encoding.Default);
                MessageBox.Show("保存成功！");
            }
        }

        private void button79_Click(object sender, EventArgs e)
        {
            AShelp.selectall(lsvchenggong);
        }

        private void button78_Click(object sender, EventArgs e)
        {
            AShelp.reversselect(lsvchenggong);

        }

        private void button77_Click(object sender, EventArgs e)
        {
            AShelp.delselect(lsvchenggong);
            UpdateTabNum();
        }

        private void button76_Click(object sender, EventArgs e)
        {
            lsvchenggong.Items.Clear();
            UpdateTabNum();
        }

        private void button75_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lsvchenggong.Items.Count; i++)
            {
                if (lsvchenggong.Items[i].Checked)
                {
                    ListViewItem lsv = new ListViewItem();
                    lsv.Text = lsvchenggong.Items[i].Text;
                    lsv.SubItems.Add("来自成功列表");
                    lsvchenggong.Items.RemoveAt(i);
                    lsvdaifa.Items.Add(lsv);
                    i--;
                }
            }
            UpdateTabNum();
        }

        private void button86_Click(object sender, EventArgs e)
        {
            AShelp.selectall(lsvshibai);
        }

        private void button85_Click(object sender, EventArgs e)
        {
            AShelp.reversselect(lsvshibai);
        }

        private void button84_Click(object sender, EventArgs e)
        {
            AShelp.delselect(lsvshibai);
            UpdateTabNum();
        }

        private void button83_Click(object sender, EventArgs e)
        {
            lsvshibai.Items.Clear();
            UpdateTabNum();
        }

        private void button82_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lsvshibai.Items.Count; i++)
            {
                if (lsvshibai.Items[i].Checked)
                {
                    ListViewItem lsv = new ListViewItem();
                    lsv.Text = lsvshibai.Items[i].Text;
                    lsv.SubItems.Add("来自失败列表");
                    lsvshibai.Items.RemoveAt(i);
                    lsvdaifa.Items.Add(lsv);
                    i--;
                }
            }
            UpdateTabNum();

        }

        private void button72_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                string txt = AShelp.LoadTXT(file);
                string[] titles = Regex.Split(txt, "\r\n", RegexOptions.IgnoreCase);
                foreach (string stitle in titles)
                {
                    if (stitle.Trim() != "")
                    {
                        ListViewItem lvt = new ListViewItem();
                        lvt.Text = stitle;
                        lvt.SubItems.Add("等待发送");
                        lsvdaifa.Items.Add(lvt);
                    }
                }

                MessageBox.Show("导入成功！");
            }
        }
        //新的敏感词获取办法 测试
        private void getmgc1()
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\badword";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DirectoryInfo folder = new DirectoryInfo(path);

            foreach (FileInfo file in folder.GetFiles("*.bin"))
            {
                MessageBox.Show(file.Name);
            }
            string key = NetHelper.GetMD5(Myinfo.username + "fangyuan888");
            var d = new StringBuilder();
            d.AppendFormat("username={0}&", Myinfo.username);
            d.AppendFormat("key={0}", key);
            string html = NetHelper.HttpPost("http://vip.hsoow.com/index.php?m=member&c=index&a=badword", d.ToString());
        }

        //旧的敏感词获取办法 在用
        private string[] getmgc()
        {
            string gg = AShelp.LoadTXT(Application.StartupPath + "\\guanggaofa.bin");
            if (gg == "")
            {
                string url = "http://7xiex6.com1.z0.glb.clouddn.com/m%E6%95%8F%E6%84%9F%E8%AF%8D/guanggaofa.bin";
                myhttp.DownloadFile(url, Application.StartupPath + "\\guanggaofa.bin");
                gg = AShelp.LoadTXT(Application.StartupPath + "\\guanggaofa.bin");
            }
            string all = AShelp.LoadTXT(Application.StartupPath + "\\all.bin");
            if (all == "")
            {
                string url = "http://7xiex6.com1.z0.glb.clouddn.com/m%E6%95%8F%E6%84%9F%E8%AF%8D/all.bin";
                myhttp.DownloadFile(url, Application.StartupPath + "\\all.bin");
                all = AShelp.LoadTXT(Application.StartupPath + "\\all.bin");
            }
            if (gg.Length > 0 && !gg.EndsWith("\r\n"))
            {
                gg = gg + "\r\n";
            }
            string sentence = "";
            try
            {
                DataSet ds = SqlHelper.ExecuteDataSet("select feifaword from t_feifaword where id=4");
                DataTable dt = ds.Tables[0];
                sentence = dt.Rows[0]["feifaword"].ToString().Replace("\\r\\n", "\r\n");
            }
            catch (Exception ex)
            {
            }
            string allstr = "";
            if (checkBox3.Checked)
                allstr = sentence;
            else
                allstr = sentence + gg + all;
            string[] s = allstr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return s;
        }
        private string changemgc(string ss)
        {
            //string jg = "";
            //if (jg == "")
            //{
            //    jg = " ";
            //}
            foreach (string s in mgcs)
            {
                bool iszm = isExists(s);
                if (!iszm && s != "高效" && s != "膨胀" && s != "氢氧化钠" && s != "氧化钠")
                    ss = ss.Replace(s, "");
                //ss = ss.Replace(s, insertmgc(s, jg));
            }
            return ss;
        }
        public bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }
        private string insertmgc(string mgc, string jg)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < mgc.Length; i++)
            {
                s.Append(mgc[i] + jg);
            }
            return s.ToString();
        }
        private void btnPub_Click(object sender, EventArgs e)
        {
            if (myhttp.islogin == "OK")
            {
                if (radioButton26.Checked && configlistbox.Items.Count > 1)//lhc1
                    timer6.Start();
                else
                {
                    timer6.Stop();//lhc1
                    iswaiting = false;
                }
                ThreadStart start = null;

                if (start == null)
                {
                    start = pubtitle;
                }
                if (isstoppub)
                {
                    if (t != null)
                        t.Abort();
                    t = new Thread(start);
                    t.IsBackground = true;
                    t.Name = "aaaaa";
                    t.Start();
                    isstoppub = false;
                }
                else
                {
                    MessageBox.Show("正在发布，请停止后再开始！");

                }
            }
            else
            {
                MessageBox.Show("请登录后再操作！");
            }
        }
        private string getPicname(string http)
        {
            string a = "";
            a = CsharpHttpHelper.HttpHelper.GetBetweenHtml(http, "m/", "_") + ".jpg";
            return a;
        }

        private string getPicid(string http)
        {
            string a = "";
            if (http.Contains("="))
                a = http.Substring(http.LastIndexOf('=') + 1);
            return a;
        }
        private int errcount = 0;
        private bool isPubOver = false;
        private void pubtitle()
        {
            mgcs = getmgc();
            bool cfb1 = true, cfb2 = true, cfb3 = true, cfb4 = true;
            cfb1 = ckbfb1.Checked;
            cfb2 = ckbfb5.Checked;
            cfb3 = ckbfb3.Checked;
            cfb4 = ckbfb4.Checked;
            int stime = 30, etime = 40, sleeptime = 30;
            int nums = 0;
            bool islimited = false;
            if (rbpubtnum.Checked)
            {
                islimited = true;
                int.TryParse(txtPubnum.Text.Trim(), out nums);
            }
            string title = "";
            string content = "";
            string baidumsg = "";
            Random rnd = new Random();
            List<string> htmllist = new List<string>();
            for (int j = 0; j < this.ckbHtml.Items.Count; j++)
            {
                if (ckbHtml.GetItemChecked(j))
                {
                    htmllist.Add(ckbHtml.GetItemText(ckbHtml.Items[j]));
                }
            }
            //if (htmllist.Count < 5)
            //{
            //    timer6.Stop();//lhc1
            //    iswaiting = false;
            //    MessageBox.Show("勾选的文章内容模版不能低于5个！");
            //    tabControl1.TabPages[3].Select();
            //    isstoppub = true;
            //    ispausd = false;
            //    isstarttime = false;
            //    return;
            //}
            //if (this.dgvpracontent.RowCount < 50)
            //{
            //    timer6.Stop();//lhc1
            //    iswaiting = false;
            //    MessageBox.Show("段落库不能少于50个段落");
            //    this.isstoppub = true;
            //    this.ispausd = false;
            //    this.isstarttime = false;
            //    return;
            //}
            if (txtFindcode.Text.Trim() == "")
            {
                MessageBox.Show("查询码不能为空");
                return;
            }
            //声明参数
            string txtgytitle = "", txtgydesc = "", cbid = "";
            string thumb1 = "", thumb2 = "", thumb = "";
            string sKeyword1 = "", sKeyword2 = "", sKeyword3 = "";
            int.TryParse(txtSinterval.Text, out stime);
            int.TryParse(txtEinterval.Text, out etime);
            //if (stime < 60)
            //{
            //    timer6.Stop();//lhc1
            //    iswaiting = false;
            //    MessageBox.Show("时间间隔最低60秒！");
            //    isstoppub = true;
            //    ispausd = false;
            //    isstarttime = false;
            //    return;
            //}
            if (etime < stime || etime == stime)
            {
                timer6.Stop();//lhc1
                iswaiting = false;
                MessageBox.Show("结束时间必须大于起始时间！");
                isstoppub = true;
                ispausd = false;
                isstarttime = false;
                return;
            }
            string[] pics = AShelp.delspaceStrings(txtrandpic.Lines);
            if (pics.Length < 1)
            {
                timer6.Stop();//lhc1
                iswaiting = false;
                MessageBox.Show("没有图片可用！");
                isstoppub = true;
                ispausd = false;
                isstarttime = false;
                return;
            }
            string prehtml = "";
            int havecont = 0;
            havecont = lsvdaifa.Items.Count;
            List<string> slist = new List<string>();
            slist.Add("http://bid.10huan.com/hyzx/handler/ModelHandler.ashx?action=moduleHtml");
            slist.Add("http://www.16fafa.cn/hyfl/handler/ModelHandler.ashx?action=moduleHtml");
            int q = -1;
            int w = slist.Count;
            if (w < 1)
            { MessageBox.Show("您没有权限发布信息"); return; }
            if (havecont > 0 && iswaiting)
                Thread.Sleep(stime + rnd.Next(etime - stime) * 1000);
            foreach (System.Windows.Forms.ListViewItem lvi in lsvdaifa.Items)  //选中项遍历
            {
                //检测段落库数量
                //if (this.dgvpracontent.RowCount < 5)
                //{
                //    timer6.Stop();//lhc1
                //    iswaiting = false;
                //    MessageBox.Show("段落库段落低于5个,严重影响文章质量，请添加段落");
                //    this.isstoppub = true;
                //    this.ispausd = false;
                //    this.isstarttime = false;
                //    txttishi.Text += "文章发布停止！\r\n";
                //    return;
                //}
                q++;
                //havecont = 0;
                //havecont = lsvdaifa.Items.Count;
                if (lvi.SubItems[1].Text.Contains("等待时间还有"))
                {
                    isstarttime = false;
                }
                lvi.SubItems[1].Text = "正在发布";
                lsvdaifa.Refresh();
                if (!isstoppub)//停止
                {
                    if (islimited)//数量限制
                    {
                        if (nums > 0)
                        {
                            nums--;
                        }
                        else
                        {
                            txttishi.Text += "设定的信息条数已发完！";
                            isstoppub = true;
                            ispausd = false;
                            isstarttime = false;
                            timer6.Stop();//lhc1
                            iswaiting = false;
                            Thread.CurrentThread.Abort();
                        }
                    }
                    try
                    {
                        //公共信息
                        title = lvi.Text.Replace("&", "%26");
                        txttishi.Text += "当前正在发布的文章为:" + title + "。\r\n";
                        txtgytitle = title.Trim();//信息标题
                        sKeyword1 = ReplaceKeywords(txtkeyword1.Text, title);
                        sKeyword2 = ReplaceKeywords(txtkeyword2.Text, title);
                        sKeyword3 = ReplaceKeywords(txtkeyword3.Text, title);
                        if (sKeyword1 == sKeyword2 && sKeyword2 == sKeyword3 && sKeyword3 == "")
                        {
                            if (txtgytitle.Length > 8)
                                sKeyword1 = txtgytitle.Substring(0, 8);
                            else
                                sKeyword1 = txtgytitle;
                        }
                        if (sKeyword1.Length > 8) sKeyword1 = sKeyword1.Substring(0, 8);
                        if (sKeyword2.Length > 8) sKeyword2 = sKeyword2.Substring(0, 8);
                        if (sKeyword3.Length > 8) sKeyword3 = sKeyword3.Substring(0, 8);
                        content = AShelp.LoadHtml(htmllist[rnd.Next(htmllist.Count)]);
                        content = Regex.Replace(content, "(?i)<IMG.*>", "");//过滤用户插入的本地图片
                        //添加一些句子（句子库）
                        //content = InsertHTMLRows(content);

                        if (lvi.Tag != null)
                            content = ReplaceHTMLWZ(content, title, lvi.Tag.ToString());
                        else
                            content = ReplaceHTMLWZ(content, title, "");
                        //是否链接上一篇文章
                        if (prehtml != "")
                            txtgydesc = content + prehtml;
                        else
                            txtgydesc = content;
                        if (txtrandpic.Lines.Length > 0)
                        {
                            thumb = pics[rnd.Next(pics.Length)];
                            thumb1 = pics[rnd.Next(pics.Length)];
                            thumb2 = pics[rnd.Next(pics.Length)];
                        }

                        //if (cfb1 && cbbone.SelectedValue != null)

                        //if (cfb2 && comboBox3.SelectedValue != null)

                        cbid = new Random().Next(23).ToString();
                        #region 敏感词过滤
                        //手机号码
                        txtgytitle = Regex.Replace(txtgytitle, "0?(13|14|15|16|17|18)[0-9]{9}", " ");
                        //电话号码
                        txtgytitle = Regex.Replace(txtgytitle, "[0-9-()（）]{7,18}", " ");
                        //过滤a标签
                        txtgydesc = Regex.Replace(txtgydesc, @"(?is)<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>", " ");

                        //url
                        //txtgydesc = Regex.Replace(txtgydesc, @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$", " ");
                        //txtgydesc = Regex.Replace(txtgydesc, @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?", " ");
                        //txtgydesc = Regex.Replace(txtgydesc, @"(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?", " ");
                        //敏感词过滤
                        txtgytitle = changemgc(txtgytitle);
                        txtgydesc = changemgc(txtgydesc);
                        sKeyword1 = changemgc(sKeyword1);
                        #endregion
                        string key = GetMD5(txtName.Text.Trim() + "100dh888");
                        //var obj = new
                        //{
                        //    catid = cbid,//栏目id，新闻相当于一个栏目
                        //    title = txtgytitle,//标题
                        //    pinpai = txt_pinpai.Text,//品牌
                        //    xinghao = txt_xinghao.Text,//产品型号
                        //    city = txt_city.Text,//发货城市
                        //    gonghuo = txt_ghzl.Text,//供货总量
                        //    qiding = txt_qdl.Text,//起订量
                        //    price = txt_price.Text,//单价
                        //    unit = txt_unit.Text,//计量单位
                        //    content = HttpUtility.UrlEncode("<p>" + txtgydesc + "</p>" + txtFindcode.Text.Trim(),Encoding.UTF8),//内容,UrlEncode编码
                        //    keywords = sKeyword1,//关键词
                        //    style_color = "",
                        //    style_font_weight = "",
                        //    page_title_value = "",
                        //    add_introduce = 1,
                        //    introcude_length = 200,
                        //    auto_thumb = 1,
                        //    auto_thumb_no = 1,
                        //    thumb,//标题图片
                        //    forward = "",
                        //    id = "",
                        //    username = Myinfo.username,
                        //    key,
                        //    dosubmit = "提交",
                        //    version = "1.0.0.0"
                        //};
                        //string postDataStr = JsonConvert.SerializeObject(obj);

                        StringBuilder strpost = new StringBuilder();
                        strpost.AppendFormat("catid={0}&", cbid);
                        strpost.AppendFormat("title={0}&", txtgytitle);
                        strpost.AppendFormat("pinpai={0}&", txt_pinpai.Text);
                        strpost.AppendFormat("xinghao={0}&", txt_xinghao.Text);
                        strpost.AppendFormat("city={0}&", txt_city.Text);
                        strpost.AppendFormat("gonghuo={0}&", txt_ghzl.Text);
                        strpost.AppendFormat("qiding={0}&", txt_qdl.Text);
                        strpost.AppendFormat("price={0}&", txt_price.Text);
                        strpost.AppendFormat("unit={0}&", txt_unit.Text);
                        string desc = txtgydesc + "<p>" + txtFindcode.Text.Trim() + "</p>";//内容,UrlEncode编码 
                        strpost.AppendFormat("content={0}&", desc);
                        strpost.AppendFormat("keywords={0}&", sKeyword1);
                        strpost.AppendFormat("style_color={0}&", "");
                        strpost.AppendFormat("style_font_weight={0}&", "");
                        strpost.AppendFormat("page_title_value={0}&", "");
                        strpost.AppendFormat("add_introduce={0}&", 1);
                        strpost.AppendFormat("introcude_length={0}&", 200);
                        strpost.AppendFormat("auto_thumb={0}&", 1);
                        strpost.AppendFormat("auto_thumb_no={0}&", 1);
                        strpost.AppendFormat("thumb={0}&", thumb);
                        strpost.AppendFormat("forward={0}&", "");
                        strpost.AppendFormat("id={0}&", "");
                        strpost.AppendFormat("username={0}&", Myinfo.username);
                        strpost.AppendFormat("key={0}&", key);
                        strpost.AppendFormat("dosubmit={0}&", "提交");
                        strpost.AppendFormat("version={0}&", "1.0.0.0");

                        #region 组织发布内容
                        for (int i = 0; i < slist.Count; i++)
                        {
                            //string host = Myinfo.rjlist[i].realmAddress;
                            if (q % w == i)
                            {
                                //地址根据不同网站变化，每个地址需要写一个接口
                                //string html = NetHelper.Post("http://39.105.196.3:4399/toolWS.asmx/Post", obj.ToString());
                                string html = NetHelper.HttpPost(slist[i], strpost.ToString());
                                JObject joo = (JObject)JsonConvert.DeserializeObject(html);
                                string code = joo["code"].ToString();
                                string msg = joo["msg"].ToString();
                                if (code == "1")//发布成功。
                                {
                                    titleurl = joo["detail"]["url"].ToString();
                                    txttishi.Text += "标题:" + title + "发布成功。\r\n";
                                    lvi.SubItems[0].Text = title;
                                    lvi.SubItems[1].Text = titleurl;
                                    lsvdaifa.Items.Remove(lvi); // 按索引移除                                     
                                    lsvchenggong.Items.Add(lvi.Clone() as ListViewItem); cgnum++;
                                    UpdateTabNum();
                                    txttishi.Text += "链接:" + titleurl + "\r\n";
                                    //随机等待0秒
                                    //ping baidu                                    
                                    baidumsg = myhttp.postToPing(titleurl);
                                    txttishi.Text += "链接ping给百度完毕\r\n";
                                    //webBrowser4.Navigate(titleurl);
                                    //等待秒数
                                    if (havecont > 1)
                                    {
                                        sleeptime = stime + rnd.Next(etime - stime);
                                        txttishi.Text += "随机等待" + sleeptime + "秒。\r\n";
                                        waitsecond = sleeptime;
                                        isstarttime = true;
                                        Thread.Sleep(sleeptime * 1000);
                                    }
                                    else
                                    {
                                        txttishi.Text += "最后一条数据发送完成！\r\n";
                                        havecont = 0;
                                    }
                                    continue;//再发送本栏目信息
                                }
                                else if (code == "0")
                                {
                                    if (msg.Contains("今日投稿已超过限制数"))
                                    {
                                        txttishi.Text += "提示信息：本栏发布数量已完成！\r\n";
                                        isstoppub = true;
                                        return;
                                    }
                                    if (msg.ToString().Contains("信息发布过快，请隔60秒再提交！"))
                                    {
                                        txttishi.Text += "出错:信息发布过快，请隔60秒再提交！\r\n";
                                        //lvi.SubItems[1].Text = "信息发布过快，请隔60秒再提交！";
                                        lvi.SubItems[1].Text = "等待发送";
                                        lsvdaifa.Items.Remove(lvi); // 按索引移除 
                                        lsvdaifa.Items.Add(lvi.Clone() as ListViewItem); //失败标题重新加入代发列表
                                                                                         //lsvshibai.Items.Add(lvi.Clone() as ListViewItem); 
                                        sbnum++;
                                        UpdateTabNum();
                                        sleeptime = stime + rnd.Next(etime - stime);
                                        txttishi.Text += "随机等待" + sleeptime + "秒。\r\n";
                                        waitsecond = sleeptime;
                                        isstarttime = true;
                                        Thread.Sleep(sleeptime * 1000);
                                        continue;
                                    }
                                    else
                                    {
                                        txttishi.Text += "出错:" + msg + "\r\n";
                                        //lvi.SubItems[1].Text = msg;
                                        lvi.SubItems[1].Text = "等待发送";
                                        lsvdaifa.Items.Remove(lvi); // 按索引移除 
                                        lsvdaifa.Items.Add(lvi.Clone() as ListViewItem); //失败标题重新加入代发列表
                                                                                         //lsvshibai.Items.Add(lvi.Clone() as ListViewItem); sbnum++;
                                        UpdateTabNum();
                                        sleeptime = stime + rnd.Next(etime - stime);
                                        txttishi.Text += "随机等待" + sleeptime + "秒。\r\n";
                                        waitsecond = sleeptime;
                                        isstarttime = true;
                                        Thread.Sleep(sleeptime * 1000);
                                        continue;
                                    }
                                }
                                else if (code == "2")//停
                                {
                                    txttishi.Text += msg + "\r\n";
                                    isstoppub = true;
                                    return;
                                }
                                else if (html.Contains("无法解析此远程名称") || html.Contains("无法连接到远程服务器") || html.Contains("remote name could not be resolved"))
                                {
                                    //lvi.SubItems[1].Text = "网络无法连接！";
                                    lvi.SubItems[1].Text = "等待发送";
                                    lsvdaifa.Items.Remove(lvi); // 按索引移除 
                                    lsvdaifa.Items.Add(lvi.Clone() as ListViewItem); //失败标题重新加入代发列表
                                                                                     //lsvshibai.Items.Add(lvi.Clone() as ListViewItem); 
                                    sbnum++;
                                    txttishi.Text += "网络无法连接！\r\n";
                                    sleeptime = stime + rnd.Next(etime - stime);
                                    txttishi.Text += "随机等待" + sleeptime + "秒。\r\n";
                                    waitsecond = sleeptime;
                                    isstarttime = true;
                                    Thread.Sleep(sleeptime * 1000);
                                    continue;//再发送本栏目信息
                                }
                                else
                                {
                                    txttishi.Text += "出错:情况未知，请查看日志！\r\n";
                                    AShelp.SaveTXT(html, Application.StartupPath + "\\错误记录\\" + title + ".txt");
                                    //lvi.SubItems[1].Text = Application.StartupPath + "\\错误记录\\" + title + ".txt";
                                    lvi.SubItems[1].Text = "等待发送";
                                    lsvdaifa.Items.Remove(lvi); // 按索引移除 
                                    lsvdaifa.Items.Add(lvi.Clone() as ListViewItem); //失败标题重新加入代发列表
                                                                                     //lsvshibai.Items.Add(lvi.Clone() as ListViewItem); 
                                    sbnum++;
                                    UpdateTabNum();
                                    sleeptime = stime + rnd.Next(etime - stime);
                                    txttishi.Text += "随机等待" + sleeptime + "秒。\r\n";
                                    waitsecond = sleeptime;
                                    isstarttime = true;
                                    Thread.Sleep(sleeptime * 1000);
                                    continue;
                                }
                                //    }
                                //}
                                //}
                                //catch (Exception ex)
                                //{
                                //    continue;
                                //}
                                #endregion
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!(ex.Message.Contains("正在中止线程") || ex.Message.Contains("aborted")))
                        {
                            errcount++;
                            //lvi.SubItems[1].Text = ex.Message;
                            lvi.SubItems[1].Text = "等待发送";
                            lsvdaifa.Items.Remove(lvi); // 按索引移除 
                            lsvdaifa.Items.Add(lvi.Clone() as ListViewItem); //失败标题重新加入代发列表
                            //lsvshibai.Items.Add(lvi.Clone() as ListViewItem); sbnum++;
                            txttishi.Text += ex.Message + "\r\n";
                            sleeptime = stime + rnd.Next(etime - stime);
                            txttishi.Text += "随机等待" + sleeptime + "秒。\r\n";
                            waitsecond = sleeptime;
                            isstarttime = true;
                            Thread.Sleep(sleeptime * 1000);
                        }
                    }
                }
                else
                {
                    if (ispausd)
                    {
                        if (!txttishi.Text.EndsWith("文章发布暂停。\r\n"))
                            txttishi.Text += "文章发布暂停。\r\n";
                    }
                    else
                    {
                        if (!txttishi.Text.EndsWith("文章发布停止。\r\n"))
                            txttishi.Text += "文章发布停止。\r\n";
                    }
                    t = null;
                }
                havecont = 0;
                havecont = lsvdaifa.Items.Count;
                label45.Text = string.Format("共有{0}个信息待发", havecont);
            }
            txttishi.Text += "文章发布完。\r\n";
            havecont = 0;
            isstoppub = true;
            ispausd = false;
            isstarttime = false;
            if (radioButton26.Checked && havecont < 1 && configlistbox.Items.Count > 1)
            {
                configlistbox.Items.Remove(Myinfo.configname);
                configlistbox.Refresh();
                isPubOver = true;
            }
            if (ckbshut.Checked)
            {
                if (radioButton1.Checked)
                {
                    SaveConfig();
                    DoExitWin(1);
                }
            }
        }

        public static string GetMD5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        private void UpdateTabNum()
        {
            if (lsvchenggong.Items.Count > 1000)
            {
                int n = lsvchenggong.Items.Count - 1000;
                for (int i = 0; i < n; i++)
                    lsvchenggong.Items.RemoveAt(0);
            }
            label45.Text = string.Format("共有{0}个信息待发，本次发布成功{1}条，失败{2}条。", lsvdaifa.Items.Count, cgnum, 0);
            label45.Refresh();
            tabControl5.TabPages[0].Text = string.Format("待发列表({0})", lsvdaifa.Items.Count);
            tabControl5.TabPages[1].Text = string.Format("成功列表({0})", lsvchenggong.Items.Count);
            tabControl5.TabPages[2].Text = string.Format("失败列表({0})", lsvshibai.Items.Count);
            tabControl5.Refresh();
        }

        public string ReplaceKeywords(string wz, string title)
        {
            if (wz.Trim() != "")
            {
                Regex r;
                Random rnd = new Random();
                string[] txt;
                string mybl = "";
                if (txtTitle.Text.Contains("【主变量】"))
                {
                    foreach (string z in txtzhubl.Lines)
                    {
                        if (title.Contains(z))
                        {
                            mybl = z;
                            break;
                        }
                    }
                    wz = wz.Replace("【主变量】", mybl);
                }
                else
                {
                    while (wz.Contains("【主变量】"))
                    {
                        r = new Regex("【主变量】");
                        txt = AShelp.delspaceStrings(txtzhubl.Lines);
                        if (txt.Length > 0)
                        {
                            string t = txt[rnd.Next(txt.Length)];
                            wz = r.Replace(wz, t, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (txtTitle.Text.Contains("【变量1】"))
                {
                    mybl = "";
                    foreach (string z in txtbl1.Lines)
                    {
                        if (title.Contains(z))
                        {
                            mybl = z;
                            break;
                        }
                    }
                    wz = wz.Replace("【变量1】", mybl);
                }
                else
                {
                    while (wz.Contains("【变量1】"))
                    {
                        r = new Regex("【变量1】");
                        txt = AShelp.delspaceStrings(txtbl1.Lines);
                        if (txt.Length > 0)
                        {
                            string t = txt[rnd.Next(txt.Length)];
                            wz = r.Replace(wz, t, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (txtTitle.Text.Contains("【变量2】"))
                {
                    mybl = "";
                    foreach (string z in txtbl2.Lines)
                    {
                        if (title.Contains(z))
                        {
                            mybl = z;
                            break;
                        }
                    }
                    wz = wz.Replace("【变量2】", mybl);
                }
                else
                {
                    while (wz.Contains("【变量2】"))
                    {
                        r = new Regex("【变量2】");
                        txt = AShelp.delspaceStrings(txtbl2.Lines);
                        if (txt.Length > 0)
                        {
                            string t = txt[rnd.Next(txt.Length)];
                            wz = r.Replace(wz, t, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (txtTitle.Text.Contains("【变量3】"))
                {
                    mybl = "";
                    foreach (string z in txtbl3.Lines)
                    {
                        if (title.Contains(z))
                        {
                            mybl = z;
                            break;
                        }
                    }
                    wz = wz.Replace("【变量3】", mybl);
                }
                else
                {
                    while (wz.Contains("【变量3】"))
                    {
                        r = new Regex("【变量3】");
                        txt = AShelp.delspaceStrings(txtbl3.Lines);
                        if (txt.Length > 0)
                        {
                            string t = txt[rnd.Next(txt.Length)];
                            wz = r.Replace(wz, t, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return wz;

        }
        public string ReplaceHTMLWZ(string wz, string title, string s)
        {
            Regex r;
            Random rnd = new Random();
            string[] txt;
            string mybl = "";

            if (txtTitle.Text.Contains("【主变量】"))
            {
                if (s.Trim() != "")
                {
                    mybl = s;
                }
                else
                {
                    foreach (string z in txtzhubl.Lines)
                    {
                        if (title.Contains(z))
                        {
                            mybl = z;
                            break;
                        }
                    }
                }
                wz = wz.Replace("【主变量】", mybl);
            }
            else
            {
                while (wz.Contains("【主变量】"))
                {
                    r = new Regex("【主变量】");
                    txt = AShelp.delspaceStrings(txtzhubl.Lines);
                    if (txt.Length > 0)
                    {
                        string t = txt[rnd.Next(txt.Length)];
                        wz = r.Replace(wz, t, 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (txtTitle.Text.Contains("【变量1】"))
            {
                mybl = "";
                foreach (string z in txtbl1.Lines)
                {
                    if (title.Contains(z))
                    {
                        mybl = z;
                        break;
                    }
                }
                wz = wz.Replace("【变量1】", mybl);
            }
            else
            {
                while (wz.Contains("【变量1】"))
                {
                    r = new Regex("【变量1】");
                    txt = AShelp.delspaceStrings(txtbl1.Lines);
                    if (txt.Length > 0)
                    {
                        string t = txt[rnd.Next(txt.Length)];
                        wz = r.Replace(wz, t, 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (txtTitle.Text.Contains("【变量2】"))
            {
                mybl = "";
                foreach (string z in txtbl2.Lines)
                {
                    if (title.Contains(z))
                    {
                        mybl = z;
                        break;
                    }
                }
                wz = wz.Replace("【变量2】", mybl);
            }
            else
            {
                while (wz.Contains("【变量2】"))
                {
                    r = new Regex("【变量2】");
                    txt = AShelp.delspaceStrings(txtbl2.Lines);
                    if (txt.Length > 0)
                    {
                        string t = txt[rnd.Next(txt.Length)];
                        wz = r.Replace(wz, t, 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            int rdindex = 0, indext = 0;
            txt = AShelp.delspaceStrings(txtrandpic.Lines);
            txt = AShelp.RandomStrings(txt);
            while (wz.Contains("【随机图片】"))
            {
                r = new Regex("【随机图片】");

                if (txt.Length > 0)
                {
                    indext = rdindex % txt.Length;
                    string t = txt[indext];
                    wz = r.Replace(wz, "<p><img src=\"" + t + "\" alt='【标题】' width='600' height='400' /></p>", 1);
                }
                else
                {
                    break;
                }
                rdindex++;
            }
            if (wz.Contains("【标题】"))
            {
                wz = wz.Replace("【标题】", title);
            }
            while (wz.Contains("【段落】"))
            {
                Regex regex = new Regex("【段落】");
                if (this.dgvpracontent.RowCount <= 0)
                {
                    break;
                }
                try
                {
                    DataGridViewRow dataGridViewRow = this.dgvpracontent.Rows[rnd.Next(this.dgvpracontent.RowCount)];
                    string str = dataGridViewRow.Cells[0].Value.ToString();
                    int num3 = this.achelp.ExcuteSql("update paragraph set UsedCount=UsedCount+1 where ID=" + str);
                    string text2 = dataGridViewRow.Cells[2].Value.ToString();
                    wz = regex.Replace(wz, "<p>" + text2.Replace("\u3000\u3000", "") + "</p>", 1);
                }
                catch (Exception ex)
                {
                }
            }
            while (wz.Contains("【年】"))
            {
                r = new Regex("【年】");
                string t = DateTime.Now.Year.ToString();
                wz = r.Replace(wz, t, 1);
            }
            while (wz.Contains("【月】"))
            {
                r = new Regex("【月】");
                string t = DateTime.Now.Month.ToString();
                wz = r.Replace(wz, t, 1);
            }
            while (wz.Contains("【日】"))
            {
                r = new Regex("【日】");
                string t = DateTime.Now.Day.ToString();
                wz = r.Replace(wz, t, 1);
            }
            wz = wz.Replace("&", "%26");
            return wz;

        }
        private void txtrandpic_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtrandpic.Lines.Length > 0)
            {
                string path = "";
                ispics = false;
                int index = txtrandpic.GetFirstCharIndexOfCurrentLine();//得到当前行第一个字符的索引
                int line = txtrandpic.GetLineFromCharIndex(index);//得到当前行的行号,从0开始，习惯是从1开始，所以+1.
                path = txtrandpic.Lines[line];
                if (path != "" && path.StartsWith("http://"))
                {
                    lblpicpath.Text = string.Format("{0}", path);
                    pictureBox1.ImageLocation = path;
                }
                else
                {
                    lblpicpath.Text = string.Format("{0}", "");
                    pictureBox1.ImageLocation = "";

                }
            }


        }

        private void txtPics_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPics.Lines.Length > 0)
            {
                string path = "";
                ispics = true;
                int index = txtPics.GetFirstCharIndexOfCurrentLine();//得到当前行第一个字符的索引
                int line = txtPics.GetLineFromCharIndex(index);//得到当前行的行号,从0开始，习惯是从1开始，所以+1.
                path = txtPics.Lines[line];
                if (path != "" && path.StartsWith("http://"))
                {
                    lblpicpath.Text = string.Format("{0}", path);
                    pictureBox1.ImageLocation = path;
                }
                else
                {
                    lblpicpath.Text = string.Format("{0}", "");
                    pictureBox1.ImageLocation = "";

                }
            }


        }

        private void button100_Click(object sender, EventArgs e)
        {
            frmgetpic f = new frmgetpic(this);
            f.TopMost = true;
            f.ShowDialog();
        }


        private void button101_Click(object sender, EventArgs e)
        {
            if (ispics)
            {
                string[] s = txtPics.Lines;
                txtPics.Text = "";
                List<string> list = new List<string>();
                foreach (string t in s)
                {
                    if (t != lblpicpath.Text && t != "")
                    {
                        list.Add(t);
                    }
                }
                txtPics.Lines = list.ToArray();
            }
            else
            {
                string[] s = txtrandpic.Lines;
                txtrandpic.Text = "";
                List<string> list = new List<string>();
                foreach (string t in s)
                {
                    if (t != lblpicpath.Text && t != "")
                    {
                        list.Add(t);
                    }
                }
                txtrandpic.Lines = list.ToArray();

            }

        }

        private void btnimgclear_Click(object sender, EventArgs e)
        {
            txtPics.Text = "";
        }

        private void txtdl4_TextChanged(object sender, EventArgs e)
        {
            label43.Text = string.Format("段落4，共有{0}行", txtdl4.Lines.Length);
        }

        private void txtdl3_TextChanged(object sender, EventArgs e)
        {
            label40.Text = string.Format("段落3，共有{0}行", txtdl3.Lines.Length);
        }

        private void txtdl2_TextChanged(object sender, EventArgs e)
        {
            label37.Text = string.Format("段落2，共有{0}行", txtdl2.Lines.Length);
        }

        private void txtdl1_TextChanged(object sender, EventArgs e)
        {
            label32.Text = string.Format("段落1，共有{0}行", txtdl1.Lines.Length);
        }

        private void txttishi_TextChanged(object sender, EventArgs e)
        {
            if (txttishi.Lines.Length > 50)
            {
                txttishi.Text = "";
            }
            this.txttishi.Select(this.txttishi.TextLength, 0);//光标定位到文本最后
            this.txttishi.ScrollToCaret();//滚动到光标处
        }

        private void button62_Click(object sender, EventArgs e)
        {
            timer6.Stop();
            isstoppub = true;
            ispausd = true;
            isstarttime = false;
            if (!txttishi.Text.EndsWith("文章发布暂停。\r\n"))
                txttishi.Text += "文章发布暂停。\r\n";
            if (t != null)
                t.Abort();


        }

        private void button63_Click(object sender, EventArgs e)
        {
            timer6.Stop();
            isstoppub = true;
            ispausd = false;
            isstarttime = false;
            if (!txttishi.Text.EndsWith("文章发布停止。\r\n"))
                txttishi.Text += "文章发布停止。\r\n";
            if (t != null)
                t.Abort();
        }

        private void button99_Click(object sender, EventArgs e)
        {
            if (myhttp.islogin == "OK")
            {
                frmRefresh f = new frmRefresh();
                f.Show();
            }
            else
            {
                MessageBox.Show("请登录后再操作！");
            }
        }

        private void lsvchenggong_DoubleClick(object sender, EventArgs e)
        {

        }

        private void lsvchenggong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = lsvchenggong.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                var videoitem = info.Item as ListViewItem;
                if (videoitem != null)
                {
                    string url = videoitem.SubItems[1].Text;
                    System.Diagnostics.Process.Start(url);
                }

            }
        }

        private void button73_Click(object sender, EventArgs e)
        {
            string url = "http://7xiex6.com1.z0.glb.clouddn.com/m%E6%95%8F%E6%84%9F%E8%AF%8D/guanggaofa.bin";
            myhttp.DownloadFile(url, Application.StartupPath + "\\guanggaofa.bin");
            MessageBox.Show("获取完成！");
        }

        private void button80_Click(object sender, EventArgs e)
        {
            string url = "http://7xiex6.com1.z0.glb.clouddn.com/m%E6%95%8F%E6%84%9F%E8%AF%8D/all.bin";
            myhttp.DownloadFile(url, Application.StartupPath + "\\all.bin");
            MessageBox.Show("获取完成！");
        }

        private void button65_Click(object sender, EventArgs e)
        {
            string gg = AShelp.LoadTXT(Application.StartupPath + "\\guanggaofa.bin");
            string all = AShelp.LoadTXT(Application.StartupPath + "\\all.bin");
            if (gg.Length > 0 && !gg.EndsWith("\r\n"))
            {
                gg = gg + "\r\n";
            }
            if (gg + all != "")
                textBox32.Text = gg + all;
            else
            {
                MessageBox.Show("还没有数据！");
            }
        }

        private void button87_Click(object sender, EventArgs e)
        {
            string gg = AShelp.LoadTXT(Application.StartupPath + "\\guanggaofa.bin");
            string all = AShelp.LoadTXT(Application.StartupPath + "\\all.bin");
            if (gg.Length > 0 && !gg.EndsWith("\r\n"))
            {
                gg = gg + "\r\n";
            }
            string allstr = gg + all;
            if (textBox31.Text.Trim() != "")
            {
                if (allstr.Length > 0 && !allstr.EndsWith("\r\n"))
                {
                    allstr = allstr + "\r\n";
                }
                allstr = allstr + textBox31.Text.Trim();

            }
            string[] s = allstr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string v = "";
            if (checkBox9.Checked)
            {
                v = txtzhubl.Text;
                if (v.Length > 0)
                    foreach (string si in s)
                    {
                        if (si.Length > 0)
                            v = v.Replace(si, "");
                    }
                txtzhubl.Text = v;
                textBox32.Text += "主变量替换完成！\r\n";
            }
            if (checkBox10.Checked)
            {
                v = txtbl1.Text;
                if (v.Length > 0)
                    foreach (string si in s)
                    {
                        if (si.Length > 0)
                            v = v.Replace(si, "");
                    }
                txtbl1.Text = v;
                textBox32.Text += "变量1替换完成！\r\n";
            }
            if (checkBox11.Checked)
            {
                v = txtbl2.Text;
                if (v.Length > 0)
                    foreach (string si in s)
                    {
                        if (si.Length > 0)
                            v = v.Replace(si, "");
                    }
                txtbl2.Text = v;
                textBox32.Text += "变量2替换完成！\r\n";
            }
            if (checkBox12.Checked)
            {
                v = txtbl3.Text;
                if (v.Length > 0)
                    foreach (string si in s)
                    {
                        if (si.Length > 0)
                            v = v.Replace(si, "");
                    }
                txtbl3.Text = v;
                textBox32.Text += "变量3替换完成！\r\n";
            }
            if (checkBox14.Checked)
            {
                //内容模版
                string temp = "";
                for (int i = 0; i < ckbHtml.Items.Count; i++)
                {
                    temp = ckbHtml.GetItemText(ckbHtml.Items[i]);
                    txtmbname.Text = temp;
                    string html = AShelp.LoadHtml(temp);
                    if (html.Length > 0)
                    {
                        foreach (string si in s)
                        {
                            if (si.Length > 0)
                                html = html.Replace(si, "");
                        }
                        AShelp.SaveHtml(html, temp);
                    }

                }
                string htmle = richTextBox1.Text;
                if (htmle != null && htmle.Length > 0)
                {
                    foreach (string si in s)
                    {
                        if (si.Length > 0)
                            htmle = htmle.Replace(si, "");
                    }
                    richTextBox1.Text = htmle; ;
                }
                textBox32.Text += "内容模版替换完成！\r\n";
            }
            //MessageBox.Show("替换完成！");
        }

        private void ckbHtml_Click(object sender, EventArgs e)
        {
            string temp = "";
            int selectCount = ckbHtml.SelectedItems.Count; //SelectedItems.Count就是：取得值，表示SelectedItems集合的物件数目。 
            if (selectCount > 0)//若selectCount大於0，说明用户有选中某列。
            {

                temp = ckbHtml.GetItemText(ckbHtml.SelectedItems[0]);
                txtmbname.Text = temp;
                string html = AShelp.LoadHtml(temp);
                richTextBox1.Text = html;
                return;
            }
        }

        private void button88_Click(object sender, EventArgs e)
        {
            frmrepeat f = new frmrepeat();
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button89_Click(object sender, EventArgs e)
        {
            frmrandom f = new frmrandom();
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button90_Click(object sender, EventArgs e)
        {
            frmreplace f = new frmreplace();
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button93_Click(object sender, EventArgs e)
        {
            frmwordtopic f = new frmwordtopic();
            f.TopMost = true;
            f.ShowDialog();
        }

        private void button92_Click(object sender, EventArgs e)
        {
            frmpiczip f = new frmpiczip();
            f.TopMost = true;
            f.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isstarttime)
                if (lsvdaifa.Items.Count > 0)
                {
                    lsvdaifa.Items[0].SubItems[1].Text = string.Format("等待时间还有{0}秒", waitsecond);
                    waitsecond--;
                    if (waitsecond < 0)
                    {
                        isstarttime = false;
                    }
                }
                else
                {
                    isstarttime = false;
                }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now > shutstart)
            {
                DoExitWin(1);
            }
        }

        private void ckbshut_Click(object sender, EventArgs e)
        {
            if (ckbshut.Checked)
            {
                if (radioButton2.Checked)
                {
                    int t = 0;
                    int.TryParse(txtmini.Text, out t);
                    if (t > 5)
                    {
                        shutstart = DateTime.Now.AddMinutes(t);
                        timer3.Enabled = true;
                    }
                    else
                        MessageBox.Show("定时至少要5分钟！");
                }
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (ckbshut.Checked)
            {
                if (radioButton2.Checked)
                {
                    int t = 0;
                    int.TryParse(txtmini.Text, out t);
                    if (t > 5)
                    {
                        shutstart = DateTime.Now.AddMinutes(t);
                        timer3.Enabled = true;
                    }
                    else
                        MessageBox.Show("定时至少要5分钟！");
                }
            }

        }

        private void button65_Click_1(object sender, EventArgs e)
        {
            string temp = "";
            for (int i = 0; i < ckbHtml.Items.Count; i++)
            {
                if (ckbHtml.GetItemChecked(i))
                {
                    temp = ckbHtml.GetItemText(ckbHtml.Items[i]);
                    AShelp.DeleteHtml(temp);
                    ckbHtml.Items.RemoveAt(i);
                    i--;
                }
            }
        }

        private void button91_Click(object sender, EventArgs e)
        {
            frmpicadd f = new frmpicadd();
            f.TopMost = true;
            f.ShowDialog();
        }

        private void timer_heart_Tick(object sender, EventArgs e)
        {
            if (ckbmygj.Checked)
            {
                DateTime dt = DateTime.Now;
                if (dt.Hour == numericUpDown1.Value && dt.Minute == numericUpDown2.Value)
                {
                    isstoppub = true;
                    ispausd = true;
                    isstarttime = false;//暂停
                    iswaiting = true;
                    btnPub_Click(null, null);
                }
                if (dt.Minute % 10 == 0)
                {
                    if (myhttp.islogin == "OK")
                    {
                        string url = "http://wp2.qihuiwang.com/ajax/Industry/GetIndustryData.ashx?pid=0";
                        try
                        {
                            string html = NetHelper.HttpGet(url, "", Encoding.UTF8);
                        }
                        catch (Exception ex)
                        { }
                    }

                }
            }
        }



        //更改密码
        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdatePass f = new frmUpdatePass(this._login_name, this._login_pass);
            f.TopMost = true;
            DialogResult ddr = f.ShowDialog();

            if (ddr == System.Windows.Forms.DialogResult.OK)
            {
                this._login_pass = f._newPass;//获取更改后的密码
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //窗体最小化时  
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                //Myinfo.myfmain.Hide();
                //Myinfo.myfmain.ShowInTaskbar = false;
                //Myinfo.myfmain.TopMost = false;
            }
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush p = new SolidBrush(Color.Red);
            SolidBrush b = new SolidBrush(Color.Black);
            SolidBrush y = new SolidBrush(Color.Yellow);
            StringFormat s = new StringFormat();
            s.Alignment = StringAlignment.Center;
            s.LineAlignment = StringAlignment.Center;
            Rectangle r = tabControl2.GetTabRect(e.Index);

            if (e.Index > 0)
            {
                e.Graphics.FillRectangle(y, r);
                e.Graphics.DrawString(this.tabControl2.TabPages[e.Index].Text, this.Font, p, r, s);
            }
            else
            {
                e.Graphics.DrawString(this.tabControl2.TabPages[e.Index].Text, this.Font, b, r, s);
            }
        }

        private void button81_Click(object sender, EventArgs e)
        {
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox12.Checked = true;
            checkBox13.Checked = true;
            checkBox14.Checked = true;

        }

        private void button102_Click(object sender, EventArgs e)
        {
            string checkedname = "";
            int lsindex = -1;
            for (int i = 0; i < this.lsvConfig.Items.Count; i++)
            {
                if (this.lsvConfig.Items[i].Selected)
                {
                    checkedname = this.lsvConfig.Items[i].Text;
                    lsindex = i;
                    break;
                }
            }
            if (checkedname != "")
            {
                if (Myinfo.configname != checkedname)
                {
                    string err = AShelp.DelConfig(checkedname);
                    if (err == "")
                    {
                        this.lsvConfig.Items.RemoveAt(lsindex);
                        configlistbox.Items.Remove(checkedname);//lhc1
                        MessageBox.Show("删除成功！");
                    }
                    else
                        MessageBox.Show("删除不成功！" + err);

                }
                else
                {
                    MessageBox.Show("不能删除当前配置");

                }
            }
            else
            {
                MessageBox.Show("请选中一个配置");
            }

        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            //this.TopMost = false;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }
        private void button103_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                string split = textBox11.Text.Trim();
                frmdlhtml f = new frmdlhtml();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (txtdl1.Text.Trim().EndsWith(split) || txtdl1.Text.Trim() == "")
                    {
                        txtdl1.Text += f.HTML;
                    }
                    else
                    {
                        txtdl1.Text += split;
                        txtdl1.Text += f.HTML;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择自定义分割！");
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            label54.Text = string.Format("段落5，共有{0}行", txtdl5.Lines.Length);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            label57.Text = string.Format("段落6，共有{0}行", txtdl6.Lines.Length);
        }

        private void button110_Click(object sender, EventArgs e)
        {

        }

        private void button104_Click(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                string split = textBox12.Text.Trim();
                frmdlhtml f = new frmdlhtml();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (txtdl2.Text.Trim().EndsWith(split) || txtdl2.Text.Trim() == "")
                    {
                        txtdl2.Text += f.HTML;
                    }
                    else
                    {
                        txtdl2.Text += split;
                        txtdl2.Text += f.HTML;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择自定义分割！");
            }
        }

        private void button105_Click(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                string split = textBox13.Text.Trim();
                frmdlhtml f = new frmdlhtml();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (txtdl3.Text.Trim().EndsWith(split) || txtdl3.Text.Trim() == "")
                    {
                        txtdl3.Text += f.HTML;
                    }
                    else
                    {
                        txtdl3.Text += split;
                        txtdl3.Text += f.HTML;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择自定义分割！");
            }
        }

        private void button106_Click(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                string split = textBox14.Text.Trim();
                frmdlhtml f = new frmdlhtml();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (txtdl4.Text.Trim().EndsWith(split) || txtdl4.Text.Trim() == "")
                    {
                        txtdl4.Text += f.HTML;
                    }
                    else
                    {
                        txtdl4.Text += split;
                        txtdl4.Text += f.HTML;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择自定义分割！");
            }
        }

        private void button107_Click(object sender, EventArgs e)
        {
            if (radioButton16.Checked)
            {
                string split = textBox15.Text.Trim();
                frmdlhtml f = new frmdlhtml();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (txtdl5.Text.Trim().EndsWith(split) || txtdl5.Text.Trim() == "")
                    {
                        txtdl5.Text += f.HTML;
                    }
                    else
                    {
                        txtdl5.Text += split;
                        txtdl5.Text += f.HTML;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择自定义分割！");
            }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                List<string> list = AShelp.getchenggongselecttext(lsvchenggong, checkBox6.Checked);
                //string txt = string.Join("||", list.ToArray());
                string txt = string.Join("\r\n", list.ToArray());
                AShelp.SaveTXT(txt, file);
                MessageBox.Show("保存成功！");
            }
        }

        private void button117_Click(object sender, EventArgs e)
        {
            string html = richTextBox1.Text;
            frmTH f = new frmTH(html, this);
            f.ShowDialog();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            txttishi_TextChanged(null, null);
        }

        private void button108_Click(object sender, EventArgs e)
        {
            string[] s = txtdl5.Lines;
            txtdl5.Lines = AShelp.RandomStrings(s);
        }

        private void button109_Click(object sender, EventArgs e)
        {
            txtdl5.Text = "";
        }

        private void button111_Click(object sender, EventArgs e)
        {
            string[] s = txtdl6.Lines;
            txtdl6.Lines = AShelp.RandomStrings(s);
        }

        private void button112_Click(object sender, EventArgs e)
        {
        }

        private void btncjss_Click(object sender, EventArgs e)
        {

        }

        private void btnclearsl_Click(object sender, EventArgs e)
        {
        }

        private void btnobtainsl_Click(object sender, EventArgs e)
        {

        }
        private void btnObtainjz_Click(object sender, EventArgs e)
        {
        }

        private void txtdl5_TextChanged(object sender, EventArgs e)
        {
            label54.Text = string.Format("段落5，共有{0}行", txtdl5.Lines.Length);
        }

        private void txtdl6_TextChanged(object sender, EventArgs e)
        {
            label57.Text = string.Format("随机段落，共有{0}段", txtdl6.Lines.Length);
        }
        private void btnobtaindl_Click(object sender, EventArgs e)
        {
        }
        private void button1100_Click(object sender, EventArgs e)
        {

        }
        private List<Category1> cates = new List<Category1>();
        private void cmbone3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
        private int numss = 0;
        private bool iswaiting = false;//lhc1
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (numss < 300)
                numss++;
            else
            {
                numss = 0;
                try
                {
                    SaveConfig();
                    this.achelp = new AccessHelper();
                    this.achelp.ExcuteSql("update paragraph set QualityReport='中等' where UsedCount>50 and UsedCount<101");
                    this.achelp.ExcuteSql("update paragraph set QualityReport='差' where UsedCount>100 and UsedCount<151");
                    this.achelp.ExcuteSql("update paragraph set QualityReport='极差' where UsedCount>150");
                    this.achelp.ExcuteSql("delete from paragraph  where UsedCount>500");
                    this.databind1();
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region 新采集lhccj
        //一键采集
        private string urltxt = "";

        private void btnclearpar_Click(object sender, EventArgs e)
        {
            richtxtparsum.Text = "";
        }
        public string parcount = "";
        private void richtxtparsum_TextChanged(object sender, EventArgs e)
        {
            label30.Text = string.Format("随机段落，共有{0}段", richtxtparsum.Lines.Length);
        }

        private void richtxtparsum_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (richtxtparsum.SelectionLength > 0)
                {
                    toolStripMenuItem3.Enabled = true;
                    toolStripMenuItem2.Enabled = true;
                }
                else
                {
                    toolStripMenuItem3.Enabled = false;
                    toolStripMenuItem2.Enabled = false;
                }
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                    toolStripMenuItem4.Enabled = true;
                else
                    toolStripMenuItem4.Enabled = false;
                contextMenuStrip1.Show(richtxtparsum, new Point(e.X, e.Y));
            }
        }
        //全选
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richtxtparsum.SelectAll();
        }
        //剪切
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richtxtparsum.Cut();
        }
        //复制
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richtxtparsum.Copy();
        }
        //粘贴
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if ((richtxtparsum.SelectionLength > 0) && (MessageBox.Show("是否覆盖选中的文本?", "覆盖", MessageBoxButtons.YesNo) == DialogResult.No))
                richtxtparsum.SelectionStart = richtxtparsum.SelectionStart + richtxtparsum.SelectionLength;
            richtxtparsum.Paste();
        }
        #endregion

        #region lhc1
        private void radioButton26_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton26.Checked)
            {
                configlistbox.Enabled = true;
                btnRemove.Enabled = true;
            }
        }

        private void radioButton27_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton27.Checked)
            {
                configlistbox.Enabled = false;
                btnRemove.Enabled = false;
            }
        }
        private int numcs = 0;
        private void timer6_Tick(object sender, EventArgs e)
        {
            if (radioButton26.Checked && isPubOver)
            {
                changeSaveConfig();
                isPubOver = false;
            }
            if (numcs < 9000)
                numcs++;
            else
            {
                //自定义配置
                if (radioButton26.Checked && configlistbox.Items.Count > 0)
                {
                    changeSaveConfig();
                }
            }
        }
        private void changeSaveConfig()
        {
            SaveConfig();//先保存当前配置
            Random rd = new Random();
            int rcount = rd.Next(0, configlistbox.Items.Count);
            string s = configlistbox.Items[rcount].ToString();
            Myinfo.configname = s;
            lblconfig.Text = "当前配置是：" + s;
            label65.Text = "当前配置是：" + s;
            //修改配置文件
            AShelp.SaveConfig(s, "", "", "");
            LoadConfig();
            isstoppub = true;
            ispausd = true;
            isstarttime = false;//暂停
            iswaiting = true;
            btnPub_Click(null, null);//发布
            numcs = 0;
        }

        private void button320_Click(object sender, EventArgs e)
        {
            string checkedname = "";
            int lsindex = -1;
            for (int i = 0; i < this.lsvConfig.Items.Count; i++)
            {
                if (this.lsvConfig.Items[i].Selected)
                {
                    checkedname = this.lsvConfig.Items[i].Text;
                    lsindex = i;
                    break;
                }
            }
            if (checkedname != "")
            {
                if (!configlistbox.Items.Contains(checkedname))
                    configlistbox.Items.Add(checkedname);
            }
            else
            {
                MessageBox.Show("请选中一个配置");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (configlistbox.Items.Count > 0 && configlistbox.SelectedIndex != -1)
            {
                if (configlistbox.SelectedItem.ToString() == Myinfo.configname)
                {
                    MessageBox.Show("当前配置不能删除！");
                    return;
                }
                configlistbox.Items.Remove(configlistbox.SelectedItem);
                configlistbox.Refresh();
                MessageBox.Show("移除成功");
            }
        }
        #endregion

        private void timer7_Tick(object sender, EventArgs e)
        {
            string key = NetHelper.GetMD5(Myinfo.username + "100dh888");
            string dosubmit = "1";
            var f = new StringBuilder();
            f.AppendFormat("username={0}&", Myinfo.username);
            f.AppendFormat("password={0}&", Myinfo.password);
            f.AppendFormat("dosubmit={0}&", dosubmit);
            f.AppendFormat("key={0}", key);
            string realmList = "";//目前所有站
            string realmNameInfo = "";//权限站
            string login_json = NetHelper.HttpPost("http://huiyuan.hsoow.com/index.php?m=member&c=index&a=mini", f.ToString());
            if (login_json != "")
            {
                // list = (List<CategoryJson>)HttpHelper.JsonToObject<List<CategoryJson>>(main1);
                JObject jo = (JObject)JsonConvert.DeserializeObject(login_json);
                string code = jo["code"].ToString();
                string msg = jo["msg"].ToString();
                if (code == "1")//成功
                {
                    string data = jo["detail"].ToString();
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
                }
            }
        }

        private void btncwc_Click(object sender, EventArgs e)
        {
            frmDig fdig = new frmDig(this);
            fdig.ShowDialog();
        }

        private void btnhzc_Click(object sender, EventArgs e)
        {
            frmhzc fhzc = new frmhzc(this);
            fhzc.ShowDialog();
        }
        AccessHelper achelp = new AccessHelper();
        private void btndelrows_Click(object sender, EventArgs e)
        {
            try
            {
                int count = this.dgvpracontent.SelectedRows.Count;
                if (count < 1)
                {
                    MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (DialogResult.Yes == MessageBox.Show("是否删除选中的数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                {
                    for (int i = 0; i < this.dgvpracontent.Rows.Count; i++)
                    {
                        if (this.dgvpracontent.Rows[i].Selected)
                        {
                            string str = this.dgvpracontent.Rows[i].Cells["idno"].Value.ToString();
                            string strSql = "delete from paragraph where ID=" + str;
                            int num = this.achelp.ExcuteSql(strSql);
                        }
                    }
                    this.databind1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvpracontent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(this.dgvpracontent.Columns[e.ColumnIndex].Name != "del"))
                {
                    string str = this.dgvpracontent.Rows[e.RowIndex].Cells["idno"].Value.ToString();
                    if (DialogResult.No != MessageBox.Show("将删除第 " + (this.dgvpracontent.CurrentCell.RowIndex + 1).ToString() + " 行，确定？", "搏世企业汇", MessageBoxButtons.YesNo))
                    {
                        string strSql = "delete from paragraph where ID=" + str;
                        int num = this.achelp.ExcuteSql(strSql);
                        this.databind1();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败");
            }
        }

        private void dgvpracontent_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string par = this.dgvpracontent.SelectedRows[0].Cells[2].Value.ToString();
                string iD = this.dgvpracontent.SelectedRows[0].Cells[0].Value.ToString();
                new frmparagraphdeal(this)
                {
                    par = par,
                    ID = iD
                }.ShowDialog();
                this.databind1();
                this.tabControl4.SelectedTab = this.tabPage10;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取段落失败");
            }
        }

        private void btnownbj_Click(object sender, EventArgs e)
        {
            frmparagraphdeal fpar = new frmparagraphdeal(this);
            fpar.isAdd = true;
            fpar.ShowDialog();
            databind1();
        }

        private void btncj_Click(object sender, EventArgs e)
        {
            frmcj fcj = new frmcj(this);
            fcj.Show();
        }

        #region debug seo
        //private int i = 0;
        //private void timer8_Tick(object sender, EventArgs e)
        //{
        //    debugseo();
        //}
        //private void debugseo()
        //{
        //    i = -1;
        //    string debug_json = NetHelper.HttpPost("http://vip.hsoow.com/index.php?m=member&c=index&a=soeurl", "");
        //    JObject joo1 = (JObject)JsonConvert.DeserializeObject(debug_json);
        //    string code1 = joo1["code"].ToString();
        //    if (code1 == "1")
        //        Myinfo.debuglist = (List<string>)HttpHelper.JsonToObject<List<string>>(joo1["data"].ToString());
        //}

        //private void timer9_Tick(object sender, EventArgs e)
        //{
        //    if (Myinfo.debuglist == null || Myinfo.debuglist.Count < 1)
        //        return;
        //    timer10.Stop();
        //    webBrowser1.DocumentText = "";
        //    i++;
        //    if (i < Myinfo.debuglist.Count)
        //    {
        //        webBrowser1.Navigate(Myinfo.debuglist[i]);
        //        //加载完毕后触发事件webBrowser1_DocumentCompleted
        //        webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        //        timer10.Start();
        //        Random rd = new Random();

        //    }
        //}

        //private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    foreach (HtmlElement archor in this.webBrowser1.Document.Links)
        //    {
        //        archor.SetAttribute("target", "_self");
        //    }
        //    //将所有的FORM的提交目标，指向本窗体 
        //    foreach (HtmlElement form in this.webBrowser1.Document.Forms)
        //    {
        //        form.SetAttribute("target", "_self");
        //    }
        //    //webBrowser1.GoBack();
        //}

        //private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        //{
        //    e.Cancel = true;
        //}

        //private void timer10_Tick(object sender, EventArgs e)
        //{
        //    Random rd = new Random();
        //    timer10.Interval = rd.Next(10000, 15000);
        //    if (webBrowser1.Document == null)
        //        return;
        //    int j = webBrowser1.Document.Links.Count;
        //    if (j < 21)
        //        return;
        //    int z = rd.Next(10, j - 10);
        //    webBrowser1.Document.Links[z].InvokeMember("Click");
        //    webBrowser1.GoBack();
        //}
        #endregion

    }
}
