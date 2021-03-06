﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace AutoSend
{
    public class json
    {
        /// <summary>
        /// json封装方法
        /// </summary>
        /// <param name="status1"></param>
        /// <param name="msg1"></param>
        /// <param name="data1"></param>
        /// <returns></returns>
        public static string WriteJson(int status1, string msg1, object data1)
        {
            var obj = new { code = status1, msg = msg1, detail = data1 };
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }
    }
}