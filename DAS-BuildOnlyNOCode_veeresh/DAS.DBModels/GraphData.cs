using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class GraphData
    {
        public int Gid { get; set; }
        public decimal? OperatingTime { get; set; }
        public decimal? PowerOff { get; set; }
        public decimal? MinTime { get; set; }
        public decimal? MinorLossTime { get; set; }
        public decimal? LossTime { get; set; }
        public int MachineId { get; set; }
        public string CorrectedDate { get; set; }
        public int? ScrapQty { get; set; }
        public decimal PerformanceFactor { get; set; }
        public int YeildQty { get; set; }
    }
}
