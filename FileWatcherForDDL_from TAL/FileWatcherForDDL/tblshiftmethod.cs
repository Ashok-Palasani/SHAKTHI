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
    
    public partial class tblshiftmethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblshiftmethod()
        {
            this.tblshiftdetails = new HashSet<tblshiftdetail>();
        }
    
        public int ShiftMethodID { get; set; }
        public string ShiftMethodName { get; set; }
        public string ShiftMethodDesc { get; set; }
        public int NoOfShifts { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> IsShiftMethodEdited { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblshiftdetail> tblshiftdetails { get; set; }
    }
}
