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
    
    public partial class tblprogramtransferhistory
    {
        public int PTHID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string ProgramName { get; set; }
        public Nullable<System.DateTime> UploadedTime { get; set; }
        public Nullable<System.DateTime> ReturnTime { get; set; }
        public Nullable<int> ReturnStatus { get; set; }
        public string ReturnDesc { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public int IsCompleted { get; set; }
        public Nullable<int> Version { get; set; }
    }
}
