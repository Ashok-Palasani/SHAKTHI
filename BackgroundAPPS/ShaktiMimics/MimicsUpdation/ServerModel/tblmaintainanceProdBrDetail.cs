//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MimicsUpdation.ServerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblmaintainanceProdBrDetail
    {
        public int id { get; set; }
        public Nullable<int> roleId { get; set; }
        public string machineName { get; set; }
        public string reasonName { get; set; }
        public string operatorName { get; set; }
        public string maintainanceOperatorName { get; set; }
        public Nullable<System.DateTime> breakdownReasonDateTime { get; set; }
        public string maintainanceRemarks { get; set; }
        public string productionRemarks { get; set; }
        public Nullable<bool> maintainanceStatus { get; set; }
        public Nullable<bool> productionStatus { get; set; }
        public Nullable<System.DateTime> acceptanceTime { get; set; }
        public Nullable<System.DateTime> closureTime { get; set; }
        public string rejectReason { get; set; }
        public Nullable<int> isDeleted { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<System.DateTime> modifiedOn { get; set; }
    }
}
