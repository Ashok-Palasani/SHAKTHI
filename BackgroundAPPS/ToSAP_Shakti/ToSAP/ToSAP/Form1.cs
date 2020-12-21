using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace ToSAP
{
    public partial class Form1 : Form
    {
        // changes for Testing Local
        // 1 InitializeComponent
        // 2 Path

        i_facility_shaktiEntities db = new i_facility_shaktiEntities();
        //[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        //private void workToDo()
        //{
        //    IntPtr tokenHandle = new IntPtr(0);
        //    tokenHandle = IntPtr.Zero;

        //    //bool returnValue = LogonUser(<userName>, <domain>, <password>, 2, 0, ref tokenHandle);  
        //    bool returnValue = LogonUser("unitworks", "taslaero", "Unit@works", 2, 0, ref tokenHandle);
        //    MessageBox.Show(""+tokenHandle);
        //    WindowsIdentity ImpersonatedIdentity = new WindowsIdentity(tokenHandle);
        //    WindowsImpersonationContext MyImpersonation = ImpersonatedIdentity.Impersonate();

        //    // MessageBox.Show("success");
        //    //Do whatever you want to do as this identity - directory.exists, directory.createdirectory, etc.  
        //    GetDetailsFromDB();

        //    MessageBox.Show("About to call Undo of Impersonation");
        //    MyImpersonation.Undo();
        //}

        public Form1()
        {
            try
            {
                string username = null, password = null, domainName1 = null;
                TATAMysqlConnection mcpPath = new TATAMysqlConnection();
                mcpPath.open();
                String queryPath = "SELECT * FROM i_facility_shakti.dbo.tblnetworkdetailsforddl where IsDeleted = 0";
                //MessageBox.Show("" + queryPath);
                SqlDataAdapter daPath = new SqlDataAdapter(queryPath, mcpPath.msqlConnection);
                DataTable dtPath = new DataTable();
                daPath.Fill(dtPath);
                mcpPath.close();


                for (int p = 0; p < dtPath.Rows.Count; p++)
                {
                    username = Convert.ToString(dtPath.Rows[0][2]);
                    password = Convert.ToString(dtPath.Rows[0][3]);
                    domainName1 = Convert.ToString(dtPath.Rows[0][4]);
                }

                //For Client
                //InitializeComponent();
                InitializeComponent(username, password, domainName1);

                ////for testing local
                //InitializeComponent();
                //workToDo();
                try
                {
                    //GetLiveWO();
                    GetDataJF();
                    SplitWO();
                }
                catch(Exception e)
                {
                    IntoFile("Get Live Data: " + e.ToString());
                }
                //GetDetailsFromDB();

            }
            catch (Exception e)
            {
                IntoFile(e.ToString());
                //MessageBox.Show(e.ToString());
            }

            Process[] GetMyProc = Process.GetProcessesByName("SAPOutput");
            if (GetMyProc.Count() > 0)
            {
                // 2017-01-20 //GetMyProc[0].Kill();
                try
                {
                    //MessageBox.Show("ablut to close");
                    GetMyProc[0].CloseMainWindow();
                    System.Threading.Thread.Sleep(10000);
                    GetMyProc[0].Kill();
                }
                catch (Exception eclose)
                {
                    //MessageBox.Show("ablut to close");
                    System.Threading.Thread.Sleep(10000);
                    GetMyProc[0].Kill();
                }
            }


        }

        //private void MyTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GenerateToSAPFile();
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.ToString());
        //    }
        //}

        public void GetDetailsFromDB()
        {

            string path = null;

            try
            {
                TATAMysqlConnection mcpPath = new TATAMysqlConnection();
                mcpPath.open();
                String queryPath = "SELECT * FROM i_facility_shakti.dbo.tbltosapfilepath where IsDeleted = 0 and toSAPFilePathID='2'";
                //MessageBox.Show("" + queryPath);
                SqlDataAdapter daPath = new SqlDataAdapter(queryPath, mcpPath.msqlConnection);
                DataTable dtPath = new DataTable();
                daPath.Fill(dtPath);
                mcpPath.close();
                for (int p = 0; p < dtPath.Rows.Count; p++)
                {
                    path = Convert.ToString(dtPath.Rows[0][2]);
                }
            }
            catch (Exception e)
            {
                IntoFile("SAP Path query : " + e);
            }

            //MessageBox.Show(""+path);
            if (string.IsNullOrEmpty(path))
            {
                IntoFile("No Path is provided");
            }
            else
            {
                //2017-01-20 Dynamic Shop && Path
                TATAMysqlConnection mcpShopsList = new TATAMysqlConnection();
                mcpShopsList.open();
                String queryShopsList = "SELECT * FROM i_facility_shakti.dbo.tbltosapshopnames where IsDeleted = 0";
                SqlDataAdapter daShopsList = new SqlDataAdapter(queryShopsList, mcpShopsList.msqlConnection);
                DataTable dtShopsList = new DataTable();
                daShopsList.Fill(dtShopsList);
                mcpShopsList.close();
                // MessageBox.Show("" + queryShopsList);
                for (int s = 0; s < dtShopsList.Rows.Count; s++)
                {
                    // For Testing //for (int j = 10; j > ShopFileNameList.Count; j++)
                    //for (int j = 0; j < ShopFileNameList.Count; j++)
                    //int shopid = ShopFileNameList[s].Key;
                    //string ShopName = ShopFileNameList[s].Value;

                    int isItSheetMetalLike = 0;
                    int.TryParse(Convert.ToString(dtShopsList.Rows[s][8]), out isItSheetMetalLike);
                    int shopid = Convert.ToInt32(dtShopsList.Rows[s][1]);
                    string ShopName = Convert.ToString(dtShopsList.Rows[s][2]);

                    TATAMysqlConnection mcpShops = new TATAMysqlConnection();
                    mcpShops.open();
                    String queryShops = "SELECT MachineID FROM i_facility_shakti.dbo.tblmachinedetails where IsDeleted = 0 and ShopID = " + shopid + " and IsNormalWC = 0";
                    //MessageBox.Show(" query:" + queryShops);
                    SqlDataAdapter daShops = new SqlDataAdapter(queryShops, mcpShops.msqlConnection);
                    DataTable dtShops = new DataTable();
                    daShops.Fill(dtShops);
                    mcpShops.close();
                    string MacIDs = null;
                    //MessageBox.Show("Count " + dtShops.Rows.Count + " query:" + queryShops);
                    for (int i = 0; i < dtShops.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            MacIDs = dtShops.Rows[i][0].ToString();
                        }
                        else
                        {
                            MacIDs += "," + dtShops.Rows[i][0].ToString();
                        }
                    }

                    if (MacIDs != null)
                    {
                        //// MessageBox.Show(path + "\\" + ShopName);
                        DirectoryInfo di = Directory.CreateDirectory(@path + "\\" + ShopName); //To Create Path if not exists
                        string pathSpecific = path;
                        pathSpecific += "\\" + ShopName + "\\" + ShopName + DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                        //MessageBox.Show("Final Path" + pathSpecific);
                        try
                        {
                            GenerateToSAPFile(MacIDs, pathSpecific, isItSheetMetalLike);
                        }
                        catch (Exception e)
                        {
                            IntoFile("Main Catch: " + e);
                        }
                    }

                    //For ManualWC's
                    MacIDs = null;
                    TATAMysqlConnection mcpShops1 = new TATAMysqlConnection();
                    mcpShops1.open();
                    String queryShops1 = "SELECT MachineID FROM i_facility_shakti.dbo.tblmachinedetails where IsDeleted = 0 and ShopID = " + shopid + " and IsNormalWC = 1 and ManualWCID is not null";
                    //MessageBox.Show(" query:" + queryShops);
                    SqlDataAdapter daShops1 = new SqlDataAdapter(queryShops1, mcpShops1.msqlConnection);
                    DataTable dtShops1 = new DataTable();
                    daShops1.Fill(dtShops1);
                    mcpShops1.close();

                    //MessageBox.Show("Count " + dtShops.Rows.Count + " query:" + queryShops);
                    for (int i = 0; i < dtShops1.Rows.Count; i++)
                    {
                        if (MacIDs == null)
                        {
                            MacIDs = dtShops1.Rows[i][0].ToString();
                        }
                        else
                        {
                            MacIDs += "," + dtShops1.Rows[i][0].ToString();
                        }
                    }

                    if (MacIDs != null)
                    {
                        //// MessageBox.Show(path + "\\" + ShopName);
                        DirectoryInfo di = Directory.CreateDirectory(@path + "\\" + ShopName); //To Create Path if not exists
                        string pathSpecific = path;
                        pathSpecific += "\\" + ShopName + "\\" + ShopName + DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                        //MessageBox.Show("Final Path" + pathSpecific);
                        try
                        {
                            GenerateToSAPFile(MacIDs, pathSpecific, 1);//No S or M for All ManualWorkCenters so directly 1
                        }
                        catch (Exception e)
                        {
                            IntoFile("Main Catch: " + e);
                        }
                    }
                }
            }
        }

        public void GenerateToSAPFile(string WCs, string FilePath, int ToSAPLogic = 0)
        {
            StreamWriter sw = null;
            try
            {
                //For Client
                string OutputPath = FilePath;

                ////For Testing Local Path
                //string OutputPath = @"C:\Users\TECH-1\Desktop\Excel Upload TATA Sikorsky\LocalSAPFiles\" + DateTime.Now.ToString("yyyy-MM-dd HH-00-00");

                //For  Client
                string CorrectedDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                string endTimeString = DateTime.Now.ToString("yyyy-MM-dd") + " " + new TimeSpan(04, 30, 00);
                string startTimeString = CorrectedDate + " " + new TimeSpan(04, 30, 00);

                //For  Testing Local
                //string CorrectedDate = "2017-05-0";
                //string startTimeString = "2017-05-05 04:30:00";
                //string endTimeString = "2017-05-06 04:30:00";
                ////WCs = "8";

                DataTable dt = new DataTable();
                using (TATAMysqlConnection mcp = new TATAMysqlConnection())
                {
                    mcp.open();
                    //2017-06-15 //String query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as h where  MachineID IN (" + WCs + ") and h.Time > '" + startTimeString + "' and h.Time <= '" + endTimeString + "' and isWorkInProgress = 1 ;";
                    string query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as h where MachineID IN (" + WCs + ") and h.Time > '" + startTimeString + "' and h.Time <= '" + endTimeString + "' and case when IsMultiWO = 0 then isWorkInProgress = 1 when IsMultiWO = 1 then isWorkInProgress <> 2 end  ;";

                    //String query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as h where  MachineID IN (" + WCs + ") and h.Time >= '" + startTimeString + "' and h.Time <= '" + endTimeString + "' and isWorkInProgress = 1  and CorrectedDate = '" + CorrectedDate + "';";//2017-03-27
                    //String query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as h where  MachineID IN (22) and h.Time >= '" + startTimeString + "' and h.Time <= '" + endTimeString + "' and isWorkInProgress = 1  ;";
                    ////String query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen where MachineID IN ("+ WCs +") and CorrectedDate = '2017-01-06' and isWorkInProgress = 1  order by HMIID desc limit 10;";
                    //String query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as h where CorrectedDate = '2017-01-24' and MachineID = 22  and Work_Order_No like '4000056039%' and OperationNo = 40 and isWorkInProgress = 1 ;";
                    SqlDataAdapter dashift = new SqlDataAdapter(query, mcp.msqlConnection);
                    dashift.Fill(dt);
                    mcp.close();
                }

                //MessageBox.Show("Main Data Rows"+dt.Rows.Count +" Query is " + query);

                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("Confirmation", typeof(string)); //New format added
                dtNew.Columns.Add("HMIID", typeof(string));
                dtNew.Columns.Add("Color", typeof(string));
                dtNew.Columns.Add("StartTime", typeof(string));
                dtNew.Columns.Add("EndTime", typeof(string));
                dtNew.Columns.Add("Reason", typeof(string));
                dtNew.Columns.Add("IsEnd", typeof(int)); //Basically : 0 , PF : 1 , JF : 2.
                dtNew.Columns.Add("WONo", typeof(string));
                dtNew.Columns.Add("OpNo", typeof(string));
                dtNew.Columns.Add("WorkCenter", typeof(string)); //Added for new template
                dtNew.Columns.Add("Plant", typeof(string)); //Added for new template
                dtNew.Columns.Add("Confirmationtype", typeof(int)); //Added for new template
                dtNew.Columns.Add("ClearOpenreservation", typeof(string)); //Added for new template

                DataTable MaindtNew = dtNew.Clone();
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            dtNew.Clear();
                            #region

                            int isMultiWO = 0;
                            int.TryParse(dt.Rows[i][24].ToString(), out isMultiWO); //isMultiWo 24th
                            int MachineID = Convert.ToInt32(dt.Rows[i][1]);

                            int isNormalWC = 0;
                            using (i_facility_shaktiEntities dbMac = new i_facility_shaktiEntities())
                            {
                                var MacData = db.tblmachinedetails.Where(m => m.MachineID == MachineID).FirstOrDefault();
                                if (MacData != null)
                                {
                                    isNormalWC = Convert.ToInt32(MacData.IsNormalWC);
                                }
                            }

                            if (isMultiWO == 0)//Its a Single WO
                            {
                                #region SingleWO

                                string WONo = dt.Rows[i][10].ToString();
                                string OpNo = dt.Rows[i][8].ToString();
                                string PartNo = dt.Rows[i][7].ToString();
                                string submitDateString = (Convert.ToDateTime(dt.Rows[i][4])).ToString("yyyy-MM-dd HH:mm:ss");
                                //Get All Rows Related to this WO && OpNo. // Where SubmitTime <= SubmitTime of Current WO 

                                TATAMysqlConnection mcpInner = new TATAMysqlConnection();
                                mcpInner.open();
                                //String queryInner = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as ths WHERE ths.MachineID = '" + MachineID + "' and  ths.Date <= '" + submitDateString + "' and ths.Work_Order_No = '" + WONo + "' and ths.PartNo = '" + PartNo + "' and ths.OperationNo = '" + OpNo + "' order by Date";
                                String queryInner = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as ths WHERE ths.Date <= '" + submitDateString + "' and ths.Work_Order_No = '" + WONo + "' and ths.PartNo = '" + PartNo + "' and ths.OperationNo = '" + OpNo + "' order by Date";
                                SqlDataAdapter dashiftInner = new SqlDataAdapter(queryInner, mcpInner.msqlConnection);
                                DataTable dtInner = new DataTable();
                                dashiftInner.Fill(dtInner);
                                mcpInner.close();
                                //MessageBox.Show("This Mac Query" + queryInner);
                                for (int j = 0; j < dtInner.Rows.Count; j++)
                                {
                                    try
                                    {
                                        string HMIID = dtInner.Rows[j][0].ToString();
                                        string SubmitDateTimeString = Convert.ToDateTime(dtInner.Rows[j][4]).ToString("yyyy-MM-dd HH:mm:ss");
                                        string DoneWithRowDateTimeString = Convert.ToDateTime(dtInner.Rows[j][5]).ToString("yyyy-MM-dd HH:mm:ss");

                                        //New Logic for shorter Duration WO's
                                        if ((Convert.ToDateTime(dtInner.Rows[j][5]) - Convert.ToDateTime(dtInner.Rows[j][4])).TotalMinutes > 2)
                                        {
                                            if (isNormalWC == 0)
                                            {
                                                #region NormalWC
                                                //To Extract all dpsdata b/w wo start and end time and get Last green
                                                DataTable dtAllJF = new DataTable();

                                                try
                                                {
                                                    using (TATAMysqlConnection mcpAllJF = new TATAMysqlConnection())
                                                    {
                                                        mcpAllJF.open();
                                                        //String queryAll = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' and ColorCode = 'green' order by ID limit 1";
                                                        //String queryAllJF = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and CorrectedDate ='" + Convert.ToDateTime(DoneWithRowDateTimeString).ToString("yyyy-MM-dd") + "'   and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' order by ID";
                                                        String queryAllJF = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "' and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' order by ID";
                                                        SqlDataAdapter daAll = new SqlDataAdapter(queryAllJF, mcpAllJF.msqlConnection);
                                                        daAll.Fill(dtAllJF);
                                                        mcpAllJF.close();
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                }

                                                string LastGreenStartDateTimeString = null, FirstGreenStartDateTimeString = null;
                                                int DPSID = 0;
                                                for (int k = dtAllJF.Rows.Count - 1; k >= 0; k--)
                                                {
                                                    if (dtAllJF.Rows[k][10].ToString() == "green")
                                                    {
                                                        LastGreenStartDateTimeString = Convert.ToDateTime(dtAllJF.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                        DPSID = Convert.ToInt32(dtAllJF.Rows[k][0]);
                                                        break;
                                                    }
                                                }

                                                int isJobFinished = 0;
                                                int.TryParse(Convert.ToString(dtInner.Rows[j][18]), out isJobFinished);

                                                if (LastGreenStartDateTimeString == null)
                                                {
                                                    string endCode = null;
                                                    string BreakdownType = "Others";
                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                    mcpLoss1.open();
                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                    DataTable dtLoss1 = new DataTable();
                                                    daLoss1.Fill(dtLoss1);
                                                    mcpLoss1.close();
                                                    if (dtLoss1.Rows.Count > 0)
                                                    {
                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                    }
                                                    else
                                                    {
                                                        endCode = "01";
                                                    }
                                                    if (isJobFinished == 1) { isJobFinished = 2; } else { isJobFinished = 1; }
                                                    dtNew.Rows.Add(HMIID, "green", Convert.ToDateTime(dtInner.Rows[j][4]), Convert.ToDateTime(dtInner.Rows[j][5]), endCode, isJobFinished, WONo, OpNo);

                                                }
                                                else
                                                {
                                                    //Its a Partial Finish
                                                    if (isJobFinished == 0)
                                                    {
                                                        //To Extract all dpsdata b/w wo start and end time that are different modes for NormalWC
                                                        try
                                                        {
                                                            #region
                                                            string PrvMode = null;
                                                            DataTable dtAll = new DataTable();

                                                            //2017-01-12 Step to Avoid DB Access is being tried.
                                                            using (TATAMysqlConnection mcpAll1 = new TATAMysqlConnection())
                                                            {
                                                                mcpAll1.open();
                                                                String queryAll1 = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime >= '" + SubmitDateTimeString + "' and EndTime <= '" + LastGreenStartDateTimeString + "' order by ID ";
                                                                SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                                                daAll1.Fill(dtAll);
                                                                mcpAll1.close();
                                                            }

                                                            //No Time to Test This 
                                                            //DataTable dtAll = dtAllJF.Clone();
                                                            //foreach (DataRow row in dtAllJF.Rows)
                                                            //{
                                                            //    int blah = Convert.ToInt32(row.Field<int>("ID"));
                                                            //    if (blah < DPSID)
                                                            //    {
                                                            //        dtAll.ImportRow(row);
                                                            //    }
                                                            //    else
                                                            //    {
                                                            //    }
                                                            //}

                                                            //2017-03-21
                                                            // If there is only blue for entire WO lifespan then just insert 1 row for that WO.

                                                            DataTable dtColorEndRowChecker = dtAll.Copy();

                                                            for (int k = 0; k < dtAll.Rows.Count; k++)
                                                            {
                                                                if (dtAll.Rows[k][10].ToString() != PrvMode)
                                                                {
                                                                    string startDateTimeMinute = Convert.ToDateTime(dtAll.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                    string endDateTimeMinute = LastGreenStartDateTimeString;
                                                                    //ignore Non-Setup yellow
                                                                    if (dtAll.Rows[k][10].ToString() == "yellow")
                                                                    {
                                                                        PrvMode = "yellow";

                                                                        if (ToSAPLogic == 0)
                                                                        {
                                                                            //Get EndDateTime of current Yellow. else use WO endTime
                                                                            //taking the startDate of next color
                                                                            DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                            if (dr != null)
                                                                            {
                                                                                endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                            }

                                                                            //int losscode = 0;
                                                                            DataTable dtLoss = new DataTable();
                                                                            using (TATAMysqlConnection mcpLoss = new TATAMysqlConnection())
                                                                            {
                                                                                mcpLoss.open();
                                                                                //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime >= '" + endDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime > '" + startDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                String queryLoss = "SELECT * From tbllivelossofentry WHERE MachineID = '" + MachineID + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) and DoneWithRow = 1  and "
                                                                                                + "( StartDateTime <= '" + startDateTimeMinute + "' and ( ( EndDateTime > '" + startDateTimeMinute + "' and ( EndDateTime < '" + endDateTimeMinute + "' or   EndDateTime > '" + endDateTimeMinute + "' )  ) ) or "
                                                                                                + " (  StartDateTime > '" + startDateTimeMinute + "' and ( StartDateTime < '" + endDateTimeMinute + "' ) )) "
                                                                                                 + " or ( StartDateTime = '" + startDateTimeMinute + "' and  EndDateTime = '" + endDateTimeMinute + "') order by LossID limit 2 ";

                                                                                SqlDataAdapter daLoss = new SqlDataAdapter(queryLoss, mcpLoss.msqlConnection);
                                                                                daLoss.Fill(dtLoss);
                                                                                mcpLoss.close();
                                                                            }

                                                                            if (dtLoss.Rows.Count > 0)
                                                                            {
                                                                                //Get the End Time & reason.
                                                                                string endDateTimeMinute1 = null;
                                                                                string endColor = null;

                                                                                if (dtLoss.Rows.Count > 1)
                                                                                {
                                                                                    endColor = "yellow";
                                                                                    endDateTimeMinute1 = Convert.ToDateTime(dtLoss.Rows[0][3]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                }
                                                                                else
                                                                                {
                                                                                    DataRow dr1 = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + dtLoss.Rows[0][2].ToString() + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                    if (dr1 != null)
                                                                                    {
                                                                                        endColor = Convert.ToString(dr1[10]);//Color DPS
                                                                                        endDateTimeMinute1 = Convert.ToDateTime(dr1[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                    }
                                                                                }
                                                                                endDateTimeMinute = endDateTimeMinute1;

                                                                                #region Get endReason
                                                                                string endCode = null;
                                                                                if (endColor == "yellow")
                                                                                {
                                                                                    //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute1).ToString("yyyy-MM-dd HH:mm:00") + "'  and StartDateTime <= '" + Convert.ToDateTime(endDateTimeMinute1).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    string LossCodeID = null;
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    // Now based on LossCodeID get EndCode

                                                                                    TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                    mcpLoss2.open();
                                                                                    //String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                    String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                    SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                    DataTable dtLoss2 = new DataTable();
                                                                                    daLoss2.Fill(dtLoss2);
                                                                                    mcpLoss2.close();
                                                                                    if (dtLoss2.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][13].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        string Type = "Others";
                                                                                        TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                        mcpLoss3.open();
                                                                                        String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                        DataTable dtLoss3 = new DataTable();
                                                                                        daLoss3.Fill(dtLoss3);
                                                                                        mcpLoss3.close();
                                                                                        if (dtLoss3.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss3.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "99";
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else if (endColor == "green")
                                                                                {
                                                                                    //string Type = "Green";
                                                                                    string Type = "Others";
                                                                                    TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                    mcpLoss3.open();
                                                                                    String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                    DataTable dtLoss3 = new DataTable();
                                                                                    daLoss3.Fill(dtLoss3);
                                                                                    mcpLoss3.close();
                                                                                    if (dtLoss3.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss3.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "98";
                                                                                    }
                                                                                }
                                                                                else if (endColor == "red")//It should be red 
                                                                                {
                                                                                    string BreakdownType = "BREAKDOWN";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "97";
                                                                                    }

                                                                                }
                                                                                else //Blue or Unknown
                                                                                {
                                                                                    string BreakdownType = "Others";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "99";
                                                                                    }

                                                                                }
                                                                                #endregion

                                                                                if (endDateTimeMinute == null)
                                                                                {
                                                                                    endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                }

                                                                                //dtNew.Rows.Add(HMIID, "yellow", dtLoss.Rows[0][2].ToString(), dtLoss.Rows[0][3].ToString(), endCode, 0, WONo, OpNo);
                                                                                dtNew.Rows.Add(HMIID, "yellow", startDateTimeMinute, endDateTimeMinute, endCode, 0, WONo, OpNo);

                                                                            }
                                                                        }
                                                                        else if (ToSAPLogic == 1)
                                                                        {

                                                                        }
                                                                    }
                                                                    else if (dtAll.Rows[k][10].ToString() == "green") //For Non-Yellow
                                                                    {
                                                                        #region BasicLogic
                                                                        ////Code for Green : 98
                                                                        //if (k == 0)
                                                                        //{
                                                                        //    string Color = dtAll.Rows[k][10].ToString();
                                                                        //    string StartTime = dtAll.Rows[k][2].ToString();
                                                                        //    //string EndTime = dtAll.Rows[k][10].ToString();
                                                                        //    //string Reason = dtAll.Rows[k][10].ToString();
                                                                        //    dtNew.Rows[k][0] = Color;
                                                                        //    dtNew.Rows[k][1] = StartTime;
                                                                        //    //dtNew.Rows[k][2] = EndTime;
                                                                        //    //dtNew.Rows[k][3] = Reason;
                                                                        //}
                                                                        //else
                                                                        //{
                                                                        ////End Previous Color
                                                                        //dtNew.Rows[k - 1][2] = dtAll.Rows[k][2].ToString();
                                                                        //dtNew.Rows[k - 1][3] = 66;
                                                                        #endregion

                                                                        PrvMode = "green";

                                                                        //Planned to End Current Color
                                                                        //Insert New Color
                                                                        //Get the End Time & reason.
                                                                        endDateTimeMinute = null;
                                                                        string endColor = null;
                                                                        DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'green'").FirstOrDefault();
                                                                        if (dr != null)
                                                                        {
                                                                            endColor = Convert.ToString(dr[10]);//Color DPS
                                                                            endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                        }

                                                                        #region Get endReason
                                                                        string endCode = null;
                                                                        if (endColor == "yellow")
                                                                        {
                                                                            //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                            mcpLoss1.open();
                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                            DataTable dtLoss1 = new DataTable();
                                                                            daLoss1.Fill(dtLoss1);
                                                                            mcpLoss1.close();
                                                                            string LossCodeID = null;
                                                                            if (dtLoss1.Rows.Count > 0)
                                                                            {
                                                                                LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                            }
                                                                            // Now based on LossCodeID get EndCode

                                                                            TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                            mcpLoss2.open();
                                                                            //String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeID = '" + LossCodeID + "' limit 1 "; //2017-01-10
                                                                            String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                            SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                            DataTable dtLoss2 = new DataTable();
                                                                            daLoss2.Fill(dtLoss2);
                                                                            mcpLoss2.close();
                                                                            if (dtLoss2.Rows.Count > 0)
                                                                            {
                                                                                endCode = dtLoss1.Rows[0][13].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                string Type = "Others";
                                                                                TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                mcpLoss3.open();
                                                                                String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                DataTable dtLoss3 = new DataTable();
                                                                                daLoss3.Fill(dtLoss3);
                                                                                mcpLoss3.close();
                                                                                if (dtLoss3.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss3.Rows[0][1].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    endCode = "01";
                                                                                }
                                                                            }
                                                                        }
                                                                        else if (endColor == "red")//It should be red 
                                                                        {
                                                                            string BreakdownType = "BREAKDOWN";
                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                            mcpLoss1.open();
                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                            DataTable dtLoss1 = new DataTable();
                                                                            daLoss1.Fill(dtLoss1);
                                                                            mcpLoss1.close();
                                                                            if (dtLoss1.Rows.Count > 0)
                                                                            {
                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                endCode = "01";
                                                                            }
                                                                        }
                                                                        else //Blue 
                                                                        {
                                                                            string BreakdownType = "Others";
                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                            mcpLoss1.open();
                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                            DataTable dtLoss1 = new DataTable();
                                                                            daLoss1.Fill(dtLoss1);
                                                                            mcpLoss1.close();
                                                                            if (dtLoss1.Rows.Count > 0)
                                                                            {
                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                endCode = "01";
                                                                            }
                                                                        }
                                                                        #endregion

                                                                        if (endDateTimeMinute == null)
                                                                        {
                                                                            endDateTimeMinute = LastGreenStartDateTimeString;
                                                                        }

                                                                        dtNew.Rows.Add(HMIID, dtAll.Rows[k][10].ToString(), startDateTimeMinute, endDateTimeMinute, endCode, 0, WONo, OpNo);
                                                                        //}
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            //if no green between WOStart and End then set IsEnd = 1 for the last row in dtNew
                                                            if (dtAll.Rows.Count > 0)
                                                            {
                                                                dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {

                                                        }
                                                    }
                                                    else //Its JF
                                                    {
                                                        //To Extract all dpsdata b/w wo start and end time that are different modes
                                                        try
                                                        {
                                                            #region
                                                            string PrvMode = null;
                                                            DataTable dtAll = new DataTable();
                                                            using (TATAMysqlConnection mcpAll1 = new TATAMysqlConnection())
                                                            {
                                                                mcpAll1.open();
                                                                String queryAll1 = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime > '" + SubmitDateTimeString + "' and EndTime <= '" + LastGreenStartDateTimeString + "' order by ID ";
                                                                SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                                                daAll1.Fill(dtAll);
                                                                mcpAll1.close();
                                                            }
                                                            DataTable dtColorEndRowChecker = dtAll.Copy();
                                                            for (int k = 0; k < dtAll.Rows.Count; k++)
                                                            {
                                                                if (dtAll.Rows[k][10].ToString() != PrvMode)
                                                                {
                                                                    string startDateTimeMinute = Convert.ToDateTime(dtAll.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                    string endDateTimeMinute = LastGreenStartDateTimeString;
                                                                    //ignore Non-Setup yellow
                                                                    if (dtAll.Rows[k][10].ToString() == "yellow")
                                                                    {
                                                                        PrvMode = "yellow";

                                                                        if (ToSAPLogic == 0)
                                                                        {
                                                                            //Get EndDateTime of current Yellow. else use WO endTime
                                                                            //taking the startDate of next color
                                                                            DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                            if (dr != null)
                                                                            {
                                                                                endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                            }

                                                                            //int losscode = 0;
                                                                            TATAMysqlConnection mcpLoss = new TATAMysqlConnection();
                                                                            mcpLoss.open();
                                                                            //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime >= '" + endDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                            //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime > '" + startDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                            String queryLoss = "SELECT * From tbllivelossofentry WHERE MachineID = '" + MachineID + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) and DoneWithRow = 1  and "
                                                                                            + "( StartDateTime <= '" + startDateTimeMinute + "' and ( ( EndDateTime > '" + startDateTimeMinute + "' and ( EndDateTime < '" + endDateTimeMinute + "' or   EndDateTime > '" + endDateTimeMinute + "' )  ) ) or "
                                                                                            + " (  StartDateTime > '" + startDateTimeMinute + "' and ( StartDateTime < '" + endDateTimeMinute + "' ) )) "
                                                                                            + " or ( StartDateTime = '" + startDateTimeMinute + "' and  EndDateTime = '" + endDateTimeMinute + "') order by LossID limit 2 ";

                                                                            SqlDataAdapter daLoss = new SqlDataAdapter(queryLoss, mcpLoss.msqlConnection);
                                                                            DataTable dtLoss = new DataTable();
                                                                            daLoss.Fill(dtLoss);
                                                                            mcpLoss.close();
                                                                            if (dtLoss.Rows.Count > 0)
                                                                            {
                                                                                //Get the End Time & reason.
                                                                                string endDateTimeMinute1 = null;
                                                                                string endColor = null;

                                                                                if (dtLoss.Rows.Count > 1)
                                                                                {
                                                                                    endColor = "yellow";
                                                                                    endDateTimeMinute1 = Convert.ToDateTime(dtLoss.Rows[0][3]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                }
                                                                                else
                                                                                {
                                                                                    DataRow dr1 = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + dtLoss.Rows[0][2].ToString() + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                    if (dr1 != null)
                                                                                    {
                                                                                        endColor = Convert.ToString(dr1[10]);//Color DPS
                                                                                        endDateTimeMinute1 = Convert.ToDateTime(dr1[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                    }
                                                                                }
                                                                                endDateTimeMinute = endDateTimeMinute1;

                                                                                #region Get endReason
                                                                                string endCode = null;
                                                                                if (endColor == "yellow")
                                                                                {
                                                                                    //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute1).ToString("yyyy-MM-dd HH:mm:00") + "'  and StartDateTime <= '" + Convert.ToDateTime(endDateTimeMinute1).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    string LossCodeID = null;
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    // Now based on LossCodeID get EndCode

                                                                                    TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                    mcpLoss2.open();
                                                                                    String queryLoss2 = "SELECT EndCode FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                    SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                    DataTable dtLoss2 = new DataTable();
                                                                                    daLoss2.Fill(dtLoss2);
                                                                                    mcpLoss2.close();
                                                                                    if (dtLoss2.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss2.Rows[0][0].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        string Type = "Others";
                                                                                        TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                        mcpLoss3.open();
                                                                                        String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                        DataTable dtLoss3 = new DataTable();
                                                                                        daLoss3.Fill(dtLoss3);
                                                                                        mcpLoss3.close();
                                                                                        if (dtLoss3.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss3.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "99";
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else if (endColor == "green")
                                                                                {
                                                                                    //string Type = "Green";
                                                                                    string Type = "Others";
                                                                                    TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                    mcpLoss3.open();
                                                                                    String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                    DataTable dtLoss3 = new DataTable();
                                                                                    daLoss3.Fill(dtLoss3);
                                                                                    mcpLoss3.close();
                                                                                    if (dtLoss3.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss3.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "98";
                                                                                    }
                                                                                }
                                                                                else if (endColor == "red")//It should be red 
                                                                                {
                                                                                    string BreakdownType = "BREAKDOWN";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "97";
                                                                                    }

                                                                                }
                                                                                else //Blue or Unknown
                                                                                {
                                                                                    string BreakdownType = "Others";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "99";
                                                                                    }
                                                                                }
                                                                                #endregion

                                                                                if (endDateTimeMinute == null)
                                                                                {
                                                                                    endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                }

                                                                                dtNew.Rows.Add(HMIID, "yellow", startDateTimeMinute.ToString(), endDateTimeMinute.ToString(), endCode, 0, WONo, OpNo);
                                                                            }
                                                                        }
                                                                        else if (ToSAPLogic == 1)
                                                                        {

                                                                        }
                                                                    }
                                                                    else if (dtAll.Rows[k][10].ToString() == "green") //For Non-Yellow
                                                                    {
                                                                        #region BasicLogic
                                                                        ////Code for Green : 98
                                                                        //if (k == 0)
                                                                        //{
                                                                        //    string Color = dtAll.Rows[k][10].ToString();
                                                                        //    string StartTime = dtAll.Rows[k][2].ToString();
                                                                        //    //string EndTime = dtAll.Rows[k][10].ToString();
                                                                        //    //string Reason = dtAll.Rows[k][10].ToString();
                                                                        //    dtNew.Rows[k][0] = Color;
                                                                        //    dtNew.Rows[k][1] = StartTime;
                                                                        //    //dtNew.Rows[k][2] = EndTime;
                                                                        //    //dtNew.Rows[k][3] = Reason;
                                                                        //}
                                                                        //else
                                                                        //{
                                                                        ////End Previous Color
                                                                        //dtNew.Rows[k - 1][2] = dtAll.Rows[k][2].ToString();
                                                                        //dtNew.Rows[k - 1][3] = 66;
                                                                        #endregion

                                                                        PrvMode = "green";

                                                                        //Planned to End Current Color
                                                                        //Insert New Color
                                                                        //Get the End Time & reason.
                                                                        endDateTimeMinute = null;
                                                                        string endColor = null;
                                                                        DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'green'").FirstOrDefault();
                                                                        if (dr != null)
                                                                        {
                                                                            endColor = Convert.ToString(dr[10]);//Color DPS
                                                                            endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                        }

                                                                        #region Get endReason
                                                                        string endCode = null;
                                                                        if (endColor == "yellow")
                                                                        {
                                                                            //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                            mcpLoss1.open();
                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "' and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                            DataTable dtLoss1 = new DataTable();
                                                                            daLoss1.Fill(dtLoss1);
                                                                            mcpLoss1.close();
                                                                            string LossCodeID = null;
                                                                            if (dtLoss1.Rows.Count > 0)
                                                                            {
                                                                                LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                            }
                                                                            // Now based on LossCodeID get EndCode

                                                                            TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                            mcpLoss2.open();
                                                                            String queryLoss2 = "SELECT EndCode FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                            SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                            DataTable dtLoss2 = new DataTable();
                                                                            daLoss2.Fill(dtLoss2);
                                                                            mcpLoss2.close();
                                                                            if (dtLoss2.Rows[0][0].ToString() != null)
                                                                            {
                                                                                endCode = dtLoss2.Rows[0][0].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                string Type = "Others";
                                                                                TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                mcpLoss3.open();
                                                                                String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' ";
                                                                                SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                DataTable dtLoss3 = new DataTable();
                                                                                daLoss3.Fill(dtLoss3);
                                                                                mcpLoss3.close();
                                                                                if (dtLoss3.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss3.Rows[0][1].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    endCode = "01";
                                                                                }
                                                                            }
                                                                        }
                                                                        else if (endColor == "red")//It should be red 
                                                                        {
                                                                            string BreakdownType = "BREAKDOWN";
                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                            mcpLoss1.open();
                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' ";
                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                            DataTable dtLoss1 = new DataTable();
                                                                            daLoss1.Fill(dtLoss1);
                                                                            mcpLoss1.close();
                                                                            if (dtLoss1.Rows.Count > 0)
                                                                            {
                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                endCode = "01";
                                                                            }
                                                                        }
                                                                        else //Blue 
                                                                        {
                                                                            string Type = "OTHERS";
                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                            mcpLoss1.open();
                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type + "' ";
                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                            DataTable dtLoss1 = new DataTable();
                                                                            daLoss1.Fill(dtLoss1);
                                                                            mcpLoss1.close();
                                                                            if (dtLoss1.Rows.Count > 0)
                                                                            {
                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                endCode = "01";
                                                                            }
                                                                        }
                                                                        #endregion

                                                                        if (endDateTimeMinute == null)
                                                                        {
                                                                            endDateTimeMinute = LastGreenStartDateTimeString;
                                                                        }

                                                                        dtNew.Rows.Add(HMIID, dtAll.Rows[k][10].ToString(), startDateTimeMinute.ToString(), endDateTimeMinute, endCode, 0, WONo, OpNo);
                                                                        //}
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            if (dtAll.Rows.Count > 0)
                                                            {
                                                                dtNew.Rows[dtNew.Rows.Count - 1][5] = 2;
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                        }
                                                    }
                                                }

                                                #endregion
                                            }
                                            else
                                            {
                                                //Logic for MWC 2017-02-28  //They are all Single WO's and Here PF

                                                #region
                                                int isJobFinished = 0;
                                                int.TryParse(Convert.ToString(dtInner.Rows[j][18]), out isJobFinished);

                                                //Its a Partial Finish
                                                if (isJobFinished == 0)
                                                {
                                                    #region
                                                    try
                                                    {
                                                        dtNew.Rows.Add(HMIID, "green", SubmitDateTimeString, DoneWithRowDateTimeString, 0, 0, WONo, OpNo);
                                                        //1st row for any WO Start will be green with default values for EndTime Reason,isEnd

                                                        DateTime StartDateTimeTemp = Convert.ToDateTime(dtInner.Rows[j][4]);
                                                        DateTime EndDateTimeFinal = Convert.ToDateTime(dtInner.Rows[j][5]);
                                                        string PrvColor = "green";

                                                        //Loop till StartDateTimeTemp >= DoneWithRowDateTimeString(WorkOrder EndTime)
                                                        while (StartDateTimeTemp < Convert.ToDateTime(dtInner.Rows[j][5]))
                                                        {
                                                            //Find MIN StartTime(IDLE/Hold) within WOStart and WOEndTime Where StartTime(IDLE/Hold) > (StartDateTimeTemp - 1Sec)
                                                            DateTime Min_StartTime = StartDateTimeTemp;
                                                            DateTime IDLEStartTime = StartDateTimeTemp;
                                                            DateTime HoldStartTime = StartDateTimeTemp;
                                                            int isHoldFirst = 2;

                                                            //var IDLEData = db.tbllivelossofentries.Where(m => m.MachineID == MachineID && m.CorrectedDate == CorrectedDate && m.StartDateTime > StartDateTimeTemp).OrderBy(m => m.StartDateTime).FirstOrDefault();
                                                            //var HoldData = db.tblmanuallossofentries.Where(m => m.MachineID == MachineID && m.CorrectedDate == CorrectedDate && m.StartDateTime > StartDateTimeTemp).OrderBy(m => m.StartDateTime).FirstOrDefault();

                                                            var IDLEData = db.tbllivelossofentries.Where(m => m.MachineID == MachineID && m.StartDateTime > StartDateTimeTemp && m.EndDateTime < EndDateTimeFinal).OrderBy(m => m.StartDateTime).FirstOrDefault();
                                                            var HoldData = db.tblmanuallossofentries.Where(m => m.MachineID == MachineID && m.StartDateTime > StartDateTimeTemp && m.EndDateTime < EndDateTimeFinal).OrderBy(m => m.StartDateTime).FirstOrDefault();

                                                            if (IDLEData == null)
                                                            {
                                                                isHoldFirst = 1; //True
                                                            }
                                                            else if (HoldData == null)
                                                            {
                                                                isHoldFirst = 0; //False
                                                            }
                                                            else if (IDLEData != null && HoldData != null)
                                                            {
                                                                HoldStartTime = Convert.ToDateTime(HoldData.StartDateTime);
                                                                IDLEStartTime = Convert.ToDateTime(IDLEData.StartDateTime);

                                                                if (HoldStartTime.Subtract(IDLEStartTime).TotalSeconds > 0)
                                                                {
                                                                    isHoldFirst = 0; //False
                                                                }
                                                                else
                                                                {
                                                                    isHoldFirst = 1; //True
                                                                }
                                                            }

                                                            //No IDLE / Hold rows , so PF it.
                                                            if (IDLEData == null && HoldData == null)
                                                            {
                                                                if (dtNew.Rows.Count > 0)
                                                                {
                                                                    string endCode = null;
                                                                    string EndType = "Others";
                                                                    var EndCodeData = db.tblendcodes.Where(m => m.EndCodeSDesc == EndType).FirstOrDefault();
                                                                    if (EndCodeData != null)
                                                                    {
                                                                        endCode = EndCodeData.EndCode;
                                                                    }
                                                                    else
                                                                    {
                                                                        endCode = "01";
                                                                    }

                                                                    dtNew.Rows[dtNew.Rows.Count - 1][3] = EndDateTimeFinal;
                                                                    dtNew.Rows[dtNew.Rows.Count - 1][4] = endCode;
                                                                    dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                                    //dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                                }
                                                                StartDateTimeTemp = EndDateTimeFinal;
                                                            }
                                                            else //There are IDLE / Hold rows so
                                                            {
                                                                //Get Data from IDLE / Hold
                                                                string endCode = null;
                                                                DateTime EndDateTimeVariable = EndDateTimeFinal;
                                                                DateTime StartDateTimeVariable = EndDateTimeFinal;
                                                                if (isHoldFirst == 1) //yes Hold is 1st
                                                                {
                                                                    int holdCodeID = HoldData.MessageCodeID;
                                                                    StartDateTimeVariable = Convert.ToDateTime(HoldData.StartDateTime);
                                                                    EndDateTimeVariable = Convert.ToDateTime(HoldData.EndDateTime);

                                                                    var EndCodeData = db.tblholdcodes.Where(m => m.HoldCodeID == holdCodeID).FirstOrDefault();
                                                                    if (EndCodeData != null)
                                                                    {
                                                                        if (EndCodeData != null)
                                                                        {
                                                                            endCode = EndCodeData.EndCode.ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            endCode = "01";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        endCode = "01";
                                                                    }
                                                                }
                                                                else if (isHoldFirst == 0) // idle is 1st
                                                                {
                                                                    StartDateTimeVariable = Convert.ToDateTime(IDLEData.StartDateTime);
                                                                    EndDateTimeVariable = Convert.ToDateTime(IDLEData.EndDateTime);

                                                                    int idleCodeID = IDLEData.MessageCodeID;
                                                                    var EndCodeData = db.tbllossescodes.Where(m => m.LossCodeID == idleCodeID).FirstOrDefault();
                                                                    if (EndCodeData != null)
                                                                    {
                                                                        endCode = EndCodeData.EndCode.ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        //endCode = "01";
                                                                        var EndCodeDataOthers1 = db.tblendcodes.Where(m => m.EndCodeSDesc == "OTHERS").FirstOrDefault();
                                                                        if (EndCodeDataOthers1 != null)
                                                                        {
                                                                            endCode = EndCodeDataOthers1.EndCode.ToString();
                                                                        }
                                                                    }
                                                                }
                                                                //1st Update Prv Row 

                                                                dtNew.Rows[dtNew.Rows.Count - 1][3] = StartDateTimeVariable;
                                                                dtNew.Rows[dtNew.Rows.Count - 1][4] = endCode;
                                                                //dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;

                                                                //2nd Insert New Row (its definitely yellow and u know the end time) //its EndReason will be green.
                                                                string EndType = "Others";
                                                                var EndCodeDataOthers = db.tblendcodes.Where(m => m.EndCodeSDesc == EndType).FirstOrDefault();
                                                                if (EndCodeDataOthers != null)
                                                                {
                                                                    endCode = EndCodeDataOthers.EndCode;
                                                                }
                                                                else
                                                                {
                                                                    endCode = "01";
                                                                }
                                                                dtNew.Rows.Add(HMIID, "yellow", StartDateTimeVariable, EndDateTimeVariable, endCode, 0, WONo, OpNo);


                                                                //3rd Insert a Green Row  //with above row's endTime as StartTime and dummy endtime and EndCode.
                                                                //they will be updated in next loop.
                                                                dtNew.Rows.Add(HMIID, "green", EndDateTimeVariable, EndDateTimeVariable, 0, 0, WONo, OpNo);

                                                                StartDateTimeTemp = EndDateTimeVariable;
                                                            }
                                                        }

                                                        //its end of partial Finish. To represent it push 1 into column IsEnd
                                                        dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                    }
                                                    catch (Exception e)
                                                    {

                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region
                                                    try
                                                    {
                                                        dtNew.Rows.Add(HMIID, "green", SubmitDateTimeString, DoneWithRowDateTimeString, 0, 0, WONo, OpNo);
                                                        //1st row for any WO Start will be green with default values for EndTime Reason,isEnd

                                                        DateTime StartDateTimeTemp = Convert.ToDateTime(dtInner.Rows[j][4]);
                                                        DateTime EndDateTimeFinal = Convert.ToDateTime(dtInner.Rows[j][5]);
                                                        string PrvColor = "green";

                                                        //Loop till StartDateTimeTemp >= DoneWithRowDateTimeString(WorkOrder EndTime)
                                                        while (StartDateTimeTemp < Convert.ToDateTime(dtInner.Rows[j][5]))
                                                        {
                                                            //Find MIN StartTime(IDLE/Hold) within WOStart and WOEndTime Where StartTime(IDLE/Hold) > (StartDateTimeTemp - 1Sec)
                                                            DateTime Min_StartTime = StartDateTimeTemp;
                                                            DateTime IDLEStartTime = StartDateTimeTemp;
                                                            DateTime HoldStartTime = StartDateTimeTemp;
                                                            int isHoldFirst = 2;
                                                            var IDLEData = db.tbllivelossofentries.Where(m => m.MachineID == MachineID && m.CorrectedDate == CorrectedDate && m.StartDateTime > StartDateTimeTemp && m.EndDateTime < EndDateTimeFinal).OrderBy(m => m.StartDateTime).FirstOrDefault();
                                                            var HoldData = db.tblmanuallossofentries.Where(m => m.MachineID == MachineID && m.CorrectedDate == CorrectedDate && m.StartDateTime > StartDateTimeTemp && m.EndDateTime < EndDateTimeFinal).OrderBy(m => m.StartDateTime).FirstOrDefault();

                                                            if (IDLEData == null)
                                                            {
                                                                isHoldFirst = 1; //True
                                                            }
                                                            else if (HoldData == null)
                                                            {
                                                                isHoldFirst = 0; //False
                                                            }
                                                            else if (IDLEData != null && HoldData != null)
                                                            {
                                                                HoldStartTime = Convert.ToDateTime(HoldData.StartDateTime);
                                                                IDLEStartTime = Convert.ToDateTime(IDLEData.StartDateTime);

                                                                if (HoldStartTime.Subtract(IDLEStartTime).TotalSeconds > 0)
                                                                {
                                                                    isHoldFirst = 0; //False
                                                                }
                                                                else
                                                                {
                                                                    isHoldFirst = 1; //True
                                                                }
                                                            }

                                                            //No IDLE / Hold rows , so PF it.
                                                            if (IDLEData == null && HoldData == null)
                                                            {
                                                                if (dtNew.Rows.Count > 0)
                                                                {
                                                                    string endCode = null;
                                                                    string EndType = "Others";
                                                                    var EndCodeData = db.tblendcodes.Where(m => m.EndCodeSDesc == EndType).FirstOrDefault();
                                                                    if (EndCodeData != null)
                                                                    {
                                                                        if (EndCodeData.EndCode == null)
                                                                        {
                                                                            endCode = "01";
                                                                        }
                                                                        else
                                                                        {
                                                                            endCode = EndCodeData.EndCode;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        endCode = "01";
                                                                    }

                                                                    dtNew.Rows[dtNew.Rows.Count - 1][3] = EndDateTimeFinal;
                                                                    dtNew.Rows[dtNew.Rows.Count - 1][4] = endCode;
                                                                    dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                                    //dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                                }
                                                                StartDateTimeTemp = EndDateTimeFinal;
                                                            }
                                                            else //There are IDLE / Hold rows so
                                                            {
                                                                //Get Data from IDLE / Hold
                                                                string endCode = null;
                                                                DateTime EndDateTimeVariable = EndDateTimeFinal;
                                                                DateTime StartDateTimeVariable = EndDateTimeFinal;
                                                                if (isHoldFirst == 1) //yes Hold is 1st
                                                                {
                                                                    int holdCodeID = HoldData.MessageCodeID;
                                                                    StartDateTimeVariable = Convert.ToDateTime(HoldData.StartDateTime);
                                                                    EndDateTimeVariable = Convert.ToDateTime(HoldData.EndDateTime);

                                                                    var EndCodeData = db.tblholdcodes.Where(m => m.HoldCodeID == holdCodeID).FirstOrDefault();
                                                                    if (EndCodeData != null)
                                                                    {
                                                                        if (EndCodeData != null)
                                                                        {
                                                                            endCode = EndCodeData.EndCode.ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            endCode = "01";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        endCode = "01";
                                                                    }
                                                                }
                                                                else if (isHoldFirst == 0) // idle is 1st
                                                                {
                                                                    StartDateTimeVariable = Convert.ToDateTime(IDLEData.StartDateTime);
                                                                    EndDateTimeVariable = Convert.ToDateTime(IDLEData.EndDateTime);

                                                                    int idleCodeID = IDLEData.MessageCodeID;
                                                                    var EndCodeData = db.tbllossescodes.Where(m => m.LossCodeID == idleCodeID).FirstOrDefault();
                                                                    if (EndCodeData != null)
                                                                    {
                                                                        endCode = EndCodeData.EndCode.ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        //endCode = "01";
                                                                        var EndCodeDataOthers1 = db.tblendcodes.Where(m => m.EndCodeSDesc == "OTHERS").FirstOrDefault();
                                                                        if (EndCodeDataOthers1 != null)
                                                                        {
                                                                            endCode = EndCodeDataOthers1.EndCode.ToString();
                                                                        }
                                                                    }
                                                                }
                                                                //1st Update Prv Row 

                                                                dtNew.Rows[dtNew.Rows.Count - 1][3] = StartDateTimeVariable;
                                                                dtNew.Rows[dtNew.Rows.Count - 1][4] = endCode;
                                                                //dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;

                                                                ////2nd Insert New Row (its definitely yellow and u know the end time) //its EndReason will be green.
                                                                //string EndType = "Others";
                                                                //var EndCodeDataOthers = db.tblendcodes.Where(m => m.EndCodeSDesc == EndType).FirstOrDefault();
                                                                //if (EndCodeDataOthers != null)
                                                                //{
                                                                //    endCode = EndCodeDataOthers.EndCode;
                                                                //}
                                                                //else
                                                                //{
                                                                //    endCode = "01";
                                                                //}
                                                                //dtNew.Rows.Add(HMIID, "yellow", StartDateTimeVariable, EndDateTimeVariable, endCode, 0, WONo, OpNo);


                                                                //3rd Insert a Green Row  //with above row's endTime as StartTime and dummy endtime and EndCode.
                                                                //they will be updated in next loop.
                                                                dtNew.Rows.Add(HMIID, "green", EndDateTimeVariable, EndDateTimeVariable, 0, 0, WONo, OpNo);

                                                                StartDateTimeTemp = EndDateTimeVariable;
                                                            }
                                                        }

                                                        //its end of partial Finish. To represent it push 1 into column IsEnd
                                                        dtNew.Rows[dtNew.Rows.Count - 1][5] = 2;
                                                    }
                                                    catch (Exception e)
                                                    {

                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                int isJobFinished = 0;
                                                int.TryParse(Convert.ToString(dtInner.Rows[j][18]), out isJobFinished);

                                                string endCode = null;
                                                string BreakdownType = "Others";
                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                mcpLoss1.open();
                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                DataTable dtLoss1 = new DataTable();
                                                daLoss1.Fill(dtLoss1);
                                                mcpLoss1.close();
                                                if (dtLoss1.Rows.Count > 0)
                                                {
                                                    endCode = dtLoss1.Rows[0][1].ToString();
                                                }
                                                else
                                                {
                                                    endCode = "01";
                                                }
                                                if (isJobFinished == 1) { isJobFinished = 2; } else { isJobFinished = 1; }
                                                dtNew.Rows.Add(HMIID, "green", Convert.ToDateTime(dtInner.Rows[j][4]), Convert.ToDateTime(dtInner.Rows[j][5]), endCode, isJobFinished, WONo, OpNo);
                                            }
                                            catch (Exception e)
                                            {

                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                #endregion

                                MaindtNew.Merge(dtNew);
                                // MessageBox.Show("done with 1 wo");
                            }
                            else //Its a Multi WO
                            {
                                #region MultiWoWO

                                int MainHMIID = Convert.ToInt32(dt.Rows[i][0]);
                                TATAMysqlConnection MultiWOListcs = new TATAMysqlConnection();
                                MultiWOListcs.open();
                                String queryMultiWOList = "SELECT * FROM i_facility_shakti.dbo.tbllivemultiwoselection where HMIID = " + MainHMIID + " ;";
                                SqlDataAdapter daMultiWOList = new SqlDataAdapter(queryMultiWOList, MultiWOListcs.msqlConnection);
                                DataTable dtMultiWOList = new DataTable();
                                daMultiWOList.Fill(dtMultiWOList);
                                MultiWOListcs.close();

                                for (int MultiWOLooper = 0; MultiWOLooper < dtMultiWOList.Rows.Count; MultiWOLooper++)
                                {

                                    int isCompleted = Convert.ToInt32(dtMultiWOList.Rows[MultiWOLooper][8]);
                                    if (isCompleted == 1)
                                    {
                                        dtNew.Clear();

                                        string WONo = dtMultiWOList.Rows[MultiWOLooper][1].ToString();
                                        string OpNo = dtMultiWOList.Rows[MultiWOLooper][3].ToString();
                                        string PartNo = dtMultiWOList.Rows[MultiWOLooper][2].ToString();
                                        string submitDateString = Convert.ToString(dt.Rows[i][4]);
                                        //Get All Rows Related to this WO && OpNo. // Where SubmitTime <= SubmitTime of Current WO 

                                        // This WorkOrder row may be , 
                                        // 1) IN HMIScreen( If previously SingleWO )or 
                                        // 2) IN tblMultiWO ( If Previously as MultiWO)

                                        //1)
                                        try
                                        {
                                            #region
                                            TATAMysqlConnection mcpInner = new TATAMysqlConnection();
                                            mcpInner.open();
                                            //String queryInner = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as ths WHERE ths.MachineID = '" + MachineID + "' and  ths.Date <= '" + submitDateString + "' and ths.Work_Order_No = '" + WONo + "' and ths.PartNo = '" + PartNo + "' and ths.OperationNo = '" + OpNo + "' order by Date";
                                            String queryInner = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as ths WHERE ths.Date <= '" + submitDateString + "' and ths.Work_Order_No = '" + WONo + "' and ths.PartNo = '" + PartNo + "' and ths.OperationNo = '" + OpNo + "' order by Date";
                                            SqlDataAdapter dashiftInner = new SqlDataAdapter(queryInner, mcpInner.msqlConnection);
                                            DataTable dtInner = new DataTable();
                                            dashiftInner.Fill(dtInner);
                                            mcpInner.close();
                                            //MessageBox.Show("This Mac Query" + queryInner);
                                            for (int j = 0; j < dtInner.Rows.Count; j++)
                                            {
                                                try
                                                {
                                                    string HMIID = dtInner.Rows[j][0].ToString();
                                                    string SubmitDateTimeString = Convert.ToDateTime(dtInner.Rows[j][4]).ToString("yyyy-MM-dd HH:mm:ss");
                                                    string DoneWithRowDateTimeString = Convert.ToDateTime(dtInner.Rows[j][5]).ToString("yyyy-MM-dd HH:mm:ss");
                                                    MachineID = Convert.ToInt32(dtInner.Rows[j][1]);
                                                    //To Extract all dpsdata b/w wo start and end time and get Last green
                                                    DataTable dtAllJF = new DataTable();
                                                    try
                                                    {
                                                        using (TATAMysqlConnection mcpAllJF = new TATAMysqlConnection())
                                                        {
                                                            mcpAllJF.open();
                                                            //String queryAll = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' and ColorCode = 'green' order by ID limit 1";
                                                            //String queryAllJF = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and CorrectedDate ='" + Convert.ToDateTime(DoneWithRowDateTimeString).ToString("yyyy-MM-dd") + "'   and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' order by ID";
                                                            String queryAllJF = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "' and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' order by ID";
                                                            SqlDataAdapter daAll = new SqlDataAdapter(queryAllJF, mcpAllJF.msqlConnection);
                                                            daAll.Fill(dtAllJF);
                                                            mcpAllJF.close();
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        IntoFile("MultiWO " + e);
                                                        continue;

                                                    }

                                                    //New Logic for shorter Duration WO's
                                                    if ((Convert.ToDateTime(dtInner.Rows[j][5]) - Convert.ToDateTime(dtInner.Rows[j][4])).TotalMinutes > 2)
                                                    {
                                                        #region
                                                        string LastGreenStartDateTimeString = null, FirstGreenStartDateTimeString = null;

                                                        for (int k = dtAllJF.Rows.Count - 1; k >= 0; k--)
                                                        {
                                                            if (dtAllJF.Rows[k][10].ToString() == "green")
                                                            {
                                                                LastGreenStartDateTimeString = Convert.ToDateTime(dtAllJF.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                break;
                                                            }
                                                        }

                                                        ////int MachineID = Convert.ToInt32(dtInner.Rows[i][1]);
                                                        ////Construct WONo OpNo string.
                                                        //string WONoInner = Convert.ToString(dtInner.Rows[j][10]);
                                                        //string OpNoInner = Convert.ToString(dtInner.Rows[j][8]);
                                                        //string SorM = "S";
                                                        //string Spaces13 = @"             ";
                                                        //string WONoOpNoSorM13Spaces = null;
                                                        //OpNoInner = OpNoInner.PadLeft(4, '0');
                                                        //WONoInner = WONoInner.PadLeft(12, '0');

                                                        //string SubmitDateTimeString = Convert.ToDateTime(dtInner.Rows[j][4]).ToString("yyyy-MM-dd HH:mm:ss");
                                                        //string DoneWithRowDateTimeString = Convert.ToDateTime(dtInner.Rows[j][5]).ToString("yyyy-MM-dd HH:mm:ss");

                                                        int isJobFinished = 0;
                                                        int.TryParse(Convert.ToString(dtInner.Rows[j][18]), out isJobFinished);

                                                        if (LastGreenStartDateTimeString == null)
                                                        {
                                                            try
                                                            {
                                                                string endCode = null;
                                                                string BreakdownType = "Others";
                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                mcpLoss1.open();
                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                DataTable dtLoss1 = new DataTable();
                                                                daLoss1.Fill(dtLoss1);
                                                                mcpLoss1.close();
                                                                if (dtLoss1.Rows.Count > 0)
                                                                {
                                                                    endCode = dtLoss1.Rows[0][1].ToString();
                                                                }
                                                                else
                                                                {
                                                                    endCode = "01";
                                                                }
                                                                if (isJobFinished == 1) { isJobFinished = 2; } else { isJobFinished = 1; }
                                                                dtNew.Rows.Add(HMIID, "green", Convert.ToDateTime(dtInner.Rows[j][4]), Convert.ToDateTime(dtInner.Rows[j][5]), endCode, isJobFinished, WONo, OpNo);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                IntoFile("MultiWO " + e);
                                                                continue;

                                                            }
                                                        }
                                                        else
                                                        {

                                                            //Its a Partial Finish
                                                            if (isJobFinished == 0)
                                                            {
                                                                //To Extract all dpsdata b/w wo start and end time that are different modes
                                                                try
                                                                {
                                                                    #region
                                                                    string PrvMode = null;
                                                                    DataTable dtAll = new DataTable();
                                                                    using (TATAMysqlConnection mcpAll1 = new TATAMysqlConnection())
                                                                    {
                                                                        mcpAll1.open();
                                                                        String queryAll1 = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime > '" + SubmitDateTimeString + "' and EndTime <= '" + LastGreenStartDateTimeString + "' order by ID ";
                                                                        SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                                                        daAll1.Fill(dtAll);
                                                                        mcpAll1.close();
                                                                    }
                                                                    DataTable dtColorEndRowChecker = dtAll.Copy();
                                                                    for (int k = 0; k < dtAll.Rows.Count; k++)
                                                                    {
                                                                        if (dtAll.Rows[k][10].ToString() != PrvMode)
                                                                        {
                                                                            string startDateTimeMinute = Convert.ToDateTime(dtAll.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                            string endDateTimeMinute = LastGreenStartDateTimeString;
                                                                            //ignore Non-Setup yellow
                                                                            if (dtAll.Rows[k][10].ToString() == "yellow")
                                                                            {
                                                                                PrvMode = "yellow";

                                                                                if (ToSAPLogic == 0)
                                                                                {
                                                                                    //Get EndDateTime of current Yellow. else use WO endTime
                                                                                    //taking the startDate of next color
                                                                                    DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                    if (dr != null)
                                                                                    {
                                                                                        endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                    }

                                                                                    //int losscode = 0;
                                                                                    TATAMysqlConnection mcpLoss = new TATAMysqlConnection();
                                                                                    mcpLoss.open();
                                                                                    //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime >= '" + endDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                    //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime > '" + startDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                    String queryLoss = "SELECT * From tbllivelossofentry WHERE MachineID = '" + MachineID + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) and DoneWithRow = 1  and "
                                                                                                    + "( StartDateTime <= '" + startDateTimeMinute + "' and ( ( EndDateTime > '" + startDateTimeMinute + "' and ( EndDateTime < '" + endDateTimeMinute + "' or   EndDateTime > '" + endDateTimeMinute + "' )  ) ) or "
                                                                                                    + " (  StartDateTime > '" + startDateTimeMinute + "' and ( StartDateTime < '" + endDateTimeMinute + "' ) )) "
                                                                                                     + " or ( StartDateTime = '" + startDateTimeMinute + "' and  EndDateTime = '" + endDateTimeMinute + "') order by LossID limit 2 ";

                                                                                    SqlDataAdapter daLoss = new SqlDataAdapter(queryLoss, mcpLoss.msqlConnection);
                                                                                    DataTable dtLoss = new DataTable();
                                                                                    daLoss.Fill(dtLoss);
                                                                                    mcpLoss.close();

                                                                                    if (dtLoss.Rows.Count > 0)
                                                                                    {
                                                                                        //Get the End Time & reason.
                                                                                        string endDateTimeMinute1 = null;
                                                                                        string endColor = null;

                                                                                        if (dtLoss.Rows.Count > 1)
                                                                                        {
                                                                                            endColor = "yellow";
                                                                                            endDateTimeMinute1 = Convert.ToDateTime(dtLoss.Rows[0][3]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            DataRow dr1 = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + dtLoss.Rows[0][2].ToString() + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                            if (dr1 != null)
                                                                                            {
                                                                                                endColor = Convert.ToString(dr1[10]);//Color DPS
                                                                                                endDateTimeMinute1 = Convert.ToDateTime(dr1[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                            }
                                                                                        }
                                                                                        endDateTimeMinute = endDateTimeMinute1;

                                                                                        #region Get endReason
                                                                                        string endCode = null;
                                                                                        if (endColor == "yellow")
                                                                                        {
                                                                                            //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                            mcpLoss1.open();
                                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute1).ToString("yyyy-MM-dd HH:mm:00") + "'  and StartDateTime <= '" + Convert.ToDateTime(endDateTimeMinute1).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                            DataTable dtLoss1 = new DataTable();
                                                                                            daLoss1.Fill(dtLoss1);
                                                                                            mcpLoss1.close();
                                                                                            string LossCodeID = null;
                                                                                            if (dtLoss1.Rows.Count > 0)
                                                                                            {
                                                                                                LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                            }
                                                                                            // Now based on LossCodeID get EndCode

                                                                                            TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                            mcpLoss2.open();
                                                                                            //String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                            String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                            SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                            DataTable dtLoss2 = new DataTable();
                                                                                            daLoss2.Fill(dtLoss2);
                                                                                            mcpLoss2.close();
                                                                                            if (dtLoss2.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss1.Rows[0][13].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                string Type = "Others";
                                                                                                TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                                mcpLoss3.open();
                                                                                                String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                                SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                                DataTable dtLoss3 = new DataTable();
                                                                                                daLoss3.Fill(dtLoss3);
                                                                                                mcpLoss3.close();
                                                                                                if (dtLoss3.Rows.Count > 0)
                                                                                                {
                                                                                                    endCode = dtLoss3.Rows[0][1].ToString();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    endCode = "99";
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        else if (endColor == "green")
                                                                                        {
                                                                                            //string Type = "Green";
                                                                                            string Type = "Others";
                                                                                            TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                            mcpLoss3.open();
                                                                                            String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                            DataTable dtLoss3 = new DataTable();
                                                                                            daLoss3.Fill(dtLoss3);
                                                                                            mcpLoss3.close();
                                                                                            if (dtLoss3.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss3.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "98";
                                                                                            }
                                                                                        }
                                                                                        else if (endColor == "red")//It should be red 
                                                                                        {
                                                                                            string BreakdownType = "BREAKDOWN";
                                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                            mcpLoss1.open();
                                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                            DataTable dtLoss1 = new DataTable();
                                                                                            daLoss1.Fill(dtLoss1);
                                                                                            mcpLoss1.close();
                                                                                            if (dtLoss1.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "97";
                                                                                            }

                                                                                        }
                                                                                        else //Blue or Unknown
                                                                                        {
                                                                                            string BreakdownType = "Others";
                                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                            mcpLoss1.open();
                                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                            DataTable dtLoss1 = new DataTable();
                                                                                            daLoss1.Fill(dtLoss1);
                                                                                            mcpLoss1.close();
                                                                                            if (dtLoss1.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "99";
                                                                                            }

                                                                                        }
                                                                                        #endregion

                                                                                        if (endDateTimeMinute == null)
                                                                                        {
                                                                                            endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                        }

                                                                                        dtNew.Rows.Add(HMIID, "yellow", dtLoss.Rows[0][2].ToString(), dtLoss.Rows[0][3].ToString(), endCode, 0, WONo, OpNo);

                                                                                    }
                                                                                }
                                                                                else if (ToSAPLogic == 1)
                                                                                {

                                                                                }

                                                                            }
                                                                            else if (dtAll.Rows[k][10].ToString() == "green") //For Non-Yellow
                                                                            {
                                                                                #region BasicLogic
                                                                                ////Code for Green : 98
                                                                                //if (k == 0)
                                                                                //{
                                                                                //    string Color = dtAll.Rows[k][10].ToString();
                                                                                //    string StartTime = dtAll.Rows[k][2].ToString();
                                                                                //    //string EndTime = dtAll.Rows[k][10].ToString();
                                                                                //    //string Reason = dtAll.Rows[k][10].ToString();
                                                                                //    dtNew.Rows[k][0] = Color;
                                                                                //    dtNew.Rows[k][1] = StartTime;
                                                                                //    //dtNew.Rows[k][2] = EndTime;
                                                                                //    //dtNew.Rows[k][3] = Reason;
                                                                                //}
                                                                                //else
                                                                                //{
                                                                                ////End Previous Color
                                                                                //dtNew.Rows[k - 1][2] = dtAll.Rows[k][2].ToString();
                                                                                //dtNew.Rows[k - 1][3] = 66;
                                                                                #endregion

                                                                                PrvMode = "green";

                                                                                //Planned to End Current Color
                                                                                //Insert New Color
                                                                                //Get the End Time & reason.
                                                                                endDateTimeMinute = null;
                                                                                string endColor = null;
                                                                                DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'green'").FirstOrDefault();
                                                                                if (dr != null)
                                                                                {
                                                                                    endColor = Convert.ToString(dr[10]);//Color DPS
                                                                                    endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                }

                                                                                #region Get endReason
                                                                                string endCode = null;
                                                                                if (endColor == "yellow")
                                                                                {
                                                                                    //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    string LossCodeID = null;
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    // Now based on LossCodeID get EndCode

                                                                                    TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                    mcpLoss2.open();
                                                                                    //String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeID = '" + LossCodeID + "' limit 1 "; //2017-01-10
                                                                                    String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                    SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                    DataTable dtLoss2 = new DataTable();
                                                                                    daLoss2.Fill(dtLoss2);
                                                                                    mcpLoss2.close();
                                                                                    if (dtLoss2.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][13].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        string Type = "Others";
                                                                                        TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                        mcpLoss3.open();
                                                                                        String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                        DataTable dtLoss3 = new DataTable();
                                                                                        daLoss3.Fill(dtLoss3);
                                                                                        mcpLoss3.close();
                                                                                        if (dtLoss3.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss3.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "01";
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else if (endColor == "red")//It should be red 
                                                                                {
                                                                                    string BreakdownType = "BREAKDOWN";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "01";
                                                                                    }
                                                                                }
                                                                                else //Blue 
                                                                                {
                                                                                    string BreakdownType = "Others";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "01";
                                                                                    }
                                                                                }
                                                                                #endregion

                                                                                if (endDateTimeMinute == null)
                                                                                {
                                                                                    endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                }

                                                                                dtNew.Rows.Add(HMIID, dtAll.Rows[k][10].ToString(), dtAll.Rows[k][2].ToString(), endDateTimeMinute, endCode, 0, WONo, OpNo);
                                                                                //}
                                                                            }
                                                                        }
                                                                    }
                                                                    #endregion

                                                                    if (dtAll.Rows.Count > 0)
                                                                    {
                                                                        dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    IntoFile("MultiWO " + e);
                                                                    continue;

                                                                }
                                                            }
                                                            else //Its JF
                                                            {

                                                                //To Extract all dpsdata b/w wo start and end time that are different modes
                                                                try
                                                                {
                                                                    #region
                                                                    string PrvMode = null;
                                                                    DataTable dtAll = new DataTable();
                                                                    using (TATAMysqlConnection mcpAll1 = new TATAMysqlConnection())
                                                                    {
                                                                        mcpAll1.open();
                                                                        String queryAll1 = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime > '" + SubmitDateTimeString + "' and EndTime <= '" + LastGreenStartDateTimeString + "' order by ID ";
                                                                        SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                                                        daAll1.Fill(dtAll);
                                                                        mcpAll1.close();
                                                                    }
                                                                    DataTable dtColorEndRowChecker = dtAll.Copy();
                                                                    for (int k = 0; k < dtAll.Rows.Count; k++)
                                                                    {
                                                                        if (dtAll.Rows[k][10].ToString() != PrvMode)
                                                                        {
                                                                            string startDateTimeMinute = Convert.ToDateTime(dtAll.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                            string endDateTimeMinute = LastGreenStartDateTimeString;
                                                                            //ignore Non-Setup yellow
                                                                            if (dtAll.Rows[k][10].ToString() == "yellow")
                                                                            {
                                                                                PrvMode = "yellow";

                                                                                if (ToSAPLogic == 0)
                                                                                {
                                                                                    //Get EndDateTime of current Yellow. else use WO endTime
                                                                                    //taking the startDate of next color
                                                                                    DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                    if (dr != null)
                                                                                    {
                                                                                        endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                    }

                                                                                    //int losscode = 0;
                                                                                    TATAMysqlConnection mcpLoss = new TATAMysqlConnection();
                                                                                    mcpLoss.open();
                                                                                    //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime >= '" + endDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                    //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime > '" + startDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                    String queryLoss = "SELECT * From tbllivelossofentry WHERE MachineID = '" + MachineID + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) and DoneWithRow = 1  and "
                                                                                                    + "( StartDateTime <= '" + startDateTimeMinute + "' and ( ( EndDateTime > '" + startDateTimeMinute + "' and ( EndDateTime < '" + endDateTimeMinute + "' or   EndDateTime > '" + endDateTimeMinute + "' )  ) ) or "
                                                                                                    + " (  StartDateTime > '" + startDateTimeMinute + "' and ( StartDateTime < '" + endDateTimeMinute + "' ) )) "
                                                                                                    + " or ( StartDateTime = '" + startDateTimeMinute + "' and  EndDateTime = '" + endDateTimeMinute + "') order by LossID limit 2 ";

                                                                                    SqlDataAdapter daLoss = new SqlDataAdapter(queryLoss, mcpLoss.msqlConnection);
                                                                                    DataTable dtLoss = new DataTable();
                                                                                    daLoss.Fill(dtLoss);
                                                                                    mcpLoss.close();
                                                                                    if (dtLoss.Rows.Count > 0)
                                                                                    {
                                                                                        //Get the End Time & reason.
                                                                                        string endDateTimeMinute1 = null;
                                                                                        string endColor = null;

                                                                                        if (dtLoss.Rows.Count > 1)
                                                                                        {
                                                                                            endColor = "yellow";
                                                                                            endDateTimeMinute1 = Convert.ToDateTime(dtLoss.Rows[0][3]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            DataRow dr1 = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + dtLoss.Rows[0][2].ToString() + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                            if (dr1 != null)
                                                                                            {
                                                                                                endColor = Convert.ToString(dr1[10]);//Color DPS
                                                                                                endDateTimeMinute1 = Convert.ToDateTime(dr1[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                            }
                                                                                        }
                                                                                        endDateTimeMinute = endDateTimeMinute1;

                                                                                        #region Get endReason
                                                                                        string endCode = null;
                                                                                        if (endColor == "yellow")
                                                                                        {
                                                                                            //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                            mcpLoss1.open();
                                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute1).ToString("yyyy-MM-dd HH:mm:00") + "'  and StartDateTime <= '" + Convert.ToDateTime(endDateTimeMinute1).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                            DataTable dtLoss1 = new DataTable();
                                                                                            daLoss1.Fill(dtLoss1);
                                                                                            mcpLoss1.close();
                                                                                            string LossCodeID = null;
                                                                                            if (dtLoss1.Rows.Count > 0)
                                                                                            {
                                                                                                LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                            }
                                                                                            // Now based on LossCodeID get EndCode

                                                                                            TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                            mcpLoss2.open();
                                                                                            String queryLoss2 = "SELECT EndCode FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                            SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                            DataTable dtLoss2 = new DataTable();
                                                                                            daLoss2.Fill(dtLoss2);
                                                                                            mcpLoss2.close();
                                                                                            if (dtLoss2.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss2.Rows[0][0].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                string Type = "Others";
                                                                                                TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                                mcpLoss3.open();
                                                                                                String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                                SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                                DataTable dtLoss3 = new DataTable();
                                                                                                daLoss3.Fill(dtLoss3);
                                                                                                mcpLoss3.close();
                                                                                                if (dtLoss3.Rows.Count > 0)
                                                                                                {
                                                                                                    endCode = dtLoss3.Rows[0][1].ToString();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    endCode = "99";
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        else if (endColor == "green")
                                                                                        {
                                                                                            //string Type = "Green";
                                                                                            string Type = "Others";
                                                                                            TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                            mcpLoss3.open();
                                                                                            String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                            DataTable dtLoss3 = new DataTable();
                                                                                            daLoss3.Fill(dtLoss3);
                                                                                            mcpLoss3.close();
                                                                                            if (dtLoss3.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss3.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "98";
                                                                                            }
                                                                                        }
                                                                                        else if (endColor == "red")//It should be red 
                                                                                        {
                                                                                            string BreakdownType = "BREAKDOWN";
                                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                            mcpLoss1.open();
                                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                            DataTable dtLoss1 = new DataTable();
                                                                                            daLoss1.Fill(dtLoss1);
                                                                                            mcpLoss1.close();
                                                                                            if (dtLoss1.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "97";
                                                                                            }

                                                                                        }
                                                                                        else //Blue or Unknown
                                                                                        {
                                                                                            string BreakdownType = "Others";
                                                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                            mcpLoss1.open();
                                                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                            DataTable dtLoss1 = new DataTable();
                                                                                            daLoss1.Fill(dtLoss1);
                                                                                            mcpLoss1.close();
                                                                                            if (dtLoss1.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "99";
                                                                                            }
                                                                                        }
                                                                                        #endregion

                                                                                        if (endDateTimeMinute == null)
                                                                                        {
                                                                                            endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                        }

                                                                                        dtNew.Rows.Add(HMIID, "yellow", dtLoss.Rows[0][2].ToString(), endDateTimeMinute.ToString(), endCode, 0, WONo, OpNo);
                                                                                    }
                                                                                }
                                                                                else if (ToSAPLogic == 1)
                                                                                {

                                                                                }
                                                                            }
                                                                            else if (dtAll.Rows[k][10].ToString() == "green") //For Non-Yellow
                                                                            {
                                                                                #region BasicLogic
                                                                                ////Code for Green : 98
                                                                                //if (k == 0)
                                                                                //{
                                                                                //    string Color = dtAll.Rows[k][10].ToString();
                                                                                //    string StartTime = dtAll.Rows[k][2].ToString();
                                                                                //    //string EndTime = dtAll.Rows[k][10].ToString();
                                                                                //    //string Reason = dtAll.Rows[k][10].ToString();
                                                                                //    dtNew.Rows[k][0] = Color;
                                                                                //    dtNew.Rows[k][1] = StartTime;
                                                                                //    //dtNew.Rows[k][2] = EndTime;
                                                                                //    //dtNew.Rows[k][3] = Reason;
                                                                                //}
                                                                                //else
                                                                                //{
                                                                                ////End Previous Color
                                                                                //dtNew.Rows[k - 1][2] = dtAll.Rows[k][2].ToString();
                                                                                //dtNew.Rows[k - 1][3] = 66;
                                                                                #endregion

                                                                                PrvMode = "green";

                                                                                //Planned to End Current Color
                                                                                //Insert New Color
                                                                                //Get the End Time & reason.
                                                                                endDateTimeMinute = null;
                                                                                string endColor = null;
                                                                                DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'green'").FirstOrDefault();
                                                                                if (dr != null)
                                                                                {
                                                                                    endColor = Convert.ToString(dr[10]);//Color DPS
                                                                                    endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                }

                                                                                #region Get endReason
                                                                                string endCode = null;
                                                                                if (endColor == "yellow")
                                                                                {
                                                                                    //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    string LossCodeID = null;
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    // Now based on LossCodeID get EndCode

                                                                                    TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                    mcpLoss2.open();
                                                                                    String queryLoss2 = "SELECT EndCode FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                    SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                    DataTable dtLoss2 = new DataTable();
                                                                                    daLoss2.Fill(dtLoss2);
                                                                                    mcpLoss2.close();
                                                                                    if (dtLoss2.Rows[0][0].ToString() != null)
                                                                                    {
                                                                                        endCode = dtLoss2.Rows[0][0].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        string Type = "Others";
                                                                                        TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                        mcpLoss3.open();
                                                                                        String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' ";
                                                                                        SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                        DataTable dtLoss3 = new DataTable();
                                                                                        daLoss3.Fill(dtLoss3);
                                                                                        mcpLoss3.close();
                                                                                        if (dtLoss3.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss3.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "01";
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else if (endColor == "red")//It should be red 
                                                                                {
                                                                                    string BreakdownType = "BREAKDOWN";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "01";
                                                                                    }
                                                                                }
                                                                                else //Blue 
                                                                                {
                                                                                    string Type = "OTHERS";
                                                                                    TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                    mcpLoss1.open();
                                                                                    String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type + "' ";
                                                                                    SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                    DataTable dtLoss1 = new DataTable();
                                                                                    daLoss1.Fill(dtLoss1);
                                                                                    mcpLoss1.close();
                                                                                    if (dtLoss1.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss1.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "01";
                                                                                    }
                                                                                }
                                                                                #endregion

                                                                                if (endDateTimeMinute == null)
                                                                                {
                                                                                    endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                }

                                                                                dtNew.Rows.Add(HMIID, dtAll.Rows[k][10].ToString(), dtAll.Rows[k][2].ToString(), endDateTimeMinute, endCode, 0, WONo, OpNo);
                                                                                //}
                                                                            }
                                                                        }
                                                                    }
                                                                    #endregion

                                                                    if (dtAll.Rows.Count > 0)
                                                                    {
                                                                        dtNew.Rows[dtNew.Rows.Count - 1][5] = 2;
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    IntoFile("MultiWO " + e);
                                                                    continue;

                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            int isJobFinished = 0;
                                                            int.TryParse(Convert.ToString(dtInner.Rows[j][18]), out isJobFinished);

                                                            string endCode = null;
                                                            string BreakdownType = "Others";
                                                            TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                            mcpLoss1.open();
                                                            String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                            SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                            DataTable dtLoss1 = new DataTable();
                                                            daLoss1.Fill(dtLoss1);
                                                            mcpLoss1.close();
                                                            if (dtLoss1.Rows.Count > 0)
                                                            {
                                                                endCode = dtLoss1.Rows[0][1].ToString();
                                                            }
                                                            else
                                                            {
                                                                endCode = "01";
                                                            }
                                                            if (isJobFinished == 1) { isJobFinished = 2; } else { isJobFinished = 1; }
                                                            dtNew.Rows.Add(HMIID, "green", Convert.ToDateTime(dtInner.Rows[j][4]), Convert.ToDateTime(dtInner.Rows[j][5]), endCode, isJobFinished, WONo, OpNo);
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            IntoFile("MultiWO " + e);
                                                            continue;
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    IntoFile("MultiWO " + e);
                                                    continue;

                                                }
                                            }
                                            #endregion
                                        }
                                        catch (Exception e)
                                        {
                                            IntoFile("MultiWO " + e);
                                            continue;

                                        }
                                        //2) 
                                        try
                                        {
                                            #region
                                            TATAMysqlConnection mcpInner1 = new TATAMysqlConnection();
                                            mcpInner1.open();
                                            //String queryInner1 = "SELECT * FROM i_facility_shakti.dbo.tbllivemultiwoselections as ths WHERE ths.IsCompleted = 1 and ths.WorkOrder = '" + WONo + "' and ths.PartNo = '" + PartNo + "' and ths.OperationNo = '" + OpNo + "' order by CreatedOn";
                                            String queryInner1 = "SELECT * FROM i_facility_shakti.dbo.tbllivemultiwoselection as ths WHERE ths.WorkOrder = '" + WONo + "' and ths.PartNo = '" + PartNo + "' and ths.OperationNo = '" + OpNo + "' order by CreatedOn";
                                            SqlDataAdapter dashiftInner1 = new SqlDataAdapter(queryInner1, mcpInner1.msqlConnection);
                                            DataTable dtInner1 = new DataTable();
                                            dashiftInner1.Fill(dtInner1);
                                            mcpInner1.close();
                                           
                                            //MessageBox.Show("This Mac Query" + queryInner);
                                            for (int j = 0; j < dtInner1.Rows.Count; j++)
                                            {
                                                string HMIID = dtInner1.Rows[j][6].ToString();

                                                //Its a multiWO so Based on HMIID Get the SubmitDate and DoneWithRow.
                                                TATAMysqlConnection mcpInner2 = new TATAMysqlConnection();
                                                mcpInner2.open();
                                                String queryInner2 = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen WHERE HMIID = " + HMIID;
                                                SqlDataAdapter dashiftInner2 = new SqlDataAdapter(queryInner2, mcpInner2.msqlConnection);
                                                DataTable dtInner2 = new DataTable();
                                                dashiftInner2.Fill(dtInner2);
                                                mcpInner2.close();

                                                //To Extract all dpsdata b/w wo start and end time and get Last green
                                                DataTable dtAllJF = new DataTable();

                                                string SubmitDateTimeString = null, DoneWithRowDateTimeString = null;
                                                string LastGreenStartDateTimeString = null, FirstGreenStartDateTimeString = null;

                                                try
                                                {
                                                    SubmitDateTimeString = Convert.ToDateTime(dtInner2.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss");
                                                    DoneWithRowDateTimeString = Convert.ToDateTime(dtInner2.Rows[0][5]).ToString("yyyy-MM-dd HH:mm:ss");
                                                    using (TATAMysqlConnection mcpAllJF = new TATAMysqlConnection())
                                                    {
                                                        mcpAllJF.open();
                                                        //String queryAll = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' and ColorCode = 'green' order by ID limit 1";
                                                        //String queryAllJF = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and CorrectedDate ='" + Convert.ToDateTime(DoneWithRowDateTimeString).ToString("yyyy-MM-dd") + "'   and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' order by ID";
                                                        String queryAllJF = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "' and StartTime >='" + SubmitDateTimeString + "' and EndTime <= '" + DoneWithRowDateTimeString + "' order by ID";
                                                        SqlDataAdapter daAll = new SqlDataAdapter(queryAllJF, mcpAllJF.msqlConnection);
                                                        daAll.Fill(dtAllJF);
                                                        mcpAllJF.close();
                                                    }
                                                    for (int k = dtAllJF.Rows.Count - 1; k >= 0; k--)
                                                    {
                                                        if (dtAllJF.Rows[k][10].ToString() == "green")
                                                        {
                                                            LastGreenStartDateTimeString = Convert.ToDateTime(dtAllJF.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                            break;
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    IntoFile("MultiWO " + e);
                                                    continue;
                                                }

                                                ////int MachineID = Convert.ToInt32(dtInner.Rows[i][1]);
                                                ////Construct WONo OpNo string.
                                                //string WONoInner = Convert.ToString(dtInner.Rows[j][10]);
                                                //string OpNoInner = Convert.ToString(dtInner.Rows[j][8]);
                                                //string SorM = "S";
                                                //string Spaces13 = @"             ";
                                                //string WONoOpNoSorM13Spaces = null;
                                                //OpNoInner = OpNoInner.PadLeft(4, '0');
                                                //WONoInner = WONoInner.PadLeft(12, '0');

                                                //string SubmitDateTimeString = Convert.ToDateTime(dtInner.Rows[j][4]).ToString("yyyy-MM-dd HH:mm:ss");
                                                //string DoneWithRowDateTimeString = Convert.ToDateTime(dtInner.Rows[j][5]).ToString("yyyy-MM-dd HH:mm:ss");


                                                //Now Based on Target and Processed GET Status. 2017-01-11
                                                int isJobFinished = 0;
                                                int.TryParse(Convert.ToString(dtInner1.Rows[j][8]), out isJobFinished);

                                                try
                                                {
                                                    //New Logic for shorter Duration WO's
                                                    if ((Convert.ToDateTime(dtInner2.Rows[0][5]) - Convert.ToDateTime(dtInner2.Rows[0][4])).TotalMinutes > 2 && (!string.IsNullOrEmpty(LastGreenStartDateTimeString)))
                                                    {

                                                        //Its a Partial Finish
                                                        if (isJobFinished == 0)
                                                        {
                                                            //To Extract all dpsdata b/w wo start and end time that are different modes
                                                            try
                                                            {
                                                                #region
                                                                string PrvMode = null;
                                                                DataTable dtAll = new DataTable();
                                                                using (TATAMysqlConnection mcpAll1 = new TATAMysqlConnection())
                                                                {
                                                                    mcpAll1.open();
                                                                    String queryAll1 = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime >= '" + SubmitDateTimeString + "' and EndTime <= '" + LastGreenStartDateTimeString + "' order by ID ";
                                                                    SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                                                    daAll1.Fill(dtAll);
                                                                    mcpAll1.close();
                                                                }
                                                                DataTable dtColorEndRowChecker = dtAll.Copy();
                                                                for (int k = 0; k < dtAll.Rows.Count; k++)
                                                                {
                                                                    if (dtAll.Rows[k][10].ToString() != PrvMode)
                                                                    {
                                                                        string startDateTimeMinute = Convert.ToDateTime(dtAll.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                        string endDateTimeMinute = LastGreenStartDateTimeString;
                                                                        //ignore Non-Setup yellow
                                                                        if (dtAll.Rows[k][10].ToString() == "yellow")
                                                                        {
                                                                            PrvMode = "yellow";

                                                                            if (ToSAPLogic == 0)
                                                                            {
                                                                                //Get EndDateTime of current Yellow. else use WO endTime
                                                                                //taking the startDate of next color
                                                                                DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                if (dr != null)
                                                                                {
                                                                                    endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                }

                                                                                //int losscode = 0;
                                                                                TATAMysqlConnection mcpLoss = new TATAMysqlConnection();
                                                                                mcpLoss.open();
                                                                                //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime >= '" + endDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime > '" + startDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                String queryLoss = "SELECT * From tbllivelossofentry WHERE MachineID = '" + MachineID + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) and DoneWithRow = 1  and "
                                                                                                + "( StartDateTime <= '" + startDateTimeMinute + "' and ( ( EndDateTime > '" + startDateTimeMinute + "' and ( EndDateTime < '" + endDateTimeMinute + "' or   EndDateTime > '" + endDateTimeMinute + "' )  ) ) or "
                                                                                                + " (  StartDateTime > '" + startDateTimeMinute + "' and ( StartDateTime < '" + endDateTimeMinute + "' ) )) "
                                                                                                 + " or ( StartDateTime = '" + startDateTimeMinute + "' and  EndDateTime = '" + endDateTimeMinute + "') order by LossID limit 2 ";

                                                                                SqlDataAdapter daLoss = new SqlDataAdapter(queryLoss, mcpLoss.msqlConnection);
                                                                                DataTable dtLoss = new DataTable();
                                                                                daLoss.Fill(dtLoss);
                                                                                mcpLoss.close();

                                                                                if (dtLoss.Rows.Count > 0)
                                                                                {
                                                                                    //Get the End Time & reason.
                                                                                    string endDateTimeMinute1 = null;
                                                                                    string endColor = null;

                                                                                    if (dtLoss.Rows.Count > 1)
                                                                                    {
                                                                                        endColor = "yellow";
                                                                                        endDateTimeMinute1 = Convert.ToDateTime(dtLoss.Rows[0][3]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        DataRow dr1 = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + dtLoss.Rows[0][2].ToString() + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                        if (dr1 != null)
                                                                                        {
                                                                                            endColor = Convert.ToString(dr1[10]);//Color DPS
                                                                                            endDateTimeMinute1 = Convert.ToDateTime(dr1[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                        }
                                                                                    }
                                                                                    endDateTimeMinute = endDateTimeMinute1;

                                                                                    #region Get endReason
                                                                                    string endCode = null;
                                                                                    if (endColor == "yellow")
                                                                                    {
                                                                                        //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                        mcpLoss1.open();
                                                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute1).ToString("yyyy-MM-dd HH:mm:00") + "'  and StartDateTime <= '" + Convert.ToDateTime(endDateTimeMinute1).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                        DataTable dtLoss1 = new DataTable();
                                                                                        daLoss1.Fill(dtLoss1);
                                                                                        mcpLoss1.close();
                                                                                        string LossCodeID = null;
                                                                                        if (dtLoss1.Rows.Count > 0)
                                                                                        {
                                                                                            LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                        }
                                                                                        // Now based on LossCodeID get EndCode

                                                                                        TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                        mcpLoss2.open();
                                                                                        //String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                        String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                        SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                        DataTable dtLoss2 = new DataTable();
                                                                                        daLoss2.Fill(dtLoss2);
                                                                                        mcpLoss2.close();
                                                                                        if (dtLoss2.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss1.Rows[0][13].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            string Type = "Others";
                                                                                            TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                            mcpLoss3.open();
                                                                                            String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                            DataTable dtLoss3 = new DataTable();
                                                                                            daLoss3.Fill(dtLoss3);
                                                                                            mcpLoss3.close();
                                                                                            if (dtLoss3.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss3.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "99";
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else if (endColor == "green")
                                                                                    {
                                                                                        //string Type = "Green";
                                                                                        string Type = "Others";
                                                                                        TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                        mcpLoss3.open();
                                                                                        String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                        DataTable dtLoss3 = new DataTable();
                                                                                        daLoss3.Fill(dtLoss3);
                                                                                        mcpLoss3.close();
                                                                                        if (dtLoss3.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss3.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "98";
                                                                                        }
                                                                                    }
                                                                                    else if (endColor == "red")//It should be red 
                                                                                    {
                                                                                        string BreakdownType = "BREAKDOWN";
                                                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                        mcpLoss1.open();
                                                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                        DataTable dtLoss1 = new DataTable();
                                                                                        daLoss1.Fill(dtLoss1);
                                                                                        mcpLoss1.close();
                                                                                        if (dtLoss1.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss1.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "97";
                                                                                        }

                                                                                    }
                                                                                    else //Blue or Unknown
                                                                                    {
                                                                                        string BreakdownType = "Others";
                                                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                        mcpLoss1.open();
                                                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                        DataTable dtLoss1 = new DataTable();
                                                                                        daLoss1.Fill(dtLoss1);
                                                                                        mcpLoss1.close();
                                                                                        if (dtLoss1.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss1.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "99";
                                                                                        }

                                                                                    }
                                                                                    #endregion

                                                                                    if (endDateTimeMinute == null)
                                                                                    {
                                                                                        endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                    }

                                                                                    dtNew.Rows.Add(HMIID, "yellow", dtLoss.Rows[0][2].ToString(), dtLoss.Rows[0][3].ToString(), endCode, 0, WONo, OpNo);

                                                                                }
                                                                            }
                                                                            else if (ToSAPLogic == 1)
                                                                            {

                                                                            }
                                                                        }
                                                                        else if (dtAll.Rows[k][10].ToString() == "green") //For Non-Yellow
                                                                        {
                                                                            #region BasicLogic
                                                                            ////Code for Green : 98
                                                                            //if (k == 0)
                                                                            //{
                                                                            //    string Color = dtAll.Rows[k][10].ToString();
                                                                            //    string StartTime = dtAll.Rows[k][2].ToString();
                                                                            //    //string EndTime = dtAll.Rows[k][10].ToString();
                                                                            //    //string Reason = dtAll.Rows[k][10].ToString();
                                                                            //    dtNew.Rows[k][0] = Color;
                                                                            //    dtNew.Rows[k][1] = StartTime;
                                                                            //    //dtNew.Rows[k][2] = EndTime;
                                                                            //    //dtNew.Rows[k][3] = Reason;
                                                                            //}
                                                                            //else
                                                                            //{
                                                                            ////End Previous Color
                                                                            //dtNew.Rows[k - 1][2] = dtAll.Rows[k][2].ToString();
                                                                            //dtNew.Rows[k - 1][3] = 66;
                                                                            #endregion

                                                                            PrvMode = "green";

                                                                            //Planned to End Current Color
                                                                            //Insert New Color
                                                                            //Get the End Time & reason.
                                                                            endDateTimeMinute = null;
                                                                            string endColor = null;
                                                                            DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'green'").FirstOrDefault();
                                                                            if (dr != null)
                                                                            {
                                                                                endColor = Convert.ToString(dr[10]);//Color DPS
                                                                                endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                            }

                                                                            #region Get endReason
                                                                            string endCode = null;
                                                                            if (endColor == "yellow")
                                                                            {
                                                                                //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                mcpLoss1.open();
                                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                DataTable dtLoss1 = new DataTable();
                                                                                daLoss1.Fill(dtLoss1);
                                                                                mcpLoss1.close();
                                                                                string LossCodeID = null;
                                                                                if (dtLoss1.Rows.Count > 0)
                                                                                {
                                                                                    LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                }
                                                                                // Now based on LossCodeID get EndCode

                                                                                TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                mcpLoss2.open();
                                                                                //String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeID = '" + LossCodeID + "' limit 1 "; //2017-01-10
                                                                                String queryLoss2 = "SELECT * FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                DataTable dtLoss2 = new DataTable();
                                                                                daLoss2.Fill(dtLoss2);
                                                                                mcpLoss2.close();
                                                                                if (dtLoss2.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss1.Rows[0][13].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    string Type = "Others";
                                                                                    TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                    mcpLoss3.open();
                                                                                    String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                    SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                    DataTable dtLoss3 = new DataTable();
                                                                                    daLoss3.Fill(dtLoss3);
                                                                                    mcpLoss3.close();
                                                                                    if (dtLoss3.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss3.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "01";
                                                                                    }
                                                                                }
                                                                            }
                                                                            else if (endColor == "red")//It should be red 
                                                                            {
                                                                                string BreakdownType = "BREAKDOWN";
                                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                mcpLoss1.open();
                                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                DataTable dtLoss1 = new DataTable();
                                                                                daLoss1.Fill(dtLoss1);
                                                                                mcpLoss1.close();
                                                                                if (dtLoss1.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss1.Rows[0][1].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    endCode = "01";
                                                                                }
                                                                            }
                                                                            else //Blue 
                                                                            {
                                                                                string BreakdownType = "Others";
                                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                mcpLoss1.open();
                                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                DataTable dtLoss1 = new DataTable();
                                                                                daLoss1.Fill(dtLoss1);
                                                                                mcpLoss1.close();
                                                                                if (dtLoss1.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss1.Rows[0][1].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    endCode = "01";
                                                                                }
                                                                            }
                                                                            #endregion

                                                                            if (endDateTimeMinute == null)
                                                                            {
                                                                                endDateTimeMinute = LastGreenStartDateTimeString;
                                                                            }

                                                                            dtNew.Rows.Add(HMIID, dtAll.Rows[k][10].ToString(), dtAll.Rows[k][2].ToString(), endDateTimeMinute, endCode, 0, WONo, OpNo);
                                                                            //}
                                                                        }
                                                                    }
                                                                }
                                                                #endregion

                                                                if (dtAll.Rows.Count > 0)
                                                                {
                                                                    dtNew.Rows[dtNew.Rows.Count - 1][5] = 1;
                                                                }
                                                            }
                                                            catch (Exception e)
                                                            {

                                                            }
                                                        }
                                                        else //Its JF
                                                        {
                                                            //To Extract all dpsdata b/w wo start and end time that are different modes
                                                            try
                                                            {
                                                                #region
                                                                string PrvMode = null;
                                                                DataTable dtAll = new DataTable();
                                                                using (TATAMysqlConnection mcpAll1 = new TATAMysqlConnection())
                                                                {
                                                                    mcpAll1.open();
                                                                    String queryAll1 = "SELECT * FROM i_facility_shakti.dbo.tbllivedailyprodstatus  WHERE MachineID = '" + MachineID + "'  and StartTime >= '" + SubmitDateTimeString + "' and EndTime <= '" + LastGreenStartDateTimeString + "' order by ID ";
                                                                    SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                                                    daAll1.Fill(dtAll);
                                                                    mcpAll1.close();
                                                                }
                                                                DataTable dtColorEndRowChecker = dtAll.Copy();
                                                                for (int k = 0; k < dtAll.Rows.Count; k++)
                                                                {
                                                                    if (dtAll.Rows[k][10].ToString() != PrvMode)
                                                                    {
                                                                        string startDateTimeMinute = Convert.ToDateTime(dtAll.Rows[k][2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                        string endDateTimeMinute = LastGreenStartDateTimeString;
                                                                        //ignore Non-Setup yellow
                                                                        if (dtAll.Rows[k][10].ToString() == "yellow")
                                                                        {
                                                                            PrvMode = "yellow";

                                                                            if (ToSAPLogic == 0)
                                                                            {
                                                                                //Get EndDateTime of current Yellow. else use WO endTime
                                                                                //taking the startDate of next color
                                                                                DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                if (dr != null)
                                                                                {
                                                                                    endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                }

                                                                                //int losscode = 0;
                                                                                TATAMysqlConnection mcpLoss = new TATAMysqlConnection();
                                                                                mcpLoss.open();
                                                                                //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime >= '" + endDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                //String queryLoss = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime <= '" + startDateTimeMinute + "' and EndDateTime > '" + startDateTimeMinute + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) order by LossID limit 1 ";
                                                                                String queryLoss = "SELECT * From tbllivelossofentry WHERE MachineID = '" + MachineID + "' and MessageCodeID IN (select LossCodeID from i_facility_shakti.dbo.tbllossescodes where LossCodesLevel1ID = 1 and IsDeleted = 0 ) and DoneWithRow = 1  and "
                                                                                                + "( StartDateTime <= '" + startDateTimeMinute + "' and ( ( EndDateTime > '" + startDateTimeMinute + "' and ( EndDateTime < '" + endDateTimeMinute + "' or   EndDateTime > '" + endDateTimeMinute + "' )  ) ) or "
                                                                                                + " (  StartDateTime > '" + startDateTimeMinute + "' and ( StartDateTime < '" + endDateTimeMinute + "' ) )) "
                                                                                                + " or ( StartDateTime = '" + startDateTimeMinute + "' and  EndDateTime = '" + endDateTimeMinute + "') order by LossID limit 2 ";

                                                                                SqlDataAdapter daLoss = new SqlDataAdapter(queryLoss, mcpLoss.msqlConnection);
                                                                                DataTable dtLoss = new DataTable();
                                                                                daLoss.Fill(dtLoss);
                                                                                mcpLoss.close();
                                                                                if (dtLoss.Rows.Count > 0)
                                                                                {
                                                                                    //Get the End Time & reason.
                                                                                    string endDateTimeMinute1 = null;
                                                                                    string endColor = null;

                                                                                    if (dtLoss.Rows.Count > 1)
                                                                                    {
                                                                                        endColor = "yellow";
                                                                                        endDateTimeMinute1 = Convert.ToDateTime(dtLoss.Rows[0][3]).ToString("yyyy-MM-dd HH:mm:ss");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        DataRow dr1 = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + dtLoss.Rows[0][2].ToString() + "' AND ColorCode <> 'yellow'").FirstOrDefault();
                                                                                        if (dr1 != null)
                                                                                        {
                                                                                            endColor = Convert.ToString(dr1[10]);//Color DPS
                                                                                            endDateTimeMinute1 = Convert.ToDateTime(dr1[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                                        }
                                                                                    }
                                                                                    endDateTimeMinute = endDateTimeMinute1;

                                                                                    #region Get endReason
                                                                                    string endCode = null;
                                                                                    if (endColor == "yellow")
                                                                                    {
                                                                                        //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                        mcpLoss1.open();
                                                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute1).ToString("yyyy-MM-dd HH:mm:00") + "'  and StartDateTime <= '" + Convert.ToDateTime(endDateTimeMinute1).AddMinutes(1).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                        DataTable dtLoss1 = new DataTable();
                                                                                        daLoss1.Fill(dtLoss1);
                                                                                        mcpLoss1.close();
                                                                                        string LossCodeID = null;
                                                                                        if (dtLoss1.Rows.Count > 0)
                                                                                        {
                                                                                            LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                        }
                                                                                        // Now based on LossCodeID get EndCode

                                                                                        TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                        mcpLoss2.open();
                                                                                        String queryLoss2 = "SELECT EndCode FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                        SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                        DataTable dtLoss2 = new DataTable();
                                                                                        daLoss2.Fill(dtLoss2);
                                                                                        mcpLoss2.close();
                                                                                        if (dtLoss2.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss2.Rows[0][0].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            string Type = "Others";
                                                                                            TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                            mcpLoss3.open();
                                                                                            String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                            SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                            DataTable dtLoss3 = new DataTable();
                                                                                            daLoss3.Fill(dtLoss3);
                                                                                            mcpLoss3.close();
                                                                                            if (dtLoss3.Rows.Count > 0)
                                                                                            {
                                                                                                endCode = dtLoss3.Rows[0][1].ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                endCode = "99";
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else if (endColor == "green")
                                                                                    {
                                                                                        //string Type = "Green";
                                                                                        string Type = "Others";
                                                                                        TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                        mcpLoss3.open();
                                                                                        String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                        DataTable dtLoss3 = new DataTable();
                                                                                        daLoss3.Fill(dtLoss3);
                                                                                        mcpLoss3.close();
                                                                                        if (dtLoss3.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss3.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "98";
                                                                                        }
                                                                                    }
                                                                                    else if (endColor == "red")//It should be red 
                                                                                    {
                                                                                        string BreakdownType = "BREAKDOWN";
                                                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                        mcpLoss1.open();
                                                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                        DataTable dtLoss1 = new DataTable();
                                                                                        daLoss1.Fill(dtLoss1);
                                                                                        mcpLoss1.close();
                                                                                        if (dtLoss1.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss1.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "97";
                                                                                        }

                                                                                    }
                                                                                    else //Blue or Unknown
                                                                                    {
                                                                                        string BreakdownType = "Others";
                                                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                        mcpLoss1.open();
                                                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                        DataTable dtLoss1 = new DataTable();
                                                                                        daLoss1.Fill(dtLoss1);
                                                                                        mcpLoss1.close();
                                                                                        if (dtLoss1.Rows.Count > 0)
                                                                                        {
                                                                                            endCode = dtLoss1.Rows[0][1].ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            endCode = "99";
                                                                                        }
                                                                                    }
                                                                                    #endregion

                                                                                    if (endDateTimeMinute == null)
                                                                                    {
                                                                                        endDateTimeMinute = LastGreenStartDateTimeString;
                                                                                    }

                                                                                    dtNew.Rows.Add(HMIID, "yellow", dtLoss.Rows[0][2].ToString(), endDateTimeMinute.ToString(), endCode, 0, WONo, OpNo);
                                                                                }
                                                                            }
                                                                            else if (ToSAPLogic == 1)
                                                                            {
                                                                            }
                                                                        }
                                                                        else if (dtAll.Rows[k][10].ToString() == "green") //For Non-Yellow
                                                                        {
                                                                            #region BasicLogic
                                                                            ////Code for Green : 98
                                                                            //if (k == 0)
                                                                            //{
                                                                            //    string Color = dtAll.Rows[k][10].ToString();
                                                                            //    string StartTime = dtAll.Rows[k][2].ToString();
                                                                            //    //string EndTime = dtAll.Rows[k][10].ToString();
                                                                            //    //string Reason = dtAll.Rows[k][10].ToString();
                                                                            //    dtNew.Rows[k][0] = Color;
                                                                            //    dtNew.Rows[k][1] = StartTime;
                                                                            //    //dtNew.Rows[k][2] = EndTime;
                                                                            //    //dtNew.Rows[k][3] = Reason;
                                                                            //}
                                                                            //else
                                                                            //{
                                                                            ////End Previous Color
                                                                            //dtNew.Rows[k - 1][2] = dtAll.Rows[k][2].ToString();
                                                                            //dtNew.Rows[k - 1][3] = 66;
                                                                            #endregion

                                                                            PrvMode = "green";

                                                                            //Planned to End Current Color
                                                                            //Insert New Color
                                                                            //Get the End Time & reason.
                                                                            endDateTimeMinute = null;
                                                                            string endColor = null;
                                                                            DataRow dr = dtColorEndRowChecker.Select("MachineID =" + MachineID + " AND StartTime > '" + startDateTimeMinute + "' AND ColorCode <> 'green'").FirstOrDefault();
                                                                            if (dr != null)
                                                                            {
                                                                                endColor = Convert.ToString(dr[10]);//Color DPS
                                                                                endDateTimeMinute = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd HH:mm:ss"); //StartTime DPS
                                                                            }

                                                                            #region Get endReason
                                                                            string endCode = null;
                                                                            if (endColor == "yellow")
                                                                            {
                                                                                //Look in tbllivelossofentry for LossCode,  tblendcodes for EndCode
                                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                mcpLoss1.open();
                                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tbllivelossofentry  WHERE MachineID = '" + MachineID + "'  and StartDateTime >= '" + Convert.ToDateTime(endDateTimeMinute).ToString("yyyy-MM-dd HH:mm:00") + "' order by LossID desc limit 1 ";
                                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                DataTable dtLoss1 = new DataTable();
                                                                                daLoss1.Fill(dtLoss1);
                                                                                mcpLoss1.close();
                                                                                string LossCodeID = null;
                                                                                if (dtLoss1.Rows.Count > 0)
                                                                                {
                                                                                    LossCodeID = dtLoss1.Rows[0][1].ToString();
                                                                                }
                                                                                // Now based on LossCodeID get EndCode

                                                                                TATAMysqlConnection mcpLoss2 = new TATAMysqlConnection();
                                                                                mcpLoss2.open();
                                                                                String queryLoss2 = "SELECT EndCode FROM i_facility_shakti.dbo.tbllossescodes  WHERE LossCodeID = '" + LossCodeID + "' limit 1 ";
                                                                                SqlDataAdapter daLoss2 = new SqlDataAdapter(queryLoss2, mcpLoss2.msqlConnection);
                                                                                DataTable dtLoss2 = new DataTable();
                                                                                daLoss2.Fill(dtLoss2);
                                                                                mcpLoss2.close();
                                                                                if (dtLoss2.Rows[0][0].ToString() != null)
                                                                                {
                                                                                    endCode = dtLoss2.Rows[0][0].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    string Type = "Others";
                                                                                    TATAMysqlConnection mcpLoss3 = new TATAMysqlConnection();
                                                                                    mcpLoss3.open();
                                                                                    String queryLoss3 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type.ToUpper() + "' ";
                                                                                    SqlDataAdapter daLoss3 = new SqlDataAdapter(queryLoss3, mcpLoss3.msqlConnection);
                                                                                    DataTable dtLoss3 = new DataTable();
                                                                                    daLoss3.Fill(dtLoss3);
                                                                                    mcpLoss3.close();
                                                                                    if (dtLoss3.Rows.Count > 0)
                                                                                    {
                                                                                        endCode = dtLoss3.Rows[0][1].ToString();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        endCode = "01";
                                                                                    }
                                                                                }
                                                                            }
                                                                            else if (endColor == "red")//It should be red 
                                                                            {
                                                                                string BreakdownType = "BREAKDOWN";
                                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                mcpLoss1.open();
                                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' ";
                                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                DataTable dtLoss1 = new DataTable();
                                                                                daLoss1.Fill(dtLoss1);
                                                                                mcpLoss1.close();
                                                                                if (dtLoss1.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss1.Rows[0][1].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    endCode = "01";
                                                                                }
                                                                            }
                                                                            else //Blue 
                                                                            {
                                                                                string Type = "OTHERS";
                                                                                TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                                                mcpLoss1.open();
                                                                                String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + Type + "' ";
                                                                                SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                                                DataTable dtLoss1 = new DataTable();
                                                                                daLoss1.Fill(dtLoss1);
                                                                                mcpLoss1.close();
                                                                                if (dtLoss1.Rows.Count > 0)
                                                                                {
                                                                                    endCode = dtLoss1.Rows[0][1].ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    endCode = "01";
                                                                                }
                                                                            }
                                                                            #endregion

                                                                            if (endDateTimeMinute == null)
                                                                            {
                                                                                endDateTimeMinute = LastGreenStartDateTimeString;
                                                                            }

                                                                            dtNew.Rows.Add(HMIID, dtAll.Rows[k][10].ToString(), dtAll.Rows[k][2].ToString(), endDateTimeMinute, endCode, 0, WONo, OpNo);
                                                                            //}
                                                                        }
                                                                    }
                                                                }
                                                                #endregion

                                                                if (dtAll.Rows.Count > 0)
                                                                {
                                                                    dtNew.Rows[dtNew.Rows.Count - 1][5] = 2;
                                                                }
                                                            }
                                                            catch (Exception e)
                                                            {

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string endCode = null;
                                                        string BreakdownType = "Others";
                                                        TATAMysqlConnection mcpLoss1 = new TATAMysqlConnection();
                                                        mcpLoss1.open();
                                                        String queryLoss1 = "SELECT * FROM i_facility_shakti.dbo.tblendcodes  WHERE EndCodeSDesc = '" + BreakdownType.ToUpper() + "' order by EndCodeID limit 1 ";
                                                        SqlDataAdapter daLoss1 = new SqlDataAdapter(queryLoss1, mcpLoss1.msqlConnection);
                                                        DataTable dtLoss1 = new DataTable();
                                                        daLoss1.Fill(dtLoss1);
                                                        mcpLoss1.close();
                                                        if (dtLoss1.Rows.Count > 0)
                                                        {
                                                            endCode = dtLoss1.Rows[0][1].ToString();
                                                        }
                                                        else
                                                        {
                                                            endCode = "01";
                                                        }
                                                        if (isJobFinished == 1) { isJobFinished = 2; } else { isJobFinished = 1; }
                                                        dtNew.Rows.Add(HMIID, "green", Convert.ToDateTime(dtInner2.Rows[0][4]), Convert.ToDateTime(dtInner2.Rows[0][5]), endCode, isJobFinished, WONo, OpNo);
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    IntoFile(" " + e);
                                                }
                                            }
                                            #endregion
                                        }
                                        catch (Exception e)
                                        {
                                            IntoFile("MultiWO " + e);
                                            continue;

                                        }

                                        MaindtNew.Merge(dtNew);
                                    }
                                }

                                #endregion
                                //MessageBox.Show("done with 1 wo");
                            }
                            #endregion
                        }
                        catch (Exception e)
                        {
                            IntoFile("" + e);
                            continue;
                        }

                    } //End of WO List which r JobFinished
                }
                catch (Exception e)
                {
                    IntoFile("MultiWO " + e);
                }

                #region Now Push into TextFile.

                long maxFileLength = 22500; //22.5 KB
                string prevHMIID = null;
                //Sort the DataTable
                DataView dataview = new DataView(MaindtNew);
                dataview.Sort = "StartTime";
                DataTable DTMaindtNew = dataview.ToTable();

                DataTable dtToSort = new DataTable();
                dtToSort.Columns.Add("WOState", typeof(int));
                dtToSort.Columns.Add("ProjectToDate", typeof(string));
                dtToSort.Columns.Add("DateTimeCol", typeof(DateTime));

                try
                {
                    DataTable dtHMIID = new DataTable();
                    //int FileCountLooper = 1;
                    //sw = new StreamWriter(OutputPath + "-" + FileCountLooper + ".txt", true);

                    if (ToSAPLogic == 0)
                    {
                        for (int toFileLooper = 0; toFileLooper < DTMaindtNew.Rows.Count; toFileLooper++)
                        {
                            string dataToFile = null;
                            string HMIID = DTMaindtNew.Rows[toFileLooper][0].ToString();

                            if (HMIID != prevHMIID)
                            {
                                prevHMIID = HMIID;
                                dtHMIID.Clear();
                                TATAMysqlConnection mcpAll1 = new TATAMysqlConnection();
                                mcpAll1.open();
                                String queryAll1 = " SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen where HMIID = " + HMIID + " ";
                                SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                daAll1.Fill(dtHMIID);
                                mcpAll1.close();
                            }

                            string WONoInner = null, OpNoInner = null, StartDateString = null, StartTimeString = null, EndDateString = null, EndTimeString = null;
                            string SorM = null, WONoOpNoSorM13Spaces = null, codeToEndPrvColor = null;
                            string Spaces13 = @"             ";

                            try
                            {
                                //Insert Dummy SetupEndRows
                                if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 2)
                                {
                                    //WONoInner = Convert.ToString(dtHMIID.Rows[0][10]);
                                    //OpNoInner = Convert.ToString(dtHMIID.Rows[0][8]);

                                    WONoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][6]);
                                    OpNoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][7]);

                                    int deliveredQty = 0, processedQty = 0;
                                    if (Convert.ToInt32(dtHMIID.Rows[0][24]) == 0) //its a single workorder
                                    {
                                        int.TryParse(Convert.ToString(dtHMIID.Rows[0][12]), out deliveredQty);
                                        int.TryParse(Convert.ToString(dtHMIID.Rows[0][22]), out processedQty);
                                    }
                                    else //Its a multi WorkOrder
                                    {
                                        DataTable dtQtyData = new DataTable();
                                        TATAMysqlConnection mcQtyData = new TATAMysqlConnection();
                                        mcQtyData.open();
                                        String queryQtyData = " SELECT * FROM i_facility_shakti.dbo.tbllivemultiwoselection where HMIID = " + HMIID + "  and WorkOrder = " + WONoInner + " and OperationNo = " + OpNoInner + " ";
                                        SqlDataAdapter daQtyData = new SqlDataAdapter(queryQtyData, mcQtyData.msqlConnection);
                                        daQtyData.Fill(dtQtyData);
                                        mcQtyData.close();

                                        //int.TryParse(Convert.ToString(dtQtyData.Rows[0][5]), out deliveredQty);
                                        int.TryParse(Convert.ToString(dtQtyData.Rows[0][4]), out deliveredQty);
                                        //int.TryParse(Convert.ToString(dtQtyData.Rows[0][22]), out processedQty); // Not for MultiWO
                                    }


                                    SorM = "S";
                                    OpNoInner = OpNoInner.PadLeft(4, '0');
                                    WONoInner = WONoInner.PadLeft(12, '0');
                                    WONoOpNoSorM13Spaces = WONoInner + OpNoInner + SorM + Spaces13;

                                    //StartDateString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("dd/MM/yyyy");
                                    DateTime StartDateTemp = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2]));
                                    StartDateString = String.Format("{0:00}", StartDateTemp.Day) + "/" + String.Format("{0:00}", StartDateTemp.Month) + "/" + StartDateTemp.Year;
                                    StartTimeString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("HH:mm:ss");

                                    dataToFile = "1,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + StartDateString + "," + StartTimeString;
                                    if (dataToFile != null)
                                    {
                                        //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFile);

                                        string projectToDate = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces;
                                        dtToSort.Rows.Add(1, projectToDate, Convert.ToDateTime(StartDateTemp.ToString("yyyy-MM-dd") + " " + StartTimeString));

                                        //sw.Flush();
                                        //long FileLength = sw.BaseStream.Length;

                                        //if ((FileLength + dataOfFileLength) < maxFileLength)
                                        //{
                                        //    sw.WriteLine(dataToFile);
                                        //}
                                        //else
                                        //{
                                        //    sw.Flush();
                                        //    sw.Close();
                                        //    FileCountLooper++;
                                        //    sw = new StreamWriter(OutputPath + "-" + FileCountLooper + ".txt", true);
                                        //    sw.WriteLine(dataToFile);
                                        //}
                                        //sw.WriteLine(dataToFile);
                                        dataToFile = null;
                                    }
                                    codeToEndPrvColor = DTMaindtNew.Rows[toFileLooper][4].ToString();
                                    codeToEndPrvColor = codeToEndPrvColor.PadLeft(2, '0');
                                    string RejectedQtyString = "00";



                                    string DeliveredQtyInnerString = Convert.ToString(deliveredQty + processedQty);
                                    DeliveredQtyInnerString = DeliveredQtyInnerString.PadLeft(2, '0');

                                    dataToFile = "3," + WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString + "," + RejectedQtyString + "," + StartDateString + "," + StartTimeString;

                                    if (dataToFile != null)
                                    {
                                        //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFile);

                                        string projectToDate = @WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString + "," + RejectedQtyString;
                                        dtToSort.Rows.Add(3, projectToDate, Convert.ToDateTime(StartDateTemp.ToString("yyyy-MM-dd") + " " + StartTimeString));

                                        //sw.Flush();
                                        //long FileLength = sw.BaseStream.Length;
                                        //if ((FileLength + dataOfFileLength) < maxFileLength)
                                        //{
                                        //    sw.WriteLine(dataToFile);
                                        //}
                                        //else
                                        //{
                                        //    sw.Flush();
                                        //    sw.Close();
                                        //    FileCountLooper++;
                                        //    sw = new StreamWriter(OutputPath + "-" + FileCountLooper + ".txt", true);
                                        //    sw.WriteLine(dataToFile);
                                        //}
                                        //sw.WriteLine(dataToFile);
                                        dataToFile = null;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                IntoFile(" " + e);
                                continue;

                            }

                            //WONoInner = Convert.ToString(dtHMIID.Rows[0][10]);
                            //OpNoInner = Convert.ToString(dtHMIID.Rows[0][8]);
                            WONoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][6]);
                            OpNoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][7]);

                            SorM = "M";
                            if ((string)DTMaindtNew.Rows[toFileLooper][1] == "yellow")
                            {
                                SorM = "S";
                            }
                            OpNoInner = OpNoInner.PadLeft(4, '0');
                            WONoInner = WONoInner.PadLeft(12, '0');
                            WONoOpNoSorM13Spaces = WONoInner + OpNoInner + SorM + Spaces13;

                            //StartDateString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("dd/MM/yyyy");
                            DateTime StartDateTemp1 = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2]));
                            StartDateString = String.Format("{0:00}", StartDateTemp1.Day) + "/" + String.Format("{0:00}", StartDateTemp1.Month) + "/" + StartDateTemp1.Year;

                            StartTimeString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("HH:mm:ss");

                            dataToFile = "1,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + StartDateString + "," + StartTimeString;
                            if (dataToFile != null)
                            {

                                string projectToSpace = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces;
                                dtToSort.Rows.Add(1, projectToSpace, Convert.ToDateTime(StartDateTemp1.ToString("yyyy-MM-dd") + " " + StartTimeString));

                                //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFile);
                                //sw.Flush();
                                //long FileLength = sw.BaseStream.Length;
                                //if ((FileLength + dataOfFileLength) < maxFileLength)
                                //{
                                //    sw.WriteLine(dataToFile);
                                //}
                                //else
                                //{
                                //    sw.Flush();
                                //    sw.Close();
                                //    FileCountLooper++;
                                //    sw = new StreamWriter(OutputPath + "-" + FileCountLooper + ".txt", true);
                                //    sw.WriteLine(dataToFile);
                                //}
                                ////sw.WriteLine(dataToFile);
                                dataToFile = null;
                            }

                            DateTime EndDateTemp = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][3]));
                            //string a = String.Format("{0:00}", DateTime.Now.Day) + "/" + String.Format("{0:00}", DateTime.Now.Month) + "/" + DateTime.Now.Year;
                            EndDateString = String.Format("{0:00}", EndDateTemp.Day) + "/" + String.Format("{0:00}", EndDateTemp.Month) + "/" + EndDateTemp.Year;
                            EndTimeString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][3])).ToString("HH:mm:ss");

                            codeToEndPrvColor = DTMaindtNew.Rows[toFileLooper][4].ToString();
                            codeToEndPrvColor = codeToEndPrvColor.PadLeft(2, '0');
                            string DeliveredQtyInnerString1 = null, RejectedQtyString1 = null;

                            if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 0)
                            {
                                dataToFile = "2,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor + "," + EndDateString + "," + EndTimeString;

                                if (dataToFile != null)
                                {
                                    string projectToDate = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor;
                                    dtToSort.Rows.Add(2, projectToDate, Convert.ToDateTime(EndDateTemp.ToString("yyyy-MM-dd") + " " + EndTimeString));
                                    dataToFile = null;
                                }
                            }
                            else if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 1)
                            {
                                dataToFile = "2,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor + "," + EndDateString + "," + EndTimeString;
                                if (dataToFile != null)
                                {
                                    string projectToDate = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor;
                                    dtToSort.Rows.Add(2, projectToDate, Convert.ToDateTime(EndDateTemp.ToString("yyyy-MM-dd") + " " + EndTimeString));
                                    dataToFile = null;
                                }
                            }
                            else if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 2) //Its JobFinish
                            {
                                RejectedQtyString1 = "00";
                                //int deliveredQty = 0;
                                //int.TryParse(Convert.ToString(dtHMIID.Rows[0][12]), out deliveredQty);
                                //int processedQty = 0;
                                //int.TryParse(Convert.ToString(dtHMIID.Rows[0][22]), out processedQty);

                                int deliveredQty = 0, processedQty = 0;
                                if (Convert.ToInt32(dtHMIID.Rows[0][24]) == 0) //its a single workorder
                                {
                                    int.TryParse(Convert.ToString(dtHMIID.Rows[0][12]), out deliveredQty);
                                    int.TryParse(Convert.ToString(dtHMIID.Rows[0][22]), out processedQty);
                                }
                                else //Its a multi WorkOrder
                                {
                                    DataTable dtQtyData = new DataTable();
                                    TATAMysqlConnection mcQtyData = new TATAMysqlConnection();
                                    mcQtyData.open();
                                    String queryQtyData = " SELECT * FROM i_facility_shakti.dbo.tbllivemultiwoselection where HMIID = " + HMIID + "  and WorkOrder = " + WONoInner + " and OperationNo = " + OpNoInner + " ";
                                    SqlDataAdapter daQtyData = new SqlDataAdapter(queryQtyData, mcQtyData.msqlConnection);
                                    daQtyData.Fill(dtQtyData);
                                    mcQtyData.close();

                                    //int.TryParse(Convert.ToString(dtQtyData.Rows[0][5]), out deliveredQty);
                                    int.TryParse(Convert.ToString(dtQtyData.Rows[0][4]), out deliveredQty);
                                    //int.TryParse(Convert.ToString(dtQtyData.Rows[0][22]), out processedQty); // Not for MultiWO
                                }

                                DeliveredQtyInnerString1 = Convert.ToString(deliveredQty + processedQty);

                                DeliveredQtyInnerString1 = DeliveredQtyInnerString1.PadLeft(2, '0');
                                dataToFile = "3," + WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString1 + "," + RejectedQtyString1 + "," + EndDateString + "," + EndTimeString;

                                if (dataToFile != null)
                                {
                                    string projectToDate = @WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString1 + "," + RejectedQtyString1;
                                    dtToSort.Rows.Add(3, projectToDate, Convert.ToDateTime(EndDateTemp.ToString("yyyy-MM-dd") + " " + EndTimeString));
                                    dataToFile = null;
                                }
                            }
                        }
                    }
                    else if (ToSAPLogic == 1)
                    {

                        #region For SheetMetal and Similar
                        for (int toFileLooper = 0; toFileLooper < DTMaindtNew.Rows.Count; toFileLooper++)
                        {
                            string dataToFile = null;
                            string HMIID = DTMaindtNew.Rows[toFileLooper][0].ToString();

                            if (HMIID != prevHMIID)
                            {
                                prevHMIID = HMIID;
                                dtHMIID.Clear();
                                TATAMysqlConnection mcpAll1 = new TATAMysqlConnection();
                                mcpAll1.open();
                                String queryAll1 = " SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen where HMIID = " + HMIID + " ";
                                SqlDataAdapter daAll1 = new SqlDataAdapter(queryAll1, mcpAll1.msqlConnection);
                                daAll1.Fill(dtHMIID);
                                mcpAll1.close();
                            }

                            string WONoInner = null, OpNoInner = null, StartDateString = null, StartTimeString = null, EndDateString = null, EndTimeString = null;
                            string SorM = null, WONoOpNoSorM13Spaces = null, codeToEndPrvColor = null;
                            string Spaces13 = @"             ";

                            ////Insert Dummy SetupEndRows  On 2017-01-27
                            //if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 2)
                            //{
                            //    //WONoInner = Convert.ToString(dtHMIID.Rows[0][10]);
                            //    //OpNoInner = Convert.ToString(dtHMIID.Rows[0][8]);

                            //    WONoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][6]);
                            //    OpNoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][7]);

                            //    SorM = "S";
                            //    OpNoInner = OpNoInner.PadLeft(4, '0');
                            //    WONoInner = WONoInner.PadLeft(12, '0');
                            //    WONoOpNoSorM13Spaces = WONoInner + OpNoInner + SorM + Spaces13;

                            //    //StartDateString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("dd/MM/yyyy");
                            //    DateTime StartDateTemp = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2]));
                            //    StartDateString = String.Format("{0:00}", StartDateTemp.Day) + "/" + String.Format("{0:00}", StartDateTemp.Month) + "/" + StartDateTemp.Year;
                            //    StartTimeString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("HH:mm:ss");

                            //    dataToFile = "1,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + StartDateString + "," + StartTimeString;
                            //    if (dataToFile != null)
                            //    {
                            //        //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFile);

                            //        string projectToDate = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces;
                            //        dtToSort.Rows.Add(1, projectToDate, Convert.ToDateTime(StartDateTemp.ToString("yyyy-MM-dd") + " " + StartTimeString));
                            //        dataToFile = null;
                            //    }
                            //    codeToEndPrvColor = DTMaindtNew.Rows[toFileLooper][4].ToString();
                            //    codeToEndPrvColor = codeToEndPrvColor.PadLeft(2, '0');
                            //    string RejectedQtyString = "00";
                            //    int deliveredQty = 0;
                            //    int.TryParse(Convert.ToString(dtHMIID.Rows[0][12]), out deliveredQty);
                            //    int processedQty = 0;
                            //    int.TryParse(Convert.ToString(dtHMIID.Rows[0][22]), out processedQty);
                            //    string DeliveredQtyInnerString = Convert.ToString(deliveredQty + processedQty);
                            //    DeliveredQtyInnerString = DeliveredQtyInnerString.PadLeft(2, '0');

                            //    dataToFile = "3," + WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString + "," + RejectedQtyString + "," + StartDateString + "," + StartTimeString;

                            //    if (dataToFile != null)
                            //    {
                            //        //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFile);

                            //        string projectToDate = @WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString + "," + RejectedQtyString;
                            //        dtToSort.Rows.Add(3, projectToDate, Convert.ToDateTime(StartDateTemp.ToString("yyyy-MM-dd") + " " + StartTimeString));

                            //        dataToFile = null;
                            //    }
                            //}

                            WONoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][6]);
                            OpNoInner = Convert.ToString(DTMaindtNew.Rows[toFileLooper][7]);


                            SorM = "M";
                            if ((string)DTMaindtNew.Rows[toFileLooper][1] == "yellow")
                            {
                                SorM = "S";
                            }
                            OpNoInner = OpNoInner.PadLeft(4, '0');
                            WONoInner = WONoInner.PadLeft(12, '0');

                            //WONoOpNoSorM13Spaces = WONoInner + OpNoInner + SorM + Spaces13; //2017-01-27
                            WONoOpNoSorM13Spaces = WONoInner + OpNoInner + Spaces13;

                            //StartDateString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("dd/MM/yyyy");
                            DateTime StartDateTemp1 = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2]));
                            StartDateString = String.Format("{0:00}", StartDateTemp1.Day) + "/" + String.Format("{0:00}", StartDateTemp1.Month) + "/" + StartDateTemp1.Year;

                            StartTimeString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][2])).ToString("HH:mm:ss");

                            //dataToFile = "1,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + StartDateString + "," + StartTimeString;
                            dataToFile = "1,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + StartDateString + "," + StartTimeString;
                            if (dataToFile != null)
                            {

                                string projectToSpace = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces;
                                dtToSort.Rows.Add(1, projectToSpace, Convert.ToDateTime(StartDateTemp1.ToString("yyyy-MM-dd") + " " + StartTimeString));
                                dataToFile = null;
                            }

                            DateTime EndDateTemp = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][3]));
                            //string a = String.Format("{0:00}", DateTime.Now.Day) + "/" + String.Format("{0:00}", DateTime.Now.Month) + "/" + DateTime.Now.Year;
                            EndDateString = String.Format("{0:00}", EndDateTemp.Day) + "/" + String.Format("{0:00}", EndDateTemp.Month) + "/" + EndDateTemp.Year;
                            EndTimeString = (Convert.ToDateTime(DTMaindtNew.Rows[toFileLooper][3])).ToString("HH:mm:ss");

                            codeToEndPrvColor = DTMaindtNew.Rows[toFileLooper][4].ToString();
                            codeToEndPrvColor = codeToEndPrvColor.PadLeft(2, '0');
                            string DeliveredQtyInnerString1 = null, RejectedQtyString1 = null;

                            if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 0)
                            {
                                dataToFile = "2,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor + "," + EndDateString + "," + EndTimeString;

                                if (dataToFile != null)
                                {
                                    string projectToDate = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor;
                                    dtToSort.Rows.Add(2, projectToDate, Convert.ToDateTime(EndDateTemp.ToString("yyyy-MM-dd") + " " + EndTimeString));
                                    dataToFile = null;
                                }
                            }
                            else if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 1)
                            {
                                dataToFile = "2,2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor + "," + EndDateString + "," + EndTimeString;
                                if (dataToFile != null)
                                {
                                    string projectToDate = @"2001-" + dtHMIID.Rows[0][20] + "," + WONoOpNoSorM13Spaces + "," + codeToEndPrvColor;
                                    dtToSort.Rows.Add(2, projectToDate, Convert.ToDateTime(EndDateTemp.ToString("yyyy-MM-dd") + " " + EndTimeString));
                                    dataToFile = null;
                                }
                            }
                            else if (Convert.ToInt32(DTMaindtNew.Rows[toFileLooper][5]) == 2) //Its JobFinish
                            {
                                RejectedQtyString1 = "00";
                                //int deliveredQty = 0;
                                //int.TryParse(Convert.ToString(dtHMIID.Rows[0][12]), out deliveredQty);
                                //int processedQty = 0;
                                //int.TryParse(Convert.ToString(dtHMIID.Rows[0][22]), out processedQty);

                                int deliveredQty = 0, processedQty = 0;
                                if (Convert.ToInt32(dtHMIID.Rows[0][24]) == 0) //its a single workorder
                                {
                                    int.TryParse(Convert.ToString(dtHMIID.Rows[0][12]), out deliveredQty);
                                    int.TryParse(Convert.ToString(dtHMIID.Rows[0][22]), out processedQty);
                                }
                                else //Its a multi WorkOrder
                                {
                                    DataTable dtQtyData = new DataTable();
                                    TATAMysqlConnection mcQtyData = new TATAMysqlConnection();
                                    mcQtyData.open();
                                    String queryQtyData = " SELECT * FROM i_facility_shakti.dbo.tbllivemultiwoselection where HMIID = " + HMIID + "  and WorkOrder = " + WONoInner + " and OperationNo = " + OpNoInner + " ";
                                    SqlDataAdapter daQtyData = new SqlDataAdapter(queryQtyData, mcQtyData.msqlConnection);
                                    daQtyData.Fill(dtQtyData);
                                    mcQtyData.close();

                                    //int.TryParse(Convert.ToString(dtQtyData.Rows[0][5]), out deliveredQty);
                                    int.TryParse(Convert.ToString(dtQtyData.Rows[0][4]), out deliveredQty);
                                    //int.TryParse(Convert.ToString(dtQtyData.Rows[0][22]), out processedQty); // Not for MultiWO
                                }

                                DeliveredQtyInnerString1 = Convert.ToString(deliveredQty + processedQty);

                                DeliveredQtyInnerString1 = DeliveredQtyInnerString1.PadLeft(2, '0');
                                dataToFile = "3," + WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString1 + "," + RejectedQtyString1 + "," + EndDateString + "," + EndTimeString;

                                if (dataToFile != null)
                                {
                                    string projectToDate = @WONoOpNoSorM13Spaces + "," + DeliveredQtyInnerString1 + "," + RejectedQtyString1;
                                    dtToSort.Rows.Add(3, projectToDate, Convert.ToDateTime(EndDateTemp.ToString("yyyy-MM-dd") + " " + EndTimeString));
                                    dataToFile = null;
                                }
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception ej)
                {

                }
                #endregion

                try
                {
                    #region
                    DataView dv1 = new DataView(dtToSort);
                    dv1.Sort = "DateTimeCol";
                    DataTable DTMainFinal = dv1.ToTable();
                    int FileCountLooperFinal = 1;
                    //int FileCountLooperFinal = 1; Set this to Number of files with that name and increment new file by 1.
                    int LoopVar = 0;
                    while (LoopVar == 0)
                    {
                        FileInfo fi = new FileInfo(OutputPath + "-" + FileCountLooperFinal + ".csv");
                        if (fi.Exists)
                        {
                            FileCountLooperFinal++;
                        }
                        else
                        {
                            LoopVar++;
                        }
                    }

                    //int FileCountLooper = 1;
                    sw = new StreamWriter(OutputPath + "-" + FileCountLooperFinal + ".csv", true);
                    //MessageBox.Show("before writing to file DTMainFinal.Rows.Count" + DTMainFinal.Rows.Count);
                    for (int ToFileLooper = 0; ToFileLooper < DTMainFinal.Rows.Count; ToFileLooper++)
                    {
                        //string DateInFormat = (Convert.ToDateTime(DTMainFinal.Rows[ToFileLooper][2])).ToString("dd/MM/yyyy");
                        DateTime dtDate = Convert.ToDateTime(DTMainFinal.Rows[ToFileLooper][2]);
                        string DayFormater = (dtDate.Day.ToString()).PadLeft(2, '0');
                        string MonthFormater = (dtDate.Month.ToString()).PadLeft(2, '0');
                        string DateInFormat = DayFormater + "/" + MonthFormater + "/" + dtDate.Year;
                        string TimeInFormat = (Convert.ToDateTime(DTMainFinal.Rows[ToFileLooper][2])).ToString("HH:mm:ss");

                        string dataToFileFinal = DTMainFinal.Rows[ToFileLooper][0] + "," + DTMainFinal.Rows[ToFileLooper][1] + "," + DateInFormat + "," + TimeInFormat;

                        long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFileFinal);
                        sw.Flush();
                        long FileLength = sw.BaseStream.Length;
                        if ((FileLength + dataOfFileLength) < maxFileLength)
                        {
                            sw.WriteLine(dataToFileFinal);
                        }
                        else
                        {
                            sw.Flush();
                            sw.Close();
                            FileCountLooperFinal++;
                            sw = new StreamWriter(OutputPath + "-" + FileCountLooperFinal + ".csv", true);
                            sw.WriteLine(dataToFileFinal);
                        }
                    }
                    #endregion
                }
                catch (Exception efinal)
                {
                    IntoFile(efinal.ToString());
                }

                sw.Close();
            }
            catch (Exception e)
            {
                IntoFile(e.ToString());
                // MessageBox.Show("Error: " + e);
            }
            finally
            {
                //sw.Close();
            }
        }

        public void IntoFile(string Msg)
        {
            try
            {
                string path1 = AppDomain.CurrentDomain.BaseDirectory;
                string appPath = Application.StartupPath + @"\LogFileOfToSAP.txt";
                using (StreamWriter writer = new StreamWriter(appPath, true)) //true => Append Text
                {
                    writer.WriteLine(System.DateTime.Now + ":  " + Msg);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("IntoFile Error " + e.ToString());
            }
        }

        public void GetDataJF()
        {
            string CorrectedDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //CorrectedDate = "2020-02-14";
            string endTimeString = DateTime.Now.ToString("yyyy-MM-dd") + " " + new TimeSpan(06, 30, 00);
            string startTimeString = CorrectedDate + " " + new TimeSpan(06, 30, 00);

            String OutputPath = db.tbltosapfilepaths.Where(m => m.IsDeleted == 0).Select(m => m.Path).FirstOrDefault();

            if (OutputPath != null)
            {
                OutputPath = @OutputPath + @"\SAPOutPutfile\ZCO11_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                //OutputPath = @OutputPath + @"\NewFormat\ZCO11_" + System.DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss") + ".txt";
            }
            IntoFile("create table");
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Confirmation", typeof(string)); //Added for new template
            dtNew.Columns.Add("WONo", typeof(string));
            dtNew.Columns.Add("OpNo", typeof(int));
            dtNew.Columns.Add("WorkCenter", typeof(string)); //Added for new template
            dtNew.Columns.Add("Plant", typeof(string)); //Added for new template
            dtNew.Columns.Add("Confirmationtype", typeof(int)); //Added for new template
            dtNew.Columns.Add("ClearOpenreservation", typeof(string)); //Added for new template
            dtNew.Columns.Add("Qty", typeof(int));
            dtNew.Columns.Add("UOM", typeof(string)); //Added for new template
            dtNew.Columns.Add("Scrap", typeof(int));
            dtNew.Columns.Add("Reasonvariance", typeof(string)); //Added for new template
            dtNew.Columns.Add("SettingTime", typeof(int));
            dtNew.Columns.Add("MachiningTime", typeof(int));
            dtNew.Columns.Add("LabourTime", typeof(int));
            dtNew.Columns.Add("Activitytype4", typeof(string)); //Added for new template
            dtNew.Columns.Add("Activitytype5", typeof(string)); //Added for new template
            dtNew.Columns.Add("Activitytype6", typeof(string)); //Added for new template
            dtNew.Columns.Add("OperatorID", typeof(string));
            dtNew.Columns.Add("StartDate", typeof(string));
            dtNew.Columns.Add("StartTime", typeof(string));
            dtNew.Columns.Add("EndDate", typeof(string));
            dtNew.Columns.Add("EndTime", typeof(string));
            dtNew.Columns.Add("Postingdate", typeof(string));
            //Added for new template                                                  
            //dtNew.Columns.Add("Reason", typeof(string));                                                  
            //dtNew.Columns.Add("IsEnd", typeof(int)); //Basically : 0 , PF : 1 , JF : 2.

            //string query = "SELECT * FROM i_facility_shakti.dbo.tbllivehmiscreen as h where case when IsMultiWO = 0 then isWorkInProgress = 1 when IsMultiWO = 1 then isWorkInProgress <> 2 end  ;";
            string Confirmation = "";
            string Plant = "SP02";
            int Confirmationtype = '1';
            string ClearOpenreservation = "";
            string UOM = "EA";
            string Reasonvariance = "";
            string Activitytype4 = "";
            string Activitytype5 = "";
            string Activitytype6 = "";
            DateTime Postingdate = DateTime.Now;

            DateTime StartTimeNew = Convert.ToDateTime(startTimeString);
            DateTime EndTimeNew = Convert.ToDateTime(endTimeString);
            var GetJFSingleWO = db.tbllivehmiscreens.Where(m => m.Time > StartTimeNew && m.Time <= EndTimeNew && m.IsMultiWO == 0 && (m.isWorkInProgress == 1 || m.isWorkInProgress == 0) && m.tblmachinedetail.IsNormalWC == 0).ToList();

            //Code for Single JO for Standard Work Centers Job Finish
            foreach (var GetJFSingleWORow in GetJFSingleWO)
            {
                int SettingTime = 0;
                int MachininingTime = 0;
                int LabourTime = 0;
                var macid = Convert.ToString(GetJFSingleWORow.MachineID);
                var machid = Convert.ToInt32(macid);
                string WorkCenter = db.tblmachinedetails.Where(m=> m.MachineID == machid).Select(m => m.MachineDisplayName).FirstOrDefault();
               
                var GetFirstTimeRunWO = db.tbllivehmiscreens.Where(m => m.Work_Order_No == GetJFSingleWORow.Work_Order_No && m.OperationNo == GetJFSingleWORow.OperationNo).ToList();
                var GetFirstTimeRunMultiWO = db.tbllivemultiwoselections.Where(m => m.WorkOrder == GetJFSingleWORow.Work_Order_No && m.OperationNo == GetJFSingleWORow.OperationNo).ToList();
                foreach (var GetFirstTimeRunWORow in GetFirstTimeRunWO)
                {
                    var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                    MachininingTime += GetMachineTime.Count;

                    //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                    //MachininingTime += GetMachineTime.Count;

                    var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.Date && m.EndDateTime <= GetFirstTimeRunWORow.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                    foreach (var GetSettingTimeListRow in GetSettingTimeList)
                    {
                        int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                        SettingTime += SettingTimeDIff;
                    }
                }

                foreach (var GetFirstTimeRunWORow in GetFirstTimeRunMultiWO)
                {
                    if (GetFirstTimeRunWORow.tbllivehmiscreen != null)
                    {
                        var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                        MachininingTime += GetMachineTime.Count;



                        var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndDateTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                        foreach (var GetSettingTimeListRow in GetSettingTimeList)
                        {
                            int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                            SettingTime += SettingTimeDIff;
                        }
                    }
                }

                String StartWODate = null;
                String StartWOTime = null;

                if (GetFirstTimeRunWO.Count > 0)
                {
                    StartWODate = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("ddMMyyyy");
                    StartWOTime = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("HHmmss");
                }

                if (GetFirstTimeRunMultiWO.Count > 0)
                {
                    if (GetFirstTimeRunMultiWO[0].HMIID != null)
                    {
                        int HMIIDSE = (int)GetFirstTimeRunMultiWO[0].HMIID;
                        var GetMultiWOStart = db.tbllivehmiscreens.Where(m => m.HMIID == HMIIDSE).FirstOrDefault();
                        if (GetFirstTimeRunWO.Count > 0)
                        {
                            if (GetMultiWOStart.Date < GetFirstTimeRunWO[0].Date)
                            {
                                StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                                StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                            }
                        }
                        else
                        {
                            StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                            StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                        }
                    }
                }
                if (GetFirstTimeRunMultiWO.Count > 0 || GetFirstTimeRunWO.Count > 0 )
                {
                    string OperatorID = GetJFSingleWORow.OperatorDet;
                    if (OperatorID.Contains(",")){
                        string[] data = OperatorID.Split(',');
                        OperatorID = data[0];
                    }

                     
                   dtNew.Rows.Add(Confirmation,GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo), WorkCenter,Plant,1,ClearOpenreservation,  GetJFSingleWORow.Delivered_Qty, UOM, 0, Reasonvariance, SettingTime, MachininingTime, LabourTime, Activitytype4, Activitytype5, Activitytype6, ((Convert.ToString(OperatorID)).ToString()), StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"), Postingdate.ToString("yyyy-MM-dd"));
                }
                //GetJFSingleWORow.HMIID,
            }

            var GetJFSingleManualWO = db.tbllivehmiscreens.Where(m => m.Time > StartTimeNew && m.Time <= EndTimeNew && m.IsMultiWO == 0 && m.isWorkInProgress == 1 && m.tblmachinedetail.IsNormalWC == 1).ToList();

            //Code for Single JO for Manual Work Centers Job Finish
            foreach (var GetJFSingleWORow in GetJFSingleManualWO)
            {
                int SettingTime = 0;
                int MachininingTime = 0;
                int LabourTime = 0;
                var macid = Convert.ToString(GetJFSingleWORow.MachineID);
                var machid = Convert.ToInt32(macid);
                string WorkCenter = db.tblmachinedetails.Where(m => m.MachineID == machid).Select(m => m.MachineDisplayName).FirstOrDefault();

                var GetFirstTimeRunWO = db.tbllivehmiscreens.Where(m => m.Work_Order_No == GetJFSingleWORow.Work_Order_No && m.OperationNo == GetJFSingleWORow.OperationNo).ToList();
                var GetFirstTimeRunMultiWO = db.tbllivemultiwoselections.Where(m => m.WorkOrder == GetJFSingleWORow.Work_Order_No && m.OperationNo == GetJFSingleWORow.OperationNo).ToList();
                foreach (var GetFirstTimeRunWORow in GetFirstTimeRunWO)
                {
                    var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                    LabourTime += GetMachineTime.Count;

                    //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                    //MachininingTime += GetMachineTime.Count;
                    var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.Date && m.EndDateTime <= GetFirstTimeRunWORow.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                    foreach (var GetSettingTimeListRow in GetSettingTimeList)
                    {
                        int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                        SettingTime += SettingTimeDIff;
                    }
                }

                foreach (var GetFirstTimeRunWORow in GetFirstTimeRunMultiWO)
                {
                    if (GetFirstTimeRunWORow.tbllivehmiscreen != null)
                    {
                        var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                        MachininingTime += GetMachineTime.Count;
                        //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                        //MachininingTime += GetMachineTime.Count;

                        var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndDateTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                        foreach (var GetSettingTimeListRow in GetSettingTimeList)
                        {
                            int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                            SettingTime += SettingTimeDIff;
                        }
                    }
                }

                String StartWODate = null;
                String StartWOTime = null;

                if (GetFirstTimeRunWO.Count > 0)
                {
                    StartWODate = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("ddMMyyyy");
                    StartWOTime = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("HHmmss");
                }
                if (GetFirstTimeRunMultiWO.Count > 0)
                {
                    int HMIIDSE = (int)GetFirstTimeRunMultiWO[0].HMIID;
                    var GetMultiWOStart = db.tbllivehmiscreens.Where(m => m.HMIID == HMIIDSE).FirstOrDefault();
                    if (GetFirstTimeRunWO.Count > 0)
                    {
                        if (GetMultiWOStart.Date < GetFirstTimeRunWO[0].Date)
                        {
                            StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                            StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                        }
                    }
                    else
                    {
                        StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                        StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                    }
                }
                if (GetFirstTimeRunMultiWO.Count > 0 || GetFirstTimeRunWO.Count > 0)
                {
                    string OperatorID = GetJFSingleWORow.OperatorDet;
                    if (OperatorID.Contains(",")){
                        string[] data = OperatorID.Split(',');
                        OperatorID = data[0];
                    }
                    //dtNew.Rows.Add(GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo), GetJFSingleWORow.Target_Qty, 0, SettingTime, MachininingTime, LabourTime, StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"), (((2001).ToString() +Convert.ToString( OperatorID))));
                    dtNew.Rows.Add(Confirmation, GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo), WorkCenter, Plant, 1, ClearOpenreservation, GetJFSingleWORow.Delivered_Qty, UOM, 0, Reasonvariance, SettingTime, MachininingTime, LabourTime, Activitytype4, Activitytype5, Activitytype6, ((Convert.ToString(OperatorID)).ToString()), StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"), Postingdate.ToString("yyyy-MM-dd"));
                }
                //GetJFSingleWORow.HMIID,
            }

            var GetJFMultiWO = db.tbllivehmiscreens.Where(m => m.Time > StartTimeNew && m.Time <= EndTimeNew && m.IsMultiWO == 1 && m.isWorkInProgress != 2).ToList();

            //Code for Multi WO for Standard Work centers Job Finish
            foreach (var GetJFSingleWORow in GetJFMultiWO)
            {
                int SettingTime = 0;
                int MachininingTime = 0;
                int LabourTime = 0;
                var macid = Convert.ToString(GetJFSingleWORow.MachineID);
                var machid = Convert.ToInt32(macid);
                string WorkCenter = db.tblmachinedetails.Where(m => m.MachineID == machid).Select(m => m.MachineDisplayName).FirstOrDefault();

                var GetMultiWOList = db.tbllivemultiwoselections.Where(m => m.HMIID == GetJFSingleWORow.HMIID && m.IsCompleted == 1).ToList();
                foreach (var GetMultiWOListRow in GetMultiWOList)
                {
                    var GetFirstTimeRunWO = db.tbllivehmiscreens.Where(m => m.Work_Order_No == GetMultiWOListRow.WorkOrder && m.OperationNo == GetMultiWOListRow.OperationNo).ToList();
                    var GetFirstTimeRunMultiWO = db.tbllivemultiwoselections.Where(m => m.WorkOrder == GetMultiWOListRow.WorkOrder && m.OperationNo == GetMultiWOListRow.OperationNo).ToList();

                    foreach (var GetFirstTimeRunWORow in GetFirstTimeRunWO)
                    {
                        var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                        MachininingTime += GetMachineTime.Count;

                        //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                        //MachininingTime += GetMachineTime.Count;

                        var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.Date && m.EndDateTime <= GetFirstTimeRunWORow.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                        foreach (var GetSettingTimeListRow in GetSettingTimeList)
                        {
                            int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                            SettingTime += SettingTimeDIff;
                        }
                    }

                    foreach (var GetFirstTimeRunWORow in GetFirstTimeRunMultiWO)
                    {
                        if (GetFirstTimeRunWORow.tbllivehmiscreen != null)
                        {
                            var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                            MachininingTime += GetMachineTime.Count;

                            //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                            //MachininingTime += GetMachineTime.Count;

                            var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndDateTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                            foreach (var GetSettingTimeListRow in GetSettingTimeList)
                            {
                                int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                                SettingTime += SettingTimeDIff;
                            }
                        }
                    }

                    String StartWODate = null;
                    String StartWOTime = null;

                    if (GetFirstTimeRunWO.Count > 0)
                    {
                        StartWODate = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("ddMMyyyy");
                        StartWOTime = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("HHmmss");
                    }
                    if (GetFirstTimeRunMultiWO.Count > 0)
                    {
                        if (GetFirstTimeRunMultiWO[0].HMIID != null)
                        {
                            int HMIIDSE = (int)GetFirstTimeRunMultiWO[0].HMIID;
                            var GetMultiWOStart = db.tbllivehmiscreens.Where(m => m.HMIID == HMIIDSE).ToList().FirstOrDefault();
                            if (GetFirstTimeRunWO.Count > 0)
                            {
                                if (GetMultiWOStart.Date < GetFirstTimeRunWO[0].Date)
                                {
                                    StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                                    StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                                }
                            }
                            else
                            {
                                StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                                StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                            }
                        }
                    }
                    if (GetFirstTimeRunMultiWO.Count > 0 || GetFirstTimeRunWO.Count > 0)
                    {
                        string OperatorID = GetJFSingleWORow.OperatorDet;
                        if (OperatorID.Contains(","))
                        {
                            string[] data = OperatorID.Split(',');
                            OperatorID = data[0];
                        }

                        //dtNew.Rows.Add(GetMultiWOListRow.WorkOrder, Convert.ToInt32(GetMultiWOListRow.OperationNo), GetMultiWOListRow.TargetQty, 0,
                        //SettingTime, MachininingTime, LabourTime, StartWODate, StartWOTime,
                        //Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"),
                        //(((2001).ToString() + Convert.ToString(OperatorID)).ToString()));
                        dtNew.Rows.Add("", GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo),
                        WorkCenter, "SP02", 1, "", GetJFSingleWORow.Delivered_Qty, "EA", 0, "",
                        SettingTime, MachininingTime, LabourTime, "", "", "", (((2001).ToString() + Convert.ToString(OperatorID)).ToString()),
                        StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"),
                        Convert.ToDateTime(DateTime.Now));
                        IntoFile("created successfully");
                    }
                    //GetJFSingleWORow.HMIID, 
                }
            }

            //Push the Data to the Text File
            try
            {
                #region
                DataView dv1 = new DataView(dtNew);
                dv1.Sort = "OpNo";
                DataTable DTMainFinal = dv1.ToTable();
                //int FileCountLooperFinal = 1;
                //int FileCountLooperFinal = 1; Set this to Number of files with that name and increment new file by 1.
                //int LoopVar = 0;
                //while (LoopVar == 0)
                //{
                //    FileInfo fi = new FileInfo(OutputPath + "-" + FileCountLooperFinal + ".txt");
                //    if (fi.Exists)
                //    {
                //        FileCountLooperFinal++;
                //    }
                //    else
                //    {
                //        LoopVar++;
                //    }
                //}

                //int FileCountLooper = 1;

                String headers = "";
                for (int i = 0; i < DTMainFinal.Columns.Count; i++)
                {
                    headers+= DTMainFinal.Columns[i];
                    if (i < DTMainFinal.Columns.Count - 1)
                    {
                        headers+=",";
                    }
                }
                
                for (int ToFileLooper = 0; ToFileLooper < DTMainFinal.Rows.Count; ToFileLooper++)
                {
                    //string dataToFileFinal = DTMainFinal.Rows[ToFileLooper][0] + "," + DTMainFinal.Rows[ToFileLooper][1] + "," + DTMainFinal.Rows[ToFileLooper][2] + "," + DTMainFinal.Rows[ToFileLooper][3]
                    //     + "," + DTMainFinal.Rows[ToFileLooper][4] + "," + DTMainFinal.Rows[ToFileLooper][5] + "," + DTMainFinal.Rows[ToFileLooper][6] +
                    //     "," + DTMainFinal.Rows[ToFileLooper][7] + "," + DTMainFinal.Rows[ToFileLooper][8] + "," + DTMainFinal.Rows[ToFileLooper][9] + ","
                    //     + DTMainFinal.Rows[ToFileLooper][10] + "," + DTMainFinal.Rows[ToFileLooper][11] + ",";
                    string dataToFileFinal = DTMainFinal.Rows[ToFileLooper][0] + "," + DTMainFinal.Rows[ToFileLooper][1] + "," + DTMainFinal.Rows[ToFileLooper][2] + "," + DTMainFinal.Rows[ToFileLooper][3]
                         + "," + DTMainFinal.Rows[ToFileLooper][4] + "," + DTMainFinal.Rows[ToFileLooper][5] + "," + DTMainFinal.Rows[ToFileLooper][6] +
                         "," + DTMainFinal.Rows[ToFileLooper][7] + "," + DTMainFinal.Rows[ToFileLooper][8] + "," + DTMainFinal.Rows[ToFileLooper][9] + ","
                         + DTMainFinal.Rows[ToFileLooper][10] + "," + DTMainFinal.Rows[ToFileLooper][11] + "," + DTMainFinal.Rows[ToFileLooper][12] + "," + DTMainFinal.Rows[ToFileLooper][13] + ","
                         + DTMainFinal.Rows[ToFileLooper][14] + "," + DTMainFinal.Rows[ToFileLooper][15] + "," + DTMainFinal.Rows[ToFileLooper][16] + "," + DTMainFinal.Rows[ToFileLooper][17] + ","
                         + DTMainFinal.Rows[ToFileLooper][18] + "," + DTMainFinal.Rows[ToFileLooper][19] + "," + DTMainFinal.Rows[ToFileLooper][20] + "," + DTMainFinal.Rows[ToFileLooper][21] + ","
                         + DTMainFinal.Rows[ToFileLooper][22] + ",";

                    var nwDetails = db.tblnetworkdetailsforddls.Where(m => m.IsDeleted == 0).FirstOrDefault();
                    string username = nwDetails.UserName;
                    string password = nwDetails.Password;
                    string domainname = nwDetails.DomainName;

                    using (new Impersonator(username, domainname, password))
                    {
                        //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFileFinal);
                        //sw.Flush();
                        using (StreamWriter sw = new StreamWriter(OutputPath, true))
                        {
                            if (ToFileLooper == 0)
                                sw.WriteLine(headers);
                            sw.WriteLine(dataToFileFinal);
                            //sw.Flush();
                        }
                    }
                }
                #endregion
            }
            catch (Exception efinal)
            {
                IntoFile(efinal.ToString());
            }

        }
        public void GetLiveWO()
        {
            try
            {
                string conString = "server = 'localhost' ;userid = 'root' ;Password = 'srks4$' ;database = 'mazakdaq';port = 3306 ;persist security info=False";
                TATAMysqlConnection con = new TATAMysqlConnection();
                using (SqlConnection databaseConnection = new SqlConnection(con.msqlConnection.ToString()))
                {
                    //String DeleteLiveLossQuery = "Truncate table i_facility_shakti.dbo.tbllivelossofentry;";
                    String GetLiveLossQuery = "INSERT into i_facility_shakti.dbo.tbllivelossofentry SELECT * from i_facility_shakti.dbo.tbllivelossofentry as hm where hm.CorrectedDate >= '" + System.DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + "';";


                    SqlCommand cmd = new SqlCommand("SAPLiveWO", databaseConnection);
                    databaseConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    var a = cmd.ExecuteNonQuery();
                    //MessageBox.Show(a.ToString());
                    databaseConnection.Close();

                    SqlCommand cmdLiveLossData = new SqlCommand(GetLiveLossQuery, databaseConnection);
                    databaseConnection.Open();
                    cmdLiveLossData.ExecuteNonQuery();
                    databaseConnection.Close();
                }
            }
            catch (Exception DelSP)
            {
                IntoFile("DeleteDuplicate SP Error: " + DelSP);
            }
        }

        public void SplitWO()
        {
            string CorrectedDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endTimeString = DateTime.Now.ToString("yyyy-MM-dd") + " " + new TimeSpan(06, 30, 00);
            string startTimeString = CorrectedDate + " " + new TimeSpan(06, 30, 00);

            String OutputPath = db.tbltosapfilepaths.Where(m => m.IsDeleted == 0).Select(m => m.Path).FirstOrDefault();

            if (OutputPath != null)
            {
                OutputPath = @OutputPath + @"\SAPOutPutfile\SplitWO\ZCO11_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            }

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Confirmation", typeof(string)); //Added for new template
            dtNew.Columns.Add("WONo", typeof(string));
            dtNew.Columns.Add("OpNo", typeof(int));
            dtNew.Columns.Add("WorkCenter", typeof(string)); //Added for new template
            dtNew.Columns.Add("Plant", typeof(string)); //Added for new template
            dtNew.Columns.Add("Confirmationtype", typeof(int)); //Added for new template
            dtNew.Columns.Add("ClearOpenreservation", typeof(string)); //Added for new template
            dtNew.Columns.Add("Qty", typeof(int));
            dtNew.Columns.Add("UOM", typeof(string)); //Added for new template
            dtNew.Columns.Add("Scrap", typeof(int));
            dtNew.Columns.Add("Reasonvariance", typeof(string)); //Added for new template
            dtNew.Columns.Add("SettingTime", typeof(int));
            dtNew.Columns.Add("MachiningTime", typeof(int));
            dtNew.Columns.Add("LabourTime", typeof(int));
            dtNew.Columns.Add("Activitytype4", typeof(string)); //Added for new template
            dtNew.Columns.Add("Activitytype5", typeof(string)); //Added for new template
            dtNew.Columns.Add("Activitytype6", typeof(string)); //Added for new template
            dtNew.Columns.Add("OperatorID", typeof(string));
            dtNew.Columns.Add("StartDate", typeof(string));
            dtNew.Columns.Add("StartTime", typeof(string));
            dtNew.Columns.Add("EndDate", typeof(string));
            dtNew.Columns.Add("EndTime", typeof(string));
            dtNew.Columns.Add("Postingdate", typeof(string)); //Added for new template

            DateTime StartTimeNew = Convert.ToDateTime(startTimeString);
            DateTime EndTimeNew = Convert.ToDateTime(endTimeString);

            //var GetJFSingleWO = db.tbllivehmiscreens.Where(m => m.Time > StartTimeNew && m.Time <= EndTimeNew && m.SplitWO == "Yes" && m.isWorkInProgress == 0 && m.Status == 1 ).ToList();
            //var GetJFSingleWO = db.tbllivehmiscreens.Where(m => m.Time > StartTimeNew && m.Time <= EndTimeNew && m.SplitWO == "Yes" && m.isWorkInProgress == 0 ).ToList();
            var GetJFSingleWO = db.tbllivehmiscreens.Where(m => m.Time > StartTimeNew && m.Time <= EndTimeNew && m.SplitWO == "Yes" && m.isWorkInProgress == 0 && m.Delivered_Qty != 0).ToList();

            //GetJFSingleWO = GetJFSingleWO.Where(m => m.Delivered_Qty != 0).ToList();
            //Code for Single JO for Standard Work Centers Job Finish
            foreach (var GetJFSingleWORow in GetJFSingleWO)
            {
                int SettingTime = 0;
                int MachininingTime = 0;
                int LabourTime = 0;
                string Confirmation = "";
                var macid = Convert.ToString(GetJFSingleWORow.MachineID);
                var machid = Convert.ToInt32(macid);
                string WorkCenter = db.tblmachinedetails.Where(m => m.MachineID == machid).Select(m => m.MachineDisplayName).FirstOrDefault();
                string Plant = "SP02";
                int Confirmationtype = '1';
                string ClearOpenreservation = "";
                string UOM = "EA";
                string Reasonvariance = "";
                string Activitytype4 = "";
                string Activitytype5 = "";
                string Activitytype6 = "";

                var GetFirstTimeRunWO = db.tbllivehmiscreens.Where(m => m.Work_Order_No == GetJFSingleWORow.Work_Order_No && m.OperationNo == GetJFSingleWORow.OperationNo).ToList();
                var GetFirstTimeRunMultiWO = db.tbllivemultiwoselections.Where(m => m.WorkOrder == GetJFSingleWORow.Work_Order_No && m.OperationNo == GetJFSingleWORow.OperationNo).ToList();
                foreach (var GetFirstTimeRunWORow in GetFirstTimeRunWO)
                {
                   
                    var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                    MachininingTime += GetMachineTime.Count;

                    //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartTime >= GetFirstTimeRunWORow.Date && m.EndTime <= GetFirstTimeRunWORow.Time && m.ColorCode == "green").ToList();
                    //MachininingTime += GetMachineTime.Count;

                    var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.Date && m.EndDateTime <= GetFirstTimeRunWORow.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                    foreach (var GetSettingTimeListRow in GetSettingTimeList)
                    {
                        int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                        SettingTime += SettingTimeDIff;
                    }
                }

                foreach (var GetFirstTimeRunWORow in GetFirstTimeRunMultiWO)
                {
                    if (GetFirstTimeRunWORow.tbllivehmiscreen != null)
                    {

                        var GetMachineTime = db.tbllivedailyprodstatus.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                        MachininingTime += GetMachineTime.Count;


                        //var GetMachineTime = db.tbllivemodedbs.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.ColorCode == "green").ToList();
                        //MachininingTime += GetMachineTime.Count;

                        var GetSettingTimeList = db.tbllivelossofentries.Where(m => m.MachineID == GetFirstTimeRunWORow.tbllivehmiscreen.MachineID && m.StartDateTime >= GetFirstTimeRunWORow.tbllivehmiscreen.Date && m.EndDateTime <= GetFirstTimeRunWORow.tbllivehmiscreen.Time && m.tbllossescode.LossCodesLevel1ID == 1).ToList();

                        foreach (var GetSettingTimeListRow in GetSettingTimeList)
                        {
                            int SettingTimeDIff = (int)Convert.ToDateTime(GetSettingTimeListRow.EndDateTime).Subtract(Convert.ToDateTime(GetSettingTimeListRow.StartDateTime)).TotalMinutes;

                            SettingTime += SettingTimeDIff;
                        }
                    }
                }

                String StartWODate = null;
                String StartWOTime = null;

                if (GetFirstTimeRunWO.Count > 0)
                {
                    StartWODate = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("ddMMyyyy");
                    StartWOTime = Convert.ToDateTime(GetFirstTimeRunWO[0].Date).ToString("HHmmss");
                }

                if (GetFirstTimeRunMultiWO.Count > 0)
                {
                    if (GetFirstTimeRunMultiWO[0].HMIID != null)
                    {
                        int HMIIDSE = (int)GetFirstTimeRunMultiWO[0].HMIID;
                        var GetMultiWOStart = db.tbllivehmiscreens.Where(m => m.HMIID == HMIIDSE).FirstOrDefault();
                        if (GetFirstTimeRunWO.Count > 0)
                        {
                            if (GetMultiWOStart.Date < GetFirstTimeRunWO[0].Date)
                            {
                                StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                                StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                            }
                        }
                        else
                        {
                            StartWODate = Convert.ToDateTime(GetMultiWOStart.Date).ToString("ddMMyyyy");
                            StartWOTime = Convert.ToDateTime(GetMultiWOStart.Date).ToString("HHmmss");
                        }
                    }
                }
                if (GetFirstTimeRunMultiWO.Count > 0 || GetFirstTimeRunWO.Count > 0)
                {
                    string OperatorID = GetJFSingleWORow.OperatorDet;
                    if (OperatorID.Contains(","))
                    {
                        string[] data = OperatorID.Split(',');
                        OperatorID = data[0];
                    }
                    //dtNew.Rows.Add(GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo), GetJFSingleWORow.Target_Qty, 0, SettingTime, MachininingTime, LabourTime, StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"), (((2001).ToString() +Convert.ToString( GetJFSingleWORow.OperatorDet)).ToString()));
                    //dtNew.Rows.Add(GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo), GetJFSingleWORow.Delivered_Qty, 0, SettingTime, MachininingTime, LabourTime, StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"), (((2001).ToString() + Convert.ToString(OperatorID)).ToString()));
                    dtNew.Rows.Add(Confirmation,GetJFSingleWORow.Work_Order_No, Convert.ToInt32(GetJFSingleWORow.OperationNo), WorkCenter, Plant, Confirmationtype, ClearOpenreservation,GetJFSingleWORow.Delivered_Qty, UOM, 0, Reasonvariance, SettingTime, MachininingTime, LabourTime, Activitytype4, Activitytype5, Activitytype6, (((2001).ToString() + Convert.ToString(OperatorID)).ToString()), StartWODate, StartWOTime, Convert.ToDateTime(GetJFSingleWORow.Time).ToString("ddMMyyyy"), Convert.ToDateTime(GetJFSingleWORow.Time).ToString("HHmmss"), Convert.ToDateTime(DateTime.Now));
                }
            }
            
            //Push the Data to the Text File
            try
            {
                #region
                DataView dv1 = new DataView(dtNew);
                dv1.Sort = "OpNo";
                DataTable DTMainFinal = dv1.ToTable();
               
                for (int ToFileLooper = 0; ToFileLooper < DTMainFinal.Rows.Count; ToFileLooper++)
                {
                    string dataToFileFinal = DTMainFinal.Rows[ToFileLooper][0] + "," + DTMainFinal.Rows[ToFileLooper][1] + "," + DTMainFinal.Rows[ToFileLooper][2] + "," + DTMainFinal.Rows[ToFileLooper][3]
                         + "," + DTMainFinal.Rows[ToFileLooper][4] + "," + DTMainFinal.Rows[ToFileLooper][5] + "," + DTMainFinal.Rows[ToFileLooper][6] +
                         "," + DTMainFinal.Rows[ToFileLooper][7] + "," + DTMainFinal.Rows[ToFileLooper][8] + "," + DTMainFinal.Rows[ToFileLooper][9] + ","
                         + DTMainFinal.Rows[ToFileLooper][10] + "," + DTMainFinal.Rows[ToFileLooper][11] + "," + DTMainFinal.Rows[ToFileLooper][12] + "," + DTMainFinal.Rows[ToFileLooper][13] + ","
                         + DTMainFinal.Rows[ToFileLooper][14] + "," + DTMainFinal.Rows[ToFileLooper][15] + "," + DTMainFinal.Rows[ToFileLooper][16] + "," + DTMainFinal.Rows[ToFileLooper][17] + ","
                         + DTMainFinal.Rows[ToFileLooper][18] + "," + DTMainFinal.Rows[ToFileLooper][19] + "," + DTMainFinal.Rows[ToFileLooper][20] + "," + DTMainFinal.Rows[ToFileLooper][21] + ","
                         + DTMainFinal.Rows[ToFileLooper][22] + ",";

                    //using (StreamWriter sw = new StreamWriter(OutputPath, true))
                    //{
                    //    sw.WriteLine(dataToFileFinal);
                    //    //sw.Flush();
                    //}
                    var nwDetails = new tblnetworkdetailsforddl();
                    using (i_facility_shaktiEntities db = new i_facility_shaktiEntities())
                    {
                         nwDetails = db.tblnetworkdetailsforddls.Where(m => m.IsDeleted == 0).FirstOrDefault();
                    }
                    string username = nwDetails.UserName;
                    string password = nwDetails.Password;
                    string domainname = nwDetails.DomainName;

                    using (new Impersonator(username, domainname, password))
                    {
                        //long dataOfFileLength = Encoding.UTF8.GetByteCount(dataToFileFinal);
                        //sw.Flush();
                        using (StreamWriter sw = new StreamWriter(OutputPath, true))
                        {
                            sw.WriteLine(dataToFileFinal);
                            //sw.Flush();
                        }
                    }
                }
                #endregion
            }
            catch (Exception efinal)
            {
                IntoFile(efinal.ToString());
            }
        }
    }
}
