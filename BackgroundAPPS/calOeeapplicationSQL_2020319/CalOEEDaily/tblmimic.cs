//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalOEEDaily
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblmimic
    {
        public int mid { get; set; }
        public int MachineOnTime { get; set; }
        public int OperatingTime { get; set; }
        public int SetupTime { get; set; }
        public int IdleTime { get; set; }
        public int MachineOffTime { get; set; }
        public int BreakdownTime { get; set; }
        public int MachineID { get; set; }
        public string Shift { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> IsSync { get; set; }
    
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
