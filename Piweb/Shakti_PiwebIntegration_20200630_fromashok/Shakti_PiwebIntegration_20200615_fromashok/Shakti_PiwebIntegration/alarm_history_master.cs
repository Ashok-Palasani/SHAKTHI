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
    
    public partial class alarm_history_master
    {
        public int AlarmID { get; set; }
        public string AlarmMessage { get; set; }
        public System.DateTime AlarmDateTime { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public Nullable<int> MachineID { get; set; }
        public string Shift { get; set; }
        public string CorrectedDate { get; set; }
        public string ErrorNum { get; set; }
        public string Status { get; set; }
        public string DetailCode1 { get; set; }
        public string DetailCode2 { get; set; }
        public string DetailCode3 { get; set; }
        public string InterferedPart { get; set; }
        public string SystemHead { get; set; }
        public string AlarmNo { get; set; }
        public string Axis_No { get; set; }
        public string Axis_Num { get; set; }
        public string Abs_Pos { get; set; }
        public int IsPiWeb { get; set; }
    }
}
