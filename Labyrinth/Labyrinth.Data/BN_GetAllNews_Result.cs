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
    
    public partial class BN_GetAllNews_Result
    {
        public int ID { get; set; }
        public Nullable<int> Type { get; set; }
        public string Title { get; set; }
        public int SecID { get; set; }
        public string SecTitle { get; set; }
        public Nullable<int> EditorID { get; set; }
        public string EditorName { get; set; }
        public string Notes { get; set; }
        public Nullable<int> CurrentUser { get; set; }
        public Nullable<int> CurrentGroup { get; set; }
        public Nullable<int> CUser { get; set; }
        public System.DateTime CDate { get; set; }
        public Nullable<System.DateTime> SchdeuledPublish { get; set; }
        public int Status { get; set; }
    }
}
