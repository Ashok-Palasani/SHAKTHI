//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRKSDemo.Server_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblcell
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblcell()
        {
            this.configuration_tblpmchecklist = new HashSet<configuration_tblpmchecklist>();
            this.configuration_tblpmcheckpoint = new HashSet<configuration_tblpmcheckpoint>();
            this.configuration_tblprimitivemaintainancescheduling = new HashSet<configuration_tblprimitivemaintainancescheduling>();
            this.tbl_autoreportsetting = new HashSet<tbl_autoreportsetting>();
            this.tblAndonDispDets = new HashSet<tblAndonDispDet>();
            this.tblAndonImageTextScheduledDisplays = new HashSet<tblAndonImageTextScheduledDisplay>();
            this.tblbottelnecks = new HashSet<tblbottelneck>();
            this.tblcellparts = new HashSet<tblcellpart>();
            this.tblemailescalations = new HashSet<tblemailescalation>();
            this.tblmachinedetails = new HashSet<tblmachinedetail>();
            this.tblmultipleworkorders = new HashSet<tblmultipleworkorder>();
            this.tblrejectreasons = new HashSet<tblrejectreason>();
            this.tblshiftplanners = new HashSet<tblshiftplanner>();
            this.TblPMSNotification_Master = new HashSet<TblPMSNotification_Master>();
        }
    
        public int CellID { get; set; }
        public string CellName { get; set; }
        public string CellDesc { get; set; }
        public Nullable<int> PlantID { get; set; }
        public int ShopID { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string CelldisplayName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<configuration_tblpmchecklist> configuration_tblpmchecklist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<configuration_tblpmcheckpoint> configuration_tblpmcheckpoint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<configuration_tblprimitivemaintainancescheduling> configuration_tblprimitivemaintainancescheduling { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_autoreportsetting> tbl_autoreportsetting { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAndonDispDet> tblAndonDispDets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAndonImageTextScheduledDisplay> tblAndonImageTextScheduledDisplays { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbottelneck> tblbottelnecks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblcellpart> tblcellparts { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual tblshop tblshop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblemailescalation> tblemailescalations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmachinedetail> tblmachinedetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmultipleworkorder> tblmultipleworkorders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblrejectreason> tblrejectreasons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblshiftplanner> tblshiftplanners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblPMSNotification_Master> TblPMSNotification_Master { get; set; }
    }
}
