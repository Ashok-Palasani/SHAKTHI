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
    
    public partial class tblProdPlanMaster
    {
        public int ProdPlanID { get; set; }
        public string WorkCenter { get; set; }
        public string Prod_Order_No { get; set; }
        public string OperationNo { get; set; }
        public string Status { get; set; }
        public string FGCode { get; set; }
        public int OrderQty { get; set; }
        public Nullable<int> SplitOrderQty { get; set; }
        public string QtyUnit { get; set; }
        public System.DateTime InsertedOn { get; set; }
    }
}
