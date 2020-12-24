using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblReworkReason
    {
        public int ReWorkId { get; set; }
        public string ReworkName { get; set; }
        public int? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
