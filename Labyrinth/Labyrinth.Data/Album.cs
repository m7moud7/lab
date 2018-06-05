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
    
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            this.News_Albums = new HashSet<News_Albums>();
            this.NewsVersion_Albums = new HashSet<NewsVersion_Albums>();
            this.OrderAlbums = new HashSet<OrderAlbum>();
            this.OrderAttachments = new HashSet<OrderAttachment>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlbumOwner { get; set; }
        public Nullable<int> AlbumType { get; set; }
        public string Cover { get; set; }
        public string CoverCaption { get; set; }
        public Nullable<int> CUser { get; set; }
        public System.DateTime CDate { get; set; }
        public bool Isdeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News_Albums> News_Albums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsVersion_Albums> NewsVersion_Albums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderAlbum> OrderAlbums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderAttachment> OrderAttachments { get; set; }
    }
}