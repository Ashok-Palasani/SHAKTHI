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
    
    public partial class tblOperatorLoginDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOperatorLoginDetail()
        {
            this.tblBreakDownTickects = new HashSet<tblBreakDownTickect>();
            this.tblBreakDownTickects1 = new HashSet<tblBreakDownTickect>();
            this.tblOperatorMachineDetails = new HashSet<tblOperatorMachineDetail>();
        }
    
        public int operatorLoginId { get; set; }
        public string operatorUserName { get; set; }
        public string operatorPwd { get; set; }
        public Nullable<int> NumOfMachines { get; set; }
        public string operatorMobileNo { get; set; }
        public string operatorEmailId { get; set; }
        public Nullable<int> isDeleted { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<int> roleId { get; set; }
        public Nullable<int> operatorId { get; set; }
        public string operatorName { get; set; }
        public string reasons { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBreakDownTickect> tblBreakDownTickects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBreakDownTickect> tblBreakDownTickects1 { get; set; }
        public virtual tblrole tblrole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOperatorMachineDetail> tblOperatorMachineDetails { get; set; }
    }
}
