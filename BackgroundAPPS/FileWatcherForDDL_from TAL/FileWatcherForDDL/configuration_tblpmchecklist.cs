//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileWatcherForDDL
{
    using System;
    using System.Collections.Generic;
    
    public partial class configuration_tblpmchecklist
    {
        public int pmcid { get; set; }
        public int pmcpID { get; set; }
        public string CheckList { get; set; }
        public string Frequency { get; set; }
        public int CellID { get; set; }
        public string Value { get; set; }
        public Nullable<int> Isdeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public int ShopID { get; set; }
        public int PlantID { get; set; }
        public string How { get; set; }
    
        public virtual tblcell tblcell { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual configuration_tblpmcheckpoint configuration_tblpmcheckpoint { get; set; }
        public virtual tblshop tblshop { get; set; }
    }
}
