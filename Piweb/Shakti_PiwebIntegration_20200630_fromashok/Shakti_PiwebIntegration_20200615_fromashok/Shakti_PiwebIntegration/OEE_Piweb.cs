using i_facilitylibrary;
using i_facilitylibrary.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using Zeiss.IMT.PiWeb.Api.Common.Client;
using Zeiss.IMT.PiWeb.Api.Common.Data;
using Zeiss.IMT.PiWeb.Api.DataService.Rest;

namespace Shakti_PiwebIntegration
{
    public class OEE_Piweb
    {
        private i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1();

        private string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
        private string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
        private string OEEPart_Path = ConfigurationManager.AppSettings["OEEPath"];
        private string Top_Loss = ConfigurationManager.AppSettings["TopLoss"];
        //private string Daystartvalue = ConfigurationManager.AppSettings["Daystartvalue"];

        #region Members
        private int _DayStart = 0;
        private InspectionPlanPart _CurrentPartOEE;
        private InspectionPlanCharacteristic[] _CurrentCharacteristicsOEE;
        private SimpleMeasurement _dataMeasurementOEE;
        private DataServiceRestClient _RestDataServiceClient_OEE;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_OEE;
        private CatalogCollection _Catalogs;


        //private InspectionPlanPart _CurrentParttoploss;
        //private InspectionPlanCharacteristic[] _CurrentCharacteristicstoploss;
        //private SimpleMeasurement _dataMeasurementtoploss;
        //private DataServiceRestClient _RestDataServiceClient_toploss;
        //private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_toploss;


        private IConnectionFactory _conn;
        private Dao obj1 = new Dao();
        private Dao1 obj = new Dao1();

        #endregion

        public OEE_Piweb(Uri _ServerUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration, CatalogCollection catalogs)
        {

            _Catalogs = catalogs;
            //_DayStart = Convert.ToInt32(Daystartvalue);
            InitializeControlOEE(_ServerUri, _Configuration, _Catalogs);
            //InitializeControltoplosses(_ServerUri, _Configuration, _Catalogs);
        }

