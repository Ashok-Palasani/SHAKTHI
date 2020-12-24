using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAS.DBModels
{
    public partial class i_facility_shaktiContext : DbContext
    {
        public i_facility_shaktiContext()
        {
        }

        public i_facility_shaktiContext(DbContextOptions<i_facility_shaktiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlarmHistoryMaster> AlarmHistoryMaster { get; set; }
        public virtual DbSet<ConfigurationTblpmchecklist> ConfigurationTblpmchecklist { get; set; }
        public virtual DbSet<ConfigurationTblpmcheckpoint> ConfigurationTblpmcheckpoint { get; set; }
        public virtual DbSet<ConfigurationTblprimitivemaintainancescheduling> ConfigurationTblprimitivemaintainancescheduling { get; set; }
        public virtual DbSet<ConfigurationTblsensorgroup> ConfigurationTblsensorgroup { get; set; }
        public virtual DbSet<Configurationtblmachinesensor> Configurationtblmachinesensor { get; set; }
        public virtual DbSet<Configurationtblsensordatalink> Configurationtblsensordatalink { get; set; }
        public virtual DbSet<Configurationtblsensormaster> Configurationtblsensormaster { get; set; }
        public virtual DbSet<Frommail> Frommail { get; set; }
        public virtual DbSet<GraphData> GraphData { get; set; }
        public virtual DbSet<HandleNoPing> HandleNoPing { get; set; }
        public virtual DbSet<JobcardDetails> JobcardDetails { get; set; }
        public virtual DbSet<LossDetails> LossDetails { get; set; }
        public virtual DbSet<Mailmaster> Mailmaster { get; set; }
        public virtual DbSet<MessageCodeMaster> MessageCodeMaster { get; set; }
        public virtual DbSet<MessageHistoryMaster> MessageHistoryMaster { get; set; }
        public virtual DbSet<Monthdata> Monthdata { get; set; }
        public virtual DbSet<Operationlog> Operationlog { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<ParametersMaster> ParametersMaster { get; set; }
        public virtual DbSet<PcbDetails> PcbDetails { get; set; }
        public virtual DbSet<PcbParameters> PcbParameters { get; set; }
        public virtual DbSet<Pcbdaq> Pcbdaq { get; set; }
        public virtual DbSet<PcbdaqinTbl> PcbdaqinTbl { get; set; }
        public virtual DbSet<PcbdpsMaster> PcbdpsMaster { get; set; }
        public virtual DbSet<ProgramMaster> ProgramMaster { get; set; }
        public virtual DbSet<QualtiyRawPiweb> QualtiyRawPiweb { get; set; }
        public virtual DbSet<Sharathtable> Sharathtable { get; set; }
        public virtual DbSet<ShiftMaster> ShiftMaster { get; set; }
        public virtual DbSet<Smtpdetails> Smtpdetails { get; set; }
        public virtual DbSet<TblAndonDispDet> TblAndonDispDet { get; set; }
        public virtual DbSet<TblAndonImageTextScheduledDisplay> TblAndonImageTextScheduledDisplay { get; set; }
        public virtual DbSet<TblAutoreportLog> TblAutoreportLog { get; set; }
        public virtual DbSet<TblAutoreportbasedon> TblAutoreportbasedon { get; set; }
        public virtual DbSet<TblAutoreportsetting> TblAutoreportsetting { get; set; }
        public virtual DbSet<TblAutoreporttime> TblAutoreporttime { get; set; }
        public virtual DbSet<TblAxisdet> TblAxisdet { get; set; }
        public virtual DbSet<TblAxisdetails1> TblAxisdetails1 { get; set; }
        public virtual DbSet<TblAxisdetails2> TblAxisdetails2 { get; set; }
        public virtual DbSet<TblBachUplossofentry> TblBachUplossofentry { get; set; }
        public virtual DbSet<TblBackupoeedashboardvariables> TblBackupoeedashboardvariables { get; set; }
        public virtual DbSet<TblBreadownAndLossReport> TblBreadownAndLossReport { get; set; }
        public virtual DbSet<TblBreakDownTickect> TblBreakDownTickect { get; set; }
        public virtual DbSet<TblBreakdowncodes> TblBreakdowncodes { get; set; }
        public virtual DbSet<TblCycleTimeAnalysis> TblCycleTimeAnalysis { get; set; }
        public virtual DbSet<TblDdlstatus> TblDdlstatus { get; set; }
        public virtual DbSet<TblDeletedProgDet> TblDeletedProgDet { get; set; }
        public virtual DbSet<TblGenericfilepath> TblGenericfilepath { get; set; }
        public virtual DbSet<TblHolidayManagment> TblHolidayManagment { get; set; }
        public virtual DbSet<TblHolidayTypeMasters> TblHolidayTypeMasters { get; set; }
        public virtual DbSet<TblLivecbmparameters> TblLivecbmparameters { get; set; }
        public virtual DbSet<TblMachinestatusrealtime> TblMachinestatusrealtime { get; set; }
        public virtual DbSet<TblMultiwoselection> TblMultiwoselection { get; set; }
        public virtual DbSet<TblNcProgramTransferMain> TblNcProgramTransferMain { get; set; }
        public virtual DbSet<TblOeedetails> TblOeedetails { get; set; }
        public virtual DbSet<TblOeereportasdivision> TblOeereportasdivision { get; set; }
        public virtual DbSet<TblOperatorDashboard> TblOperatorDashboard { get; set; }
        public virtual DbSet<TblOperatorHeader> TblOperatorHeader { get; set; }
        public virtual DbSet<TblOperatorLoginDetails> TblOperatorLoginDetails { get; set; }
        public virtual DbSet<TblOperatorMachineDetails> TblOperatorMachineDetails { get; set; }
        public virtual DbSet<TblPiwebAdjOee> TblPiwebAdjOee { get; set; }
        public virtual DbSet<TblPiwebLoss> TblPiwebLoss { get; set; }
        public virtual DbSet<TblPmsnotificationMaster> TblPmsnotificationMaster { get; set; }
        public virtual DbSet<TblPrecentColour> TblPrecentColour { get; set; }
        public virtual DbSet<TblPresentTool> TblPresentTool { get; set; }
        public virtual DbSet<TblProdAndonDisp> TblProdAndonDisp { get; set; }
        public virtual DbSet<TblProdManMachine> TblProdManMachine { get; set; }
        public virtual DbSet<TblProdOrderLosses> TblProdOrderLosses { get; set; }
        public virtual DbSet<TblProdPlanMasters> TblProdPlanMasters { get; set; }
        public virtual DbSet<TblQualityPiweb> TblQualityPiweb { get; set; }
        public virtual DbSet<TblReportmaster> TblReportmaster { get; set; }
        public virtual DbSet<TblReworkReason> TblReworkReason { get; set; }
        public virtual DbSet<TblSapinput> TblSapinput { get; set; }
        public virtual DbSet<TblSaveNcprogVer> TblSaveNcprogVer { get; set; }
        public virtual DbSet<TblServodetails> TblServodetails { get; set; }
        public virtual DbSet<TblSetupMaint> TblSetupMaint { get; set; }
        public virtual DbSet<TblShiftUtilReport> TblShiftUtilReport { get; set; }
        public virtual DbSet<TblStdToolLife> TblStdToolLife { get; set; }
        public virtual DbSet<TblTcfApprovedMaster> TblTcfApprovedMaster { get; set; }
        public virtual DbSet<TblTcfModule> TblTcfModule { get; set; }
        public virtual DbSet<TblTempLiveLossOfEntry> TblTempLiveLossOfEntry { get; set; }
        public virtual DbSet<TblToolCounter> TblToolCounter { get; set; }
        public virtual DbSet<TblUtilReport> TblUtilReport { get; set; }
        public virtual DbSet<Tblactivitylog> Tblactivitylog { get; set; }
        public virtual DbSet<Tblalarmdetails> Tblalarmdetails { get; set; }
        public virtual DbSet<TblappPaths> TblappPaths { get; set; }
        public virtual DbSet<Tblatccounter> Tblatccounter { get; set; }
        public virtual DbSet<Tblbottelneck> Tblbottelneck { get; set; }
        public virtual DbSet<Tblbreakdown> Tblbreakdown { get; set; }
        public virtual DbSet<Tblcell> Tblcell { get; set; }
        public virtual DbSet<Tblcellpart> Tblcellpart { get; set; }
        public virtual DbSet<Tblcustomer> Tblcustomer { get; set; }
        public virtual DbSet<Tbldailyprodstatus> Tbldailyprodstatus { get; set; }
        public virtual DbSet<Tbldaytiming> Tbldaytiming { get; set; }
        public virtual DbSet<Tblddl> Tblddl { get; set; }
        public virtual DbSet<Tbldowntimecategory> Tbldowntimecategory { get; set; }
        public virtual DbSet<Tbldowntimedetails> Tbldowntimedetails { get; set; }
        public virtual DbSet<Tblemailescalation> Tblemailescalation { get; set; }
        public virtual DbSet<Tblendcodes> Tblendcodes { get; set; }
        public virtual DbSet<Tblescalationlog> Tblescalationlog { get; set; }
        public virtual DbSet<Tblgenericworkcodes> Tblgenericworkcodes { get; set; }
        public virtual DbSet<Tblgenericworkentry> Tblgenericworkentry { get; set; }
        public virtual DbSet<Tblhistpms> Tblhistpms { get; set; }
        public virtual DbSet<Tblhmiscreen> Tblhmiscreen { get; set; }
        public virtual DbSet<Tblholdcodes> Tblholdcodes { get; set; }
        public virtual DbSet<Tblholidays> Tblholidays { get; set; }
        public virtual DbSet<Tbllivedailyprodstatus> Tbllivedailyprodstatus { get; set; }
        public virtual DbSet<Tbllivehmiscreen> Tbllivehmiscreen { get; set; }
        public virtual DbSet<Tbllivelossofentry> Tbllivelossofentry { get; set; }
        public virtual DbSet<Tbllivemanuallossofentry> Tbllivemanuallossofentry { get; set; }
        public virtual DbSet<Tbllivemode> Tbllivemode { get; set; }
        public virtual DbSet<Tbllivemultiwoselection> Tbllivemultiwoselection { get; set; }
        public virtual DbSet<Tbllossescodes> Tbllossescodes { get; set; }
        public virtual DbSet<Tbllossofentry> Tbllossofentry { get; set; }
        public virtual DbSet<Tblmachineallocation> Tblmachineallocation { get; set; }
        public virtual DbSet<Tblmachineaxisdetails> Tblmachineaxisdetails { get; set; }
        public virtual DbSet<Tblmachinecategory> Tblmachinecategory { get; set; }
        public virtual DbSet<Tblmachinedetails> Tblmachinedetails { get; set; }
        public virtual DbSet<Tblmailids> Tblmailids { get; set; }
        public virtual DbSet<TblmaintainanceProdBrDetails> TblmaintainanceProdBrDetails { get; set; }
        public virtual DbSet<Tblmanuallossofentry> Tblmanuallossofentry { get; set; }
        public virtual DbSet<TblmasterpartsStSw> TblmasterpartsStSw { get; set; }
        public virtual DbSet<Tblmimics> Tblmimics { get; set; }
        public virtual DbSet<Tblmode> Tblmode { get; set; }
        public virtual DbSet<Tblmodetemp> Tblmodetemp { get; set; }
        public virtual DbSet<Tblmodulehelper> Tblmodulehelper { get; set; }
        public virtual DbSet<Tblmodulemaster> Tblmodulemaster { get; set; }
        public virtual DbSet<Tblmodules> Tblmodules { get; set; }
        public virtual DbSet<Tblmultipleworkorder> Tblmultipleworkorder { get; set; }
        public virtual DbSet<Tblnetworkdetailsforddl> Tblnetworkdetailsforddl { get; set; }
        public virtual DbSet<Tbloeedashboardfinalvariables> Tbloeedashboardfinalvariables { get; set; }
        public virtual DbSet<Tbloeedashboardvariables> Tbloeedashboardvariables { get; set; }
        public virtual DbSet<Tbloeedashboardvariablestoday> Tbloeedashboardvariablestoday { get; set; }
        public virtual DbSet<Tbloperatordetails> Tbloperatordetails { get; set; }
        public virtual DbSet<Tblpartlearningreport> Tblpartlearningreport { get; set; }
        public virtual DbSet<Tblparts> Tblparts { get; set; }
        public virtual DbSet<Tblpartscountandcutting> Tblpartscountandcutting { get; set; }
        public virtual DbSet<Tblpartwisesp> Tblpartwisesp { get; set; }
        public virtual DbSet<TblpiwebOee> TblpiwebOee { get; set; }
        public virtual DbSet<TblpiwebUtil> TblpiwebUtil { get; set; }
        public virtual DbSet<Tblplannedbreak> Tblplannedbreak { get; set; }
        public virtual DbSet<Tblplant> Tblplant { get; set; }
        public virtual DbSet<Tblpmsdetails> Tblpmsdetails { get; set; }
        public virtual DbSet<Tblpriorityalarms> Tblpriorityalarms { get; set; }
        public virtual DbSet<Tblprogramtransferhistory> Tblprogramtransferhistory { get; set; }
        public virtual DbSet<Tblrejectqty> Tblrejectqty { get; set; }
        public virtual DbSet<Tblrejectreason> Tblrejectreason { get; set; }
        public virtual DbSet<Tblreportholder> Tblreportholder { get; set; }
        public virtual DbSet<Tblrolemodulelink> Tblrolemodulelink { get; set; }
        public virtual DbSet<Tblroles> Tblroles { get; set; }
        public virtual DbSet<Tblsendermailid> Tblsendermailid { get; set; }
        public virtual DbSet<TblshiftBreaks> TblshiftBreaks { get; set; }
        public virtual DbSet<TblshiftMstr> TblshiftMstr { get; set; }
        public virtual DbSet<Tblshiftdetails> Tblshiftdetails { get; set; }
        public virtual DbSet<TblshiftdetailsMachinewise> TblshiftdetailsMachinewise { get; set; }
        public virtual DbSet<Tblshiftmethod> Tblshiftmethod { get; set; }
        public virtual DbSet<Tblshiftplanner> Tblshiftplanner { get; set; }
        public virtual DbSet<Tblshop> Tblshop { get; set; }
        public virtual DbSet<Tbltcflossofentry> Tbltcflossofentry { get; set; }
        public virtual DbSet<Tbltoollifeoperator> Tbltoollifeoperator { get; set; }
        public virtual DbSet<Tbltosapfilepath> Tbltosapfilepath { get; set; }
        public virtual DbSet<Tbltosapshopnames> Tbltosapshopnames { get; set; }
        public virtual DbSet<Tblunit> Tblunit { get; set; }
        public virtual DbSet<Tblusers> Tblusers { get; set; }
        public virtual DbSet<Tblwolossess> Tblwolossess { get; set; }
        public virtual DbSet<TblwolossessBackup> TblwolossessBackup { get; set; }
        public virtual DbSet<Tblworeport> Tblworeport { get; set; }
        public virtual DbSet<TblworeportBackup> TblworeportBackup { get; set; }
        public virtual DbSet<Tblworkorderentry> Tblworkorderentry { get; set; }
        public virtual DbSet<TempTblmode> TempTblmode { get; set; }
        public virtual DbSet<Weekdata> Weekdata { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
              
              //  optionsBuilder.UseSqlServer("Server=DESKTOP-72HGDFG\\SQLDEV17013;Database=i_facility_shakti;user id=sa;password=srks4$;Trusted_Connection=True;");

                optionsBuilder.UseSqlServer("Server=tcp:192.168.0.2,1433;Database=i_facility_shakti;user id=sa;password=srks4$shakti;Trusted_Connection=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AlarmHistoryMaster>(entity =>
            {
                entity.HasKey(e => e.AlarmId)
                    .HasName("PK_alarm_history_master_AlarmID");

                entity.ToTable("alarm_history_master");

                entity.Property(e => e.AlarmId).HasColumnName("AlarmID");

                entity.Property(e => e.AbsPos)
                    .HasColumnName("Abs_Pos")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AlarmDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.AlarmMessage).IsRequired();

                entity.Property(e => e.AlarmNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AxisNo)
                    .HasColumnName("Axis_No")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AxisNum)
                    .HasColumnName("Axis_Num")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.DetailCode1).HasMaxLength(45);

                entity.Property(e => e.DetailCode2).HasMaxLength(45);

                entity.Property(e => e.DetailCode3).HasMaxLength(45);

                entity.Property(e => e.ErrorNum).HasMaxLength(45);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.InterferedPart).HasMaxLength(45);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.Shift).HasMaxLength(4);

                entity.Property(e => e.Status).HasMaxLength(45);

                entity.Property(e => e.SystemHead).HasMaxLength(10);
            });

            modelBuilder.Entity<ConfigurationTblpmchecklist>(entity =>
            {
                entity.HasKey(e => e.Pmcid)
                    .HasName("PK__configur__23BAE1135C4B4EC1");

                entity.ToTable("configuration_tblpmchecklist");

                entity.Property(e => e.Pmcid).HasColumnName("pmcid");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CheckList)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Frequency)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('null')");

                entity.Property(e => e.How)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Isdeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.PmcpId).HasColumnName("pmcpID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Value)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('null')");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.ConfigurationTblpmchecklist)
                    .HasForeignKey(d => d.CellId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cellID_tblcell_tblpmchecklist");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ConfigurationTblpmchecklist)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("plantID_FK");

                entity.HasOne(d => d.Pmcp)
                    .WithMany(p => p.ConfigurationTblpmchecklist)
                    .HasForeignKey(d => d.PmcpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pmcpID_FK");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ConfigurationTblpmchecklist)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shopID_FK");
            });

            modelBuilder.Entity<ConfigurationTblpmcheckpoint>(entity =>
            {
                entity.HasKey(e => e.PmcpId)
                    .HasName("PK__configur__77319ED6DD8EDBC7");

                entity.ToTable("configuration_tblpmcheckpoint");

                entity.Property(e => e.PmcpId).HasColumnName("pmcpID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CheckList)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Frequency)
                    .IsRequired()
                    .HasColumnName("frequency")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.How)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Isdeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.TypeofCheckpoint)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('null')");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.ConfigurationTblpmcheckpoint)
                    .HasForeignKey(d => d.CellId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cellID");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ConfigurationTblpmcheckpoint)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("plantid_tblplant");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ConfigurationTblpmcheckpoint)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shopid_tblshop");
            });

            modelBuilder.Entity<ConfigurationTblprimitivemaintainancescheduling>(entity =>
            {
                entity.HasKey(e => e.Pmid)
                    .HasName("PK__configur__412600BAE2BF3704");

                entity.ToTable("configuration_tblprimitivemaintainancescheduling");

                entity.Property(e => e.Pmid).HasColumnName("pmid");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CellName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('null')");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MachineName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Null')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Month)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MonthId).HasColumnName("MonthID");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.PlantName)
                    .HasColumnName("plantName")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('null')");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Shopname)
                    .HasColumnName("shopname")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('null')");

                entity.Property(e => e.WeekId).HasColumnName("WeekID");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.ConfigurationTblprimitivemaintainancescheduling)
                    .HasForeignKey(d => d.CellId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cellid_tblprimitivemaintainancescheduling2223");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.ConfigurationTblprimitivemaintainancescheduling)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("machineID_tblprimitivemaintainancescheduling2223");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ConfigurationTblprimitivemaintainancescheduling)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("PlantID_tblprimitivemaintainancescheduling2223");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ConfigurationTblprimitivemaintainancescheduling)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("ShopID_tblprimitivemaintainancescheduling2223");
            });

            modelBuilder.Entity<ConfigurationTblsensorgroup>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__configur__CA19597092286E84");

                entity.ToTable("configuration_tblsensorgroup");

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.SensorDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SensorGroupName)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Configurationtblmachinesensor>(entity =>
            {
                entity.HasKey(e => e.Msid)
                    .HasName("PK__configur__6CB36003809F242B");

                entity.ToTable("configurationtblmachinesensor");

                entity.Property(e => e.Msid).HasColumnName("MSID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasColumnName("IPAddress")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(6)");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Configurationtblmachinesensor)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbllk_tblMachineSensor");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Configurationtblmachinesensor)
                    .HasForeignKey(d => d.Sid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblkl_tblMachineSensor");
            });

            modelBuilder.Entity<Configurationtblsensordatalink>(entity =>
            {
                entity.HasKey(e => e.ParameterTypeId)
                    .HasName("PK__configur__7FF7AC58CA7AA2F4");

                entity.ToTable("configurationtblsensordatalink");

                entity.Property(e => e.ParameterTypeId).HasColumnName("ParameterTypeID");

                entity.Property(e => e.AxisId).HasColumnName("AxisID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.Deterioration)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Element)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.LogFreqUnitId).HasColumnName("LogFreqUnitID");

                entity.Property(e => e.Lsl)
                    .HasColumnName("LSL")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.ParameterDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SubElement)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Usl)
                    .HasColumnName("USL")
                    .HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Axis)
                    .WithMany(p => p.Configurationtblsensordatalink)
                    .HasForeignKey(d => d.AxisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("axisid");

                entity.HasOne(d => d.LogFreqUnit)
                    .WithMany(p => p.ConfigurationtblsensordatalinkLogFreqUnit)
                    .HasForeignKey(d => d.LogFreqUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("logfreqid");

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany(p => p.ConfigurationtblsensordatalinkUnitNavigation)
                    .HasForeignKey(d => d.Unit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("unit");
            });

            modelBuilder.Entity<Configurationtblsensormaster>(entity =>
            {
                entity.HasKey(e => e.Smid)
                    .HasName("PK__configur__A47B2F5613189512");

                entity.ToTable("configurationtblsensormaster");

                entity.Property(e => e.Smid).HasColumnName("SMID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(6)");

                entity.Property(e => e.Parametertypeid).HasColumnName("parametertypeid");

                entity.Property(e => e.ScalingFactor)
                    .HasColumnName("scalingFactor")
                    .HasColumnType("decimal(6, 4)");

                entity.Property(e => e.SensorDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SensorlimitHigh).HasColumnName("sensorlimitHigh");

                entity.Property(e => e.SensorlimitLow).HasColumnName("sensorlimitLow");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Configurationtblsensormaster)
                    .HasForeignKey(d => d.Sid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SID_FK_ID");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Configurationtblsensormaster)
                    .HasForeignKey(d => d.Unitid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("uid");
            });

            modelBuilder.Entity<Frommail>(entity =>
            {
                entity.ToTable("frommail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Domain)
                    .HasColumnName("domain")
                    .HasMaxLength(45);

                entity.Property(e => e.FromEmailAdd)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<GraphData>(entity =>
            {
                entity.HasKey(e => e.Gid);

                entity.ToTable("Graph_Data");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.LossTime).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MinTime).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MinorLossTime).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OperatingTime).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PerformanceFactor).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PowerOff)
                    .HasColumnName("PowerOFF")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<HandleNoPing>(entity =>
            {
                entity.HasKey(e => e.NoPingId)
                    .HasName("PK_handle_no_ping_NoPingID");

                entity.ToTable("handle_no_ping");

                entity.Property(e => e.NoPingId).HasColumnName("NoPingID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<JobcardDetails>(entity =>
            {
                entity.HasKey(e => e.JobcardId)
                    .HasName("PK_jobcard_details_JobcardID");

                entity.ToTable("jobcard_details");

                entity.Property(e => e.JobcardId).HasColumnName("JobcardID");

                entity.Property(e => e.Compcode).HasMaxLength(25);

                entity.Property(e => e.EmpNo).HasMaxLength(25);

                entity.Property(e => e.Fromdatetime).HasColumnType("datetime2(0)");

                entity.Property(e => e.JobCardDate).HasMaxLength(45);

                entity.Property(e => e.MachineInvNumber).HasMaxLength(45);

                entity.Property(e => e.OpnIdleCode).HasMaxLength(25);

                entity.Property(e => e.Shift).HasMaxLength(45);

                entity.Property(e => e.Slno).HasColumnName("slno");

                entity.Property(e => e.Totalhours).HasMaxLength(45);

                entity.Property(e => e.Workorderno).HasMaxLength(25);
            });

            modelBuilder.Entity<LossDetails>(entity =>
            {
                entity.HasKey(e => e.Lid);

                entity.Property(e => e.Losscodeid).HasColumnName("losscodeid");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");
            });

            modelBuilder.Entity<Mailmaster>(entity =>
            {
                entity.ToTable("mailmaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bccadd).HasColumnName("BCCAdd");

                entity.Property(e => e.Ccadd).HasColumnName("CCAdd");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("EmailID")
                    .HasMaxLength(45);

                entity.Property(e => e.Toadd)
                    .HasColumnName("TOAdd")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MessageCodeMaster>(entity =>
            {
                entity.HasKey(e => e.MessageCodeId)
                    .HasName("PK_message_code_master_MessageCodeID");

                entity.ToTable("message_code_master");

                entity.Property(e => e.MessageCodeId).HasColumnName("MessageCodeID");

                entity.Property(e => e.ColourCode).HasMaxLength(45);

                entity.Property(e => e.InsertedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MessageCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MessageDescription)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.MessageMcode)
                    .IsRequired()
                    .HasColumnName("MessageMCode")
                    .HasMaxLength(10);

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ModifiedBy).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ReportDispName).HasMaxLength(100);
            });

            modelBuilder.Entity<MessageHistoryMaster>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK_message_history_master_MessageID");

                entity.ToTable("message_history_master");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.Meassage).IsRequired();

                entity.Property(e => e.MessageCode).HasMaxLength(10);

                entity.Property(e => e.MessageDate).HasColumnType("date");

                entity.Property(e => e.MessageDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.MessageNo)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MessageShift).HasMaxLength(10);

                entity.Property(e => e.MessageType).HasMaxLength(10);
            });

            modelBuilder.Entity<Monthdata>(entity =>
            {
                entity.HasKey(e => e.MonthId)
                    .HasName("PK__monthdat__9FA83F86469FBC26");

                entity.ToTable("monthdata");

                entity.Property(e => e.MonthId).HasColumnName("MonthID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Isdeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operationlog>(entity =>
            {
                entity.HasKey(e => e.Idoperationlog)
                    .HasName("PK_operationlog_idoperationlog");

                entity.ToTable("operationlog");

                entity.Property(e => e.Idoperationlog).HasColumnName("idoperationlog");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OpDate).HasColumnType("date");

                entity.Property(e => e.OpDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.OpReason).HasMaxLength(256);
            });

            modelBuilder.Entity<Parameters>(entity =>
            {
                entity.HasKey(e => e.ParameterId)
                    .HasName("PK_parameters_ParameterID");

                entity.ToTable("parameters");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ParameterType).HasMaxLength(45);
            });

            modelBuilder.Entity<ParametersMaster>(entity =>
            {
                entity.HasKey(e => e.ParameterId)
                    .HasName("PK_parameters_master_ParameterID");

                entity.ToTable("parameters_master");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.AutoCutTime).HasMaxLength(45);

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.CuttingTime).HasMaxLength(45);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperatingTime)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.PowerOnTime)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.SetupTime).HasMaxLength(45);

                entity.Property(e => e.TotalCutTime)
                    .HasColumnName("Total_CutTime")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<PcbDetails>(entity =>
            {
                entity.HasKey(e => e.Pcbid)
                    .HasName("PK_pcb_details_PCBID");

                entity.ToTable("pcb_details");

                entity.Property(e => e.Pcbid).HasColumnName("PCBID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Pcbipaddress)
                    .IsRequired()
                    .HasColumnName("PCBIPAddress")
                    .HasMaxLength(30);

                entity.Property(e => e.Pcbmacaddress)
                    .HasColumnName("PCBMACAddress")
                    .HasMaxLength(45);

                entity.Property(e => e.Pcbno).HasColumnName("PCBNo");
            });

            modelBuilder.Entity<PcbParameters>(entity =>
            {
                entity.HasKey(e => e.ParameterId)
                    .HasName("PK_pcb_parameters_ParameterID");

                entity.ToTable("pcb_parameters");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ColorCode).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.LogFile).HasMaxLength(45);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ParameterType).HasMaxLength(45);
            });

            modelBuilder.Entity<Pcbdaq>(entity =>
            {
                entity.ToTable("pcbdaq");

                entity.Property(e => e.Pcbdaqid).HasColumnName("PCBDAQID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PcbdateTime)
                    .HasColumnName("PCBDateTime")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Pcbipaddress)
                    .IsRequired()
                    .HasColumnName("PCBIPAddress")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<PcbdaqinTbl>(entity =>
            {
                entity.HasKey(e => e.Daqinid)
                    .HasName("PK_pcbdaqin_tbl_DAQINID");

                entity.ToTable("pcbdaqin_tbl");

                entity.Property(e => e.Daqinid).HasColumnName("DAQINID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ParamPin).HasColumnName("ParamPIN");

                entity.Property(e => e.Pcbipaddress)
                    .IsRequired()
                    .HasColumnName("PCBIPAddress")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<PcbdpsMaster>(entity =>
            {
                entity.ToTable("pcbdps_master");

                entity.Property(e => e.PcbDpsMasterId).HasColumnName("PcbDpsMasterID");

                entity.Property(e => e.ColorValue).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");
            });

            modelBuilder.Entity<ProgramMaster>(entity =>
            {
                entity.HasKey(e => e.ProgramId)
                    .HasName("PK_program_master_ProgramID");

                entity.ToTable("program_master");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.ComponentCode).HasMaxLength(20);

                entity.Property(e => e.ComponentDescription).HasMaxLength(50);

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperationDescription).HasMaxLength(20);

                entity.Property(e => e.OpnCode1)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.OpnCode2).HasMaxLength(10);

                entity.Property(e => e.OpnCode3).HasMaxLength(10);

                entity.Property(e => e.ProgramDate).HasColumnType("date");

                entity.Property(e => e.ProgramDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Shift).HasMaxLength(10);

                entity.Property(e => e.WorkOrderNo1)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.WorkOrderNo2).HasMaxLength(20);

                entity.Property(e => e.WorkOrderNo3).HasMaxLength(20);
            });

            modelBuilder.Entity<QualtiyRawPiweb>(entity =>
            {
                entity.HasKey(e => e.QualityId);

                entity.ToTable("QualtiyRaw_piweb");

                entity.Property(e => e.QualityId).HasColumnName("QualityID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MeasDateTime)
                    .HasColumnName("Meas_DateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Op10).HasColumnName("OP10");

                entity.Property(e => e.Op20).HasColumnName("OP20");

                entity.Property(e => e.Op30).HasColumnName("OP30");

                entity.Property(e => e.PartIdentity).HasMaxLength(50);

                entity.Property(e => e.PartNumber).HasMaxLength(150);

                entity.Property(e => e.WorkOrderNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<Sharathtable>(entity =>
            {
                entity.HasKey(e => e.Sharathid);

                entity.ToTable("sharathtable");

                entity.Property(e => e.Sharathid).HasColumnName("sharathid");

                entity.Property(e => e.Mcid).HasColumnName("MCID");

                entity.Property(e => e.Sharathname)
                    .HasColumnName("sharathname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sharathvalue).HasColumnName("sharathvalue");

                entity.Property(e => e.Syncid).HasColumnName("syncid");

                entity.Property(e => e.TabSharathId).HasColumnName("TabSharathID");
            });

            modelBuilder.Entity<ShiftMaster>(entity =>
            {
                entity.HasKey(e => e.ShiftId);

                entity.ToTable("shift_master");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ShiftName).HasMaxLength(10);
            });

            modelBuilder.Entity<Smtpdetails>(entity =>
            {
                entity.HasKey(e => e.SmtpId);

                entity.ToTable("smtpdetails");

                entity.Property(e => e.SmtpId).HasColumnName("smtpID");

                entity.Property(e => e.ConnectType).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.FromMailId).HasMaxLength(50);

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.TcfModuleId).HasColumnName("tcfModuleId");
            });

            modelBuilder.Entity<TblAndonDispDet>(entity =>
            {
                entity.HasKey(e => e.Andondispid)
                    .HasName("PK_tblAndonDispDet_1");

                entity.ToTable("tblAndonDispDet");

                entity.Property(e => e.Andondispid)
                    .HasColumnName("ANDONDISPID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.TblAndonDispDet)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("FK_tblAndonDispDEt_tblcell");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.TblAndonDispDet)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAndonDispDEt_tblplant");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.TblAndonDispDet)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_tblAndonDispDEt_tblshop");
            });

            modelBuilder.Entity<TblAndonImageTextScheduledDisplay>(entity =>
            {
                entity.HasKey(e => e.TextImageAndonId);

                entity.ToTable("tblAndonImageTextScheduledDisplay");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.DefaultScreenVisible).HasDefaultValueSql("(N'0')");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ScreenType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'1,1,0,0-1')");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.TblAndonImageTextScheduledDisplay)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("FK_tblAndonImageTextScheduledDisplay_tblcell");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.TblAndonImageTextScheduledDisplay)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_tblAndonImageTextScheduledDisplay_tblplant");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.TblAndonImageTextScheduledDisplay)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_tblAndonImageTextScheduledDisplay_tblshop");
            });

            modelBuilder.Entity<TblAutoreportLog>(entity =>
            {
                entity.HasKey(e => e.AutoReportLogId)
                    .HasName("PK_tbl_autoreport_log_AutoReportLogID");

                entity.ToTable("tbl_autoreport_log");

                entity.Property(e => e.AutoReportLogId).HasColumnName("AutoReportLogID");

                entity.Property(e => e.AutoReportId).HasColumnName("AutoReportID");

                entity.Property(e => e.CompletedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.ExcelCreated).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExcelCreatedTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MailSent).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AutoReport)
                    .WithMany(p => p.TblAutoreportLog)
                    .HasForeignKey(d => d.AutoReportId)
                    .HasConstraintName("tbl_autoreport_log$AutoReportID");
            });

            modelBuilder.Entity<TblAutoreportbasedon>(entity =>
            {
                entity.HasKey(e => e.BasedOnId)
                    .HasName("PK_tbl_autoreportbasedon_BasedOnID");

                entity.ToTable("tbl_autoreportbasedon");

                entity.Property(e => e.BasedOnId).HasColumnName("BasedOnID");

                entity.Property(e => e.BasedOn).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Desc).HasMaxLength(100);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<TblAutoreportsetting>(entity =>
            {
                entity.HasKey(e => e.AutoReportId)
                    .HasName("PK_tbl_autoreportsetting_AutoReportID");

                entity.ToTable("tbl_autoreportsetting");

                entity.Property(e => e.AutoReportId).HasColumnName("AutoReportID");

                entity.Property(e => e.AutoReportTimeId).HasColumnName("AutoReportTimeID");

                entity.Property(e => e.CcmailList).HasColumnName("CCMailList");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.NextRunDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.AutoReportTime)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.AutoReportTimeId)
                    .HasConstraintName("tbl_autoreportsetting$ReportTimeID");

                entity.HasOne(d => d.BasedOnNavigation)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.BasedOn)
                    .HasConstraintName("tbl_autoreportsetting$BasedOnID");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("tbl_autoreportsetting$ReportCellID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("tbl_autoreportsetting$ReportWorkCentreID");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tbl_autoreportsetting$ReportPlantID");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("tbl_autoreportsetting$ReportID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.TblAutoreportsetting)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("tbl_autoreportsetting$ReportShopID");
            });

            modelBuilder.Entity<TblAutoreporttime>(entity =>
            {
                entity.HasKey(e => e.AutoReportTimeId)
                    .HasName("PK_tbl_autoreporttime_AutoReportTimeID");

                entity.ToTable("tbl_autoreporttime");

                entity.Property(e => e.AutoReportTimeId).HasColumnName("AutoReportTimeID");

                entity.Property(e => e.AutoReportTime).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Description).HasMaxLength(45);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<TblAxisdet>(entity =>
            {
                entity.HasKey(e => e.AxisDetId)
                    .HasName("PK_tbl_axisdet_AxisDetID");

                entity.ToTable("tbl_axisdet");

                entity.Property(e => e.AxisDetId).HasColumnName("AxisDetID");

                entity.Property(e => e.AxisId).HasColumnName("AxisID");

                entity.Property(e => e.AxisName)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.SpindlePath).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblAxisdet)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_axisdet$AxisMachineIDIdx");
            });

            modelBuilder.Entity<TblAxisdetails1>(entity =>
            {
                entity.HasKey(e => e.Adid);

                entity.ToTable("tbl_axisdetails1");

                entity.Property(e => e.Adid).HasColumnName("ADID");

                entity.Property(e => e.AbsPos).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.Axis)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DistPos).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MacPos).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.RelPos).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblAxisdetails2>(entity =>
            {
                entity.HasKey(e => e.Ad2id)
                    .HasName("PK_tbl_axisdetails2_AD2ID");

                entity.ToTable("tbl_axisdetails2");

                entity.Property(e => e.Ad2id).HasColumnName("AD2ID");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.FeedRate).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.SpindleLoad).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.SpindleSpeed).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<TblBachUplossofentry>(entity =>
            {
                entity.HasKey(e => e.BlossId)
                    .HasName("PK__tblbackuplosso__7025E39495D86E55");

                entity.ToTable("tblBachUplossofentry");

                entity.Property(e => e.BlossId).HasColumnName("BLossID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.EntryTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCodeId).HasColumnName("MessageCodeID");

                entity.Property(e => e.MessageDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<TblBackupoeedashboardvariables>(entity =>
            {
                entity.HasKey(e => e.BoeevariablesId)
                    .HasName("PK__tbloeedasad__BB29BFE58B476DBD");

                entity.ToTable("tblBackupoeedashboardvariables");

                entity.HasIndex(e => e.BoeevariablesId)
                    .HasName("BOEEVariablesID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.BoeevariablesId).HasColumnName("BOEEVariablesID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Loss1Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Loss2Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Loss3Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Loss4Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Loss5Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ReWotime).HasColumnName("ReWOTime");

                entity.Property(e => e.Roalossess).HasColumnName("ROALossess");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.SummationOfSctvsPp).HasColumnName("SummationOfSCTvsPP");

                entity.Property(e => e.Wcid).HasColumnName("WCID");
            });

            modelBuilder.Entity<TblBreadownAndLossReport>(entity =>
            {
                entity.ToTable("tblBreadownAndLossReport");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BreakDownAndLossCode).HasColumnName("breakDownAndLossCode");

                entity.Property(e => e.BreakDownId).HasColumnName("breakDownId");

                entity.Property(e => e.CorrectedDate)
                    .HasColumnName("correctedDate")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DoneWithRow).HasColumnName("doneWithRow");

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.EntryTime).HasColumnName("entryTime");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.MessageCodeId).HasColumnName("messageCodeId");

                entity.Property(e => e.Shift)
                    .HasColumnName("shift")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnName("startTime");
            });

            modelBuilder.Entity<TblBreakDownTickect>(entity =>
            {
                entity.ToTable("tblBreakDownTickect");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcceptFlage).HasDefaultValueSql("((0))");

                entity.Property(e => e.BdTktDateTime)
                    .HasColumnName("bdTktDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CorrectedDate)
                    .HasColumnName("correctedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.MaintFinished).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaintFlage).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaintRejId).HasColumnName("maintRejId");

                entity.Property(e => e.MntAcpRejDateTime)
                    .HasColumnName("mntAcp/RejDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.MntClosureOpId).HasColumnName("mntClosureOpId");

                entity.Property(e => e.MntOpId).HasColumnName("mntOpId");

                entity.Property(e => e.MntRemarks)
                    .HasColumnName("mntRemarks")
                    .HasMaxLength(500);

                entity.Property(e => e.MntRrejectReason)
                    .HasColumnName("mntRrejectReason")
                    .HasMaxLength(500);

                entity.Property(e => e.MntStatus).HasColumnName("mntStatus");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("operatorId")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProdAcpRejDateTime)
                    .HasColumnName("prodAcp/RejDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProdFinished).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProdOpId).HasColumnName("prodOpId");

                entity.Property(e => e.ProdRejId).HasColumnName("prodRejId");

                entity.Property(e => e.ProdRemarks)
                    .HasColumnName("prodRemarks")
                    .HasMaxLength(500);

                entity.Property(e => e.ProdStatus).HasColumnName("prodStatus");

                entity.Property(e => e.ReasonId).HasColumnName("reasonId");

                entity.Property(e => e.TktClosingTime)
                    .HasColumnName("tktClosingTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.WoId).HasColumnName("woId");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblBreakDownTickect)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_tblBreakDownTickect_tblmachinedetails");

                entity.HasOne(d => d.MntOp)
                    .WithMany(p => p.TblBreakDownTickectMntOp)
                    .HasForeignKey(d => d.MntOpId)
                    .HasConstraintName("FK_tblBreakDownTickect_tblOperatorLoginDetails");

                entity.HasOne(d => d.ProdOp)
                    .WithMany(p => p.TblBreakDownTickectProdOp)
                    .HasForeignKey(d => d.ProdOpId)
                    .HasConstraintName("FK_tblBreakDownTickect_tblOperatorLoginDetails1");

                entity.HasOne(d => d.Reason)
                    .WithMany(p => p.TblBreakDownTickect)
                    .HasForeignKey(d => d.ReasonId)
                    .HasConstraintName("FK_tblBreakDownTickect_tbllossescodes");
            });

            modelBuilder.Entity<TblBreakdowncodes>(entity =>
            {
                entity.HasKey(e => e.BreakdownId)
                    .HasName("PK_tblBreakdowncodes_BreakdownID");

                entity.ToTable("tblBreakdowncodes");

                entity.Property(e => e.BreakdownId).HasColumnName("BreakdownID");

                entity.Property(e => e.BreakdownCode).HasMaxLength(50);

                entity.Property(e => e.BreakdownDesc).HasMaxLength(45);

                entity.Property(e => e.BreakdownLevel1Id).HasColumnName("BreakdownLevel1ID");

                entity.Property(e => e.BreakdownLevel2Id).HasColumnName("BreakdownLevel2ID");

                entity.Property(e => e.ContributeTo).HasMaxLength(30);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.MessageType).HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.TargetPercent)
                    .HasColumnType("decimal(6, 2)")
                    .HasDefaultValueSql("((0.01))");
            });

            modelBuilder.Entity<TblCycleTimeAnalysis>(entity =>
            {
                entity.HasKey(e => e.Ctaid);

                entity.ToTable("tbl_CycleTimeAnalysis");

                entity.Property(e => e.Ctaid).HasColumnName("CTAID");

                entity.Property(e => e.AvgLoadTimeinMinutes).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgOperatingTime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgOperatingTimeUnit).HasMaxLength(50);

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LossTime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LossTimeUnit).HasMaxLength(50);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperatingTime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OperatingTimeUnit).HasMaxLength(50);

                entity.Property(e => e.PartNum).HasMaxLength(50);

                entity.Property(e => e.StdCycleTime)
                    .HasColumnName("Std_CycleTime")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StdCycleTimeUnit)
                    .HasColumnName("Std_CycleTimeUnit")
                    .HasMaxLength(50);

                entity.Property(e => e.StdLoadTime)
                    .HasColumnName("Std_LoadTime")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StdLoadTimeUnit)
                    .HasColumnName("Std_LoadTimeUnit")
                    .HasMaxLength(50);

                entity.Property(e => e.TotalLoadTime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalLoadTimeUnit).HasMaxLength(50);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblCycleTimeAnalysis)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_tbl_CycleTimeAnalysis_tblmachinedetails");
            });

            modelBuilder.Entity<TblDdlstatus>(entity =>
            {
                entity.HasKey(e => e.DdlstatusId);

                entity.ToTable("tblDDLStatus");

                entity.Property(e => e.DdlstatusId).HasColumnName("DDLStatusId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StatusMessage)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblDeletedProgDet>(entity =>
            {
                entity.HasKey(e => e.ProgDeletedId);

                entity.ToTable("tbl_deletedProgDet");

                entity.Property(e => e.ProgDeletedId).HasColumnName("ProgDeletedID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifedOn).HasColumnType("datetime");

                entity.Property(e => e.ProgramData)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ProgramNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblGenericfilepath>(entity =>
            {
                entity.HasKey(e => e.FilePathId)
                    .HasName("PK_tbl_genericfilepath_FilePathID");

                entity.ToTable("tbl_genericfilepath");

                entity.Property(e => e.FilePathId).HasColumnName("FilePathID");

                entity.Property(e => e.CompleteFilePath).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.FilePathDesc).HasMaxLength(150);

                entity.Property(e => e.FilePathDet).HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<TblHolidayManagment>(entity =>
            {
                entity.HasKey(e => e.HolidayManagmentId);

                entity.ToTable("tblHolidayManagment");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DaysDuration)
                    .HasColumnName("daysDuration")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.HolidayManagmentDesc).HasMaxLength(400);

                entity.Property(e => e.HolidayManagmentName).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("isDelete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblHolidayTypeMasters>(entity =>
            {
                entity.HasKey(e => e.HolidayTypeId);

                entity.ToTable("tblHolidayTypeMasters");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HolidayTypeColorCode).HasMaxLength(50);

                entity.Property(e => e.HolidayTypeName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblLivecbmparameters>(entity =>
            {
                entity.HasKey(e => e.CbmpId)
                    .HasName("PK__tbl_live__C79BE4D40F7D434F");

                entity.ToTable("tbl_livecbmparameters");

                entity.Property(e => e.CbmpId).HasColumnName("cbmpID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasColumnName("IPAddress")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IsConverted).HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.SensorGroupId).HasColumnName("SensorGroupID");
            });

            modelBuilder.Entity<TblMachinestatusrealtime>(entity =>
            {
                entity.HasKey(e => e.MachineStatusId);

                entity.ToTable("tbl_machinestatusrealtime");

                entity.Property(e => e.MachineStatusId).HasColumnName("MachineStatusID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineAlarm).HasMaxLength(50);

                entity.Property(e => e.MachineEmergency).HasMaxLength(50);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MachineStatus).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMultiwoselection>(entity =>
            {
                entity.HasKey(e => e.MultiWoid)
                    .HasName("PK__tbl_mult__22EB3C7E18F7F6D4");

                entity.ToTable("tbl_multiwoselection");

                entity.Property(e => e.MultiWoid)
                    .HasColumnName("MultiWOID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DdlworkCentre)
                    .HasColumnName("DDLWorkCentre")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsSubmit).HasDefaultValueSql("('0')");

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ScrapQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.WorkOrder)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hmi)
                    .WithMany(p => p.TblMultiwoselection)
                    .HasForeignKey(d => d.Hmiid)
                    .HasConstraintName("fkHMIID");
            });

            modelBuilder.Entity<TblNcProgramTransferMain>(entity =>
            {
                entity.HasKey(e => e.NcProgramTransferId);

                entity.ToTable("tblNcProgramTransferMain");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FromCnc)
                    .HasColumnName("FromCNC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProgramNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Mc)
                    .WithMany(p => p.TblNcProgramTransferMain)
                    .HasForeignKey(d => d.McId)
                    .HasConstraintName("FK_tblNcProgramTransferMain_tblmachinedetails");
            });

            modelBuilder.Entity<TblOeedetails>(entity =>
            {
                entity.HasKey(e => e.Oeeid);

                entity.ToTable("tbl_OEEDetails");

                entity.Property(e => e.Oeeid).HasColumnName("OEEID");

                entity.Property(e => e.Availability).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.Oee)
                    .HasColumnName("OEE")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OperatingTimeinMin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Performance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PerformanceFactor).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PowerOffTimeInMinutes).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PowerOnTimeInMinutes).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Quality).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalIdletimeinMin)
                    .HasColumnName("TotalIDLETimeinMin")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalTimeInMinutes).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblOeedetails)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_tbl_OEEDetails_tbl_MachineDetails");
            });

            modelBuilder.Entity<TblOeereportasdivision>(entity =>
            {
                entity.HasKey(e => e.Lid)
                    .HasName("PK_unitworkccs.tbl_oeereportasdivision");

                entity.ToTable("tbl_oeereportasdivision");

                entity.Property(e => e.Lid).HasColumnName("LID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.Fgcode)
                    .HasColumnName("FGCode")
                    .HasMaxLength(50);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.LossId).HasColumnName("LossID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");
            });

            modelBuilder.Entity<TblOperatorDashboard>(entity =>
            {
                entity.HasKey(e => e.OperatorDashboardId);

                entity.ToTable("tblOperatorDashboard");

                entity.Property(e => e.OperatorDashboardId).HasColumnName("OperatorDashboardID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MessageDescription).HasMaxLength(150);

                entity.Property(e => e.MessageEndTime).HasColumnType("datetime");

                entity.Property(e => e.MessageStartTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblOperatorDashboard)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOperatorDashboard_tblmachinedetails");
            });

            modelBuilder.Entity<TblOperatorHeader>(entity =>
            {
                entity.HasKey(e => e.OperatorId);

                entity.ToTable("tblOperatorHeader");

                entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MachineMode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ServerConnecStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Shift)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TabConnecStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblOperatorHeader)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOperatorHeader_tblmachinedetails");
            });

            modelBuilder.Entity<TblOperatorLoginDetails>(entity =>
            {
                entity.HasKey(e => e.OperatorLoginId);

                entity.ToTable("tblOperatorLoginDetails");

                entity.Property(e => e.OperatorLoginId).HasColumnName("operatorLoginId");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorEmailId)
                    .HasColumnName("operatorEmailId")
                    .HasMaxLength(200);

                entity.Property(e => e.OperatorId).HasColumnName("operatorId");

                entity.Property(e => e.OperatorMobileNo)
                    .HasColumnName("operatorMobileNo")
                    .HasMaxLength(20);

                entity.Property(e => e.OperatorName)
                    .HasColumnName("operatorName")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorPwd)
                    .HasColumnName("operatorPwd")
                    .HasMaxLength(200);

                entity.Property(e => e.OperatorUserName)
                    .HasColumnName("operatorUserName")
                    .HasMaxLength(200);

                entity.Property(e => e.Reasons)
                    .HasColumnName("reasons")
                    .HasMaxLength(500);

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblOperatorLoginDetails)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_tblOperatorLoginDetails_tblroles");
            });

            modelBuilder.Entity<TblOperatorMachineDetails>(entity =>
            {
                entity.HasKey(e => e.OpertorMacDetId);

                entity.ToTable("tblOperatorMachineDetails");

                entity.Property(e => e.OpertorMacDetId).HasColumnName("opertorMacDetId");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MachineId).HasColumnName("machineId");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorLoginId).HasColumnName("operatorLoginId");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblOperatorMachineDetails)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_tblOperatorMachineDetails_tblmachinedetails");

                entity.HasOne(d => d.OperatorLogin)
                    .WithMany(p => p.TblOperatorMachineDetails)
                    .HasForeignKey(d => d.OperatorLoginId)
                    .HasConstraintName("FK_tblOperatorMachineDetails_tblOperatorLoginDetails");
            });

            modelBuilder.Entity<TblPiwebAdjOee>(entity =>
            {
                entity.ToTable("tblPiweb_AdjOEE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdjOeeuuid)
                    .HasColumnName("AdjOEEuuid")
                    .HasMaxLength(150);

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.Createdon).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPiwebLoss>(entity =>
            {
                entity.HasKey(e => e.PkLossUuid);

                entity.ToTable("tblPiweb_Loss");

                entity.Property(e => e.PkLossUuid).HasColumnName("PK_Loss_UUID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LossUuid)
                    .HasColumnName("LOSS_UUID")
                    .HasMaxLength(500);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");
            });

            modelBuilder.Entity<TblPmsnotificationMaster>(entity =>
            {
                entity.HasKey(e => e.Pmsid);

                entity.ToTable("TblPMSNotification_Master");

                entity.Property(e => e.Pmsid).HasColumnName("PMSId");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.NoOfDaysPrior).HasMaxLength(50);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.SmscontactList).HasColumnName("SMSContactList");

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.Property(e => e.WorkCenterId).HasColumnName("WorkCenterID");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.TblPmsnotificationMaster)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("FK_TblPMSNotification_Master_tblcell");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.TblPmsnotificationMaster)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_TblPMSNotification_Master_tblplant2");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.TblPmsnotificationMaster)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_TblPMSNotification_Master_tblshop");

                entity.HasOne(d => d.WorkCenter)
                    .WithMany(p => p.TblPmsnotificationMaster)
                    .HasForeignKey(d => d.WorkCenterId)
                    .HasConstraintName("FK_TblPMSNotification_Master_tblmachinedetails1");
            });

            modelBuilder.Entity<TblPrecentColour>(entity =>
            {
                entity.HasKey(e => e.ColourId);

                entity.ToTable("tblPrecentColour");

                entity.Property(e => e.ColourId).HasColumnName("ColourID");

                entity.Property(e => e.Colour).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPresentTool>(entity =>
            {
                entity.HasKey(e => e.PreToolId);

                entity.ToTable("tblPresentTool");

                entity.Property(e => e.PreToolId).HasColumnName("PreToolID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");
            });

            modelBuilder.Entity<TblProdAndonDisp>(entity =>
            {
                entity.HasKey(e => e.ProdDashboardId);

                entity.ToTable("tbl_ProdAndonDisp");

                entity.Property(e => e.ProdDashboardId).HasColumnName("ProdDashboardID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.TotalLoss).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.TotalOperatingTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.TotalSetup).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.UtilPercent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Woid).HasColumnName("WOID");

                entity.Property(e => e.WorkOrderNo)
                    .HasColumnName("Work_Order_No")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblProdAndonDisp)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ProdAndonDisp_tblmachinedetails");

                entity.HasOne(d => d.Wo)
                    .WithMany(p => p.TblProdAndonDisp)
                    .HasForeignKey(d => d.Woid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ProdAndonDisp_tblworkorderentry");
            });

            modelBuilder.Entity<TblProdManMachine>(entity =>
            {
                entity.HasKey(e => e.ProdManMachineId);

                entity.ToTable("tbl_ProdManMachine");

                entity.Property(e => e.ProdManMachineId).HasColumnName("ProdManMachineID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.PerformancePerCent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.QualityPercent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.TotalLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalMinorLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalOperatingTime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPowerLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalSetup).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalSetupMinorLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UtilPercent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Woid).HasColumnName("WOID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblProdManMachine)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ProdManMachine_tblmachinedetails");

                entity.HasOne(d => d.Wo)
                    .WithMany(p => p.TblProdManMachine)
                    .HasForeignKey(d => d.Woid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ProdManMachine_tblworkorderentry");
            });

            modelBuilder.Entity<TblProdOrderLosses>(entity =>
            {
                entity.HasKey(e => e.ProdOrderlossId);

                entity.ToTable("tbl_ProdOrderLosses");

                entity.Property(e => e.ProdOrderlossId).HasColumnName("ProdOrderlossID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.LossCodeL1id).HasColumnName("LossCodeL1ID");

                entity.Property(e => e.LossCodeL2id).HasColumnName("LossCodeL2ID");

                entity.Property(e => e.LossId).HasColumnName("LossID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.Woid).HasColumnName("WOID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblProdOrderLosses)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ProdOrderLosses_tblmachinedetails");
            });

            modelBuilder.Entity<TblProdPlanMasters>(entity =>
            {
                entity.HasKey(e => e.ProdPlanId);

                entity.ToTable("tblProdPlanMasters");

                entity.Property(e => e.ProdPlanId).HasColumnName("ProdPlanID");

                entity.Property(e => e.Fgcode)
                    .IsRequired()
                    .HasColumnName("FGCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.OperationNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProdOrderNo)
                    .IsRequired()
                    .HasColumnName("Prod_Order_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QtyUnit)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.WorkCenter)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblQualityPiweb>(entity =>
            {
                entity.HasKey(e => e.QualityId);

                entity.ToTable("tblQuality_Piweb");

                entity.Property(e => e.QualityId).HasColumnName("QualityID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.PartNumber).HasMaxLength(150);

                entity.Property(e => e.WorkOrderNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<TblReportmaster>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK_tbl_reportmaster_ReportID");

                entity.ToTable("tbl_reportmaster");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ReportDescription).HasMaxLength(100);

                entity.Property(e => e.ReportDispName).HasMaxLength(50);

                entity.Property(e => e.ReportName).HasMaxLength(100);

                entity.Property(e => e.ReportTemplatePath).HasMaxLength(100);
            });

            modelBuilder.Entity<TblReworkReason>(entity =>
            {
                entity.HasKey(e => e.ReWorkId);

                entity.ToTable("tblReworkReason");

                entity.Property(e => e.ReWorkId).HasColumnName("ReWorkID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReworkName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<TblSapinput>(entity =>
            {
                entity.HasKey(e => e.SapInputId);

                entity.ToTable("tblSAPInput");

                entity.Property(e => e.SapInputId).HasColumnName("SapInputID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PathLocation)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<TblSaveNcprogVer>(entity =>
            {
                entity.HasKey(e => e.NcprogVerId);

                entity.ToTable("tbl_SaveNCProgVer");

                entity.Property(e => e.NcprogVerId).HasColumnName("NCProgVerID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProgramData).HasColumnType("ntext");

                entity.Property(e => e.ProgramNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblServodetails>(entity =>
            {
                entity.HasKey(e => e.Sdid);

                entity.ToTable("tbl_servodetails");

                entity.Property(e => e.Sdid).HasColumnName("SDID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.LoadCurrent).HasColumnType("decimal(6, 3)");

                entity.Property(e => e.LoadCurrentAmp).HasColumnType("decimal(6, 3)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ServoAxis)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ServoLoadMeter).HasColumnType("decimal(6, 3)");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSetupMaint>(entity =>
            {
                entity.HasKey(e => e.SetMainId);

                entity.ToTable("tblSetupMaint");

                entity.Property(e => e.SetMainId).HasColumnName("SetMainID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.LossCodeId).HasColumnName("LossCodeID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModeId).HasColumnName("ModeID");

                entity.Property(e => e.ServerSetMainId).HasColumnName("ServerSetMainID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.LossCode)
                    .WithMany(p => p.TblSetupMaint)
                    .HasForeignKey(d => d.LossCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSetupMaint_tbllossescodes");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblSetupMaint)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSetupMaint_tblmachinedetails");
            });

            modelBuilder.Entity<TblShiftUtilReport>(entity =>
            {
                entity.HasKey(e => e.ShiftUtilReportId)
                    .HasName("PK_ShiftUtilReportID");

                entity.ToTable("tblShiftUtilReport");

                entity.Property(e => e.ShiftUtilReportId).HasColumnName("ShiftUtilReportID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.CuttingTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.CuttingTimePrecent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperatingTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OperatingTimePrecent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.PowerOnTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.PowerOnTimePrecent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Shift)
                    .HasColumnName("shift")
                    .HasMaxLength(25);

                entity.Property(e => e.TotalTime).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblShiftUtilReport)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblShiftUtilReport_tblmachinedetails");
            });

            modelBuilder.Entity<TblStdToolLife>(entity =>
            {
                entity.HasKey(e => e.StdToolLifeId);

                entity.ToTable("tblStdToolLife");

                entity.Property(e => e.StdToolLifeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Ctcode)
                    .IsRequired()
                    .HasColumnName("CTCode")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Fgcode)
                    .IsRequired()
                    .HasColumnName("FGCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OperationNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ToolName).HasMaxLength(255);

                entity.Property(e => e.ToolNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTcfApprovedMaster>(entity =>
            {
                entity.HasKey(e => e.TcfApprovedMasterId);

                entity.ToTable("tblTcfApprovedMaster");

                entity.Property(e => e.TcfApprovedMasterId).HasColumnName("tcfApprovedMasterId");

                entity.Property(e => e.CellId).HasColumnName("cellId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstApproverCcList)
                    .HasColumnName("firstApproverCcList")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstApproverToList)
                    .HasColumnName("firstApproverToList")
                    .HasMaxLength(500);

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlantId).HasColumnName("plantId");

                entity.Property(e => e.SecondApproverCcList)
                    .HasColumnName("secondApproverCcList")
                    .HasMaxLength(500);

                entity.Property(e => e.SecondApproverToList)
                    .HasColumnName("secondApproverToList")
                    .HasMaxLength(500);

                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.TcfModuleId).HasColumnName("tcfModuleId");
            });

            modelBuilder.Entity<TblTcfModule>(entity =>
            {
                entity.HasKey(e => e.TcfModuleId);

                entity.ToTable("tblTcfModule");

                entity.Property(e => e.TcfModuleId).HasColumnName("tcfModuleId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.TcfModuleDesc)
                    .HasColumnName("tcfModuleDesc")
                    .HasMaxLength(500);

                entity.Property(e => e.TcfModuleName)
                    .HasColumnName("tcfModuleName")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TblTempLiveLossOfEntry>(entity =>
            {
                entity.HasKey(e => e.TempLossId);

                entity.ToTable("tblTempLiveLossOfEntry");

                entity.Property(e => e.TempLossId).HasColumnName("tempLossId");

                entity.Property(e => e.CorrectedDate)
                    .HasColumnName("correctedDate")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DoneWithRow).HasColumnName("doneWithRow");

                entity.Property(e => e.EndDateTime)
                    .HasColumnName("endDateTime")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.EntryTime)
                    .HasColumnName("entryTime")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ForRefresh).HasColumnName("forRefresh");

                entity.Property(e => e.IsScreen).HasColumnName("isScreen");

                entity.Property(e => e.IsStart).HasColumnName("isStart");

                entity.Property(e => e.IsUpdate).HasColumnName("isUpdate");

                entity.Property(e => e.MachineId).HasColumnName("machineID");

                entity.Property(e => e.MessageCode)
                    .HasColumnName("messageCode")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCodeId).HasColumnName("messageCodeId");

                entity.Property(e => e.MessageDesc)
                    .HasColumnName("messageDesc")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ModeId).HasColumnName("modeId");

                entity.Property(e => e.Shift)
                    .HasColumnName("shift")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime)
                    .HasColumnName("startDateTime")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.TempModeId).HasColumnName("tempModeId");
            });

            modelBuilder.Entity<TblToolCounter>(entity =>
            {
                entity.HasKey(e => e.ToolCounterId);

                entity.ToTable("tblToolCounter");

                entity.Property(e => e.ToolCounterId).HasColumnName("ToolCounterID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ToolNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUtilReport>(entity =>
            {
                entity.HasKey(e => e.UtilReportId);

                entity.ToTable("tbl_UtilReport");

                entity.Property(e => e.UtilReportId).HasColumnName("UtilReportID");

                entity.Property(e => e.Bdtime)
                    .HasColumnName("BDTime")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.LossTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MinorLossTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OperatingTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.PowerOffTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.SetupMinorTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.SetupTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.TotalTime).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.UtilPercent).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblUtilReport)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_UtilReport_tblmachinedetails");
            });

            modelBuilder.Entity<Tblactivitylog>(entity =>
            {
                entity.HasKey(e => e.TrackId)
                    .HasName("PK_tblactivitylog_TrackID");

                entity.ToTable("tblactivitylog");

                entity.Property(e => e.TrackId).HasColumnName("TrackID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ClientSystemName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompleteModificationDetails)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OpDate).HasColumnType("date");

                entity.Property(e => e.OpDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.OpTime).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Tblalarmdetails>(entity =>
            {
                entity.HasKey(e => e.AlarmId);

                entity.ToTable("tblalarmdetails");

                entity.Property(e => e.AlarmId).HasColumnName("AlarmID");

                entity.Property(e => e.AlarmDesc)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AlarmNumber).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblappPaths>(entity =>
            {
                entity.HasKey(e => e.AppPathsId)
                    .HasName("PK_tblapp_paths_AppPathsID");

                entity.ToTable("tblapp_paths");

                entity.Property(e => e.AppPathsId).HasColumnName("AppPathsID");

                entity.Property(e => e.AppName).HasMaxLength(145);

                entity.Property(e => e.AppPath).HasMaxLength(245);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblatccounter>(entity =>
            {
                entity.HasKey(e => e.Atcid)
                    .HasName("PK_tblatccounter_ATCID");

                entity.ToTable("tblatccounter");

                entity.HasIndex(e => e.Atcid)
                    .HasName("tblatccounter$ATCID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Atcid).HasColumnName("ATCID");

                entity.Property(e => e.CycleEndTime).HasColumnType("datetime");

                entity.Property(e => e.CycleStartTime).HasColumnType("datetime");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.PartCount).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Tblbottelneck>(entity =>
            {
                entity.HasKey(e => e.Bid)
                    .HasName("PK__tblbotte__C6D111C9159F7125");

                entity.ToTable("tblbottelneck");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CellName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Cpid).HasColumnName("cpid");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PlantId).HasColumnName("plantID");

                entity.Property(e => e.PlantName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblbottelneck)
                    .HasForeignKey(d => d.CellId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CellID");

                entity.HasOne(d => d.Cp)
                    .WithMany(p => p.Tblbottelneck)
                    .HasForeignKey(d => d.Cpid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cpid");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblbottelneck)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MachineID");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblbottelneck)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlantID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Tblbottelneck)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ShopID");
            });

            modelBuilder.Entity<Tblbreakdown>(entity =>
            {
                entity.HasKey(e => e.BreakdownId)
                    .HasName("PK_tblbreakdown_BreakdownID");

                entity.ToTable("tblbreakdown");

                entity.Property(e => e.BreakdownId).HasColumnName("BreakdownID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode).HasMaxLength(45);

                entity.Property(e => e.MessageDesc).HasMaxLength(45);

                entity.Property(e => e.Shift).HasMaxLength(45);

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.BreakDownCodeNavigation)
                    .WithMany(p => p.Tblbreakdown)
                    .HasForeignKey(d => d.BreakDownCode)
                    .HasConstraintName("FK_tblbreakdown_tbllossescodes");
            });

            modelBuilder.Entity<Tblcell>(entity =>
            {
                entity.HasKey(e => e.CellId)
                    .HasName("PK_tblcell_CellID");

                entity.ToTable("tblcell");

                entity.Property(e => e.CellId)
                    .HasColumnName("CellID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CellDesc)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.CellName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.CelldisplayName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblcell)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tblcell$PlantsId");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Tblcell)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblcell$ShopId");
            });

            modelBuilder.Entity<Tblcellpart>(entity =>
            {
                entity.HasKey(e => e.CpId)
                    .HasName("PK__tblcellp__2C1266AE4CF91034");

                entity.ToTable("tblcellpart");

                entity.Property(e => e.CpId).HasColumnName("cpID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PartDescription)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasColumnName("partNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblcellpart)
                    .HasForeignKey(d => d.CellId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CellID_FK");
            });

            modelBuilder.Entity<Tblcustomer>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK_tblcustomer_CID");

                entity.ToTable("tblcustomer");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AddressLine2).HasMaxLength(50);

                entity.Property(e => e.Cpdepartment)
                    .HasColumnName("CPDepartment")
                    .HasMaxLength(20);

                entity.Property(e => e.Cpdesignation)
                    .HasColumnName("CPDesignation")
                    .HasMaxLength(30);

                entity.Property(e => e.CpemailId)
                    .HasColumnName("CPEmailID")
                    .HasMaxLength(50);

                entity.Property(e => e.CpmobileNo).HasColumnName("CPMobileNO");

                entity.Property(e => e.Cpname)
                    .HasColumnName("CPName")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CustomerShortName).HasMaxLength(20);

                entity.Property(e => e.EmailId1)
                    .IsRequired()
                    .HasColumnName("EmailID1")
                    .HasMaxLength(30);

                entity.Property(e => e.EmailId2)
                    .HasColumnName("EmailID2")
                    .HasMaxLength(30);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Logo).IsRequired();

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Website).HasMaxLength(30);
            });

            modelBuilder.Entity<Tbldailyprodstatus>(entity =>
            {
                entity.ToTable("tbldailyprodstatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColorCode).HasMaxLength(45);

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Status).HasMaxLength(45);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tbldailyprodstatus)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("tbldailyprodstatus$MachineID1");
            });

            modelBuilder.Entity<Tbldaytiming>(entity =>
            {
                entity.ToTable("tbldaytiming");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblddl>(entity =>
            {
                entity.HasKey(e => e.Ddlid)
                    .HasName("PK__tblddl__47402305084DA64A");

                entity.ToTable("tblddl");

                entity.HasIndex(e => e.Ddlid)
                    .HasName("DDLID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Ddlid).HasColumnName("DDLID");

                entity.Property(e => e.Bhmiid)
                    .HasColumnName("BHMIID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DaysAgeing)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveredQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.DueDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FlagRushInd)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsWoselected)
                    .HasColumnName("IsWOSelected")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Maddate)
                    .HasColumnName("MADDate")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MaddateInd)
                    .HasColumnName("MADDateInd")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OperationDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OperationsOnHold)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PartName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreOpnEndDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Project)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonForHold)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ScrapQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TargetQty)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.WorkCenter)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.WorkOrder)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tbldowntimecategory>(entity =>
            {
                entity.HasKey(e => e.DtcId)
                    .HasName("PK_tbldowntimecategory_DTC_ID");

                entity.ToTable("tbldowntimecategory");

                entity.Property(e => e.DtcId).HasColumnName("DTC_ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Dtcategory)
                    .IsRequired()
                    .HasColumnName("DTCategory")
                    .HasMaxLength(50);

                entity.Property(e => e.DtcategoryDesc)
                    .IsRequired()
                    .HasColumnName("DTCategoryDesc")
                    .HasMaxLength(60);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tbldowntimedetails>(entity =>
            {
                entity.HasKey(e => e.DtId)
                    .HasName("PK_tbldowntimedetails_DT_ID");

                entity.ToTable("tbldowntimedetails");

                entity.Property(e => e.DtId).HasColumnName("DT_ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DtcategoryId).HasColumnName("DTCategoryID");

                entity.Property(e => e.Dtdesc)
                    .IsRequired()
                    .HasColumnName("DTDesc")
                    .HasMaxLength(50);

                entity.Property(e => e.Dtreason)
                    .IsRequired()
                    .HasColumnName("DTReason")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Dtcategory)
                    .WithMany(p => p.Tbldowntimedetails)
                    .HasForeignKey(d => d.DtcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbldowntimedetails$tbldowntimedetails_ibfk_1");
            });

            modelBuilder.Entity<Tblemailescalation>(entity =>
            {
                entity.HasKey(e => e.EmailEscalationId)
                    .HasName("PK_tblemailescalation_EMailEscalationID");

                entity.ToTable("tblemailescalation");

                entity.HasIndex(e => e.EmailEscalationId)
                    .HasName("tblemailescalation$EMailEscalationID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.EmailEscalationId).HasColumnName("EMailEscalationID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.EmailEscalationName)
                    .HasColumnName("EMailEscalationName")
                    .HasMaxLength(200);

                entity.Property(e => e.MessageType).HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.WorkCenterId).HasColumnName("WorkCenterID");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblemailescalation)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("tblemailescalation$CellIDFK");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblemailescalation)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tblemailescalation$PlantIDFK");

                entity.HasOne(d => d.ReasonLevel2Navigation)
                    .WithMany(p => p.TblemailescalationReasonLevel2Navigation)
                    .HasForeignKey(d => d.ReasonLevel2)
                    .HasConstraintName("FK_tblemailescalation_tbllossescodes1");

                entity.HasOne(d => d.ReasonLevel3Navigation)
                    .WithMany(p => p.TblemailescalationReasonLevel3Navigation)
                    .HasForeignKey(d => d.ReasonLevel3)
                    .HasConstraintName("FK_tblemailescalation_tbllossescodes2");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Tblemailescalation)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("tblemailescalation$ShopIDFK");

                entity.HasOne(d => d.WorkCenter)
                    .WithMany(p => p.Tblemailescalation)
                    .HasForeignKey(d => d.WorkCenterId)
                    .HasConstraintName("tblemailescalation$WCIDFK");
            });

            modelBuilder.Entity<Tblendcodes>(entity =>
            {
                entity.HasKey(e => e.EndCodeId)
                    .HasName("PK_tblendcodes_EndCodeID");

                entity.ToTable("tblendcodes");

                entity.Property(e => e.EndCodeId).HasColumnName("EndCodeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndCode).HasMaxLength(11);

                entity.Property(e => e.EndCodeLdesc)
                    .HasColumnName("EndCodeLDesc")
                    .HasMaxLength(200);

                entity.Property(e => e.EndCodeSdesc)
                    .HasColumnName("EndCodeSDesc")
                    .HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblescalationlog>(entity =>
            {
                entity.HasKey(e => e.Elid)
                    .HasName("PK_tblescalationlog_ELID");

                entity.ToTable("tblescalationlog");

                entity.HasIndex(e => e.Elid)
                    .HasName("tblescalationlog$ELID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Elid).HasColumnName("ELID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EscalationId).HasColumnName("EscalationID");

                entity.Property(e => e.EscalationSentOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.LossCodeId).HasColumnName("LossCodeID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PmsEscid).HasColumnName("PMS_ESCID");

                entity.Property(e => e.Pmsid).HasColumnName("PMSID");

                entity.Property(e => e.Wcid).HasColumnName("WCID");
            });

            modelBuilder.Entity<Tblgenericworkcodes>(entity =>
            {
                entity.HasKey(e => e.GenericWorkId)
                    .HasName("PK_tblgenericworkcodes_GenericWorkID");

                entity.ToTable("tblgenericworkcodes");

                entity.Property(e => e.GenericWorkId).HasColumnName("GenericWorkID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.GenericWorkCode).HasMaxLength(50);

                entity.Property(e => e.GenericWorkDesc).HasMaxLength(45);

                entity.Property(e => e.GwcodesLevel).HasColumnName("GWCodesLevel");

                entity.Property(e => e.GwcodesLevel1Id).HasColumnName("GWCodesLevel1ID");

                entity.Property(e => e.GwcodesLevel2Id).HasColumnName("GWCodesLevel2ID");

                entity.Property(e => e.MessageType).HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblgenericworkentry>(entity =>
            {
                entity.HasKey(e => e.GwentryId)
                    .HasName("PK_tblgenericworkentry_GWEntryID");

                entity.ToTable("tblgenericworkentry");

                entity.Property(e => e.GwentryId).HasColumnName("GWEntryID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Gwcode)
                    .HasColumnName("GWCode")
                    .HasMaxLength(45);

                entity.Property(e => e.GwcodeDesc)
                    .HasColumnName("GWCodeDesc")
                    .HasMaxLength(45);

                entity.Property(e => e.GwcodeId).HasColumnName("GWCodeID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.Shift).HasMaxLength(45);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.GwcodeNavigation)
                    .WithMany(p => p.Tblgenericworkentry)
                    .HasForeignKey(d => d.GwcodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblgenericworkentry$GenericWorkID");
            });

            modelBuilder.Entity<Tblhistpms>(entity =>
            {
                entity.ToTable("tblhistpms");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Pmchecklistname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Pmsid).HasColumnName("pmsid");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Workdone)
                    .HasColumnName("workdone")
                    .HasDefaultValueSql("('0')");
            });

            modelBuilder.Entity<Tblhmiscreen>(entity =>
            {
                entity.HasKey(e => e.Hmiid)
                    .HasName("PK__tblhmisc__B07815CA8F773C52");

                entity.ToTable("tblhmiscreen");

                entity.HasIndex(e => new { e.MachineId, e.CorrectedDate, e.IsWorkInProgress })
                    .HasName("HMImachineidindex");

                entity.HasIndex(e => new { e.MachineId, e.CorrectedDate, e.WorkOrderNo, e.PartNo, e.OperationNo })
                    .HasName("opnobased index");

                entity.Property(e => e.Hmiid)
                    .HasColumnName("HMIID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BatchCount).HasColumnName("batchCount");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime2(0)");

                entity.Property(e => e.DdlwokrCentre)
                    .HasColumnName("DDLWokrCentre")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveredQty).HasColumnName("Delivered_Qty");

                entity.Property(e => e.DoneWithRow).HasDefaultValueSql("('0')");

                entity.Property(e => e.Hmimonth).HasColumnName("HMIMonth");

                entity.Property(e => e.Hmiquarter).HasColumnName("HMIQuarter");

                entity.Property(e => e.HmiweekNumber).HasColumnName("HMIWeekNumber");

                entity.Property(e => e.Hmiyear).HasColumnName("HMIYear");

                entity.Property(e => e.IsHold).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsMultiWo)
                    .HasColumnName("IsMultiWO")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsSplitSapUpdated).HasColumnName("isSplitSapUpdated");

                entity.Property(e => e.IsUpdate)
                    .HasColumnName("isUpdate")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsWorkInProgress)
                    .HasColumnName("isWorkInProgress")
                    .HasDefaultValueSql("('2')");

                entity.Property(e => e.IsWorkOrder)
                    .HasColumnName("isWorkOrder")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OperatiorId).HasColumnName("OperatiorID");

                entity.Property(e => e.OperatorDet)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PestartTime)
                    .HasColumnName("PEStartTime")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.PrevQty).HasColumnName("prevQty");

                entity.Property(e => e.ProcessQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.ProdFai)
                    .HasColumnName("Prod_FAI")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Project)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.RejQty)
                    .HasColumnName("Rej_Qty")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.TargetQty).HasColumnName("Target_Qty");

                entity.Property(e => e.Time).HasColumnType("datetime2(0)");

                entity.Property(e => e.WorkOrderNo)
                    .HasColumnName("Work_Order_No")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblhmiscreen)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MachineIDhmi");
            });

            modelBuilder.Entity<Tblholdcodes>(entity =>
            {
                entity.HasKey(e => e.HoldCodeId)
                    .HasName("PK_tblholdcodes_HoldCodeID");

                entity.ToTable("tblholdcodes");

                entity.Property(e => e.HoldCodeId).HasColumnName("HoldCodeID");

                entity.Property(e => e.ContributeTo).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.HoldCode).HasMaxLength(100);

                entity.Property(e => e.HoldCodeDesc).HasMaxLength(100);

                entity.Property(e => e.HoldCodesLevel1Id).HasColumnName("HoldCodesLevel1ID");

                entity.Property(e => e.HoldCodesLevel2Id).HasColumnName("HoldCodesLevel2ID");

                entity.Property(e => e.MessageType).HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblholidays>(entity =>
            {
                entity.HasKey(e => e.HolidayId)
                    .HasName("PK_tblholidays_HolidayId");

                entity.ToTable("tblholidays");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.HolidayDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Reason).HasMaxLength(45);
            });

            modelBuilder.Entity<Tbllivedailyprodstatus>(entity =>
            {
                entity.ToTable("tbllivedailyprodstatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ColorCode).HasMaxLength(45);

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Status).HasMaxLength(45);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tbllivedailyprodstatus)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("tbllivedailyprodstatus$MachineIDLDPS");
            });

            modelBuilder.Entity<Tbllivehmiscreen>(entity =>
            {
                entity.HasKey(e => e.Hmiid)
                    .HasName("PK__tblliveh__B07815CA2EE88F09");

                entity.ToTable("tbllivehmiscreen");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.ActivityName).HasMaxLength(50);

                entity.Property(e => e.AutoBatchNumber)
                    .HasColumnName("autoBatchNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.BatchHmiid).HasColumnName("BatchHMIID");

                entity.Property(e => e.BatchNo).HasColumnName("batchNo");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime2(0)");

                entity.Property(e => e.DdlwokrCentre)
                    .HasColumnName("DDLWokrCentre")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveredQty).HasColumnName("Delivered_Qty");

                entity.Property(e => e.DoneWithRow).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsHold).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsMultiWo)
                    .HasColumnName("IsMultiWO")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsUpdate)
                    .HasColumnName("isUpdate")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsWorkInProgress)
                    .HasColumnName("isWorkInProgress")
                    .HasDefaultValueSql("('2')");

                entity.Property(e => e.IsWorkOrder)
                    .HasColumnName("isWorkOrder")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OperatiorId).HasColumnName("OperatiorID");

                entity.Property(e => e.OperatorDet)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PestartTime).HasColumnName("PEStartTime");

                entity.Property(e => e.PrevQty).HasColumnName("prevQty");

                entity.Property(e => e.ProcessQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.ProdFai)
                    .HasColumnName("Prod_FAI")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Project)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.RejQty)
                    .HasColumnName("Rej_Qty")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.TargetQty).HasColumnName("Target_Qty");

                entity.Property(e => e.Time).HasColumnType("datetime2(0)");

                entity.Property(e => e.WorkOrderNo)
                    .HasColumnName("Work_Order_No")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tbllivehmiscreen)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbllivehmiscreen_tblmachinedetails");
            });

            modelBuilder.Entity<Tbllivelossofentry>(entity =>
            {
                entity.HasKey(e => e.LossId)
                    .HasName("PK__tbllivel__7025E39412BAD000");

                entity.ToTable("tbllivelossofentry");

                entity.Property(e => e.LossId).HasColumnName("LossID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DoneWithRow).HasDefaultValueSql("('0')");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.EntryTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.ForRefresh).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsUpdate).HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCodeId).HasColumnName("MessageCodeID");

                entity.Property(e => e.MessageDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.MessageCodeNavigation)
                    .WithMany(p => p.Tbllivelossofentry)
                    .HasForeignKey(d => d.MessageCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbllivelossofentry_tbllossescodes");
            });

            modelBuilder.Entity<Tbllivemanuallossofentry>(entity =>
            {
                entity.HasKey(e => e.MlossId)
                    .HasName("PK__tbllivem__1ED730DE45B4F6E3");

                entity.ToTable("tbllivemanuallossofentry");

                entity.Property(e => e.MlossId).HasColumnName("MLossID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndHmiid).HasColumnName("EndHMIID");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCodeId).HasColumnName("MessageCodeID");

                entity.Property(e => e.MessageDesc)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Wono)
                    .HasColumnName("WONo")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tbllivemode>(entity =>
            {
                entity.HasKey(e => e.ModeId);

                entity.ToTable("tbllivemode");

                entity.HasIndex(e => new { e.ModeId, e.MachineId, e.MacMode, e.CorrectedDate, e.StartTime, e.EndTime, e.ColorCode, e.IsCompleted, e.DurationInSec, e.LossCodeId, e.BreakdownId, e.ModeType, e.ModeTypeEnd, e.StartIdle, e.TotalPartsCount })
                    .HasName("LivemodeIndexer")
                    .IsUnique();

                entity.Property(e => e.ModeId).HasColumnName("ModeID");

                entity.Property(e => e.BreakDownCodeId).HasColumnName("breakDownCodeID");

                entity.Property(e => e.BreakdownId).HasColumnName("BreakdownID");

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.CuttingDuration).HasDefaultValueSql("('0')");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsInserted).HasDefaultValueSql("('0')");

                entity.Property(e => e.LossCodeEnteredBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LossCodeEnteredTime).HasColumnType("datetime2(3)");

                entity.Property(e => e.LossCodeId).HasColumnName("LossCodeID");

                entity.Property(e => e.MacMode)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModeType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModeTypeEnd).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartIdle).HasDefaultValueSql("('0')");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.TotalPartsCount).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Breakdown)
                    .WithMany(p => p.Tbllivemode)
                    .HasForeignKey(d => d.BreakdownId)
                    .HasConstraintName("FK_tbllivemode_tblbreakdown");

                entity.HasOne(d => d.LossCode)
                    .WithMany(p => p.Tbllivemode)
                    .HasForeignKey(d => d.LossCodeId)
                    .HasConstraintName("FK_tbllivemode_tbllossescodes");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tbllivemode)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblmode_tblmachinedetails1");
            });

            modelBuilder.Entity<Tbllivemultiwoselection>(entity =>
            {
                entity.HasKey(e => e.MultiWoid)
                    .HasName("PK__tbllivem__22EB3C7E838BE2C0");

                entity.ToTable("tbllivemultiwoselection");

                entity.Property(e => e.MultiWoid).HasColumnName("MultiWOID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DdlworkCentre)
                    .HasColumnName("DDLWorkCentre")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsSubmit).HasDefaultValueSql("('0')");

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ScrapQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.WorkOrder)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hmi)
                    .WithMany(p => p.Tbllivemultiwoselection)
                    .HasForeignKey(d => d.Hmiid)
                    .HasConstraintName("fkwoliveHMIID");
            });

            modelBuilder.Entity<Tbllossescodes>(entity =>
            {
                entity.HasKey(e => e.LossCodeId)
                    .HasName("PK__tbllosse__9A1C17316AD81566");

                entity.ToTable("tbllossescodes");

                entity.Property(e => e.LossCodeId).HasColumnName("LossCodeID");

                entity.Property(e => e.ContributeTo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.LossCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LossCodeDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LossCodesLevel1Id).HasColumnName("LossCodesLevel1ID");

                entity.Property(e => e.LossCodesLevel2Id).HasColumnName("LossCodesLevel2ID");

                entity.Property(e => e.MessageType)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.OeecatId).HasColumnName("OEECatId");
            });

            modelBuilder.Entity<Tbllossofentry>(entity =>
            {
                entity.HasKey(e => e.LossId)
                    .HasName("PK__tbllosso__7025E39495D86E55");

                entity.ToTable("tbllossofentry");

                entity.Property(e => e.LossId)
                    .HasColumnName("LossID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DoneWithRow).HasDefaultValueSql("('0')");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.EntryTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.ForRefresh).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsUpdate).HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCodeId).HasColumnName("MessageCodeID");

                entity.Property(e => e.MessageDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.MessageCodeNavigation)
                    .WithMany(p => p.Tbllossofentry)
                    .HasForeignKey(d => d.MessageCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbllossofentry_tbllossescodes1");
            });

            modelBuilder.Entity<Tblmachineallocation>(entity =>
            {
                entity.ToTable("tblmachineallocation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblmachineallocation)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("macid");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Tblmachineallocation)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("shiftid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tblmachineallocation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("userid");
            });

            modelBuilder.Entity<Tblmachineaxisdetails>(entity =>
            {
                entity.HasKey(e => e.MacAxisId)
                    .HasName("PK_tblmachineaxisdetails_MacAxisID");

                entity.ToTable("tblmachineaxisdetails");

                entity.HasIndex(e => e.MacAxisId)
                    .HasName("tblmachineaxisdetails$MacAxisID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MacAxisId).HasColumnName("MacAxisID");

                entity.Property(e => e.AxisName).HasMaxLength(150);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblmachinecategory>(entity =>
            {
                entity.ToTable("tblmachinecategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblmachinedetails>(entity =>
            {
                entity.HasKey(e => e.MachineId)
                    .HasName("PK_tblmachinedetails_MachineID");

                entity.ToTable("tblmachinedetails");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.ControllerType).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EnableToolLife).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertedOn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsNormalWc)
                    .HasColumnName("IsNormalWC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPcb).HasColumnName("IsPCB");

                entity.Property(e => e.LossFlag).HasDefaultValueSql("((1))");

                entity.Property(e => e.MacConnName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MacType).HasMaxLength(45);

                entity.Property(e => e.MachineDescription).HasMaxLength(150);

                entity.Property(e => e.MachineDisplayName).HasMaxLength(100);

                entity.Property(e => e.MachineMake).HasMaxLength(50);

                entity.Property(e => e.MachineModel).HasMaxLength(50);

                entity.Property(e => e.MachineModelType).HasDefaultValueSql("((1))");

                entity.Property(e => e.MachineName).HasMaxLength(45);

                entity.Property(e => e.ManualWcid).HasColumnName("ManualWCID");

                entity.Property(e => e.ModelType).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ProgDbit)
                    .HasColumnName("ProgDBit")
                    .HasMaxLength(10);

                entity.Property(e => e.ProgramNum).HasMaxLength(10);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.ShopNo).HasMaxLength(200);

                entity.Property(e => e.SpindleAxis)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TabIpaddress)
                    .HasColumnName("TabIPAddress")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblmachinedetails)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("tblmachinedetails$CellIDtblmachinedetails");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblmachinedetails)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tblmachinedetails$PlantIDtblmachinedetails");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Tblmachinedetails)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("tblmachinedetails$ShopIDtblmachinedetails");
            });

            modelBuilder.Entity<Tblmailids>(entity =>
            {
                entity.HasKey(e => e.MailIdsId)
                    .HasName("PK_tblmailids_MailIDsID");

                entity.ToTable("tblmailids");

                entity.HasIndex(e => e.MailIdsId)
                    .HasName("tblmailids$MailIDsID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MailIdsId).HasColumnName("MailIDsID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasMaxLength(100);

                entity.Property(e => e.EmployeeCode).HasMaxLength(45);

                entity.Property(e => e.EmployeeContactNum).HasMaxLength(15);

                entity.Property(e => e.EmployeeDesignation).HasMaxLength(45);

                entity.Property(e => e.EmployeeName).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<TblmaintainanceProdBrDetails>(entity =>
            {
                entity.ToTable("tblmaintainanceProdBrDetails");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcceptanceTime)
                    .HasColumnName("acceptanceTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.BreakdownReasonDateTime)
                    .HasColumnName("breakdownReasonDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ClosureTime)
                    .HasColumnName("closureTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MachineName)
                    .HasColumnName("machineName")
                    .HasMaxLength(200);

                entity.Property(e => e.MaintainanceOperatorName)
                    .HasColumnName("maintainanceOperatorName")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MaintainanceRemarks)
                    .HasColumnName("maintainanceRemarks")
                    .HasMaxLength(500);

                entity.Property(e => e.MaintainanceStatus).HasColumnName("maintainanceStatus");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorName)
                    .HasColumnName("operatorName")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProductionRemarks)
                    .HasColumnName("productionRemarks")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductionStatus).HasColumnName("productionStatus");

                entity.Property(e => e.ReasonName)
                    .HasColumnName("reasonName")
                    .HasMaxLength(500);

                entity.Property(e => e.RejectReason)
                    .HasColumnName("rejectReason")
                    .HasMaxLength(500);

                entity.Property(e => e.RoleId).HasColumnName("roleId");
            });

            modelBuilder.Entity<Tblmanuallossofentry>(entity =>
            {
                entity.HasKey(e => e.MlossId)
                    .HasName("PK__tblmanua__1ED730DEB0036E86");

                entity.ToTable("tblmanuallossofentry");

                entity.Property(e => e.MlossId)
                    .HasColumnName("MLossID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndHmiid).HasColumnName("EndHMIID");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MessageCode)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.MessageCodeId).HasColumnName("MessageCodeID");

                entity.Property(e => e.MessageDesc)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Shift)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Wono)
                    .HasColumnName("WONo")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblmasterpartsStSw>(entity =>
            {
                entity.HasKey(e => e.Partsstswid)
                    .HasName("PK_tblmasterparts_st_sw_PARTSSTSWID");

                entity.ToTable("tblmasterparts_st_sw");

                entity.Property(e => e.Partsstswid).HasColumnName("PARTSSTSWID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.InputWeight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.InputWeightUnit).HasMaxLength(10);

                entity.Property(e => e.MaterialRemovedQty).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MaterialRemovedQtyUnit).HasMaxLength(10);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.OpNo).HasMaxLength(45);

                entity.Property(e => e.OutputWeight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.OutputWeightUnit).HasMaxLength(10);

                entity.Property(e => e.PartNo).HasMaxLength(45);

                entity.Property(e => e.StdChangeoverTime).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StdChangeoverTimeUnit).HasMaxLength(10);

                entity.Property(e => e.StdCuttingTime).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StdCuttingTimeUnit).HasMaxLength(10);

                entity.Property(e => e.StdSetupTime).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StdSetupTimeUnit).HasMaxLength(10);
            });

            modelBuilder.Entity<Tblmimics>(entity =>
            {
                entity.HasKey(e => e.Mid)
                    .HasName("PK_tblmimics_mid");

                entity.ToTable("tblmimics");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.Shift).HasMaxLength(45);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblmimics)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblmimics$MachineID");
            });

            modelBuilder.Entity<Tblmode>(entity =>
            {
                entity.HasKey(e => e.ModeId)
                    .HasName("PK_tblmode_ModeID");

                entity.ToTable("tblmode");

                entity.Property(e => e.ModeId)
                    .HasColumnName("ModeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BreakdownId).HasColumnName("BreakdownID");

                entity.Property(e => e.ColorCode).HasMaxLength(45);

              //  entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.DurationInSec).HasDefaultValueSql("((0))");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.LossCodeEnteredBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LossCodeEnteredTime).HasColumnType("datetime");

                entity.Property(e => e.LossCodeId).HasColumnName("LossCodeID");

                entity.Property(e => e.MacMode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModeType).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Breakdown)
                    .WithMany(p => p.TblmodeBreakdown)
                    .HasForeignKey(d => d.BreakdownId)
                    .HasConstraintName("FK_tblmode_tblmachinedetails");

                entity.HasOne(d => d.LossCode)
                    .WithMany(p => p.TblmodeLossCode)
                    .HasForeignKey(d => d.LossCodeId)
                    .HasConstraintName("FK_tblmode_tbllossescodes");
            });

            modelBuilder.Entity<Tblmodetemp>(entity =>
            {
                entity.HasKey(e => e.ModeId)
                    .HasName("PK_tblmodetemp_ModeID");

                entity.ToTable("tblmodetemp");

                entity.Property(e => e.ModeId).HasColumnName("ModeID");

                entity.Property(e => e.BreakdownId).HasColumnName("BreakdownID");

                entity.Property(e => e.ColorCode).HasMaxLength(45);

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.DurationInSec).HasDefaultValueSql("((0))");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.LossCodeEnteredTime).HasColumnType("datetime");

                entity.Property(e => e.LossCodeId).HasColumnName("LossCodeID");

                entity.Property(e => e.MacMode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModeType).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.TabModeId).HasColumnName("TabModeID");

                entity.HasOne(d => d.Breakdown)
                    .WithMany(p => p.Tblmodetemp)
                    .HasForeignKey(d => d.BreakdownId)
                    .HasConstraintName("FK_tblmodetemp_tblbreakdown");

                entity.HasOne(d => d.LossCode)
                    .WithMany(p => p.Tblmodetemp)
                    .HasForeignKey(d => d.LossCodeId)
                    .HasConstraintName("FK_tblmodetemp_tbllossescodes");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblmodetemp)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblmodetemp_tblmachinedetails");
            });

            modelBuilder.Entity<Tblmodulehelper>(entity =>
            {
                entity.ToTable("tblmodulehelper");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsAdded)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.IsAll)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(0x00)");

                entity.Property(e => e.IsEdited)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.IsHidden)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.IsReadonly)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.IsRemoved)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.ModuleId)
                    .HasColumnName("ModuleID")
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<Tblmodulemaster>(entity =>
            {
                entity.HasKey(e => e.ModuleId)
                    .HasName("PK_tblmodulemaster_ModuleID");

                entity.ToTable("tblmodulemaster");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EnableAdd).HasMaxLength(1);

                entity.Property(e => e.EnableDelete).HasMaxLength(1);

                entity.Property(e => e.EnableEdit).HasMaxLength(1);

                entity.Property(e => e.EnableReport).HasMaxLength(1);

                entity.Property(e => e.IsSuperAdmin).HasMaxLength(1);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<Tblmodules>(entity =>
            {
                entity.HasKey(e => e.ModuleId)
                    .HasName("PK_tblmodules_ModuleId");

                entity.ToTable("tblmodules");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Module).HasMaxLength(50);

                entity.Property(e => e.ModuleDesc).HasMaxLength(100);

                entity.Property(e => e.ModuleDispName).HasMaxLength(50);
            });

            modelBuilder.Entity<Tblmultipleworkorder>(entity =>
            {
                entity.HasKey(e => e.Mwoid)
                    .HasName("PK_tblmultipleworkorder_MWOID");

                entity.ToTable("tblmultipleworkorder");

                entity.Property(e => e.Mwoid).HasColumnName("MWOID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MultipleWodesc)
                    .HasColumnName("MultipleWODesc")
                    .HasMaxLength(200);

                entity.Property(e => e.MultipleWoname)
                    .IsRequired()
                    .HasColumnName("MultipleWOName")
                    .HasMaxLength(45);

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Wcid).HasColumnName("WCID");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblmultipleworkorder)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("tblmultipleworkorder$CellIDtblMWO");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblmultipleworkorder)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tblmultipleworkorder$PlantIDtblMWO");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Tblmultipleworkorder)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("tblmultipleworkorder$ShopIdtblMWO");

                entity.HasOne(d => d.Wc)
                    .WithMany(p => p.Tblmultipleworkorder)
                    .HasForeignKey(d => d.Wcid)
                    .HasConstraintName("tblmultipleworkorder$WCIDtblMWO");
            });

            modelBuilder.Entity<Tblnetworkdetailsforddl>(entity =>
            {
                entity.HasKey(e => e.Npfddlid)
                    .HasName("PK__tblnetwo__F454508FA5DA51BA");

                entity.ToTable("tblnetworkdetailsforddl");

                entity.HasIndex(e => e.Npfddlid)
                    .HasName("NPFDDLID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Npfddlid).HasColumnName("NPFDDLID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DomainName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.Isincddl).HasColumnName("ISINCDDL");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tbloeedashboardfinalvariables>(entity =>
            {
                entity.HasKey(e => e.OeedashboardId)
                    .HasName("PK_tbloeedashboardfinalvariables_OEEDashboardID");

                entity.ToTable("tbloeedashboardfinalvariables");

                entity.HasIndex(e => e.OeedashboardId)
                    .HasName("tbloeedashboardfinalvariables$OEEDashboardID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.OeedashboardId).HasColumnName("OEEDashboardID");

                entity.Property(e => e.Availability).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(65);

                entity.Property(e => e.IsOverallPlantWise).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOverallShopWise).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOverallWcwise)
                    .HasColumnName("IsOverallWCWise")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Loss1Name).HasMaxLength(45);

                entity.Property(e => e.Loss2Name).HasMaxLength(45);

                entity.Property(e => e.Loss3Name).HasMaxLength(45);

                entity.Property(e => e.Loss4Name).HasMaxLength(45);

                entity.Property(e => e.Loss5Name).HasMaxLength(45);

                entity.Property(e => e.Oee)
                    .HasColumnName("OEE")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Performance).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Quality).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.StartDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Wcid).HasColumnName("WCID");
            });

            modelBuilder.Entity<Tbloeedashboardvariables>(entity =>
            {
                entity.HasKey(e => e.OeevariablesId)
                    .HasName("PK_tbloeedashboardvariables_OEEVariablesID");

                entity.ToTable("tbloeedashboardvariables");

                entity.HasIndex(e => e.OeevariablesId)
                    .HasName("tbloeedashboardvariables$OEEVariablesID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StartDate)
                    .HasName("Correcteddateindex");

                entity.HasIndex(e => new { e.StartDate, e.CellId })
                    .HasName("Cellbasedindex");

                entity.Property(e => e.OeevariablesId).HasColumnName("OEEVariablesID");

                entity.Property(e => e.Blue).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DownTimeBreakdown).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Green).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Loss1Name).HasMaxLength(45);

                entity.Property(e => e.Loss2Name).HasMaxLength(45);

                entity.Property(e => e.Loss3Name).HasMaxLength(45);

                entity.Property(e => e.Loss4Name).HasMaxLength(45);

                entity.Property(e => e.Loss5Name).HasMaxLength(45);

                entity.Property(e => e.MinorLosses).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ReWotime)
                    .HasColumnName("ReWOTime")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Roalossess)
                    .HasColumnName("ROALossess")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ScrapQtyTime).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.SettingTime).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.StartDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.SummationOfSctvsPp)
                    .HasColumnName("SummationOfSCTvsPP")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Wcid).HasColumnName("WCID");
            });

            modelBuilder.Entity<Tbloeedashboardvariablestoday>(entity =>
            {
                entity.HasKey(e => e.OeevariablesId)
                    .HasName("PK_tbloeedashboardvariablestoday_OEEVariablesID");

                entity.ToTable("tbloeedashboardvariablestoday");

                entity.HasIndex(e => e.OeevariablesId)
                    .HasName("tbloeedashboardvariablestoday$OEEVariablesID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.OeevariablesId).HasColumnName("OEEVariablesID");

                entity.Property(e => e.Blue).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DownTimeBreakdown).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Green).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(45);

                entity.Property(e => e.Loss1Name).HasMaxLength(45);

                entity.Property(e => e.Loss2Name).HasMaxLength(45);

                entity.Property(e => e.Loss3Name).HasMaxLength(45);

                entity.Property(e => e.Loss4Name).HasMaxLength(45);

                entity.Property(e => e.Loss5Name).HasMaxLength(45);

                entity.Property(e => e.MinorLosses).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Partscount).HasColumnName("partscount");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ReWotime)
                    .HasColumnName("ReWOTime")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Roalossess)
                    .HasColumnName("ROALossess")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ScrapQtyTime).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.SettingTime).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.StartDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Stdcycletime)
                    .IsRequired()
                    .HasColumnName("stdcycletime")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SummationOfSctvsPp)
                    .HasColumnName("SummationOfSCTvsPP")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Wcid).HasColumnName("WCID");
            });

            modelBuilder.Entity<Tbloperatordetails>(entity =>
            {
                entity.HasKey(e => e.Opid);

                entity.ToTable("tbloperatordetails");

                entity.Property(e => e.Opid).HasColumnName("OPID");

                entity.Property(e => e.ContactNo)
                    .HasColumnName("contactNo")
                    .HasMaxLength(50);

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dept).HasMaxLength(50);

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employeeId")
                    .HasMaxLength(500);

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OperatorId)
                    .IsRequired()
                    .HasColumnName("OperatorID")
                    .HasMaxLength(50);

                entity.Property(e => e.OperatorMailId)
                    .HasColumnName("operatorMailId")
                    .HasMaxLength(500);

                entity.Property(e => e.OperatorName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tblpartlearningreport>(entity =>
            {
                entity.HasKey(e => e.PlreportId)
                    .HasName("PK_tblworeport_PLReportID");

                entity.ToTable("tblpartlearningreport");

                entity.HasIndex(e => e.PlreportId)
                    .HasName("tblworeport$PLReportID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PlreportId).HasColumnName("PLReportID");

                entity.Property(e => e.AvgCuttingTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Breakdown).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Fgcode)
                    .HasColumnName("FGCode")
                    .HasMaxLength(45);

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.Idle).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MinorLoss).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OpNo).HasMaxLength(6);

                entity.Property(e => e.PowerOff).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SettingTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.StdCycleTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.StdMinorLoss).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalNccuttingTime)
                    .HasColumnName("TotalNCCuttingTime")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TotalStdCycleTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TotalStdMinorLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Woefficiency)
                    .HasColumnName("WOEfficiency")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WorkOrderNo).HasMaxLength(45);
            });

            modelBuilder.Entity<Tblparts>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PK_tblparts_PartID");

                entity.ToTable("tblparts");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DrawingNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fgcode)
                    .IsRequired()
                    .HasColumnName("FGCode")
                    .HasMaxLength(50);

                entity.Property(e => e.IdealCycleTime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.OperationNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PartName).HasMaxLength(50);

                entity.Property(e => e.StdCycleTime).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StdLoadUnloadTime)
                    .HasColumnName("Std_Load_UnloadTime")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StdLoadingTime).HasColumnType("decimal(8, 3)");

                entity.Property(e => e.StdMinorLoss).HasMaxLength(50);

                entity.Property(e => e.StdSetupTime)
                    .HasColumnName("Std_SetupTime")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StdUnLoadingTime).HasColumnType("decimal(8, 3)");

                entity.HasOne(d => d.UnitDescNavigation)
                    .WithMany(p => p.Tblparts)
                    .HasForeignKey(d => d.UnitDesc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblparts_tblunit");
            });

            modelBuilder.Entity<Tblpartscountandcutting>(entity =>
            {
                entity.HasKey(e => e.Pcid)
                    .HasName("PK__tblparts__83E06A9F7BA7292C");

                entity.ToTable("tblpartscountandcutting");

                entity.Property(e => e.Pcid).HasColumnName("pcid");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PartsPerCyscleEnteredTime).HasColumnType("datetime");

                entity.Property(e => e.ShiftName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.WoTargetQty).HasColumnName("woTargetQty");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblpartscountandcutting)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MachineID_fk");
            });

            modelBuilder.Entity<Tblpartwisesp>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PK__tblpartw__7C3F0D50D8CDE1E4");

                entity.ToTable("tblpartwisesp");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PartName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblpiwebOee>(entity =>
            {
                entity.ToTable("tblpiweb_OEE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.Createdon).HasColumnType("datetime");

                entity.Property(e => e.Oeeuuid)
                    .HasColumnName("OEEuuid")
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<TblpiwebUtil>(entity =>
            {
                entity.ToTable("tblpiweb_util");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Correcteddate).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UtilUuid)
                    .HasColumnName("utilUuid")
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<Tblplannedbreak>(entity =>
            {
                entity.HasKey(e => e.BreakId)
                    .HasName("PK_tblplannedbreak_BreakID");

                entity.ToTable("tblplannedbreak");

                entity.Property(e => e.BreakId).HasColumnName("BreakID");

                entity.Property(e => e.BreakReason).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Tblplannedbreak)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("tblplannedbreak$shiftbreak");
            });

            modelBuilder.Entity<Tblplant>(entity =>
            {
                entity.HasKey(e => e.PlantId)
                    .HasName("PK_tblplant_PlantID");

                entity.ToTable("tblplant");

                entity.Property(e => e.PlantId)
                    .HasColumnName("PlantID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantDesc)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.PlantDisplayName).HasMaxLength(150);

                entity.Property(e => e.PlantName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Tblpmsdetails>(entity =>
            {
                entity.HasKey(e => e.Pmsid)
                    .HasName("PK__tblpmsde__1FBBE522503859F7");

                entity.ToTable("tblpmsdetails");

                entity.Property(e => e.Pmsid).HasColumnName("pmsid");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsSubmitted).HasDefaultValueSql("('0')");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PmendDate)
                    .HasColumnName("PMEndDate")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PmstartDate)
                    .HasColumnName("PMStartDate")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblpmsdetails)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblpmsdetails_tblmachinedetails");
            });

            modelBuilder.Entity<Tblpriorityalarms>(entity =>
            {
                entity.HasKey(e => e.AlarmId)
                    .HasName("PK_tblpriorityalarms_AlarmID");

                entity.ToTable("tblpriorityalarms");

                entity.Property(e => e.AlarmId).HasColumnName("AlarmID");

                entity.Property(e => e.AlarmDesc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AlarmGroup).HasMaxLength(50);

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.CreatedOn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsMailSent)
                    .HasColumnName("isMailSent")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Tblprogramtransferhistory>(entity =>
            {
                entity.HasKey(e => e.Pthid)
                    .HasName("PK_tblprogramtransferhistory_PTHID");

                entity.ToTable("tblprogramtransferhistory");

                entity.Property(e => e.Pthid).HasColumnName("PTHID");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ProgramName).HasMaxLength(150);

                entity.Property(e => e.ReturnTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.UploadedTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Tblrejectqty>(entity =>
            {
                entity.HasKey(e => e.Rqid)
                    .HasName("PK__tblrejec__E345435AAE1A69F1");

                entity.ToTable("tblrejectqty");

                entity.Property(e => e.Rqid).HasColumnName("RQID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('0')");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsFinished)
                    .HasColumnName("isFinished")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.RejectQty).HasDefaultValueSql("('0')");

                entity.Property(e => e.Rid).HasColumnName("RID");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Woid).HasColumnName("WOID");

                entity.HasOne(d => d.R)
                    .WithMany(p => p.Tblrejectqty)
                    .HasForeignKey(d => d.Rid)
                    .HasConstraintName("tblrejectqty_ibfk_1");
            });

            modelBuilder.Entity<Tblrejectreason>(entity =>
            {
                entity.HasKey(e => e.Rid)
                    .HasName("PK__tblrejec__CAFF4132403EDC8F");

                entity.ToTable("tblrejectreason");

                entity.Property(e => e.Rid).HasColumnName("RID");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('1')");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsTcf).HasColumnName("IsTCF");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("('1')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.RejectName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.RejectNameDesc)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblrejectreason)
                    .HasForeignKey(d => d.Cellid)
                    .HasConstraintName("tblrejectreason_ibfk_1");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblrejectreason)
                    .HasForeignKey(d => d.Machineid)
                    .HasConstraintName("tblrejectreason_ibfk_2");
            });

            modelBuilder.Entity<Tblreportholder>(entity =>
            {
                entity.HasKey(e => e.Rhid)
                    .HasName("PK_tblreportholder_RHID");

                entity.ToTable("tblreportholder");

                entity.Property(e => e.Rhid).HasColumnName("RHID");

                entity.Property(e => e.FromDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Shift).HasMaxLength(45);

                entity.Property(e => e.ShopNo).HasMaxLength(100);

                entity.Property(e => e.ToDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.WorkCenter).HasMaxLength(100);
            });

            modelBuilder.Entity<Tblrolemodulelink>(entity =>
            {
                entity.HasKey(e => e.Mrmlid)
                    .HasName("PK_tblrolemodulelink_MRMLID");

                entity.ToTable("tblrolemodulelink");

                entity.Property(e => e.Mrmlid).HasColumnName("MRMLID");

                entity.Property(e => e.EnableAdd).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.EnableDelete).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.EnableEdit).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.EnableReadOnly).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.EnableReport).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsSuperAdmin).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.IsVisible).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.InsertedByNavigation)
                    .WithMany(p => p.Tblrolemodulelink)
                    .HasForeignKey(d => d.InsertedBy)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tblrolemodulelink$InsertedBy_Id");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Tblrolemodulelink)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_tblrolemodulelink_tblmodules");

                entity.HasOne(d => d.ModuleNavigation)
                    .WithMany(p => p.Tblrolemodulelink)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_tblrolemodulelink_tblroles");
            });

            modelBuilder.Entity<Tblroles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_tblroles_Role_ID");

                entity.ToTable("tblroles");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.RoleDesc).HasMaxLength(45);

                entity.Property(e => e.RoleDisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Tblsendermailid>(entity =>
            {
                entity.HasKey(e => e.SeId)
                    .HasName("PK_tblsendermailid_SE_ID");

                entity.ToTable("tblsendermailid");

                entity.Property(e => e.SeId).HasColumnName("SE_ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrimaryMailId)
                    .IsRequired()
                    .HasColumnName("PrimaryMailID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblshiftBreaks>(entity =>
            {
                entity.HasKey(e => e.BreakId)
                    .HasName("PK_tblshift_breaks_BreakID");

                entity.ToTable("tblshift_breaks");

                entity.Property(e => e.BreakId).HasColumnName("BreakID");

                entity.Property(e => e.BreakReason).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
            });

            modelBuilder.Entity<TblshiftMstr>(entity =>
            {
                entity.HasKey(e => e.ShiftId)
                    .HasName("PK_tblshift_mstr_ShiftID");

                entity.ToTable("tblshift_mstr");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftName).HasMaxLength(45);
            });

            modelBuilder.Entity<Tblshiftdetails>(entity =>
            {
                entity.HasKey(e => e.ShiftDetailsId)
                    .HasName("PK_tblshiftdetails_ShiftDetailsID");

                entity.ToTable("tblshiftdetails");

                entity.Property(e => e.ShiftDetailsId).HasColumnName("ShiftDetailsID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsGshift)
                    .HasColumnName("IsGShift")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsShiftDetailsEdited).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftDetailsDesc).HasMaxLength(60);

                entity.Property(e => e.ShiftDetailsEditedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftDetailsName).HasMaxLength(30);

                entity.Property(e => e.ShiftMethodId).HasColumnName("ShiftMethodID");

                entity.HasOne(d => d.ShiftMethod)
                    .WithMany(p => p.Tblshiftdetails)
                    .HasForeignKey(d => d.ShiftMethodId)
                    .HasConstraintName("tblshiftdetails$ShiftMethodIDshiftmethod");
            });

            modelBuilder.Entity<TblshiftdetailsMachinewise>(entity =>
            {
                entity.HasKey(e => e.ShiftDetailsMacId)
                    .HasName("PK_tblshiftdetails_machinewise_ShiftDetailsMacID");

                entity.ToTable("tblshiftdetails_machinewise");

                entity.Property(e => e.ShiftDetailsMacId).HasColumnName("ShiftDetailsMacID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(30);

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.InsertedOn).HasMaxLength(50);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ShiftDetailsId).HasColumnName("ShiftDetailsID");

                entity.Property(e => e.ShiftName).HasMaxLength(30);

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.TblshiftdetailsMachinewise)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblshiftdetails_machinewise$MachineIDShiftWise");
            });

            modelBuilder.Entity<Tblshiftmethod>(entity =>
            {
                entity.HasKey(e => e.ShiftMethodId)
                    .HasName("PK_tblshiftmethod_ShiftMethodID");

                entity.ToTable("tblshiftmethod");

                entity.Property(e => e.ShiftMethodId).HasColumnName("ShiftMethodID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EditedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsShiftMethodEdited).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShiftMethodDesc)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.ShiftMethodName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Tblshiftplanner>(entity =>
            {
                entity.HasKey(e => e.ShiftPlannerId)
                    .HasName("PK_tblshiftplanner_ShiftPlannerID");

                entity.ToTable("tblshiftplanner");

                entity.Property(e => e.ShiftPlannerId).HasColumnName("ShiftPlannerID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IsPlanRemoved).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPlanStopped).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlanStoppedDate).HasColumnType("date");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShiftMethodId).HasColumnName("ShiftMethodID");

                entity.Property(e => e.ShiftPlannerDesc).HasMaxLength(100);

                entity.Property(e => e.ShiftPlannerName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Cell)
                    .WithMany(p => p.Tblshiftplanner)
                    .HasForeignKey(d => d.CellId)
                    .HasConstraintName("tblshiftplanner$CellIDshiftplanner");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblshiftplanner)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("tblshiftplanner$MachineIDshiftplanner");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblshiftplanner)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tblshiftplanner$PlantIDshiftplanner");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Tblshiftplanner)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("tblshiftplanner$ShopIDshiftplanner");
            });

            modelBuilder.Entity<Tblshop>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK_tblshop_ShopID");

                entity.ToTable("tblshop");

                entity.Property(e => e.ShopId)
                    .HasColumnName("ShopID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.ShopDesc)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Shopdisplayname).HasMaxLength(100);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tblshop)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("tblshop$PlantID");
            });

            modelBuilder.Entity<Tbltcflossofentry>(entity =>
            {
                entity.HasKey(e => e.Ncid);

                entity.ToTable("tbltcflossofentry");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsAccept).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsArroval).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsSplitDuration).HasColumnName("isSplitDuration");

                entity.Property(e => e.IsUpdate).HasDefaultValueSql("('0')");

                entity.Property(e => e.ReasonLevel1).HasMaxLength(50);

                entity.Property(e => e.ReasonLevel2).HasMaxLength(50);

                entity.Property(e => e.ReasonLevel3).HasMaxLength(50);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tbltoollifeoperator>(entity =>
            {
                entity.HasKey(e => e.ToolLifeId);

                entity.ToTable("tbltoollifeoperator");

                entity.Property(e => e.ToolLifeId).HasColumnName("ToolLifeID");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ResetReason)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ToolCtcode)
                    .HasColumnName("ToolCTCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToolIdadmin).HasColumnName("ToolIDAdmin");

                entity.Property(e => e.ToolName).HasMaxLength(150);

                entity.Property(e => e.ToolNo)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Toollifecounter).HasColumnName("toollifecounter");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tbltoollifeoperator)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbltoollifeoperator_tblmachinedetails");
            });

            modelBuilder.Entity<Tbltosapfilepath>(entity =>
            {
                entity.HasKey(e => e.ToSapfilePathId)
                    .HasName("PK_tbltosapfilepath_toSAPFilePathID");

                entity.ToTable("tbltosapfilepath");

                entity.Property(e => e.ToSapfilePathId).HasColumnName("toSAPFilePathID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Path).HasMaxLength(250);

                entity.Property(e => e.PathName).HasMaxLength(45);

                entity.Property(e => e.Tbltosapfilepathcol)
                    .HasColumnName("tbltosapfilepathcol")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Tbltosapshopnames>(entity =>
            {
                entity.HasKey(e => e.ToSapshopNamesId)
                    .HasName("PK_tbltosapshopnames_toSAPShopNamesID");

                entity.ToTable("tbltosapshopnames");

                entity.Property(e => e.ToSapshopNamesId).HasColumnName("toSAPShopNamesID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsSetupShown)
                    .HasColumnName("isSetupShown")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShopName).HasMaxLength(65);
            });

            modelBuilder.Entity<Tblunit>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK_tblunit_U_ID");

                entity.ToTable("tblunit");

                entity.HasIndex(e => e.UId)
                    .HasName("tblunit$U_ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UId).HasColumnName("U_ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitDesc)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tblusers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_tblusers_UserID");

                entity.ToTable("tblusers");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblusers)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tblusers$MachineFkforUser");

                entity.HasOne(d => d.PrimaryRoleNavigation)
                    .WithMany(p => p.TblusersPrimaryRoleNavigation)
                    .HasForeignKey(d => d.PrimaryRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblusers$PrimaryRole");

                entity.HasOne(d => d.SecondaryRoleNavigation)
                    .WithMany(p => p.TblusersSecondaryRoleNavigation)
                    .HasForeignKey(d => d.SecondaryRole)
                    .HasConstraintName("tblusers$SecondaryRole");
            });

            modelBuilder.Entity<Tblwolossess>(entity =>
            {
                entity.HasKey(e => e.WolossesId)
                    .HasName("PK_tblwolossess_WOLossesID");

                entity.ToTable("tblwolossess");

                entity.HasIndex(e => e.Hmiid)
                    .HasName("HMIIDIndex");

                entity.HasIndex(e => new { e.WolossesId, e.Hmiid })
                    .HasName("IX_tblwolossess");

                entity.Property(e => e.WolossesId).HasColumnName("WOLossesID");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.LossCodeLevel1Id).HasColumnName("LossCodeLevel1ID");

                entity.Property(e => e.LossCodeLevel1Name).HasMaxLength(145);

                entity.Property(e => e.LossCodeLevel2Id).HasColumnName("LossCodeLevel2ID");

                entity.Property(e => e.LossCodeLevel2Name).HasMaxLength(145);

                entity.Property(e => e.LossDuration).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.LossId).HasColumnName("LossID");

                entity.Property(e => e.LossName).HasMaxLength(145);
            });

            modelBuilder.Entity<TblwolossessBackup>(entity =>
            {
                entity.HasKey(e => e.WolossesBackupId)
                    .HasName("PK__tblwolossessBackup__3B0E3C5A4209E126");

                entity.ToTable("tblwolossessBackup");

                entity.Property(e => e.WolossesBackupId).HasColumnName("WOLossesBackupID");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.LossCodeLevel1Id).HasColumnName("LossCodeLevel1ID");

                entity.Property(e => e.LossCodeLevel1Name)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.LossCodeLevel2Id).HasColumnName("LossCodeLevel2ID");

                entity.Property(e => e.LossCodeLevel2Name)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.LossDuration).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.LossId).HasColumnName("LossID");

                entity.Property(e => e.LossName)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.Property(e => e.WoLossesId).HasColumnName("woLossesId");
            });

            modelBuilder.Entity<Tblworeport>(entity =>
            {
                entity.HasKey(e => e.WoreportId)
                    .HasName("PK_tblworeport_WOReportID");

                entity.ToTable("tblworeport");

                entity.HasIndex(e => e.Hmiid)
                    .HasName("woreportHMIIDindex");

                entity.HasIndex(e => e.WoreportId)
                    .HasName("tblworeport$WOReportID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.CorrectedDate, e.MachineId, e.Hmiid })
                    .HasName("Machineidindex");

                entity.Property(e => e.WoreportId).HasColumnName("WOReportID");

                entity.Property(e => e.BatchNo).HasColumnName("batchNo");

                entity.Property(e => e.Blue)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Breakdown).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CorrectedDate).HasMaxLength(45);

                entity.Property(e => e.CuttingTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.HoldReason).HasMaxLength(225);

                entity.Property(e => e.Idle).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsMultiWo).HasColumnName("IsMultiWO");

                entity.Property(e => e.IsNormalWc)
                    .HasColumnName("IsNormalWC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPf).HasColumnName("IsPF");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MinorLoss)
                    .HasColumnType("decimal(6, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Mrweight)
                    .HasColumnName("MRWeight")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.NccuttingTimePerPart)
                    .HasColumnName("NCCuttingTimePerPart")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.OpNo).HasMaxLength(6);

                entity.Property(e => e.OperatorName).HasMaxLength(45);

                entity.Property(e => e.PartNo).HasMaxLength(45);

                entity.Property(e => e.Program).HasMaxLength(45);

                entity.Property(e => e.ReWorkTime)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.RejectedReason).HasMaxLength(245);

                entity.Property(e => e.ScrapQtyTime)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.SelfInspection).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SettingTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Shift).HasMaxLength(4);

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(10);

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.SummationOfSctvsPp)
                    .HasColumnName("SummationOfSCTvsPP")
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TotalNccuttingTime)
                    .HasColumnName("TotalNCCuttingTime")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Type).HasMaxLength(45);

                entity.Property(e => e.Woefficiency)
                    .HasColumnName("WOEfficiency")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WorkOrderNo).HasMaxLength(45);
            });

            modelBuilder.Entity<TblworeportBackup>(entity =>
            {
                entity.HasKey(e => e.WoreportBackupId)
                    .HasName("PK__tblworepBackup__E4D0233D169095A2");

                entity.ToTable("tblworeportBackup");

                entity.HasIndex(e => e.WoreportBackupId)
                    .HasName("WOReportBackupID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.WoreportBackupId).HasColumnName("WOReportBackupID");

                entity.Property(e => e.BatchNo).HasColumnName("batchNo");

                entity.Property(e => e.Blue)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("('0.00')");

                entity.Property(e => e.Breakdown).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CorrectedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CuttingTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.HoldReason)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Idle).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.IsMultiWo)
                    .HasColumnName("IsMultiWO")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsNormalWc)
                    .HasColumnName("IsNormalWC")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsPf).HasColumnName("IsPF");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.MinorLoss)
                    .HasColumnType("decimal(6, 2)")
                    .HasDefaultValueSql("('0.00')");

                entity.Property(e => e.Mrweight)
                    .HasColumnName("MRWeight")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.NccuttingTimePerPart)
                    .HasColumnName("NCCuttingTimePerPart")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.OpNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Program)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReWorkTime)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("('0.00')");

                entity.Property(e => e.RejectedReason)
                    .HasMaxLength(245)
                    .IsUnicode(false);

                entity.Property(e => e.ScrapQtyTime)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("('0.00')");

                entity.Property(e => e.SelfInspection).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SettingTime).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Shift)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SplitWo)
                    .HasColumnName("SplitWO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.SummationOfSctvsPp)
                    .HasColumnName("SummationOfSCTvsPP")
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("('0.00')");

                entity.Property(e => e.TotalNccuttingTime)
                    .HasColumnName("TotalNCCuttingTime")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.WoReportId).HasColumnName("woReportID");

                entity.Property(e => e.Woefficiency)
                    .HasColumnName("WOEfficiency")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WorkOrderNo)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblworkorderentry>(entity =>
            {
                entity.HasKey(e => e.Hmiid);

                entity.ToTable("tblworkorderentry");

                entity.Property(e => e.Hmiid).HasColumnName("HMIID");

                entity.Property(e => e.CellId).HasColumnName("CellID");

                entity.Property(e => e.CorrectedDate).HasColumnType("date");

                entity.Property(e => e.Fgcode)
                    .HasColumnName("FGCode")
                    .HasMaxLength(50);

                entity.Property(e => e.HoldCodeId).HasColumnName("HoldCodeID");

                entity.Property(e => e.HoldTime).HasColumnType("datetime");

                entity.Property(e => e.IsMultiWo).HasColumnName("IsMultiWO");

                entity.Property(e => e.IsSplit).HasColumnName("isSplit");

                entity.Property(e => e.IsWorkOrder).HasColumnName("isWorkOrder");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.OperationNo)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OperatorId)
                    .IsRequired()
                    .HasColumnName("OperatorID")
                    .HasMaxLength(45);

                entity.Property(e => e.PartNo).HasMaxLength(45);

                entity.Property(e => e.PartsPerCycle).HasDefaultValueSql("((1))");

                entity.Property(e => e.PestartTime)
                    .HasColumnName("PEStartTime")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ProdOrderNo)
                    .IsRequired()
                    .HasColumnName("Prod_Order_No")
                    .HasMaxLength(45);

                entity.Property(e => e.ReWorkReasonId).HasColumnName("ReWorkReasonID");

                entity.Property(e => e.ReworkEndTime)
                    .HasColumnName("reworkEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReworkStartTime)
                    .HasColumnName("reworkStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.TotalQty).HasColumnName("Total_Qty");

                entity.Property(e => e.Woend)
                    .HasColumnName("WOEnd")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Wostart)
                    .HasColumnName("WOStart")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.YieldQty).HasColumnName("Yield_Qty");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Tblworkorderentry)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblworkorderentry_tblmachinedetails");
            });

            modelBuilder.Entity<TempTblmode>(entity =>
            {
                entity.HasKey(e => e.TempModeId);

                entity.ToTable("Temp_tblmode");

                entity.Property(e => e.TempModeId).HasColumnName("tempModeID");

                entity.Property(e => e.CorrectedDate).HasMaxLength(50);

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.MachineId).HasColumnName("MachineID");

                entity.Property(e => e.ModeType).HasMaxLength(50);

                entity.Property(e => e.StartTime)
                    .HasColumnName("startTime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Weekdata>(entity =>
            {
                entity.HasKey(e => e.WeekId)
                    .HasName("PK__weekdata__C814A5E16F9FA7F6");

                entity.ToTable("weekdata");

                entity.Property(e => e.WeekId).HasColumnName("WeekID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Isdeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime2(0)");

                entity.Property(e => e.Value).HasColumnName("value");
            });
        }
    }
}
