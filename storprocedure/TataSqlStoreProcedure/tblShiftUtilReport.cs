//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TataSqlStoreProcedure
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblShiftUtilReport
    {
        public int ShiftUtilReportID { get; set; }
        public int MachineID { get; set; }
        public System.DateTime CorrectedDate { get; set; }
        public decimal TotalTime { get; set; }
        public decimal OperatingTime { get; set; }
        public decimal PowerOnTime { get; set; }
        public decimal CuttingTime { get; set; }
        public decimal OperatingTimePrecent { get; set; }
        public decimal PowerOnTimePrecent { get; set; }
        public decimal CuttingTimePrecent { get; set; }
        public System.DateTime InsertedOn { get; set; }
        public string shift { get; set; }
    
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
