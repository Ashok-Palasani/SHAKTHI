//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToSAP
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_SaveNCProgVer
    {
        public int NCProgVerID { get; set; }
        public int MachineID { get; set; }
        public string ProgramNo { get; set; }
        public int VersionNo { get; set; }
        public string ProgramData { get; set; }
        public System.DateTime InsertedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
