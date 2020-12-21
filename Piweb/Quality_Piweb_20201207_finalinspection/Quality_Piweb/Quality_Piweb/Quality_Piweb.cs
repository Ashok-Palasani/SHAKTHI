using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using Zeiss.IMT.PiWeb.Api.Common.Client;
using Zeiss.IMT.PiWeb.Api.Common.Data;
using Zeiss.IMT.PiWeb.Api.DataService.Rest;

namespace Quality_PiwebApp
{
    public class Quality_Piweb
    {
        private string PiWEBUserName = ConfigurationManager.AppSettings["PiWEBUserName"];
        private string PiWEBPwd = ConfigurationManager.AppSettings["PiWEBPwd"];
        private string QualityPartPath = ConfigurationManager.AppSettings["QualityPartPath"];
        private string Daystartvalue = ConfigurationManager.AppSettings["Daystartvalue"];

        #region Members
        private DataServiceRestClient _RestDataServiceClientQuality;
        private InspectionPlanPart _CurrentPartQuality;
        private InspectionPlanCharacteristic[] _CurrentCharacteristicsQuality;
        private SimpleMeasurement[] _dataMeasurementQuality;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_Quality;
        private CatalogCollection _Catalogs;

        private Uri _URI;
        private Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration_OEE;
        #endregion

        public Quality_Piweb()
        {

        }

        public Quality_Piweb(Uri _ServerUri, Zeiss.IMT.PiWeb.Api.DataService.Rest.Configuration _Configuration, CatalogCollection catalogs)
        {
            _Catalogs = catalogs;
            _URI = _ServerUri;
            _Configuration_OEE = _Configuration;
            InitializeControlQuality(_ServerUri, _Configuration, _Catalogs);
        }


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

                //PathInformation partPath_Quality = PathHelper.String2PartPathInformation(QualityPartPath);   //commented on 2020-05-15
                /*_RestDataServiceClientQuality.GetParts(partPath_Quality).Wait();*/
                //_RestDataServiceClientQuality.GetParts().Wait();    commented by ashok
                //IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientQuality.GetParts(partPath_Quality); //commented on 2020-05-15
                IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientQuality.GetParts();

                //IEnumerable<InspectionPlanPart> parts = await _RestDataServiceClientQuality.GetParts(partPath_Quality);
                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                if (partsArray.Any())
                {
                    //_CurrentPartQuality = partsArray.First();  //For verification purpose commented this on 2020-05-16
                    _CurrentPartQuality = partsArray[1];
                }

                //IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClientQuality.GetCharacteristics(partPath_Quality);  //commented on 2020-05-15
                IEnumerable<InspectionPlanCharacteristic> chars = await _RestDataServiceClientQuality.GetCharacteristics();
                InspectionPlanCharacteristic[] charsArray = chars as InspectionPlanCharacteristic[] ?? chars.ToArray();
                if (charsArray.Any())
                {
                    _CurrentCharacteristicsQuality = charsArray;
                }

