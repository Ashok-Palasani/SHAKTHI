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
    
    public partial class tbllivemode
    {
        public int ModeID { get; set; }
        public int MachineID { get; set; }
        public string MacMode { get; set; }
        public System.DateTime InsertedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime CorrectedDate { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string ColorCode { get; set; }
        public int IsCompleted { get; set; }
        public Nullable<int> DurationInSec { get; set; }
        public Nullable<int> LossCodeID { get; set; }
        public Nullable<int> BreakdownID { get; set; }
        public string ModeType { get; set; }
        public int ModeTypeEnd { get; set; }
        public int StartIdle { get; set; }
        public Nullable<System.DateTime> LossCodeEnteredTime { get; set; }
        public string LossCodeEnteredBy { get; set; }
        public int IsInserted { get; set; }
        public Nullable<int> TotalPartsCount { get; set; }
        public Nullable<int> CuttingDuration { get; set; }
        public int IsShiftEnd { get; set; }
        public Nullable<int> breakDownCodeID { get; set; }
    
        public virtual tblbreakdown tblbreakdown { get; set; }
        public virtual tbllossescode tbllossescode { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
