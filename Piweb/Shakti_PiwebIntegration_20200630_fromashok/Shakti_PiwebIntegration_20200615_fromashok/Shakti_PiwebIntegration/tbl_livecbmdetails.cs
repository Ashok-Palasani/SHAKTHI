//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shakti_PiwebIntegration
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_livecbmdetails
    {
        public int cbmdetailsid { get; set; }
        public Nullable<int> MachineID { get; set; }
        public Nullable<int> SensorMasterID { get; set; }
        public Nullable<int> SensorValues { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CorrectedDate { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    }
}
