﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class Tblpartscountandcutting
    {
        public int Pcid { get; set; }
        public int MachineId { get; set; }
        public int PartCount { get; set; }
        public int CuttingTime { get; set; }
        public int TargetQuantity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Isdeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CorrectedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? PartsPerCyscleEnteredTime { get; set; }
        public int? WoPartCount { get; set; }
        public string ShiftName { get; set; }
        public int? CuttingTimeInSec { get; set; }
        public int? WoTargetQty { get; set; }

        public virtual Tblmachinedetails Machine { get; set; }
    }
}