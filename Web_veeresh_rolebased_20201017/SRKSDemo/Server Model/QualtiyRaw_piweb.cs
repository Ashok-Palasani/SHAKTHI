//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRKSDemo.Server_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class QualtiyRaw_piweb
    {
        public int QualityID { get; set; }
        public string PartNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public Nullable<int> OperationNumber { get; set; }
        public Nullable<int> OP10 { get; set; }
        public Nullable<int> OP20 { get; set; }
        public Nullable<int> OP30 { get; set; }
        public string PartIdentity { get; set; }
        public Nullable<int> MachineID { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsPiweb { get; set; }
        public Nullable<System.DateTime> Meas_DateTime { get; set; }
        public Nullable<int> OP40 { get; set; }
    }
}
