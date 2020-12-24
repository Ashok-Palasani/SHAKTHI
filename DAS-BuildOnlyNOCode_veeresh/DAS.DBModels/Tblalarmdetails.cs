using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class Tblalarmdetails
    {
        public int AlarmId { get; set; }
        public string AlarmDesc { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int IsDeleted { get; set; }
        public string AlarmNumber { get; set; }
    }
}
