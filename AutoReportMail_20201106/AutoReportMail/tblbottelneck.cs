//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoReportMail
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblbottelneck
    {
        public int Bid { get; set; }
        public string PlantName { get; set; }
        public string ShopName { get; set; }
        public string CellName { get; set; }
        public string MachineName { get; set; }
        public string PartNo { get; set; }
        public int plantID { get; set; }
        public int ShopID { get; set; }
        public int CellID { get; set; }
        public int MachineID { get; set; }
        public int cpid { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual tblcell tblcell { get; set; }
        public virtual tblcellpart tblcellpart { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual tblshop tblshop { get; set; }
    }
}
