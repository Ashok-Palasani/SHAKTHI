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
    
    public partial class tblddl
    {
        public int DDLID { get; set; }
        public string WorkCenter { get; set; }
        public string WorkOrder { get; set; }
        public string OperationNo { get; set; }
        public string OperationDesc { get; set; }
        public string MaterialDesc { get; set; }
        public string PartName { get; set; }
        public string TargetQty { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public string MADDate { get; set; }
        public string MADDateInd { get; set; }
        public string PreOpnEndDate { get; set; }
        public string DaysAgeing { get; set; }
        public string OperationsOnHold { get; set; }
        public string ReasonForHold { get; set; }
        public string DueDate { get; set; }
        public string FlagRushInd { get; set; }
        public string SplitWO { get; set; }
        public int IsCompleted { get; set; }
        public int IsDeleted { get; set; }
        public int DeliveredQty { get; set; }
        public int ScrapQty { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> IsWOSelected { get; set; }
        public Nullable<int> BHMIID { get; set; }
    }
}
