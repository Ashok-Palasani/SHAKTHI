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
    
    public partial class tblbreakdown
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblbreakdown()
        {
            this.tbllivemodes = new HashSet<tbllivemode>();
            this.tblmodetemps = new HashSet<tblmodetemp>();
        }
    
        public int BreakdownID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> BreakDownCode { get; set; }
        public int MachineID { get; set; }
        public string CorrectedDate { get; set; }
        public string Shift { get; set; }
        public string MessageDesc { get; set; }
        public string MessageCode { get; set; }
        public Nullable<int> DoneWithRow { get; set; }
    
        public virtual tbllossescode tbllossescode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbllivemode> tbllivemodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmodetemp> tblmodetemps { get; set; }
    }
}
