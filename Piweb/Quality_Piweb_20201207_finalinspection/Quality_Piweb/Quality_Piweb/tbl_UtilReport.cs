//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quality_PiwebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_UtilReport
    {
        public int UtilReportID { get; set; }
        public int MachineID { get; set; }
        public System.DateTime CorrectedDate { get; set; }
        public decimal TotalTime { get; set; }
        public decimal OperatingTime { get; set; }
        public decimal SetupTime { get; set; }
        public decimal MinorLossTime { get; set; }
        public decimal LossTime { get; set; }
        public decimal BDTime { get; set; }
        public decimal PowerOffTime { get; set; }
        public decimal UtilPercent { get; set; }
        public System.DateTime InsertedOn { get; set; }
        public decimal SetupMinorTime { get; set; }
    
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
