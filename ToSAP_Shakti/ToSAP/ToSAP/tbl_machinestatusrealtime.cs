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
    
    public partial class tbl_machinestatusrealtime
    {
        public int MachineStatusID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public string MachineStatus { get; set; }
        public string MachineEmergency { get; set; }
        public string MachineAlarm { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public int IsDeleted { get; set; }
        public string CorrectedDate { get; set; }
    
        public virtual tbl_machinestatusrealtime tbl_machinestatusrealtime1 { get; set; }
        public virtual tbl_machinestatusrealtime tbl_machinestatusrealtime2 { get; set; }
    }
}
