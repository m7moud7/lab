//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Noor.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Section
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public Nullable<long> AttachmentID { get; set; }
        public string Description { get; set; }
        public string FBPage { get; set; }
        public string TwitterPage { get; set; }
        public string GooglePlusPage { get; set; }
        public int DisplayOrder { get; set; }
        public int CUser { get; set; }
        public System.DateTime CDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> AlbumID { get; set; }
        public int ParentID { get; set; }
    }
}
