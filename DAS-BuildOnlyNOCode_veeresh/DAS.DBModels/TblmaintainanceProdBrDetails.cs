using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblmaintainanceProdBrDetails
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string MachineName { get; set; }
        public string ReasonName { get; set; }
        public string OperatorName { get; set; }
        public string MaintainanceOperatorName { get; set; }
        public DateTime? BreakdownReasonDateTime { get; set; }
        public string MaintainanceRemarks { get; set; }
        public string ProductionRemarks { get; set; }
        public bool? MaintainanceStatus { get; set; }
        public bool? ProductionStatus { get; set; }
        public DateTime? AcceptanceTime { get; set; }
        public DateTime? ClosureTime { get; set; }
        public string RejectReason { get; set; }
        public int? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
