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
    
    public partial class WorkFlowAttachment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkFlowAttachment()
        {
            this.News_WorkFlowAttachment = new HashSet<News_WorkFlowAttachment>();
            this.NewsVersion_WorkFlowAttachment = new HashSet<NewsVersion_WorkFlowAttachment>();
        }
    
        public long Id { get; set; }
        public Nullable<short> Type { get; set; }
        public string AltImage { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }
        public string Embed { get; set; }
        public bool IsPublish { get; set; }
        public string FolderName { get; set; }
        public int CUser { get; set; }
        public System.DateTime CDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News_WorkFlowAttachment> News_WorkFlowAttachment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsVersion_WorkFlowAttachment> NewsVersion_WorkFlowAttachment { get; set; }
    }
}
