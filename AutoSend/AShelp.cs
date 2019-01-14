using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Configuration;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace AutoSend
{
    public static class AShelp
    {
        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }
        public static string[] RandomStrings(string[] str, int count)
        {
            if (str.Length > 0)
            {
                List<System.String> list = new List<System.String>(str);
                List<string> newstr = new List<string>();
                Random r = new Random();
                string temp = "";
                int c = list.Count;
                if (c >= count)
                {
                    int t = 0;
                    for (int i = 0; i < count; i++)
                    {
                        t = r.Next(list.Count);
                        temp = list[t];
                        list.RemoveAt(t);
                        newstr.Add(temp);
                    }
                    return newstr.ToArray();
                }
                else
                {
                    newstr = list;
                    int addc = count - list.Count;
                    int indext = 0;
                    for (int i = 0; i < addc; i++)
                    {
                        indext = i % list.Count;
                        newstr.Add(list[indext]);
                    }
                    return newstr.ToArray();
                }
            }
            else
            {
                return str;
            }
        }
        public static void SaveTXT(string html, string path, Encoding en)
        {
            File.WriteAllText(path, html, en);
        }

        public static string UrlEncode(string temp, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                string t = temp[i].ToString();
                string k = System.Web.HttpUtility.UrlEncode(t, encoding);
                if (t == k)
                {
                    stringBuilder.Append(t);
                }
                else
                {
                    stringBuilder.Append(k.ToUpper());
                }
            }
            return stringBuilder.ToString();
        }
        public static void SaveHtml(string html, string name)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\html";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + name + ".txt";
            File.WriteAllText(filename, html);
        }
        public static string LoadTXT(string path)
        {
            string html = "";
            if (!File.Exists(path))
            {
                return "";
            }
            html = File.ReadAllText(path, Encoding.Default);
            return html;
        }
        public static void SaveTXT(string html, string path)
        {
            File.WriteAllText(path, html);
        }
        public static string LoadHtml(string name)
        {
            string html = "";
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\html\" + name + ".txt";
            if (!File.Exists(path))
            {
                return "";
            }
            html = File.ReadAllText(path);
            return html;
        }
        public static bool DeleteHtml(string name)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\html\" + name + ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        public static List<string> LoadConfig(string file)
        {
            List<string> list = new List<string>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + "\\" + file;
            if (!File.Exists(path))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            //DataContractSerializer serializer = new DataContractSerializer(list.GetType());
            using (XmlReader reader = new XmlTextReader(path))
            {
                return (List<string>)serializer.Deserialize(reader);
            }
        }
        #region lhc1
        public static void SaveCategoryConfig(List<string> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static List<string> LoadCategoryConfig(string type)
        {
            List<string> list = new List<string>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\" + type;
            if (!File.Exists(path))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (XmlReader reader = new XmlTextReader(path))
            {
                return (List<string>)serializer.Deserialize(reader);
            }
        }
        #endregion
        public static string DelConfig(string file)
        {
            string isok = "配置不存在";
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + "\\config\\" + file;
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                    isok = "";
                }
                catch (Exception exx)
                {
                    isok = exx.Message.ToString();
                }

            }
            return isok;
        }
        public static List<string> Load(string file)
        {
            List<string> list = new List<string>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + file;
            if (!File.Exists(path))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (XmlReader reader = new XmlTextReader(path))
            {
                return (List<string>)serializer.Deserialize(reader);
            }
        }
        public static void SaveConfig(string name1, string name2, string name3, string name4)
        {
            List<string> graph = new List<string> {
                name1,
                name2,
                name3,
                name4
            };
            string path = Application.StartupPath + "\\" + Myinfo.snameword;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + Myinfo.username + "\\use";
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }

        public static void Save(string name1, string name2, string name3, string name4, string file)
        {
            List<string> graph = new List<string> {
                name1,
                name2,
                name3,
                name4
            };
            string path = Application.StartupPath + "\\" + Myinfo.snameword;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + file;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            //ts.Subtract(new TimeSpan(0, 0, 1));
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }
        public static string DateDiff(ref TimeSpan ts)
        {
            string dateDiff = null;
            ts = ts.Subtract(new TimeSpan(0, 0, 1));
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }
        /// <summary>
        /// 打乱数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] RandomStrings(string[] str)
        {
            if (str.Length > 0)
            {
                List<System.String> list = new List<System.String>(str);
                List<string> newstr = new List<string>();
                Random r = new Random();
                string temp = "";
                int c = list.Count;
                int t = 0;
                for (int i = 0; i < c; i++)
                {
                    t = r.Next(list.Count);
                    temp = list[t];
                    list.RemoveAt(t);
                    newstr.Add(temp);
                }
                return newstr.ToArray();
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 去重复数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] delreStrings(string[] str)
        {
            if (str.Length > 0)
            {
                List<string> jg = new List<string>();

                foreach (string s in str)
                {
                    jg.Add(s);
                }
                return delreStrings(jg);
            }
            else
                return str;
        }
        /// <summary>
        /// 去重复数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] delreStrings(List<System.String> str)
        {
            if (str.Count > 0)
            {
                string t = "";
                for (int i = 0; i < str.Count; i++)
                {
                    t = str[i];
                    for (int j = i + 1; j < str.Count; j++)
                    {
                        if (t == str[j])
                        {
                            str.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
            return str.ToArray();
        }
        /// <summary>
        /// 去空数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] delspaceStrings(string[] s)
        {
            List<System.String> str = new List<string>(s);
            List<string> newstr = new List<string>();
            if (str.Count > 0)
            {
                string t = "";
                for (int i = 0; i < str.Count; i++)
                {
                    t = str[i];
                    if (t.Trim().Length > 0 && t.Trim() != "\r\n")
                        newstr.Add(t);
                }
            }
            return newstr.ToArray();
        }
        public static string[] GetConfigs()
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path + @"\默认");
            }
            string[] dirs = Directory.GetDirectories(path);
            List<string> subDirectories = new List<string>();
            foreach (string subDirectory in dirs)
            {
                subDirectories.Add(subDirectory.Substring(subDirectory.LastIndexOf(@"\") + 1));
            }
            return subDirectories.ToArray();
        }
        public static string[] GetHtmls()
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\html";
            if (!Directory.Exists(path))
            {
                return null;
            }
            string[] dirs = Directory.GetFiles(path, "*.txt");
            List<string> subDirectories = new List<string>();
            foreach (string subDirectory in dirs)
            {
                string f = new FileInfo(subDirectory).Name;
                subDirectories.Add(f.Substring(0, f.LastIndexOf(".")));
            }
            return subDirectories.ToArray();
        }
        public static bool ChangeConfigs(string oldname, string newname)
        {
            string npath = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + newname;
            string opath = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + oldname;
            if (!Directory.Exists(opath))
            {
                return false;
            }
            else
            {
                try
                {
                    System.IO.Directory.Move(opath, npath);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                    return false;
                }
            }
        }
        public static bool CopyConfigs(string oldname, string newname)
        {
            string npath = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + newname;
            string opath = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + oldname;
            if (!Directory.Exists(opath))
            {
                return false;
            }
            else
            {
                try
                {
                    CopyDirectory(opath, npath);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                    return false;
                }
            }
        }
        static void CopyDirectory(string srcDir, string tgtDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            //if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    throw new Exception("父目录不能拷贝到子目录！");
            //}

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name);
            }
        }
        public static bool DBcheck(string configName)
        {
            bool ishave = false;
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + configName + @"\Database2.dll";
            if (File.Exists(path))
            {
                ishave = true;
            }
            else
            {
                string syspath = System.AppDomain.CurrentDomain.BaseDirectory;
                if (!syspath.EndsWith(@"\")) syspath = syspath + @"\";
                syspath = syspath + @"Database2.dll";
                try
                {
                    File.Copy(syspath, path);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                    ishave = false;
                }
            }
            return ishave;
        }
        public static void SaveWZ(List<string> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static void SaveWZbak(List<string> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\bak";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static List<string> LoadWZ(string type)
        {
            List<string> list = new List<string>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\" + type;
            if (!File.Exists(path))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (XmlReader reader = new XmlTextReader(path))
            {
                return (List<string>)serializer.Deserialize(reader);
            }
        }

        public static void SaveTitle(List<infos> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static void SavebakTitle(List<infos> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\bak";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static List<infos> LoadTitle(string type)
        {
            List<infos> list = new List<infos>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\" + type;
            if (!File.Exists(path))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (XmlReader reader = new XmlTextReader(path))
            {
                return (List<infos>)serializer.Deserialize(reader);
            }
        }
        public static void SaveCategory(List<Category> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }

        public static void SaveCategory1(List<Category1> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }

        public static List<Category> LoadCategory(string type)
        {
            List<Category> list = new List<Category>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\" + type;
            if (!File.Exists(path))
            {
                return null;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(list.GetType());
                using (XmlReader reader = new XmlTextReader(path))
                {
                    return (List<Category>)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {

                return list;
            }
        }
        public static List<Category1> LoadCategory1(string type)
        {
            List<Category1> list = new List<Category1>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\" + type;
            if (!File.Exists(path))
            {
                return null;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(list.GetType());
                using (XmlReader reader = new XmlTextReader(path))
                {
                    return (List<Category1>)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {

                return list;
            }
        }
        public static void SaveDropDL(List<DropDL> graph, string type)
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + @"\" + type;
            XmlSerializer serializer = new XmlSerializer(graph.GetType());
            using (XmlWriter writer = new XmlTextWriter(filename, Encoding.UTF8))
            {
                serializer.Serialize(writer, graph);
            }
        }
        public static List<DropDL> LoadDropDL(string type)
        {
            List<DropDL> list = new List<DropDL>();
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\" + type;
            if (!File.Exists(path))
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (XmlReader reader = new XmlTextReader(path))
            {
                return (List<DropDL>)serializer.Deserialize(reader);
            }
        }
        public static void selectall(System.Windows.Forms.ListView ls)
        {
            for (int i = 0; i < ls.Items.Count; i++)
            {
                ls.Items[i].Checked = true;
            }
        }
        public static void reversselect(System.Windows.Forms.ListView ls)
        {
            for (int i = 0; i < ls.Items.Count; i++)
            {
                ls.Items[i].Checked = !ls.Items[i].Checked;
            }
        }
        public static void delselect(System.Windows.Forms.ListView ls)
        {
            foreach (System.Windows.Forms.ListViewItem lvi in ls.CheckedItems)  //选中项遍历
            {
                ls.Items.RemoveAt(lvi.Index); // 按索引移除                
            }
        }
        public static List<string> getselecttext(System.Windows.Forms.ListView ls)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < ls.Items.Count; i++)
            {
                if (ls.Items[i].Checked)
                    list.Add(ls.Items[i].Text);
            }
            return list;
        }
        public static List<string> getchenggongselecttext(System.Windows.Forms.ListView ls, bool istitle)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < ls.Items.Count; i++)
            {
                if (ls.Items[i].Checked)
                {
                    var str = ls.Items[i].SubItems[1].Text;
                    if (string.IsNullOrEmpty(str)) continue;
                    if (istitle)
                    {
                        str += "\t" + ls.Items[i].SubItems[0].Text;
                    }
                    list.Add(str);
                }
            }
            return list;
        }


    }
}
