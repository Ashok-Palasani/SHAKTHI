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
    
    public partial class tblshiftdetails_machinewise
    {
        public int ShiftDetailsMacID { get; set; }
        public int MachineID { get; set; }
        public string ShiftName { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public string InsertedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<int> ShiftDetailsID { get; set; }
    
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}