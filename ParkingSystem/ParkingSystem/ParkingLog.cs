//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParkingSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParkingLog
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Imagecdn { get; set; }
        public string INAgentMACID { get; set; }
        public string OUTAgentMACID { get; set; }
        public string Status { get; set; }
        public string InGateSessionID { get; set; }
        public string OutGateSessionID { get; set; }
        public Nullable<System.DateTime> InTimeStamp { get; set; }
        public Nullable<System.DateTime> OutTimeStamp { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
    }
}
