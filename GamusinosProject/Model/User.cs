//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamusinosProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public long id { get; set; }
        public string nick { get; set; }
        public string password { get; set; }
        public string recoverCode { get; set; }
        public Nullable<System.DateTime> lastLogin { get; set; }
        public Nullable<byte> online { get; set; }
        public long Player_id { get; set; }
    
        public virtual Player Player { get; set; }
    }
}
