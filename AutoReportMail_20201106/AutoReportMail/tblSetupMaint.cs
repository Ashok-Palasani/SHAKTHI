//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoReportMail
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSetupMaint
    {
        public int SetMainID { get; set; }
        public int LossCodeID { get; set; }
        public System.DateTime StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public int IsCompleted { get; set; }
        public int ModeID { get; set; }
        public int IsStarted { get; set; }
        public int IsSetup { get; set; }
        public int MachineID { get; set; }
        public int DurationInSec { get; set; }
        public int MinorLossTime { get; set; }
        public int Sync { get; set; }
        public Nullable<int> ServerSetMainID { get; set; }
    
        public virtual tbllossescode tbllossescode { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
