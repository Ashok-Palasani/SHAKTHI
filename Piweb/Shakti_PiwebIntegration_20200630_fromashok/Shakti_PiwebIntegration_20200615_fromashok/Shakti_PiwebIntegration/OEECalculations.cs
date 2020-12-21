using i_facilitylibrary;
using i_facilitylibrary.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Shakti_PiwebIntegration
{
    public class OEECalculations
    {
        private IConnectionFactory _conn;
        private Dao obj1 = new Dao();
        private Dao1 obj = new Dao1();
        public void CalculateOEEToday(string CorrectedDate, int MachineID, string ipAddress)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); DateTime CurrentTime = System.DateTime.Now;
            DateTime StartTime = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 06:00:00"));

            if (CurrentTime.Hour >= 0 && CurrentTime.Hour < 6)
            {
                StartTime = StartTime.AddDays(-1);
                //fromdate = fromdate.AddDays(-1);
            }

            DateTime UsedDateForExcel = Convert.ToDateTime(CorrectedDate);
            //double TotalDay = todate.Subtract(fromdate).TotalDays;
            #region

            //string ipAddress = GetIPAddressOf();
            List<i_facilitylibrary.DAL.tbloeedashboardvariablestoday> OEEDataPresent = obj.GettbloeeListDet3(MachineID, UsedDateForExcel);
            // var OEEDataPresent = db.tbloeedashboardvariablestodays.Where(m => m.WCID == MachineID && m.StartDate == UsedDateForExcel && m.IPAddress == ipAddress).ToList();
            if (OEEDataPresent.Count == 0)
            {
                double green, blue, OperatingTime = 0, DownTimeBreakdown = 0, ROALossess = 0, SettingTime = 0;
                double SummationOfSCTvsPP = 0, MinorLosses = 0;
                double ScrapQtyTime = 0, ReWOTime = 0;

                MinorLosses = GetMinorLossesToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID, "YELLOW");
                if (MinorLosses < 0)
                {
                    MinorLosses = 0;
                }
                blue = GetOPIDleBreakDownToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID, "BLUE");
                green = GetOPIDleBreakDownToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID, "GREEN");

                //Availability
                SettingTime = GetSettingTimeToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID);
                if (SettingTime < 0)
                {
                    SettingTime = 0;
                }
                ROALossess = GetDownTimeLossesToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID, "ROA");
                if (ROALossess < 0)
                {
                    ROALossess = 0;
                }
                DownTimeBreakdown = GetDownTimeBreakdownToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID);
                if (DownTimeBreakdown < 0)
                {
                    DownTimeBreakdown = 0;
                }

                //Performance
                SummationOfSCTvsPP = GetSummationOfSCTvsPPToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID);
                if (SummationOfSCTvsPP <= 0)
                {
                    SummationOfSCTvsPP = 0;
                }
                //ROPLosses = GetDownTimeLosses(UsedDateForExcel.ToString("yyyy-MM-dd"), MachineID, "ROP");

                //Quality
                //ScrapQtyTime = GetScrapQtyTimeOfWOToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID);

                //Qualityfactor with Piweb
                ScrapQtyTime= QualityFactor_Piweb(MachineID, UsedDateForExcel.ToString("yyyy-MM-dd"));
                if (ScrapQtyTime < 0)
                {
                    ScrapQtyTime = 0;
                }
                ReWOTime = GetScrapQtyTimeOfRWOToday(UsedDateForExcel.ToString("yyyy-MM-dd"), StartTime.ToString("yyyy-MM-dd HH:mm:ss"), CurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), MachineID);
                if (ReWOTime < 0)
                {
                    ReWOTime = 0;
                }

                //if (DateTime.Now.Hour > 6)
                //{
                //    AvailableTime = (DateTime.Now - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 06:00:00"))).TotalMinutes;
                //}
                //else
                //{
                //    AvailableTime = (DateTime.Now - Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 06:00:00"))).TotalMinutes;
                //}
                OperatingTime = green;

                //To get Top 5 Losses for this WC
                string todayAsCorrectedDate = UsedDateForExcel.ToString("yyyy-MM-dd");
                List<i_facilitylibrary.DAL.tbllossofentry> lossData = obj.GettbllossofentryDet2(MachineID, todayAsCorrectedDate);
                //var lossData = db.tbllossofentries.Where(m => m.CorrectedDate == todayAsCorrectedDate && m.MachineID == MachineID).ToList();

                DataTable DTLosses = new DataTable();
                DTLosses.Columns.Add("lossCodeID", typeof(int));
                DTLosses.Columns.Add("LossDuration", typeof(int));
                foreach (i_facilitylibrary.DAL.tbllossofentry row in lossData)
                {
                    int lossCodeID = Convert.ToInt32(row.MessageCodeID);
                    DateTime startDate = Convert.ToDateTime(row.StartDateTime);
                    DateTime endDate = Convert.ToDateTime(row.EndDateTime);
                    int duration = Convert.ToInt32(endDate.Subtract(startDate).TotalMinutes);

                    DataRow dr = DTLosses.Select("lossCodeID= '" + lossCodeID + "'").FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        int LossDurationPrev = Convert.ToInt32(dr["LossDuration"]); //get lossduration and update it.
                        dr["LossDuration"] = (LossDurationPrev + duration);
                    }
                    //}
                    else
                    {
                        DTLosses.Rows.Add(lossCodeID, duration);
                    }
                }
                DataTable DTLossesTop5 = DTLosses.Clone();
                //get only the rows you want
                DataRow[] results = DTLosses.Select("", "LossDuration DESC");
                //populate new destination table
                if (DTLosses.Rows.Count > 0)
                {
                    int num = DTLosses.Rows.Count;
                    for (int iDT = 0; iDT < num; iDT++)
                    {
                        if (results[iDT] != null)
                        {
                            DTLossesTop5.ImportRow(results[iDT]);
                        }
                        else
                        {
                            DTLossesTop5.Rows.Add(0, 0);
                        }
                        if (iDT == 4)
                        {
                            break;
                        }
                    }
                    if (num < 5)
                    {
                        for (int iDT = num; iDT < 5; iDT++)
                        {
                            DTLossesTop5.Rows.Add(0, 0);
                        }
                    }
                }
                else
                {
                    for (int iDT = 0; iDT < 5; iDT++)
                    {
                        DTLossesTop5.Rows.Add(0, 0);
                    }
                }
                //Gather LossValues
                string lossCode1, lossCode2, lossCode3, lossCode4, lossCode5 = null;
                int lossCodeVal1, lossCodeVal2, lossCodeVal3, lossCodeVal4, lossCodeVal5 = 0;

                lossCode1 = Convert.ToString(DTLossesTop5.Rows[0][0]);
                lossCode2 = Convert.ToString(DTLossesTop5.Rows[1][0]);
                lossCode3 = Convert.ToString(DTLossesTop5.Rows[2][0]);
                lossCode4 = Convert.ToString(DTLossesTop5.Rows[3][0]);
                lossCode5 = Convert.ToString(DTLossesTop5.Rows[4][0]);
                lossCodeVal1 = Convert.ToInt32(DTLossesTop5.Rows[0][1]);
                lossCodeVal2 = Convert.ToInt32(DTLossesTop5.Rows[1][1]);
                lossCodeVal3 = Convert.ToInt32(DTLossesTop5.Rows[2][1]);
                lossCodeVal4 = Convert.ToInt32(DTLossesTop5.Rows[3][1]);
                lossCodeVal5 = Convert.ToInt32(DTLossesTop5.Rows[4][1]);
                string PlantIDS = null, ShopIDS = null, CellIDS = null;
                int value;
                i_facilitylibrary.DAL.tblmachinedetail WCData = obj1.GetmacDetails(MachineID);
                //var WCData = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.MachineID == MachineID).FirstOrDefault();
                string TempVal = WCData.PlantID.ToString();
                if (int.TryParse(TempVal, out value))
                {
                    PlantIDS = value.ToString();
                }

                TempVal = WCData.ShopID.ToString();
                if (int.TryParse(TempVal, out value))
                {
                    ShopIDS = value.ToString();
                }

                TempVal = WCData.CellID.ToString();
                if (int.TryParse(TempVal, out value))
                {
                    CellIDS = value.ToString();
                }


                //Now insert into table
                using (MsqlConnection mcInsertRows = new MsqlConnection())
                {
                    try
                    {
                        mcInsertRows.open();
                        SqlCommand cmdInsertRows = new SqlCommand("INSERT INTO [i_facility_shakti].[dbo].tbloeedashboardvariablestoday(PlantID,ShopID,CellID,WCID,StartDate,EndDate,MinorLosses,Blue,Green,SettingTime,ROALossess,DownTimeBreakdown,SummationOfSCTvsPP,ScrapQtyTime,ReWOTime,Loss1Name,Loss1Value,Loss2Name,Loss2Value,Loss3Name,Loss3Value,Loss4Name,Loss4Value,Loss5Name,Loss5Value,CreatedOn,CreatedBy,IsDeleted,IPAddress)VALUES('" + PlantIDS + "','" + ShopIDS + "','" + CellIDS + "','" + MachineID + "','" + UsedDateForExcel.ToString("yyyy-MM-dd") + "','" + UsedDateForExcel.ToString("yyyy-MM-dd") + "','" + MinorLosses + "','" + blue + "','" + green + "','" + SettingTime + "','" + ROALossess + "','" + DownTimeBreakdown + "','" + SummationOfSCTvsPP + "','" + ScrapQtyTime + "','" + ReWOTime + "','" + lossCode1 + "','" + lossCodeVal1 + "','" + lossCode2 + "','" + lossCodeVal2 + "','" + lossCode3 + "','" + lossCodeVal3 + "','" + lossCode4 + "','" + lossCodeVal4 + "','" + lossCode5 + "','" + lossCodeVal5 + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + 1 + "','" + 0 + "','" + ipAddress + "' );", mcInsertRows.msqlConnection);
                        cmdInsertRows.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    finally
                    {
                        mcInsertRows.close();
                    }
                }
            }
            UsedDateForExcel = UsedDateForExcel.AddDays(+1);
            #endregion

            //now push to tbloeedashboardFinalVariables.
        }

        public void CalculateSummarizedOEEWC(DateTime fromdate, DateTime todate, int FactorID, string TimeType, string SummarizeAs, string ipAddress)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn);
            DateTime UsedDateForExcel = Convert.ToDateTime(fromdate);
            double TotalDay = todate.Subtract(fromdate).TotalDays;

            if (TimeType == "Today")
            {
                todate = fromdate;
                #region
                //now push to tbloeedashboardFinalVariables.
                double OEEFactor, AvaillabilityFactor, EfficiencyFactor, QualityFactor;
                //string ipAddress = GetIPAddressOf();

                //Gather Hierarchy Details
                string PlantIDS = null, ShopIDS = null, CellIDS = null, WCID = null;
                List<i_facilitylibrary.DAL.tbloeedashboardvariablestoday> getWC = obj.GettbloeecellListDet1(FactorID, ipAddress, fromdate, todate);
                //var getWC = db.tbloeedashboardvariablestodays.Where(m => m.IsDeleted == 0 && m.CellID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate && m.IPAddress == ipAddress).Select(m => m.WCID).Distinct().ToList();

                if (SummarizeAs == "Plant")
                {
                    getWC = obj.GettbloeepalntListDet1(FactorID, ipAddress, fromdate, todate);
                    //getWC = db.tbloeedashboardvariablestodays.Where(m => m.IsDeleted == 0 && m.PlantID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate && m.IPAddress == ipAddress).Select(m => m.WCID).Distinct().ToList();
                }
                else if (SummarizeAs == "Shop")
                {
                    getWC = obj.GettbloeeshopListDet1(FactorID, ipAddress, fromdate, todate);
                    // getWC = db.tbloeedashboardvariablestodays.Where(m => m.IsDeleted == 0 && m.ShopID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate && m.IPAddress == ipAddress).Select(m => m.WCID).Distinct().ToList();
                }
                else if (SummarizeAs == "Cell")
                {
                    getWC = obj.GettbloeecellListDet1(FactorID, ipAddress, fromdate, todate);
                    // getWC = db.tbloeedashboardvariablestodays.Where(m => m.IsDeleted == 0 && m.CellID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate && m.IPAddress == ipAddress).Select(m => m.WCID).Distinct().ToList();
                }
                else if (SummarizeAs == "WorkCentre")
                {
                    getWC = obj.GettbloeeWCListDet1(FactorID, ipAddress, fromdate, todate);
                    //getWC = db.tbloeedashboardvariablestodays.Where(m => m.IsDeleted == 0 && m.WCID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate && m.IPAddress == ipAddress).Select(m => m.WCID).Distinct().ToList();
                }
                int once = 0;

                foreach (i_facilitylibrary.DAL.tbloeedashboardvariablestoday WCRow in getWC)
                {
                    double green = 0, blue = 0, OperatingTime = 0, DownTimeBreakdown = 0, ROALossess = 0, AvailableTime = 0, SettingTime = 0;
                    double SummationOfSCTvsPP = 0, MinorLosses = 0;
                    double ScrapQtyTime = 0, ReWOTime = 0;
                    int id = Convert.ToInt32(WCRow.WCID);
                    List<i_facilitylibrary.DAL.tbloeedashboardvariablestoday> OEEDataToSummarize = obj.GettbloeeWCListDet2(id, ipAddress, fromdate, todate);
                    //var OEEDataToSummarize = db.tbloeedashboardvariablestodays.Where(m => m.WCID == WCRow && m.StartDate >= fromdate && m.StartDate <= todate && m.IPAddress == ipAddress).ToList();
                    foreach (i_facilitylibrary.DAL.tbloeedashboardvariablestoday row in OEEDataToSummarize)
                    {
                        if (once == 0)
                        {
                            PlantIDS = Convert.ToString(row.PlantID);
                            ShopIDS = Convert.ToString(row.ShopID);
                            CellIDS = Convert.ToString(row.CellID);
                            WCID = Convert.ToString(row.WCID);
                            once++;
                        }

                        MinorLosses += Convert.ToDouble(row.MinorLosses);
                        blue += Convert.ToDouble(row.Blue);
                        green += Convert.ToDouble(row.Green);

                        //Availability
                        SettingTime += Convert.ToDouble(row.SettingTime);
                        ROALossess += Convert.ToDouble(row.ROALossess);
                        DownTimeBreakdown += Convert.ToDouble(row.DownTimeBreakdown);

                        //Performance
                        SummationOfSCTvsPP += Convert.ToDouble(row.SummationOfSCTvsPP);

                        //Quality
                        ScrapQtyTime += Convert.ToDouble(row.ScrapQtyTime);
                        ReWOTime += Convert.ToDouble(row.ReWOTime);
                    }

                    if (TimeType == "Today")
                    {
                        if (DateTime.Now.Hour > 6)
                        {
                            AvailableTime = (DateTime.Now - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 06:00:00"))).TotalMinutes;
                        }
                        else
                        {
                            AvailableTime = (DateTime.Now - Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 06:00:00"))).TotalMinutes;
                        }
                    }
                    //double planned_Duration = GetPlannedDuration();
                    double planned_Duration = get_PlanBreakdownduration();
                    if (AvailableTime > planned_Duration)
                        AvailableTime -= planned_Duration;
                    #region Getting the Top 5 Losses
                        //Have to Calculate based on Summarization condition, use code in CalculateOEE().
                    string GetDistinctLossQuery = "Select Distinct f.lossname from" +
                                             "(" +
                                             "Select Distinct a.Loss1Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday a where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                             "Union All " +
                                             "Select Distinct b.Loss2Name  as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday b where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                             "Union All " +
                                             "Select Distinct c.Loss3Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday c where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                             "Union All " +
                                             "Select Distinct d.Loss4Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday d where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                             "Union All " +
                                             "Select Distinct e.Loss5Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday e where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                             ") f order by f.lossname desc";
                    DataTable GetDistinctLossDT = new DataTable();
                    using (MsqlConnection mc = new MsqlConnection())
                    {
                        mc.open();
                        SqlDataAdapter GetDistinctLossDA = new SqlDataAdapter(GetDistinctLossQuery, mc.msqlConnection);
                        GetDistinctLossDA.Fill(GetDistinctLossDT);
                        mc.close();
                    }
                    //Gather LossValues
                    string lossCode1 = null, lossCode2 = null, lossCode3 = null, lossCode4 = null, lossCode5 = null;
                    int lossCodeVal1 = 0, lossCodeVal2 = 0, lossCodeVal3 = 0, lossCodeVal4 = 0, lossCodeVal5 = 0;
                    List<KeyValuePair<String, Double>> LossDetails = new List<KeyValuePair<string, double>>();
                    int LossCount = GetDistinctLossDT.Rows.Count;
                    for (int i = 0; i < LossCount; i++)
                    {
                        String LossName = GetDistinctLossDT.Rows[i][0].ToString();
                        double LossDuration = 0;
                        String GetLossDurationQuery = "Select SUM(Total) From " +
                                       "(Select SUM(Loss1Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday " +
                                       "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss1Name = '" + LossName + "'  " +
                                       "Union all " +
                                       "Select SUM(Loss2Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday  " +
                                       "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss2Name = '" + LossName + "'  " +
                                       "Union All " +
                                       "Select SUM(Loss3Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday  " +
                                       "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss3Name = '" + LossName + "' " +
                                       "Union All " +
                                       "Select SUM(Loss4Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday  " +
                                       "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss4Name = '" + LossName + "' " +
                                       "Union All " +
                                       "Select SUM(Loss5Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariablestoday " +
                                       "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss5Name = '" + LossName + "' " +
                                       ") z;";

                        DataTable GetLossDurationDT = new DataTable();
                        using (MsqlConnection mc = new MsqlConnection())
                        {
                            mc.open();
                            SqlDataAdapter GetLossDurationDA = new SqlDataAdapter(GetLossDurationQuery, mc.msqlConnection);
                            GetLossDurationDA.Fill(GetLossDurationDT);
                            mc.close();
                        }
                        if (GetLossDurationDT.Rows.Count > 0)
                        {
                            string value = Convert.ToString(GetLossDurationDT.Rows[0][0]);
                            if (Double.TryParse(value, out LossDuration))
                            {
                                LossDuration = LossDuration;
                            }
                        }
                        LossDetails.Add(new KeyValuePair<string, double>(LossName, LossDuration));
                    }
                    #endregion

                    List<KeyValuePair<string, double>> losslist = LossDetails.OrderByDescending(m => m.Value).ToList();
                    int losscount = losslist.Count;

                    for (int i = 0; i < losscount; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                lossCode1 = losslist[i].Key.ToString();
                                lossCodeVal1 = (int)losslist[i].Value;
                                break;
                            case 1:
                                lossCode2 = losslist[i].Key.ToString();
                                lossCodeVal2 = (int)losslist[i].Value;
                                break;
                            case 2:
                                lossCode3 = losslist[i].Key.ToString();
                                lossCodeVal3 = (int)losslist[i].Value;
                                break;
                            case 3:
                                lossCode4 = losslist[i].Key.ToString();
                                lossCodeVal4 = (int)losslist[i].Value;
                                break;
                            case 4:
                                lossCode5 = losslist[i].Key.ToString();
                                lossCodeVal5 = (int)losslist[i].Value;
                                break;
                        }
                    }

                    //OEE Values

                    OperatingTime = green;
                    //Availability Factor
                    //double val = OperatingTime / AvailableTime;
                    double val = (OperatingTime + MinorLosses) / AvailableTime;  //including Minorloss and Availablity with planned duration.
                    AvaillabilityFactor = Math.Round(val * 100, 2);
                    if (AvaillabilityFactor > 0 && AvaillabilityFactor < 100)
                    {
                        AvaillabilityFactor = Math.Round(val * 100, 2);
                    }
                    else
                    {
                        if (AvaillabilityFactor > 100)
                        {
                            AvaillabilityFactor = 100;
                        }
                        else if (AvaillabilityFactor < 0)
                        {
                            AvaillabilityFactor = 0;
                        }
                    }

                    if (AvaillabilityFactor > 0)
                    {
                        //Performance(Efficiency) Factor
                        if (OperatingTime == 0)
                        {
                            EfficiencyFactor = 100;
                        }
                        else
                        {
                            EfficiencyFactor = Math.Round((SummationOfSCTvsPP / (OperatingTime)) * 100, 2);
                            if (SummationOfSCTvsPP == -1 || SummationOfSCTvsPP == 0)
                            {
                                EfficiencyFactor = 100;
                            }
                            else
                            {
                                EfficiencyFactor = Math.Round((SummationOfSCTvsPP / (OperatingTime)) * 100, 2);
                                if (EfficiencyFactor >= 0 && EfficiencyFactor <= 100)
                                {
                                }
                                else if (EfficiencyFactor > 100 || EfficiencyFactor < 0)
                                {
                                    EfficiencyFactor = 100;
                                }
                            }
                        }

                        //QualityFactor
                        if (OperatingTime == 0)
                        {
                            QualityFactor = 0;
                            /*QualityFactor = 100;*/ //for verification purpose we had comment above line and added this one on 2020-05-15
                        }
                        else
                        {
                            //QualityFactor = (OperatingTime - ScrapQtyTime - ReWOTime) / OperatingTime;
                            QualityFactor=ScrapQtyTime;  

                            if (QualityFactor >= 0 && QualityFactor <= 100)
                            {
                                //QualityFactor = Math.Round(QualityFactor * 100, 2); //for verification purpose we had comment above line and added this one on 2020-05-17
                                QualityFactor = Math.Round(QualityFactor);
                                //QualityFactor = 100;   //for verification purpose we had comment above line and added this one on 2020-05-15
                            }
                            else if (QualityFactor > 100 || QualityFactor < 0)
                            {
                                QualityFactor = 100;
                            }
                        }
                    }
                    //else if (AvaillabilityFactor == 0)    //for verification purpose we had comment above line and added this one on 2020-05-15
                    //{
                    //    QualityFactor = 0; EfficiencyFactor = 0;
                    //}
                    else
                    {
                        EfficiencyFactor = 0; QualityFactor = 0;

                        //QualityFactor = 100; //for verification purpose we had comment above line and added this one on 2020-05-15
                    }

                    //OEE Factor
                    if (AvaillabilityFactor <= 0 || EfficiencyFactor <= 0 || QualityFactor <= 0)
                    {
                        OEEFactor = 0;
                    }
                    else
                    {
                        OEEFactor = Math.Round((AvaillabilityFactor / 100) * (EfficiencyFactor / 100) * (QualityFactor / 100) * 100, 2);
                        if (OEEFactor >= 0 && OEEFactor <= 100)
                        {
                            OEEFactor = Math.Round(OEEFactor, 2);
                        }
                        else if (OEEFactor > 100)
                        {
                            OEEFactor = 100;
                        }
                        else if (OEEFactor < 0)
                        {
                            OEEFactor = 0;
                        }
                    }

                    string IPAddress = ipAddress;
                    int isShopWise = 0;
                    int isCellWise = 0;
                    int isWCWise = 1;

                    //Now insert into table
                    using (MsqlConnection mcFinalInsertRows = new MsqlConnection())
                    {
                        mcFinalInsertRows.open();
                        try
                        {
                            string query = "INSERT INTO  [i_facility_shakti].[dbo].tbloeedashboardfinalvariables(PlantID,ShopID,CellID,WCID,StartDate,EndDate,OEE,Availability,Performance,Quality,IsOverallShopWise,IsOverallCellWise,IsOverallWCWise,Loss1Name,Loss1Value,Loss2Name,Loss2Value,Loss3Name,Loss3Value,Loss4Name,Loss4Value,Loss5Name,Loss5Value,IPAddress,CreatedOn,CreatedBy,IsDeleted,IsToday)VALUES('" + Convert.ToInt32(PlantIDS) + "','" + Convert.ToInt32(ShopIDS) + "','" + Convert.ToInt32(CellIDS) + "','" + Convert.ToInt32(WCRow.WCID) + "','" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + OEEFactor + "','" + AvaillabilityFactor + "','" + EfficiencyFactor + "','" + QualityFactor + "','" + isShopWise + "','" + isCellWise + "','" + isWCWise + "','" + lossCode1 + "','" + lossCodeVal1 + "','" + lossCode2 + "','" + lossCodeVal2 + "','" + lossCode3 + "','" + lossCodeVal3 + "','" + lossCode4 + "','" + lossCodeVal4 + "','" + lossCode5 + "','" + lossCodeVal5 + "','" + IPAddress + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + 1 + "','" + 0 + "','" + 1 + "');";
                            SqlCommand cmdFinalInsertRows = new SqlCommand("INSERT INTO [i_facility_shakti].[dbo].tbloeedashboardfinalvariables(PlantID,ShopID,CellID,WCID,StartDate,EndDate,OEE,Availability,Performance,Quality,IsOverallShopWise,IsOverallCellWise,IsOverallWCWise,Loss1Name,Loss1Value,Loss2Name,Loss2Value,Loss3Name,Loss3Value,Loss4Name,Loss4Value,Loss5Name,Loss5Value,IPAddress,CreatedOn,CreatedBy,IsDeleted,IsToday)VALUES('" + Convert.ToInt32(PlantIDS) + "','" + Convert.ToInt32(ShopIDS) + "','" + Convert.ToInt32(CellIDS) + "','" + Convert.ToInt32(WCRow.WCID) + "','" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + OEEFactor + "','" + AvaillabilityFactor + "','" + EfficiencyFactor + "','" + QualityFactor + "','" + isShopWise + "','" + isCellWise + "','" + isWCWise + "','" + lossCode1 + "','" + lossCodeVal1 + "','" + lossCode2 + "','" + lossCodeVal2 + "','" + lossCode3 + "','" + lossCodeVal3 + "','" + lossCode4 + "','" + lossCodeVal4 + "','" + lossCode5 + "','" + lossCodeVal5 + "','" + IPAddress + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + 1 + "','" + 0 + "','" + 1 + "');", mcFinalInsertRows.msqlConnection);
                            cmdFinalInsertRows.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            //operationlog log1 = new operationlog();
                            //log1.MachineID = FactorID;
                            string OpMsg = " Value of Query " + WCRow + " " + e.Message;
                            DateTime? OpDate = System.DateTime.Now.Date;
                            DateTime? OpDateTime = System.DateTime.Now;
                            TimeSpan? OpTime = System.DateTime.Now.TimeOfDay;

                            obj.InsertoperationlogDetails(FactorID, OpMsg, OpDate, OpDateTime, OpTime);
                        }
                        finally
                        {
                            mcFinalInsertRows.close();
                        }
                    }
                }
                #endregion
            }
            //else if (TimeType == "GodHours" || TimeType == "NoBlue")
            //{
            //    //now push to tbloeedashboardFinalVariables.
            //    double OEEFactor, AvaillabilityFactor, EfficiencyFactor, QualityFactor;

            //    //Gather Hierarchy Details
            //    string PlantIDS = null, ShopIDS = null, CellIDS = null, WCID = null;
            //    var getWC = obj.GettbloeecellvariableListDet1(FactorID, fromdate, todate);
            //    //var getWC = db.tbloeedashboardvariables.Where(m => m.IsDeleted == 0 && m.CellID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate).Select(m => m.WCID).Distinct().ToList();

            //    if (SummarizeAs == "Plant")
            //    {
            //        getWC = obj.GettbloeepalntvariableListDet1(FactorID, fromdate, todate);
            //        //getWC = db.tbloeedashboardvariables.Where(m => m.IsDeleted == 0 && m.PlantID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate).Select(m => m.WCID).Distinct().ToList();
            //    }
            //    else if (SummarizeAs == "Shop")
            //    {
            //        getWC = obj.GettbloeeshopvariableListDet1(FactorID, fromdate, todate);
            //        //getWC = db.tbloeedashboardvariables.Where(m => m.IsDeleted == 0 && m.ShopID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate).Select(m => m.WCID).Distinct().ToList();
            //    }
            //    else if (SummarizeAs == "Cell")
            //    {
            //        getWC = obj.GettbloeecellvariableListDet1(FactorID, fromdate, todate);
            //        //getWC = db.tbloeedashboardvariables.Where(m => m.IsDeleted == 0 && m.CellID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate).Select(m => m.WCID).Distinct().ToList();
            //    }
            //    else if (SummarizeAs == "WorkCentre")
            //    {
            //        getWC = obj.GettbloeeWCvariableListDet1(FactorID, fromdate, todate);
            //        //getWC = db.tbloeedashboardvariables.Where(m => m.IsDeleted == 0 && m.WCID == FactorID && m.StartDate >= fromdate && m.StartDate <= todate).Select(m => m.WCID).Distinct().ToList();
            //    }
            //    int once = 0;

            //    foreach (var WCRow in getWC)
            //    {
            //        double green = 0, red = 0, yellow = 0, blue = 0, setup = 0, scrap = 0, OperatingTime = 0, DownTimeBreakdown = 0, ROALossess = 0, AvailableTime = 0, SettingTime = 0, PlannedDownTime = 0, UnPlannedDownTime = 0;
            //        double SummationOfSCTvsPP = 0, MinorLosses = 0, ROPLosses = 0;
            //        double ScrapQtyTime = 0, ReWOTime = 0, ROQLosses = 0;
            //        int id = Convert.ToInt32(WCRow.WCID);
            //        var OEEDataToSummarize = obj.GettbloeeWCvariableListDet1(id, fromdate, todate);
            //        // var OEEDataToSummarize = db.tbloeedashboardvariables.Where(m => m.WCID == WCRow && m.StartDate >= fromdate && m.StartDate <= todate).ToList();
            //        foreach (var row in OEEDataToSummarize)
            //        {
            //            if (once == 0)
            //            {
            //                PlantIDS = Convert.ToString(row.PlantID);
            //                ShopIDS = Convert.ToString(row.ShopID);
            //                CellIDS = Convert.ToString(row.CellID);
            //                WCID = Convert.ToString(row.WCID);
            //                once++;
            //            }

            //            MinorLosses += Convert.ToDouble(row.MinorLosses);
            //            blue += Convert.ToDouble(row.Blue);
            //            green += Convert.ToDouble(row.Green);

            //            //Availability
            //            SettingTime += Convert.ToDouble(row.SettingTime);
            //            ROALossess += Convert.ToDouble(row.ROALossess);
            //            DownTimeBreakdown += Convert.ToDouble(row.DownTimeBreakdown);

            //            //Performance
            //            SummationOfSCTvsPP += Convert.ToDouble(row.SummationOfSCTvsPP);

            //            //Quality
            //            ScrapQtyTime += Convert.ToDouble(row.ScrapQtyTime);
            //            ReWOTime += Convert.ToDouble(row.ReWOTime);
            //        }

            //        int Days = Convert.ToInt32(todate.Subtract(fromdate).TotalDays) + 1;
            //        if (TimeType == "GodHours")
            //        {
            //            AvailableTime = 24 * 60 * Days; //24Hours to Minutes;
            //        }
            //        else if (TimeType == "NoBlue")
            //        {
            //            AvailableTime = (24 * 60 * Days) - blue; //24Hours to Minutes;
            //        }

            //        #region Getting the Top 5 Losses
            //        //Have to Calculate based on Summarization condition, use code in CalculateOEE().
            //        /*     string GetDistinctLossQuery = "Select Distinct f.lossname from" +
            //                                      "(" +
            //                                      "Select Distinct a.Loss1Name as lossname from "+ MsqlConnection.DbName + ".tbloeedashboardvariables a where WCID = " + WCRow + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                      "Union All " +
            //                                      "Select Distinct b.Loss2Name  as lossname from "+ MsqlConnection.DbName + ".tbloeedashboardvariables b where WCID = " + WCRow + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                      "Union All " +
            //                                      "Select Distinct c.Loss3Name as lossname from "+ MsqlConnection.DbName + ".tbloeedashboardvariables c where WCID = " + WCRow + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                      "Union All " +
            //                                      "Select Distinct d.Loss4Name as lossname from "+ MsqlConnection.DbName + ".tbloeedashboardvariables d where WCID = " + WCRow + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                      "Union All " +
            //                                      "Select Distinct e.Loss5Name as lossname from "+ MsqlConnection.DbName + ".tbloeedashboardvariables e where WCID = " + WCRow + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                      ") f order by f.lossname desc";*/
            //        string GetDistinctLossQuery = "Select Distinct f.lossname from" +
            //                                 "(" +
            //                                 "Select Distinct a.Loss1Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariables a where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                 "Union All " +
            //                                 "Select Distinct b.Loss2Name  as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariables b where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                 "Union All " +
            //                                 "Select Distinct c.Loss3Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariables c where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                 "Union All " +
            //                                 "Select Distinct d.Loss4Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariables d where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                 "Union All " +
            //                                 "Select Distinct e.Loss5Name as lossname from " + MsqlConnection.DbName + ".tbloeedashboardvariables e where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
            //                                 ") f order by f.lossname desc";
            //        MsqlConnection mc = new MsqlConnection();
            //        mc.open();
            //        SqlDataAdapter GetDistinctLossDA = new SqlDataAdapter(GetDistinctLossQuery, mc.msqlConnection);
            //        DataTable GetDistinctLossDT = new DataTable();
            //        GetDistinctLossDA.Fill(GetDistinctLossDT);
            //        mc.close();
            //        //Gather LossValues
            //        string lossCode1 = null, lossCode2 = null, lossCode3 = null, lossCode4 = null, lossCode5 = null;
            //        int lossCodeVal1 = 0, lossCodeVal2 = 0, lossCodeVal3 = 0, lossCodeVal4 = 0, lossCodeVal5 = 0;
            //        List<KeyValuePair<String, Double>> LossDetails = new List<KeyValuePair<string, double>>();
            //        int LossCount = GetDistinctLossDT.Rows.Count;
            //        for (int i = 0; i < LossCount; i++)
            //        {
            //            String LossName = GetDistinctLossDT.Rows[i][0].ToString();
            //            double LossDuration = 0;
            //            String GetLossDurationQuery = "Select SUM(Total) From " +
            //                           "(Select SUM(Loss1Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariables " +
            //                           "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss1Name = '" + LossName + "'  " +
            //                           "Union all " +
            //                           "Select SUM(Loss2Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariables  " +
            //                           "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss2Name = '" + LossName + "'  " +
            //                           "Union All " +
            //                           "Select SUM(Loss3Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariables  " +
            //                           "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss3Name = '" + LossName + "' " +
            //                           "Union All " +
            //                           "Select SUM(Loss4Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariables  " +
            //                           "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss4Name = '" + LossName + "' " +
            //                           "Union All " +
            //                           "Select SUM(Loss5Value)  as Total From " + MsqlConnection.DbName + ".tbloeedashboardvariables " +
            //                           "Where WCID = " + WCRow.WCID + " and StartDate Between '" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Loss5Name = '" + LossName + "' " +
            //                           ") z;";
            //            mc.open();
            //            SqlDataAdapter GetLossDurationDA = new SqlDataAdapter(GetLossDurationQuery, mc.msqlConnection);
            //            DataTable GetLossDurationDT = new DataTable();
            //            GetLossDurationDA.Fill(GetLossDurationDT);
            //            mc.close();
            //            if (GetLossDurationDT.Rows.Count > 0)
            //            {
            //                string value = Convert.ToString(GetLossDurationDT.Rows[0][0]);
            //                if (Double.TryParse(value, out LossDuration))
            //                    LossDuration = LossDuration;
            //            }
            //            LossDetails.Add(new KeyValuePair<string, double>(LossName, LossDuration));
            //        }
            //        #endregion

            //        var losslist = LossDetails.OrderByDescending(m => m.Value).ToList();
            //        int losscount = losslist.Count;

            //        for (int i = 0; i < losscount; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    lossCode1 = losslist[i].Key.ToString();
            //                    lossCodeVal1 = (int)losslist[i].Value;
            //                    break;
            //                case 1:
            //                    lossCode2 = losslist[i].Key.ToString();
            //                    lossCodeVal2 = (int)losslist[i].Value;
            //                    break;
            //                case 2:
            //                    lossCode3 = losslist[i].Key.ToString();
            //                    lossCodeVal3 = (int)losslist[i].Value;
            //                    break;
            //                case 3:
            //                    lossCode4 = losslist[i].Key.ToString();
            //                    lossCodeVal4 = (int)losslist[i].Value;
            //                    break;
            //                case 4:
            //                    lossCode5 = losslist[i].Key.ToString();
            //                    lossCodeVal5 = (int)losslist[i].Value;
            //                    break;
            //            }
            //        }

            //        //OEE Values

            //        OperatingTime = green;
            //        //Availability Factor
            //        double val = OperatingTime / AvailableTime;
            //        AvaillabilityFactor = Math.Round(val * 100, 2);
            //        if (AvaillabilityFactor > 0 && AvaillabilityFactor < 100)
            //        {
            //            AvaillabilityFactor = Math.Round(val * 100, 2);
            //        }
            //        else
            //        {
            //            if (AvaillabilityFactor > 100)
            //            {
            //                AvaillabilityFactor = 100;
            //            }
            //            else if (AvaillabilityFactor < 0)
            //            {
            //                AvaillabilityFactor = 0;
            //            }
            //        }

            //        if (AvaillabilityFactor > 0)
            //        {
            //            //Performance(Efficiency) Factor
            //            if (OperatingTime == 0)
            //            {
            //                EfficiencyFactor = 100;
            //            }
            //            else
            //            {

            //                EfficiencyFactor = Math.Round((SummationOfSCTvsPP / (OperatingTime)) * 100, 2);
            //                if (SummationOfSCTvsPP == -1 || SummationOfSCTvsPP == 0)
            //                {
            //                    EfficiencyFactor = 100;
            //                }
            //                else
            //                {
            //                    EfficiencyFactor = Math.Round((SummationOfSCTvsPP / (OperatingTime)) * 100, 2);
            //                    if (EfficiencyFactor >= 0 && EfficiencyFactor <= 100)
            //                    {
            //                    }
            //                    else if (EfficiencyFactor > 100 || EfficiencyFactor < 0)
            //                    {
            //                        EfficiencyFactor = 100;
            //                    }
            //                }
            //            }
            //            //QualityFactor
            //            if (OperatingTime == 0)
            //            {
            //                QualityFactor = 0;
            //            }
            //            else
            //            {
            //                QualityFactor = (OperatingTime - ScrapQtyTime - ReWOTime) / OperatingTime;
            //                if (QualityFactor >= 0 && QualityFactor <= 100)
            //                {
            //                    QualityFactor = Math.Round(QualityFactor * 100, 2);
            //                }
            //                else if (QualityFactor > 100 || QualityFactor < 0)
            //                {
            //                    QualityFactor = 100;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            EfficiencyFactor = 0; QualityFactor = 0;
            //        }

            //        //OEE Factor
            //        if (AvaillabilityFactor <= 0 || EfficiencyFactor <= 0 || QualityFactor <= 0)
            //        {
            //            OEEFactor = 0;
            //        }
            //        else
            //        {
            //            OEEFactor = Math.Round((AvaillabilityFactor / 100) * (EfficiencyFactor / 100) * (QualityFactor / 100) * 100, 2);
            //            if (OEEFactor >= 0 && OEEFactor <= 100)
            //            {
            //                OEEFactor = Math.Round(OEEFactor, 2);
            //            }
            //            else if (OEEFactor > 100)
            //            {
            //                OEEFactor = 100;
            //            }
            //            else if (OEEFactor < 0)
            //            {
            //                OEEFactor = 0;
            //            }
            //        }

            //        //string IPAddress = GetIPAddressOf();
            //        int isShopWise = 0;
            //        int isCellWise = 0;
            //        int isWCWise = 1;

            //        //Now insert into table
            //        MsqlConnection mcFinalInsertRows = new MsqlConnection();
            //        mcFinalInsertRows.open();
            //        try
            //        {
            //            SqlCommand cmdFinalInsertRows = new SqlCommand("INSERT INTO tbloeedashboardfinalvariables(PlantID,ShopID,CellID,WCID,StartDate,EndDate,OEE,Availability,Performance,Quality,IsOverallShopWise,IsOverallCellWise,IsOverallWCWise,Loss1Name,Loss1Value,Loss2Name,Loss2Value,Loss3Name,Loss3Value,Loss4Name,Loss4Value,Loss5Name,Loss5Value,IPAddress,CreatedOn,CreatedBy,IsDeleted)VALUES('" + Convert.ToInt32(PlantIDS) + "','" + Convert.ToInt32(ShopIDS) + "','" + Convert.ToInt32(CellIDS) + "','" + Convert.ToInt32(WCRow.WCID) + "','" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + OEEFactor + "','" + AvaillabilityFactor + "','" + EfficiencyFactor + "','" + QualityFactor + "','" + isShopWise + "','" + isCellWise + "','" + isWCWise + "','" + lossCode1 + "','" + lossCodeVal1 + "','" + lossCode2 + "','" + lossCodeVal2 + "','" + lossCode3 + "','" + lossCodeVal3 + "','" + lossCode4 + "','" + lossCodeVal4 + "','" + lossCode5 + "','" + lossCodeVal5 + "','" + IPAddress + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + 1 + "','" + 0 + "');", mcFinalInsertRows.msqlConnection);
            //            // cmdFinalInsertRows.ExecuteNonQuery();

            //            //SqlCommand cmdFinalInsertRows = new SqlCommand("INSERT INTO tbloeedashboardfinalvariables(PlantID,ShopID,CellID,WCID,StartDate,EndDate,OEE,Availability,Performance,Quality,IsOverallShopWise,IsOverallWCWise,Loss1Name,Loss1Value,Loss2Name,Loss2Value,Loss3Name,Loss3Value,Loss4Name,Loss4Value,Loss5Name,Loss5Value,IPAddress,CreatedOn,CreatedBy,IsDeleted,IsToday)VALUES('" + Convert.ToInt32(PlantIDS) + "','" + Convert.ToInt32(ShopIDS) + "','" + Convert.ToInt32(CellIDS) + "','" + Convert.ToInt32(WCID) + "','" + fromdate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + todate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + OEEFactor + "','" + AvaillabilityFactor + "','" + EfficiencyFactor + "','" + QualityFactor + "','" + isShopWise + "','" + isCellWise + "','" + lossCode1 + "','" + lossCodeVal1 + "','" + lossCode2 + "','" + lossCodeVal2 + "','" + lossCode3 + "','" + lossCodeVal3 + "','" + lossCode4 + "','" + lossCodeVal4 + "','" + lossCode5 + "','" + lossCodeVal5 + "','" + IPAddress + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + 1 + "','" + 0 + "','" + 1 + "');", mcFinalInsertRows.msqlConnection);
            //            cmdFinalInsertRows.ExecuteNonQuery();
            //            mcFinalInsertRows.close();
            //        }
            //        catch (Exception e)
            //        {
            //            string OpMsg = " Value of Query " + WCRow + " " + e.Message;
            //            DateTime? OpDate = System.DateTime.Now.Date;
            //            DateTime? OpDateTime = System.DateTime.Now;
            //            TimeSpan? OpTime = System.DateTime.Now.TimeOfDay;

            //            obj.InsertoperationlogDetails(FactorID, OpMsg, OpDate, OpDateTime, OpTime);
            //            //operationlog log1 = new operationlog();
            //            //log1.MachineID = FactorID;
            //            //log1.OpMsg = " Value of Query " + WCRow + " " + e.Message;
            //            //log1.OpDate = System.DateTime.Now.Date;
            //            //log1.OpDateTime = System.DateTime.Now;
            //            //log1.OpTime = System.DateTime.Now.TimeOfDay;
            //            //db.operationlogs.Add(log1);
            //            //db.SaveChanges();
            //        }
            //        finally
            //        {
            //            mcFinalInsertRows.close();
            //        }
            //    }
            //}
        }

        //2017-04-05 //Get Minor Loss for Today OEE Calculation - Completed
        public int GetMinorLossesToday(string CorrectedDate, String StartTime, String EndTime, int MachineID, string Colour)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); DateTime currentdate = Convert.ToDateTime(CorrectedDate);
            string datetime = currentdate.ToString("yyyy-MM-dd");
            int minorloss = 0;
            //int count = 0;
            DataTable GetMinorLossDT = new DataTable();
            using (MsqlConnection mc = new MsqlConnection())
            {
                //mc.open();
                //String GetMinorLoss = "SELECT ColorCode FROM tbllivedailyprodstatus WHERE MachineID = " + MachineID + " AND IsDeleted = 0 AND CorrectedDate = '" + CorrectedDate + "' AND StartTime BETWEEN '" + StartTime + "' AND '" + EndTime + "';";
                //SqlDataAdapter GetMinorLossDA = new SqlDataAdapter(GetMinorLoss, mc.msqlConnection);
                //GetMinorLossDA.Fill(GetMinorLossDT);
                //mc.close();
                mc.open();
                String GetMinorLossNew = "SELECT SUM(DurationInSec) from [i_facility_shakti].[dbo].[tbllivemode] where MachineID = " + MachineID + " and IsDeleted = 0 and CorrectedDate = '" + CorrectedDate + "' and ColorCode = '" + Colour + "' and DurationInSec < 120  and IsCompleted = 1;";
                SqlDataAdapter GetMinorLossNewDA = new SqlDataAdapter(GetMinorLossNew, mc.msqlConnection);
                GetMinorLossNewDA.Fill(GetMinorLossDT);
                mc.close();
            }
            int DataCount = GetMinorLossDT.Rows.Count;
            if (DataCount > 0)
            {
                String Val = GetMinorLossDT.Rows[0][0].ToString();
                if (GetMinorLossDT.Rows[0][0].ToString() != null && Val != "")
                {
                    minorloss = Convert.ToInt32(GetMinorLossDT.Rows[0][0]) / 60;
                }
            }

            ////var Data = db.tbldailyprodstatus.Where(m => m.IsDeleted == 0 && m.MachineID == MachineID && m.CorrectedDate == CorrectedDate).OrderBy(m => m.StartTime).ToList();
            //for (int i = 0; i < DataCount; i++)
            //{
            //    //foreach (var row in Data)
            //    {
            //        if (GetMinorLossDT.Rows[i][0].ToString().ToUpper() == "YELLOW")
            //        {
            //            count++;
            //        }
            //        else
            //        {
            //            if (count > 0 && count < 2)
            //            {
            //                minorloss += count;
            //                count = 0;

            //            }
            //            count = 0;
            //        }
            //    }
            //}
            return minorloss;
        }

        //Get Idle/BD Loss for Today OEE Calculation - Completed
        public int GetOPIDleBreakDownToday(string CorrectedDate, String StartTime, String EndTime, int MachineID, string Colour)
        {
            int ModeDuration = 0;
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); DateTime currentdate = Convert.ToDateTime(CorrectedDate);
            string datetime = currentdate.ToString("yyyy-MM-dd");

            int[] count = new int[4];
            DataTable OP = new DataTable();
            DataTable RunningOP = new DataTable();
            using (MsqlConnection mc = new MsqlConnection())
            {
                ////operating
                //mc.open();
                //String query1 = "SELECT count(ID) From tbllivedailyprodstatus WHERE CorrectedDate='" + CorrectedDate + "' AND MachineID=" + MachineID + " AND ColorCode='" + Colour + "' AND StartTime BETWEEN '" + StartTime + "' AND '" + EndTime + "';";
                //SqlDataAdapter da1 = new SqlDataAdapter(query1, mc.msqlConnection);
                //da1.Fill(OP);
                //mc.close();

                mc.open();
                String GetDurationQuery = "SELECT SUM(DurationInSec) from [i_facility_shakti].[dbo].[tbllivemode] where MachineID = " + MachineID + " and IsDeleted = 0 and CorrectedDate = '" + CorrectedDate + "' and ColorCode = '" + Colour + "' and IsCompleted = 1;";
                SqlDataAdapter GetDurationDA = new SqlDataAdapter(GetDurationQuery, mc.msqlConnection);
                GetDurationDA.Fill(OP);
                mc.close();

                mc.open();
                String GetRunningDurationQuery = "SELECT StartTime from [i_facility_shakti].[dbo].[tbllivemode] where MachineID = " + MachineID + " and IsDeleted = 0 and CorrectedDate = '" + CorrectedDate + "' and ColorCode = '" + Colour + "' and IsCompleted = 0;";
                SqlDataAdapter GetRunningDurationDA = new SqlDataAdapter(GetRunningDurationQuery, mc.msqlConnection);
                GetRunningDurationDA.Fill(RunningOP);
                mc.close();
            }
            if (OP.Rows.Count != 0)
            {
                String Val = OP.Rows[0][0].ToString();
                if (OP.Rows[0][0].ToString() != null && Val != "")
                {
                    ModeDuration = Convert.ToInt32(OP.Rows[0][0]) / 60;
                }
            }
            if (RunningOP.Rows.Count != 0)
            {
                DateTime StartTimeRunnning = Convert.ToDateTime(RunningOP.Rows[0][0]);
                int DurationRunning = (int)DateTime.Now.Subtract(StartTimeRunnning).TotalSeconds / 60;
                ModeDuration += DurationRunning;
            }
            return ModeDuration;
        }
        //Get Setting Time for Today OEE Calculation - Completed
        public double GetSettingTimeToday(string UsedDateForExcel, String StartTime, String EndTime, int MachineID)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn);
            double settingTime = 0;
            int setupid = 0;
            string settingString = "Setup";
            i_facilitylibrary.DAL.tbllossescode setupiddata = obj.GettbloeelossDet3(settingString);
            //var setupiddata = db.tbllossescodes.Where(m => m.MessageType.Contains(settingString)).FirstOrDefault();
            if (setupiddata != null)
            {
                setupid = setupiddata.LossCodeID;
            }
            else
            {
                //Session["Error"] = "Unable to get Setup's ID";
                return -1;
            }
            //getting all setup's sublevels ids.
            List<string> SettingIDs = obj.GettbllossescodeDet1(setupid);
            //var SettingIDs = dbloss.tbllossescodes.Where(m => (m.LossCodesLevel1ID == setupid || m.LossCodesLevel2ID == setupid)).Select(m => m.LossCodeID).ToList();

            //settingTime = (from row in db.tbllossofentries
            //               where  row.CorrectedDate == UsedDateForExcel && row.MachineID == MachineID );
            foreach (string Setting in SettingIDs)
            {
                DataTable GetSettingTimeDT = new DataTable();
                using (MsqlConnection mc = new MsqlConnection())
                {
                    mc.open();
                    String GetSettingTime = "SELECT * FROM tbllivelossofentry WHERE MachineID = " + MachineID + " AND MessageCodeID = " + Setting + " AND CorrectedDate = '" + UsedDateForExcel + "' AND DoneWithRow = 1 AND StartDateTime BETWEEN '" + StartTime + "' AND '" + EndTime + "';";
                    SqlDataAdapter GetSettingTimeDA = new SqlDataAdapter(GetSettingTime, mc.msqlConnection);
                    GetSettingTimeDA.Fill(GetSettingTimeDT);
                    mc.close();
                }
                int DataCount = GetSettingTimeDT.Rows.Count;
                //var SettingData = db.tbllossofentries.Where(m => SettingIDs.Contains(m.MessageCodeID) && m.MachineID == MachineID && m.CorrectedDate == UsedDateForExcel && m.DoneWithRow == 1).ToList();
                for (int i = 0; i < DataCount; i++)
                {
                    DateTime startTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][2].ToString());
                    DateTime endTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][3].ToString());
                    settingTime += endTime.Subtract(startTime).TotalMinutes;
                }
            }
            return settingTime;
        }
        //Get Downtime Loss for Today OEE Calculation - Completed
        public double GetDownTimeLossesToday(string UsedDateForExcel, String StartTime, String EndTime, int MachineID, string contribute)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); double LossTime = 0;
            //string contribute = "ROA";
            //getting all ROA sublevels ids. Only those of IDLE.
            List<string> SettingIDs = obj.GettbllossescodeDet2(contribute);
            // var SettingIDs = db.tbllossescodes.Where(m => m.ContributeTo == contribute && (m.MessageType != "PM" || m.MessageType != "BREAKDOWN" || m.MessageType != "Setup")).Select(m => m.LossCodeID).ToList();
            DataTable GetSettingTimeDT = new DataTable();
            foreach (string Setting in SettingIDs)
            {
                GetSettingTimeDT.Clear();
                using (MsqlConnection mc = new MsqlConnection())
                {
                    mc.open();
                    String GetSettingTime = "SELECT * FROM tbllivelossofentry WHERE MachineID = " + MachineID + " AND MessageCodeID = " + Setting + " AND CorrectedDate = '" + UsedDateForExcel + "' AND DoneWithRow = 1 AND StartDateTime BETWEEN '" + StartTime + "' AND '" + EndTime + "';";
                    SqlDataAdapter GetSettingTimeDA = new SqlDataAdapter(GetSettingTime, mc.msqlConnection);
                    GetSettingTimeDA.Fill(GetSettingTimeDT);
                    mc.close();
                }
                int DataCount = GetSettingTimeDT.Rows.Count;
                for (int i = 0; i < DataCount; i++)
                {
                    //var SettingData = db.tbllossofentries.Where(m => SettingIDs.Contains(m.MessageCodeID) && m.MachineID == MachineID && m.CorrectedDate == UsedDateForExcel && m.DoneWithRow == 1).ToList();
                    //foreach (var row in SettingData)
                    {
                        DateTime startTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][2].ToString());
                        DateTime endTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][3].ToString());
                        LossTime += endTime.Subtract(startTime).TotalMinutes;
                    }
                }
            }
            return LossTime;
        }
        //Get BreakdownLoss for Today OEE Calculation - Completed
        public double GetDownTimeBreakdownToday(string UsedDateForExcel, String StartTime, String EndTime, int MachineID)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); double LossTime = 0;
            //var BreakdownData = db.tblbreakdowns.Where(m => m.MachineID == MachineID && m.CorrectedDate == UsedDateForExcel && m.DoneWithRow == 1).ToList();
            DataTable GetSettingTimeDT = new DataTable();
            using (MsqlConnection mc = new MsqlConnection())
            {
                mc.open();
                String GetSettingTime = "SELECT * FROM tblbreakdown WHERE MachineID = " + MachineID + " AND CorrectedDate = '" + UsedDateForExcel + "' AND DoneWithRow = 1 AND StartTime BETWEEN '" + StartTime + "' AND '" + EndTime + "';";
                SqlDataAdapter GetSettingTimeDA = new SqlDataAdapter(GetSettingTime, mc.msqlConnection);
                GetSettingTimeDA.Fill(GetSettingTimeDT);
                mc.close();
            }
            int DataCount = GetSettingTimeDT.Rows.Count;
            for (int i = 0; i < DataCount; i++)
            {
                {
                    if ((Convert.ToString(GetSettingTimeDT.Rows[i][2]) == null) || GetSettingTimeDT.Rows[i][2].ToString() == null)
                    {
                        //do nothing
                    }
                    else
                    {
                        DateTime startTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][1].ToString());
                        DateTime endTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][2].ToString());
                        LossTime += endTime.Subtract(startTime).TotalMinutes;
                    }
                }
            }
            return LossTime;
        }
        //Get SCTVsPP for Today OEE Calculation - Completed
        public double GetSummationOfSCTvsPPToday(string UsedDateForExcel, String StartTime, String EndTime, int MachineID)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); double SummationofTime = 0;
            DataTable GetSettingTimeDT = new DataTable();
            using (MsqlConnection mc = new MsqlConnection())
            {
                mc.open();
                String GetSettingTime = "SELECT * FROM tbllivehmiscreen WHERE MachineID = " + MachineID + " AND CorrectedDate = '" + UsedDateForExcel + "' AND isWorkOrder = 0 AND (isWorkInProgress = 1 OR isWorkInProgress = 0);";
                SqlDataAdapter GetSettingTimeDA = new SqlDataAdapter(GetSettingTime, mc.msqlConnection);
                GetSettingTimeDA.Fill(GetSettingTimeDT);
                mc.close();
            }
            int DataCount = GetSettingTimeDT.Rows.Count;
            //var PartsData = db.tblhmiscreens.Where(m => m.CorrectedDate == UsedDateForExcel && m.MachineID == MachineID && m.isWorkOrder == 0 && (m.isWorkInProgress == 1 || m.isWorkInProgress == 0)).ToList();
            if (DataCount == 0)
            {
                return -1;
            }
            for (int i = 0; i < DataCount; i++)
            //foreach (var row in PartsData)
            {
                string partno = GetSettingTimeDT.Rows[i][7].ToString();
                string operationno = GetSettingTimeDT.Rows[i][8].ToString();
                int rejectedQty = 0, deliveredQty = 0;
                string deliveredQtyString = GetSettingTimeDT.Rows[i][12].ToString();
                string rejectedQtyString = QualityFactor_PiwebReject(MachineID,  UsedDateForExcel).ToString();
                rejectedQty = rejectedQtyString != "" ? Convert.ToInt32(rejectedQtyString) : 0;
                deliveredQty = deliveredQtyString != "" ? Convert.ToInt32(deliveredQtyString) : 0;
                int totalpartproduced = deliveredQty + rejectedQty;
                double stdCuttingTime = 0;
                i_facilitylibrary.DAL.tblmasterparts_st_sw stdcuttingTimeData = obj.Gettblmasterparts_st_swDet3(operationno, partno);
                //var stdcuttingTimeData = db.tblmasterparts_st_sw.Where(m => m.IsDeleted == 0 && m.OpNo == operationno && m.PartNo == partno).FirstOrDefault();
                //foreach (var row1 in stdcuttingTimeData)
                if (stdcuttingTimeData != null)
                {
                    string stdcuttingvalString = Convert.ToString(stdcuttingTimeData.StdCuttingTime);
                    Double stdcuttingval = 0;
                    if (double.TryParse(stdcuttingvalString, out stdcuttingval))
                    {
                        stdcuttingval = stdcuttingval;
                    }
                    string Unit = Convert.ToString(stdcuttingTimeData.StdCuttingTimeUnit);
                    if (Unit == "Hrs")
                    {
                        stdCuttingTime = stdcuttingval * 60;
                    }
                    else //Unit is Minutes
                    {
                        stdCuttingTime = stdcuttingval;
                    }
                }
                SummationofTime += stdCuttingTime * totalpartproduced;
            }
            return SummationofTime;
        }
        //Get Scrap Qty Operating Time for Today OEE Calculation - Completed
        public double GetScrapQtyTimeOfWOToday(string UsedDateForExcel, String StartTime, String EndTime, int MachineID)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); double SQT = 0;
            DataTable GetSettingTimeDT = new DataTable();
            using (MsqlConnection mc = new MsqlConnection())
            {
                mc.open();
                String GetSettingTime = "SELECT * FROM tbllivehmiscreen WHERE MachineID = " + MachineID + " AND CorrectedDate = '" + UsedDateForExcel + "' AND isWorkOrder = 0 AND (isWorkInProgress = 1 OR isWorkInProgress = 0) ;";
                SqlDataAdapter GetSettingTimeDA = new SqlDataAdapter(GetSettingTime, mc.msqlConnection);
                GetSettingTimeDA.Fill(GetSettingTimeDT);
                mc.close();
            }
            int DataCount = GetSettingTimeDT.Rows.Count;

            //var PartsData = db.tblhmiscreens.Where(m => m.CorrectedDate == UsedDateForExcel && m.MachineID == MachineID && (m.isWorkInProgress == 1 || m.isWorkInProgress == 0) && m.isWorkOrder == 0).ToList();
            for (int i = 0; i < DataCount; i++)
            //foreach (var row in PartsData)
            {
                string partno = GetSettingTimeDT.Rows[i][7].ToString();
                string operationno = GetSettingTimeDT.Rows[i][8].ToString();
                //int scrapQty = Convert.ToInt32(GetSettingTimeDT.Rows[i][9].ToString());
                //int DeliveredQty = Convert.ToInt32(GetSettingTimeDT.Rows[i][12].ToString());

                int scrapQty = 0;
                int DeliveredQty = 0;
                string scrapQtyString = Convert.ToString(GetSettingTimeDT.Rows[i][9]);
                string DeliveredQtyString = Convert.ToString(GetSettingTimeDT.Rows[i][12]);
                string x = scrapQtyString;
                int value;
                if (int.TryParse(x, out value))
                {
                    scrapQty = value;
                }
                x = DeliveredQtyString;
                if (int.TryParse(x, out value))
                {
                    DeliveredQty = value;
                }

                if (scrapQty != 0)
                {
                    DateTime startTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][4].ToString());
                    DateTime endTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][5].ToString());
                    //Double WODuration = endTimeTemp.Subtract(startTime).TotalMinutes;
                    Double WODuration = GetGreenToday(UsedDateForExcel, startTime, endTime, MachineID);

                    if ((scrapQty + DeliveredQty) == 0)
                    {
                        SQT += 0;
                    }
                    else
                    {
                        SQT += (WODuration / (scrapQty + DeliveredQty)) * scrapQty;
                    }
                }
                else
                {
                    //do nothing
                }
            }
            return SQT;
        }
        //Get ReWork Order Time for Today OEE Calculation - Completed


        /*GOD*/
        public double GetScrapQtyTimeOfRWOToday(string UsedDateForExcel, String StartTime, String EndTime, int MachineID)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn); double SQT = 0;
            DataTable GetSettingTimeDT = new DataTable();
            using (MsqlConnection mc = new MsqlConnection())
            {
                mc.open();
                String GetSettingTime = "SELECT * FROM tbllivehmiscreen WHERE MachineID = " + MachineID + " AND CorrectedDate = '" + UsedDateForExcel + "' AND isWorkOrder = 1 AND (isWorkInProgress = 1 OR isWorkInProgress = 0) ;";
                SqlDataAdapter GetSettingTimeDA = new SqlDataAdapter(GetSettingTime, mc.msqlConnection);
                GetSettingTimeDA.Fill(GetSettingTimeDT);
                mc.close();
            }
            int DataCount = GetSettingTimeDT.Rows.Count;
            //var PartsData = db.tblhmiscreens.Where(m => m.CorrectedDate == UsedDateForExcel && m.MachineID == MachineID && (m.isWorkInProgress == 1 || m.isWorkInProgress == 0) && m.isWorkOrder == 1).ToList();
            for (int i = 0; i < DataCount; i++)
            //foreach (var row in PartsData)
            {
                string partno = GetSettingTimeDT.Rows[i][7].ToString();
                string operationno = GetSettingTimeDT.Rows[i][8].ToString();
                //int scrapQty = Convert.ToInt32(GetSettingTimeDT.Rows[i][9].ToString());
                //int DeliveredQty = Convert.ToInt32(GetSettingTimeDT.Rows[i][12].ToString());

                int scrapQty = 0;
                int DeliveredQty = 0;
                string scrapQtyString = Convert.ToString(GetSettingTimeDT.Rows[i][9]);
                string DeliveredQtyString = Convert.ToString(GetSettingTimeDT.Rows[i][12]);
                string x = scrapQtyString;
                int value;
                if (int.TryParse(x, out value))
                {
                    scrapQty = value;
                }
                x = DeliveredQtyString;
                if (int.TryParse(x, out value))
                {
                    DeliveredQty = value;
                }

                DateTime startTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][4].ToString());
                DateTime endTime = Convert.ToDateTime(GetSettingTimeDT.Rows[i][5].ToString());
                Double WODuration = GetGreenToday(UsedDateForExcel, startTime, endTime, MachineID);

                //Double WODuration = endTime.Subtract(startTime).TotalMinutes;
                //For Availability Loss
                //double Settingtime = GetSetupForReworkLoss(UsedDateForExcel, startTime, endTime, MachineID);
                //double green = GetOT(UsedDateForExcel, startTime, endTime, MachineID);
                //double DownTime = GetDownTimeForReworkLoss(UsedDateForExcel, startTime, endTime, MachineID, "ROA");
                //double BreakdownTime = GetBreakDownTimeForReworkLoss(UsedDateForExcel, startTime, endTime, MachineID);
                //double AL = DownTime + BreakdownTime + Settingtime;

                //For Performance Loss
                //double downtimeROP = GetDownTimeForReworkLoss(UsedDateForExcel, startTime, endTime, MachineID, "ROP");
                //double minorlossWO = GetMinorLossForReworkLoss(UsedDateForExcel, startTime, endTime, MachineID, "YELLOW");
                //double PL = downtimeROP + minorlossWO;

                SQT += WODuration;
            }
            return SQT;
        }
        public double GetGreenToday(string UsedDateForExcel, DateTime TSstartTime, DateTime TSendTime, int MachineID)
        {
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn);
            double settingTime = 0;
            DateTime WOstarttimeDate = Convert.ToDateTime(TSstartTime);
            DateTime WOendtimeDate = Convert.ToDateTime(TSendTime);

            DataTable lossesData = new DataTable();

            using (MsqlConnection mc = new MsqlConnection())
            {
                mc.open();
                String query1 = "SELECT Count(ID) From tbllivedailyprodstatus WHERE MachineID = '" + MachineID + "' and CorrectedDate = '" + UsedDateForExcel + "' and ColorCode = 'green'"
                    + " and ( StartTime >= '" + WOstarttimeDate + "' and EndTime <= '" + WOendtimeDate + "' )";
                SqlDataAdapter da1 = new SqlDataAdapter(query1, mc.msqlConnection);
                da1.Fill(lossesData);
                mc.close();
            }
            if (lossesData.Rows.Count > 0)
            {
                settingTime = Convert.ToDouble(lossesData.Rows[0][0]);
            }
            return settingTime;
        }   
        

        public double QualityFactor_Piweb(int MachineID, string UsedDateForExcel)
        {
            double qualitydata = 0;

         
            using (i_facility_shaktiEntities1 dbhmi = new i_facility_shaktiEntities1())
            {
                var PartsData = dbhmi.tbllivehmiscreens.Where(m => m.CorrectedDate == UsedDateForExcel && m.MachineID == MachineID && (m.isWorkInProgress == 1 || m.isWorkInProgress == 0) && m.isWorkOrder == 0).ToList();
                double totaldelquantity = 0;
                double totalrejqty = 0;
                double totalreworkqty = 0;
                foreach (var row in PartsData)
                {
                    string partno = row.PartNo;
                    int operationno = Convert.ToInt32( row.OperationNo);
                    int scrapQty = 0;
                    int DeliveredQty = 0;

                    var qulaity_row = new tblQuality_Piweb();
                    using(i_facility_shaktiEntities1 db= new i_facility_shaktiEntities1())
                    {
                         qulaity_row = db.tblQuality_Piweb.Where(m => m.PartNumber == partno && m.OperationNum == operationno && m.MachineID == MachineID && m.CorrectedDate == UsedDateForExcel).FirstOrDefault();
                    }
                    int rejectQty = 0;
                    int totalQty = 0;
                    if (qulaity_row != null)
                    {  
                        //totalQty = Convert.ToInt32(qulaity_row.TotalQty); //total qty
                        totalQty= Convert.ToInt32(qulaity_row.ApprovedQty) + Convert.ToInt32(qulaity_row.RejectedQty);    //Approvedqty + Rejectionqty = Delivered Qty
                        rejectQty =  Convert.ToInt32(qulaity_row.RejectedQty); //difference of total qty and approved qty
                    }
                    string scrapQtyString = Convert.ToString(row.Rej_Qty);
                    string DeliveredQtyString = Convert.ToString(row.Delivered_Qty);
                    string x = scrapQtyString;
                    double rework = 0;
                    double deliveredQty = Convert.ToInt32(DeliveredQtyString);

                    scrapQty = rejectQty;
                    DeliveredQty = totalQty;


                    //qualitydata = Convert.ToDouble(((decimal)rejectQty /(decimal) totalQty));   //this condition was commented for verification purpose on 2020-05-17
                    var reworkTime = dbhmi.tbllivehmiscreens.Where(m => m.MachineID == MachineID && m.CorrectedDate == UsedDateForExcel && m.isWorkOrder == 1).Sum(m => m.Delivered_Qty);
                    if(reworkTime != null)
                    {
                        rework = Convert.ToInt32(reworkTime);

                    }

                    totaldelquantity += DeliveredQty; 
                    totalrejqty += scrapQty;
                    totalreworkqty += rework;
                    //qualitydata = ((deliveredQty - scrapQty - rework) / (deliveredQty) * 100);

                }
                if (totaldelquantity == 0)
                    totaldelquantity = 1;
                qualitydata = ((totaldelquantity - totalrejqty - totalreworkqty) / (totaldelquantity) * 100);

            }
            return qualitydata;
        }

        public int QualityFactor_PiwebReject(int MachineID, string UsedDateForExcel)
        {
            int qualitydata = 0;


            using (i_facility_shaktiEntities1 dbhmi = new i_facility_shaktiEntities1())
            {
                var PartsData = dbhmi.tbllivehmiscreens.Where(m => m.CorrectedDate == UsedDateForExcel && m.MachineID == MachineID && (m.isWorkInProgress == 1 || m.isWorkInProgress == 0) && m.isWorkOrder == 0).ToList();
                foreach (var row in PartsData)
                {
                    string partno = row.PartNo;
                    int operationno = Convert.ToInt32(row.OperationNo);
                    int scrapQty = 0;
                    int DeliveredQty = 0;

                    var qulaity_row = new tblQuality_Piweb();
                    using (i_facility_shaktiEntities1 db = new i_facility_shaktiEntities1())
                    {
                        qulaity_row = db.tblQuality_Piweb.Where(m => m.PartNumber == partno && m.OperationNum == operationno && m.MachineID == MachineID && m.CorrectedDate == UsedDateForExcel).FirstOrDefault();
                    }
                    int rejectQty = 0;
                    int totalQty = 0;
                    if (qulaity_row != null)
                    {
                        rejectQty = Convert.ToInt32(qulaity_row.RejectedQty);
                        totalQty = Convert.ToInt32(qulaity_row.TotalQty);

                    }
                    string scrapQtyString = Convert.ToString(row.Rej_Qty);
                    string DeliveredQtyString = Convert.ToString(row.Delivered_Qty);
                    string x = scrapQtyString;

                    scrapQty = rejectQty;
                    DeliveredQty = totalQty;

                    if (totalQty == 0)
                        totalQty = 1;
                    qualitydata = (rejectQty / totalQty);

                    qualitydata = rejectQty;
                }
            }
            return qualitydata;
        }

        public int GetPlannedDuration()
        {
            int ModeDuration = 0;
            _conn = new ConnectionFactory();
            obj1 = new Dao(_conn);
            obj = new Dao1(_conn);            
            int[] count = new int[4];
            DataTable OP = new DataTable();
            string startime = DateTime.Now.ToString("HH:mm:ss");
           
            using (MsqlConnection mc = new MsqlConnection())
            {
                mc.open();
                String GetDurationQuery = "SELECT SUM(BreakDuration) from [i_facility_shakti].[dbo].[tblplannedbreak] where IsDeleted = 0;";
                SqlDataAdapter GetDurationDA = new SqlDataAdapter(GetDurationQuery, mc.msqlConnection);
                GetDurationDA.Fill(OP);
                mc.close();
            }
            if (OP.Rows.Count != 0)
            {
                String Val = OP.Rows[0][0].ToString();
                if (OP.Rows[0][0].ToString() != null && Val != "")
                {
                    ModeDuration = Convert.ToInt32(OP.Rows[0][0]) / 60;
                }
            }
            return ModeDuration;
        }


        public int get_PlanBreakdownduration()
        {

            int duration = 0;
            var msgs2 = new List<tblplannedbreak>();

            using (i_facility_shaktiEntities1 db1 = new i_facility_shaktiEntities1())
            {
                msgs2 = db1.tblplannedbreaks.Where(m => m.IsDeleted == 0).ToList();
            }
            String[] msgtime = DateTime.Now.ToString("HH:mm:00").Split(':');
            TimeSpan msgstime = new TimeSpan(Convert.ToInt32(msgtime[0]), Convert.ToInt32(msgtime[1]), Convert.ToInt32(msgtime[2]));
            TimeSpan s1t1 = new TimeSpan(0, 0, 0), s1t2 = new TimeSpan(0, 0, 0);
            TimeSpan s2t1 = new TimeSpan(0, 0, 0), s2t2 = new TimeSpan(0, 0, 0);
            TimeSpan s3t1 = new TimeSpan(0, 0, 0), s3t2 = new TimeSpan(0, 0, 0), s3t3 = new TimeSpan(0, 0, 0), s3t4 = new TimeSpan(23, 59, 59);


            for (int j = 0; j < msgs2.Count; j++)
            {
                if (msgs2[j].ShiftID.ToString().Contains("1") || msgs2[j].ShiftID.ToString().Contains("A"))
                {
                    String[] s1 = msgs2[j].StartTime.ToString().Split(':');
                    s1t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                    String[] s11 = msgs2[j].EndTime.ToString().Split(':');
                    s1t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));

                    if (msgstime >= s1t1 || msgstime < s1t2)
                    {
                        duration += Convert.ToInt32(msgs2[j].BreakDuration);
                    }
                }
                if (msgs2[j].ShiftID.ToString().Contains("2") || msgs2[j].ShiftID.ToString().Contains("B"))
                {
                    String[] s1 = msgs2[j].StartTime.ToString().Split(':');
                    s2t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                    String[] s11 = msgs2[j].EndTime.ToString().Split(':');
                    s2t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));

                    if (msgstime >= s2t1 && msgstime < s2t2)
                    {
                        duration += Convert.ToInt32(msgs2[j].BreakDuration);
                    }
                }
                if (msgs2[j].ShiftID.ToString().Contains("3") || msgs2[j].ShiftID.ToString().Contains("C"))
                {
                    String[] s1 = msgs2[j].StartTime.ToString().Split(':');
                    s3t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                    String[] s11 = msgs2[j].EndTime.ToString().Split(':');
                    s3t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));


                    if (msgstime >= s3t1 && msgstime < s3t2)
                    {
                        duration += Convert.ToInt32(msgs2[j].BreakDuration);
                    }
                }


            }
           
            return duration;

        }

    }

}
