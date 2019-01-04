using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using AutoSend;
using System.IO;

namespace yxdain
{
    public class AccessHelper
    {
        private string conn_str = null;

        public AccessHelper()
        {
            string path = Application.StartupPath + "\\" + Myinfo.snameword + @"\" + Myinfo.username + @"\config\" + Myinfo.configname + @"\Database2.dll";
            if (File.Exists(path))
                this.conn_str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "'";
        }

        public DataTable GetDataTableFromDB(string strSql)
        {
            using (OleDbConnection xonn = new OleDbConnection(conn_str))
            {
                xonn.Open();
                using (OleDbCommand cmd = xonn.CreateCommand())
                {
                    cmd.CommandText = strSql;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        public int ExcuteSql(string strSql)
        {
            using (OleDbConnection xonn = new OleDbConnection(conn_str))
            {
                xonn.Open();
                using (OleDbCommand cmd = xonn.CreateCommand())
                {
                    cmd.CommandText = strSql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
