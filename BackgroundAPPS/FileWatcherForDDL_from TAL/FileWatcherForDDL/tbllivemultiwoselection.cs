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
    
    public partial class tbllivemultiwoselection
    {
        public int MultiWOID { get; set; }
        public string WorkOrder { get; set; }
        public string PartNo { get; set; }
        public string OperationNo { get; set; }
        public Nullable<int> TargetQty { get; set; }
        public Nullable<int> DeliveredQty { get; set; }
        public Nullable<int> HMIID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> IsCompleted { get; set; }
        public Nullable<int> ProcessQty { get; set; }
        public string DDLWorkCentre { get; set; }
        public int IsSubmit { get; set; }
        public int ScrapQty { get; set; }
        public string SplitWO { get; set; }
    
        public virtual tbllivehmiscreen tbllivehmiscreen { get; set; }
    }
}
