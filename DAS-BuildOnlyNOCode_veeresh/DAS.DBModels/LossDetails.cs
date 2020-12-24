using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class LossDetails
    {
        public int Lid { get; set; }
        public int Losscodeid { get; set; }
        public int Duration { get; set; }
        public DateTime? CorrectedDate { get; set; }
        public int MachineId { get; set; }
    }
}
