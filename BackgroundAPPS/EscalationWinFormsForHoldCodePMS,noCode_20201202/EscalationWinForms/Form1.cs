﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using TataMySqlConnection;

namespace EscalationWinForms
{
    public partial class Form1 : Form
    {
        String CorrectedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        bool SSL = Convert.ToBoolean(MsqlConnection.IsSSL);
        public Form1()
        {
            InitializeComponent();

            try
            {
                //ModeMoreThanTenMinutes();

                // EmailEscalationForWC(1654, "PINGFAIL", 2, 1, "2017-02-23");
                //PMS_EMAILEscalationforWC(1, 21, 2, "2020-07-13");
                EscalatEmailForNoCode();

                //var week = DateTime.Now.;

                //Get CorrectedDate & shift
                #region
                MsqlConnection mcp = new MsqlConnection();
                mcp.open();
                String queryshift = "SELECT ShiftName,StartTime,EndTime FROM " + MsqlConnection.DbName + ".tblshift_mstr WHERE IsDeleted = 0";
                SqlDataAdapter dashift = new SqlDataAdapter(queryshift, mcp.msqlConnection);
                DataTable dtshift = new DataTable();
                dashift.Fill(dtshift);
                String[] msgtime = System.DateTime.Now.TimeOfDay.ToString().Split(':');
                TimeSpan msgstime = System.DateTime.Now.TimeOfDay;
                //TimeSpan msgstime = new TimeSpan(Convert.ToInt32(msgtime[0]), Convert.ToInt32(msgtime[1]), Convert.ToInt32(msgtime[2]));
                TimeSpan s1t1 = new TimeSpan(0, 0, 0), s1t2 = new TimeSpan(0, 0, 0), s2t1 = new TimeSpan(0, 0, 0), s2t2 = new TimeSpan(0, 0, 0);
                TimeSpan s3t1 = new TimeSpan(0, 0, 0), s3t2 = new TimeSpan(0, 0, 0), s3t3 = new TimeSpan(0, 0, 0), s3t4 = new TimeSpan(23, 59, 59);
                for (int k = 0; k < dtshift.Rows.Count; k++)
                {
                    if (dtshift.Rows[k][0].ToString().Contains("A"))
                    {
                        String[] s1 = dtshift.Rows[k][1].ToString().Split(':');
                        s1t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                        String[] s11 = dtshift.Rows[k][2].ToString().Split(':');
                        s1t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));
                    }
                    else if (dtshift.Rows[k][0].ToString().Contains("B"))
                    {
                        String[] s1 = dtshift.Rows[k][1].ToString().Split(':');
                        s2t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                        String[] s11 = dtshift.Rows[k][2].ToString().Split(':');
                        s2t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));
                    }
                    else if (dtshift.Rows[k][0].ToString().Contains("C"))
                    {
                        String[] s1 = dtshift.Rows[k][1].ToString().Split(':');
                        s3t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                        String[] s11 = dtshift.Rows[k][2].ToString().Split(':');
                        s3t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));
                    }
                }
                
                if (msgstime >= s1t1 && msgstime < s1t2)
                {
                }
                else if (msgstime >= s2t1 && msgstime < s2t2)
                {
                }
                else if ((msgstime >= s3t1 && msgstime <= s3t4) || (msgstime >= s3t3 && msgstime < s3t2))
                {
                    if (msgstime >= s3t3 && msgstime < s3t2)
                    {
                        CorrectedDate = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    }
                }
                mcp.close();

                #endregion
                PMS_Scheduling(CorrectedDate);
                EmailEscalator(CorrectedDate);
            }
            catch (Exception exception)
            {
                IntoFile(exception.ToString());
                //MessageBox.Show(exception.ToString());
            }
            try
            {
                Timer MyTimer = new Timer();
                MyTimer.Interval = (60 * 1000); // 1 mins
                MyTimer.Tick += new EventHandler(MyTimer_Tick);
                MyTimer.Start();

                
                Timer MyTimer1 = new Timer();
                MyTimer1.Interval = (60 * 1000 *60); //1hr          
                MyTimer1.Enabled = true;
                MyTimer1.Tick += new EventHandler(MyTimer_Tick1);
                MyTimer1.Start();
            }
            catch (Exception exception)
            {
                IntoFile(exception.ToString());
                //MessageBox.Show(exception.ToString());
            }
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //ModeMoreThanTenMinutes();  // Escalate mail if the current mode is continuous More than 10 min

                #region //Get CorrectedDate & shift

                MsqlConnection mcp = new MsqlConnection();
                mcp.open();
                String queryshift = "SELECT ShiftName,StartTime,EndTime FROM " + MsqlConnection.DbName + ".tblshift_mstr WHERE IsDeleted = 0";
                SqlDataAdapter dashift = new SqlDataAdapter(queryshift, mcp.msqlConnection);
                DataTable dtshift = new DataTable();
                dashift.Fill(dtshift);
                String[] msgtime = System.DateTime.Now.TimeOfDay.ToString().Split(':');
                TimeSpan msgstime = System.DateTime.Now.TimeOfDay;
                //TimeSpan msgstime = new TimeSpan(Convert.ToInt32(msgtime[0]), Convert.ToInt32(msgtime[1]), Convert.ToInt32(msgtime[2]));
                TimeSpan s1t1 = new TimeSpan(0, 0, 0), s1t2 = new TimeSpan(0, 0, 0), s2t1 = new TimeSpan(0, 0, 0), s2t2 = new TimeSpan(0, 0, 0);
                TimeSpan s3t1 = new TimeSpan(0, 0, 0), s3t2 = new TimeSpan(0, 0, 0), s3t3 = new TimeSpan(0, 0, 0), s3t4 = new TimeSpan(23, 59, 59);
                for (int k = 0; k < dtshift.Rows.Count; k++)
                {
                    if (dtshift.Rows[k][0].ToString().Contains("A"))
                    {
                        String[] s1 = dtshift.Rows[k][1].ToString().Split(':');
                        s1t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                        String[] s11 = dtshift.Rows[k][2].ToString().Split(':');
                        s1t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));
                    }
                    else if (dtshift.Rows[k][0].ToString().Contains("B"))
                    {
                        String[] s1 = dtshift.Rows[k][1].ToString().Split(':');
                        s2t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                        String[] s11 = dtshift.Rows[k][2].ToString().Split(':');
                        s2t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));
                    }
                    else if (dtshift.Rows[k][0].ToString().Contains("C"))
                    {
                        String[] s1 = dtshift.Rows[k][1].ToString().Split(':');
                        s3t1 = new TimeSpan(Convert.ToInt32(s1[0]), Convert.ToInt32(s1[1]), Convert.ToInt32(s1[2]));
                        String[] s11 = dtshift.Rows[k][2].ToString().Split(':');
                        s3t2 = new TimeSpan(Convert.ToInt32(s11[0]), Convert.ToInt32(s11[1]), Convert.ToInt32(s11[2]));
                    }
                }
                String CorrectedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                if (msgstime >= s1t1 && msgstime < s1t2)
                {
                }
                else if (msgstime >= s2t1 && msgstime < s2t2)
                {
                }
                else if ((msgstime >= s3t1 && msgstime <= s3t4) || (msgstime >= s3t3 && msgstime < s3t2))
                {
                    if (msgstime >= s3t3 && msgstime < s3t2)
                    {
                        CorrectedDate = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    }
                }
                mcp.close();

                #endregion
                EmailEscalator(CorrectedDate);
                EscalatEmailForNoCode();
               // EmailEscalatorForCriticalMachine(CorrectedDate);// for critical machines

            }
            catch (Exception exception)
            {
                IntoFile(" Main Catch " + exception.ToString());
            }
        }

        #region if machine in Idle escalate a mail
        public void MailEscalateWhenIdle()
        {
            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE IsDeleted = 0";
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();
            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                int machineid = Convert.ToInt32(dtEscData.Rows[i][0]);

                DataTable dtEscData1 = new DataTable();
                MsqlConnection mc1 = new MsqlConnection();
                mc1.open();
                String query1 = "SELECT Top 1 * From " + MsqlConnection.DbName + ".tbllivemode WHERE MachineID = " + machineid + " and IsCompleted =0 order by ModeID desc";
                SqlDataAdapter da1 = new SqlDataAdapter(query1, mc.msqlConnection);
                da1.Fill(dtEscData1);
                mc1.close();

                string Mode = Convert.ToString(dtEscData1.Rows[0][2]);
                //if(Mode == "IDLE")
                //{
                //    sendMailWhenIdle(machineid);
                //}
            }

        }
        #endregion

        #region For Noraml Machine             

        public void EmailEscalator(string CorrectedDate)
        {
            //Step 1: Get Machine or Group of Machines
            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE IsDeleted = 0 ";
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();

            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                string Plantid = Convert.ToString(dtEscData.Rows[i][6]);
                string Shopid = Convert.ToString(dtEscData.Rows[i][7]);
                string Cellid = Convert.ToString(dtEscData.Rows[i][8]);
                string WCid = Convert.ToString(dtEscData.Rows[i][9]);
                int ThisEscHours = Convert.ToInt32(dtEscData.Rows[i][10]);
                int ThisEscMinutes = Convert.ToInt32(dtEscData.Rows[i][11]);
                int ThisEscDuration = (ThisEscHours > 0 ? ThisEscHours * 60 : 0) + ThisEscMinutes;
                int escalationID = Convert.ToInt32(dtEscData.Rows[i][0]);
                string msgType = Convert.ToString(dtEscData.Rows[i][2]);

                //if (Shopid != null)
                //if (!DBNull.Value.Equals(Shopid))
                int shopidINT = 0;
                if (int.TryParse(Shopid, out shopidINT))
                {
                    //if (Cellid != null)
                    //if (!DBNull.Value.Equals(Cellid))
                    int cellidINT = 0;
                    if (int.TryParse(Cellid, out cellidINT))
                    {
                        //if (WCid != null)
                        //if (!DBNull.Value.Equals(WCid))
                        int wcidINT = 0;
                        if (int.TryParse(WCid, out wcidINT))
                        {
                            int wcidInteger = Convert.ToInt32(wcidINT);
                            EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                        }
                        else //Plant , Shop , Cell Only
                        {
                            int CellIDInteger = Convert.ToInt32(cellidINT);
                            DataTable dtMacData = new DataTable();
                            MsqlConnection mcMacData = new MsqlConnection();
                            mcMacData.open();
                            String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE  IsDeleted = 0 and CellID = " + CellIDInteger;
                            SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                            daMacData.Fill(dtMacData);
                            mcMacData.close();
                            for (int j = 0; j < dtMacData.Rows.Count; j++)
                            {
                                int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                                EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                            }
                        }
                    }
                    else //Plant & Shop Only
                    {
                        int ShopIDInteger = Convert.ToInt32(shopidINT);
                        DataTable dtMacData = new DataTable();
                        MsqlConnection mcMacData = new MsqlConnection();
                        mcMacData.open();
                        String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails  WHERE  IsDeleted = 0 and ShopID = " + ShopIDInteger;
                        SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                        daMacData.Fill(dtMacData);
                        mcMacData.close();
                        for (int j = 0; j < dtMacData.Rows.Count; j++)
                        {
                            int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                            EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                        }
                    }
                }
                else //Just Plant
                {
                    int PlantIDInteger = Convert.ToInt32(Plantid);
                    DataTable dtMacData = new DataTable();
                    MsqlConnection mcMacData = new MsqlConnection();
                    mcMacData.open();
                    String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE  IsDeleted = 0 and PlantID = " + PlantIDInteger;
                    SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                    daMacData.Fill(dtMacData);
                    mcMacData.close();
                    for (int j = 0; j < dtMacData.Rows.Count; j++)
                    {
                        int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                        EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                    }
                }
            }
        }

        public void EmailEscalationForWC(int escalationID, string msgType, int WCid, int ThisEscDuration, string CorrectedDate)
        {
            if (msgType == "IDLE")
            {
                //Step 2: Get the Reason 
                List<KeyValuePair<int, string>> Reason = GetReasonIDLE(escalationID);

                //Step 3: Get Data from tbllossofentry for today, reason, WCID, 
                //Step 3: Get Data from tblbreakdown for today, reason, WCID, 

                foreach (var row in Reason)
                {

                    int LossCodeID = row.Key;
                    string LossCode = row.Value;

                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    // String querylossData = "SELECT * From tbllossofentry WHERE MachineID = " + WCid + " and CorrectedDate = '" + CorrectedDate + "' and MessageCodeID = " + LossCodeID + " and DoneWithRow = 0 ";
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllivelossofentry WHERE MachineID = " + WCid + " and CorrectedDate = '" + CorrectedDate + "' and MessageCodeID = " + LossCodeID + " and DoneWithRow = 0 ";//change by v
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();
                    for (int j = 0; j < dtlossData.Rows.Count; j++)
                    {
                        DateTime StartTimeFromTable = Convert.ToDateTime(dtlossData.Rows[j][2]);
                        DateTime DateTimeNow = DateTime.Now;
                        int DurationFromLossTable = Convert.ToInt32(DateTimeNow.Subtract(StartTimeFromTable).TotalMinutes);

                        if (DurationFromLossTable < ThisEscDuration)
                        {
                            //Do Nothing.
                        }
                        else
                        {
                            //int donewithrow = Convert.ToInt32( dtlossData.Rows[j][11]);
                            //int AllNormalFlow = 0;

                            //if (donewithrow == 0){
                            //    AllNormalFlow = 1;
                            //}
                            //else {
                            //    DateTime EndTimeFromTable = Convert.ToDateTime(dtlossData.Rows[j][3]);
                            //    int DurationForDoneWithRow = Convert.ToInt32( EndTimeFromTable.Subtract(StartTimeFromTable).TotalMinutes );
                            //    if (DurationForDoneWithRow > ThisEscDuration) {
                            //        AllNormalFlow = 1;
                            //    }
                            //}


                            //if (AllNormalFlow == 1)
                            //{
                            DataTable dtEscLogData = new DataTable();
                            MsqlConnection mcEscLog = new MsqlConnection();
                            mcEscLog.open();
                            String queryEscLog = "SELECT Top 1 * From " + MsqlConnection.DbName + ".tblescalationlog WHERE IsDeleted = 0 and WCID = " + WCid + " and EscalationID = " + escalationID + " and LossCodeID = " + LossCodeID + " and CorrectedDate = '" + CorrectedDate + "' and EscalationSentOn = '" + StartTimeFromTable.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY CreatedOn desc";
                            SqlDataAdapter daEscLog = new SqlDataAdapter(queryEscLog, mcEscLog.msqlConnection);
                            daEscLog.Fill(dtEscLogData);
                            mcEscLog.close();
                            // MessageBox.Show(dtEscLogData.Rows.Count.ToString());
                            if (dtEscLogData.Rows.Count > 0)
                            {
                                //MessageBox.Show("Inside IF");
                                //DateTime StartTimeFromEscLogTable = Convert.ToDateTime(dtEscLogData.Rows[0][4]);
                                //DateTime DateTimeNow1 = DateTime.Now;
                                //int DurationFromLossTableNextTime = Convert.ToInt32(DateTimeNow1.Subtract(StartTimeFromEscLogTable).TotalMinutes);

                                //if (DurationFromLossTableNextTime < ThisEscDuration)
                                //{
                                //    //Do Nothing.
                                //}
                                //else //send mail and update to tblescalationlog
                                //{
                                //    bool status = sendMail(escalationID, StartTimeFromEscLogTable, WCid, Reason[1]);
                                //    DateTime EscSentTime2 = StartTimeFromEscLogTable.AddMinutes(ThisEscDuration);
                                //    bool InsertEsc = InsertIntoEscLog(escalationID, Convert.ToInt32(Reason[0]), msgType, EscSentTime2, CorrectedDate);
                                //}
                            }
                            else //send mail and update to tblescalationlog
                            {
                                //MessageBox.Show("Inside Else: " + escalationID + " ," + StartTimeFromTable + " ," + WCid + " ," + Reason[1]);
                                bool status = sendMail(escalationID, StartTimeFromTable, WCid, LossCodeID);
                                if (status)
                                {
                                    bool InsertEsc = InsertIntoEscLog(WCid, escalationID, LossCodeID, msgType, StartTimeFromTable, CorrectedDate);
                                }
                            }
                            //}
                        }
                    }
                }
            }
            if (msgType == "HOLD")
            {
                //Step 2: Get the Reason 
                List<KeyValuePair<int, string>> Reason = GetReasonHOLD(escalationID);

                //Step 3: Get Data from tbllossofentry for today, reason, WCID, 
                //Step 3: Get Data from tblbreakdown for today, reason, WCID, 

                foreach (KeyValuePair<int, string> row in Reason)
                {

                    int HoldCodeID = row.Key;
                    string HoldCode = row.Value;

                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    // String querylossData = "SELECT * From tbllossofentry WHERE MachineID = " + WCid + " and CorrectedDate = '" + CorrectedDate + "' and MessageCodeID = " + LossCodeID + " and DoneWithRow = 0 ";
                    String querylossData = "select * FROM " + MsqlConnection.DbName + ".[tbllivemanuallossofentry] where MachineID=" + WCid + " and CorrectedDate='" + CorrectedDate + "' and MessageCodeID=" + HoldCodeID + " and EndDateTime is null";//change by v
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();
                    for (int j = 0; j < dtlossData.Rows.Count; j++)
                    {
                        DateTime StartTimeFromTable = Convert.ToDateTime(dtlossData.Rows[j][2]);
                        DateTime DateTimeNow = DateTime.Now;
                        int DurationFromLossTable = Convert.ToInt32(DateTimeNow.Subtract(StartTimeFromTable).TotalMinutes);

                        if (DurationFromLossTable < ThisEscDuration)
                        {
                            //Do Nothing.
                        }
                        else
                        {
                            //int donewithrow = Convert.ToInt32( dtlossData.Rows[j][11]);
                            //int AllNormalFlow = 0;

                            //if (donewithrow == 0){
                            //    AllNormalFlow = 1;
                            //}
                            //else {
                            //    DateTime EndTimeFromTable = Convert.ToDateTime(dtlossData.Rows[j][3]);
                            //    int DurationForDoneWithRow = Convert.ToInt32( EndTimeFromTable.Subtract(StartTimeFromTable).TotalMinutes );
                            //    if (DurationForDoneWithRow > ThisEscDuration) {
                            //        AllNormalFlow = 1;
                            //    }
                            //}


                            //if (AllNormalFlow == 1)
                            //{
                            DataTable dtEscLogData = new DataTable();
                            MsqlConnection mcEscLog = new MsqlConnection();
                            mcEscLog.open();
                            String queryEscLog = "SELECT Top 1 * From " + MsqlConnection.DbName + ".tblescalationlog WHERE IsDeleted = 0 and WCID = " + WCid + " and EscalationID = " + escalationID + " and LossCodeID = " + HoldCodeID + " and CorrectedDate = '" + CorrectedDate + "' and EscalationSentOn = '" + StartTimeFromTable.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY CreatedOn desc";
                            SqlDataAdapter daEscLog = new SqlDataAdapter(queryEscLog, mcEscLog.msqlConnection);
                            daEscLog.Fill(dtEscLogData);
                            mcEscLog.close();
                            // MessageBox.Show(dtEscLogData.Rows.Count.ToString());
                            if (dtEscLogData.Rows.Count > 0)
                            {
                                //MessageBox.Show("Inside IF");
                                //DateTime StartTimeFromEscLogTable = Convert.ToDateTime(dtEscLogData.Rows[0][4]);
                                //DateTime DateTimeNow1 = DateTime.Now;
                                //int DurationFromLossTableNextTime = Convert.ToInt32(DateTimeNow1.Subtract(StartTimeFromEscLogTable).TotalMinutes);

                                //if (DurationFromLossTableNextTime < ThisEscDuration)
                                //{
                                //    //Do Nothing.
                                //}
                                //else //send mail and update to tblescalationlog
                                //{
                                //    bool status = sendMail(escalationID, StartTimeFromEscLogTable, WCid, Reason[1]);
                                //    DateTime EscSentTime2 = StartTimeFromEscLogTable.AddMinutes(ThisEscDuration);
                                //    bool InsertEsc = InsertIntoEscLog(escalationID, Convert.ToInt32(Reason[0]), msgType, EscSentTime2, CorrectedDate);
                                //}
                            }
                            else //send mail and update to tblescalationlog
                            {
                                //MessageBox.Show("Inside Else: " + escalationID + " ," + StartTimeFromTable + " ," + WCid + " ," + Reason[1]);
                                bool status = sendMail(escalationID, StartTimeFromTable, WCid, HoldCodeID);
                                if (status)
                                {
                                    bool InsertEsc = InsertIntoEscLog(WCid, escalationID, HoldCodeID, msgType, StartTimeFromTable, CorrectedDate);
                                }
                            }
                            //}
                        }
                    }
                }
            }
            else if (msgType == "PINGFAIL")
            {
                DataTable dtDuration = new DataTable();
                string query = "select StartTime FROM " + MsqlConnection.DbName + ".[tbllivemode] where machineid=" + WCid + " and iscompleted=0";
                MsqlConnection mclossData = new MsqlConnection();
                mclossData.open();
                SqlDataAdapter modeMachine = new SqlDataAdapter(query, mclossData.msqlConnection);
                modeMachine.Fill(dtDuration);
                mclossData.close();
                if (dtDuration.Rows.Count > 0)
                {
                    DateTime startDateTime = Convert.ToDateTime(dtDuration.Rows[0][0]);
                    DateTime endDateTime = DateTime.Now;
                    int durationInSec = Convert.ToInt32(endDateTime.Subtract(startDateTime).TotalMinutes);
                    if (durationInSec >= ThisEscDuration)
                    {
                        DataTable machDT = new DataTable();
                        string machQry = "Select IPAddress FROM " + MsqlConnection.DbName + ".[tblmachinedetails] where MachineID=" + WCid + "";
                        MsqlConnection mcMach = new MsqlConnection();
                        mcMach.open();
                        SqlDataAdapter macSDA = new SqlDataAdapter(machQry, mcMach.msqlConnection);
                        macSDA.Fill(machDT);
                        mcMach.close();
                        if (machDT.Rows.Count > 0)
                        {
                            string ipAddress = Convert.ToString(machDT.Rows[0][0]);
                            int lossId = Convert.ToInt32(ConfigurationManager.AppSettings["lossid"]);
                            Ping myPing = new Ping();
                            PingReply reply = myPing.Send(ipAddress, 1000);
                            if (reply.Status != IPStatus.Success)
                            {
                                DataTable dtEscLogData = new DataTable();
                                MsqlConnection mcEscLog = new MsqlConnection();
                                mcEscLog.open();
                                String queryEscLog = "SELECT Top 1 * From " + MsqlConnection.DbName + ".tblescalationlog WHERE IsDeleted = 0 and WCID = " + WCid + " and EscalationID = " + escalationID + " and LossCodeID=" + lossId + " and CorrectedDate = '" + CorrectedDate + "' and EscalationSentOn = '" + startDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY CreatedOn desc";
                                SqlDataAdapter daEscLog = new SqlDataAdapter(queryEscLog, mcEscLog.msqlConnection);
                                daEscLog.Fill(dtEscLogData);
                                mcEscLog.close();

                                if (dtEscLogData.Rows.Count == 0)
                                {
                                    bool status = sendMail(escalationID, startDateTime, WCid, lossId);
                                    if (status)
                                    {
                                        bool InsertEsc = InsertIntoEscLog(WCid, escalationID, lossId, msgType, startDateTime, CorrectedDate);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else    //Breakdown
            {
                //Step 2: Get the Reason 
                List<KeyValuePair<int, string>> Reason = GetReasonBreakdown(escalationID);

                //Step 3: Get Data from tbllossofentry for today, reason, WCID, 
                //Step 3: Get Data from tblbreakdown for today, reason, WCID, 

                foreach (KeyValuePair<int, string> row in Reason)
                {
                    int LossCodeID = row.Key;
                    string LossCode = row.Value;

                    //MessageBox.Show("Breakdown");
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tblbreakdown WHERE MachineID = " + WCid + " and CorrectedDate = '" + CorrectedDate + "' and BreakDownCode = " + LossCodeID + " and DoneWithRow = 0 ";
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();
                    for (int j = 0; j < dtlossData.Rows.Count; j++)
                    {
                        DateTime StartTimeFromTable = Convert.ToDateTime(dtlossData.Rows[j][1]);
                        DateTime DateTimeNow = DateTime.Now;
                        int DurationFromLossTable = Convert.ToInt32(DateTimeNow.Subtract(StartTimeFromTable).TotalMinutes);

                        if (DurationFromLossTable < ThisEscDuration)
                        {
                            //Do Nothing.
                        }
                        else
                        {
                            //Check if mail is already sent for this escalationid , lossid, correctedDate, 
                            // if mail sent then check duration from EscalationSentOn time to datetime.now
                            // if its time then send mail else do nothing, 
                            // else send Mail & update to tblescalationlog table

                            //int donewithrow = Convert.ToInt32( dtlossData.Rows[j][9]);
                            //int AllNormalFlow = 0;

                            //if (donewithrow == 0){
                            //    AllNormalFlow = 1;
                            //}
                            //else {
                            //    DateTime EndTimeFromTable = Convert.ToDateTime(dtlossData.Rows[j][2]);
                            //    int DurationForDoneWithRow = Convert.ToInt32( EndTimeFromTable.Subtract(StartTimeFromTable).TotalMinutes );
                            //    if (DurationForDoneWithRow > ThisEscDuration) {
                            //        AllNormalFlow = 1;
                            //    }
                            //}

                            //if (AllNormalFlow == 1)
                            //{
                            DataTable dtEscLogData = new DataTable();
                            MsqlConnection mcEscLog = new MsqlConnection();
                            mcEscLog.open();
                            String queryEscLog = "SELECT top 1 * From " + MsqlConnection.DbName + ".tblescalationlog WHERE IsDeleted = 0 and WCID = " + WCid + " and EscalationID = " + escalationID + " and LossCodeID = '" + LossCodeID + "' and EscalationSentOn = '" + StartTimeFromTable.ToString("yyyy-MM-dd HH:mm:ss") + "'  and CorrectedDate = '" + CorrectedDate + "' ORDER BY CreatedOn desc";
                            SqlDataAdapter daEscLog = new SqlDataAdapter(queryEscLog, mcEscLog.msqlConnection);
                            daEscLog.Fill(dtEscLogData);
                            mcEscLog.close();
                            if (dtEscLogData.Rows.Count > 0)
                            {
                                //DateTime StartTimeFromEscLogTable = Convert.ToDateTime(dtEscLogData.Rows[0][4]);
                                //DateTime DateTimeNow1 = DateTime.Now;
                                //int DurationFromLossTableNextTime = Convert.ToInt32(DateTimeNow1.Subtract(StartTimeFromEscLogTable).TotalMinutes);

                                //if (DurationFromLossTableNextTime < ThisEscDuration)
                                //{
                                //    //Do Nothing.
                                //}
                                //else //send mail and update to tblescalationlog
                                //{
                                //    bool status = sendMail(escalationID, StartTimeFromEscLogTable, WCid, Reason[1]);
                                //    DateTime EscSentTime2 = StartTimeFromEscLogTable.AddMinutes(ThisEscDuration);
                                //    bool InsertEsc = InsertIntoEscLog(escalationID, Convert.ToInt32(Reason[0]), msgType, EscSentTime2, CorrectedDate);
                                //}
                            }
                            else //send mail and update to tblescalationlog
                            {
                                bool status = sendMail(escalationID, StartTimeFromTable, WCid, LossCodeID);
                                if (status)
                                {
                                    bool InsertEsc = InsertIntoEscLog(WCid, escalationID, LossCodeID, msgType, StartTimeFromTable, CorrectedDate);
                                }
                            }
                        }
                        //}
                    }
                }
            }

        }

        public List<KeyValuePair<int, string>> GetReasonIDLE(int escID)
        {
            List<KeyValuePair<int, string>> reason = new List<KeyValuePair<int, string>>();

            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE EMailEscalationID = " + escID;
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();

            int reasonid = 0;
            int level = 0;
            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                string rl1 = Convert.ToString(dtEscData.Rows[i][3]); //rl1 is Reason Level 1
                string rl2 = Convert.ToString(dtEscData.Rows[i][4]);
                string rl3 = Convert.ToString(dtEscData.Rows[i][5]);

                int value;

                if (int.TryParse(rl3, out value))
                {
                    level = 3;
                    reasonid = value;
                }
                else if (int.TryParse(rl2, out value))
                {
                    level = 2;
                    reasonid = value;
                }
                else
                {
                    level = 1;
                    reasonid = Convert.ToInt32(dtEscData.Rows[i][3]);
                }
            }

            //Get Loss Name
            DataTable dtlossData6 = new DataTable();
            MsqlConnection mclossData6 = new MsqlConnection();
            mclossData6.open();
            String querylossData6 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE LossCodeID = " + reasonid;
            SqlDataAdapter dalossData6 = new SqlDataAdapter(querylossData6, mclossData6.msqlConnection);
            dalossData6.Fill(dtlossData6);
            mclossData6.close();
            string MainLossName = null;

            if (dtlossData6.Rows.Count > 0)
            {
                MainLossName = Convert.ToString(dtlossData6.Rows[0][1]);
            }

            if (level == 1)
            {
                DataTable dtlossData1 = new DataTable();
                MsqlConnection mclossData1 = new MsqlConnection();
                mclossData1.open();
                String querylossData1 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE IsDeleted = 0 and LossCodesLevel1ID = " + reasonid;
                SqlDataAdapter dalossData1 = new SqlDataAdapter(querylossData1, mclossData1.msqlConnection);
                dalossData1.Fill(dtlossData1);
                mclossData1.close();

                for (int i = 0; i < dtlossData1.Rows.Count; i++)
                {
                    reasonid = Convert.ToInt32(dtlossData1.Rows[i][0]);

                    //now get the (Reason) LossCode from tbllosscodes
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE  IsDeleted = 0 and LossCodeID = " + reasonid;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();

                    if (dtlossData.Rows.Count > 0)
                    {
                        reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                    }
                }
                if (dtlossData1.Rows.Count == 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, MainLossName));
                }
            }
            else if (level == 2)
            {
                //reason.Add(reasonid.ToString());
                DataTable dtlossData1 = new DataTable();
                MsqlConnection mclossData1 = new MsqlConnection();
                mclossData1.open();
                String querylossData1 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE IsDeleted = 0 and LossCodesLevel2ID = " + reasonid;
                SqlDataAdapter dalossData1 = new SqlDataAdapter(querylossData1, mclossData1.msqlConnection);
                dalossData1.Fill(dtlossData1);
                mclossData1.close();

                for (int i = 0; i < dtlossData1.Rows.Count; i++)
                {
                    reasonid = Convert.ToInt32(dtlossData1.Rows[i][0]);

                    //now get the (Reason) LossCode from tbllosscodes
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE  IsDeleted = 0 and LossCodeID = " + reasonid;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();

                    if (dtlossData.Rows.Count > 0)
                    {
                        reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                    }
                }
                if (dtlossData1.Rows.Count == 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, MainLossName));
                }
            }
            else if (level == 3)
            {
                //now get the (Reason) LossCode from tbllosscodes
                DataTable dtlossData = new DataTable();
                MsqlConnection mclossData = new MsqlConnection();
                mclossData.open();
                String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE  IsDeleted = 0 and LossCodeID = " + reasonid;
                SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                dalossData.Fill(dtlossData);
                mclossData.close();

                if (dtlossData.Rows.Count > 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                }
            }

            return reason;
        }

        public List<KeyValuePair<int, string>> GetReasonHOLD(int escID)
        {
            List<KeyValuePair<int, string>> reason = new List<KeyValuePair<int, string>>();

            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE EMailEscalationID = " + escID;
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();

            int reasonid = 0;
            int level = 0;
            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                string rl1 = Convert.ToString(dtEscData.Rows[i][20]); //rl1 is Reason Level 1
                string rl2 = Convert.ToString(dtEscData.Rows[i][21]);
                string rl3 = Convert.ToString(dtEscData.Rows[i][22]);

                int value;

                if (int.TryParse(rl3, out value))
                {
                    level = 3;
                    reasonid = value;
                }
                else if (int.TryParse(rl2, out value))
                {
                    level = 2;
                    reasonid = value;
                }
                else
                {
                    level = 1;
                    reasonid = Convert.ToInt32(dtEscData.Rows[i][3]);
                }
            }

            //Get Loss Name
            DataTable dtlossData6 = new DataTable();
            MsqlConnection mclossData6 = new MsqlConnection();
            mclossData6.open();
            String querylossData6 = "SELECT * From " + MsqlConnection.DbName + ".[tblholdcodes] WHERE HoldCodeID = " + reasonid;
            SqlDataAdapter dalossData6 = new SqlDataAdapter(querylossData6, mclossData6.msqlConnection);
            dalossData6.Fill(dtlossData6);
            mclossData6.close();
            string MainLossName = null;

            if (dtlossData6.Rows.Count > 0)
            {
                MainLossName = Convert.ToString(dtlossData6.Rows[0][1]);
            }

            if (level == 1)
            {
                DataTable dtlossData1 = new DataTable();
                MsqlConnection mclossData1 = new MsqlConnection();
                mclossData1.open();
                String querylossData1 = "SELECT * From " + MsqlConnection.DbName + ".[tblholdcodes] WHERE IsDeleted = 0 and HoldCodesLevel1ID = " + reasonid;
                SqlDataAdapter dalossData1 = new SqlDataAdapter(querylossData1, mclossData1.msqlConnection);
                dalossData1.Fill(dtlossData1);
                mclossData1.close();

                for (int i = 0; i < dtlossData1.Rows.Count; i++)
                {
                    reasonid = Convert.ToInt32(dtlossData1.Rows[i][0]);

                    //now get the (Reason) LossCode from tbllosscodes
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".[tblholdcodes] WHERE  IsDeleted = 0 and HoldCodeID = " + reasonid;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();

                    if (dtlossData.Rows.Count > 0)
                    {
                        reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                    }
                }
                if (dtlossData1.Rows.Count == 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, MainLossName));
                }
            }
            else if (level == 2)
            {
                //reason.Add(reasonid.ToString());
                DataTable dtlossData1 = new DataTable();
                MsqlConnection mclossData1 = new MsqlConnection();
                mclossData1.open();
                String querylossData1 = "SELECT * From " + MsqlConnection.DbName + ".[tblholdcodes] WHERE IsDeleted = 0 and HoldCodesLevel2ID = " + reasonid;
                SqlDataAdapter dalossData1 = new SqlDataAdapter(querylossData1, mclossData1.msqlConnection);
                dalossData1.Fill(dtlossData1);
                mclossData1.close();

                for (int i = 0; i < dtlossData1.Rows.Count; i++)
                {
                    reasonid = Convert.ToInt32(dtlossData1.Rows[i][0]);

                    //now get the (Reason) LossCode from tbllosscodes
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".[tblholdcodes] WHERE  IsDeleted = 0 and HoldCodeID = " + reasonid;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();

                    if (dtlossData.Rows.Count > 0)
                    {
                        reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                    }
                }
                if (dtlossData1.Rows.Count == 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, MainLossName));
                }
            }
            else if (level == 3)
            {
                //now get the (Reason) LossCode from tbllosscodes
                DataTable dtlossData = new DataTable();
                MsqlConnection mclossData = new MsqlConnection();
                mclossData.open();
                String querylossData = "SELECT * From " + MsqlConnection.DbName + ".[tblholdcodes] WHERE  IsDeleted = 0 and HoldCodeID = " + reasonid;
                SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                dalossData.Fill(dtlossData);
                mclossData.close();

                if (dtlossData.Rows.Count > 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                }
            }

            return reason;
        }

        public List<KeyValuePair<int, string>> GetReasonBreakdown(int escID)
        {
            List<KeyValuePair<int, string>> reason = new List<KeyValuePair<int, string>>();



            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE EMailEscalationID = " + escID;
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();

            int reasonid = 0;
            int level = 0;
            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                string rl1 = Convert.ToString(dtEscData.Rows[i][3]); //rl1 is Reason Level 1
                string rl2 = Convert.ToString(dtEscData.Rows[i][4]);
                string rl3 = Convert.ToString(dtEscData.Rows[i][5]);

                int value;

                if (int.TryParse(rl3, out value))
                {
                    level = 3;
                    reasonid = value;
                }
                else if (int.TryParse(rl2, out value))
                {
                    level = 2;
                    reasonid = value;
                }
                else
                {
                    level = 1;
                    reasonid = Convert.ToInt32(dtEscData.Rows[i][3]);
                }
            }

            //Get Breakdown Name
            DataTable dtlossData6 = new DataTable();
            MsqlConnection mclossData6 = new MsqlConnection();
            mclossData6.open();
            String querylossData6 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes  WHERE  IsDeleted = 0 and LossCodeID = " + reasonid;
            SqlDataAdapter dalossData6 = new SqlDataAdapter(querylossData6, mclossData6.msqlConnection);
            dalossData6.Fill(dtlossData6);
            mclossData6.close();
            string MainLossName = null;

            if (dtlossData6.Rows.Count > 0)
            {
                MainLossName = Convert.ToString(dtlossData6.Rows[0][1]);
            }

            if (level == 1)
            {
                DataTable dtlossData1 = new DataTable();
                MsqlConnection mclossData1 = new MsqlConnection();
                mclossData1.open();
                String querylossData1 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes  WHERE IsDeleted = 0 and  LossCodesLevel1ID = " + reasonid;
                SqlDataAdapter dalossData1 = new SqlDataAdapter(querylossData1, mclossData1.msqlConnection);
                dalossData1.Fill(dtlossData1);
                mclossData1.close();

                for (int i = 0; i < dtlossData1.Rows.Count; i++)
                {
                    reasonid = Convert.ToInt32(dtlossData1.Rows[i][0]);

                    //now get the (Reason) LossCode from tbllosscodes
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE  IsDeleted = 0 and  LossCodeID = " + reasonid;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();

                    if (dtlossData.Rows.Count > 0)
                    {
                        reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                    }
                }

                if (dtlossData1.Rows.Count == 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, MainLossName));
                }

            }
            else if (level == 2)
            {
                //reason.Add(reasonid.ToString());
                DataTable dtlossData1 = new DataTable();
                MsqlConnection mclossData1 = new MsqlConnection();
                mclossData1.open();
                String querylossData1 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE  IsDeleted = 0 and LossCodesLevel2ID = " + reasonid;
                SqlDataAdapter dalossData1 = new SqlDataAdapter(querylossData1, mclossData1.msqlConnection);
                dalossData1.Fill(dtlossData1);
                mclossData1.close();

                for (int i = 0; i < dtlossData1.Rows.Count; i++)
                {
                    reasonid = Convert.ToInt32(dtlossData1.Rows[i][0]);

                    //now get the (Reason) LossCode from tbllosscodes
                    DataTable dtlossData = new DataTable();
                    MsqlConnection mclossData = new MsqlConnection();
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes   WHERE IsDeleted = 0 and LossCodeID = " + reasonid;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtlossData);
                    mclossData.close();

                    if (dtlossData.Rows.Count > 0)
                    {
                        reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                    }
                }

                if (dtlossData1.Rows.Count == 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, MainLossName));
                }
            }
            else if (level == 3)
            {
                //now get the (Reason) LossCode from tbllosscodes
                DataTable dtlossData = new DataTable();
                MsqlConnection mclossData = new MsqlConnection();
                mclossData.open();
                String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE  IsDeleted = 0 and  LossCodeID = " + reasonid;
                SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                dalossData.Fill(dtlossData);
                mclossData.close();

                if (dtlossData.Rows.Count > 0)
                {
                    reason.Add(new KeyValuePair<int, string>(reasonid, Convert.ToString(dtlossData.Rows[0][1])));
                }
            }

            return reason;
        }

        public bool sendMail(int escID, DateTime StartTimeFromEscLogTable, int WCID, int reasonID)
        {
            bool Status = false;
            try
            {
                string Hierarchy = null;
                string MachineName = null;
                string plantName = null;
                string shopName = null;
                string cellName = null;
                DataTable dtMacData = new DataTable();
                MsqlConnection mclossData = new MsqlConnection();
                mclossData.open();
                String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE MachineID = " + WCID;
                SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                dalossData.Fill(dtMacData);
                mclossData.close();

                if (dtMacData.Rows.Count > 0)
                {
                    MachineName = dtMacData.Rows[0][10].ToString();
                    int PlantID = 0, ShopID = 0, CellID = 0;
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][6]), out PlantID);
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][7]), out ShopID);
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][8]), out CellID);

                    if (PlantID != 0)
                    {
                        DataTable dtPlantData = new DataTable();
                        MsqlConnection mcPlantData = new MsqlConnection();
                        mcPlantData.open();
                        String queryPlantData = "SELECT PlantName From " + MsqlConnection.DbName + ".tblplant WHERE PlantID = " + PlantID;
                        SqlDataAdapter daPlantData = new SqlDataAdapter(queryPlantData, mcPlantData.msqlConnection);
                        daPlantData.Fill(dtPlantData);
                        mcPlantData.close();

                        if (dtPlantData.Rows.Count > 0)
                        {
                            plantName = dtPlantData.Rows[0][0].ToString();
                            Hierarchy = plantName;
                        }
                    }

                    if (ShopID != 0)
                    {
                        DataTable dtShopData = new DataTable();
                        MsqlConnection mcShopData = new MsqlConnection();
                        mcShopData.open();
                        String queryShopData = "SELECT ShopName From " + MsqlConnection.DbName + ".tblshop WHERE ShopID = " + ShopID;
                        SqlDataAdapter daShopData = new SqlDataAdapter(queryShopData, mcShopData.msqlConnection);
                        daShopData.Fill(dtShopData);
                        mcShopData.close();

                        if (dtShopData.Rows.Count > 0)
                        {
                            shopName = dtShopData.Rows[0][0].ToString();
                            Hierarchy += "-->" + shopName;
                        }
                    }
                    if (CellID != 0)
                    {
                        DataTable dtCellData = new DataTable();
                        MsqlConnection mcCellData = new MsqlConnection();
                        mcCellData.open();
                        String queryCellData = "SELECT CellName From " + MsqlConnection.DbName + ".tblcell WHERE CellID = " + CellID;
                        SqlDataAdapter daCellData = new SqlDataAdapter(queryCellData, mcCellData.msqlConnection);
                        daCellData.Fill(dtCellData);
                        mcCellData.close();

                        if (dtCellData.Rows.Count > 0)
                        {
                            cellName = dtCellData.Rows[0][0].ToString();
                            Hierarchy += "-->" + cellName;
                        }
                    }
                    Hierarchy += "-->" + MachineName;

                }

                DataTable dtEscData = new DataTable();
                MsqlConnection mcEscData = new MsqlConnection();
                mcEscData.open();
                String queryEscData = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE EMailEscalationID = " + escID;
                SqlDataAdapter daEscData = new SqlDataAdapter(queryEscData, mcEscData.msqlConnection);
                daEscData.Fill(dtEscData);
                mcEscData.close();

                string toMailIDs = dtEscData.Rows[0][12].ToString();
                string ccMailIDs = dtEscData.Rows[0][13].ToString();
                string MessageType = dtEscData.Rows[0][2].ToString();

                string Duration = (Convert.ToInt32(DateTime.Now.Subtract(StartTimeFromEscLogTable).TotalMinutes) + 1).ToString();
                string ReasonPath = GetReasonPath(reasonID);

                MailMessage mail = new MailMessage();

                string[] Tomails = toMailIDs.Split(',');
                foreach (string mailid in Tomails)
                {
                    string mailID = Convert.ToString(mailid).Trim();
                    if (!string.IsNullOrEmpty(mailID))
                    {
                        mail.To.Add(new MailAddress(mailid));
                    }
                }

                string[] Ccmails = ccMailIDs.Split(',');
                foreach (string mailid in Ccmails)
                {
                    string mailID = Convert.ToString(mailid).Trim();
                    if (!string.IsNullOrEmpty(mailID))
                    {
                        mail.CC.Add(new MailAddress(mailid));
                    }
                }


                //mail.From = new MailAddress("narendramourya37@gmail.com");

                mail.From = new MailAddress(MsqlConnection.UserNamemail);
                //mail.Subject = MachineName + " " + MessageType;
                mail.Subject = "Work Stoppage Stage - " + MachineName;
                string MailBodyMsg = null;
                if (MessageType == "IDLE")
                {
                    MailBodyMsg = "Work stoppage stage";
                }
                else
                {
                    MailBodyMsg = MessageType;
                }

                //mail.Body = "<p><b>Dear Concerned,</b></p>" +
                //            "<b></b>" +
                //            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine " + MachineName + " has gone into " + MailBodyMsg + " for " + reason + " at " + StartTimeFromEscLogTable + " &nbsp;<span>.</b></p>" +
                //            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  This Mail has been escalated for following Reasons " + ReasonPath + "  after " + Duration + " Minutes. &nbsp;<span>.</b></p>" +
                //            "<p><b><br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Note: This Email has been sent for the demo purpose of EMail Escalation. &nbsp;<span>.</b></p>" +
                //            "<p><b></b></p>";

                mail.Body = "<p><b>Dear Concerned,</b></p>" +
                            "<p><b></b></p>" +
                            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine </b></p>" +
                            "<p><b> " + Hierarchy + "</b></p>" +
                             "<table><tr> <td> " + ReasonPath + "</td> <td> From " + StartTimeFromEscLogTable + " .</td>  <td> Escalated after " + Duration + " Minutes</td></tr></table>" +

                             "<p><b></b></p>" +
                             "<p><b></b></p>" +
                             "<p><b> Regards,</b></p>" +
                            "<p><b> UnitWorks</b></p>" +

                             "<p><b></b></p>" +
                             "<p><b></b></p>" +
                            "<p><b>Note: This is an autogenerated E-Mail for the Escalation Process. Do Not Reply back on this Mail.</b></p>" +
                            "<p><b></b></p>";

                //mail.Bcc = new MailAddress("janardhan.g@srkssolutions.com");

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = MsqlConnection.Host;
                smtp.Port = MsqlConnection.Portmail;
                smtp.UseDefaultCredentials = false;
                //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtp.Credentials = new System.Net.NetworkCredential(MsqlConnection.UserNamemail, MsqlConnection.Passwordmail/*, MsqlConnection.Domain*/);
                //smtp.Credentials = new System.Net.NetworkCredential("unitworks@tasl.aero", "527293");
                smtp.EnableSsl = SSL;
                smtp.Send(mail);
                Status = true;
                // MessageBox.Show("Status : success ");
            }
            catch (Exception e)
            {
                // MessageBox.Show("Error :: sendMail : " + e);
                Status = false;
                IntoFile("Error While Sending Mail " + e);
            }
            return Status;
        }

        //public bool sendMailWhenIdle(int WCID)
        //{
        //    bool Status = false;
        //    try
        //    {
        //        string Hierarchy = null;
        //        string MachineName = null;
        //        string plantName = null;
        //        string shopName = null;
        //        string cellName = null;
        //        DataTable dtMacData = new DataTable();
        //        MsqlConnection mclossData = new MsqlConnection();
        //        mclossData.open();
        //        String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE MachineID = " + WCID;
        //        SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
        //        dalossData.Fill(dtMacData);
        //        mclossData.close();

        //        if (dtMacData.Rows.Count > 0)
        //        {
        //            MachineName = dtMacData.Rows[0][13].ToString();
        //            int PlantID = 0, ShopID = 0, CellID = 0;
        //            int.TryParse(Convert.ToString(dtMacData.Rows[0][18]), out PlantID);
        //            int.TryParse(Convert.ToString(dtMacData.Rows[0][19]), out ShopID);
        //            int.TryParse(Convert.ToString(dtMacData.Rows[0][20]), out CellID);

        //            if (PlantID != 0)
        //            {
        //                DataTable dtPlantData = new DataTable();
        //                MsqlConnection mcPlantData = new MsqlConnection();
        //                mcPlantData.open();
        //                String queryPlantData = "SELECT PlantName From " + MsqlConnection.DbName + ".tblplant WHERE PlantID = " + PlantID;
        //                SqlDataAdapter daPlantData = new SqlDataAdapter(queryPlantData, mcPlantData.msqlConnection);
        //                daPlantData.Fill(dtPlantData);
        //                mcPlantData.close();

        //                if (dtPlantData.Rows.Count > 0)
        //                {
        //                    plantName = dtPlantData.Rows[0][0].ToString();
        //                    Hierarchy = plantName;
        //                }
        //            }

        //            if (ShopID != 0)
        //            {
        //                DataTable dtShopData = new DataTable();
        //                MsqlConnection mcShopData = new MsqlConnection();
        //                mcShopData.open();
        //                String queryShopData = "SELECT ShopName From " + MsqlConnection.DbName + ".tblshop WHERE ShopID = " + ShopID;
        //                SqlDataAdapter daShopData = new SqlDataAdapter(queryShopData, mcShopData.msqlConnection);
        //                daShopData.Fill(dtShopData);
        //                mcShopData.close();

        //                if (dtShopData.Rows.Count > 0)
        //                {
        //                    shopName = dtShopData.Rows[0][0].ToString();
        //                    Hierarchy += "-->" + shopName;
        //                }
        //            }
        //            if (CellID != 0)
        //            {
        //                DataTable dtCellData = new DataTable();
        //                MsqlConnection mcCellData = new MsqlConnection();
        //                mcCellData.open();
        //                String queryCellData = "SELECT CellName From " + MsqlConnection.DbName + ".tblcell WHERE CellID = " + CellID;
        //                SqlDataAdapter daCellData = new SqlDataAdapter(queryCellData, mcCellData.msqlConnection);
        //                daCellData.Fill(dtCellData);
        //                mcCellData.close();

        //                if (dtCellData.Rows.Count > 0)
        //                {
        //                    cellName = dtCellData.Rows[0][0].ToString();
        //                    Hierarchy += "-->" + cellName;
        //                }
        //            }
        //            Hierarchy += "-->" + MachineName;

        //        }

        //        DataTable dtEscData = new DataTable();
        //        MsqlConnection mcEscData = new MsqlConnection();
        //        mcEscData.open();
        //        String queryEscData = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE EMailEscalationID = " + escID;
        //        SqlDataAdapter daEscData = new SqlDataAdapter(queryEscData, mcEscData.msqlConnection);
        //        daEscData.Fill(dtEscData);
        //        mcEscData.close();

        //        string toMailIDs = dtEscData.Rows[0][12].ToString();
        //        string ccMailIDs = dtEscData.Rows[0][13].ToString();
        //        string MessageType = dtEscData.Rows[0][2].ToString();

        //        string Duration = (Convert.ToInt32(DateTime.Now.Subtract(StartTimeFromEscLogTable).TotalMinutes) + 1).ToString();
        //        string ReasonPath = GetReasonPath(reasonID);

        //        MailMessage mail = new MailMessage();

        //        string[] Tomails = toMailIDs.Split(',');
        //        foreach (var mailid in Tomails)
        //        {
        //            string mailID = Convert.ToString(mailid).Trim();
        //            if (!string.IsNullOrEmpty(mailID))
        //            {
        //                mail.To.Add(new MailAddress(mailid));
        //            }
        //        }

        //        string[] Ccmails = ccMailIDs.Split(',');
        //        foreach (var mailid in Ccmails)
        //        {
        //            string mailID = Convert.ToString(mailid).Trim();
        //            if (!string.IsNullOrEmpty(mailID))
        //            {
        //                mail.CC.Add(new MailAddress(mailid));
        //            }
        //        }


        //        //mail.From = new MailAddress("narendramourya37@gmail.com");

        //        mail.From = new MailAddress(MsqlConnection.UserNamemail);
        //        //mail.Subject = MachineName + " " + MessageType;
        //        mail.Subject = "Work Stoppage Stage - " + MachineName;
        //        string MailBodyMsg = null;
        //        if (MessageType == "IDLE")
        //        {
        //            MailBodyMsg = "Work stoppage stage";
        //        }
        //        else
        //        {
        //            MailBodyMsg = MessageType;
        //        }

        //        //mail.Body = "<p><b>Dear Concerned,</b></p>" +
        //        //            "<b></b>" +
        //        //            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine " + MachineName + " has gone into " + MailBodyMsg + " for " + reason + " at " + StartTimeFromEscLogTable + " &nbsp;<span>.</b></p>" +
        //        //            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  This Mail has been escalated for following Reasons " + ReasonPath + "  after " + Duration + " Minutes. &nbsp;<span>.</b></p>" +
        //        //            "<p><b><br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Note: This Email has been sent for the demo purpose of EMail Escalation. &nbsp;<span>.</b></p>" +
        //        //            "<p><b></b></p>";

        //        mail.Body = "<p><b>Dear Concerned,</b></p>" +
        //                    "<p><b></b></p>" +
        //                    "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine </b></p>" +
        //                    "<p><b> " + Hierarchy + "</b></p>" +
        //                     "<table><tr> <td> " + ReasonPath + "</td> <td> From " + StartTimeFromEscLogTable + " .</td>  <td> Escalated after " + Duration + " Minutes</td></tr></table>" +

        //                     "<p><b></b></p>" +
        //                     "<p><b></b></p>" +
        //                     "<p><b> Regards,</b></p>" +
        //                    "<p><b> UnitWorks</b></p>" +

        //                     "<p><b></b></p>" +
        //                     "<p><b></b></p>" +
        //                    "<p><b>Note: This is an autogenerated E-Mail for the Escalation Process. Do Not Reply back on this Mail.</b></p>" +
        //                    "<p><b></b></p>";

        //        //mail.Bcc = new MailAddress("janardhan.g@srkssolutions.com");

        //        mail.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = MsqlConnection.Host;
        //        smtp.Port = MsqlConnection.Portmail;
        //        smtp.UseDefaultCredentials = false;
        //        //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
        //        smtp.Credentials = new System.Net.NetworkCredential(MsqlConnection.UserNamemail, MsqlConnection.Passwordmail, MsqlConnection.Domain);
        //        //smtp.Credentials = new System.Net.NetworkCredential("unitworks@tasl.aero", "527293");
        //        smtp.EnableSsl = SSL;
        //        smtp.Send(mail);
        //        Status = true;
        //        // MessageBox.Show("Status : success ");
        //    }
        //    catch (Exception e)
        //    {
        //        // MessageBox.Show("Error :: sendMail : " + e);
        //        Status = false;
        //        IntoFile("Error While Sending Mail " + e);
        //    }
        //    return Status;
        //}

        public bool InsertIntoEscLog(int wcid, int escalationID, int Reason, string msgType, DateTime StartTimeFromTable, string CorrectedDate)
        {
            bool Status = false;
            int isIdleValue = 0;
            if (msgType == "BREAKDOWN")
            {
                isIdleValue = 1;
            }
            try
            {
                string starttimefromtableString = StartTimeFromTable.ToString("yyyy-MM-dd HH:mm:ss");
                MsqlConnection mc1 = new MsqlConnection();
                mc1.open();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO " + MsqlConnection.DbName + ".tblescalationlog(WCID,EscalationID,LossCodeID,IsIdle,EscalationSentOn,CorrectedDate,IsDeleted,CreatedOn,CreatedBy) VALUES (" + wcid + ",'" + escalationID + "','" + Reason + "','" + isIdleValue + "','" + starttimefromtableString + "','" + CorrectedDate + "',0,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1) ", mc1.msqlConnection);
                cmd2.ExecuteNonQuery();
                mc1.close();
                Status = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error :: InsertIntoEscLog : " + e);
            }
            return Status;
        }

        public string GetReasonPath(int reasonID)
        {
            string reasonpath = null;
            try
            {
                int level = 0;

                //step 1: Now Create path.
                DataTable dtReasonData = new DataTable();
                MsqlConnection mcReason = new MsqlConnection();
                mcReason.open();
                String queryReason = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE LossCodeID = " + reasonID;
                SqlDataAdapter daReason = new SqlDataAdapter(queryReason, mcReason.msqlConnection);
                daReason.Fill(dtReasonData);
                mcReason.close();

                for (int i = 0; i < dtReasonData.Rows.Count; i++)
                {
                    level = Convert.ToInt32(dtReasonData.Rows[i][4]);
                    if (level == 1)
                    {
                        reasonpath = dtReasonData.Rows[i][1].ToString();
                    }
                    else if (level == 2)
                    {
                        int reasonid1 = Convert.ToInt32(dtReasonData.Rows[0][5]);
                        DataTable dtReasonData1 = new DataTable();
                        MsqlConnection mcReason1 = new MsqlConnection();
                        mcReason1.open();
                        String queryReason1 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE LossCodeID = " + reasonid1;
                        SqlDataAdapter daReason1 = new SqlDataAdapter(queryReason1, mcReason1.msqlConnection);
                        daReason1.Fill(dtReasonData1);
                        mcReason1.close();
                        for (int j = 0; j < dtReasonData1.Rows.Count; j++)
                        {
                            reasonpath = dtReasonData1.Rows[j][1].ToString();
                        }
                        reasonpath += " --> " + dtReasonData.Rows[i][1].ToString();
                    }
                    else if (level == 3)
                    {
                        int reasonid1 = Convert.ToInt32(dtReasonData.Rows[0][5]);
                        DataTable dtReasonData1 = new DataTable();
                        MsqlConnection mcReason1 = new MsqlConnection();
                        mcReason1.open();
                        String queryReason1 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE LossCodeID = " + reasonid1;
                        SqlDataAdapter daReason1 = new SqlDataAdapter(queryReason1, mcReason1.msqlConnection);
                        daReason1.Fill(dtReasonData1);
                        mcReason1.close();
                        for (int j = 0; j < dtReasonData1.Rows.Count; j++)
                        {
                            reasonpath = dtReasonData1.Rows[j][1].ToString();
                        }

                        int reasonid2 = Convert.ToInt32(dtReasonData.Rows[0][6]);

                        DataTable dtReasonData3 = new DataTable();
                        MsqlConnection mcReason3 = new MsqlConnection();
                        mcReason3.open();
                        String queryReason3 = "SELECT * From " + MsqlConnection.DbName + ".tbllossescodes WHERE LossCodeID = " + reasonid2;
                        SqlDataAdapter daReason3 = new SqlDataAdapter(queryReason3, mcReason3.msqlConnection);
                        daReason3.Fill(dtReasonData3);
                        mcReason3.close();
                        for (int k = 0; k < dtReasonData3.Rows.Count; k++)
                        {
                            reasonpath += " --> " + dtReasonData3.Rows[k][1].ToString();
                        }

                        reasonpath += " --> " + dtReasonData.Rows[i][1].ToString();
                    }
                }
            }
            catch (Exception)
            {

            }

            return reasonpath;
        }

        public void EscalatEmailForNoCode()
        {
            try
            {
                DateTime toDayTime = DateTime.Now;
                string toDay = toDayTime.ToString("yyyy-MM-dd");
                DataTable machDT = new DataTable();
                string machineQuery = "select *  FROM " + MsqlConnection.DbName + ".[tblmachinedetails] where isdeleted=0 and isnormalWC=0 ";
                using (MsqlConnection mcMachine = new MsqlConnection())
                {
                    mcMachine.open();
                    SqlDataAdapter machSDA = new SqlDataAdapter(machineQuery, mcMachine.msqlConnection);
                    machSDA.Fill(machDT);
                    mcMachine.close();
                }

                int column = machDT.Columns.Count;

                for (int i = 0; i < machDT.Rows.Count; i++)
                {
                    int machineId = Convert.ToInt32(machDT.Rows[i][0]);
                    int cellId = Convert.ToInt32(machDT.Rows[i][8]);
                    int shopId = Convert.ToInt32(machDT.Rows[i][7]);
                    int plantId = Convert.ToInt32(machDT.Rows[i][6]);
                    //machineId = 990;
                    DataTable lossDT = new DataTable();
                    string lossQuery = "select top 1 * from " + MsqlConnection.DbName + ".tbllivelossofentry where MachineID=" + machineId + " and MessageCodeID=999 and CorrectedDate='" + toDay + "' and DoneWithRow=0 order by LossID desc";
                    using (MsqlConnection mcLoss = new MsqlConnection())
                    {
                        mcLoss.open();
                        SqlDataAdapter lossSDA = new SqlDataAdapter(lossQuery, mcLoss.msqlConnection);
                        lossSDA.Fill(lossDT);
                        mcLoss.close();
                    }
                    if (lossDT.Rows.Count > 0)
                    {
                        DateTime lossStartTime = Convert.ToDateTime(lossDT.Rows[0][2]);
                        int duration = Convert.ToInt32(toDayTime.Subtract(lossStartTime).TotalMinutes);
                        if (duration > 5)
                        {
                            DataTable exclaLogDT = new DataTable();
                            string exclaLogQuery = "select top 1 * FROM " + MsqlConnection.DbName + ".[tblescalationlog] where LossCodeID=999 and EscalationSentOn='" + lossStartTime + "' and CorrectedDate='" + toDay + "' and WCID=" + machineId + " order by ELID desc";
                            using (MsqlConnection mcexclaLog = new MsqlConnection())
                            {
                                mcexclaLog.open();
                                SqlDataAdapter exclaLogSDA = new SqlDataAdapter(exclaLogQuery, mcexclaLog.msqlConnection);
                                exclaLogSDA.Fill(exclaLogDT);
                                mcexclaLog.close();
                            }
                            if (exclaLogDT.Rows.Count > 0)
                            {
                                // alredy sent mail do nothing
                            }
                            else
                            {
                                DataTable exclaDT = new DataTable();
                                string exclaQuery = "select * FROM " + MsqlConnection.DbName + ".[tblemailescalation] where ReasonLevel1=999 and WorkCenterID=" + machineId + "";
                                using (MsqlConnection mcexcla = new MsqlConnection())
                                {
                                    mcexcla.open();
                                    SqlDataAdapter exclaSDA = new SqlDataAdapter(exclaQuery, mcexcla.msqlConnection);
                                    exclaSDA.Fill(exclaDT);
                                    mcexcla.close();
                                }
                                if (exclaDT.Rows.Count > 0)
                                {
                                    // call mail
                                    int escID = Convert.ToInt32(exclaDT.Rows[0][0]);
                                    bool check = sendMail(escID, lossStartTime, machineId, 999);
                                    if (check)
                                    {
                                        check = InsertIntoEscLog(machineId, escID, 999, "NO Code", lossStartTime, toDay);
                                        if (!check)
                                        {
                                            IntoFile("Failed to sent mail");
                                        }
                                    }
                                    else
                                    {
                                        IntoFile("Failed to sent mail");
                                    }
                                }
                                else
                                {
                                    using (MsqlConnection mcexcla = new MsqlConnection())
                                    {
                                        exclaQuery = "select * FROM " + MsqlConnection.DbName + ".[tblemailescalation] where ReasonLevel1=999 and CellID=" + cellId + "";
                                        mcexcla.open();
                                        SqlDataAdapter exclaSDA = new SqlDataAdapter(exclaQuery, mcexcla.msqlConnection);
                                        exclaSDA.Fill(exclaDT);
                                        mcexcla.close();
                                    }
                                    if (exclaDT.Rows.Count > 0)
                                    {
                                        // call mail
                                        int escID = Convert.ToInt32(exclaDT.Rows[0][0]);
                                        bool check = sendMail(escID, lossStartTime, machineId, 999);
                                        if (check)
                                        {
                                            check = InsertIntoEscLog(machineId, escID, 999, "NO Code", lossStartTime, toDay);
                                            if (!check)
                                            {
                                                IntoFile("Failed to sent mail");
                                            }
                                        }
                                        else
                                        {
                                            IntoFile("Failed to sent mail");
                                        }
                                    }
                                    else
                                    {
                                        using (MsqlConnection mcexcla = new MsqlConnection())
                                        {
                                            exclaQuery = "select * FROM " + MsqlConnection.DbName + ".[tblemailescalation] where ReasonLevel1=999 and ShopID=" + shopId + "";
                                            mcexcla.open();
                                            SqlDataAdapter exclaSDA = new SqlDataAdapter(exclaQuery, mcexcla.msqlConnection);
                                            exclaSDA.Fill(exclaDT);
                                            mcexcla.close();
                                        }
                                        if (exclaDT.Rows.Count > 0)
                                        {
                                            // call mail
                                            int escID = Convert.ToInt32(exclaDT.Rows[0][0]);
                                            bool check = sendMail(escID, lossStartTime, machineId, 999);
                                            if (check)
                                            {
                                                check = InsertIntoEscLog(machineId, escID, 999, "NO Code", lossStartTime, toDay);
                                                if (!check)
                                                {
                                                    IntoFile("Failed to sent mail");
                                                }
                                            }
                                            else
                                            {
                                                IntoFile("Failed to sent mail");
                                            }
                                        }
                                        else
                                        {
                                            using (MsqlConnection mcexcla = new MsqlConnection())
                                            {
                                                exclaQuery = "select * FROM " + MsqlConnection.DbName + ".[tblemailescalation] where ReasonLevel1=999 and PlantID=" + shopId + "";
                                                mcexcla.open();
                                                SqlDataAdapter exclaSDA = new SqlDataAdapter(exclaQuery, mcexcla.msqlConnection);
                                                exclaSDA.Fill(exclaDT);
                                                mcexcla.close();
                                            }
                                            if (exclaDT.Rows.Count > 0)
                                            {
                                                // call mail
                                                int escID = Convert.ToInt32(exclaDT.Rows[0][0]);
                                                bool check = sendMail(escID, lossStartTime, machineId, 999);
                                                if (check)
                                                {
                                                    check = InsertIntoEscLog(machineId, escID, 999, "NO Code", lossStartTime, toDay);
                                                    if (!check)
                                                    {
                                                        IntoFile("Failed to sent mail");
                                                    }
                                                }
                                                else
                                                {
                                                    IntoFile("Failed to sent mail");
                                                }
                                            }
                                            else
                                            {
                                                // no exclation created to send mail
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // not greater than 5 
                        }
                    }
                    else
                    {
                        // no records in tbllossofentry table
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile(ex.ToString());
            }
        }

        public void EscalationEmailForHolCode()
        {

        }

        #endregion

        #region Critical Machines

        public void EmailEscalatorForCriticalMachine(string CorrectedDate)
        {
            //Step 1: Get Machine or Group of Machines
            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".tblemailescalation WHERE IsDeleted = 0 and IsCritical=1";
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();

            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                string Plantid = Convert.ToString(dtEscData.Rows[i][6]);
                string Shopid = Convert.ToString(dtEscData.Rows[i][7]);
                string Cellid = Convert.ToString(dtEscData.Rows[i][8]);
                string WCid = Convert.ToString(dtEscData.Rows[i][9]);
                int ThisEscHours = Convert.ToInt32(dtEscData.Rows[i][10]);
                int ThisEscMinutes = Convert.ToInt32(dtEscData.Rows[i][11]);
                int ThisEscDuration = (ThisEscHours > 0 ? ThisEscHours * 60 : 0) + ThisEscMinutes;
                int escalationID = Convert.ToInt32(dtEscData.Rows[i][0]);
                string msgType = Convert.ToString(dtEscData.Rows[i][2]);

                //if (Shopid != null)
                //if (!DBNull.Value.Equals(Shopid))
                int shopidINT = 0;
                if (int.TryParse(Shopid, out shopidINT))
                {
                    //if (Cellid != null)
                    //if (!DBNull.Value.Equals(Cellid))
                    int cellidINT = 0;
                    if (int.TryParse(Cellid, out cellidINT))
                    {
                        //if (WCid != null)
                        //if (!DBNull.Value.Equals(WCid))
                        int wcidINT = 0;
                        if (int.TryParse(WCid, out wcidINT))
                        {
                            int wcidInteger = Convert.ToInt32(wcidINT);
                            EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                        }
                        else //Plant , Shop , Cell Only
                        {
                            int CellIDInteger = Convert.ToInt32(cellidINT);
                            DataTable dtMacData = new DataTable();
                            MsqlConnection mcMacData = new MsqlConnection();
                            mcMacData.open();
                            String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE  IsDeleted = 0 and CellID = " + CellIDInteger;
                            SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                            daMacData.Fill(dtMacData);
                            mcMacData.close();
                            for (int j = 0; j < dtMacData.Rows.Count; j++)
                            {
                                int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                                EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                            }
                        }
                    }
                    else //Plant & Shop Only
                    {
                        int ShopIDInteger = Convert.ToInt32(shopidINT);
                        DataTable dtMacData = new DataTable();
                        MsqlConnection mcMacData = new MsqlConnection();
                        mcMacData.open();
                        String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails  WHERE  IsDeleted = 0 and ShopID = " + ShopIDInteger;
                        SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                        daMacData.Fill(dtMacData);
                        mcMacData.close();
                        for (int j = 0; j < dtMacData.Rows.Count; j++)
                        {
                            int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                            EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                        }
                    }
                }
                else //Just Plant
                {
                    int PlantIDInteger = Convert.ToInt32(Plantid);
                    DataTable dtMacData = new DataTable();
                    MsqlConnection mcMacData = new MsqlConnection();
                    mcMacData.open();
                    String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE  IsDeleted = 0 and PlantID = " + PlantIDInteger;
                    SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                    daMacData.Fill(dtMacData);
                    mcMacData.close();
                    for (int j = 0; j < dtMacData.Rows.Count; j++)
                    {
                        int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                        EmailEscalationForWC(escalationID, msgType, wcidInteger, ThisEscDuration, CorrectedDate);
                    }
                }
            }
        }

        #endregion


        public void IntoFile(string Msg)
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = Application.StartupPath + @"\EscalationLogFile.txt";
            using (StreamWriter writer = new StreamWriter(appPath, true)) //true => Append Text
            {
                writer.WriteLine(System.DateTime.Now + ":  " + Msg);
            }

        }

        #region Mode greater than 10 minutes mail trigger    

        private void MyTimer_Tick1(object sender, EventArgs e)
        {
            PMS_Scheduling(CorrectedDate);
        }

        public void ModeMoreThanTenMinutes()
        {
            try
            {
                DataTable machDet = new DataTable();
                string machQuery = "Select MachineID FROM " + MsqlConnection.DbName + ".[tblmachinedetails] where IsDeleted=0 and IsNormalWC=0";
                using (MsqlConnection machConn = new MsqlConnection())
                {
                    machConn.open();
                    SqlDataAdapter mach = new SqlDataAdapter(machQuery, machConn.msqlConnection);
                    mach.Fill(machDet);
                    machConn.close();
                }
                //int lossId = Convert.ToInt32(ConfigurationManager.AppSettings["lossid"]);
                //int escalationID = Convert.ToInt32(ConfigurationManager.AppSettings["exId"]);
                int duration = Convert.ToInt32(ConfigurationManager.AppSettings["duration"]);

                for (int i = 0; i < machDet.Rows.Count; i++)
                {
                    int WCid = Convert.ToInt32(machDet.Rows[i][0]);
                    DataTable dtDuration = new DataTable();
                    string query = "select StartTime,Mode FROM " + MsqlConnection.DbName + ".[tbllivemode] where machineid=" + WCid + " and iscompleted=0";
                    using (MsqlConnection mclossData = new MsqlConnection())
                    {
                        mclossData.open();
                        SqlDataAdapter modeMachine = new SqlDataAdapter(query, mclossData.msqlConnection);
                        modeMachine.Fill(dtDuration);
                        mclossData.close();
                    }

                    if (dtDuration.Rows.Count > 0)
                    {
                        DateTime startDateTime = Convert.ToDateTime(dtDuration.Rows[0][0]);
                        string mode = Convert.ToString(dtDuration.Rows[0][1]);
                        string CorrectedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        DateTime endDateTime = DateTime.Now;
                        int durationInSec = Convert.ToInt32(endDateTime.Subtract(startDateTime).TotalMinutes);
                        if (durationInSec >= duration && (mode != "BREAKDOWN" || mode != "breakdown"))
                        {
                            DataTable dtEscLogData = new DataTable();
                            using (MsqlConnection mcEscLog = new MsqlConnection())
                            {
                                mcEscLog.open();
                                String queryEscLog = "Select top 1 * FROM " + MsqlConnection.DbName + ".[ModeLog] where MachineId=" + WCid + " and MailSentDateTime='" + startDateTime + "' order by mid desc";
                                SqlDataAdapter daEscLog = new SqlDataAdapter(queryEscLog, mcEscLog.msqlConnection);
                                daEscLog.Fill(dtEscLogData);
                                mcEscLog.close();
                            }

                            if (dtEscLogData.Rows.Count == 0)
                            {
                                bool status = sendMailMode(startDateTime, WCid);
                                if (status)
                                {
                                    bool InsertEsc = InsertIntoModeLog(WCid, startDateTime);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public bool sendMailMode(DateTime StartTimeFromEscLogTable, int WCID)
        {
            bool Status = false;
            try
            {
                string Hierarchy = null;
                string MachineName = null;
                string plantName = null;
                string shopName = null;
                string cellName = null;
                DataTable dtMacData = new DataTable();
                using (MsqlConnection mclossData = new MsqlConnection())
                {
                    mclossData.open();
                    String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE MachineID = " + WCID;
                    SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    dalossData.Fill(dtMacData);
                    mclossData.close();
                }

                if (dtMacData.Rows.Count > 0)
                {
                    MachineName = dtMacData.Rows[0][13].ToString();
                    int PlantID = 0, ShopID = 0, CellID = 0;
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][6]), out PlantID);
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][7]), out ShopID);
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][8]), out CellID);

                    if (PlantID != 0)
                    {
                        DataTable dtPlantData = new DataTable();
                        using (MsqlConnection mcPlantData = new MsqlConnection())
                        {
                            mcPlantData.open();
                            String queryPlantData = "SELECT PlantName From " + MsqlConnection.DbName + ".tblplant WHERE PlantID = " + PlantID;
                            SqlDataAdapter daPlantData = new SqlDataAdapter(queryPlantData, mcPlantData.msqlConnection);
                            daPlantData.Fill(dtPlantData);
                            mcPlantData.close();
                        }

                        if (dtPlantData.Rows.Count > 0)
                        {
                            plantName = dtPlantData.Rows[0][0].ToString();
                            Hierarchy = plantName;
                        }
                    }

                    if (ShopID != 0)
                    {
                        DataTable dtShopData = new DataTable();
                        using (MsqlConnection mcShopData = new MsqlConnection())
                        {
                            mcShopData.open();
                            String queryShopData = "SELECT ShopName From " + MsqlConnection.DbName + ".tblshop WHERE ShopID = " + ShopID;
                            SqlDataAdapter daShopData = new SqlDataAdapter(queryShopData, mcShopData.msqlConnection);
                            daShopData.Fill(dtShopData);
                            mcShopData.close();
                        }
                        if (dtShopData.Rows.Count > 0)
                        {
                            shopName = dtShopData.Rows[0][0].ToString();
                            Hierarchy += "-->" + shopName;
                        }
                    }
                    if (CellID != 0)
                    {
                        DataTable dtCellData = new DataTable();
                        using (MsqlConnection mcCellData = new MsqlConnection())
                        {
                            mcCellData.open();
                            String queryCellData = "SELECT CellName From " + MsqlConnection.DbName + ".tblcell WHERE CellID = " + CellID;
                            SqlDataAdapter daCellData = new SqlDataAdapter(queryCellData, mcCellData.msqlConnection);
                            daCellData.Fill(dtCellData);
                            mcCellData.close();
                        }
                        if (dtCellData.Rows.Count > 0)
                        {
                            cellName = dtCellData.Rows[0][0].ToString();
                            Hierarchy += "-->" + cellName;
                        }
                    }
                    Hierarchy += "-->" + MachineName;

                }

                DataTable dtEscData = new DataTable();
                using (MsqlConnection mcEscData = new MsqlConnection())
                {
                    mcEscData.open();
                    String queryEscData = "Select * FROM " + MsqlConnection.DbName + ".[modeEMails] where isdeleted=0";
                    SqlDataAdapter daEscData = new SqlDataAdapter(queryEscData, mcEscData.msqlConnection);
                    daEscData.Fill(dtEscData);
                    mcEscData.close();
                }
                string toMailIDs = dtEscData.Rows[0][1].ToString();
                string ccMailIDs = dtEscData.Rows[0][2].ToString();
                string MessageType = dtEscData.Rows[0][6].ToString();

                string Duration = (Convert.ToInt32(DateTime.Now.Subtract(StartTimeFromEscLogTable).TotalMinutes) + 1).ToString();
                //string ReasonPath = GetReasonPath(reasonID);

                MailMessage mail = new MailMessage();

                string[] Tomails = toMailIDs.Split(',');
                foreach (string mailid in Tomails)
                {
                    string mailID = Convert.ToString(mailid).Trim();
                    if (!string.IsNullOrEmpty(mailID))
                    {
                        mail.To.Add(new MailAddress(mailid));
                    }
                }

                string[] Ccmails = ccMailIDs.Split(',');
                foreach (string mailid in Ccmails)
                {
                    string mailID = Convert.ToString(mailid).Trim();
                    if (!string.IsNullOrEmpty(mailID))
                    {
                        mail.CC.Add(new MailAddress(mailid));
                    }
                }


                //mail.From = new MailAddress("narendramourya37@gmail.com");

                mail.From = new MailAddress("swamy@blr.shaktiprecision.in");
                //mail.Subject = MachineName + " " + MessageType;
                mail.Subject = "ContinuousMode - " + MachineName;
                string MailBodyMsg = null;
                if (MessageType == "IDLE")
                {
                    MailBodyMsg = "Work stoppage stage";
                }
                else
                {
                    MailBodyMsg = MessageType;
                }

                //mail.Body = "<p><b>Dear Concerned,</b></p>" +
                //            "<b></b>" +
                //            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine " + MachineName + " has gone into " + MailBodyMsg + " for " + reason + " at " + StartTimeFromEscLogTable + " &nbsp;<span>.</b></p>" +
                //            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  This Mail has been escalated for following Reasons " + ReasonPath + "  after " + Duration + " Minutes. &nbsp;<span>.</b></p>" +
                //            "<p><b><br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Note: This Email has been sent for the demo purpose of EMail Escalation. &nbsp;<span>.</b></p>" +
                //            "<p><b></b></p>";

                mail.Body = "<p><b>Dear Concerned,</b></p>" +
                            "<p><b></b></p>" +
                            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine </b></p>" +
                            "<p><b> " + Hierarchy + "</b></p>" +
                             "<table><tr> <td> From " + StartTimeFromEscLogTable + " .</td>  <td> Escalated after " + Duration + " Minutes</td></tr></table>" +

                             "<p><b></b></p>" +
                             "<p><b></b></p>" +
                             "<p><b> Regards,</b></p>" +
                            "<p><b> UnitWorks</b></p>" +

                             "<p><b></b></p>" +
                             "<p><b></b></p>" +
                            "<p><b>Note: This is an autogenerated E-Mail for the Escalation Process. Do Not Reply back on this Mail.</b></p>" +
                            "<p><b></b></p>";

                //mail.Bcc = new MailAddress("janardhan.g@srkssolutions.com");

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "10.30.10.57";
                smtp.Host = "192.168.1.252";
                smtp.Port = 465;
                smtp.UseDefaultCredentials = false;
                //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtp.Credentials = new System.Net.NetworkCredential("swamy@blr.shaktiprecision.in", "Swam@2020", "blr.shaktiprecision.in");
                //smtp.Credentials = new System.Net.NetworkCredential("unitworks@tasl.aero", "527293");
                smtp.EnableSsl = SSL;
                smtp.Send(mail);
                Status = true;
                // MessageBox.Show("Status : success ");
            }
            catch (Exception e)
            {
                // MessageBox.Show("Error :: sendMail : " + e);
                Status = false;
                IntoFile("Error While Sending Mail " + e);
            }
            return Status;
        }

        public bool InsertIntoModeLog(int wcid, DateTime StartTimeFromTable)
        {
            bool Status = false;
            try
            {
                string starttimefromtableString = StartTimeFromTable.ToString("yyyy-MM-dd HH:mm:ss");
                using (MsqlConnection mc1 = new MsqlConnection())
                {
                    mc1.open();
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO " + MsqlConnection.DbName + ".ModeLog(MailSentDateTime,MachineId,CreatedOn,CreatedBy) VALUES ('" + StartTimeFromTable + "','" + wcid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1) ", mc1.msqlConnection);
                    cmd2.ExecuteNonQuery();
                    mc1.close();
                }
                Status = true;
            }
            catch (Exception e)
            {
                IntoFile("Error:: InsertIntoModeLog" + e.ToString());
                //MessageBox.Show("Error :: InsertIntoModeLog : " + e);
            }
            return Status;
        }

        #endregion

        #region PMS Scheduling
        public void PMS_Scheduling(string CorrectedDate)
        {
            DataTable dtEscData = new DataTable();
            MsqlConnection mc = new MsqlConnection();
            mc.open();
            String query = "SELECT * From " + MsqlConnection.DbName + ".TblPMSNotification_Master WHERE IsDeleted = 0 ";
            SqlDataAdapter da = new SqlDataAdapter(query, mc.msqlConnection);
            da.Fill(dtEscData);
            mc.close();
            for (int i = 0; i < dtEscData.Rows.Count; i++)
            {
                int escalationID = Convert.ToInt32(dtEscData.Rows[i][0]);
                string Plantid = Convert.ToString(dtEscData.Rows[i][1]);
                string Shopid = Convert.ToString(dtEscData.Rows[i][2]);
                string Cellid = Convert.ToString(dtEscData.Rows[i][3]);
                string WCid = Convert.ToString(dtEscData.Rows[i][4]);
                int PriorDays = Convert.ToInt32(dtEscData.Rows[i][8]);

                //if (Shopid != null)
                //if (!DBNull.Value.Equals(Shopid))
                int shopidINT = 0;
                if (int.TryParse(Shopid, out shopidINT))
                {
                    //if (Cellid != null)
                    //if (!DBNull.Value.Equals(Cellid))
                    int cellidINT = 0;
                    if (int.TryParse(Cellid, out cellidINT))
                    {
                        //if (WCid != null)
                        //if (!DBNull.Value.Equals(WCid))
                        int wcidINT = 0;
                        if (int.TryParse(WCid, out wcidINT))
                        {
                            int wcidInteger = Convert.ToInt32(wcidINT);
                            PMS_EMAILEscalationforWC(escalationID, wcidInteger, PriorDays, CorrectedDate);
                        }
                        else //Plant , Shop , Cell Only
                        {
                            int CellIDInteger = Convert.ToInt32(cellidINT);
                            DataTable dtMacData = new DataTable();
                            MsqlConnection mcMacData = new MsqlConnection();
                            mcMacData.open();
                            String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE  IsDeleted = 0 and CellID = " + CellIDInteger;
                            SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                            daMacData.Fill(dtMacData);
                            mcMacData.close();
                            for (int j = 0; j < dtMacData.Rows.Count; j++)
                            {
                                int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                                PMS_EMAILEscalationforWC(escalationID, wcidInteger, PriorDays, CorrectedDate);
                            }
                        }
                    }
                    else //Plant & Shop Only
                    {
                        int ShopIDInteger = Convert.ToInt32(shopidINT);
                        DataTable dtMacData = new DataTable();
                        MsqlConnection mcMacData = new MsqlConnection();
                        mcMacData.open();
                        String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails  WHERE  IsDeleted = 0 and ShopID = " + ShopIDInteger;
                        SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                        daMacData.Fill(dtMacData);
                        mcMacData.close();
                        for (int j = 0; j < dtMacData.Rows.Count; j++)
                        {
                            int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                            PMS_EMAILEscalationforWC(escalationID, wcidInteger, PriorDays, CorrectedDate);
                        }
                    }
                }
                else //Just Plant
                {
                    int PlantIDInteger = Convert.ToInt32(Plantid);
                    DataTable dtMacData = new DataTable();
                    MsqlConnection mcMacData = new MsqlConnection();
                    mcMacData.open();
                    String queryMacData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE  IsDeleted = 0 and PlantID = " + PlantIDInteger;
                    SqlDataAdapter daMacData = new SqlDataAdapter(queryMacData, mcMacData.msqlConnection);
                    daMacData.Fill(dtMacData);
                    mcMacData.close();
                    for (int j = 0; j < dtMacData.Rows.Count; j++)
                    {
                        int wcidInteger = Convert.ToInt32(dtMacData.Rows[j][0]);
                        PMS_EMAILEscalationforWC(escalationID, wcidInteger, PriorDays, CorrectedDate);

                    }
                }
            }
        }

        public void PMS_EMAILEscalationforWC(int escalationID, int WCid, int Num_PriorDays, string CorrectedDate)
        {
            DataTable dtPMSScheduleData = new DataTable();
            CultureInfo cul = CultureInfo.CurrentCulture;
            int currenmonth = Convert.ToInt32(DateTime.Now.Month);
            int year = Convert.ToInt32(DateTime.Now.Date.Year);
            int weekNum = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int weeknumberbyMonth = GetWeekNumberOfMonth(Convert.ToDateTime(CorrectedDate));

            try
            {
                DateTime escalationDate = getDatetime(year, weekNum).AddDays(-Num_PriorDays);

                using (MsqlConnection mclossData = new MsqlConnection())
                {
                    mclossData.open();

                    String querylossData = "select * FROM " + MsqlConnection.DbName + ".configuration_tblprimitivemaintainancescheduling where MachineID=" + WCid + " and MonthID='" + currenmonth + "' and [Week]=" + weeknumberbyMonth + "";
                    SqlDataAdapter daPMSData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                    daPMSData.Fill(dtPMSScheduleData);
                    mclossData.close();
                }

                for (int j = 0; j < dtPMSScheduleData.Rows.Count; j++)
                {
                    int PMScheduleID = Convert.ToInt32(dtPMSScheduleData.Rows[j][0]);
                    int weekNumber = Convert.ToInt32(dtPMSScheduleData.Rows[j][4]);
                    string Month = Convert.ToString(dtPMSScheduleData.Rows[j][3]);
                    DateTime Escalationdate = escalationDate;
                    DateTime DTcorrectedDate = Convert.ToDateTime(CorrectedDate);

                    if (Escalationdate < DTcorrectedDate)
                    {
                        DataTable dtEscLogData = new DataTable();
                        MsqlConnection mcEscLog = new MsqlConnection();
                        mcEscLog.open();
                        String queryEscLog = "SELECT Top 1 * From " + MsqlConnection.DbName + ".tblescalationlog WHERE IsDeleted = 0 and WCID = " + WCid + " and PMS_ESCID = " + escalationID + " and PMSID=" + PMScheduleID + " and CorrectedDate = '" + CorrectedDate + "' and EscalationSentOn = '" + escalationDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY CreatedOn desc";
                        SqlDataAdapter daEscLog = new SqlDataAdapter(queryEscLog, mcEscLog.msqlConnection);
                        daEscLog.Fill(dtEscLogData);
                        mcEscLog.close();

                        if (dtEscLogData.Rows.Count == 0)
                        {
                            bool status = PMS_sendMail(escalationID, escalationDate, WCid, PMScheduleID, weekNumber, Month);
                            if (status)
                            {
                                bool InsertEsc = false;
                                InsertEsc = PMS_InsertIntoEscLog(WCid, 0, PMScheduleID, escalationID, "PMS", escalationDate, CorrectedDate);
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

        public int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public DateTime getDatetime(int year, int weekOfYear)
        {

            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear * 7);
        }

        public int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DayOfWeek dayofweek = date.DayOfWeek;

            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }


        public bool PMS_sendMail(int escID, DateTime StartTimeFromEscLogTable, int WCID, int PMSID,int weekNumber, string Month)
        {
            bool Status = false;
            try
            {
                string Hierarchy = null;
                string MachineName = null;
                string plantName = null;
                string shopName = null;
                string cellName = null;
                DataTable dtMacData = new DataTable();
                MsqlConnection mclossData = new MsqlConnection();
                mclossData.open();
                String querylossData = "SELECT * From " + MsqlConnection.DbName + ".tblmachinedetails WHERE MachineID = " + WCID;
                SqlDataAdapter dalossData = new SqlDataAdapter(querylossData, mclossData.msqlConnection);
                dalossData.Fill(dtMacData);
                mclossData.close();

                if (dtMacData.Rows.Count > 0)
                {
                    MachineName = dtMacData.Rows[0][10].ToString();
                    int PlantID = 0, ShopID = 0, CellID = 0;
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][6]), out PlantID);
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][7]), out ShopID);
                    int.TryParse(Convert.ToString(dtMacData.Rows[0][8]), out CellID);

                    if (PlantID != 0)
                    {
                        DataTable dtPlantData = new DataTable();
                        MsqlConnection mcPlantData = new MsqlConnection();
                        mcPlantData.open();
                        String queryPlantData = "SELECT PlantName From " + MsqlConnection.DbName + ".tblplant WHERE PlantID = " + PlantID;
                        SqlDataAdapter daPlantData = new SqlDataAdapter(queryPlantData, mcPlantData.msqlConnection);
                        daPlantData.Fill(dtPlantData);
                        mcPlantData.close();

                        if (dtPlantData.Rows.Count > 0)
                        {
                            plantName = dtPlantData.Rows[0][0].ToString();
                            Hierarchy = plantName;
                        }
                    }

                    if (ShopID != 0)
                    {
                        DataTable dtShopData = new DataTable();
                        MsqlConnection mcShopData = new MsqlConnection();
                        mcShopData.open();
                        String queryShopData = "SELECT ShopName From " + MsqlConnection.DbName + ".tblshop WHERE ShopID = " + ShopID;
                        SqlDataAdapter daShopData = new SqlDataAdapter(queryShopData, mcShopData.msqlConnection);
                        daShopData.Fill(dtShopData);
                        mcShopData.close();

                        if (dtShopData.Rows.Count > 0)
                        {
                            shopName = dtShopData.Rows[0][0].ToString();
                            Hierarchy += "-->" + shopName;
                        }
                    }
                    if (CellID != 0)
                    {
                        DataTable dtCellData = new DataTable();
                        MsqlConnection mcCellData = new MsqlConnection();
                        mcCellData.open();
                        String queryCellData = "SELECT CellName From " + MsqlConnection.DbName + ".tblcell WHERE CellID = " + CellID;
                        SqlDataAdapter daCellData = new SqlDataAdapter(queryCellData, mcCellData.msqlConnection);
                        daCellData.Fill(dtCellData);
                        mcCellData.close();

                        if (dtCellData.Rows.Count > 0)
                        {
                            cellName = dtCellData.Rows[0][0].ToString();
                            Hierarchy += "-->" + cellName;
                        }
                    }
                    Hierarchy += "-->" + MachineName;

                }

                DataTable dtEscData = new DataTable();
                MsqlConnection mcEscData = new MsqlConnection();
                mcEscData.open();
                String queryEscData = "SELECT * From " + MsqlConnection.DbName + ".TblPMSNotification_Master WHERE PMSId = " + escID;
                SqlDataAdapter daEscData = new SqlDataAdapter(queryEscData, mcEscData.msqlConnection);
                daEscData.Fill(dtEscData);
                mcEscData.close();

                string toMailIDs = dtEscData.Rows[0][5].ToString();
                string ccMailIDs = dtEscData.Rows[0][6].ToString();
                //string WeekNumber = dtEscData.Rows[0][4].ToString();
                //string MonthName = dtEscData.Rows[0][3].ToString();
                string Duration = (Convert.ToInt32(DateTime.Now.Subtract(StartTimeFromEscLogTable).TotalMinutes) + 1).ToString();
                string ReasonPath = " Has Preventive Maintainance on "+weekNumber+ " week of "+ Month;

                MailMessage mail = new MailMessage();

                string[] Tomails = toMailIDs.Split(',');
                foreach (string mailid in Tomails)
                {
                    string mailID = Convert.ToString(mailid).Trim();
                    if (!string.IsNullOrEmpty(mailID))
                    {
                        mail.To.Add(new MailAddress(mailid));
                    }
                }

                string[] Ccmails = ccMailIDs.Split(',');
                foreach (string mailid in Ccmails)
                {
                    string mailID = Convert.ToString(mailid).Trim();
                    if (!string.IsNullOrEmpty(mailID))
                    {
                        mail.CC.Add(new MailAddress(mailid));
                    }
                }


                //mail.From = new MailAddress("narendramourya37@gmail.com");

                mail.From = new MailAddress(MsqlConnection.UserNamemail);
                //mail.Subject = MachineName + " " + MessageType;
                mail.Subject = " Preventive Maintainance  - " + MachineName;
                string MailBodyMsg = "Preventive Maintainance";              
                   

              

                mail.Body = "<p><b>Dear Concerned,</b></p>" +
                            "<p><b></b></p>" +
                            "<p><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is to inform you that machine </b></p>" +
                            "<p><b> " + Hierarchy + "</b></p>" +
                             "<table><tr> <td> " + ReasonPath +" So please plan accordingly  </td></tr></table>" +

                             "<p><b></b></p>" +
                             "<p><b></b></p>" +
                             "<p><b> Regards,</b></p>" +
                            "<p><b> Shakti Team </b></p>" +

                             "<p><b></b></p>" +
                             "<p><b></b></p>" +
                            "<p><b>Note: This is an autogenerated E-Mail for the Escalation Process. Do Not Reply back on this Mail.</b></p>" +
                            "<p><b></b></p>";

                //mail.Bcc = new MailAddress("janardhan.g@srkssolutions.com");

                mail.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = MsqlConnection.Host;
                    smtp.Port = MsqlConnection.Portmail;
                    smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    //smtp.Credentials = new System.Net.NetworkCredential(MsqlConnection.UserNamemail, MsqlConnection.Passwordmail, MsqlConnection.Domain);
                    smtp.Credentials = new System.Net.NetworkCredential(MsqlConnection.UserNamemail, MsqlConnection.Passwordmail);
                    //smtp.Credentials = new System.Net.NetworkCredential("unitworks@tasl.aero", "527293");                    
                    smtp.EnableSsl = SSL;
                    smtp.Send(mail);
                    Status = true;
                }
                // MessageBox.Show("Status : success ");
            }
            catch (Exception e)
            {
                // MessageBox.Show("Error :: sendMail : " + e);
                Status = false;
                IntoFile("Error While Sending Mail " + e);
            }
            return Status;
        }

        public bool PMS_InsertIntoEscLog(int wcid, int escalationID, int PMSID,int PMSESICID, string msgType, DateTime StartTimeFromTable, string CorrectedDate)
        {
            bool Status = false;
            int isIdleValue = 0;
            try
            {
                string starttimefromtableString = StartTimeFromTable.ToString("yyyy-MM-dd HH:mm:ss");
                MsqlConnection mc1 = new MsqlConnection();
                mc1.open();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO " + MsqlConnection.DbName + ".tblescalationlog(WCID,PMS_ESCID,PMSID,IsIdle,EscalationSentOn,CorrectedDate,IsDeleted,CreatedOn,CreatedBy) VALUES (" + wcid + ",'" + PMSESICID + "','" + PMSID + "','" + isIdleValue + "','" + starttimefromtableString + "','" + CorrectedDate + "',0,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1) ", mc1.msqlConnection);
                cmd2.ExecuteNonQuery();
                mc1.close();
                Status = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error :: InsertIntoEscLog : " + e);
            }
            return Status;
        }


        #endregion
    }
}
