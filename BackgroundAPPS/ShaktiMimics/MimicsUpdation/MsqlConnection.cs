using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MimicsUpdation 
{
    class MsqlConnection : IDisposable
    {
        //Server
        //static String ServerName = @"TCP:192.168.0.3,1433";
        //static String username = "sa";
        //static String password = "Shakti$piweb";
        //static String port = "3306";
        //static String DB = "i_facility_shakti";

        public static String ServerName = @"" + ConfigurationManager.AppSettings["ServerName"]; //SIEMENS\SQLEXPRESS
        public static String username = ConfigurationManager.AppSettings["username"]; //sa
                                                                                      //static String password = "srks4$";//server
        public static String password = ConfigurationManager.AppSettings["password"];
        public static String port = "3306";
        public static String DB = ConfigurationManager.AppSettings["DB"];// i_facility_tsal //Common
        public static String Schema = ConfigurationManager.AppSettings["Schema"];  //Schema Name
        public static String DatabaseName = ConfigurationManager.AppSettings["DbName"];

        ////Local
        //static String ServerName = @"PAVANKUMARV013\SQL2017EXPDELL";
        //static String username = "sa";
        //static String password = "srks4$";
        //static String DB = "i_facility_shakti";

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

        void IDisposable.Dispose()
        {
        }
    }
}
