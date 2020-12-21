using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zeiss.IMT.PiWeb.Api.Common.Client;
using Zeiss.IMT.PiWeb.Api.Common.Data;
using Zeiss.IMT.PiWeb.Api.DataService.Rest;

namespace Quality_PiwebApp
{
    public partial class Form1 : Form
    {

        private string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
        private string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
        private string AlaramPath = ConfigurationManager.AppSettings["AlaramPath"];
        private string SpindlePath = ConfigurationManager.AppSettings["SpindlePath"];
        //private string QualityPartPath = ConfigurationManager.AppSettings["QualityPartPath"];
        private string OEEPart_Path = ConfigurationManager.AppSettings["OEEPath"];
        private string Daystartvalue = ConfigurationManager.AppSettings["Daystartvalue"];
        private string Utilization_Path = ConfigurationManager.AppSettings["MachineDataPath"];

        #region methods
        /// <summary>
        /// This methods fetches the service information from both the data service and the raw data service.
        /// The service information contains general information about the services like its version and the 
        /// servers feature set. Fetching the service information can also be used for connection check purposes
        /// since it's guaranteed that the check is fast and does not cause any noticeable server load.
        /// </summary>
        private async Task CheckConnection()
        {
            // Data Service
            try
            {
                IntoFile("Fetching service information from data service.");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                Zeiss.IMT.PiWeb.Api.DataService.Rest.ServiceInformation serviceInformatrion = await _RestDataServiceClient.GetServiceInformation();
                sw.Stop();

                IntoFile("Successfully fetched service information from data service in {0} ms: '{1}'.\r\n" + sw.ElapsedMilliseconds + serviceInformatrion);
            }
            catch (Exception ex)
            {
                IntoFile("Error fetching service information from data service: '{0}'.\r\n" + ex.ToString());
            }

            // Raw Data Service
            //try
            //{
            //    IntoFile("Fetching service information from raw data service.");

            //    var sw = System.Diagnostics.Stopwatch.StartNew();
            //    var rdServiceInformation = await _RestRawDataServiceClient.GetServiceInformation();
            //    sw.Stop();

            //    IntoFile("Successfully fetched service information from data service in {0} ms: '{1}'.\r\n", sw.ElapsedMilliseconds, rdServiceInformation);
            //}
            //catch (Exception ex)
            //{
            //    IntoFile("Error fetching service information from raw data service: '{0}'.\r\n", ex.ToString());
            //}
        }

        /// <summary>
        /// This method fetches the configuration from the data service. The configuration contains definitions about all
        /// attributes that might be present for part, characteristics, measurements, values and catalog. Each attribute
        /// definition defines a data type for the attribute value.
        /// </summary>
        private async Task FetchConfiguration()
        {
            try
            {
                IntoFile("Fetching configuration from data service.");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                _Configuration = await _RestDataServiceClient.GetConfiguration();
                sw.Stop();

                IntoFile("Successfully fetched configuration with {1} attribute definitions from data service in {0} ms.\r\n" + sw.ElapsedMilliseconds + _Configuration.AllAttributes.Length);
            }
            catch (Exception ex)
            {
                IntoFile("Error fetching configuration from data service: '{0}'.\r\n" + ex.ToString());
            }
        }

        /// <summary>
        /// This method fetches the list of catalogs from the data service. Attributes can point to a catalog entry (as described by the
        /// configuration) using the index of the catalog entry. This is the list of all catalogs including their entries. Please keep in
        /// mind that it might be possible that an attribute value points to a non existing catalog entry.
        /// </summary>
        private async Task FetchCatalogs()
        {
            try
            {
                IntoFile("Fetching catalogs from data service.");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                Catalog[] catalogs = await _RestDataServiceClient.GetAllCatalogs();
                _Catalogs = new CatalogCollection(catalogs);
                sw.Stop();

                IntoFile("Successfully fetched {1} catalogs from data service in {0} ms: '{2}'.\r\n" + sw.ElapsedMilliseconds + _Catalogs.Count + string.Join(", ", _Catalogs.Select(c => c.Name)));
            }
            catch (Exception ex)
            {
                IntoFile("Error fetching catalogs from data service: '{0}'.\r\n" + ex.ToString());
            }
        }
        #endregion

