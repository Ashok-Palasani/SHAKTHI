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
    
    public partial class tblmailid
    {
        public int MailIDsID { get; set; }
        public string EmployeeName { get; set; }
        public string EmailID { get; set; }
        public string EmployeeContactNum { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeDesignation { get; set; }
    }
}
