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
    
    public partial class Serial_Channel
    {
        public int ID { get; set; }
        public int SerialID { get; set; }
        public int ChannelID { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Channel Channel { get; set; }
        public virtual Serial Serial { get; set; }
    }
}
