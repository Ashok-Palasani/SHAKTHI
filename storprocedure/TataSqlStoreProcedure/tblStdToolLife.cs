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
    
    public partial class tblStdToolLife
    {
        public int StdToolLifeId { get; set; }
        public string FGCode { get; set; }
        public string OperationNo { get; set; }
        public string ToolNo { get; set; }
        public string CTCode { get; set; }
        public string ToolName { get; set; }
        public int StdToolLife { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public int MachineID { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