                try
                {
                    GetMeasurements_Qualty(QualityPartPath, partsArray);
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

        }
        private async void GetMeasurements_Qualty(String QualityPartPath, IEnumerable<InspectionPlanPart> parts)
        {
            try
            {
                //IntoFile("Creating a measurement and Its values");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                InspectionPlanPart[] partsArray = parts as InspectionPlanPart[] ?? parts.ToArray();
                List<tbllivehmiscreen> hmidatalist = new List<tbllivehmiscreen>();
                string CorrectedDate = GetCorrectedDate();
                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                    hmidatalist = db.tbllivehmiscreens.Where(m => m.CorrectedDate == CorrectedDate  && m.PartNo!=null && m.Date!= null).OrderByDescending(m=>m.HMIID).ToList();
                }
                var partnumbers = hmidatalist.Select(m =>Convert.ToString(m.PartNo)).Distinct().ToList();
                
                //IEnumerable<InspectionPlanPart> partsdet;
                var det = parts.Select(m =>m.Path.Name.ToString()).ToList();

                var det1 = det.Where(m => partnumbers.Contains(m.ToString())).ToList();
                List<QUALITY> finalQuality = new List<QUALITY>();

                foreach (string partpathdet in det1)
                {
                    QUALITY qulaity_I = new QUALITY();
                    List<QualityData> qualitydat = new List<QualityData>();
                    string partpathdet1 = partpathdet;

                    //if (partsArray.Any())
                    //{
                    //    //_CurrentPartQuality = partpathdet;
                    //    partpathdet1 = _CurrentPartQuality.Path.ToString();
                    //}
                    string[] data = partpathdet1.Split('/');
                    qulaity_I.PartNumber = partpathdet1;
                    //if (data.Length >= 2)    //For verification purpose we added = condition on 2020-05-16
                    //{

                    //    qulaity_I.PartNumber = partpathdet1.Split('/')[1];   //changed the array element 2 to 1 because element 2 was not there. on 2020-05-16
                    //}
                    PathInformation parthdet1 = PathHelper.String2PartPathInformation(partpathdet1);

                    if (parthdet1.Name != "")
                    {
                        SimpleMeasurement[] meas = await _RestDataServiceClientQuality.GetMeasurements(parthdet1);
                        SimpleMeasurement[] measArray = meas as DataMeasurement[] ?? meas.ToArray();



                        //if (TypeofInsp == 0)
                        {
                            if (measArray.Any())
                            {
                                _dataMeasurementQuality = meas;
                                //string startdate = CorrectedDate + " 01:30:00";
                                //DateTime DTstart = Convert.ToDateTime(startdate);

                                DateTime StartDate = Convert.ToDateTime(CorrectedDate + " 00:30:00");
                                DateTime EndDate = StartDate.AddDays(1);

                                var datar = meas.ToList().Where(m => m.Time >= StartDate).OrderBy(m=>m.Time).ToList();

                                List<SimpleMeasurement> data1 = meas.ToList();
                                //var OP101 = data1.Where(m => m.Attributes.ToString().Contains("K15")).ToList();
                                //List<Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute> OP10 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4051")).ToList();
                                foreach (SimpleMeasurement dataMeasurement in datar)
                                {
                                    QualityData data_Q = new QualityData();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP10 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4051")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute Workorder = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K8")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute PartIdent = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K14")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute Status = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K96")).FirstOrDefault();
                                    //int K4051 = dataMeasurement.Attributes.ToList().IndexOf(dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4051")).FirstOrDefault());
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP20 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4052")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP30 = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4053")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute MachinieID = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4151")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP10Time = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K3286")).FirstOrDefault();
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP20Time = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K3193")).FirstOrDefault();
                                   Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OP30Time = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K3197")).FirstOrDefault();
                                    if (MachinieID==null)
                                        MachinieID = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K9")).FirstOrDefault();
                                    //Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute MachinieID = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K9")).FirstOrDefault();

