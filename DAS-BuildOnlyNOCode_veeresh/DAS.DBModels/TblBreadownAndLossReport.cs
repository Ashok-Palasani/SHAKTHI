using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblBreadownAndLossReport
    {
        public int Id { get; set; }
        public int? BreakDownId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? BreakDownAndLossCode { get; set; }
        public int? MachineId { get; set; }
        public string Shift { get; set; }
        public int? DoneWithRow { get; set; }
        public string CorrectedDate { get; set; }
        public DateTime? EntryTime { get; set; }
        public int? MessageCodeId { get; set; }
    }
}
