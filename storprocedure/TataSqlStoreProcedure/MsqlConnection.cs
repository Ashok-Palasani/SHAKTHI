using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TataSqlStoreProcedure
{
    class MsqlConnection : IDisposable
    {
        //static String ServerName = @"TCP:USER-PC\SQLEXPRESS,9021";
        //static String username = "sa";
        //static String password = "srks4$";
        ////static String port = "3306";
        //static String DB = "i_facility_tsal";

        //static String ServerName = @"SIEMENS\SQLEXPRESS";
        //static String username = "sa";
        //static String password = "srks4$taml";
        //static String DB = "i_facility_taml";

        public static String ServerName = @"" + ConfigurationManager.AppSettings["ServerName"]; //SIEMENS\SQLEXPRESS
        public static String username = ConfigurationManager.AppSettings["username"]; //sa
                                                                                      //static String password = "srks4$";//server
        public static String password = ConfigurationManager.AppSettings["password"];
        public static String port = "3306";
        public static String DB = ConfigurationManager.AppSettings["DB"];// i_facility_tsal //Common
        public static String Schema = ConfigurationManager.AppSettings["Schema"];  //Schema Name


        //public MySqlConnection msqlConnection = new MySqlConnection("server = " + ServerName + ";userid = " + username + ";Password = " + password + ";database = " + DB + ";port = " + port + ";persist security info=False");
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
