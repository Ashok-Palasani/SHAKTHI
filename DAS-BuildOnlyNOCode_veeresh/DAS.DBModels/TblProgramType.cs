using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class TblProgramType
    {
        public int Pgid { get; set; }
        public string ProgramtypeName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int Isdeleted { get; set; }
    }
}
