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
    
    public partial class tblToolCounter
    {
        public int ToolCounterID { get; set; }
        public int MachineID { get; set; }
        public string ToolNum { get; set; }
        public int LifeCounter { get; set; }
        public int TotalLifeCounter { get; set; }
        public int IsReset { get; set; }
        public int ResetCounter { get; set; }
        public int InsertedBy { get; set; }
        public System.DateTime InsertedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
