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
    
    public partial class tblpartscountandcutting
    {
        public int pcid { get; set; }
        public int MachineID { get; set; }
        public int PartCount { get; set; }
        public int CuttingTime { get; set; }
        public int TargetQuantity { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int Isdeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> CorrectedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> PartsPerCyscleEnteredTime { get; set; }
        public Nullable<int> WoPartCount { get; set; }
        public string ShiftName { get; set; }
        public Nullable<int> CuttingTimeInSec { get; set; }
        public Nullable<int> woTargetQty { get; set; }
    
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
