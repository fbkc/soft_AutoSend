using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace AutoSend
{
    public class Tools
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key_64">key值 密钥</param>
        /// <param name="Iv_64">iv值 向量</param>
        /// <returns></returns>
        public static string Encode(string data, string Key_64, string Iv_64)
        {

            string KEY_64 = Key_64;// "VavicApp";

            string IV_64 = Iv_64;// "VavicApp";

            try
            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                int i = cryptoProvider.KeySize;

                MemoryStream ms = new MemoryStream();

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);

                sw.Write(data);

                sw.Flush();

                cst.FlushFinalBlock();

                sw.Flush();

                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

            }

            catch (Exception x)
            {

                return x.Message;

            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key_64">key值 密钥</param>
        /// <param name="Iv_64">iv值 向量</param>
        /// <returns></returns>
        public static string Decode(string data, string Key_64, string Iv_64)
        {

            string KEY_64 = Key_64;// "VavicApp";密钥

            string IV_64 = Iv_64;// "VavicApp"; 向量

            try
            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc;

                byEnc = Convert.FromBase64String(data); //把需要解密的字符串转为8位无符号数组

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(byEnc);

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);

                StreamReader sr = new StreamReader(cst);

                return sr.ReadToEnd();

            }

            catch (Exception x)
            {

                return x.Message;

            }

        }

    }
}
