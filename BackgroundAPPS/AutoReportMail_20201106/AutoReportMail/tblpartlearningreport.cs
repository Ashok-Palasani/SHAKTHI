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
    
    public partial class tblpartlearningreport
    {
        public int PLReportID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public Nullable<int> HMIID { get; set; }
        public string CorrectedDate { get; set; }
        public string WorkOrderNo { get; set; }
        public string FGCode { get; set; }
        public string OpNo { get; set; }
        public Nullable<int> TargetQty { get; set; }
        public Nullable<int> TotalQty { get; set; }
        public Nullable<int> YieldQty { get; set; }
        public Nullable<int> ScrapQty { get; set; }
        public Nullable<decimal> SettingTime { get; set; }
        public Nullable<decimal> Idle { get; set; }
        public Nullable<decimal> MinorLoss { get; set; }
        public Nullable<decimal> PowerOff { get; set; }
        public Nullable<decimal> Breakdown { get; set; }
        public Nullable<decimal> TotalNCCuttingTime { get; set; }
        public Nullable<decimal> AvgCuttingTime { get; set; }
        public Nullable<decimal> StdCycleTime { get; set; }
        public Nullable<decimal> TotalStdCycleTime { get; set; }
        public Nullable<decimal> WOEfficiency { get; set; }
        public Nullable<decimal> StdMinorLoss { get; set; }
        public Nullable<decimal> TotalStdMinorLoss { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    }
}
