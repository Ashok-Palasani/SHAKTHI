//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileWatcherForDDL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblrejectreason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblrejectreason()
        {
            this.tblrejectqties = new HashSet<tblrejectqty>();
        }
    
        public int RID { get; set; }
        public string RejectName { get; set; }
        public string RejectNameDesc { get; set; }
        public Nullable<int> isDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> Cellid { get; set; }
        public Nullable<int> Machineid { get; set; }
        public int IsMaint { get; set; }
        public int IsProd { get; set; }
        public int IsTCF { get; set; }
    
        public virtual tblcell tblcell { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblrejectqty> tblrejectqties { get; set; }
    }
}
