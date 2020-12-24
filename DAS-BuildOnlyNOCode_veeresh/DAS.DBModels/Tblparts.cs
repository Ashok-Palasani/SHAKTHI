﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class Tblparts
    {
        public int PartId { get; set; }
        public string Fgcode { get; set; }
        public string OperationNo { get; set; }
        public string PartName { get; set; }
        public decimal IdealCycleTime { get; set; }
        public int? PartsPerCycle { get; set; }
        public int UnitDesc { get; set; }
        public int IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string DrawingNo { get; set; }
        public DateTime? DeletedDate { get; set; }
        public decimal? StdLoadUnloadTime { get; set; }
        public decimal? StdSetupTime { get; set; }
        public int? MachineId { get; set; }
        public string StdMinorLoss { get; set; }
        public decimal? StdLoadingTime { get; set; }
        public decimal? StdUnLoadingTime { get; set; }
        public decimal? StdCycleTime { get; set; }

        public virtual Tblunit UnitDescNavigation { get; set; }
    }
}