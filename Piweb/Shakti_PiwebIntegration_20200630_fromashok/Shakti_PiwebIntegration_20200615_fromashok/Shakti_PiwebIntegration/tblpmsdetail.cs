//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shakti_PiwebIntegration
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblpmsdetail
    {
        public int pmsid { get; set; }
        public int MachineID { get; set; }
        public string PMStartDate { get; set; }
        public string PMEndDate { get; set; }
        public Nullable<int> IsCompleted { get; set; }
        public Nullable<int> IsSubmitted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
