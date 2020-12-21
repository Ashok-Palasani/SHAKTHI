using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml;
using Zeiss.IMT.PiWeb.Api.Common.Client;
using Zeiss.IMT.PiWeb.Api.Common.Data;
using Zeiss.IMT.PiWeb.Api.DataService.Rest;

namespace Shakti_PiwebIntegration
{
    public class Spindle_Alarm_Piweb
    {
        i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1();

        private string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
        private string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
        private string AlaramPath = ConfigurationManager.AppSettings["AlaramPath"];
        private string SpindlePath = ConfigurationManager.AppSettings["SpindlePath"];
        private string Daystartvalue = ConfigurationManager.AppSettings["Daystartvalue"];

        #region Members
       
        private CatalogCollection _Catalogs;
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


        private Uri _URI;
        //private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Alarm;
        #endregion

        //Aswini commented on 2020-05-12

        //public Spindle_Alarm_Piweb()
        //{
        //    try
        //    {
        //        InsertIntoPiwebAlarm();
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(ex.ToString());
        //    }


        //    try
        //    {
        //        InsertIntoPiwebSpindle();
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(ex.ToString());
        //    }
        //}

        public Spindle_Alarm_Piweb(Uri _ServerUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration, CatalogCollection catalogs)
        {
            _Catalogs = catalogs;
            _URI = _ServerUri;
            _Configuration_Alarm = _Configuration;
            InitializeControlAlarm(_ServerUri, _Configuration, _Catalogs);
            InitializeControlSpindle(_ServerUri, _Configuration, _Catalogs);
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

            InsertIntoPiwebAlarm();

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

                CatalogAttributeDefinition inspectorDef = _Configuration_Alarm.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
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

                CatalogAttributeDefinition inspectorDef = _Configuration_Alarm.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, MachineID));
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(18, AlarmDesc));
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                //SimpleMeasurement[] meas = await _RestDataServiceClientAlarm.GetMeasurements(partPath);
                //SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                //if (measArray.Any())
                //{
                //    _dataMeasurementAlarmDel = measArray;
                //}

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
                IntoFile(String.Format("Error Alarm creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }
        public void InsertIntoPiwebAlarm()
        {
            try
            {
                string correcteDate = GetCorrectedDate();
                DateTime CorectedDate1 = Convert.ToDateTime(correcteDate);
                List<alarm_history_master> AlarmdetList = new List<alarm_history_master>();
                tblmachinedetail machinedet = new tblmachinedetail();
                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                    AlarmdetList = db.alarm_history_master.Where(m => m.IsPiWeb == 0 && m.CorrectedDate == correcteDate).OrderBy(m => m.CorrectedDate).ToList();
                
                }
                IntoFile("Alarm count" + AlarmdetList.Count);
                //List<AlaramExcel> alaramExcels = new List<AlaramExcel>();

                //foreach (alarm_history_master row in AlarmdetList)
                //{
                //    using(i_facility_shaktiEntities1 db =new i_facility_shaktiEntities1())
                //    {
                //        machinedet = db.tblmachinedetails.Find(row.MachineID);
                //    }
                //    AlaramExcel objalarm = new AlaramExcel();
                //    objalarm.AlaramType = row.AlarmMessage;
                //    objalarm.Tme_Date = row.AlarmDateTime;
                //    objalarm.SlNo = row.AlarmID;
                //    objalarm.MachineID =Convert.ToInt32( row.MachineID);
                //    if(machinedet!=null)
                //        objalarm.MachineName = machinedet.MachineName;
                //    alaramExcels.Add(objalarm);

                //    //using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                //    //{
                //    //    alarm_history_master UpdateRow = db.alarm_history_master.Find(row.AlarmID);
                //    //    UpdateRow.IsPiWeb = 1;
                //    //    db.Entry(UpdateRow).State = System.Data.Entity.EntityState.Modified;
                //    //    db.SaveChanges();
                //    //}

                //}
                //if (alaramExcels.Count > 0)
                //{
                //    DataTable dt = ToDataTable(alaramExcels);
                //    //ToEXCEL(dt, "Sampel");
                //    //ToCSV(dt, @"C:\OEE-CSV\Alaram_Type_" + DateTime.Now.ToString("yyyyyMMddHHmmss") + ".csv");
                //}

                foreach (alarm_history_master row in AlarmdetList)
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

            InsertIntoPiwebSpindle();
        }
        private async void CreateMeasurementsSpindle(String PartPath, String SpindleLoad, DateTime AlarmDatetime, int Machineid)
        {
            try
            {
                //IntoFile("Creating a spindle measurement and Its values");

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
                //IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
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
               // List<SpindleExcel> spindleExcels = new List<SpindleExcel>();
                tblmachinedetail machinedet = new tblmachinedetail();
                //foreach (tbl_axisdetails2 row in prevutilizationDet)
                //{
                //    using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                //    {
                //        machinedet = db.tblmachinedetails.Find(row.MachineID);
                //    }
                //    SpindleExcel objspindle = new SpindleExcel();
                //    objspindle.Tme_Date = Convert.ToDateTime( row.InsertedOn);
                //    objspindle.SlNo = row.AD2ID;
                //    objspindle.SpindleLoadValue = row.SpindleLoad.ToString();
                //    objspindle.MachineID =Convert.ToInt32( row.MachineID);
                //    if(machinedet!=null)
                //        objspindle.MachineName = machinedet.MachineName;
                //    spindleExcels.Add(objspindle);

                //    using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                //    {
                //        tbl_axisdetails2 UpdateRow = db.tbl_axisdetails2.Find(row.AD2ID);
                //        UpdateRow.IsPiWeb = 1;
                //        db.Entry(UpdateRow).State = System.Data.Entity.EntityState.Modified;
                //        db.SaveChanges();
                //    }

                //}
                //try
                //{
                //    if (spindleExcels.Count > 0)
                //    {
                //        DataTable dt = ToDataTable(spindleExcels);
                //        //ToEXCEL(dt, "Sampel");
                //        ToCSV(dt, @"C:\OEE-CSV\Spindle_Load_" + DateTime.Now.ToString("yyyyyMMddHHmmss") + ".csv");
                //    }
                //}
                //catch(Exception ex)
                //{
                //    IntoFile(ex.ToString());
                //}
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

        public void ToCSV(DataTable dtDataTable, string strFilePath)
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


        public string GetCorrectedDate()
        {
            string correctedDate = DateTime.Now.ToString("yyyy-MM-dd 06:00:00");
            try
            {

                string CorrectedDate = "";
                tbldaytiming StartTime1 = db.tbldaytimings.Where(m => m.IsDeleted == 0).FirstOrDefault();
                TimeSpan Start = StartTime1.StartTime;
                if (Start <= DateTime.Now.TimeOfDay)
                {
                    CorrectedDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    CorrectedDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                return CorrectedDate;

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
                string appPath = ConfigurationManager.AppSettings["ApplicationPath"] + @"\Piweb_Loggerfile_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                using (StreamWriter writer = new StreamWriter(appPath, true)) //true => Append Text
                {
                    writer.WriteLine(System.DateTime.Now + ":  " + Msg);
                }
            }
            catch (Exception)
            {

            }

        }
        #endregion
    }


}
