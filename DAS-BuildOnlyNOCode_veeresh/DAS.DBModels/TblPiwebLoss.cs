using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblPiwebLoss
    {
        public int PkLossUuid { get; set; }
        public int? MachineId { get; set; }
        public string CorrectedDate { get; set; }
        public string LossUuid { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int DaystartValue { get; set; }
        public int IsDeleted { get; set; }
    }
}
