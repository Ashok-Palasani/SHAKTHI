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
    
    public partial class Temp_tblmode
    {
        public int tempModeID { get; set; }
        public Nullable<int> MachineID { get; set; }
        public string ModeType { get; set; }
        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<int> isdeleted { get; set; }
        public string CorrectedDate { get; set; }
    }
}
