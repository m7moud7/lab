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
    
    public partial class OrderLevel
    {
        public int ID { get; set; }
        public string LevelName { get; set; }
        public string AliasLevelName { get; set; }
        public int NewsCount { get; set; }
        public Nullable<int> TransitionLevelID { get; set; }
        public Nullable<int> SecIdInOrder { get; set; }
    }
}