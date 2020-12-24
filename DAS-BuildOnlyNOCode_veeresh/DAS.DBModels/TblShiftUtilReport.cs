using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblShiftUtilReport
    {
        public int ShiftUtilReportId { get; set; }
        public int MachineId { get; set; }
        public DateTime CorrectedDate { get; set; }
        public decimal TotalTime { get; set; }
        public decimal OperatingTime { get; set; }
        public decimal PowerOnTime { get; set; }
        public decimal CuttingTime { get; set; }
        public decimal OperatingTimePrecent { get; set; }
        public decimal PowerOnTimePrecent { get; set; }
        public decimal CuttingTimePrecent { get; set; }
        public DateTime InsertedOn { get; set; }
        public string Shift { get; set; }

        public virtual Tblmachinedetails Machine { get; set; }
    }
}
