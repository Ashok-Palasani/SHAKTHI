using ExcelDataReader;
using i_facilitylibrary;
using i_facilitylibrary.DAO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Zeiss.IMT.PiWeb.Api.Common.Client;
using Zeiss.IMT.PiWeb.Api.Common.Data;
using Zeiss.IMT.PiWeb.Api.DataService.Rest;

namespace Shakti_PiwebIntegration
{
    public partial class Form1 : Form
    {
        private string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
        private string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
        private string AlaramPath = ConfigurationManager.AppSettings["AlaramPath"];
        private string SpindlePath = ConfigurationManager.AppSettings["SpindlePath"];
        private string QualityPartPath = ConfigurationManager.AppSettings["QualityPartPath"];
        private string OEEPart_Path = ConfigurationManager.AppSettings["OEEPath"];
        private string Daystartvalue = ConfigurationManager.AppSettings["Daystartvalue"];


        private IConnectionFactory _conn;
        private Dao obj1 = new Dao();
        private Dao1 obj = new Dao1();

        #region members

        private int _DayStart = 0;

        //private DataServiceRestClient _RestDataServiceClient;
        private DataServiceRestClient _RestDataServiceClient;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration;
        private CatalogCollection _Catalogs;
        private InspectionPlanCharacteristic[] _CurrentCharacteristics;


        private InspectionPlanPart _CurrentPart;
        //private InspectionPlanCharacteristic[] _CurrentCharacteristics;
        private SimpleMeasurement _dataMeasurement;

        private InspectionPlanPart _CurrentPartOEE;
        private InspectionPlanCharacteristic[] _CurrentCharacteristicsOEE;
        private SimpleMeasurement _dataMeasurementOEE;
        private DataServiceRestClient _RestDataServiceClient_OEE;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_OEE;


        private DataServiceRestClient _RestDataServiceClient_Util;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Util;
        private InspectionPlanCharacteristic[] _CurrentCharacteristics_Util;
        private SimpleMeasurement _dataMeasurement_Util;
        private InspectionPlanPart _CurrentPart_Util;


        private DataServiceRestClient _RestDataServiceClientAlarm;
        private InspectionPlanPart _CurrentPartAlarm;
        private InspectionPlanCharacteristic[] _CurrentCharacteristicsAlarm;
        private SimpleMeasurement _dataMeasurementAlarm;
        private SimpleMeasurement[] _dataMeasurementAlarmDel;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Alarm;



        private DataServiceRestClient _RestDataServiceClientSpindle;
        private InspectionPlanPart _CurrentPartSpindle;
        private InspectionPlanCharacteristic[] _CurrentCharacteristicsSpindle;
        private SimpleMeasurement[] _dataMeasurementSpindleDel;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Spindle;


        private DataServiceRestClient _RestDataServiceClientQuality;
        private InspectionPlanPart _CurrentPartQuality;
        private InspectionPlanCharacteristic[] _CurrentCharacteristicsQuality;
        private SimpleMeasurement[] _dataMeasurementQuality;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Quality;
        //private SimpleMeasurement _dataMeasurement_Periodic;
        #endregion


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

