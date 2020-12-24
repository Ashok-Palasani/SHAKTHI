﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblOeedetails
    {
        public int Oeeid { get; set; }
        public int? MachineId { get; set; }
        public string CorrectedDate { get; set; }
        public decimal? Availability { get; set; }
        public decimal? Performance { get; set; }
        public decimal? Quality { get; set; }
        public decimal? Oee { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? IsDeleted { get; set; }
        public decimal? OperatingTimeinMin { get; set; }
        public int? TotalPartsCount { get; set; }
        public decimal? PerformanceFactor { get; set; }
        public decimal? TotalIdletimeinMin { get; set; }
        public decimal? PowerOffTimeInMinutes { get; set; }
        public decimal? PowerOnTimeInMinutes { get; set; }
        public decimal? TotalTimeInMinutes { get; set; }

        public virtual Tblmachinedetails Machine { get; set; }
    }
}