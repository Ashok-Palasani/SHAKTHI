//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToSAP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Graph_Data
    {
        public int Gid { get; set; }
        public Nullable<decimal> OperatingTime { get; set; }
        public Nullable<decimal> PowerOFF { get; set; }
        public Nullable<decimal> MinTime { get; set; }
        public Nullable<decimal> MinorLossTime { get; set; }
        public Nullable<decimal> LossTime { get; set; }
        public int MachineID { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> ScrapQty { get; set; }
        public decimal PerformanceFactor { get; set; }
        public int YeildQty { get; set; }
    }
}
