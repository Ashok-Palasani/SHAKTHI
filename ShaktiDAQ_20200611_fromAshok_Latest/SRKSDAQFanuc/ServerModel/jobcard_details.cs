//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRKSDAQFanuc.ServerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class jobcard_details
    {
        public int JobcardID { get; set; }
        public Nullable<int> slno { get; set; }
        public string EmpNo { get; set; }
        public string OpnIdleCode { get; set; }
        public string Workorderno { get; set; }
        public string Compcode { get; set; }
        public Nullable<int> Qtyprod { get; set; }
        public Nullable<int> QtyAccp { get; set; }
        public Nullable<int> QtyRej { get; set; }
        public Nullable<System.TimeSpan> Fromtime { get; set; }
        public Nullable<System.TimeSpan> Totime { get; set; }
        public string Totalhours { get; set; }
        public string MachineInvNumber { get; set; }
        public string Shift { get; set; }
        public string JobCardDate { get; set; }
        public Nullable<System.DateTime> Fromdatetime { get; set; }
    }
}