﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblpiwebOee
    {
        public int Id { get; set; }
        public int? Machineid { get; set; }
        public string CorrectedDate { get; set; }
        public string Oeeuuid { get; set; }
        public DateTime? Createdon { get; set; }
        public int DaystartValue { get; set; }
    }
}