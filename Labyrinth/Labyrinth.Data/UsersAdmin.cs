//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Labyrinth.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersAdmin
    {
        public int ID { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Profile_Picture { get; set; }
        public int Role { get; set; }
        public Nullable<int> CUser { get; set; }
        public System.DateTime CDate { get; set; }
    }
}