        #region members

        private int _DayStart = 0;

        //private DataServiceRestClient _RestDataServiceClient;
        private DataServiceRestClient _RestDataServiceClient;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration;
        private CatalogCollection _Catalogs;
        private InspectionPlanCharacteristic[] _CurrentCharacteristics;
        #endregion
        public Form1()
        {
            InitializeComponent();

            ConfigurationManager.AppSettings["ApplicationPath"] = Application.StartupPath;
            _DayStart = Convert.ToInt32(Daystartvalue);

            ////Utilization
            //try
            //{
            //    PIWEB_Integration_util();

            //}
            //catch (Exception ex)
            //{
            //    IntoFile(ex.ToString());
            //}

            //System.Threading.Thread.Sleep(30 * 1000); //30sec
            //OEE
            try
            {
                PIWEB_Integration();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            //Scheduling    
            try
            {
                //OEE,Alaram,Spindle,Quality
                System.Timers.Timer T3 = new System.Timers.Timer();
                T3.Interval = (1000 * 60 * 10); // 25 Min
                T3.AutoReset = true;
                T3.Enabled = true;
                T3.Elapsed += new System.Timers.ElapsedEventHandler(insertdb1);

                ////Utilization
                //System.Timers.Timer T2 = new System.Timers.Timer();
                //T2.Interval = (1000 * 60 * 10); // 10 Min
                //T2.AutoReset = true;
                //T2.Enabled = true;
                //T2.Elapsed += new System.Timers.ElapsedEventHandler(insertdb);

            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }

        }

        private async void insertdb1(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {   
                PIWEB_Integration();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
           
        }

        private async void insertdb(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //PIWEB_Integration_1();
               // PIWEB_Integration_util();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
           

        }


        public async void PIWEB_Integration()
        {
            string URL = ConfigurationManager.AppSettings["PiWEBURL"];// + "?user=administrator&password=adm!n!strat0r";

            Uri _ServerUri = new Uri(URL);
            _RestDataServiceClient = new DataServiceRestClient(_ServerUri);
            AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
            NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
            //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
            AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
            _RestDataServiceClient.AuthenticationContainer = Authent;


            //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
            await CheckConnection();
            await FetchConfiguration();
            await FetchCatalogs();
            try
            {
                Quality_Piweb objQuality = new Quality_Piweb(_ServerUri, _Configuration, _Catalogs);
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }


            //await PIWEB_Integration_OEE().ConfigureAwait(false);

        }

        public string GetCorrectedDate()
        {
            string correctedDate = DateTime.Now.ToString("yyyy-MM-dd 06:00:00");
            try
            {

                int start = Convert.ToDateTime(correctedDate).Hour;
                if (start <= DateTime.Now.Hour)
                {
                    correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    correctedDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            return correctedDate;

        }

        private PathInformation GetCharPath(String Value, String partPath)
        {
            return PathHelper.String2PathInformation(string.Concat(partPath.Trim('/'), PathHelper.DelimiterString, Value.Trim('/')), "PC");
        }
        public void IntoFile(string Msg)
        {
            try
            {
                string path1 = AppDomain.CurrentDomain.BaseDirectory;
                string appPath = Application.StartupPath + @"\Piweb_Loggerfile_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                using (StreamWriter writer = new StreamWriter(appPath, true)) //true => Append Text
                {
                    writer.WriteLine(System.DateTime.Now + ":  " + Msg);
                }
            }
            catch (Exception)
            {
                //IntoFile(ex.ToString());
                ////messagebox.Show(ex.ToString());
            }

        }

        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath);
            Application.Restart();
        }

    }
}
