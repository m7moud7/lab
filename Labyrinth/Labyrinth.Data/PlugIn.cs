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
    
    public partial class PlugIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlugIn()
        {
            this.PlugInItems = new HashSet<PlugInItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string CSSClass { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlugInItem> PlugInItems { get; set; }
    }
}