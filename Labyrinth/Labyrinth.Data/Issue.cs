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
    
    public partial class Issue
    {
        public int Id { get; set; }
        public string PDFName { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
    }
}
