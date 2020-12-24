using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class QualtiyRawPiweb
    {
        public int QualityId { get; set; }
        public string PartNumber { get; set; }
        public string WorkOrderNumber { get; set; }
        public int? OperationNumber { get; set; }
        public int? Op10 { get; set; }
        public int? Op20 { get; set; }
        public int? Op30 { get; set; }
        public string PartIdentity { get; set; }
        public int? MachineId { get; set; }
        public string CorrectedDate { get; set; }
        public int? Status { get; set; }
        public int? IsPiweb { get; set; }
        public DateTime? MeasDateTime { get; set; }
    }
}
