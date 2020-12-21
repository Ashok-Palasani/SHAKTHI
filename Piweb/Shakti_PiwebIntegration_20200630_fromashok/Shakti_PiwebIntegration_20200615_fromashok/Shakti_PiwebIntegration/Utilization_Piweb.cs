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
    public class Utilization_Piweb
    {
        private i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1();

        private string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
        private string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
        private string Utilization_Path = ConfigurationManager.AppSettings["MachineDataPath"];
        //private string Daystartvalue = ConfigurationManager.AppSettings["Daystartvalueutil"];

        #region Members
        private int _DayStart = 0;
        private InspectionPlanPart _CurrentPart;
        private DataServiceRestClient _RestDataServiceClient;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration;
        private CatalogCollection _Catalogs;
        private InspectionPlanCharacteristic[] _CurrentCharacteristics;

        private DataServiceRestClient _RestDataServiceClient_Util;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Util;
        private InspectionPlanCharacteristic[] _CurrentCharacteristics_Util;
        private SimpleMeasurement _dataMeasurement_Util;
        private InspectionPlanPart _CurrentPart_Util;

        private IConnectionFactory _conn;
        private Dao obj1 = new Dao();
        private Dao1 obj = new Dao1();

        #endregion

        public Utilization_Piweb(Uri _ServerUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration, CatalogCollection catalogs)
        {
            _Catalogs = catalogs;
            //_DayStart = Convert.ToInt32(Daystartvalue);
            InitializeControlUtil(_ServerUri, _Configuration, _Catalogs, Utilization_Path);
        }

        public async void InitializeControlUtil(Uri serverUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration configuration, CatalogCollection catalogs, string Utilization_Path)
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

                PathInformation partPath = PathHelper.String2PartPathInformation(Utilization_Path);
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

                try
                {
                    GetModeHour();
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


            //AddPartUtil(Utilization_Path, "OpTime", "IdleTime", "MNTTime", "POffTime", "MeasDate", "MeasHour");
        }

        private PathInformation GetCharPath(String Value, String partPath)
        {
            return PathHelper.String2PathInformation(string.Concat(partPath.Trim('/'), PathHelper.DelimiterString, Value.Trim('/')), "PC");
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
                IntoFile(String.Format("Error creating part '{0}': '{1}'.\r\n" + partPath, ex.ToString()));
                _CurrentPart = null;
                _CurrentCharacteristics = null;
            }
        }

        #region Commented bcz we are not using this
        //private async void CreateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MeasHour)
        //{
        //    double val1, val2, val3, val4, val5, val6;

        //    if (!Double.TryParse(OpTime, out val1) || !Double.TryParse(IdleTime, out val2) ||
        //        !Double.TryParse(MNTTime, out val3) || !Double.TryParse(POffTime, out val4) || !Double.TryParse(MeasDate, out val5) ||
        //        !Double.TryParse(MeasHour, out val6) || _CurrentPart == null || _CurrentCharacteristics == null || _CurrentCharacteristics.Length != 3)
        //    {
        //        IntoFile("Creating measurement failed due to badly formatted values!\r\n");
        //        return;
        //    }

        //    try
        //    {
        //        IntoFile("Creating a measurement and Its values");

        //        Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

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
        //        SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
        //        SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
        //        if (measArray.Any())
        //        {
        //            _dataMeasurement = measArray.Last();
        //        }

        //        DataMeasurement measurement = new DataMeasurement
        //        {
        //            Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
        //            Attributes = attributes.ToArray(),
        //            Characteristics = new[]
        //            {
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristics[0].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val1 )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristics[1].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val2 )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristics[2].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val3 )
        //                }
        //            },
        //            PartUuid = _CurrentPart.Uuid
        //        };
        //        await _RestDataServiceClient.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

        //        sw.Stop();
        //        IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(String.Format("Error creating measurement: '{0}'.\r\n", ex.ToString()));
        //    }
        //}

        ////private async void CreateMeasurements_Periodic(String PartPath, String Pressure_regulator, String ShiftVal)
        ////{
        ////    double val1;


        ////    //if (!Double.TryParse(Pressure_regulator, out val1) || !Double.TryParse(LH_air_pressure, out val2) ||
        ////    //    !Double.TryParse(RH_air_pressure, out val3) || !Double.TryParse(SwithceOperation, out val4) || !Double.TryParse(abnormal, out val5) ||
        ////    //    !Double.TryParse(LH_RH_GreaseDispenses, out val6) || _CurrentPartMNT == null || _CurrentCharacteristicsMNT == null || _CurrentCharacteristicsMNT.Length != 13)
        ////    //{
        ////    //    IntoFile("Creating measurement failed due to badly formatted values!\r\n");

        ////    //}

        ////    try
        ////    {
        ////        IntoFile("Creating a measurement periodic and Its values");

        ////        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        ////        List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> attributes = new List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute>();
        ////        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.Time, XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind)));

        ////        CatalogAttributeDefinition inspectorDef = _Configuration.GetDefinition(Entity.Measurement, WellKnownKeys.Measurement.InspectorName) as CatalogAttributeDefinition;
        ////        Random rdm = new Random();
        ////        if (inspectorDef != null)
        ////        {
        ////            CatalogEntry catalogEntry = _Catalogs[inspectorDef.Catalog, rdm.Next(0, _Catalogs[inspectorDef.Catalog].CatalogEntries.Length).ToString()];
        ////            attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.InspectorName, catalogEntry.Key));
        ////        }
        ////        PathInformation partPath = PathHelper.String2PartPathInformation(PartPath);
        ////        SimpleMeasurement[] meas = await _RestDataServiceMachine.GetMeasurements(partPath);
        ////        SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
        ////        if (measArray.Any())
        ////        {
        ////            _dataMeasurement = measArray.First();
        ////        }
        ////        val1 = 0;
        ////        IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient.GetParts(partPath);
        ////        InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();

        ////        if (partsArray.Any())
        ////        {
        ////            _CurrentPartPeriodic = partsArray.First();
        ////        }

        ////        IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClient.GetCharacteristics(partPath);
        ////        InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
        ////        if (charsArray.Any())
        ////        {
        ////            _CurrentCharacteristicsMachine = charsArray;

        ////        }

        ////        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(WellKnownKeys.Measurement.MeasurementStatus, 0));
        ////        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(61, 0));
        ////        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(62, DateTime.Now));
        ////        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(63, 0));
        ////        attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(850, ShiftVal));


        ////        DataMeasurement measurement = new DataMeasurement
        ////        {
        ////            Uuid = Guid.NewGuid(),  //_dataMeasurement[0].Uuid,//.NewGuid(),
        ////            Attributes = attributes.ToArray(),

        ////            Characteristics = new[]
        ////            {
        ////                new DataCharacteristic
        ////                {
        ////                    Uuid = _CurrentCharacteristicsMachine[0].Uuid,
        ////                    Timestamp = DateTime.Now,
        ////                    Value = new DataValue( val1 )
        ////                },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[1].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[2].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[3].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[4].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[5].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                     new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[6].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                      new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[7].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                           new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[8].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                   new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[9].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[10].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },

        ////                   new DataCharacteristic
        ////                {
        ////                    Uuid = _CurrentCharacteristicsMachine[11].Uuid,
        ////                    Timestamp = DateTime.Now,
        ////                    Value = new DataValue( val1 )
        ////                },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[12].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[13].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[14].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[15].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[16].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                     new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[17].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                      new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[18].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                           new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[19].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                   new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[20].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[21].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },

        ////                    new DataCharacteristic
        ////                {
        ////                    Uuid = _CurrentCharacteristicsMachine[22].Uuid,
        ////                    Timestamp = DateTime.Now,
        ////                    Value = new DataValue( val1 )
        ////                },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[23].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[24].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[25].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[26].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[27].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                     new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[28].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                      new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[29].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                           new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[30].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                   new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[31].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[32].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },

        ////                   new DataCharacteristic
        ////                {
        ////                    Uuid = _CurrentCharacteristicsMachine[33].Uuid,
        ////                    Timestamp = DateTime.Now,
        ////                    Value = new DataValue( val1 )
        ////                },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[34].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    },
        ////                    new DataCharacteristic
        ////                    {
        ////                        Uuid = _CurrentCharacteristicsMachine[35].Uuid,
        ////                        Timestamp = DateTime.Now,
        ////                        Value = new DataValue( val1 )
        ////                    }
        ////                    //new DataCharacteristic
        ////                //    {
        ////                //        Uuid = _CurrentCharacteristicsMNT[36].Uuid,
        ////                //        Timestamp = DateTime.Now,
        ////                //        Value = new DataValue( val1 )
        ////                //    },
        ////                //    new DataCharacteristic
        ////                //    {
        ////                //        Uuid = _CurrentCharacteristicsMNT[37].Uuid,
        ////                //        Timestamp = DateTime.Now,
        ////                //        Value = new DataValue( val1 )
        ////                //    },
        ////                //    new DataCharacteristic
        ////                //    {
        ////                //        Uuid = _CurrentCharacteristicsMNT[38].Uuid,
        ////                //        Timestamp = DateTime.Now,
        ////                //        Value = new DataValue( val1 )
        ////                //    },
        ////                //     new DataCharacteristic
        ////                //    {
        ////                //        Uuid = _CurrentCharacteristicsMNT[39].Uuid,
        ////                //        Timestamp = DateTime.Now,
        ////                //        Value = new DataValue( val1 )
        ////                //    },
        ////                //      new DataCharacteristic
        ////                //    {
        ////                //        Uuid = _CurrentCharacteristicsMNT[40].Uuid,
        ////                //        Timestamp = DateTime.Now,
        ////                //        Value = new DataValue( val1 )
        ////                //    }
        ////        },
        ////            PartUuid = _CurrentPartPeriodic.Uuid
        ////        };
        ////        await _RestDataServiceClient.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

        ////        sw.Stop();
        ////        IntoFile("Successfully created a periodic measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        IntoFile(String.Format("Error creating measurement: '{0}'.\r\n", ex.ToString()));
        ////    }
        ////}

        //private async void UpdateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MeasHour)
        //{
        //    double val1, val2, val3, val4, val5, val6;

        //    if (!Double.TryParse(OpTime, out val1) || !Double.TryParse(IdleTime, out val2) ||
        //        !Double.TryParse(MNTTime, out val3) || !Double.TryParse(POffTime, out val4) || !Double.TryParse(MeasDate, out val5) ||
        //        !Double.TryParse(MeasHour, out val6) || _CurrentPart == null || _CurrentCharacteristics == null || _CurrentCharacteristics.Length != 3)
        //    {
        //        IntoFile("Updating measurement failed due to badly formatted values!\r\n");
        //        return;
        //    }

        //    try
        //    {
        //        IntoFile("Updating a measurement and its values");

        //        Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

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
        //        SimpleMeasurement[] meas = await _RestDataServiceClient.GetMeasurements(partPath);
        //        SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();
        //        if (measArray.Any())
        //        {
        //            _dataMeasurement = measArray.Last();
        //        }

        //        DataMeasurement measurement = new DataMeasurement
        //        {
        //            Uuid = _dataMeasurement.Uuid,//.NewGuid(),
        //            Attributes = attributes.ToArray(),
        //            Characteristics = new[]
        //            {
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristics[0].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val1 )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristics[1].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val2 )
        //                },
        //                new DataCharacteristic
        //                {
        //                    Uuid = _CurrentCharacteristics[2].Uuid,
        //                    Timestamp = DateTime.Now,
        //                    Value = new DataValue( val3 )
        //                }
        //            },
        //            PartUuid = _CurrentPart.Uuid
        //        };
        //        await _RestDataServiceClient.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

        //        sw.Stop();
        //        IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile(String.Format("Error Updating measurement: '{0}'.\r\n", ex.ToString()));
        //    }
        //}

        #endregion
        private async void UpdateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MinorLoss, string MachineName, int Machineid, string ColorCode, string uuidfromdb)
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

                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient_Util.GetParts(partPath);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                if (partsArray.Any())
                {
                    _CurrentPart_Util = partsArray.First();
                }

                //bool a = Convert.ToBoolean(_dataMeasurement_Util.Attributes.ToList().First(i => i.ToString().Contains("K4152")));
                //IntoFile("util machineid:" + a);


                //int k4152 = _dataMeasurement_Util.Attributes.ToList().IndexOf(_dataMeasurement_Util.Attributes.ToList().First(i => i.ToString().Contains("K4152")));

                //  Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute MachinieID = _dataMeasurement_Util.Attributes.ToList().Where(m => m.ToString().Contains("K9")).FirstOrDefault();

                // IntoFile("util machineid:" + MachinieID);

                Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute[] data1 = _dataMeasurement_Util.Attributes.ToArray();
                //if (Convert.ToInt32(MachinieID.Value) == Machineid)
                {
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(7, MachineName));
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(9, Machineid));
                    attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4056, ColorCode));
                    DataMeasurement measurement = new DataMeasurement
                    {
                        // Uuid = _dataMeasurement_Util.Uuid,//.NewGuid(),
                        Uuid = (Guid.Parse(uuidfromdb)),
                        Attributes = attributes.ToArray(),
                        Characteristics = new[]
                    {

                              new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[0].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },
                             new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[1].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },

                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[5].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(OpTime) )
                            },

                            //new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[3].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[4].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[5].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[6].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[7].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[8].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[9].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                               new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics_Util[4].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble(MNTTime) )
                        },

                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[3].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(MinorLoss) )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[2].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(IdleTime) )
                            },
                             new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[6].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(POffTime) )
                            },
                            //  new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[14].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            //   new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[15].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //}, 
                        },
                           PartUuid = _CurrentPart_Util.Uuid
                          
                };
                    
                    IntoFile("uuid from measurement " + measurement.Uuid.ToString());
                    IntoFile("partuuid: " + _CurrentPart_Util.Uuid);
                    await _RestDataServiceClient_Util.UpdateMeasurementValues(new[] { measurement });//CreateMeasurementValues(new[] { measurement });//

                    sw.Stop();
                    IntoFile("Successfully Updated the measurmeent and the values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
                    
                }

            }
            catch (Exception ex)
            {
                IntoFile("utilization Error Updating measurement: '{0}'.\r\n" + ex.ToString());
            }
        }

        private async void CreateMeasurementsUtil(String PartPath, String OpTime, String IdleTime, String MNTTime, String POffTime, String MeasDate, String MinorLoss, string MachineName, int Machineid, string ColorCode, string correctedDate_S)
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

                //attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4152, Machineid));
                attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4056, ColorCode));
                //attributes.Add(new Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute(4, Convert.ToDateTime( MeasDate).AddHours(5).AddMinutes(30)));
                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClient_Util.GetParts(partPath);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                if (partsArray.Any())
                {
                    _CurrentPart_Util = partsArray.First();
                }

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
                                Value = new DataValue( 0 )
                            },
                             new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[1].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( 0 )
                            },

                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[5].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(OpTime) )
                            },

                            //new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[3].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[4].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[5].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[6].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[7].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[8].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            // new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[9].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                               new DataCharacteristic
                        {
                            Uuid = _CurrentCharacteristics_Util[4].Uuid,
                            Timestamp = DateTime.Now,
                            Value = new DataValue( Convert.ToDouble(MNTTime) )
                        },

                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[3].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(MinorLoss) )
                            },
                            new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[2].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(IdleTime) )
                            },
                             new DataCharacteristic
                            {
                                Uuid = _CurrentCharacteristics_Util[6].Uuid,
                                Timestamp = DateTime.Now,
                                Value = new DataValue( Convert.ToDouble(POffTime) )
                            },
                            //  new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[14].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                            //   new DataCharacteristic
                            //{
                            //    Uuid = _CurrentCharacteristics_Util[15].Uuid,
                            //    Timestamp = DateTime.Now,
                            //    Value = new DataValue( 0 )
                            //},
                },
                    PartUuid = _CurrentPart_Util.Uuid
                };
                string Uuid = measurement.Uuid.ToString();

                await _RestDataServiceClient_Util.CreateMeasurementValues(new[] { measurement });//.UpdateMeasurementValues(new[] { measurement });

                //Insert Uuid vaue to table based on machine id and correcteddate

                Insert_Uuid_piweb(Machineid, correctedDate_S, Uuid);


                sw.Stop();
                IntoFile("Successfully create a measurmeent and three values in {0} ms.\r\n" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format(" UTilization Error creating measurement: '{0}'.\r\n", ex.ToString()));
            }
        }



        private void GetModeHour()
        {

            string Machinedata_PartPath = Utilization_Path;
            try
            {

                string correctedDate_S = GetCorrectedDate();
                DateTime CorrectedDate_T = Convert.ToDateTime(correctedDate_S);
                List<tblmachinedetail> machinedetails = new List<tblmachinedetail>();
                using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                {
                    machinedetails = db1.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).ToList();
                }
                


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
                //    ConfigurationManager.AppSettings["Daystartvalueutil"] = "0";
                //}
                foreach (tblmachinedetail machine in machinedetails)
                {
                    int Insert = 0;

                    int machineid = machine.MachineID;
                    //IsmachineExist = await ismachineavailable(machineid, Machinedata_PartPath, correctedDate.ToString("yyyy-MM-dd"));

                    tbllivemode GetCDateMode = db.tbllivemodes.Where(m => m.MachineID == machineid).OrderByDescending(m => m.ModeID).FirstOrDefault();
                    if (GetCDateMode != null)
                    {
                        CorrectedDate_T = GetCDateMode.CorrectedDate;
                    }


                    //int GetHour = System.DateTime.Now.Hour;

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
                    string uuidfromdb = "";
                    int util_Macid = machineid;
                    tblpiweb_util utilization = db.tblpiweb_util.Where(m => m.Correcteddate == correctedDate_S && m.Machineid == machineid).FirstOrDefault();
                    if (utilization != null)
                    {
                        uuidfromdb = utilization.utilUuid;
                        _DayStart = Convert.ToInt32(utilization.DaystartValue);
                        util_Macid = Convert.ToInt32(utilization.Machineid);
                    }
                    else
                        _DayStart = 1;
                    if ((correctedDate != CorrectedDate_T || _DayStart == 1) && machineid == util_Macid)
                    {
                        Insert = 1;
                    }
                    if (Insert == 1)
                    {

                        CreateMeasurementsUtil(Machinedata_PartPath, OperatingTime.ToString(), LossTime.ToString(), MntTime.ToString(), PowerOffTime.ToString(), correctedDate.ToString("MM/dd/yyyy"), MinorLossTime.ToString(), machine.MachineDisplayName, machineid, Present_ModeColor, correctedDate_S);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        UpdateMeasurementsUtil(Machinedata_PartPath, OperatingTime.ToString(), LossTime.ToString(), MntTime.ToString(), PowerOffTime.ToString(), correctedDate.ToString("MM/dd/yyyy"), MinorLossTime.ToString(), machine.MachineDisplayName, machineid, Present_ModeColor, uuidfromdb);
                        IntoFile("machineid: " + machineid + " Uuid" + uuidfromdb);
                    }

                    decimal IdleTime = LossTime + MinorLossTime;
                    decimal BDTime = MntTime;
                    int TotalTime = Convert.ToInt32(PowerONTime) + Convert.ToInt32(OperatingTime) + Convert.ToInt32(IdleTime) + Convert.ToInt32(BDTime);
                    if (TotalTime == 0)
                    {
                        TotalTime = 1;
                    }
                    Utilization = Convert.ToInt32(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(OperatingTime) / Convert.ToDouble(TotalTime)) * 100));
                    IntoFile("UTILIZATION for Machineid" + machine.MachineID + "is : " + Utilization + "%");
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

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

        public string Insert_Uuid_piweb(int machineid, string correcteddate, string Utiluuid)

        {
            tblpiweb_util util = new tblpiweb_util();
            string retstatus = "fail";

            using (i_facility_shaktiEntities1 piwebutil = new i_facility_shaktiEntities1())
            {

                util = piwebutil.tblpiweb_util.Where(m => m.Correcteddate == correcteddate && m.Machineid == machineid).FirstOrDefault();
            }
            if (util == null)
            {
                tblpiweb_util utilrow = new tblpiweb_util();
                utilrow.Machineid = machineid;
                utilrow.Correcteddate = correcteddate;
                utilrow.utilUuid = Utiluuid;
                utilrow.CreatedOn = DateTime.Now;
                using (i_facility_shaktiEntities1 piwebutil = new i_facility_shaktiEntities1())
                {
                    piwebutil.tblpiweb_util.Add(utilrow);
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