        public Form1()
        {
            InitializeComponent();

            try
            {
                _DayStart = Convert.ToInt32(Daystartvalue);
                //_DayStart = 1;

                PIWEB_Integration();
               // OEE_Piweb();

            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
                //Application.Restart();
                Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }

            try
            {
                //System.Timers.Timer T2 = new System.Timers.Timer();
                //T2.Interval = (1000 * 60 * 1); // 5 Min
                //T2.AutoReset = true;
                //T2.Enabled = true;
                //T2.Elapsed += new System.Timers.ElapsedEventHandler(insertdb);

                System.Timers.Timer T3 = new System.Timers.Timer();
                T3.Interval = (1000 * 60 * 30); // 5 Min
                T3.AutoReset = true;
                T3.Enabled = true;
                T3.Elapsed += new System.Timers.ElapsedEventHandler(insertdb1);

            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
                //Application.Restart();
                Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
            finally
            {
                // This code always executed even if application crashes.
                IntoFile(System.DateTime.Now.ToString() + " Locking Timer Issue");
                //Process.Start(Application.ExecutablePath);
                //Environment.Exit(0);
            }
        }

        private async void insertdb(object sender, System.Timers.ElapsedEventArgs e)
        {
            string URL = ConfigurationManager.AppSettings["PiWEBURL"];// + "?user=administrator&password=adm!n!strat0r";
            string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
            string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
            Uri _ServerUri = new Uri(URL);
            _RestDataServiceClient = new DataServiceRestClient(_ServerUri);
            AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
            NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
            //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
            AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
            _RestDataServiceClient.AuthenticationContainer = Authent;


            await CheckConnection();
            await FetchConfiguration();
            await FetchCatalogs();
            string Machinedata_PartPath = ConfigurationManager.AppSettings["MachineDataPath"];
            string OEEPart_Path = ConfigurationManager.AppSettings["OEEPath"];
            //InitializeControlUtil(_ServerUri, _Configuration, _Catalogs, Machinedata_PartPath);

            //InitializeControlOEE(_ServerUri, _Configuration, _Catalogs,OEEPart_Path);
            //InitializeControlLoss(_ServerUri, _Configuration, _Catalogs);
            try
            {
              //  InsertIntoPiwebAlarm();
                //InsertIntoPiwebSpindle();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            try
            {
                //GetModeHour();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        private async void insertdb1(object sender, System.Timers.ElapsedEventArgs e)
        {
            string URL = ConfigurationManager.AppSettings["PiWEBURL"];// + "?user=administrator&password=adm!n!strat0r";
            string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
            string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
            Uri _ServerUri = new Uri(URL);
            _RestDataServiceClient = new DataServiceRestClient(_ServerUri);
            AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
            NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
            //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
            AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
            _RestDataServiceClient.AuthenticationContainer = Authent;


            await CheckConnection();
            await FetchConfiguration();
            await FetchCatalogs();
            string Machinedata_PartPath = ConfigurationManager.AppSettings["MachineDataPath"];
            string OEEPart_Path = ConfigurationManager.AppSettings["OEEPath"];
            try
            {
                InitializeControlQuality(_ServerUri, _Configuration, _Catalogs);
                //InitializeControlOEE(_ServerUri, _Configuration, _Catalogs);

                GetMeasurements_Qualty(QualityPartPath);
                System.Threading.Thread.Sleep(2000);
                OEE_Piweb();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            //InitializeControlOEE(_ServerUri, _Configuration, _Catalogs,OEEPart_Path);
            //InitializeControlLoss(_ServerUri, _Configuration, _Catalogs);

        }
        private void GetModeHour()
        {

            string Machinedata_PartPath = ConfigurationManager.AppSettings["MachineDataPath"];
            try
            {

                string correctedDate_S = GetCorrectedDate();
                DateTime CorrectedDate_T = Convert.ToDateTime(correctedDate_S);
                List<tblmachinedetail> machinedetails = new List<tblmachinedetail>();
                using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                {
                    machinedetails = db1.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).ToList();
                }
                int Insert = 0;


                DateTime correctedDate = DateTime.Now.Date;
                i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1();

                if (correctedDate != CorrectedDate_T || _DayStart == 1)
                {
                    Insert = 1;
                    _DayStart = 0;
                }
                foreach (tblmachinedetail machine in machinedetails)
                {

                    int machineid = machine.MachineID;
                    //IsmachineExist = await ismachineavailable(machineid, Machinedata_PartPath, correctedDate.ToString("yyyy-MM-dd"));

                    tbllivemode GetCDateMode = db.tbllivemodes.Where(m => m.MachineID == machineid).OrderByDescending(m => m.ModeID).FirstOrDefault();
                    if (GetCDateMode != null)
                    {
                        CorrectedDate_T = GetCDateMode.CorrectedDate;
                    }


                    int GetHour = System.DateTime.Now.Hour;

                    //DateTime StartModeTime = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd " + GetHour + ":00:00"));
                    decimal OperatingTime = 0;
                    decimal LossTime = 0;
                    decimal MinorLossTime = 0;
                    decimal MntTime = 0;
                    decimal SetupTime = 0;
                    decimal SetupMinorTime = 0;
                    decimal PowerOffTime = 0;
                    decimal PowerONTime = 0;
                    decimal Utilization = 0;
                    string Present_ModeColor = "";

                    List<tbllivemode> GetModeDurations = db.tbllivemodes.Where(m => m.MachineID == machineid && m.CorrectedDate == correctedDate.Date && m.IsCompleted == 1 && m.ModeTypeEnd == 1).ToList();
                    foreach (tbllivemode ModeRow in GetModeDurations)
                    {
                        CorrectedDate_T = ModeRow.CorrectedDate;
                        if (ModeRow.ModeType == "PROD")
                        {
                            OperatingTime += (decimal)(ModeRow.DurationInSec / 60.00);
                        }
                        else if (ModeRow.ModeType == "IDLE" && ModeRow.DurationInSec > 600)
                        {
                            LossTime += (decimal)(ModeRow.DurationInSec / 60.00);
                            decimal LossDuration = (decimal)(ModeRow.DurationInSec / 60.00);
                            //if (ModeRow.LossCodeID != null)
                            // insertProdlosses(ProdRow.HMIID, (int)ModeRow.LossCodeID, LossDuration, CorrectedDate, MachineID);
                        }
                        else if (ModeRow.ModeType == "IDLE" && ModeRow.DurationInSec < 600)
                        {
                            MinorLossTime += (decimal)(ModeRow.DurationInSec / 60.00);
                        }
                        else if (ModeRow.ModeType == "MNT")
                        {
                            MntTime += (decimal)(ModeRow.DurationInSec / 60.00);
                        }
                        else if (ModeRow.ModeType == "POWEROFF")
                        {
                            PowerOffTime += (decimal)(ModeRow.DurationInSec / 60.00);
                        }
                        else if (ModeRow.ModeType == "SETUP")
                        {
                            try
                            {
                                SetupTime += (decimal)Convert.ToDateTime(ModeRow.LossCodeEnteredTime).Subtract(Convert.ToDateTime(ModeRow.StartTime)).TotalMinutes;
                                SetupMinorTime += (decimal)(db.tblSetupMaints.Where(m => m.ModeID == ModeRow.ModeID).Select(m => m.MinorLossTime).First() / 60.00);
                            }
                            catch { }
                        }
                        else if (ModeRow.ModeType == "POWERON")
                        {
                            PowerONTime += (decimal)(ModeRow.DurationInSec / 60.00);
                        }
                    }

                    List<tbllivemode> GetModeDurationsRunning = db.tbllivemodes.Where(m => m.MachineID == machineid && m.CorrectedDate == correctedDate.Date && m.IsCompleted == 0).ToList();
                    foreach (tbllivemode ModeRow in GetModeDurationsRunning)
                    {
                        String ColorCode = ModeRow.ColorCode;
                        Present_ModeColor = ColorCode;
                        DateTime StartTime = (DateTime)ModeRow.StartTime;
                        decimal Duration = (decimal)System.DateTime.Now.Subtract(StartTime).TotalMinutes;
                        if (ColorCode == "YELLOW")
                        {
                            LossTime += Duration;
                        }
                        else if (ColorCode == "GREEN")
                        {
                            OperatingTime += Duration;
                        }
                        else if (ColorCode == "RED")
                        {
                            MntTime += Duration;
                        }
                        else if (ColorCode == "BLUE")
                        {
                            PowerOffTime += Duration;
                        }
                    }

                    if (Insert == 1)
                    {

                        CreateMeasurementsUtil(Machinedata_PartPath, OperatingTime.ToString(), LossTime.ToString(), MntTime.ToString(), PowerOffTime.ToString(), correctedDate.ToString("MM/dd/yyyy"), MinorLossTime.ToString(), machine.MachineDisplayName, machineid, Present_ModeColor);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        UpdateMeasurementsUtil(Machinedata_PartPath, OperatingTime.ToString(), LossTime.ToString(), MntTime.ToString(), PowerOffTime.ToString(), correctedDate.ToString("MM/dd/yyyy"), MinorLossTime.ToString(), machine.MachineDisplayName, machineid, Present_ModeColor);
                    }

                    decimal IdleTime = LossTime + MinorLossTime;
                    decimal BDTime = MntTime;
                    int TotalTime = Convert.ToInt32(PowerONTime) + Convert.ToInt32(OperatingTime) + Convert.ToInt32(IdleTime) + Convert.ToInt32(BDTime);
                    if (TotalTime == 0)
                    {
                        TotalTime = 1;
                    }
                    Utilization = Convert.ToInt32(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(OperatingTime) / Convert.ToDouble(TotalTime)) * 100));





                    // OEE
                    //decimal DayOEEPercent = 0;
                    //int PerformanceFactor = 0;
                    //decimal Quality = 0;
                    //List<tblworkorderentry> prodplanchine = db.tblworkorderentries.Where(m => m.MachineID == 1 && m.CorrectedDate == correctedDate.Date && (m.IsFinished == 1 || m.IsHold == 1)).ToList();
                    //foreach (tblworkorderentry ProdRow in prodplanchine)
                    //{
                    //    int TotlaQty = ProdRow.Total_Qty;
                    //    if (TotlaQty == 0)
                    //    {
                    //        TotlaQty = 1;
                    //    }

                    //    decimal GetOptime = OperatingTime;
                    //    if (GetOptime == 0)
                    //    {
                    //        GetOptime = 1;
                    //    }
                    //    decimal IdealCycleTimeVal = 2;
                    //    tblpart IdealCycTime = db.tblparts.Where(m => m.FGCode == ProdRow.FGCode && m.OperationNo == ProdRow.OperationNo).FirstOrDefault();
                    //    if (IdealCycTime != null)
                    //    {
                    //        IdealCycleTimeVal = IdealCycTime.IdealCycleTime;
                    //    }

                    //    double TotalTime1 = Convert.ToDouble(PowerONTime) + Convert.ToDouble(OperatingTime) + Convert.ToDouble(IdleTime) + Convert.ToDouble(BDTime);
                    //    //decimal UtilPercent = (decimal)Math.Round((double)OperatingTime / TotalTime1 * 100, 2);
                    //    Quality = (decimal)Math.Round((double)ProdRow.Yield_Qty / TotlaQty * 100, 2);
                    //    decimal Performance = (decimal)Math.Round((double)IdealCycleTimeVal * ProdRow.Total_Qty / (double)GetOptime * 100, 2);
                    //    PerformanceFactor = (int)IdealCycleTimeVal * ProdRow.Total_Qty;
                    //    if (PerformanceFactor == 0)
                    //    {
                    //        PerformanceFactor = 100;
                    //    }
                    //    if (Quality == 0)
                    //    {
                    //        Quality = 100;
                    //    }
                    //}
                    //DayOEEPercent = (decimal)Math.Round((double)(Utilization) * PerformanceFactor * (double)(Quality), 2);
                    //if (DayOEEPercent == 0)
                    //{
                    //    if (PerformanceFactor == 0)
                    //    {
                    //        PerformanceFactor = 100;
                    //    }
                    //    if (Quality == 0)
                    //    {
                    //        Quality = 100;
                    //    }
                    //    DayOEEPercent = (decimal)Math.Round((double)(Utilization) * PerformanceFactor * (double)(Quality), 2);
                    //}
                    //if (Insert == 1)
                    //{
                    //    //Task.Delay(5000);
                    //    // CreateMeasurementsOEE(OEEPart_Path, Utilization.ToString(), PerformanceFactor.ToString(), Quality.ToString(), DayOEEPercent.ToString(), correctedDate.ToString("MM/dd/yyyy"));
                    //}
                    //else
                    //{
                    //    // CreateMeasurementsOEE(OEEPart_Path, Utilization.ToString(), PerformanceFactor.ToString(), Quality.ToString(), DayOEEPercent.ToString(), correctedDate.ToString("MM/dd/yyyy"));
                    //}
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        private void OEE_Piweb()
        {

            OEECalculations oeecal = new OEECalculations();
            string correctedDate_S = GetCorrectedDate();
            DateTime CorrectedDate_T = Convert.ToDateTime(correctedDate_S);
            List<tblmachinedetail> machinedetails = new List<tblmachinedetail>();
            using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
            {
                machinedetails = db1.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).ToList(); // && m.MachineID==20
            }

            int Insert = 0;

            DateTime correctedDate = DateTime.Now.Date;
            i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1();

            if (correctedDate != CorrectedDate_T || _DayStart == 1)
            {
                Insert = 1;
                _DayStart = 0;
            }

            List<PiwebOEE> oeedata = new List<PiwebOEE>();
            foreach (tblmachinedetail machine in machinedetails)
            {
                double Availability = 0;
                double Performance = 0;
                double Quality = 0;
                double OEE = 0;
                int MachineID = machine.MachineID;
                _conn = new ConnectionFactory();
                obj1 = new Dao(_conn);
                obj = new Dao1(_conn);
                //Dao obj1 = new Dao();
                //Dao1 obj = new Dao1();
                PiwebOEE oeemachine = new PiwebOEE();
                try
                {
                    obj.deletetbloeedashboardvariablestodaysDetails2(machine.IPAddress.ToString(),MachineID);
                    //db.tbloeedashboardvariablestodays.RemoveRange(db.tbloeedashboardvariablestodays.Where(x => x.IPAddress == machine.IPAddress.ToString()));
                    //db.SaveChanges();
                }
                catch (Exception e)
                {
                    IntoFile(e.ToString());
                }

                try
                {
                    obj.deletetbloeedashboardfinalvariablesDetails2(machine.IPAddress.ToString(), MachineID);
                    //dboee.tbloeedashboardfinalvariables.RemoveRange(dboee.tbloeedashboardfinalvariables.Where(x => x.IPAddress == ipAddress));
                    //dboee.SaveChanges();
                }
                catch (Exception e)
                {
                    IntoFile(e.ToString());
                }



                oeecal.CalculateOEEToday(correctedDate_S, MachineID, machine.IPAddress);
                string summarizeAs = "WorkCentre";
                string TimeType = "Today";
                oeecal.CalculateSummarizedOEEWC(CorrectedDate_T, correctedDate, Convert.ToInt32(machine.MachineID), TimeType, summarizeAs, machine.IPAddress);


                i_facilitylibrary.DAL.tbloeedashboardfinalvariable OEEDataSummarizedToView = obj.GettbloeeDet(MachineID, machine.IPAddress, CorrectedDate_T, correctedDate);

                if (OEEDataSummarizedToView != null)
                {
                    oeemachine.MachineID = MachineID;
                    

                    // ViewBag.OverAllMax = db.tbloeedashboardfinalvariables.Where(m => m.IsDeleted == 0 && m.WCID == WorkCenterID && m.IPAddress == ipAddress && m.StartDate == frmDate && m.EndDate == toDate).Max(m => m.Loss1Value);
                    OEE = Convert.ToDouble(OEEDataSummarizedToView.OEE);
                    Availability = Convert.ToDouble(OEEDataSummarizedToView.Availability);
                    if (OEEDataSummarizedToView.Availability > 0)
                    {
                        Performance = Convert.ToDouble(OEEDataSummarizedToView.Performance);
                        Quality = Convert.ToDouble(OEEDataSummarizedToView.Quality);
                        if (Quality == 0)
                        {
                            Quality = 100;
                        }

                        if (OEE == 0)
                        {
                            OEE = (Availability / 100 * (Quality / 100) * (Performance / 100)) * 100;
                        }
                      
                    }
                    if (Quality == 0)
                    {
                        Quality = 100;
                    }

                    oeemachine.Availability = Convert.ToString(Availability);

                    oeemachine.Quality = Convert.ToString(Quality);

                    oeemachine.Performance = Convert.ToString(Performance);
                    oeemachine.OEE = Convert.ToString(OEE);

                    oeedata.Add(oeemachine);
                    if (Insert == 1)
                    {
                        System.Threading.Thread.Sleep(2000);
                        CreateMeasurementsOEE(OEEPart_Path, Availability.ToString(), Performance.ToString(), Quality.ToString(), OEE.ToString(), correctedDate.ToString("MM/dd/yyyy"), MachineID, machine.MachineName);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(2000);
                        UpdateMeasurementsOEE(OEEPart_Path, Availability.ToString(), Performance.ToString(), Quality.ToString(), OEE.ToString(), correctedDate.ToString("MM/dd/yyyy"), MachineID, machine.MachineName);
                    }

                }

               

            }

            if (oeedata.Count > 0)
            {
                DataTable dt = ToDataTable(oeedata);
                //ToEXCEL(dt, "Sampel");
                ToCSV(dt, @"C:\OEE-CSV\OEESample_" + DateTime.Now.ToString("yyyyyMMddHHmmss") + ".csv");
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
            
                InitializeControllers(_ServerUri, _Configuration, _Catalogs);
                //await ismachineavailable(0, Machinedata_PartPath,DateTime.Now.ToString("yyyy-MM-dd"));
                //System.Threading.Thread.Sleep(1000);
                //GetModeHour();
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        private  void InitializeControllers(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            string Machinedata_PartPath = ConfigurationManager.AppSettings["MachineDataPath"];
            string OEEPart_Path = ConfigurationManager.AppSettings["OEEPath"];
          //  InitializeControlUtil(serverUri, _Configuration, _Catalogs, Machinedata_PartPath);

            InitializeControlQuality(serverUri, _Configuration, _Catalogs);
            //InitializeControlAlarm(serverUri, _Configuration, _Catalogs);
            // InitializeControlSpindle(serverUri, _Configuration, _Catalogs);
            InitializeControlOEE(serverUri, _Configuration, _Catalogs);


            //try
            //{
            //    GetMeasurements_Qualty(QualityPartPath);
            //}
            //catch (Exception ex)
            //{
            //    IntoFile(ex.ToString());
            //}

            //InitializeControlOEE(serverUri, _Configuration, _Catalogs, OEEPart_Path);
            //try
            //{
            //    System.Threading.Thread.Sleep(1000*60*1);
            //    OEE_Piweb();
            //}
            //catch (Exception ex)
            //{
            //    IntoFile(ex.ToString());
            //}


        }

        public async void InitializeControlUtil(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs, string machinpartpath)
        {
            try
            {
                _Configuration = configuration;
                _Catalogs = catalogs;
                _RestDataServiceClient_Util = new DataServiceRestClient(serverUri);
                AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
                NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
                //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
                AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
                _RestDataServiceClient_Util.AuthenticationContainer = Authent;

                PathInformation partPath = PathHelper.String2PartPathInformation(machinpartpath);
                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient_Util.GetParts(partPath);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                if (partsArray.Any())
                {
                    _CurrentPart_Util = partsArray.First();
                }

                IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClient_Util.GetCharacteristics(partPath);
                InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
                if (charsArray.Any())
                {
                    _CurrentCharacteristics_Util = charsArray;
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }


            //AddPartUtil(machinpartpath, "OpTime", "IdleTime", "MNTTime", "POffTime", "MeasDate", "MeasHour");
        }

        private async void AddPartUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MeasHour)
        {
            IntoFile("Creating inspection plan items");

            Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
            PathInformation char1Path = GetCharPath(OpTime, PartPath);
            PathInformation char2Path = GetCharPath(IdleTime, PartPath);
            PathInformation char3Path = GetCharPath(MNTTime, PartPath);
            PathInformation char4Path = GetCharPath(POffTime, PartPath);
            PathInformation char5Path = GetCharPath(MeasDate, PartPath);
            PathInformation char6Path = GetCharPath(MeasHour, PartPath);
            try
            {
                InspectionPlanPart part = new InspectionPlanPart { Path = partPath, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char1 = new InspectionPlanCharacteristic { Path = char1Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char2 = new InspectionPlanCharacteristic { Path = char2Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char3 = new InspectionPlanCharacteristic { Path = char3Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char4 = new InspectionPlanCharacteristic { Path = char4Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char5 = new InspectionPlanCharacteristic { Path = char5Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char6 = new InspectionPlanCharacteristic { Path = char6Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic[] characteristics = new[] { char1, char2, char3, char4, char5, char6 };

                await _RestDataServiceClient.CreateParts(new[] { part });
                await _RestDataServiceClient.CreateCharacteristics(characteristics);

                _CurrentPart = part;
                _CurrentCharacteristics = characteristics;

                sw.Stop();
                IntoFile("Succesfully created inspection plan items in {0} ms\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error creating part '{0}': '{1}'.\r\n" + partPath , ex.ToString()));
                _CurrentPart = null;
                _CurrentCharacteristics = null;
            }
        }

        private async void CreateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MeasHour)
        {
            double val1, val2, val3, val4, val5, val6;

            if (!Double.TryParse(OpTime, out val1) || !Double.TryParse(IdleTime, out val2) ||
                !Double.TryParse(MNTTime, out val3) || !Double.TryParse(POffTime, out val4) || !Double.TryParse(MeasDate, out val5) ||
                !Double.TryParse(MeasHour, out val6) || _CurrentPart == null || _CurrentCharacteristics == null || _CurrentCharacteristics.Length != 3)
            {
                IntoFile("Creating measurement failed due to badly formatted values!\r\n");
                return;
            }

            try
            {
                IntoFile("Creating a measurement and Its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));

                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurement = measArray.Last();
                }

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val1 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics[1].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val2 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics[2].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val3 )
                        }
                    },
                    PartUuid = _CurrentPart.Uuid
                };
                await _RestDataServiceClient.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }

        //private async void CreateMeasurements_Periodic(String PartPath, String Pressure_regulator, String ShiftVal)
        //{
        //    double val1;


        //    //if (!Double.TryParse(Pressure_regulator, out val1) || !Double.TryParse(LH_air_pressure, out val2) ||
        //    //    !Double.TryParse(RH_air_pressure, out val3) || !Double.TryParse(SwithceOperation, out val4) || !Double.TryParse(abnormal, out val5) ||
        //    //    !Double.TryParse(LH_RH_GreaseDispenses, out val6) || _CurrentPartMNT == null || _CurrentCharacteristicsMNT == null || _CurrentCharacteristicsMNT.Length != 13)
        //    //{
        //    //    IntoFile("Creating measurement failed due to badly formatted values!\r\n");

        //    //}

        //    try
        //    {
        //        IntoFile("Creating a measurement periodic and Its values");

        //        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        //        List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

        //        CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
        //        Random rdm = new Random();
        //        if (inspectorDef != null)
        //        {
        //            CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
        //            attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
        //        }
        //        PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
        //        SimpleMeasurement[] meas = await _RestDataServiceMachine.GetMeasurements(partPath);
        //        SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
        //        if (measArray.Any())
        //        {
        //            _dataMeasurement = measArray.First();
        //        }
        //        val1 = 0;
        //        IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient.GetParts(partPath);
        //        InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();

        //        if (partsArray.Any())
        //        {
        //            _CurrentPartPeriodic = partsArray.First();
        //        }

        //        IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClient.GetCharacteristics(partPath);
        //        InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
        //        if (charsArray.Any())
        //        {
        //            _CurrentCharacteristicsMachine = charsArray;

        //        }

        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.MeasurementStatus, 0));
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(61, 0));
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(62, DateTime.Now));
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(63, 0));
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(850, ShiftVal));


        //        DataMeasurement measurement = new DataMeasurement
        //        {
        //            Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
        //            Attributes = attributes.ToArray(),

        //            Characteristics = new[]
        //            {
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicsMachine[0].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val1 )
        //                },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[1].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[2].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[3].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[4].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[5].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                     new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[6].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                      new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[7].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                           new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[8].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                   new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[9].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[10].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },

        //                   new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicsMachine[11].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val1 )
        //                },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[12].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[13].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[14].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[15].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[16].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                     new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[17].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                      new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[18].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                           new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[19].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                   new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[20].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[21].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },

        //                    new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicsMachine[22].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val1 )
        //                },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[23].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[24].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[25].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[26].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[27].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                     new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[28].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                      new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[29].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                           new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[30].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                   new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[31].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[32].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },

        //                   new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicsMachine[33].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val1 )
        //                },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[34].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    },
        //                    new DataCharacteristic
        //                    {
        //                        Uuid = _CurrentCharacteristicsMachine[35].Uuid,
        //                        Timestamp = DateTime.Now,
        //                        Value = new DataValue( val1 )
        //                    }
        //                    //new DataCharacteristic
        //                //    {
        //                //        Uuid = _CurrentCharacteristicsMNT[36].Uuid,
        //                //        Timestamp = DateTime.Now,
        //                //        Value = new DataValue( val1 )
        //                //    },
        //                //    new DataCharacteristic
        //                //    {
        //                //        Uuid = _CurrentCharacteristicsMNT[37].Uuid,
        //                //        Timestamp = DateTime.Now,
        //                //        Value = new DataValue( val1 )
        //                //    },
        //                //    new DataCharacteristic
        //                //    {
        //                //        Uuid = _CurrentCharacteristicsMNT[38].Uuid,
        //                //        Timestamp = DateTime.Now,
        //                //        Value = new DataValue( val1 )
        //                //    },
        //                //     new DataCharacteristic
        //                //    {
        //                //        Uuid = _CurrentCharacteristicsMNT[39].Uuid,
        //                //        Timestamp = DateTime.Now,
        //                //        Value = new DataValue( val1 )
        //                //    },
        //                //      new DataCharacteristic
        //                //    {
        //                //        Uuid = _CurrentCharacteristicsMNT[40].Uuid,
        //                //        Timestamp = DateTime.Now,
        //                //        Value = new DataValue( val1 )
        //                //    }
        //        },
        //            PartUuid = _CurrentPartPeriodic.Uuid
        //        };
        //        await _RestDataServiceClient.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

        //        sw.Stop();
        //        IntoFile("Successfully created a periodic measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(String.Format("Error creating measurement: '{0}'.\r\n", ex.ToString()));
        //    }
        //}

        private async void UpdateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MeasHour)
        {
            double val1, val2, val3, val4, val5, val6;

            if (!Double.TryParse(OpTime, out val1) || !Double.TryParse(IdleTime, out val2) ||
                !Double.TryParse(MNTTime, out val3) || !Double.TryParse(POffTime, out val4) || !Double.TryParse(MeasDate, out val5) ||
                !Double.TryParse(MeasHour, out val6) || _CurrentPart == null || _CurrentCharacteristics == null || _CurrentCharacteristics.Length != 3)
            {
                IntoFile("Updating measurement failed due to badly formatted values!\r\n");
                return;
            }

            try
            {
                IntoFile("Updating a measurement and its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurement = measArray.Last();
                }

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = _dataMeasurement.Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val1 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics[1].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val2 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics[2].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val3 )
                        }
                    },
                    PartUuid = _CurrentPart.Uuid
                };
                await _RestDataServiceClient.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

                sw.Stop();
                IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Updating measurement: '{0}'.\r\n" , ex.ToString()));
            }
        }


        private async void UpdateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MinorLoss, string MachineName, int Machineid, string ColorCode)
        {

            //if (!Double.TryParse(OpTime, out val1) || !Double.TryParse(IdleTime, out val2) ||
            //    !Double.TryParse(MNTTime, out val3) || !Double.TryParse(POffTime, out val4) || !Double.TryParse(MeasDate, out val5) ||
            //    !Double.TryParse(MeasHour, out val6) || _CurrentPart == null || _CurrentCharacteristics_Util == null || _CurrentCharacteristics_Util.Length != 3)
            //{
            //    IntoFile("Updating measurement failed due to badly formatted values!\r\n");
            //    return;
            //}

            try
            {
                IntoFile("Updating a measurement of Utilization of machineid:" + Machineid + " and its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient_Util.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurement_Util = measArray.Last();
                }


                int k9 = _dataMeasurement_Util.Attributes.ToList().IndexOf(_dataMeasurement_Util.Attributes.ToList().First(i => i.ToString().Contains("K9")));



                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute[] data1 = _dataMeasurement_Util.Attributes.ToArray();
                if (Convert.ToInt32(data1[k9].Value) == Machineid)
                {
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, Machineid));
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4056, ColorCode));
                    DataMeasurement measurement = new DataMeasurement
                    {
                        Uuid = _dataMeasurement_Util.Uuid,//.NewGuid(),
                        Attributes = attributes.ToArray(),
                        Characteristics = new[]
                        {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics_Util[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble(MNTTime) )
                        },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[1].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[2].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[3].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(MinorLoss) )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[4].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(IdleTime) )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[5].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(OpTime) )
                            },
                             new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[6].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(POffTime) )
                            },
                              new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[7].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            }

                },
                        PartUuid = _CurrentPart_Util.Uuid
                    };
                    await _RestDataServiceClient.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

                    sw.Stop();
                    IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
                }

            }
            catch (Exception ex)
            {
                IntoFile("utilization Error Updating measurement: '{0}'.\r\n" + ex.ToString());
            }
        }

        private async void CreateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MinorLoss, string MachineName, int Machineid, string ColorCode)
        {

            //if (!Double.TryParse(OpTime, out val1) || !Double.TryParse(IdleTime, out val2) ||
            //    !Double.TryParse(MNTTime, out val3) || !Double.TryParse(POffTime, out val4) || !Double.TryParse(MeasDate, out val5) ||
            //    !Double.TryParse(MeasHour, out val6) || _CurrentPart == null || _CurrentCharacteristics_Util == null || _CurrentCharacteristics_Util.Length != 3)
            //{
            //    IntoFile("Creating measurement failed due to badly formatted values!\r\n");
            //    return;
            //}

            try
            {
                IntoFile("Creating a utilization measurement and Its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));

                }

                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                //var meas = await _RestDataServiceClient_Util.GetMeasurements(partPath);
                //System.Threading.Thread.Sleep(1000);
                //var measArray = meas as DataMeasurement[] ?? meas.ToArray();
                //if (measArray.Any())
                //    _dataMeasurement_Util = measArray.Last();


                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, Machineid));
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4056, ColorCode));
                //attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4, Convert.ToDateTime( MeasDate).AddHours(5).AddMinutes(30)));
                //var parts = await _RestDataServiceClient_Util.GetParts(partPath);
                //var partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                //if (partsArray.Any())
                //    _CurrentPart_Util = partsArray.First();
                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),

                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics_Util[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble(MNTTime) )
                        },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[1].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[2].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[3].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(MinorLoss) )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[4].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(IdleTime) )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[5].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(OpTime) )
                            },
                             new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[6].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(POffTime) )
                            },
                              new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[7].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            }

                },
                    PartUuid = _CurrentPart_Util.Uuid
                };
                await _RestDataServiceClient_Util.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });


                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format(" UTilization Error creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }


        public async void InitializeControlOEE(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            _Configuration_OEE = configuration;
            _Catalogs = catalogs;
            _RestDataServiceClient_OEE = new DataServiceRestClient(serverUri);
            try
            {

                AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
                NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
                //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
                AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
                _RestDataServiceClient_OEE.AuthenticationContainer = Authent;
                PathInformation partPath_Oee = PathHelper.String2PartPathInformation(OEEPart_Path);
                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient_OEE.GetParts(partPath_Oee);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                if (partsArray.Any())
                {
                    _CurrentPartOEE = partsArray.First();
                }

                IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClient_OEE.GetCharacteristics(partPath_Oee);
                InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
                if (charsArray.Any())
                {
                    _CurrentCharacteristicsOEE = charsArray;
                }

                try
                {
                    System.Threading.Thread.Sleep(1000 * 60 * 1);
                    OEE_Piweb();
                }
                catch (Exception ex)
                {
                    IntoFile(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }

            //  AddPartOEE("MC1OEE", "AF", "PF", "QF", "OEE", "MeasDate");
        }

        private async void AddPartOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate)
        {
            IntoFile("Creating inspection plan items");

            Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
            PathInformation char1Path = GetCharPath(AF, PartPath);
            PathInformation char2Path = GetCharPath(PF, PartPath);
            PathInformation char3Path = GetCharPath(QF, PartPath);
            PathInformation char4Path = GetCharPath(OEE, PartPath);
            PathInformation char5Path = GetCharPath(MeasDate, PartPath);
            try
            {
                InspectionPlanPart part = new InspectionPlanPart { Path = partPath, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char1 = new InspectionPlanCharacteristic { Path = char1Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char2 = new InspectionPlanCharacteristic { Path = char2Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char3 = new InspectionPlanCharacteristic { Path = char3Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char4 = new InspectionPlanCharacteristic { Path = char4Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char5 = new InspectionPlanCharacteristic { Path = char5Path, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic[] characteristics = new[] { char1, char2, char3, char4, char5 };

                await _RestDataServiceClient.CreateParts(new[] { part });
                await _RestDataServiceClient.CreateCharacteristics(characteristics);

                _CurrentPartOEE = part;
                _CurrentCharacteristicsOEE = characteristics;

                sw.Stop();
                IntoFile("Succesfully created inspection plan items in {0} ms\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error creating part '{0}': '{1}'.\r\n" + partPath , ex.ToString()));
                _CurrentPartOEE = null;
                _CurrentCharacteristicsOEE = null;
            }
        }
        private async void CreateMeasurementsOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate)
        {
            double val1, val2, val3, val4, val5;

            if (!Double.TryParse(AF, out val1) || !Double.TryParse(PF, out val2) ||
                !Double.TryParse(QF, out val3) || !Double.TryParse(OEE, out val4) || !Double.TryParse(MeasDate, out val5) ||
                _CurrentPartOEE == null || _CurrentCharacteristicsOEE == null || _CurrentCharacteristicsOEE.Length != 3)
            {
                IntoFile("Creating measurement failed due to badly formatted values!\r\n");
                return;
            }

            try
            {
                IntoFile("Creating a measurement and Its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementOEE = measArray.Last();
                }

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val1 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[1].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val2 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[2].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val3 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[3].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val4 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[4].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val5 )
                        }
                    },
                    PartUuid = _CurrentPartOEE.Uuid
                };
                await _RestDataServiceClient.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }

        private async void UpdateMeasurementsOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate)
        {
            double val1, val2, val3, val4, val5;

            if (!Double.TryParse(AF, out val1) || !Double.TryParse(PF, out val2) ||
                !Double.TryParse(QF, out val3) || !Double.TryParse(OEE, out val4) || !Double.TryParse(MeasDate, out val5) ||
                _CurrentPartOEE == null || _CurrentCharacteristicsOEE == null || _CurrentCharacteristicsOEE.Length != 3)
            {
                IntoFile("Updating measurement failed due to badly formatted values!\r\n");
                return;
            }

            try
            {
                IntoFile("Updating a measurement and its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementOEE = measArray.Last();
                }

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = _dataMeasurementOEE.Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val1 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[1].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val2 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[2].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val3 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[3].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val4 )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[4].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val5 )
                        }
                    },
                    PartUuid = _CurrentPartOEE.Uuid
                };
                await _RestDataServiceClient.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

                sw.Stop();
                IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Updating measurement: '{0}'.\r\n" , ex.ToString()));
            }
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

        public async Task<bool> ismachineavailable(int Machineid, string Machinepartpath, string CorrectedDate)
        {
            bool isexist = false;
            try
            {
                //IntoFile("Creating a measurement and Its values");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributesCharacter = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(Machinepartpath);
                SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();

                if (measArray.Any())
                {
                    List<SimpleMeasurement> measurements = measArray.ToList();
                    foreach (SimpleMeasurement measured in measurements)
                    {
                        _dataMeasurement = measured;

                        Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute Machine = _dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K9")).FirstOrDefault();
                        Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute K4_correctedDate = _dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4")).FirstOrDefault();

                        //foreach (var machines in Machine)
                        //{

                        if (Convert.ToInt32(Machine.Value) == Machineid)
                        {
                            isexist = true;
                        }
                        //}
                    }
                }
                else
                {
                    isexist = true;
                }
            }

            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }

            return await Task.FromResult<bool>(isexist);

        }

        public bool AuthenticateUser(string APIUser, string Password)
        {
            //Create an AuthenticationContainer
            bool isAuthenticated = false;
            AuthenticationContainer authContainer = new AuthenticationContainer
            (
              AuthenticationMode.NoneOrBasic,
              new NetworkCredential(APIUser, Password)
            );

            return isAuthenticated;
        }


        #region ALARAM
        //Alaram Details
        public async void InitializeControlAlarm(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            _Configuration_Alarm = configuration;
            _Catalogs = catalogs;
            _RestDataServiceClientAlarm = new DataServiceRestClient(serverUri);

            AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
            NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
            //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
            AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
            _RestDataServiceClientAlarm.AuthenticationContainer = Authent;

            PathInformation partPath_Alarm = PathHelper.String2PartPathInformation(AlaramPath);
            IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientAlarm.GetParts(partPath_Alarm);
            InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
            if (partsArray.Any())
            {
                _CurrentPartAlarm = partsArray.First();
            }

            IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClientAlarm.GetCharacteristics(partPath_Alarm);
            InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
            if (charsArray.Any())
            {
                _CurrentCharacteristicsAlarm = charsArray;
            }

            //AddPartAlarm(partPath_Alarm.ToString(), "AlarmDesc");
            //    PathInformation partPath = PathHelper.String2PartPathInformation("MC1Loss");
            //    SimpleMeasurement[] meas = await _RestDataServiceClientLoss.GetMeasurements(partPath);
            //    SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
            //    if (measArray.Any())
            //    {
            //        _dataMeasurementLossDel = measArray;
            //    }

            //    await _RestDataServiceClientLoss.DeleteMeasurementsByPartPath(partPath);//.UpdateMeasurementValues(new[] { measurement });
        }
        private async void AddPartAlarm(String PartPath, String AlarmDesc)
        {
            IntoFile("Creating inspection plan items");

            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
            PathInformation char1Path = GetCharPath(AlarmDesc, PartPath);
            //PathInformation char2Path = GetCharPath(AlarmDateTime, PartPath);

            try
            {
                InspectionPlanPart part = new InspectionPlanPart { Path = partPath, Uuid = Guid.NewGuid() };
                InspectionPlanCharacteristic char1 = new InspectionPlanCharacteristic { Path = char1Path, Uuid = Guid.NewGuid() };
                //InspectionPlanCharacteristic char2 = new InspectionPlanCharacteristic { Path = char2Path, Uuid = Guid.NewGuid() };

                InspectionPlanCharacteristic[] characteristics = new[] { char1 };

                await _RestDataServiceClientAlarm.CreateParts(new[] { part });
                await _RestDataServiceClientAlarm.CreateCharacteristics(characteristics);

                _CurrentPartAlarm = part;
                _CurrentCharacteristicsAlarm = characteristics;

                sw.Stop();
                IntoFile("Succesfully created inspection plan items in {0} ms\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error creating part '{0}': '{1}'.\r\n", partPath, ex.ToString()));
                //_CurrentPartLoss = null;
                //_CurrentCharacteristicsLoss = null;
            }
        }
        private async void CreateMeasurementsAlarm(String PartPath, String AlarmDesc, DateTime AlarmDatetime)
        {
            double val1;


            if (!Double.TryParse(AlarmDesc, out val1) || _CurrentPartAlarm == null || _CurrentCharacteristicsAlarm == null || _CurrentCharacteristicsAlarm.Length != 1)
            {
                IntoFile("Creating measurement failed due to badly formatted values!\r\n");
                return;
            }

            try
            {
                IntoFile("Creating a measurement and Its values");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClientAlarm.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementAlarmDel = measArray;
                }

                //await _RestDataServiceClientLoss.DeleteMeasurementsByPartPath(partPath);//.UpdateMeasurementValues(new[] { measurement });

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Time = Convert.ToDateTime(AlarmDatetime.ToUniversalTime().ToLocalTime()),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsAlarm[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( val1 ),
                        }
                    },
                    PartUuid = _CurrentPartAlarm.Uuid
                };
                await _RestDataServiceClientAlarm.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }
        private async void CreateMeasurementsAlarmOnload(String PartPath, String AlarmDesc, DateTime AlarmDatetime, int MachineID)
        {


            //if (!Double.TryParse(AlarmDesc, out val1) || _CurrentPartAlarm == null || _CurrentCharacteristicsAlarm == null || _CurrentCharacteristicsAlarm.Length != 1)
            //{
            //    IntoFile("Creating measurement failed due to badly formatted values!\r\n");
            //    return;
            //}

            try
            {
                IntoFile("AlarmDesc:" + AlarmDesc + " AlarmDatetime:" + AlarmDatetime);
                IntoFile("Creating a measurement and Its values");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, MachineID));
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(12, AlarmDesc));
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClientAlarm.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementAlarmDel = measArray;
                }

                //await _RestDataServiceClientLoss.DeleteMeasurementsByPartPath(partPath);//.UpdateMeasurementValues(new[] { measurement });

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Time = Convert.ToDateTime(AlarmDatetime.ToUniversalTime().ToLocalTime()),

                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsAlarm[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( 1 ),

                        }
                    },
                    PartUuid = _CurrentPartAlarm.Uuid
                };
                await _RestDataServiceClientAlarm.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Alarm creating measurement: '{0}'.\r\n" , ex.ToString()));
            }
        }
        public void InsertIntoPiwebAlarm()
        {
            try
            {
                string correcteDate = GetCorrectedDate();
                DateTime CorectedDate1 = Convert.ToDateTime(correcteDate);
                List<alarm_history_master> prevutilizationDet = new List<alarm_history_master>();
                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                    prevutilizationDet = db.alarm_history_master.Where(m => m.IsPiWeb == 0 && m.CorrectedDate == correcteDate).OrderBy(m => m.CorrectedDate).ToList();
                }
                IntoFile("Alarm count" + prevutilizationDet.Count);
                foreach (alarm_history_master row in prevutilizationDet)
                {

                    //string CorrectedDate1 = CorrectedDate.ToString("MM-dd-yyyy");
                    if (row.AlarmMessage.ToString() != "")
                    {
                        CreateMeasurementsAlarmOnload(AlaramPath, row.AlarmMessage.ToString(), Convert.ToDateTime(row.InsertedOn), Convert.ToInt32(row.MachineID));
                    }

                    using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                    {
                        alarm_history_master UpdateRow = db.alarm_history_master.Find(row.AlarmID);
                        UpdateRow.IsPiWeb = 1;
                        db.Entry(UpdateRow).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            //List<DateTime> date = prevutilizationDet.Select(m => Convert.ToDateTime(m.CorrectedDate)).ToList();
            //return date;
        }
        #endregion

        #region SpindleLoad
        public async void InitializeControlSpindle(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            _Configuration_Spindle = configuration;
            _Catalogs = catalogs;
            _RestDataServiceClientSpindle = new DataServiceRestClient(serverUri);

            AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
            NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
            //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
            AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
            _RestDataServiceClientSpindle.AuthenticationContainer = Authent;

            PathInformation partPath_Alarm = PathHelper.String2PartPathInformation(SpindlePath);
            IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientSpindle.GetParts(partPath_Alarm);
            InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
            if (partsArray.Any())
            {
                _CurrentPartSpindle = partsArray.First();
            }

            IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClientSpindle.GetCharacteristics(partPath_Alarm);
            InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
            if (charsArray.Any())
            {
                _CurrentCharacteristicsSpindle = charsArray;
            }


        }
        private async void CreateMeasurementsSpindle(String PartPath, String SpindleLoad, DateTime AlarmDatetime, int Machineid)
        {
            try
            {
                IntoFile("Creating a spindle measurement and Its values");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration_Spindle.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClientSpindle.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementSpindleDel = measArray;
                }

                //await _RestDataServiceClientLoss.DeleteMeasurementsByPartPath(partPath);//.UpdateMeasurementValues(new[] { measurement });
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, Machineid));
                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Time = Convert.ToDateTime(AlarmDatetime.ToUniversalTime().ToLocalTime()),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsSpindle[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble(SpindleLoad )),
                        }
                    },
                    PartUuid = _CurrentPartSpindle.Uuid
                };
                await _RestDataServiceClientSpindle.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error spindle creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }
        public void InsertIntoPiwebSpindle()
        {
            try
            {
                string correcteDate = GetCorrectedDate();
                DateTime CorectedDate1 = Convert.ToDateTime(correcteDate);
                List<tbl_axisdetails2> prevutilizationDet = new List<tbl_axisdetails2>();
                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                    prevutilizationDet = db.tbl_axisdetails2.Where(m => m.IsPiWeb == 0).OrderBy(m => m.AD2ID).ToList();
                }
                IntoFile("Spindle count" + prevutilizationDet.Count);
                foreach (tbl_axisdetails2 row in prevutilizationDet)
                {

                    //string CorrectedDate1 = CorrectedDate.ToString("MM-dd-yyyy");

                    CreateMeasurementsSpindle(SpindlePath, row.SpindleLoad.ToString(), Convert.ToDateTime(row.StartTime), Convert.ToInt32(row.MachineID));

                    using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                    {
                        tbl_axisdetails2 UpdateRow = db.tbl_axisdetails2.Find(row.AD2ID);
                        UpdateRow.IsPiWeb = 1;
                        db.Entry(UpdateRow).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            //List<DateTime> date = prevutilizationDet.Select(m => Convert.ToDateTime(m.CorrectedDate)).ToList();
            //return date;
        }
        #endregion


        #region Quality

        public async void InitializeControlQuality(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            _Configuration_Quality = configuration;
            _Catalogs = catalogs;
            _RestDataServiceClientQuality = new DataServiceRestClient(serverUri);
            try
            {

                AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
                NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
                //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
                AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
                _RestDataServiceClientQuality.AuthenticationContainer = Authent;

                PathInformation partPath_Quality = PathHelper.String2PartPathInformation(QualityPartPath);
                //_RestDataServiceClientQuality.GetParts(partPath_Quality).Wait();
                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientQuality.GetParts(partPath_Quality);

                //IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientQuality.GetParts(partPath_Quality);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                if (partsArray.Any())
                {
                    _CurrentPartAlarm = partsArray.First();
                }

                IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClientQuality.GetCharacteristics(partPath_Quality);
                InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
                if (charsArray.Any())
                {
                    _CurrentCharacteristicsQuality = charsArray;
                }

                try
                {
                    GetMeasurements_Qualty(QualityPartPath);
                }
                catch (Exception ex)
                {
                    IntoFile(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
            //AddPartAlarm(partPath_Alarm.ToString(), "AlarmDesc");
            //    PathInformation partPath = PathHelper.String2PartPathInformation("MC1Loss");
            //    SimpleMeasurement[] meas = await _RestDataServiceClientLoss.GetMeasurements(partPath);
            //    SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
            //    if (measArray.Any())
            //    {
            //        _dataMeasurementLossDel = measArray;
            //    }

            //    await _RestDataServiceClientLoss.DeleteMeasurementsByPartPath(partPath);//.UpdateMeasurementValues(new[] { measurement });
        }
        private async void GetMeasurements_Qualty(String QualityPartPath)
        {
            try
            {
                //IntoFile("Creating a measurement and Its values");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributesCharacter = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration_Quality.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(QualityPartPath);
                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientQuality.GetParts(partPath);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                List<QUALITY> finalQuality = new List<QUALITY>();


                foreach (InspectionPlanPart partpathdet in parts)
                {
                    QUALITY qulaity_I = new QUALITY();
                    List<QualityData> qualitydat = new List<QualityData>();
                    string partpathdet1 = "";

                    if (partsArray.Any())
                    {
                        _CurrentPartQuality = partpathdet;
                        partpathdet1 = _CurrentPartQuality.Path.ToString();
                    }
                    string[] data = partpathdet1.Split('/');

                    if (data.Length > 2)
                    {

                        qulaity_I.PartNumber = partpathdet1.Split('/')[2];
                    }
                    PathInformation parthdet1 = PathHelper.String2PartPathInformation(partpathdet1);
                    SimpleMeasurement[] meas = await _RestDataServiceClientQuality.GetMeasurements(parthdet1);
                    SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                    //if (TypeofInsp == 0)
                    {
                        if (measArray.Any())
                        {
                            _dataMeasurementQuality = meas;

                            List<SimpleMeasurement> data1 = meas.ToList();
                            //var OP101 = data1.Where(m => m.Attributes.ToString().Contains("K15")).ToList();
                            //List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> OP10 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4051")).ToList();
                            foreach (SimpleMeasurement dataMeasurement in _dataMeasurementQuality)
                            {
                                QualityData data_Q = new QualityData();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP10 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4051")).FirstOrDefault();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute Workorder = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K8")).FirstOrDefault();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute PartIdent = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K14")).FirstOrDefault();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute Status = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K96")).FirstOrDefault();
                                //int K4051 = dataMeasurement.Attributes.ToList().IndexOf(dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4051")).FirstOrDefault());
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP20 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4052")).FirstOrDefault();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP30 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4053")).FirstOrDefault();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute MachinieID = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K9")).FirstOrDefault();
                                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OperationConfirmation = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4054")).FirstOrDefault();
                                if (MachinieID != null && PartIdent != null)
                                {
                                    data_Q.MachineID = Convert.ToInt32(MachinieID.Value);

                                    if (OperationConfirmation != null)
                                    {

                                        if (OperationConfirmation.Value == "3")      //OP30 
                                        {
                                            if (OP30.Value == "30")
                                            {
                                                data_Q.OperationNum = 30;
                                                data_Q.PartIdentity = PartIdent.Value;
                                                if (Status.Value == "2")
                                                {
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op10 = 1;
                                                    data_Q.Op20 = 1;
                                                    data_Q.Status = 2; //Rejected
                                                }
                                                else if (Status.Value == "1")
                                                {
                                                    data_Q.Op10 = 1;  //op10 conifirmed
                                                    data_Q.Op20 = 1;  //op20 Confirmed
                                                    data_Q.Op30 = 1; //op30 Confirmed
                                                    data_Q.Status = 1; //Approved
                                                }
                                                else
                                                {
                                                    data_Q.Op10 = 0;
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op20 = 0;
                                                }
                                                data_Q.WorkOrderNum = Workorder.Value;
                                            }
                                            else
                                            {
                                                data_Q.Op10 = 0;
                                                data_Q.Op30 = 0;
                                                data_Q.Op20 = 0;
                                            }
                                        }
                                        else if (OperationConfirmation.Value == "2")     //OP20 
                                        {
                                            if (OP20.Value == "20")
                                            {
                                                data_Q.OperationNum = 20;

                                                data_Q.PartIdentity = PartIdent.Value;
                                                if (Status.Value == "2")
                                                {
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op20 = 0;
                                                    data_Q.Op10 = 1;
                                                    data_Q.Status = 2; //Rejected
                                                }
                                                else if (Status.Value == "1")
                                                {
                                                    data_Q.Op10 = 1;
                                                    data_Q.Op20 = 1;
                                                    data_Q.Op30 = 0;
                                                    data_Q.Status = 1; //Approved
                                                }

                                                data_Q.WorkOrderNum = Workorder.Value;

                                            }
                                            else
                                            {
                                                data_Q.Op10 = 0;
                                                data_Q.Op30 = 0;
                                                data_Q.Op20 = 0;
                                            }


                                        }

                                        else if (OperationConfirmation.Value == "1")     //OP10 
                                        {
                                            if (OP10.Value == "10")
                                            {
                                                data_Q.OperationNum = 10;

                                                data_Q.Op20 = 0;
                                                data_Q.Op30 = 0;
                                                data_Q.PartIdentity = PartIdent.Value;
                                                if (Status.Value == "2")
                                                {
                                                    data_Q.Op10 = 0;
                                                    data_Q.Status = 2; //Rejected
                                                }
                                                else if (Status.Value == "1")
                                                {
                                                    data_Q.Op10 = 1;
                                                    data_Q.Status = 1; //Approved

                                                }

                                                data_Q.WorkOrderNum = Workorder.Value;

                                            }
                                            else
                                            {
                                                data_Q.Op10 = 0;
                                                data_Q.Op30 = 0;
                                                data_Q.Op20 = 0;
                                            }
                                        }
                                    }
                                    List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> Attribureslist = dataMeasurement.Attributes.ToList();
                                    if (data_Q.OperationNum != null)
                                    {

                                        qualitydat.Add(data_Q);

                                    }
                                }

                            }
                            //GetLockData(out LockValue, out UnlockValue); // With serial communication

                            sw.Stop();
                        }

                    }

                    if (qulaity_I.PartNumber != null)
                    {

                        qulaity_I.QualityDatameasurements = qualitydat;
                        finalQuality.Add(qulaity_I);
                    }





                }
                insertQualityDB(finalQuality);
                //LockmachineTCP_Multiple();
                //IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Quality getting measurement: typeof inspec: \r\n" , ex.ToString()));
            }
        }
        #endregion


        #region OEEPiweb 

        private async void CreateMeasurementsOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate, int MachineID, string MachineName)
        {

            //if (!Double.TryParse(AF, out val1) || !Double.TryParse(PF, out val2) ||
            //    !Double.TryParse(QF, out val3) || !Double.TryParse(OEE, out val4) || !Double.TryParse(MeasDate, out val5) ||
            //    _CurrentPartOEE == null || _CurrentCharacteristicsOEE == null || _CurrentCharacteristicsOEE.Length != 3)
            //{
            //    //IntoFile("Creating measurement failed due to badly formatted values!\r\n");
            //    //return;
            //}

            try
            {
                IntoFile("Creating a OEE measurement and Its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration_OEE.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                //SimpleMeasurement[] meas = await _RestDataServiceClient_OEE.GetMeasurements(partPath);
                //SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                //if (measArray.Any())
                //{
                //    _dataMeasurementOEE = measArray.Last();
                //}


                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, MachineID));

                DataMeasurement measurement = new DataMeasurement
                {
                    Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
                    Attributes = attributes.ToArray(),
                    Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue(Convert.ToDouble( AF) )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[1].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble( OEE) )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[2].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble( PF) )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[3].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble( QF) )
                        }

                    },
                    PartUuid = _CurrentPartOEE.Uuid
                };
                await _RestDataServiceClient.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error OEE creating measurement :  '{0}'.\r\n", ex.ToString()));
            }
        }

        private async void UpdateMeasurementsOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate, int MachineID, string MachineName)
        {

            //if (!Double.TryParse(AF, out val1) || !Double.TryParse(PF, out val2) ||
            //    !Double.TryParse(QF, out val3) || !Double.TryParse(OEE, out val4) || !Double.TryParse(MeasDate, out val5) ||
            //    _CurrentPartOEE == null || _CurrentCharacteristicsOEE == null || _CurrentCharacteristicsOEE.Length != 3)
            //{
            //    IntoFile("Updating measurement failed due to badly formatted values!\r\n");
            //    return;
            //}

            try
            {
                IntoFile("Updating a measurement of OEE " + MachineID + " and its values");

                Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

                CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementOEE = measArray.Last();
                }

                int k9 = _dataMeasurementOEE.Attributes.ToList().IndexOf(_dataMeasurementOEE.Attributes.ToList().First(i => i.ToString().Contains("K9")));
                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute[] data1 = _dataMeasurementOEE.Attributes.ToArray();

                if (Convert.ToInt32(data1[k9].Value) == MachineID)
                {
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, MachineID));
                    DataMeasurement measurement = new DataMeasurement
                    {
                        Uuid = _dataMeasurementOEE.Uuid,//.NewGuid(),
                        Attributes = attributes.ToArray(),
                        Characteristics = new[]
                    {
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[0].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue(Convert.ToDouble( AF) )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[1].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble( OEE) )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[2].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble( PF) )
                        },
                        new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristicsOEE[3].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble( QF) )
                        }

                    },
                        PartUuid = _CurrentPartOEE.Uuid
                    };
                    await _RestDataServiceClient.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

                    sw.Stop();
                    IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Updating measurement: '{0}'.\r\n" , ex.ToString()));
            }
        }
        #endregion
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

        private void insertQualityDB(List<QUALITY> finalQuality)
        {
            try
            {
                if (finalQuality.Count > 0)
                {
                    string CorrectedDate = GetCorrectedDate();
                    foreach (QUALITY row in finalQuality)
                    {
                        List<QualityData> rawdataqualtiy = row.QualityDatameasurements;

                        foreach (QualityData rowq in rawdataqualtiy)
                        {
                            QualtiyRaw_piweb rawQualtiy = new QualtiyRaw_piweb();

                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {

                                rawQualtiy = db.QualtiyRaw_piweb.Where(m => m.CorrectedDate == CorrectedDate && m.IsPiweb == 0 && m.PartNumber == row.PartNumber && m.PartIdentity == rowq.PartIdentity).FirstOrDefault();
                            }

                            if (rawQualtiy == null)
                            {
                                QualtiyRaw_piweb rawQ = new QualtiyRaw_piweb();
                                rawQ.CorrectedDate = CorrectedDate;
                                rawQ.MachineID = rowq.MachineID;
                                rawQ.OP10 = rowq.Op10;
                                rawQ.OP20 = rowq.Op20;
                                rawQ.OP30 = rowq.Op30;
                                rawQ.OperationNumber = rowq.OperationNum;
                                rawQ.PartIdentity = rowq.PartIdentity;
                                rawQ.Status = rowq.Status;
                                rawQ.WorkOrderNumber = rowq.WorkOrderNum;
                                rawQ.IsPiweb = 0;
                                rawQ.MachineID = rowq.MachineID;
                                rawQ.PartNumber = row.PartNumber;
                                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                                {

                                    db.QualtiyRaw_piweb.Add(rawQ);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                                {
                                    QualtiyRaw_piweb rawQ = db.QualtiyRaw_piweb.Find(rawQualtiy.QualityID);

                                    rawQ.OP10 = rowq.Op10;
                                    rawQ.OP20 = rowq.Op20;
                                    rawQ.OP30 = rowq.Op30;
                                    rawQ.OperationNumber = rowq.OperationNum;
                                    rawQ.PartIdentity = rowq.PartIdentity;
                                    rawQ.Status = rowq.Status;
                                    using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                                    {

                                        db1.Entry(rawQ).State = System.Data.Entity.EntityState.Modified;
                                        db1.SaveChanges();
                                    }
                                }
                            }
                        }

                        formulateQuality(CorrectedDate, row.PartNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        private void formulateQuality(string correctedDate, string Partnum)
        {
            try
            {
                List<QualtiyRaw_piweb> rawdata = new List<QualtiyRaw_piweb>();
                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                    rawdata = db.QualtiyRaw_piweb.Where(m => m.CorrectedDate == correctedDate && m.PartNumber == Partnum).ToList();
                }
                if (rawdata.Count > 0)
                {
                    string workordernum = rawdata.Select(m => m.WorkOrderNumber).FirstOrDefault();
                    int? machineid = rawdata.Select(m => m.MachineID).FirstOrDefault();
                    int op10count = rawdata.Where(m => m.OP10 == 1 && m.Status == 1).ToList().Count;  //approved quntity without rejection
                    int op20count = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 1 && m.Status == 1).ToList().Count; //approved quntity without rejection
                    int op30count = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 1 && m.Status == 1).ToList().Count; //approved quntity without rejection

                    int totalOp10Count = rawdata.Where(m => m.OP10 == 1 || m.OP10 == 0).ToList().Count;
                    int totalOp20Count = rawdata.Where(m => m.OP20 == 1 || m.OP20 == 0).ToList().Count;
                    int totalOp30Count = rawdata.Where(m => m.OP20 == 1 || m.OP20 == 0).ToList().Count;

                    if (op10count != null)
                    {
                        tblQuality_Piweb singlerow = new tblQuality_Piweb();

                        using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                        {
                            singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 10 && m.MachineID == machineid && m.PartNumber == Partnum).FirstOrDefault();


                        }

                        if (singlerow == null)
                        {
                            tblQuality_Piweb row = new tblQuality_Piweb();
                            row.CorrectedDate = correctedDate;
                            row.IsPiweb = 0;
                            row.MachineID = machineid;
                            row.OperationNum = 10;
                            row.PartNumber = Partnum;
                            row.RejectedQty = op10count;
                            row.TotalQty = totalOp10Count;
                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {

                                db.tblQuality_Piweb.Add(row);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {
                                tblQuality_Piweb rawQ = db.tblQuality_Piweb.Find(singlerow.QualityID);
                            }

                            singlerow.RejectedQty = op10count;
                            singlerow.TotalQty = totalOp10Count;
                            using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                            {

                                db1.Entry(singlerow).State = System.Data.Entity.EntityState.Modified;
                                db1.SaveChanges();
                            }
                        }
                    }
                    if (op20count != null)
                    {
                        tblQuality_Piweb singlerow = new tblQuality_Piweb();

                        using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                        {
                            singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 20 && m.MachineID == machineid && m.PartNumber == Partnum).FirstOrDefault();

                        }
                        if (singlerow == null)
                        {
                            tblQuality_Piweb row = new tblQuality_Piweb();
                            row.CorrectedDate = correctedDate;
                            row.IsPiweb = 0;
                            row.MachineID = machineid;
                            row.OperationNum = 20;
                            row.PartNumber = Partnum;
                            row.RejectedQty = op20count;
                            row.TotalQty = totalOp20Count;
                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {

                                db.tblQuality_Piweb.Add(row);
                                db.SaveChanges();
                            }
                        }
                        else
                        {

                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {
                                tblQuality_Piweb rawQ = db.tblQuality_Piweb.Find(singlerow.QualityID);
                            }

                            singlerow.RejectedQty = op20count;
                            singlerow.TotalQty = totalOp20Count;
                            using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                            {

                                db1.Entry(singlerow).State = System.Data.Entity.EntityState.Modified;
                                db1.SaveChanges();
                            }
                        }
                    }
                    if (op30count != null)
                    {
                        tblQuality_Piweb singlerow = new tblQuality_Piweb();

                        using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                        {
                            singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 30 && m.MachineID == machineid && m.PartNumber == Partnum).FirstOrDefault();

                        }
                        if (singlerow == null)
                        {

                            tblQuality_Piweb row = new tblQuality_Piweb();
                            row.CorrectedDate = correctedDate;
                            row.IsPiweb = 0;
                            row.MachineID = machineid;
                            row.OperationNum = 30;
                            row.PartNumber = Partnum;
                            row.RejectedQty = op30count;
                            row.TotalQty = totalOp30Count;
                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {

                                db.tblQuality_Piweb.Add(row);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {
                                tblQuality_Piweb rawQ = db.tblQuality_Piweb.Find(singlerow.QualityID);
                            }

                            singlerow.RejectedQty = op30count;
                            singlerow.TotalQty = totalOp30Count;
                            using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                            {

                                db1.Entry(singlerow).State = System.Data.Entity.EntityState.Modified;
                                db1.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }





        // Convert List TO DataTable
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        //push the data into EXCEL
        private void ToEXCEL(DataTable dt, string FileName)
        {

            try
            {
                FileInfo templateFile = new FileInfo(@"C:\OEE-CSV\Template\" + FileName + ".csv");
            
                
                ExcelPackage templatep = new ExcelPackage(templateFile);
                ExcelWorksheet Templatews = templatep.Workbook.Worksheets[1];

                String FileDir = @"C:\OEE-CSV";
                //String FileDir = @"C:\inetpub\ContiAndonWebApp\Reports\" + System.DateTime.Now.ToString("yyyy");

                bool exists = System.IO.Directory.Exists(FileDir);

                if (!exists)
                    System.IO.Directory.CreateDirectory(FileDir);

                FileInfo newFile = new FileInfo(System.IO.Path.Combine(FileDir, FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv")); //+ " to " + toda.ToString("yyyy-MM-dd") 
                if (newFile.Exists)
                {
                    try
                    {
                        newFile.Delete();  // ensures we create a new workbook
                        newFile = new FileInfo(System.IO.Path.Combine(FileDir, FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv")); //" to " + toda.ToString("yyyy-MM-dd") + 
                    }
                    catch
                    {
                        IntoFile("Excel with same date is already open, please close it and try to generate!!!!");
                        //return View();
                    }
                }
                //Using the File for generation and populating it
                ExcelPackage p = null;
                p = new ExcelPackage(newFile);
                ExcelWorksheet worksheet = null;

                //Creating the WorkSheet for populating
                try
                {
                    worksheet = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("yyyyMMddHHmmss"), Templatews);
                }
                catch { }

                if (worksheet == null)
                {
                    worksheet = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("yyyyMMddHHmmss"), Templatews);
                }

                worksheet.Cells["A1"].LoadFromDataTable(dt, true);


                p.Save();
                //Downloding Excel
                string path1 = System.IO.Path.Combine(FileDir, FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                System.IO.FileInfo file1 = new System.IO.FileInfo(path1);
                string Outgoingfile = FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        public  void ToCSV( DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

    }
}
