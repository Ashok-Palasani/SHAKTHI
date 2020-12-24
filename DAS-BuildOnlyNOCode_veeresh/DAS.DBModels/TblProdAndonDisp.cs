﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblProdAndonDisp
    {
        public int ProdDashboardId { get; set; }
        public int MachineId { get; set; }
        public DateTime CorrectedDate { get; set; }
        public int Woid { get; set; }
        public decimal? UtilPercent { get; set; }
        public decimal TotalLoss { get; set; }
        public decimal TotalSetup { get; set; }
        public decimal TotalOperatingTime { get; set; }
        public DateTime InsertedOn { get; set; }
        public string WorkOrderNo { get; set; }

        public virtual Tblmachinedetails Machine { get; set; }
        public virtual Tbllivehmiscreen Wo { get; set; }
    }
}