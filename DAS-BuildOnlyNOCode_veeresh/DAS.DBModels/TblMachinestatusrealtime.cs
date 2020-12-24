using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblMachinestatusrealtime
    {
        public int MachineStatusId { get; set; }
        public int? MachineId { get; set; }
        public string MachineStatus { get; set; }
        public string MachineEmergency { get; set; }
        public string MachineAlarm { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int IsDeleted { get; set; }
        public string CorrectedDate { get; set; }
    }
}
