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
    
    public partial class tblactivitylog
    {
        public int TrackID { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string IpAddress { get; set; }
        public string ClientSystemName { get; set; }
        public string CompleteModificationDetails { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Idoperationlog { get; set; }
        public string OpTime { get; set; }
        public System.DateTime OpDate { get; set; }
        public System.DateTime OpDateTime { get; set; }
    }
}
