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
    
    public partial class Attachment
    {
        public long ID { get; set; }
        public Nullable<short> Type { get; set; }
        public string AltImage { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }
        public string Embed { get; set; }
        public bool IsPublish { get; set; }
        public string FolderName { get; set; }
        public Nullable<int> CUser { get; set; }
        public System.DateTime CDate { get; set; }
        public string PicsData { get; set; }
        public bool IsDeleted { get; set; }
    }
}