                                    DateTime dt = Convert.ToDateTime( dataMeasurement.Time.Value.ToString());
                                    string Meas_DateTime = dt.ToString();
                                    //String MeasUUID = dataMeasurement.Uuid.ToString();
                                    if (dt != null)
                                         Meas_DateTime = dataMeasurement.Time.Value.AddHours(5).AddMinutes(30).ToString();
                                    else
                                    {
                                        IntoFile("error");
                                    }
                                    //IntoFile("Quality MachineId " + MachinieID);
                                    Zeiss.IMT.PiWeb.Api.DataService.Rest.Attribute OperationConfirmation = dataMeasurement.Attributes.ToList().Where(m => m.ToString().Contains("K4054")).FirstOrDefault();
                                    if (MachinieID != null && PartIdent != null && Convert.ToInt32( MachinieID.Value)!= 1)
                                    {
                                        data_Q.MachineID = Convert.ToInt32(MachinieID.Value);

                                        if (OperationConfirmation != null)
                                        {
                                            if(OperationConfirmation.Value == "5")
                                            {
                                                data_Q.OperationNum = 40;
                                                data_Q.PartIdentity = PartIdent.Value;
                                                data_Q.MeasureDateTime = Convert.ToDateTime(OP30Time.Value);// Meas_DateTime);
                                                if (Status.Value == "2")
                                                {
                                                    data_Q.Op40 = 0;
                                                    data_Q.Op30 = 1;
                                                    data_Q.Op10 = 1;
                                                    data_Q.Op20 = 1;
                                                    data_Q.Status = 2; //Rejected
                                                }
                                                else if (Status.Value == "1")
                                                {
                                                    data_Q.Op40 = 1;
                                                    data_Q.Op10 = 1;  //op10 conifirmed
                                                    data_Q.Op20 = 1;  //op20 Confirmed
                                                    data_Q.Op30 = 1; //op30 Confirmed
                                                    data_Q.Status = 1; //Approved
                                                }
                                                else
                                                {
                                                    data_Q.Op40 = 0;
                                                    data_Q.Op10 = 0;
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op20 = 0;
                                                }
                                                if (Workorder != null)
                                                {
                                                    data_Q.WorkOrderNum = Workorder.Value;
                                                }
                                                //}
                                                else
                                                {
                                                    data_Q.Op40 = 0;
                                                    data_Q.Op10 = 0;
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op20 = 0;
                                                }
                                            }
                                            else if (OperationConfirmation.Value == "3")      //OP30 
                                            {
                                                //if (OP30.Value == "30")
                                                //{
                                                data_Q.OperationNum = 30;
                                                data_Q.PartIdentity = PartIdent.Value;
                                                data_Q.MeasureDateTime = Convert.ToDateTime(OP30Time.Value);// Meas_DateTime);
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
                                                if (Workorder != null)
                                                {
                                                    data_Q.WorkOrderNum = Workorder.Value;
                                                }
                                                //}
                                                else
                                                {
                                                    data_Q.Op10 = 0;
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op20 = 0;
                                                }
                                            }
                                            else if (OperationConfirmation.Value == "2")     //OP20 
                                            {
                                                //if (OP20.Value == "20")
                                                //{
                                                data_Q.OperationNum = 20;

                                                data_Q.PartIdentity = PartIdent.Value;
                                                data_Q.MeasureDateTime = Convert.ToDateTime(OP20Time.Value);// Meas_DateTime);
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
                                                if (Workorder != null)
                                                {
                                                    data_Q.WorkOrderNum = Workorder.Value;
                                                }

                                                //}
                                                else
                                                {
                                                    data_Q.Op10 = 0;
                                                    data_Q.Op30 = 0;
                                                    data_Q.Op20 = 0;
                                                }


                                            }

                                            else if (OperationConfirmation.Value == "1")     //OP10 
                                            {
                                                //if (OP10.Value == "10")   // verification purpouse commented this on 2020-05-16
                                                //{
                                                data_Q.OperationNum = 10;

                                                data_Q.Op20 = 0;
                                                data_Q.Op30 = 0;
                                                data_Q.PartIdentity = PartIdent.Value;
                                                data_Q.MeasureDateTime = Convert.ToDateTime(OP10Time.Value);// Meas_DateTime);
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
                                                if (Workorder != null)
                                                {
                                                    data_Q.WorkOrderNum = Workorder.Value;
                                                }

                                                //}
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
                                            //break;
                                        }
                                    }

                                }
                                //GetLockData(out LockValue, out UnlockValue); // With serial communication

