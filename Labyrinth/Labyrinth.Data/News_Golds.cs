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
    
    public partial class News_Golds
    {
        public int ID { get; set; }
        public int NewsId { get; set; }
        public int GoldId { get; set; }
    
        public virtual Gold Gold { get; set; }
        public virtual News News { get; set; }
    }
}
