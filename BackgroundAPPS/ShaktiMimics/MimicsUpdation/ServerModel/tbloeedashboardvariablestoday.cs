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
    
    public partial class tbloeedashboardvariablestoday
    {
        public int OEEVariablesID { get; set; }
        public Nullable<int> PlantID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> CellID { get; set; }
        public Nullable<int> WCID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> MinorLosses { get; set; }
        public Nullable<decimal> Blue { get; set; }
        public Nullable<decimal> Green { get; set; }
        public Nullable<decimal> SettingTime { get; set; }
        public Nullable<decimal> ROALossess { get; set; }
        public Nullable<decimal> DownTimeBreakdown { get; set; }
        public Nullable<decimal> SummationOfSCTvsPP { get; set; }
        public Nullable<decimal> ScrapQtyTime { get; set; }
        public Nullable<decimal> ReWOTime { get; set; }
        public string Loss1Name { get; set; }
        public Nullable<int> Loss1Value { get; set; }
        public string Loss2Name { get; set; }
        public Nullable<int> Loss2Value { get; set; }
        public string Loss3Name { get; set; }
        public Nullable<int> Loss3Value { get; set; }
        public string Loss4Name { get; set; }
        public Nullable<int> Loss4Value { get; set; }
        public string Loss5Name { get; set; }
        public Nullable<int> Loss5Value { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int IsDeleted { get; set; }
        public int IsToday { get; set; }
        public string IPAddress { get; set; }
    }
}