                                sw.Stop();
                            }

                        }
                    }
                    if (!string.IsNullOrEmpty(qulaity_I.PartNumber))
                    {

                        qulaity_I.QualityDatameasurements = qualitydat;
                        finalQuality.Add(qulaity_I);
                    }
                }
                insertQualityDB(finalQuality);
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Quality getting measurement: typeof inspec: \r\n", ex.ToString()));
            }

            try
            {  
                IntoFile("Quality Piweb data has been inserted ");
            }
            catch (Exception ex)
            {
                IntoFile(String.Format("Error Quality getting measurement: typeof inspec: \r\n", ex.ToString()));
            }

        }
        #endregion


        private string GetCorrectedDate()
        {
            string CorrectedDate = "";
            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
            {
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
            }
            return CorrectedDate;
        }

        private void insertQualityDB(List<QUALITY> finalQuality)
        {
            IntoFile("Getting into method");
            try
            {
                if (finalQuality.Count > 0)
                {
                    IntoFile("finalQuality Count " + finalQuality.Count);
                    int? MachineID = 0;
                    string CorrectedDate = GetCorrectedDate();
                    foreach (QUALITY row in finalQuality)
                    {
                        List<QualityData> rawdataqualtiy = row.QualityDatameasurements;

                        foreach (QualityData rowq in rawdataqualtiy)
                        {
                            QualtiyRaw_piweb rawQualtiy = new QualtiyRaw_piweb();
                            MachineID = rowq.MachineID;
                            string workorderno = rowq.WorkOrderNum;
                            int? opno = rowq.OperationNum;

                            if (MachineID != 0)
                            {
                                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                                {

                                    rawQualtiy = db.QualtiyRaw_piweb.Where(m => m.CorrectedDate == CorrectedDate && m.IsPiweb == 0 && m.PartNumber == row.PartNumber && m.PartIdentity == rowq.PartIdentity && m.MachineID == rowq.MachineID).FirstOrDefault();

                                }
                                //IntoFile("Inserting into raw data ");

                                if (rawQualtiy == null)
                                {
                                    QualtiyRaw_piweb rawQ = new QualtiyRaw_piweb();
                                    rawQ.CorrectedDate = CorrectedDate;
                                    rawQ.MachineID = rowq.MachineID;
                                    rawQ.OP10 = rowq.Op10;
                                    rawQ.OP20 = rowq.Op20;
                                    rawQ.OP30 = rowq.Op30;
                                    rawQ.OP40 = rowq.Op40;  //Need to add op40 col in quality piweb
                                    rawQ.OperationNumber = rowq.OperationNum;
                                    rawQ.PartIdentity = rowq.PartIdentity;
                                    rawQ.Status = rowq.Status;
                                    rawQ.WorkOrderNumber = rowq.WorkOrderNum;
                                    rawQ.IsPiweb = 0;
                                    rawQ.Meas_DateTime = rowq.MeasureDateTime;
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
                                        rawQ.OP40 = rowq.Op40;  //Need to add op40 col in quality piweb
                                        rawQ.OperationNumber = rowq.OperationNum;
                                        rawQ.PartIdentity = rowq.PartIdentity;
                                        rawQ.Status = rowq.Status;
                                        rawQ.Meas_DateTime = rowq.MeasureDateTime;
                                        using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                                        {

                                            db1.Entry(rawQ).State = System.Data.Entity.EntityState.Modified;
                                            db1.SaveChanges();
                                        }
                                    }
                                }

                            }
                            if (MachineID != 0)
                            {
                                formulateQuality(CorrectedDate, row.PartNumber, Convert.ToInt32(MachineID),workorderno,Convert.ToInt32(opno));
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

        private void formulateQuality(string correctedDate, string Partnum, int machineid, string work_order_no, int opno)
        {
            try
            {
                List<QualtiyRaw_piweb> rawdata = new List<QualtiyRaw_piweb>();  
                DateTime StartDate =Convert.ToDateTime( correctedDate + " 06:00:00");
                DateTime EndDate = StartDate.AddDays(1);

                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                    rawdata = db.QualtiyRaw_piweb.Where(m => m.CorrectedDate == correctedDate && m.PartNumber == Partnum && m.MachineID == machineid && m.Meas_DateTime>=StartDate && m.Meas_DateTime<=EndDate).ToList();
                }
                if (rawdata.Count > 0)
                {
                    var workordernumlist = rawdata.Select(m => m.WorkOrderNumber).Distinct().ToList();
                    foreach (var objworkorder in workordernumlist) {
                        string workordernum = objworkorder;
                        machineid = rawdata.Select(m => Convert.ToInt32(m.MachineID)).FirstOrDefault();
                        int op10count = rawdata.Where(m => ((m.OP10 == 1 && m.Status == 1) || (m.OP10 == 1 && m.Status == 2)) && m.WorkOrderNumber == workordernum).ToList().Count;  //approved quntity without rejection
                        int op20count = rawdata.Where(m => (m.OP10 == 1 && (m.OP20 == 1 && m.Status == 1) || (m.OP20 == 1 && m.Status == 2)) && m.WorkOrderNumber == workordernum).ToList().Count; //approved quntity without rejection
                        int op30count = rawdata.Where(m => ((m.OP10 == 1 && m.OP20 == 1) && (m.OP30 == 1 && m.Status == 1) || (m.OP30 == 1 && m.Status == 2)) && m.WorkOrderNumber == workordernum).ToList().Count; //approved quntity without rejection
                        int op40count = rawdata.Where(m => ((m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 1) && (m.OP40 == 1 && m.Status == 1 ) || (m.OP40 == 1 && m.Status == 2)) && m.WorkOrderNumber == workordernum).ToList().Count; //approved quntity without rejection


                        int op10countRej = rawdata.Where(m => m.OP10 == 0 && m.Status == 2 && m.WorkOrderNumber == workordernum).ToList().Count;  //OP10 Rejection Qty
                        int op20countRej = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 0 && m.Status == 2 && m.WorkOrderNumber == workordernum).ToList().Count; //OP20 Rejection Qty
                        int op30countRej = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 0 && m.Status == 2 && m.WorkOrderNumber == workordernum).ToList().Count; //OP30 Rejection Qty
                        int op40countRej = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 1 && m.OP40 == 0 && m.Status == 2 && m.WorkOrderNumber == workordernum).ToList().Count; //OP30 Rejection Qty

                        int totalOp10Count = rawdata.Where(m => (m.OP10 == 1 || (m.OP10 == 0 && m.Status == 2)) && (m.WorkOrderNumber == workordernum)).ToList().Count;
                        int totalOp20Count = rawdata.Where(m => (m.OP20 == 1 || (m.OP10 == 1 && m.OP20 == 0 && m.Status == 2)) && (m.WorkOrderNumber == workordernum)).ToList().Count;
                        int totalOp30Count = rawdata.Where(m => (m.OP30 == 1 || (m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 0 && m.Status == 2)) && (m.WorkOrderNumber == workordernum)).ToList().Count;
                        int totalOp40Count = rawdata.Where(m => (m.OP40 == 1 || (m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 1 && m.OP40 == 0 && m.Status == 2)) && (m.WorkOrderNumber == workordernum)).ToList().Count;

                        //int totalOp10Count = rawdata.Where(m => (m.OP10 == 1 || (m.OP10 == 0)) && (m.WorkOrderNumber == workordernum)).ToList().Count;
                        //int totalOp20Count = rawdata.Where(m => (m.OP20 == 1 || (m.OP20 == 0)) && (m.WorkOrderNumber == workordernum)).ToList().Count;
                        //int totalOp30Count = rawdata.Where(m => (m.OP30 == 1 || (m.OP30 == 0)) && (m.WorkOrderNumber == workordernum)).ToList().Count;



                        if (op10count != null)     //op10count.count > 0
                        {
                            tblQuality_Piweb singlerow = new tblQuality_Piweb();

                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {
                                singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 10 && m.MachineID == machineid && m.PartNumber == Partnum && m.WorkOrderNumber == workordernum).FirstOrDefault();


                            }

                            if (singlerow == null)
                            {
                                tblQuality_Piweb row = new tblQuality_Piweb();
                                row.CorrectedDate = correctedDate;
                                row.IsPiweb = 0;
                                row.MachineID = machineid;
                                row.OperationNum = 10;
                                row.PartNumber = Partnum;
                                row.RejectedQty = op10countRej; //RejQty
                                row.ApprovedQty = op10count; //APPROVEDQTY
                                row.TotalQty = totalOp10Count;
                                row.WorkOrderNumber = workordernum;
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

                                singlerow.RejectedQty = op10countRej;
                                singlerow.ApprovedQty = op10count;
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
                                singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 20 && m.MachineID == machineid && m.PartNumber == Partnum && m.WorkOrderNumber == workordernum).FirstOrDefault();

                            }
                            if (singlerow == null)
                            {
                                tblQuality_Piweb row = new tblQuality_Piweb();
                                row.CorrectedDate = correctedDate;
                                row.IsPiweb = 0;
                                row.MachineID = machineid;
                                row.OperationNum = 20;
                                row.PartNumber = Partnum;
                                row.ApprovedQty = op20count;
                                row.RejectedQty = op20countRej;
                                row.TotalQty = totalOp20Count;
                                row.WorkOrderNumber = workordernum;
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

                                singlerow.ApprovedQty = op20count;
                                singlerow.RejectedQty = op20countRej;

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
                                singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 30 && m.MachineID == machineid && m.PartNumber == Partnum && m.WorkOrderNumber == workordernum).FirstOrDefault();

                            }
                            if (singlerow == null)
                            {

                                tblQuality_Piweb row = new tblQuality_Piweb();
                                row.CorrectedDate = correctedDate;
                                row.IsPiweb = 0;
                                row.MachineID = machineid;
                                row.OperationNum = 30;
                                row.PartNumber = Partnum;
                                row.ApprovedQty = op30count;
                                row.RejectedQty = op30countRej;
                                row.TotalQty = totalOp30Count;
                                row.WorkOrderNumber = workordernum;
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

                                singlerow.ApprovedQty = op30count;
                                singlerow.RejectedQty = op30countRej;
                                singlerow.TotalQty = totalOp30Count;
                                using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                                {

                                    db1.Entry(singlerow).State = System.Data.Entity.EntityState.Modified;
                                    db1.SaveChanges();
                                }
                            }
                        }
                        if (op40count != null)
                        {
                            tblQuality_Piweb singlerow = new tblQuality_Piweb();

                            using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                            {
                                singlerow = db.tblQuality_Piweb.Where(m => m.CorrectedDate == correctedDate && m.OperationNum == 40 && m.MachineID == machineid && m.PartNumber == Partnum && m.WorkOrderNumber == workordernum).FirstOrDefault();

                            }
                            if (singlerow == null)
                            {

                                tblQuality_Piweb row = new tblQuality_Piweb();
                                row.CorrectedDate = correctedDate;
                                row.IsPiweb = 0;
                                row.MachineID = machineid;
                                row.OperationNum = 40;
                                row.PartNumber = Partnum;
                                row.ApprovedQty = op40count;
                                row.RejectedQty = op40countRej;
                                row.TotalQty = totalOp40Count;
                                row.WorkOrderNumber = workordernum;
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

                                singlerow.ApprovedQty = op40count;
                                singlerow.RejectedQty = op40countRej;
                                singlerow.TotalQty = totalOp40Count;
                                using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
                                {

                                    db1.Entry(singlerow).State = System.Data.Entity.EntityState.Modified;
                                    db1.SaveChanges();
                                }
                            }
                        }
                    } 
                }
                if (machineid != 0)
                {
                    Insertintolivehmi(correctedDate, Partnum, Convert.ToInt32(machineid),work_order_no, opno);
                }
            }  

            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        //wo, op, part

        private void Insertintolivehmi(string correctedDate, string Partnum, int machineid, string workorderno,int operationno)
        {
            try
            {
                string opno = Convert.ToString(operationno);
                using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                {
                   
                    var hmidata = db.tbllivehmiscreens.Where(m =>m.Date != null && m.Time == null && m.CorrectedDate == correctedDate).ToList();
                    
                    IntoFile("hmidata count :" + hmidata.Count);
                   // var qualitycount = rawdata.Count;

                        if (hmidata.Count >0)
                    {
                        //var workordernumlist = rawdata.Select(m => m.WorkOrderNumber).Distinct().ToList();
                        foreach (var objworkorder in hmidata)
                        {
                            //IntoFile("hmidata" + objworkorder.PartNo + "Machineid: " + objworkorder.MachineID);
                            int quaopno = Convert.ToInt32(objworkorder.OperationNo);
                            var rawdata = db.QualtiyRaw_piweb.Where(m =>m.PartNumber == objworkorder.PartNo && m.MachineID == objworkorder.MachineID && m.WorkOrderNumber == objworkorder.Work_Order_No && m.OperationNumber == quaopno && m.Meas_DateTime >= objworkorder.Date).ToList();
                            IntoFile("PartNumber :" + objworkorder.PartNo + " Machineid: " + objworkorder.MachineID + "work_order_No: " + objworkorder.Work_Order_No);
                            if (rawdata != null)
                            {   
                                if (quaopno == 10)
                                {
                                    objworkorder.Delivered_Qty = rawdata.Where(m => (m.OP10 == 1 || (m.OP10 == 0 && m.Status == 2)) && (m.WorkOrderNumber == objworkorder.Work_Order_No)).ToList().Count;
                                    objworkorder.Rej_Qty = rawdata.Where(m => m.OP10 == 0 && m.Status == 2 && m.WorkOrderNumber == objworkorder.Work_Order_No).ToList().Count;
                                    IntoFile("Delivered_qty :" + objworkorder.Delivered_Qty + "Rej_Qty: " + objworkorder.Rej_Qty + "OPNO: " + 10 + "WorkOrderNumber: " + objworkorder.Work_Order_No);
                                }
                                if(quaopno == 20)
                                {
                                    objworkorder.Delivered_Qty = rawdata.Where(m => (m.OP20 == 1 || (m.OP10 == 1 && m.OP20 == 0 && m.Status == 2)) && (m.WorkOrderNumber == objworkorder.Work_Order_No)).ToList().Count;
                                    objworkorder.Rej_Qty = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 0 && m.Status == 2 && m.WorkOrderNumber == objworkorder.Work_Order_No).ToList().Count;
                                    IntoFile("Delivered_qty :" + objworkorder.Delivered_Qty + "Rej_Qty: " + objworkorder.Rej_Qty + "OPNO :" + 20 + "WorkOrderNumber: " + objworkorder.Work_Order_No);
                                }
                                   
                                if(quaopno == 30)
                                {
                                    objworkorder.Delivered_Qty = rawdata.Where(m => (m.OP30 == 1 || (m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 0 && m.Status == 2)) && (m.WorkOrderNumber == objworkorder.Work_Order_No)).ToList().Count;
                                    objworkorder.Rej_Qty = rawdata.Where(m => m.OP10 == 1 && m.OP20 == 1 && m.OP30 == 0 && m.Status == 2 && m.WorkOrderNumber == objworkorder.Work_Order_No).ToList().Count;
                                    IntoFile("Delivered_qty :" + objworkorder.Delivered_Qty + "Rej_Qty: " + objworkorder.Rej_Qty + "OPNO :" + 30 + "WorkOrderNumber: " + objworkorder.Work_Order_No);
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
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
