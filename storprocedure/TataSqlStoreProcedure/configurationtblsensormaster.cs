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
    
    public partial class configurationtblsensormaster
    {
        public int SMID { get; set; }
        public int IsAnalog { get; set; }
        public int Sid { get; set; }
        public int ChannelNo { get; set; }
        public Nullable<int> MemoryAddress { get; set; }
        public int Unitid { get; set; }
        public string SensorDesc { get; set; }
        public Nullable<decimal> scalingFactor { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> CountLow { get; set; }
        public Nullable<int> CountHigh { get; set; }
        public Nullable<int> sensorlimitLow { get; set; }
        public Nullable<int> sensorlimitHigh { get; set; }
        public int parametertypeid { get; set; }
    
        public virtual configuration_tblsensorgroup configuration_tblsensorgroup { get; set; }
        public virtual tblunit tblunit { get; set; }
    }
}
