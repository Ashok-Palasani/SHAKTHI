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
    
    public partial class tbl_servodetails
    {
        public int SDID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public string ServoAxis { get; set; }
        public Nullable<decimal> ServoLoadMeter { get; set; }
        public Nullable<decimal> LoadCurrent { get; set; }
        public Nullable<decimal> LoadCurrentAmp { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public Nullable<int> Insertedby { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
