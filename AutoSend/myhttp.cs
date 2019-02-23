using System;
using System.Collections.Generic;

using System.Text;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Net;

using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AutoSend
{
    public class myhttp
    {
        public static string cookie;
        public static HttpHelper http = new HttpHelper();
        public static string strjs;
        public static string mainurl;
        public static string myid;
        public static string sgid;
        public static string uploadset;
        public static string islogin = "OK";
        public static int GetPage(string url)
        {
            int i = 1;
            string r = NetHelper.HttpGet(url, "", Encoding.UTF8);
            r = HttpHelper.GetBetweenHtml(r, "pageCount =", ";").Trim();
            if (r.Length > 0)
            {
                r = r.Trim();
                //r = r.Substring(1, r.Length - 2);
                int.TryParse(r, out i);
            }

            return i;
        }
        public static int getconeid()
        {
            int i = 0;
            string httppub = "http://b2b.sg560.com/member/sell/SellTosr" + myhttp.sgid + ".html";
            HttpResult r = HttpGet(httppub, "");
            string hidtitlc = myhttp.GetHtmlID(r.Html.ToString(), "input", "coneid");
            int.TryParse(hidtitlc, out i);
            return i;
        }
        public static bool DownloadFile(string URL, string filename)
        {
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                myrp.Close();
                Myrq.Abort();
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        public static List<CsharpHttpHelper.Item.ImgItem> GetImags(string url, out string html)
        {
            html = NetHelper.HttpGet(url, "", Encoding.UTF8);

            List<CsharpHttpHelper.Item.ImgItem> imglist = HttpHelper.GetImgList(html);
            return imglist;

        }
        public static List<CsharpHttpHelper.Item.ImgItem> GetImagshg(string url)
        {
            string html = NetHelper.HttpGet(url, "", Encoding.Default);

            List<CsharpHttpHelper.Item.ImgItem> imglist = HttpHelper.GetImgList(html);
            return imglist;

        }
        public static string postToPing(string url)
        {
            string re = "";
            try
            {
                string posturl = "http://ping.baidu.com/ping/RPC2"; //post 提交地址
                string refurl = ""; //这里可以随便填写.
                string content_type = "text/xml";        //提交类型.这里一定要text/xml
                string postdt = postdata(url);              //提交数据
                string str = baiduping(posturl, postdt, content_type, refurl, false, Encoding.UTF8);
                Stream sm = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(str)); //下面这里检测提交是否成功
                XmlDocument doc = new XmlDocument();
                doc.Load(sm);
                XmlElement xle = doc.DocumentElement;
                XmlNode node = doc.SelectSingleNode("int");
                string value = node.InnerText;

                if (value != "0")
                {

                    re = "失败";
                }
                else
                {
                    re = "成功";
                }
            }
            catch (Exception ex)
            {
                re = ex.Message;
            }
            return re;
        }
        private static string postdata(string url)
        {
            //注意xml拼接的时候,xml的第一行的开头必须不能有空格等 //下面直接是引用百度的例子
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?><methodCall>");
            sb.AppendLine(" <methodName>weblogUpdates.extendedPing</methodName>");
            sb.AppendLine("<params>");
            sb.AppendLine(" <param> ");
            sb.AppendLine("<value><string>" + url + "</string></value>");
            sb.AppendLine(" </param> ");
            sb.AppendLine(" <param>");
            sb.AppendLine("<value><string>" + url + "/</string></value>");
            sb.AppendLine("</param>");
            sb.AppendLine(" <param> ");
            sb.AppendLine("<value><string>" + url + "</string></value>");
            sb.AppendLine(" </param> ");
            sb.AppendLine(" <param>");
            sb.AppendLine("<value><string>" + url + "/</string></value>");
            sb.AppendLine("</param>");
            sb.AppendLine("</params>");
            sb.AppendLine("</methodCall>");
            return sb.ToString().Trim();
        }

        private static string baiduping(string targetURL, string formData, string contentType, string referer, bool allowAutoRedirect, Encoding ed)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(formData);
            //请求目标网页
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
            request.Method = "POST";    //使用post方式发送数据
            request.UserAgent = "request";
            request.Referer = referer;
            request.ProtocolVersion = new Version("1.0");  //注意这里这个版本好.一定要设置.现在默认提交是1.1了.否则会一直提示504
            request.ContentType = contentType == "" ? "application/x-www-form-urlencoded" : contentType;
            request.Timeout = 1000 * 10;
            request.ContentLength = data.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string html = new StreamReader(stream, ed).ReadToEnd();
            return html;
        }
        public static Image getCodeimage()
        {
            string url = "http://www.dh0315.com/new/api.php?op=checkcode&code_len=4&font_size=16&width=150&height=45&font_color=&background=&" + new Random().Next().ToString();
            Image img = NetHelper.HttpGetImage(url);
            return img;
        }
        public static string host = "http://cdn.chemcp.com";
        public static string Login(string name, string pwd, string code)
        {
            //myhttp.host = "http://cdn.chemcp.com";
            //string html = "";
            //string html1 = "";
            string html2 = "";
            //string url = "http://www.hsoow.com/member/login.php";//华搜网
            //string url1 = myhttp.host + "/login/Login_Check.asp";//化工产品网
            string url2 = "http://www.dh0315.com/new/login.html";//唐山网
            //string postDate = string.Format("username={0}&password={1}&submit=", name, pwd, code);
            //string postDate1 = string.Format("UsernameGet={0}&PasswordGet={1}", name, pwd);
            string postDate2 = string.Format("forward=&username={0}&password={1}&code={2}&dosubmit=1", name, pwd, code);
            //html = NetHelper.HttpPost(url, postDate, Encoding.UTF8);
            //html1 = NetHelper.HttpPost(url1, postDate1,Encoding.Default);
            html2 = NetHelper.HttpPost(url2, postDate2, "");
            //1
            if (html2.Contains("登录成功"))
            {
                html2 = "ok";
            }
            else
            {
                //string temp = html2.Substring(html2.LastIndexOf("<div class=\"msg\" style=\"margin-top:15px\">") + 41);
                //html2 = temp.Substring(0, temp.IndexOf("</div>"));
                html2 = "登录失败，请检查用户名密码或验证码是否错误";
            }
            return html2;
        }
        public static HttpResult PubTitleByte(string url, string post)
        {
            return HttpPostByte(url, post);
        }
        public static HttpResult PubTitle(string url, string post)
        {
            return HttpPost(url, post);
        }
        public static HttpResult PubTitleMulti(string url, string post)
        {
            return HttpPostMulti(url, post);
        }
        public static string GetHtmlValues(string html, string tag, string search, string searchattr, string getattr)
        {
            string v = "";
            Regex re = new Regex(@"(<" + tag + @"[\s\S]*?>)");
            MatchCollection imgreg = re.Matches(html);
            List<string> m_Attributes = new List<string>();
            Regex attrReg = new Regex(@"([a-zA-Z1-9_-]+)\s*=\s*(\x27|\x22)([^\x27\x22]*)(\x27|\x22)", RegexOptions.IgnoreCase);
            Regex id = new Regex(" " + searchattr + "=\"(?<value>.*?)\"", RegexOptions.IgnoreCase);
            Regex value = new Regex(" " + getattr + "=\"(?<value>.*?)\"", RegexOptions.IgnoreCase);
            for (int i = 0; i < imgreg.Count; i++)
            {
                MatchCollection matchs = id.Matches(imgreg[i].ToString());
                for (int j = 0; j < matchs.Count; j++)
                {
                    GroupCollection groups = matchs[j].Groups;
                    if (search.ToUpper() == groups["value"].Value.ToUpper())
                    {
                        matchs = value.Matches(imgreg[i].ToString());
                        for (int m = 0; m < matchs.Count; m++)
                        {
                            if (v == "")
                                v = matchs[m].Groups["value"].Value.ToString();
                            else
                                v += "," + matchs[m].Groups["value"].Value.ToString();

                        }
                    }
                }
            }
            return v;
        }
        public static string Refreshtitle(string url, int i, string purl)
        {
            string re = "";
            re = NetHelper.HttpGetUTF(url + "?page=" + i + "&kind=1&key=", "");
            string values = GetHtmlValues(re, "input", "cbItem", "name", "value");
            if (values != "")
            {
                StringBuilder st = new StringBuilder();

                st.AppendFormat("id={0}&", values);
                st.AppendFormat("type={0}&", "refreshlist");
                st.AppendFormat("kind={0}&", "");
                st.AppendFormat("m={0}&", new Random().Next());
                re = NetHelper.HttpPostR(purl, st.ToString(), url);
                //return re;
                if (re.Contains("成功"))
                    re = "一键刷新成功！";
                return re;
            }
            else
                return "没有相关数据";

        }
        public static string Refreshtitlep(string url, int i, string purl)
        {
            string re = "";
            re = NetHelper.HttpGetUTF(url + "?page=" + i + "&kind=1&key=", "");
            string values = GetHtmlValues(re, "input", "iptckb", "name", "value");
            if (values != "")
            {
                StringBuilder st = new StringBuilder();
                st.AppendFormat("id={0}&", values);
                st.AppendFormat("type={0}&", "1");
                re = NetHelper.HttpPostR(purl, st.ToString(), url);
                if (re.Contains("1"))
                    re = "一键刷新成功！";
                return re;
            }
            else
                return "没有相关数据";

        }
        public static string Refreshtitle()
        {
            string re = "";
            HttpResult r = HttpGet("http://b2b.sg560.com/handler/UpdateTime.ashx?id=" + myhttp.myid + "&kind=1&key=", "");
            string data = r.Html;
            if (data == "1")
            {
                re = "刷新完成";
            }
            else if (data == "2")
            {
                re = "您今天已经刷新过了，请勿重复刷新";
            }
            else if (data == "3")
            {
                re = "您在8小时内已经刷新过了，请勿重复刷新";
            }
            else if (data == "4")
            {
                re = "您在6小时内已经刷新过了，请勿重复刷新";
            }
            else if (data == "5")
            {
                re = "您在4小时内已经刷新过了，请勿重复刷新";
            }
            else
            {
                re = "您这个月已经刷新过了，请勿重复刷新";
            }
            return re;

        }
        public static HttpResult HttpGet(string url, string postDataStr)
        {
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项    
                Method = "get",//URL     可选项 默认为Get  
                Cookie = cookie,
            };
            HttpResult result = http.GetHtml(item);

            return result;
        }
        public static List<Category> GetDDL(string url, string postDataStr)
        {
            List<Category> list = new List<Category>();
            string html = NetHelper.HttpGetUTF(url, "");
            list = getSelect("CustomType", html);
            return list;

        }
        public static List<Category> GetDDL(string postDataStr)
        {
            List<Category> list = new List<Category>();
            string html = NetHelper.HttpPostUTF("http://wp2.qihuiwang.com/Product/SubCustomType.aspx", "id=" + postDataStr + "&m=0." + new Random().Next().ToString());
            list = getSelectOptions(html);
            return list;

        }
        public static List<DropDL> GetHtmlahrefLike3(string html, string content)
        {
            List<DropDL> list = new List<DropDL>();
            Regex regex = new Regex("<td height=\"27\" width=\"200\"><font color=\"#d71345\">(?<key>.*?)</font></td>");
            MatchCollection matchCollection = regex.Matches(html);
            foreach (Match match in matchCollection)
            {
                list.Add(new DropDL
                {
                    value = match.Groups["key"].Value
                });
            }
            return list;
        }

        public static string HttpPostmy(string url, string postData)
        {
            HttpWebResponse response;
            HttpWebRequest request;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(postData);
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version10;
            request.UserAgent = "Mozilla/4.0";
            request.Headers.Add("Cookie", cookie);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            string html = string.Empty;
            try
            {
                //获取服务器返回的资源  
                response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                html = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                html = ex.Message;
            }
            return html;
        }
        public static HttpResult HttpPostByte(string url, string postDataStr)
        {
            byte[] bytedate = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项    
                Method = "post",//URL     可选项 默认为Get  
                Cookie = cookie,
                PostdataByte = bytedate,
                PostDataType = PostDataType.Byte,
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                ContentType = "application/x-www-form-urlencoded",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36",// "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                PostEncoding = System.Text.Encoding.UTF8,
            };
            HttpResult result = http.GetHtml(item);
            return result;
        }
        public static HttpResult HttpPost(string url, string postDataStr)
        {
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项    
                Method = "post",//URL     可选项 默认为Get  
                Cookie = cookie,
                Postdata = postDataStr,
                ContentType = "application/x-www-form-urlencoded",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                PostDataType = PostDataType.String,
                PostEncoding = System.Text.Encoding.UTF8,
            };
            HttpResult result = http.GetHtml(item);
            return result;
        }
        public static HttpResult HttpPostMulti(string url, string postDataStr)
        {
            string boundary = "----WebKitFormBoundaryYajflqIp8XWL7Dxd";
            string[] posts = postDataStr.Split('&');
            string name = "", value = "";
            StringBuilder sb = new StringBuilder();
            foreach (string s in posts)
            {
                name = s.Substring(0, s.IndexOf("="));
                value = s.Substring(s.IndexOf("="));
                byte[] bytedate = System.Text.Encoding.UTF8.GetBytes(value);
                sb.Append("--");
                sb.Append(boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"" + name + "\"\r\n\r\n");
                sb.Append(bytedate);
                sb.Append("\r\n");
            }
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项    
                Method = "post",//URL     可选项 默认为Get  
                Cookie = cookie,
                Postdata = postDataStr,
                ContentType = "multipart/form-data; boundary=" + boundary,
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                PostEncoding = System.Text.Encoding.UTF8,
            };
            HttpResult result = http.GetHtml(item);
            return result;
        }
        public static string GetValue(string url, string tag, string id)
        {
            string v = "";
            HttpResult r = HttpGet(url, "");
            v = GetHtmlID(r.Html.ToString(), tag, id);
            return v;

        }
        public static List<Category> getSelect(string key, string name, string strDoc)
        {
            string v = "";
            Regex status = new Regex("(?is)<select(?:(?!" + key + "=).)*" + key + "=\"" + name + "\"[^>]*>]*.*?</select>");
            List<Category> list = new List<Category>();
            MatchCollection mc = status.Matches(strDoc);
            foreach (Match m in mc)
            {
                v = m.Value;
            }
            if (v.Length < 1)
                return list;
            status = new Regex(@"(?is)<option(?:(?!value=).)*value=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?).)*)</option>");
            mc = status.Matches(v);
            foreach (Match m in mc)
            {
                int id = 0;
                int.TryParse(m.Groups["url"].Value.Trim(), out id);
                Category c = new Category();
                c.Id = id;
                c.EngName = m.Groups["text"].Value;
                list.Add(c);
            }
            return list;
        }
        public static List<Category> getSelect(string name, string strDoc)
        {
            string v = "";
            Regex status = new Regex("(?is)<select(?:(?!id=).)*id=\"" + name + "\"[^>]*>]*.*?</select>");
            MatchCollection mc = status.Matches(strDoc);
            foreach (Match m in mc)
            {
                v = m.Value;
            }
            return getSelectOptions(v);
        }

        public static List<Category> getSelectOptions(string v)
        {
            List<Category> list = new List<Category>();
            if (v.Length < 1)
                return list;
            Regex status = new Regex(@"(?is)<option(?:(?!value=).)*value=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?).)*)</option>");
            MatchCollection mc = status.Matches(v);
            foreach (Match m in mc)
            {
                int ids = 0;
                int.TryParse(m.Groups["url"].Value.Trim(), out ids);
                Category c = new Category();
                c.Id = ids;
                c.EngName = m.Groups["text"].Value.Replace("\r\n", "").Trim();
                list.Add(c);
            }
            return list;
        }
        public static List<Category> LoadCategory1(out List<Category> news)
        {
            string url = "http://www.dh0315.com/new/?members-content-public_publish.html";
            List<Category> list = new List<Category>();
            news = new List<Category>();
            var r = NetHelper.HttpGet(url, "", Encoding.UTF8);
            var i = r.IndexOf("<select ") + 8;
            r = r.Substring(i);
            r = r.Substring(0, r.IndexOf("</select>"));
            var a = r.Split(new[] { "<optgroup" }, StringSplitOptions.RemoveEmptyEntries);
            string cp = a[0], xw = a[1];
            i = cp.IndexOf("value=");
            while (i > -1)
            {
                cp = cp.Substring(i + 1);
                var c = new Category();
                c.ChsName = cp.Substring(0, cp.IndexOf("'"));
                cp = cp.Substring(cp.IndexOf("├ ") + 2);
                c.EngName = cp.Substring(0, cp.IndexOf("</option>"));
                list.Add(c);
                i = cp.IndexOf("value=");
            }
            i = xw.IndexOf("value=");
            while (i > -1)
            {
                xw = xw.Substring(i + 1);
                var c = new Category();
                c.ChsName = xw.Substring(0, xw.IndexOf("'"));
                xw = xw.Substring(xw.IndexOf("├ ") + 2);
                c.EngName = xw.Substring(0, xw.IndexOf("</option>"));
                news.Add(c);
                i = xw.IndexOf("value=");
            }
            return list;
        }
        public static List<Category> LoadCategory(string url)
        {
            List<Category> list = new List<Category>();
            string main1 = NetHelper.HttpGet(url, "", Encoding.UTF8);
            if (main1.Length > 0)
            {
                list = getSelect("size", "2", main1);
            }
            return list;
        }
        public static List<Category1> LoadCategoryhg(string url)
        {
            List<Category1> list = new List<Category1>();
            try
            {
                //获取添加页面html
                var content = NetHelper.HttpGet(url, "", Encoding.Default);
                //截取产品类别包含的片段
                var index = content.IndexOf("产品类别：");
                content = content.Substring(index);
                index = content.IndexOf("请选择产品小类");
                content = content.Substring(0, index);

                //获取大类
                index = content.IndexOf("请选择产品大类");
                var content1 = content.Substring(index).ToLower();
                index = content1.IndexOf("option value=") + 13;
                content1 = content1.Substring(index);
                index = content1.IndexOf("</option>");
                content1 = content1.Substring(0, index).Replace("\"", "");
                var arr = content1.Split('>');
                var cate = new Category1 { Name = arr[1], Id = arr[0], Children = new List<Category1>() };
                list.Add(cate);

                //获取小类
                index = content.IndexOf("subcat[0]");
                content = content.Substring(index);
                index = content.IndexOf("onecount");
                content = content.Substring(0, index);
                index = content.IndexOf("new Array");
                do
                {
                    content = content.Substring(index + 9);
                    index = content.IndexOf(";");
                    content1 = content.Substring(0, index);
                    arr = content1.Split(new[] { '"', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    var sub = new Category1 { Id = arr[2], Name = arr[0] };
                    cate.Children.Add(sub);

                    index = content.IndexOf("new Array");
                } while (index > 0);
            }
            catch { }

            return list;
        }
        public static List<Category> LoadCategory1(string url, string catId)
        {
            List<Category> list = new List<Category>();
            string main1 = NetHelper.HttpGet(url, "", Encoding.UTF8);
            if (main1.Length > 0)
            {
                string id = "";
                list = getSelect(catId, main1, out id);
            }
            return list;
        }
        public static List<Category> getSelect(string name, string strDoc, out string id)
        {
            string v = "";
            //Regex status = new Regex(@"(?is)<select(?:(?!name=).)*name=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</select>");
            Regex status = new Regex("(?is)<select(?:(?!id=).)*id=\"" + name + "\"[^>]*>]*.*?</select>");
            MatchCollection mc = status.Matches(strDoc);
            foreach (Match m in mc)
            {
                v = m.Value;
            }
            return getSelectOptions(v, out id);
        }
        public static List<Category> getSelectOptions(string v, out string id)
        {
            List<Category> list = new List<Category>();
            id = "";
            if (v.Length < 1)
                return list;
            Regex status = new Regex(@"(?is)<option(?:(?!value=).)*value=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?).)*)</option>");
            //(?is)<option(?:(?!value=).)*value=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(]*.*?)(?<text>[^"">]*)</option>
            MatchCollection mc = status.Matches(v);
            foreach (Match m in mc)
            {
                Category c = new Category();
                c.ChsName = m.Groups["url"].Value.Trim();
                c.EngName = m.Groups["text"].Value;
                list.Add(c);
                if (m.Value.Contains("selected"))
                {
                    id = c.ChsName;
                }
            }
            return list;
        }

        public static void getStrjs()
        {
            string url = "http://b2b.sg560.com/SgStyle/js/jsSort.js";
            strjs = string.Empty;
            strjs = HttpGet(url, "").Html.ToString();
            strjs += "function getdsy(i){var s='';var ar=dsy.Items[i];for (var i = 0; i < ar.length; i++) {s+= ar[i]+','; }return s;}";

        }

        public static List<Category> LoadChildren(string url, string id)
        {
            List<Category> list = new List<Category>();
            string postdata = "action=category&category_title=选择分类&category_moduleid=5&category_extend=55196ewGKH3xdcMc-P-PwACkB3lJJTmFXWk2zeGrr4H4yNI2yN5e42V7xi8Fn3N3PIeZk7WfZwDNt0QWPZ-P-ndADEKwsYtimtoIOdLr-P-TqdenOk-S-ymeV3mxYKTOoV0&category_deep=0&cat_id=1&catid=" + id;
            string main1 = NetHelper.HttpPost(url, postdata);
            if (main1.Length > 0)
            {
                list = getSelect("size", "2", main1);
            }
            return list;
        }
        public static string GetHtmlID(string html, string tag, string searchid)
        {
            string v = "";
            Regex re = new Regex(@"(<" + tag + @"[\w\W].+?>)");
            MatchCollection imgreg = re.Matches(html);
            List<string> m_Attributes = new List<string>();
            Regex attrReg = new Regex(@"([a-zA-Z1-9_-]+)\s*=\s*(\x27|\x22)([^\x27\x22]*)(\x27|\x22)", RegexOptions.IgnoreCase);
            Regex id = new Regex(" id=\"(?<value>.*?)\"", RegexOptions.IgnoreCase);
            Regex value = new Regex(" value=\"(?<value>.*?)\"", RegexOptions.IgnoreCase);
            for (int i = 0; i < imgreg.Count; i++)
            {
                MatchCollection matchs = id.Matches(imgreg[i].ToString());
                for (int j = 0; j < matchs.Count; j++)
                {
                    GroupCollection groups = matchs[j].Groups;
                    if (searchid.ToUpper() == groups["value"].Value.ToUpper())
                    {
                        matchs = value.Matches(imgreg[i].ToString());
                        for (int m = 0; m < matchs.Count; m++)
                        {
                            v = matchs[m].Groups["value"].Value.ToString();
                            break;
                        }
                    }
                }
            }
            return v;
        }
        public static string GetHtmlahref(string html, string content)
        {
            string v = "";
            Regex reg = new Regex(@"(?is)<a(?:(?!href=).)*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
            MatchCollection mc = reg.Matches(html);
            foreach (Match m in mc)
            {
                if (m.Groups["text"].Value.Trim() == HttpUtility.HtmlEncode(content).Trim())
                {
                    v = m.Groups["url"].Value;
                    return v;
                }
            }
            return v;

        }
        public static string GetHtmlahrefLike(string html, string content)
        {
            string v = "";
            Regex reg = new Regex(@"(?is)<a(?:(?!href=).)*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
            MatchCollection mc = reg.Matches(html);
            foreach (Match m in mc)
            {
                if (m.Groups["text"].Value.Trim().Contains(content))
                {
                    v = m.Groups["url"].Value;
                    return v;
                }
            }
            return v;

        }
        public static string GetHtmlValue(string html, string tag, string search, string searchattr, string getattr)
        {
            string v = "";
            Regex re = new Regex(@"(<" + tag + @"[\w\W].+?>)");
            MatchCollection imgreg = re.Matches(html);
            List<string> m_Attributes = new List<string>();
            Regex attrReg = new Regex(@"([a-zA-Z1-9_-]+)\s*=\s*(\x27|\x22)([^\x27\x22]*)(\x27|\x22)", RegexOptions.IgnoreCase);
            Regex id = new Regex(" " + searchattr + "=\"(?<value>.*?)\"", RegexOptions.IgnoreCase);
            Regex value = new Regex(" " + getattr + "=\"(?<value>.*?)\"", RegexOptions.IgnoreCase);
            for (int i = 0; i < imgreg.Count; i++)
            {
                MatchCollection matchs = id.Matches(imgreg[i].ToString());
                for (int j = 0; j < matchs.Count; j++)
                {
                    GroupCollection groups = matchs[j].Groups;
                    if (search.ToUpper() == groups["value"].Value.ToUpper())
                    {
                        matchs = value.Matches(imgreg[i].ToString());
                        for (int m = 0; m < matchs.Count; m++)
                        {
                            v = matchs[m].Groups["value"].Value.ToString();
                            break;
                        }
                    }
                }
            }
            return v;
        }
        private List<string> GetHtmlAttr(string html, string tag, string attr)
        {
            Regex re = new Regex(@"(<" + tag + @"[\w\W].+?>)");
            MatchCollection imgreg = re.Matches(html);
            List<string> m_Attributes = new List<string>();
            Regex attrReg = new Regex(@"([a-zA-Z1-9_-]+)\s*=\s*(\x27|\x22)([^\x27\x22]*)(\x27|\x22)", RegexOptions.IgnoreCase);
            for (int i = 0; i < imgreg.Count; i++)
            {
                MatchCollection matchs = attrReg.Matches(imgreg[i].ToString());
                for (int j = 0; j < matchs.Count; j++)
                {
                    GroupCollection groups = matchs[j].Groups;
                    if (attr.ToUpper() == groups[1].Value.ToUpper())
                    {
                        m_Attributes.Add(groups[3].Value);
                        break;
                    }
                }
            }
            return m_Attributes;
        }
        public static string getmyid(string url)
        {
            string myid = "";
            string temp = url.Substring(url.IndexOf("index") + 5);
            string mydate = DateTime.Now.ToString("MMddyyyy");
            myid = temp.Substring(0, temp.IndexOf(mydate) - 1);
            return myid;

        }
        public static string getmysgid(string url, string endstr)
        {
            string myid = "";
            string temp = url.Substring(url.IndexOf(endstr) + endstr.Length);
            myid = temp.Substring(0, temp.IndexOf("."));
            return myid;

        }
        public static string Upload(string files)
        {
            //上传到服务器tool域名下
            string url = "http://tool.100dh.cn/UploadImgHandler.ashx";
            FileInfo info = new FileInfo(files);
            string str2 = string.Empty;

            NameValueCollection stringDict = new NameValueCollection();
            stringDict.Add("username", Myinfo.username);
            stringDict.Add("productId", "1");
            stringDict.Add("key", GetMD5(Myinfo.username + "100dh888"));
            stringDict.Add("type", "SOFT");//数据库可增加一个“SOFT/YUN”字段，暂未加
            //stringDict.Add("uptype", "oss");
            stringDict.Add("submit", "1");
            try
            {
                str2 = NetHelper.HttpPostData(url, "file", info.FullName, stringDict);
                JObject joo = (JObject)JsonConvert.DeserializeObject(str2);
                if (joo == null)
                    str2 = "上传失败";
                else
                {
                    string code = joo["code"].ToString();
                    if (code == "1")
                        str2 = joo["detail"]["imgUrl"].ToString();
                    if (code == "0")
                        str2 = joo["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                str2 = "上传失败:" + ex.ToString();
            }
            return str2;
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
        public static string getpicsrc(string title)
        {
            string str2 = NetHelper.HttpPost("http://www.hsoow.com/ajax.php?action=choose&fid=&from=album&widget=1", null);
            string str3 = "<div class=\"photo_img\"><img src=\"";
            int index = str2.IndexOf(str3);
            string url = "";
            str2 = str2.Substring(index + str3.Length);
            index = str2.IndexOf("\"");
            string item = str2.Substring(0, index);
            url = item;
            return url;
        }

        public static string getpicsrchg(string ret, string name)
        {
            var index = ret.IndexOf(name);
            ret = ret.Substring(0, index - 1);
            index = ret.LastIndexOf(" ");
            ret = ret.Substring(0, index);
            index = ret.LastIndexOf("href=");
            ret = ret.Substring(index + 5);
            return ret.Trim('"');
        }
        public static string NoHtml(string Htmlstring)
        {
            //删除与数据库相关的词
            Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, "*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);
            return Htmlstring;
        }
        public static string ByteToHexString(byte[] bytes)
        {
            string str = string.Empty; if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++) { str += bytes[i].ToString("X2"); }
            }
            return str;
        }

        #region 数组组合
        public static byte[] ComposeArrays(byte[] Array1, byte[] Array2)
        {
            byte[] Temp = new byte[Array1.Length + Array2.Length];
            Array1.CopyTo(Temp, 0);
            Array2.CopyTo(Temp, Array1.Length);
            return Temp;
        }
        #endregion

        #region 图片转Byte数组
        public static byte[] ImageToBytesFromFilePath(string FilePath)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Image Img = Image.FromFile(FilePath))
                {
                    using (Bitmap Bmp = new Bitmap(Img))
                    {
                        Bmp.Save(ms, Img.RawFormat);
                    }
                }
                return ms.ToArray();
            }
        }
        #endregion


        public static string HttpUploadFile(string url, NameValueCollection nvc)
        {
            string r = "";
            var boundary = "---------------------------7e01ff335133c";
            var boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Headers.Add("Cookie", cookie);

            var rs = wr.GetRequestStream();

            const string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                var formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            var header = string.Format(headerTemplate, "sFileUpload", "", "application/octet-stream");
            var headerbytes = Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            //var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            //var buffer = new byte[4096];
            //var bytesRead = 0;
            //while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            //{
            //    rs.Write(buffer, 0, bytesRead);
            //}
            //fileStream.Close();

            var trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                var stream2 = wresp.GetResponseStream();
                var reader2 = new StreamReader(stream2);
                r = reader2.ReadToEnd();
                //MessageBox.Show(string.Format("Response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error uploading file" + ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;

            }
            return r;
        }

        //public static byte[] BuildMultipartPostData(string Boundary, Dictionary<string, string> HttpPostData, UploadFile FileUploadData)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    // append access token. 
        //    sb.AppendLine("--" + Boundary);
        //    sb.Append(Environment.NewLine);

        //    // append form part. 
        //    if (HttpPostData != null && HttpPostData.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, string> HttpPostDataItem in HttpPostData)
        //        {
        //            sb.AppendLine("--" + Boundary);
        //            sb.AppendLine(string.Format("Content-Disposition: form-data;name=\"{0}\"", HttpPostDataItem.Key));
        //            sb.Append(Environment.NewLine);
        //            sb.AppendLine(HttpPostDataItem.Value);
        //        }
        //    }

        //    // handle file upload. 
        //    if (FileUploadData != null)
        //    {
        //        sb.AppendLine("--" + Boundary);
        //        sb.AppendLine(string.Format("Content-Disposition: form-data;name=\"{0}\"; filename=\"{1}\"", FileUploadData.Name, FileUploadData.Filename));
        //        sb.AppendLine(string.Format("Content-Type: {0}", FileUploadData.ContentType));
        //        sb.Append(Environment.NewLine);
        //    }

        //    MemoryStream ms = new MemoryStream();
        //    BinaryWriter bw = new BinaryWriter(ms);
        //    bw.Write(Encoding.Default.GetBytes(sb.ToString()));
        //    bw.Write(FileUploadData.Data);
        //    bw.Write(Encoding.Default.GetBytes(Environment.NewLine));
        //    bw.Write(Encoding.Default.GetBytes("--" + Boundary));
        //    ms.Flush();
        //    ms.Position = 0;

        //    byte[] result = ms.ToArray();

        //    bw.Close();

        //    return result;
        //}


        //public static string MakeHttpPost(string RequestUrl, Dictionary<string, string> HttpPostData, UploadFile FileUploadData)
        //{
        //    HttpWebRequest request = WebRequest.Create(RequestUrl) as HttpWebRequest;
        //    HttpWebResponse response = null;

        //    StreamReader sr = null;
        //    string boundary = "---------------------------7e01ff335133c";

        //    try
        //    {
        //        request.Method = "POST";
        //        request.ContentType = "multipart/form-data; boundary=" + boundary;

        //        byte[] multipartPostData = BuildMultipartPostData(boundary, HttpPostData, FileUploadData);

        //        BinaryWriter bw = new BinaryWriter(request.GetRequestStream());
        //        bw.Write(multipartPostData);
        //        bw.Close();

        //        response = request.GetResponse() as HttpWebResponse;

        //        sr = new StreamReader(response.GetResponseStream());
        //        string responseData = sr.ReadToEnd();
        //        sr.Close();

        //        response.Close();

        //        return responseData;
        //    }
        //    catch (WebException we)
        //    {
        //        throw new Exception(we.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        private static readonly Encoding encoding = Encoding.UTF8;
        public static string MultipartFormDataPost(string postUrl, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            HttpWebResponse webResponse = PostForm(postUrl, contentType, formData);

            // Process response
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string html = responseReader.ReadToEnd();
            webResponse.Close();
            return html;
        }
        private static HttpWebResponse PostForm(string postUrl, string contentType, byte[] formData)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
            //request.
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586";
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Accept-Encoding:gzip, deflate");
            request.Headers.Add("Accept-Language: zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    //formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        public class UploadFile
        {
            public UploadFile()
            {
                ContentType = "application/octet-stream";
            }
            public string Name { get; set; }
            public string Filename { get; set; }
            public string ContentType { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
