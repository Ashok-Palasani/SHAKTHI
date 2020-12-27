using System;
using System.Configuration;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;

namespace SRKSDAQFanuc
{
    class MsqlConnection : IDisposable
    {
        //static String ServerName = @"PLM"; //@"SVCCSOEE\CCSOEESQLEXPRESS";//@"DESKTOP-NFACHKF\SQL2012EXP013";//
        //static String username = "srkssa";
        //static String password = "srks4$maini";
        //static String port = "3306";
        //static String DB = "unitworksccs";

        static String ServerName = @"" + ConfigurationManager.AppSettings["ServerName"]; //SIEMENS\SQLEXPRESS
        static String username = ConfigurationManager.AppSettings["username"]; //sa
        //static String password = "srks4$";//server
        static String password = ConfigurationManager.AppSettings["password"];
        //static String port = "3306";
        static String DB = ConfigurationManager.AppSettings["DB"];
        static string Poolsize = ConfigurationManager.AppSettings["Poolsize"];

        static string ispool = ConfigurationManager.AppSettings["Ispool"];
        
        bool ispoolenabled =Convert.ToBoolean( Convert.ToInt32(ispool));
        //  public MySqlConnection msqlConnection = new MySqlConnection("server = " + ServerName + ";userid = " + username + ";Password = " + password + ";database = " + DB + ";port = " + port + ";persist security info=False");
       
        public SqlConnection msqlConnection = new SqlConnection(@"Data Source = " + ServerName + ";User ID = " + username + ";Password = " + password + ";Initial Catalog = " + DB + ";Persist Security Info=True;");
        public MsqlConnection()
        {
            if(ispoolenabled)
                msqlConnection = new SqlConnection(@"Data Source = " + ServerName + ";User ID = " + username + ";Password = " + password + ";Initial Catalog = " + DB + ";Persist Security Info=True;pooling=true;Max Pool Size="+Poolsize+"");

        }

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