        public async void InitializeControlOEE(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            IntoFile("Intialize control OEE calling");
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
                    //System.Threading.Thread.Sleep(1000 * 60 * 1);
                    OEECalPiweb(serverUri, configuration, catalogs);
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

        private void OEECalPiweb(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        {
            IntoFile("OEECalculations started ");
            OEECalculations oeecal = new OEECalculations();
            string correctedDate_S = GetCorrectedDate();
            DateTime CorrectedDate_T = Convert.ToDateTime(correctedDate_S);
            List<tblmachinedetail> machinedetails = new List<tblmachinedetail>();
            using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
            {
                machinedetails = db1.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0 /*&& m.MachineID == 6*/).ToList(); // && m.MachineID==20
            }

            IntoFile("OEE Machines count" + machinedetails.Count);

            

            DateTime correctedDate = DateTime.Now.Date;
            i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1();

            tbllivemode OEEcorrecteddate = db.tbllivemodes.OrderByDescending(m => m.ModeID).FirstOrDefault();
            if (OEEcorrecteddate != null)
            {
                correctedDate = Convert.ToDateTime(OEEcorrecteddate.CorrectedDate);
            }
            //if (correctedDate != CorrectedDate_T || _DayStart == 1)
            //{
            //    Insert = 1;
            //    _DayStart = 0;
            //    ConfigurationManager.AppSettings["Daystartvalue"] = "0";
            //}

            List<PiwebOEE> oeedata = new List<PiwebOEE>();
            foreach (tblmachinedetail machine in machinedetails)
            {
                double Availability = 0;
                double Performance = 0;
                double Quality = 0;
                double OEE = 0;
                int MachineID = machine.MachineID;
                int Insert = 0;
                _conn = new ConnectionFactory();
                obj1 = new Dao(_conn);
                obj = new Dao1(_conn);
                //Dao obj1 = new Dao();
                //Dao1 obj = new Dao1();
                PiwebOEE oeemachine = new PiwebOEE();
                try
                {
                    obj.deletetbloeedashboardvariablestodaysDetails2(machine.IPAddress.ToString(), MachineID);
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

                IntoFile("after calculation  OEE Machine id :" + MachineID);
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

                    string oeefromdb = "";
                    int OEE_Macid = MachineID;
                    tblpiweb_OEE oee = db.tblpiweb_OEE.Where(m => m.CorrectedDate == correctedDate_S && m.Machineid == MachineID).FirstOrDefault();
                    if (oee != null)
                    {
                        oeefromdb = oee.OEEuuid;
                        _DayStart = Convert.ToInt32(oee.DaystartValue);
                        OEE_Macid = Convert.ToInt32(oee.Machineid);
                    }
                    else
                        _DayStart = 1;
                    if ((correctedDate != CorrectedDate_T || _DayStart == 1) && MachineID == OEE_Macid)
                    {
                        Insert = 1;
                    }

                    if (Insert == 1)
                    {
                        //System.Threading.Thread.Sleep(2000);
                        IntoFile("Create OEE Machine id :" + MachineID);
                        CreateMeasurementsOEE(OEEPart_Path, Availability.ToString(), Performance.ToString(), Quality.ToString(), OEE.ToString(), correctedDate.ToString("MM/dd/yyyy"), MachineID, machine.MachineName, correctedDate_S);
                    }
                    else
                    {
                        //System.Threading.Thread.Sleep(2000);
                        IntoFile("Updating OEE Machine id :" + MachineID);
                        UpdateMeasurementsOEE(OEEPart_Path, Availability.ToString(), Performance.ToString(), Quality.ToString(), OEE.ToString(), correctedDate.ToString("MM/dd/yyyy"), MachineID, machine.MachineName, oeefromdb);
                        IntoFile("MachineID" + MachineID + " Uuid:" + oeefromdb);
                    }

                }



            }

            if (oeedata.Count > 0)
            {
                //DataTable dt = ToDataTable(oeedata);
                ////ToEXCEL(dt, "Sampel");
                //ToCSV(dt, @"C:\OEE-CSV\OEESample_" + DateTime.Now.ToString("yyyyyMMddHHmmss") + ".csv");
            }

            try
            {
                IntoFile("alaram_spindle calling");
                Spindle_Alarm_Piweb objspindle = new Spindle_Alarm_Piweb(serverUri, configuration, catalogs);

            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }

        }


        #region OEEPiweb 

        private async void CreateMeasurementsOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate, int MachineID, string MachineName, string correctedDate)
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

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

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
                string Uuid = measurement.Uuid.ToString();
                await _RestDataServiceClient_OEE.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                //Insert Uuid vaue to table based on machine id and correcteddate

