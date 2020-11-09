using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TataSqlStoreProcedure
{
    public partial class Form1 : Form
    {
       

        public Form1()
        {
            InitializeComponent();
            //callofsp();
            //*SP_LossEntryIssueUpdateEvent();

            Timer MyTimer = new Timer();
            MyTimer.Interval = (60 * 10000); //10min            
            MyTimer.Enabled = true;
            MyTimer.Tick += new  EventHandler(MyTimer_Tick);
            MyTimer.Start();


            // no store procedure is calling here
            //Timer MyTimer1 = new Timer();
            //MyTimer1.Interval = (60 * 1000); //1min            
            //MyTimer1.Enabled = true;
            //MyTimer1.Tick += new EventHandler(MyTimer_Tick1);
            //MyTimer1.Start();

            Timer MyTimer2 = new Timer();
            MyTimer2.Interval = (120 * 10000); //20min            
            MyTimer2.Enabled = true;
            MyTimer2.Tick += new EventHandler(MyTimer_Tick2);
            MyTimer2.Start();

            Timer MyTimer3 = new Timer();
            MyTimer3.Interval = (180 * 10000); //30min            
            MyTimer3.Enabled = true;
            MyTimer3.Tick += new EventHandler(MyTimer_Tick3);
            MyTimer3.Start();


            Timer MyTimer4 = new Timer();
            TimeSpan t = new TimeSpan(1, 0, 0, 0);
            MyTimer4.Interval = Convert.ToInt32(t.TotalDays); //1 day            
            MyTimer4.Enabled = true;
            MyTimer4.Tick += new EventHandler(MyTimer_Tick4);
            MyTimer4.Start();

            //Timer MyTimer5 = new Timer();
            //TimeSpan t1 = new TimeSpan(1, 0, 0, 0);
            //MyTimer5.Interval = Convert.ToInt32(t1.TotalDays); //1 day          
            //MyTimer5.Enabled = true;
            //MyTimer5.Tick += new EventHandler(MyTimer_Tick5);
            //MyTimer5.Start();

            Timer MyTimer6 = new Timer();
            TimeSpan t2 = new TimeSpan(1, 0, 0, 0);
            MyTimer6.Interval = Convert.ToInt32(t2.TotalDays); //1 day           
            MyTimer6.Enabled = true;
            MyTimer6.Tick += new EventHandler(MyTimer_Tick6);
            MyTimer6.Start();


        }


        private async void MyTimer_Tick(object sender, EventArgs e)

        {
            try
            {
                await SP_DashboardLiveModeTblINUPP();
                IntoFile("Pushing into Live mode");
                }

            catch (Exception ex)
            {
                IntoFile("10 minute call store procedure SP_DashboardLiveModeTblINUPP error" + ex.ToString());
            }
            try
            {
                await SP_DashboardLiveHMIMWOSTblINUP();
            }
            catch (Exception ex)
            {
                IntoFile("10 minute call store procedure SP_DashboardLiveHMIMWOSTblINUP error" + ex.ToString());
            }
            try
            {
                await SP_DashboardLiveLossManualTblINUP();
            }
            catch (Exception ex)
            {
                IntoFile("10 minute call store procedure SP_DashboardLiveLossManualTblINUP error" + ex.ToString());
            }
            try
            {
                await SP_DeleteExtraRowsHMI();
            }
            catch(Exception ex)
            {
                IntoFile("10 minute call store procedure SP_DeleteExtraRowsHMI error" + ex.ToString());
            }
            try
            {
                await SP_DeleteNegativeLossDur();
            }
            catch (Exception ex)
            {
                IntoFile("10 minute call store procedure SP_DeleteNegativeLossDur error" + ex.ToString());
            }
            try
            {
                await SP_DeleteNegativeModeDuration();
            }
            catch(Exception ex)
            {
                IntoFile("10 minute call store procedure SP_DeleteNegativeModeDuration error" + ex.ToString());
            }
            try
            { 
                await SP_HMIUpdationofMonthYearWeek();
            }
            catch (Exception ex)
            {
                IntoFile("10 minute call store procedure SP_HMIUpdationofMonthYearWeek error" + ex.ToString());
            }

        }

        // every one minutes calling store procedure which includes wrappers.
        //private async void MyTimer_Tick1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //await SP_DPSToMode();
        //       // await SP_DuplicateLossUpdate();
        //       // await SP_LossEntryIssueUpdateEvent();
        //        //await SP_ModeToDPS();
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile("1 minute call store procedure error" + ex.ToString());
        //    }
        //}

        private async void MyTimer_Tick2(object sender, EventArgs e)
        {
            try
            {
                await SP_LossEntryUpdationofMonthYearWeek();
            }
            catch (Exception ex)
            {
                IntoFile("20 minute call store procedure error" + ex.ToString());
            }
        }

        private async void MyTimer_Tick3(object sender, EventArgs e)
        {
            try
            {
                await SP_ManualLossUpdationofMonthYearWeek();
            }
            catch (Exception ex)
            {
                IntoFile("30 minute call store procedure error" + ex.ToString());
            }
            try
            {
                await SP_ModeUpdationofMonthYearWeek();
            }
            catch (Exception ex)
            {
                IntoFile("30 minute call store procedure error" + ex.ToString());
            }
        }

        private async void MyTimer_Tick4(object sender, EventArgs e)
        {
            //try
            //{
            //    //await SP_liveDataDelAfterMaovedToHistorin();
            //    //await SP_OperationLogDelAfterFewdays();
            //}
            //catch (Exception ex)
            //{
            //    IntoFile("1 day call store procedure error" + ex.ToString());
            //}
            try
            { 
            await SP_parameters_masterDelAfterFewDays();
            }
            catch (Exception ex)
            {
                IntoFile("1 day call store procedure error" + ex.ToString());
            }
        }

        //private async void MyTimer_Tick5(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SP_DeleteExtraDupWorkorderFromddl();
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile("1 Day call store procedure error" + ex.ToString());
        //    }
        //}

        private async void MyTimer_Tick6(object sender, EventArgs e)
        {
            try
            {
                SP_DeleteReocrdsFromDDL();
            }
            catch (Exception ex)
            {
                IntoFile("1 Day call store procedure error" + ex.ToString());
            }
        }

        //private void Form1_Closed(object sender, EventArgs e)
        //{
        //    //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        //    //Application.Current.Shutdown();

        //    if ((MessageBox.Show("Would you like to play again?", "Message", MessageBoxButtons.YesNo)) ==
        //        DialogResult.Yes)
        //    {
        //        Application.Restart();
        //    }

        //    System.Diagnostics.Process.Start(Application.StartupPath);
        //    Application.Restart();
        //}


        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

          //System.Diagnostics.Process.Start(Application.StartupPath);
            Application.Restart();
        }


        #region 30 minutes once calling store procedure

        public async Task SP_ManualLossUpdationofMonthYearWeek()
        {
            try
            {
                var values = new List<tblmanuallossofentry>();

                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                     //values = db.tblmanuallossofentries.Where(m => m.ManualLossMonth == null && m.ManualLossYear == null && m.ManualLossWeekNumber == null && m.ManualLossQuarter == null).Select(m => new { m.CorrectedDate, m.MLossID }).ToList();

                    values = db.tblmanuallossofentries.Where(m => m.ManualLossMonth == null && m.ManualLossYear == null && m.ManualLossWeekNumber == null && m.ManualLossQuarter == null).ToList();
                }
                foreach (var item in values)
                {
                    using (MsqlConnection conn = new MsqlConnection())
                    {
                        conn.open();
                        SqlCommand cmd = new SqlCommand("ManualLossUpdationofMonthYearWeek", conn.msqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@V_Date", item.CorrectedDate);
                        cmd.Parameters.AddWithValue("@V_MLossID", item.MLossID);
                        cmd.ExecuteNonQuery();
                        conn.close();
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_ManualLossUpdationofMonthYearWeek() error" + ex.ToString());
            }
        }

        public async Task SP_ModeUpdationofMonthYearWeek()
        {
            try
            {
                var values = new List<tblmode>();

                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {

                     //values = db.tblmodes.Where(m => m.ModeMonth == null && m.ModeYear == null && m.ModeWeekNumber == null && m.ModeQuarter == null).Select(m => new { m.CorrectedDate, m.ModeID }).ToList();
                    values = db.tblmodes.Where(m => m.ModeMonth == null && m.ModeYear == null && m.ModeWeekNumber == null && m.ModeQuarter == null).ToList();
                }

                foreach (var item in values)
                {
                    using (MsqlConnection conn = new MsqlConnection())
                    {
                        conn.open();
                        SqlCommand cmd = new SqlCommand("ModeUpdationofMonthYearWeek", conn.msqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@V_Date", item.CorrectedDate);
                        cmd.Parameters.AddWithValue("@V_ModeID", item.ModeID);
                        cmd.ExecuteNonQuery();
                        conn.close();
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_ModeUpdationofMonthYearWeek() error" + ex.ToString());
            }
        }

        #endregion


        #region 20 minute once calling store procedure

        public async Task SP_LossEntryUpdationofMonthYearWeek()
        {
            var values = new List<tbllossofentry>();

            try
            {
                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                    // var values = db.tbllossofentries.Where(m => m.LossMonth == null && m.LossYear == null && m.LossWeekNumber == null && m.LossQuarter == null).Select(m => new { m.LossID, m.CorrectedDate }).ToList();

                    values = db.tbllossofentries.Where(m => m.LossMonth == null && m.LossYear == null && m.LossWeekNumber == null && m.LossQuarter == null).ToList();
                    foreach (var item in values)
                    {
                        using (MsqlConnection conn = new MsqlConnection())
                        {
                            conn.open();
                            SqlCommand cmd = new SqlCommand("LossEntryUpdationofMonthYearWeek", conn.msqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@V_Date", item.CorrectedDate);
                            cmd.Parameters.AddWithValue("@V_LossID", item.LossID);
                            cmd.ExecuteNonQuery();
                            conn.close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_LossEntryUpdationofMonthYearWeek() error" + ex.ToString());
            }
        }

        #endregion


        #region 1 minute once calling store procedure

        public async Task SP_DPSToMode()
        {
            var machineIds = new List<tblmachinedetail>();
            try
            {
                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                    //  var machineIds = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsPCB == 0 && m.IsNormalWC == 0).Select(m => m.MachineID).ToList();
                    machineIds = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsPCB == 0 && m.IsNormalWC == 0).ToList();
                    foreach (var machineId in machineIds)
                    {
                        using (MsqlConnection conn = new MsqlConnection())
                        {
                            conn.open();
                            SqlCommand cmd = new SqlCommand("DPSToMode", conn.msqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@V_MacID", machineId);
                            cmd.ExecuteNonQuery();
                            conn.close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DPSToMode() error" + ex.ToString());
            }
        }

        public async Task SP_DuplicateLossUpdate()
        {
            var machineIds =new List<tblmachinedetail>();

            try
            {
                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                    //var machineIds = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).Select(m => m.MachineID).ToList();
                    machineIds = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).ToList();

                    foreach (var machineId in machineIds)
                    {
                        using (MsqlConnection conn = new MsqlConnection())
                        {
                            conn.open();
                            SqlCommand cmd = new SqlCommand("DuplicateLossUpdate", conn.msqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@V_MacID", machineId);
                            cmd.ExecuteNonQuery();
                            conn.close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DuplicateLossUpdate() error" + ex.ToString());
            }
        }

        public async Task SP_LossEntryIssueUpdateEvent()
        {
            var machineIds = new List<tblmachinedetail>();
            try
            {
                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                    // var machineIds = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).Select(m => m.MachineID).ToList();
                    machineIds = db.tblmachinedetails.Where(m => m.IsDeleted == 0 && m.IsNormalWC == 0).ToList();

                    foreach (var machineId in machineIds)
                    {
                        using (MsqlConnection conn = new MsqlConnection())
                        {
                            conn.open();
                            SqlCommand cmd = new SqlCommand("LossEntryIssueUpdate", conn.msqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@V_MacID", machineId);
                            cmd.ExecuteNonQuery();
                            conn.close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_LossEntryIssueUpdateEvent() error" + ex.ToString());
            }
        }

        public async Task SP_ModeToDPS()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MsqlConnection mc = new MsqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("GetRows", mc.msqlConnection))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            mc.open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@mode", "mode");
                            sda.Fill(dt);
                            mc.close();
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string mode = dt.Rows[i][0].ToString();
                    int machineId = Convert.ToInt32(dt.Rows[i][1]);

                    using (MsqlConnection conn = new MsqlConnection())
                    {
                        conn.open();
                        SqlCommand cmd = new SqlCommand("ModeToDPS", conn.msqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Mode", mode);
                        cmd.Parameters.AddWithValue("@MacID", machineId);
                        cmd.ExecuteNonQuery();
                        conn.close();
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_ModeToDPS() error" + ex.ToString());
            }
        }


        #endregion

        #region One day once calling region store procedure

        //public async Task SP_liveDataDelAfterMaovedToHistorin()
        //{
        //    try
        //    {
        //        using (MsqlConnection conn = new MsqlConnection())
        //        {
        //            conn.open();
        //            SqlCommand cmd = new SqlCommand("liveDataDelAfterMaovedToHistorin", conn.msqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //            conn.close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile("SP_liveDataDelAfterMaovedToHistorin() error" + ex.ToString());
        //    }
        //}

        //public async Task SP_OperationLogDelAfterFewdays()
        //{
        //    try
        //    {
        //        using (MsqlConnection conn = new MsqlConnection())
        //        {
        //            conn.open();
        //            SqlCommand cmd = new SqlCommand("OperationLogDelAfterFewdays", conn.msqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //            conn.close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile("SP_OperationLogDelAfterFewdays() error" + ex.ToString());
        //    }
        //}

        public async Task SP_parameters_masterDelAfterFewDays()
        {
            try
            {
                using (MsqlConnection conn = new MsqlConnection())
                {
                    conn.open();
                    SqlCommand cmd = new SqlCommand("parameters_masterDelAfterFewDays", conn.msqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.close();
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_parameters_masterDelAfterFewDays() error" + ex.ToString());
            }
        }

        #endregion 

        #region 10 minutes once calling store procedure

        public async Task SP_DashboardLiveHMIMWOSTblINUP()
        {
            try
            {
                using (MsqlConnection conn = new MsqlConnection())
                {
                    conn.open();
                    SqlCommand cmd = new SqlCommand("DashboardLiveHMIMWOSTblINUP", conn.msqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.close();
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DashboardLiveHMIMWOSTblINUP() error" + ex.ToString());
            }
        }

        public async Task SP_DashboardLiveLossManualTblINUP()
        {
            try
            {
                using (MsqlConnection conn = new MsqlConnection())
                {
                    conn.open();
                    SqlCommand cmd = new SqlCommand("DashboardLiveLossManualTblINUP", conn.msqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.close();
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DashboardLiveLossManualTblINUP() error" + ex.ToString());
            }
        }

        public async Task SP_DashboardLiveModeTblINUPP()
        {
            try
            {
                using (MsqlConnection conn = new MsqlConnection())
                {
                    conn.open();
                    SqlCommand cmd = new SqlCommand("DashboardLiveModeTblINUP", conn.msqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.close();
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DashboardLiveModeTblINUPP() error" + ex.ToString());
            }
        }

        #region Commented because of in SP no functionality is done
        //public async Task SP_DeleteExtraModeRows()
        //{
        //    try
        //    {
        //        using (MsqlConnection conn = new MsqlConnection())
        //        {
        //            conn.open();
        //            SqlCommand cmd = new SqlCommand("DeleteExtraModeRows", conn.msqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //            conn.close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile("SP_DeleteExtraModeRows() error" + ex.ToString());
        //    }
        //}
        #endregion

        #region Delete functionality for clearing issues
        public async Task SP_DeleteExtraRowsHMI()
        {
            IntoFile("Calling DeleteExtraRowsHMI SP :");
            try
            {
                
                using (MsqlConnection conn = new MsqlConnection())
                {
                    conn.open();
                    SqlCommand cmd = new SqlCommand("DeleteExtraRowsHMI", conn.msqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.close();
                    IntoFile("Deleted successfully");
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DeleteExtraRowsHMI() error" + ex.ToString());
            }
        }

        //public async Task SP_DeleteExtraDupWorkorderFromddl()
        //{
        //    IntoFile("SP_DeleteExtraDupWorkorderFromddl");
        //    try
        //    {
        //        using (MsqlConnection conn = new MsqlConnection())
        //        {
        //            conn.open();
        //            SqlCommand cmd = new SqlCommand("DeleteDupWorkOrdersFromDDLList", conn.msqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //            conn.close();
        //            IntoFile("Deleted Successfully");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IntoFile("SP_DeleteExtraDupWorkorderFromddl() error" + ex.ToString());
        //    }
        //}

        public async Task SP_DeleteReocrdsFromDDL()
        {
            IntoFile("SP_DeleteReocrdsFromDDL");
            try
            {
                using (MsqlConnection conn = new MsqlConnection())
                {
                    conn.open();
                    SqlCommand cmd = new SqlCommand("DeleteReocrdsFromDDL", conn.msqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conn.close();
                    IntoFile("Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DeleteExtraDupWorkorderFromddl() error" + ex.ToString());
            }
        }

        public async Task SP_DeleteNegativeLossDur()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MsqlConnection mc = new MsqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("GetRows", mc.msqlConnection))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            mc.open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@loss", "loss");
                            sda.Fill(dt);
                            mc.close();
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int lossId = Convert.ToInt32(dt.Rows[i][0]);
                    using (MsqlConnection conn = new MsqlConnection())
                    {
                        conn.open();
                        SqlCommand cmd = new SqlCommand("DeleteNegativeLossDur", conn.msqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@V_LossID", lossId);
                        cmd.ExecuteNonQuery();
                        conn.close();
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DeleteNegativeLossDur() error" + ex.ToString());
            }
        }

        public async Task SP_DeleteNegativeModeDuration()
        {
            var modeIds = new List<tbllivemode>();
            try
            {
                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                    DateTime correctedDate = DateTime.Now;
                    // var modeIds = db.tbllivemodedbs.Where(m => m.IsCompleted == 1 && m.DurationInSec <= 0 && m.CorrectedDate == correctedDate).Select(m => m.ModeID).ToList();
                    modeIds = db.tbllivemodes.Where(m => m.IsCompleted == 1 && m.DurationInSec <= 0 && m.CorrectedDate == correctedDate).ToList();

                    foreach (var modeId in modeIds)
                    {
                        using (MsqlConnection conn = new MsqlConnection())
                        {
                            conn.open();
                            SqlCommand cmd = new SqlCommand("DeleteNegativeModeDuration", conn.msqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@V_ModeID", modeId);
                            cmd.ExecuteNonQuery();
                            conn.close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_DeleteNegativeModeDuration() error" + ex.ToString());
            }
        }
        #endregion

        public async Task SP_HMIUpdationofMonthYearWeek()
        {
            var values = new List<tblhmiscreen>();
            try
            {
                using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                {
                    // var values = db.tblhmiscreens.Where(m => m.HMIMonth == null && m.HMIYear == null && m.HMIWeekNumber == null && m.HMIQuarter == null).Select(m => new { m.CorrectedDate, m.HMIID }).ToList();
                    values = db.tblhmiscreens.Where(m => m.HMIMonth == null && m.HMIYear == null && m.HMIWeekNumber == null && m.HMIQuarter == null).ToList();

                    foreach (var item in values)
                    {
                        using (MsqlConnection conn = new MsqlConnection())
                        {
                            conn.open();
                            SqlCommand cmd = new SqlCommand("HMIUpdationofMonthYearWeek", conn.msqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@V_Date", item.CorrectedDate);
                            cmd.Parameters.AddWithValue("@V_HMIID", item.HMIID);
                            cmd.ExecuteNonQuery();
                            conn.close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IntoFile("SP_HMIUpdationofMonthYearWeek() error" + ex.ToString());
            }
        }

        #endregion

        public void IntoFile(string Msg)
        {
            try
            {
                string path1 = AppDomain.CurrentDomain.BaseDirectory;
                string appPath = Application.StartupPath + @"\SPLogfile.txt";
                using (StreamWriter writer = new StreamWriter(appPath, true)) //true => Append Text
                {
                    writer.WriteLine(System.DateTime.Now + ":  " + Msg);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("IntoFile Error " + e.ToString());
            }

        }

     
    }
}
