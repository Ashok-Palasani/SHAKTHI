//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MimicsUpdation.ServerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class configuration_tblprimitivemaintainancescheduling
    {
        public int pmid { get; set; }
        public int MachineID { get; set; }
        public string MachineName { get; set; }
        public string Month { get; set; }
        public Nullable<int> Week { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string CellName { get; set; }
        public int CellID { get; set; }
        public Nullable<int> PlantID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public string plantName { get; set; }
        public string shopname { get; set; }
        public int WeekID { get; set; }
        public Nullable<int> MonthID { get; set; }
    
        public virtual tblcell tblcell { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual tblshop tblshop { get; set; }
    }
}
