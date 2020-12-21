//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MimicsUpdation.ServerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_autoreportsetting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_autoreportsetting()
        {
            this.tbl_autoreport_log = new HashSet<tbl_autoreport_log>();
        }
    
        public int AutoReportID { get; set; }
        public Nullable<int> ReportID { get; set; }
        public Nullable<int> BasedOn { get; set; }
        public Nullable<int> AutoReportTimeID { get; set; }
        public string ToMailList { get; set; }
        public string CCMailList { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> PlantID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> CellID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public Nullable<System.DateTime> NextRunDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_autoreport_log> tbl_autoreport_log { get; set; }
        public virtual tbl_autoreportbasedon tbl_autoreportbasedon { get; set; }
        public virtual tblcell tblcell { get; set; }
        public virtual tbl_reportmaster tbl_reportmaster { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual tblshop tblshop { get; set; }
        public virtual tbl_autoreporttime tbl_autoreporttime { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
