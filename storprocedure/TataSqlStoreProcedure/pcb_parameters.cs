//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TataSqlStoreProcedure
{
    using System;
    using System.Collections.Generic;
    
    public partial class pcb_parameters
    {
        public int ParameterID { get; set; }
        public string ParameterType { get; set; }
        public string Description { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public int PinNumber { get; set; }
        public string LogFile { get; set; }
        public int MachineID { get; set; }
        public int HighValue { get; set; }
        public string ColorCode { get; set; }
        public int IsPulse { get; set; }
    }
}
