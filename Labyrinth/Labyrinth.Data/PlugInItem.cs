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
    
    public partial class PlugInItem
    {
        public int Id { get; set; }
        public int PlugInId { get; set; }
        public string Title { get; set; }
        public string AltImage { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public Nullable<int> Type { get; set; }
        public string Code { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
    
        public virtual PlugIn PlugIn { get; set; }
    }
}
