﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalOEEDaily
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class i_facility_shaktiEntities : DbContext
    {
        public i_facility_shaktiEntities()
            : base("name=i_facility_shaktiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<alarm_history_master> alarm_history_master { get; set; }
        public virtual DbSet<configuration_tblpmchecklist> configuration_tblpmchecklist { get; set; }
        public virtual DbSet<configuration_tblpmcheckpoint> configuration_tblpmcheckpoint { get; set; }
        public virtual DbSet<configuration_tblprimitivemaintainancescheduling> configuration_tblprimitivemaintainancescheduling { get; set; }
        public virtual DbSet<configuration_tblsensorgroup> configuration_tblsensorgroup { get; set; }
        public virtual DbSet<configurationtblmachinesensor> configurationtblmachinesensors { get; set; }
        public virtual DbSet<configurationtblsensordatalink> configurationtblsensordatalinks { get; set; }
        public virtual DbSet<configurationtblsensormaster> configurationtblsensormasters { get; set; }
        public virtual DbSet<dashboard_menus> dashboard_menus { get; set; }
        public virtual DbSet<frommail> frommails { get; set; }
        public virtual DbSet<Graph_Data> Graph_Data { get; set; }
        public virtual DbSet<handle_no_ping> handle_no_ping { get; set; }
        public virtual DbSet<jobcard_details> jobcard_details { get; set; }
        public virtual DbSet<LossDetail> LossDetails { get; set; }
        public virtual DbSet<mailmaster> mailmasters { get; set; }
        public virtual DbSet<menu_styles> menu_styles { get; set; }
        public virtual DbSet<menu> menus { get; set; }
        public virtual DbSet<message_code_master> message_code_master { get; set; }
        public virtual DbSet<message_history_master> message_history_master { get; set; }
        public virtual DbSet<monthdata> monthdatas { get; set; }
        public virtual DbSet<operationlog> operationlogs { get; set; }
        public virtual DbSet<parameter> parameters { get; set; }
        public virtual DbSet<parameters_master> parameters_master { get; set; }
        public virtual DbSet<pcb_details> pcb_details { get; set; }
        public virtual DbSet<pcb_parameters> pcb_parameters { get; set; }
        public virtual DbSet<pcbdaq> pcbdaqs { get; set; }
        public virtual DbSet<pcbdaqin_tbl> pcbdaqin_tbl { get; set; }
        public virtual DbSet<pcbdps_master> pcbdps_master { get; set; }
        public virtual DbSet<program_master> program_master { get; set; }
        public virtual DbSet<QualtiyRaw_piweb> QualtiyRaw_piweb { get; set; }
        public virtual DbSet<sharathtable> sharathtables { get; set; }
        public virtual DbSet<shift_master> shift_master { get; set; }
        public virtual DbSet<sidebar_menus> sidebar_menus { get; set; }
        public virtual DbSet<smtpdetail> smtpdetails { get; set; }
        public virtual DbSet<tbl_autoreport_log> tbl_autoreport_log { get; set; }
        public virtual DbSet<tbl_autoreportbasedon> tbl_autoreportbasedon { get; set; }
        public virtual DbSet<tbl_autoreportsetting> tbl_autoreportsetting { get; set; }
        public virtual DbSet<tbl_autoreporttime> tbl_autoreporttime { get; set; }
        public virtual DbSet<tbl_axisdet> tbl_axisdet { get; set; }
        public virtual DbSet<tbl_axisdetails1> tbl_axisdetails1 { get; set; }
        public virtual DbSet<tbl_axisdetails2> tbl_axisdetails2 { get; set; }
        public virtual DbSet<tbl_CycleTimeAnalysis> tbl_CycleTimeAnalysis { get; set; }
        public virtual DbSet<tbl_deletedProgDet> tbl_deletedProgDet { get; set; }
        public virtual DbSet<tbl_genericfilepath> tbl_genericfilepath { get; set; }
        public virtual DbSet<tbl_livecbmparameters> tbl_livecbmparameters { get; set; }
        public virtual DbSet<tbl_machinestatusrealtime> tbl_machinestatusrealtime { get; set; }
        public virtual DbSet<tbl_multiwoselection> tbl_multiwoselection { get; set; }
        public virtual DbSet<tbl_OEEDetails> tbl_OEEDetails { get; set; }
        public virtual DbSet<tbl_oeereportasdivision> tbl_oeereportasdivision { get; set; }
        public virtual DbSet<tbl_ProdAndonDisp> tbl_ProdAndonDisp { get; set; }
        public virtual DbSet<tbl_ProdManMachine> tbl_ProdManMachine { get; set; }
        public virtual DbSet<tbl_ProdOrderLosses> tbl_ProdOrderLosses { get; set; }
        public virtual DbSet<tbl_reportmaster> tbl_reportmaster { get; set; }
        public virtual DbSet<tbl_SaveNCProgVer> tbl_SaveNCProgVer { get; set; }
        public virtual DbSet<tbl_servodetails> tbl_servodetails { get; set; }
        public virtual DbSet<tbl_UtilReport> tbl_UtilReport { get; set; }
        public virtual DbSet<tblactivitylog> tblactivitylogs { get; set; }
        public virtual DbSet<tblalarmdetail> tblalarmdetails { get; set; }
        public virtual DbSet<tblAndonDispDet> tblAndonDispDets { get; set; }
        public virtual DbSet<tblAndonImageTextScheduledDisplay> tblAndonImageTextScheduledDisplays { get; set; }
        public virtual DbSet<tblapp_paths> tblapp_paths { get; set; }
        public virtual DbSet<tblatccounter> tblatccounters { get; set; }
        public virtual DbSet<tblBachUplossofentry> tblBachUplossofentries { get; set; }
        public virtual DbSet<tblBackupoeedashboardvariable> tblBackupoeedashboardvariables { get; set; }
        public virtual DbSet<tblbottelneck> tblbottelnecks { get; set; }
        public virtual DbSet<tblBreadownAndLossReport> tblBreadownAndLossReports { get; set; }
        public virtual DbSet<tblbreakdown> tblbreakdowns { get; set; }
        public virtual DbSet<tblBreakdowncode> tblBreakdowncodes { get; set; }
        public virtual DbSet<tblBreakDownTickect> tblBreakDownTickects { get; set; }
        public virtual DbSet<tblcell> tblcells { get; set; }
        public virtual DbSet<tblcellpart> tblcellparts { get; set; }
        public virtual DbSet<tblcustomer> tblcustomers { get; set; }
        public virtual DbSet<tbldailyprodstatu> tbldailyprodstatus { get; set; }
        public virtual DbSet<tbldaytiming> tbldaytimings { get; set; }
        public virtual DbSet<tblddl> tblddls { get; set; }
        public virtual DbSet<tblDDLStatu> tblDDLStatus { get; set; }
        public virtual DbSet<tbldowntimecategory> tbldowntimecategories { get; set; }
        public virtual DbSet<tbldowntimedetail> tbldowntimedetails { get; set; }
        public virtual DbSet<tblemailescalation> tblemailescalations { get; set; }
        public virtual DbSet<tblendcode> tblendcodes { get; set; }
        public virtual DbSet<tblescalationlog> tblescalationlogs { get; set; }
        public virtual DbSet<tblgenericworkcode> tblgenericworkcodes { get; set; }
        public virtual DbSet<tblgenericworkentry> tblgenericworkentries { get; set; }
        public virtual DbSet<tblhistpm> tblhistpms { get; set; }
        public virtual DbSet<tblhmiscreen> tblhmiscreens { get; set; }
        public virtual DbSet<tblholdcode> tblholdcodes { get; set; }
        public virtual DbSet<tblHolidayManagment> tblHolidayManagments { get; set; }
        public virtual DbSet<tblholiday> tblholidays { get; set; }
        public virtual DbSet<tblHolidayTypeMaster> tblHolidayTypeMasters { get; set; }
        public virtual DbSet<tbllivedailyprodstatu> tbllivedailyprodstatus { get; set; }
        public virtual DbSet<tbllivehmiscreen> tbllivehmiscreens { get; set; }
        public virtual DbSet<tbllivelossofentry> tbllivelossofentries { get; set; }
        public virtual DbSet<tbllivemanuallossofentry> tbllivemanuallossofentries { get; set; }
        public virtual DbSet<tbllivemode> tbllivemodes { get; set; }
        public virtual DbSet<tbllivemultiwoselection> tbllivemultiwoselections { get; set; }
        public virtual DbSet<tbllossescode> tbllossescodes { get; set; }
        public virtual DbSet<tbllossofentry> tbllossofentries { get; set; }
        public virtual DbSet<tblmachineallocation> tblmachineallocations { get; set; }
        public virtual DbSet<tblmachineaxisdetail> tblmachineaxisdetails { get; set; }
        public virtual DbSet<tblmachinecategory> tblmachinecategories { get; set; }
        public virtual DbSet<tblmachinedetail> tblmachinedetails { get; set; }
        public virtual DbSet<tblmailid> tblmailids { get; set; }
        public virtual DbSet<tblmaintainanceProdBrDetail> tblmaintainanceProdBrDetails { get; set; }
        public virtual DbSet<tblmanuallossofentry> tblmanuallossofentries { get; set; }
        public virtual DbSet<tblmasterparts_st_sw> tblmasterparts_st_sw { get; set; }
        public virtual DbSet<tblmimic> tblmimics { get; set; }
        public virtual DbSet<tblmode> tblmodes { get; set; }
        public virtual DbSet<tblmodetemp> tblmodetemps { get; set; }
        public virtual DbSet<tblmodulehelper> tblmodulehelpers { get; set; }
        public virtual DbSet<tblmodulemaster> tblmodulemasters { get; set; }
        public virtual DbSet<tblmodule> tblmodules { get; set; }
        public virtual DbSet<tblmultipleworkorder> tblmultipleworkorders { get; set; }
        public virtual DbSet<tblNcProgramTransferMain> tblNcProgramTransferMains { get; set; }
        public virtual DbSet<tblnetworkdetailsforddl> tblnetworkdetailsforddls { get; set; }
        public virtual DbSet<tbloeedashboardfinalvariable> tbloeedashboardfinalvariables { get; set; }
        public virtual DbSet<tbloeedashboardvariable> tbloeedashboardvariables { get; set; }
        public virtual DbSet<tbloeedashboardvariablestoday> tbloeedashboardvariablestodays { get; set; }
        public virtual DbSet<tblOperatorDashboard> tblOperatorDashboards { get; set; }
        public virtual DbSet<tbloperatordetail> tbloperatordetails { get; set; }
        public virtual DbSet<tblOperatorHeader> tblOperatorHeaders { get; set; }
        public virtual DbSet<tblOperatorLoginDetail> tblOperatorLoginDetails { get; set; }
        public virtual DbSet<tblOperatorMachineDetail> tblOperatorMachineDetails { get; set; }
        public virtual DbSet<tblpartlearningreport> tblpartlearningreports { get; set; }
        public virtual DbSet<tblpart> tblparts { get; set; }
        public virtual DbSet<tblpartscountandcutting> tblpartscountandcuttings { get; set; }
        public virtual DbSet<tblpartwisesp> tblpartwisesps { get; set; }
        public virtual DbSet<tblPiweb_AdjOEE> tblPiweb_AdjOEE { get; set; }
        public virtual DbSet<tblPiweb_Loss> tblPiweb_Loss { get; set; }
        public virtual DbSet<tblpiweb_OEE> tblpiweb_OEE { get; set; }
        public virtual DbSet<tblpiweb_util> tblpiweb_util { get; set; }
        public virtual DbSet<tblplannedbreak> tblplannedbreaks { get; set; }
        public virtual DbSet<tblplant> tblplants { get; set; }
        public virtual DbSet<tblpmsdetail> tblpmsdetails { get; set; }
        public virtual DbSet<TblPMSNotification_Master> TblPMSNotification_Master { get; set; }
        public virtual DbSet<tblPrecentColour> tblPrecentColours { get; set; }
        public virtual DbSet<tblPresentTool> tblPresentTools { get; set; }
        public virtual DbSet<tblpriorityalarm> tblpriorityalarms { get; set; }
        public virtual DbSet<tblProdPlanMaster> tblProdPlanMasters { get; set; }
        public virtual DbSet<tblprogramtransferhistory> tblprogramtransferhistories { get; set; }
        public virtual DbSet<tblQuality_Piweb> tblQuality_Piweb { get; set; }
        public virtual DbSet<tblrejectqty> tblrejectqties { get; set; }
        public virtual DbSet<tblrejectreason> tblrejectreasons { get; set; }
        public virtual DbSet<tblreportholder> tblreportholders { get; set; }
        public virtual DbSet<tblReworkReason> tblReworkReasons { get; set; }
        public virtual DbSet<tblrolemodulelink> tblrolemodulelinks { get; set; }
        public virtual DbSet<tblrole> tblroles { get; set; }
        public virtual DbSet<tblSAPInput> tblSAPInputs { get; set; }
        public virtual DbSet<tblsendermailid> tblsendermailids { get; set; }
        public virtual DbSet<tblSetupMaint> tblSetupMaints { get; set; }
        public virtual DbSet<tblshift_breaks> tblshift_breaks { get; set; }
        public virtual DbSet<tblshift_mstr> tblshift_mstr { get; set; }
        public virtual DbSet<tblshiftdetail> tblshiftdetails { get; set; }
        public virtual DbSet<tblshiftdetails_machinewise> tblshiftdetails_machinewise { get; set; }
        public virtual DbSet<tblshiftmethod> tblshiftmethods { get; set; }
        public virtual DbSet<tblshiftplanner> tblshiftplanners { get; set; }
        public virtual DbSet<tblShiftUtilReport> tblShiftUtilReports { get; set; }
        public virtual DbSet<tblshop> tblshops { get; set; }
        public virtual DbSet<tblStdToolLife> tblStdToolLives { get; set; }
        public virtual DbSet<tblTcfApprovedMaster> tblTcfApprovedMasters { get; set; }
        public virtual DbSet<tbltcflossofentry> tbltcflossofentries { get; set; }
        public virtual DbSet<tblTcfModule> tblTcfModules { get; set; }
        public virtual DbSet<tblTempLiveLossOfEntry> tblTempLiveLossOfEntries { get; set; }
        public virtual DbSet<tblToolCounter> tblToolCounters { get; set; }
        public virtual DbSet<tbltoollifeoperator> tbltoollifeoperators { get; set; }
        public virtual DbSet<tbltosapfilepath> tbltosapfilepaths { get; set; }
        public virtual DbSet<tbltosapshopname> tbltosapshopnames { get; set; }
        public virtual DbSet<tblunit> tblunits { get; set; }
        public virtual DbSet<tbluser> tblusers { get; set; }
        public virtual DbSet<tblwolossess> tblwolossesses { get; set; }
        public virtual DbSet<tblwolossessBackup> tblwolossessBackups { get; set; }
        public virtual DbSet<tblworeport> tblworeports { get; set; }
        public virtual DbSet<tblworeportBackup> tblworeportBackups { get; set; }
        public virtual DbSet<tblworkorderentry> tblworkorderentries { get; set; }
        public virtual DbSet<Temp_tblmode> Temp_tblmode { get; set; }
        public virtual DbSet<user_menus> user_menus { get; set; }
        public virtual DbSet<weekdata> weekdatas { get; set; }
        public virtual DbSet<tbl_livecbmdetails> tbl_livecbmdetails { get; set; }
        public virtual DbSet<tbl_livetblsensorvalue> tbl_livetblsensorvalue { get; set; }
        public virtual DbSet<tblAndonDisplayRotate> tblAndonDisplayRotates { get; set; }
    }
}
