//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace i_facility_IdleHandlerWithOptimization
{
    using System;
    using System.Collections.Generic;
    
    public partial class configurationtblsensordatalink
    {
        public int ParameterTypeID { get; set; }
        public string Element { get; set; }
        public string SubElement { get; set; }
        public string Deterioration { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDesc { get; set; }
        public int IsAxis { get; set; }
        public int AxisID { get; set; }
        public Nullable<int> IsSensor { get; set; }
        public Nullable<decimal> LSL { get; set; }
        public Nullable<decimal> USL { get; set; }
        public int Unit { get; set; }
        public int LogFrequency { get; set; }
        public int LogFreqUnitID { get; set; }
        public Nullable<int> IsCycle { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual tbl_axisdet tbl_axisdet { get; set; }
    }
}