                Insert_Uuid_oee(MachineID, correctedDate, Uuid);
                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error OEE creating measurement :  '{0}'.\r\n", ex.ToString()));
            }
        }


        private async void UpdateMeasurementsOEE(String PartPath, String AF, String PF, String QF, String OEE, String MeasDate, int MachineID, string MachineName, string oeefromdb)
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

                CatalogAttributeDefinition inspectorDef = _Configuration_OEE.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
                Random rdm = new Random();
                if (inspectorDef != null)
                {
                    CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
                }
                PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
                SimpleMeasurement[] meas = await _RestDataServiceClient_OEE.GetMeasurements(partPath);
                SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
                if (measArray.Any())
                {
                    _dataMeasurementOEE = measArray.Last();
                }

                // Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute MachinieID = _dataMeasurementOEE.Attributes.ToList().Where(m => m.ToString().Contains("K4152")).FirstOrDefault();

                //IntoFile("OEE Machineid: " + MachinieID);
                //int k4152 = _dataMeasurementOEE.Attributes.ToList().IndexOf(_dataMeasurementOEE.Attributes.ToList().First(i => i.ToString().Contains("K4152")));
                // Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute[] data1 = _dataMeasurementOEE.Attributes.ToArray();

                //if (Convert.ToInt32(MachinieID.Value) == MachineID)
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
                    IntoFile("OEE uuid from measurement " + measurement.Uuid.ToString());
                    IntoFile("OEE partuuid: " + _CurrentPartOEE.Uuid);
                    await _RestDataServiceClient_OEE.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//


                    sw.Stop();
                    IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);

                }
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Updating measurement: '{0}'.\r\n" + MachineID, ex.ToString()));
            }
        }

        //public async void InitializeControltoplosses(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs)
        //{
        //    _Configuration_OEE = configuration;
        //    _Catalogs = catalogs;
        //    _RestDataServiceClient_toploss = new DataServiceRestClient(serverUri);
        //    try
        //    {



        //        AuthenticationMode AuthenMode = Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationMode.NoneOrBasic;
        //        NetworkCredential LoginCred = new System.Net.NetworkCredential(PiWEBUserName, PiWEBPwd);
        //        //var PiwebCredentials = new UsernamePasswordCredential("administrator", "adm!n!strat0r");
        //        AuthenticationContainer Authent = new Zeiss.IMT.PiWeb.Api.Common.Client.AuthenticationContainer(authenticationMode: AuthenMode, LoginCred);
        //        _RestDataServiceClient_toploss.AuthenticationContainer = Authent;
        //        PathInformation partPath_loss = PathHelper.String2PartPathInformation(Top_Loss);
        //        IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient_toploss.GetParts(partPath_loss);
        //        InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
        //        if (partsArray.Any())
        //        {
        //            _CurrentParttoploss = partsArray.First();
        //        }

        //        IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClient_toploss.GetCharacteristics(partPath_loss);
        //        InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
        //        if (charsArray.Any())
        //        {
        //            _CurrentCharacteristicstoploss = charsArray;
        //        }

        //        try
        //        {
        //            //System.Threading.Thread.Sleep(1000 * 60 * 1);
        //            OEECalPiweb(serverUri, configuration, catalogs);
        //        }
        //        catch (Exception ex)
        //        {
        //            IntoFile(ex.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(ex.ToString());
        //    }

        //    //  AddPartOEE("MC1OEE", "AF", "PF", "QF", "OEE", "MeasDate");
        //}

        //private async void CreateMeasurementstoploss(String PartPath, String Top1Loss, String Top2Loss, String Top3Loss, String Top4Loss, String Top5Loss, int MachineID, string MachineName)
        //{

        //    //if (!Double.TryParse(AF, out val1) || !Double.TryParse(PF, out val2) ||
        //    //    !Double.TryParse(QF, out val3) || !Double.TryParse(OEE, out val4) || !Double.TryParse(MeasDate, out val5) ||
        //    //    _CurrentPartOEE == null || _CurrentCharacteristicsOEE == null || _CurrentCharacteristicsOEE.Length != 3)
        //    //{
        //    //    //IntoFile("Creating measurement failed due to badly formatted values!\r\n");
        //    //    //return;
        //    //}

        //    try
        //    {
        //        IntoFile("Creating a top5 losses measurement and Its values");

        //        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        //        List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

        //        CatalogAttributeDefinition inspectorDef = _Configuration_toploss.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
        //        Random rdm = new Random();
        //        if (inspectorDef != null)
        //        {
        //            CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
        //            attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
        //        }
        //        PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
        //        //SimpleMeasurement[] meas = await _RestDataServiceClient_OEE.GetMeasurements(partPath);
        //        //SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
        //        //if (measArray.Any())
        //        //{
        //        //    _dataMeasurementOEE = measArray.Last();
        //        //}


        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, MachineID));

        //        DataMeasurement measurement = new DataMeasurement
        //        {
        //            Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
        //            Attributes = attributes.ToArray(),
        //            Characteristics = new[]
        //            {
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[0].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue(Convert.ToDouble( Top1Loss) )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[1].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( Top2Loss) )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[2].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( Top3Loss) )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[3].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( Top4Loss) )
        //                },

        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[4].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( Top5Loss) )
        //                }
        //            },
        //            PartUuid = _CurrentParttoploss.Uuid
        //        };
        //        await _RestDataServiceClient_toploss.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

        //        sw.Stop();
        //        IntoFile("Successfully create a measurmeent values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(String.Format("Error top 5 losses creating measurement :  '{0}'.\r\n", ex.ToString()));
        //    }
        //}

        //private async void UpdateMeasurementstoploss(String PartPath, String top1, String top2, String top3, String top4, String top5, int MachineID, string MachineName)
        //{

        //    //if (!Double.TryParse(AF, out val1) || !Double.TryParse(PF, out val2) ||
        //    //    !Double.TryParse(QF, out val3) || !Double.TryParse(OEE, out val4) || !Double.TryParse(MeasDate, out val5) ||
        //    //    _CurrentPartOEE == null || _CurrentCharacteristicsOEE == null || _CurrentCharacteristicsOEE.Length != 3)
        //    //{
        //    //    IntoFile("Updating measurement failed due to badly formatted values!\r\n");
        //    //    return;
        //    //}

        //    try
        //    {
        //        IntoFile("Updating a measurement of toplosses " + MachineID + " and its values");

        //        Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        //        List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
        //        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

        //        CatalogAttributeDefinition inspectorDef = _Configuration_toploss.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
        //        Random rdm = new Random();
        //        if (inspectorDef != null)
        //        {
        //            CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
        //            attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
        //        }
        //        PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
        //        SimpleMeasurement[] meas = await _RestDataServiceClient_toploss.GetMeasurements(partPath);
        //        SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
        //        if (measArray.Any())
        //        {
        //            _dataMeasurementtoploss = measArray.Last();
        //        }

        //        int k9 = _dataMeasurementtoploss.Attributes.ToList().IndexOf(_dataMeasurementtoploss.Attributes.ToList().First(i => i.ToString().Contains("K9")));
        //        Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute[] data1 = _dataMeasurementtoploss.Attributes.ToArray();

        //        if (Convert.ToInt32(data1[k9].Value) == MachineID)
        //        {
        //            attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
        //            attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, MachineID));
        //            DataMeasurement measurement = new DataMeasurement
        //            {
        //                Uuid = _dataMeasurementtoploss.Uuid,//.NewGuid(),
        //                Attributes = attributes.ToArray(),
        //                Characteristics = new[]
        //            {
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[0].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue(Convert.ToDouble( top1) )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[1].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( top2) )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[2].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( top3) )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristicstoploss[3].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( top4) )
        //                },
        //                 new DataCharacteristic
        //                 {
        //                    Uuid = _CurrentCharacteristicstoploss[4].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( Convert.ToDouble( top5) )
        //                 }

        //            },
        //                PartUuid = _CurrentParttoploss.Uuid
        //            };
        //            await _RestDataServiceClient_toploss.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

        //            sw.Stop();
        //            IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(String.Format("Error Updating measurement: '{0}'.\r\n", ex.ToString()));
        //    }
        //}
        #endregion
        //public string GetCorrectedDate()
        //{
        //    // string correctedDate = DateTime.Now.ToString("yyyy-MM-dd 06:00:00");
        //    string CorrectedDate = DateTime.Now.ToString("yyyy-MM-dd"); 
        //    try
        //    {


        //        tbldaytiming StartTime1 = db.tbldaytimings.Where(m => m.IsDeleted == 0).FirstOrDefault();
        //        TimeSpan Start = StartTime1.StartTime;
        //        if (Start <= DateTime.Now.TimeOfDay)
        //        {
        //            CorrectedDate = DateTime.Now.ToString("yyyy-MM-dd");
        //        }
        //        else
        //        {
        //            CorrectedDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(ex.ToString());
        //    }
        //    return CorrectedDate;

        //}

        private string GetCorrectedDate()
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

        public string Insert_Uuid_oee(int Machineid, string CorrectedDate, string OEEuuid)

        {
            tblpiweb_OEE util = new tblpiweb_OEE();
            string retstatus = "fail";

            using (i_facility_shaktiEntities1 piweboee = new i_facility_shaktiEntities1())
            {

                util = piweboee.tblpiweb_OEE.Where(m => m.CorrectedDate == CorrectedDate && m.Machineid == Machineid).FirstOrDefault();
            }
            if (util == null)
            {
                tblpiweb_OEE utilrow = new tblpiweb_OEE();
                utilrow.Machineid = Machineid;
                utilrow.CorrectedDate = CorrectedDate;
                utilrow.OEEuuid = OEEuuid;
                utilrow.Createdon = DateTime.Now;
                //utilrow.DaystartValue = 0;
                using (i_facility_shaktiEntities1 piwebutil = new i_facility_shaktiEntities1())
                {
                    piwebutil.tblpiweb_OEE.Add(utilrow);

                    piwebutil.SaveChanges();
                }
            }
            return retstatus;

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
    }
}
