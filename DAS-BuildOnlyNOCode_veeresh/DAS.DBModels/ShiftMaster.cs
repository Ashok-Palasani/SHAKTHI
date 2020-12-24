﻿using System;
using System.Collections.Generic;

namespace DAS.DBModels
{
    public partial class ShiftMaster
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? InsertedOn { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? IsDeleted { get; set; }
        public int? Duration { get; set; }
    }
}