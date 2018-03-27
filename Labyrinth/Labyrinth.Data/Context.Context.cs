﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ContextEntities : DbContext
    {
        public ContextEntities()
            : base("name=ContextEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UsersAdmin> UsersAdmins { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<OrderLevel> OrderLevels { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<News_Tags> News_Tags { get; set; }
        public virtual DbSet<Editor> Editors { get; set; }
        public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
    
        public virtual ObjectResult<BN_CheckUser_Result> BN_CheckUser(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_CheckUser_Result>("BN_CheckUser", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<BN_GetAllEditors_Result> BN_GetAllEditors(Nullable<int> take, Nullable<int> pageID, string isDeleted, string title)
        {
            var takeParameter = take.HasValue ?
                new ObjectParameter("Take", take) :
                new ObjectParameter("Take", typeof(int));
    
            var pageIDParameter = pageID.HasValue ?
                new ObjectParameter("PageID", pageID) :
                new ObjectParameter("PageID", typeof(int));
    
            var isDeletedParameter = isDeleted != null ?
                new ObjectParameter("IsDeleted", isDeleted) :
                new ObjectParameter("IsDeleted", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetAllEditors_Result>("BN_GetAllEditors", takeParameter, pageIDParameter, isDeletedParameter, titleParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> BN_GetAllEditors_Count(string isDeleted, string title)
        {
            var isDeletedParameter = isDeleted != null ?
                new ObjectParameter("IsDeleted", isDeleted) :
                new ObjectParameter("IsDeleted", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("BN_GetAllEditors_Count", isDeletedParameter, titleParameter);
        }
    
        public virtual ObjectResult<BN_GetAllEditors_DDL_Result> BN_GetAllEditors_DDL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetAllEditors_DDL_Result>("BN_GetAllEditors_DDL");
        }
    
        public virtual ObjectResult<BN_GetAllSections_Result> BN_GetAllSections(Nullable<int> take, Nullable<int> pageID, string isDeleted, string title)
        {
            var takeParameter = take.HasValue ?
                new ObjectParameter("Take", take) :
                new ObjectParameter("Take", typeof(int));
    
            var pageIDParameter = pageID.HasValue ?
                new ObjectParameter("PageID", pageID) :
                new ObjectParameter("PageID", typeof(int));
    
            var isDeletedParameter = isDeleted != null ?
                new ObjectParameter("IsDeleted", isDeleted) :
                new ObjectParameter("IsDeleted", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetAllSections_Result>("BN_GetAllSections", takeParameter, pageIDParameter, isDeletedParameter, titleParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> BN_GetAllSections_Count(string isDeleted, string title)
        {
            var isDeletedParameter = isDeleted != null ?
                new ObjectParameter("IsDeleted", isDeleted) :
                new ObjectParameter("IsDeleted", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("BN_GetAllSections_Count", isDeletedParameter, titleParameter);
        }
    
        public virtual ObjectResult<BN_GetAllSections_DDL_Result> BN_GetAllSections_DDL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetAllSections_DDL_Result>("BN_GetAllSections_DDL");
        }
    
        public virtual ObjectResult<BN_GetAllSections_ForReOrder_Result> BN_GetAllSections_ForReOrder()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetAllSections_ForReOrder_Result>("BN_GetAllSections_ForReOrder");
        }
    
        public virtual ObjectResult<BN_GetNewsTags_Result> BN_GetNewsTags(Nullable<int> newsID)
        {
            var newsIDParameter = newsID.HasValue ?
                new ObjectParameter("NewsID", newsID) :
                new ObjectParameter("NewsID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetNewsTags_Result>("BN_GetNewsTags", newsIDParameter);
        }
    
        public virtual ObjectResult<BN_GetAllEditors_ForReOrder_Result> BN_GetAllEditors_ForReOrder()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BN_GetAllEditors_ForReOrder_Result>("BN_GetAllEditors_ForReOrder");
        }
    }
}
