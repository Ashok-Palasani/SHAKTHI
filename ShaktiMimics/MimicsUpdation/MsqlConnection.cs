using System;
using System.Data.SqlClient;

namespace MimicsUpdation 
{
    class MsqlConnection : IDisposable
    {
        //Server
        static String ServerName = @"TCP:192.168.0.3,1433";
        static String username = "sa";
        static String password = "Shakti$piweb";
        static String port = "3306";
        static String DB = "i_facility_shakti";


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
