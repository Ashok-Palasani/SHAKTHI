//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quality_PiwebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblsendermailid
    {
        public int SE_ID { get; set; }
        public string PrimaryMailID { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public int AutoEmailType { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
}
