//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quality_PiwebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblmachinedetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblmachinedetail()
        {
            this.configuration_tblprimitivemaintainancescheduling = new HashSet<configuration_tblprimitivemaintainancescheduling>();
            this.configurationtblmachinesensors = new HashSet<configurationtblmachinesensor>();
            this.tbl_autoreportsetting = new HashSet<tbl_autoreportsetting>();
            this.tbl_axisdet = new HashSet<tbl_axisdet>();
            this.tbl_CycleTimeAnalysis = new HashSet<tbl_CycleTimeAnalysis>();
            this.tbl_OEEDetails = new HashSet<tbl_OEEDetails>();
            this.tbl_ProdAndonDisp = new HashSet<tbl_ProdAndonDisp>();
            this.tbl_ProdManMachine = new HashSet<tbl_ProdManMachine>();
            this.tbl_ProdOrderLosses = new HashSet<tbl_ProdOrderLosses>();
            this.tbl_UtilReport = new HashSet<tbl_UtilReport>();
            this.tblbottelnecks = new HashSet<tblbottelneck>();
            this.tblBreakDownTickects = new HashSet<tblBreakDownTickect>();
            this.tbldailyprodstatus = new HashSet<tbldailyprodstatu>();
            this.tblemailescalations = new HashSet<tblemailescalation>();
            this.tblhmiscreens = new HashSet<tblhmiscreen>();
            this.tbllivedailyprodstatus = new HashSet<tbllivedailyprodstatu>();
            this.tbllivehmiscreens = new HashSet<tbllivehmiscreen>();
            this.tbllivemodes = new HashSet<tbllivemode>();
            this.tblmachineallocations = new HashSet<tblmachineallocation>();
            this.tblmodetemps = new HashSet<tblmodetemp>();
            this.tblNcProgramTransferMains = new HashSet<tblNcProgramTransferMain>();
            this.tblOperatorDashboards = new HashSet<tblOperatorDashboard>();
            this.tblOperatorHeaders = new HashSet<tblOperatorHeader>();
            this.tblOperatorMachineDetails = new HashSet<tblOperatorMachineDetail>();
            this.tblpmsdetails = new HashSet<tblpmsdetail>();
            this.TblPMSNotification_Master = new HashSet<TblPMSNotification_Master>();
            this.tblSetupMaints = new HashSet<tblSetupMaint>();
            this.tblShiftUtilReports = new HashSet<tblShiftUtilReport>();
            this.tbltoollifeoperators = new HashSet<tbltoollifeoperator>();
            this.tblworkorderentries = new HashSet<tblworkorderentry>();
            this.tblpartscountandcuttings = new HashSet<tblpartscountandcutting>();
            this.tblmimics = new HashSet<tblmimic>();
            this.tblmultipleworkorders = new HashSet<tblmultipleworkorder>();
            this.tblrejectreasons = new HashSet<tblrejectreason>();
            this.tblshiftdetails_machinewise = new HashSet<tblshiftdetails_machinewise>();
            this.tblshiftplanners = new HashSet<tblshiftplanner>();
            this.tblusers = new HashSet<tbluser>();
        }
    
        public int MachineID { get; set; }
        public string InsertedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> PlantID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> CellID { get; set; }
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public string MachineDisplayName { get; set; }
        public Nullable<int> CellOrderNo { get; set; }
        public string IPAddress { get; set; }
        public Nullable<int> MachineType { get; set; }
        public string ControllerType { get; set; }
        public string MachineModel { get; set; }
        public string MachineMake { get; set; }
        public string ModelType { get; set; }
        public Nullable<int> IsParameters { get; set; }
        public string ShopNo { get; set; }
        public Nullable<int> IsPCB { get; set; }
        public Nullable<int> IsLevel { get; set; }
        public Nullable<int> IsNormalWC { get; set; }
        public Nullable<int> ManualWCID { get; set; }
        public Nullable<int> NoOfAxis { get; set; }
        public string MacType { get; set; }
        public Nullable<int> CurrentControlAxis { get; set; }
        public string ProgramNum { get; set; }
        public string ProgDBit { get; set; }
        public int MachineModelType { get; set; }
        public string MacConnName { get; set; }
        public string SpindleAxis { get; set; }
        public string TabIPAddress { get; set; }
        public Nullable<int> MachineLockBit { get; set; }
        public Nullable<int> MachineSetupBit { get; set; }
        public Nullable<int> MachineMaintBit { get; set; }
        public Nullable<int> MachineToolLifeBit { get; set; }
        public Nullable<int> MachineUnlockBit { get; set; }
        public Nullable<int> MachineIdleBit { get; set; }
        public Nullable<int> MachineIdleMin { get; set; }
        public int EnableLockLogic { get; set; }
        public int ServerTabFlagSync { get; set; }
        public int ServerTabCheck { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> EnableToolLife { get; set; }
        public Nullable<int> IsBottelNeck { get; set; }
        public Nullable<int> IsFirstMachine { get; set; }
        public Nullable<int> IsLastMachine { get; set; }
        public Nullable<int> OperationNumber { get; set; }
        public int IsShiftWise { get; set; }
        public Nullable<int> LossFlag { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<configuration_tblprimitivemaintainancescheduling> configuration_tblprimitivemaintainancescheduling { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<configurationtblmachinesensor> configurationtblmachinesensors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_autoreportsetting> tbl_autoreportsetting { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_axisdet> tbl_axisdet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CycleTimeAnalysis> tbl_CycleTimeAnalysis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OEEDetails> tbl_OEEDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ProdAndonDisp> tbl_ProdAndonDisp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ProdManMachine> tbl_ProdManMachine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ProdOrderLosses> tbl_ProdOrderLosses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UtilReport> tbl_UtilReport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbottelneck> tblbottelnecks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBreakDownTickect> tblBreakDownTickects { get; set; }
        public virtual tblcell tblcell { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbldailyprodstatu> tbldailyprodstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblemailescalation> tblemailescalations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblhmiscreen> tblhmiscreens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbllivedailyprodstatu> tbllivedailyprodstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbllivehmiscreen> tbllivehmiscreens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbllivemode> tbllivemodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmachineallocation> tblmachineallocations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmodetemp> tblmodetemps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblNcProgramTransferMain> tblNcProgramTransferMains { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOperatorDashboard> tblOperatorDashboards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOperatorHeader> tblOperatorHeaders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOperatorMachineDetail> tblOperatorMachineDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpmsdetail> tblpmsdetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblPMSNotification_Master> TblPMSNotification_Master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSetupMaint> tblSetupMaints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblShiftUtilReport> tblShiftUtilReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbltoollifeoperator> tbltoollifeoperators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblworkorderentry> tblworkorderentries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpartscountandcutting> tblpartscountandcuttings { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual tblshop tblshop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmimic> tblmimics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmultipleworkorder> tblmultipleworkorders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblrejectreason> tblrejectreasons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblshiftdetails_machinewise> tblshiftdetails_machinewise { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblshiftplanner> tblshiftplanners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbluser> tblusers { get; set; }
    }
}
