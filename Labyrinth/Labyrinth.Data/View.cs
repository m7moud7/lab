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
    
    public partial class View
    {
        public int Id { get; set; }
        public Nullable<int> DirectionId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> SecId { get; set; }
        public Nullable<int> NewsNo { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    
        public virtual ViewsDirection ViewsDirection { get; set; }
        public virtual ViewsType ViewsType { get; set; }
    }
}
