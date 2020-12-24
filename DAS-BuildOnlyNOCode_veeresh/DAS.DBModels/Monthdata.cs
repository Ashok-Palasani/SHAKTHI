﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class Monthdata
    {
        public int MonthId { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public int? Isdeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}