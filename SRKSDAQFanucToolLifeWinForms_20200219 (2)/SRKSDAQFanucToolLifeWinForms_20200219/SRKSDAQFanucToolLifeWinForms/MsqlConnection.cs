﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRKSDAQFanucToolLifeWinForms
{
    class MsqlConnection : IDisposable
    {
        //static String ServerName = "localhost";
        //static String username = "root";    
        //static String password = "srks4$";
        //static String port = "3306";
        //static String DB = "i_facility";

        static string ServerName = ConfigurationManager.AppSettings["ServerName"];
        static string DB = ConfigurationManager.AppSettings["Database"];
        static string username = ConfigurationManager.AppSettings["user"];
        static string password = ConfigurationManager.AppSettings["password"];
        static string schemaname = ConfigurationManager.AppSettings["dbName"];

        //public MySqlConnection msqlConnection = new MySqlConnection("server = " + ServerName + ";userid = " + username + ";Password = " + password + ";database = " + DB + ";port = " + port + ";persist security info=False;SslMode=None");

        public SqlConnection msqlConnection = new SqlConnection(@"Data Source = " + ServerName + ";User ID = " + username + ";Password = " + password + ";Initial Catalog = " + DB + ";Persist Security Info=True");

        public void open()
        {
            if (msqlConnection.State != System.Data.ConnectionState.Open)
                msqlConnection.Open();
        }

        public void close()
        {
            msqlConnection.Close();
        }

        public void Dispose()
        {
            msqlConnection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}