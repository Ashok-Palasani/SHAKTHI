//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalOEEDaily
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblwolossessBackup
    {
        public int WOLossesBackupID { get; set; }
        public Nullable<int> woLossesId { get; set; }
        public Nullable<int> HMIID { get; set; }
        public Nullable<int> LossID { get; set; }
        public string LossName { get; set; }
        public Nullable<decimal> LossDuration { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<int> LossCodeLevel1ID { get; set; }
        public string LossCodeLevel1Name { get; set; }
        public Nullable<int> LossCodeLevel2ID { get; set; }
        public string LossCodeLevel2Name { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
