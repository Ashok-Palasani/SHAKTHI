//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRKSDAQFanuc.ServerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class configurationtblmachinesensor
    {
        public int MSID { get; set; }
        public int MachineId { get; set; }
        public int Sid { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public int PortNo { get; set; }
    
        public virtual configuration_tblsensorgroup configuration_tblsensorgroup { get; set; }
        public virtual tblmachinedetail tblmachinedetail { get; set; }
    }
}
