using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class AlarmHistoryMaster
    {
        public int AlarmId { get; set; }
        public string AlarmMessage { get; set; }
        public DateTime AlarmDateTime { get; set; }
        public DateTime? InsertedOn { get; set; }
        public int? MachineId { get; set; }
        public string Shift { get; set; }
        public string CorrectedDate { get; set; }
        public string ErrorNum { get; set; }
        public string Status { get; set; }
        public string DetailCode1 { get; set; }
        public string DetailCode2 { get; set; }
        public string DetailCode3 { get; set; }
        public string InterferedPart { get; set; }
        public string SystemHead { get; set; }
        public string AlarmNo { get; set; }
        public string AxisNo { get; set; }
        public string AxisNum { get; set; }
        public string AbsPos { get; set; }
        public int IsPiWeb { get; set; }
    }
}
