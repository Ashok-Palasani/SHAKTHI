using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TempTblmode
    {
        public int TempModeId { get; set; }
        public int? MachineId { get; set; }
        public string ModeType { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Isdeleted { get; set; }
        public string CorrectedDate { get; set; }
    }
}
