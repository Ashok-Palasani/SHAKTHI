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
    
    public partial class tblrejectqty
    {
        public int RQID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> RID { get; set; }
        public Nullable<int> WOID { get; set; }
        public Nullable<int> isFinished { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<int> ShiftID { get; set; }
        public Nullable<int> RejectQty { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> isDeleted { get; set; }
    
        public virtual tblrejectreason tblrejectreason { get; set; }
    }
}
