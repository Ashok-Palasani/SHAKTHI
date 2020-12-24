using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblpiwebUtil
    {
        public int Id { get; set; }
        public int? Machineid { get; set; }
        public string Correcteddate { get; set; }
        public string UtilUuid { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int DaystartValue { get; set; }
    }
}
