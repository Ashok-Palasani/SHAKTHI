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
    
    public partial class TblPMSNotification_Master
    {
        public int PMSId { get; set; }
        public Nullable<int> PlantID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> CellID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public string ToList { get; set; }
        public string CcList { get; set; }
        public string SMSContactList { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string NoOfDaysPrior { get; set; }
        public Nullable<int> Frequency { get; set; }
        public string Unit { get; set; }
    
        public virtual tblcell tblcell { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
        public virtual tblplant tblplant { get; set; }
        public virtual tblshop tblshop { get; set; }
    }
}
