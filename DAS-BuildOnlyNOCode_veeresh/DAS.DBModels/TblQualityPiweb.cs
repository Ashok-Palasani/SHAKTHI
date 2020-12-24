using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblQualityPiweb
    {
        public int QualityId { get; set; }
        public string PartNumber { get; set; }
        public int? OperationNum { get; set; }
        public int? PartIdentity { get; set; }
        public int? MachineId { get; set; }
        public string CorrectedDate { get; set; }
        public int IsPiweb { get; set; }
        public int? RejectedQty { get; set; }
        public int? TotalQty { get; set; }
        public int? ApprovedQty { get; set; }
        public string WorkOrderNumber { get; set; }
    }
}
