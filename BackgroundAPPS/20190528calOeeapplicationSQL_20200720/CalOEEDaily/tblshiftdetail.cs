//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalOEEDaily
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblshiftdetail
    {
        public int ShiftDetailsID { get; set; }
        public string ShiftDetailsName { get; set; }
        public string ShiftDetailsDesc { get; set; }
        public Nullable<int> ShiftMethodID { get; set; }
        public Nullable<System.TimeSpan> ShiftStartTime { get; set; }
        public Nullable<System.TimeSpan> ShiftEndTime { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> NextDay { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> IsShiftDetailsEdited { get; set; }
        public Nullable<System.DateTime> ShiftDetailsEditedDate { get; set; }
        public Nullable<int> IsGShift { get; set; }
    
        public virtual tblshiftmethod tblshiftmethod { get; set; }
    }
}
