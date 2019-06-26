using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;

namespace Noor.Data
{
    public partial class News
    {
        public static News Clone(ArticleVM ViewModel)
        {
            return new News()
            {
                ID = ViewModel.ID,
                SecID = ViewModel.SecID != null ? ViewModel.SecID : 0,
                WrittenBy = ViewModel.WrittenBy,
                Title = ViewModel.Title,
                SubTitle = ViewModel.SubTitle,
                Brief = ViewModel.Brief,
                Story = ViewModel.Story,
                //AttachmentID = ViewModel.AttachmentID != null ? ViewModel.AttachmentID : 0,
                AttachmentID = ViewModel.AttachmentID,
                ImageCaption = ViewModel.ImageCaption,
                Notes = ViewModel.Notes,
                Cover = ViewModel.Cover != null ? ViewModel.Cover.ID : 0,
                CoverCaption = ViewModel.ImageCaption,
                Type = ViewModel.Type,
                Embed = ViewModel.Embed,
                //EditorID = ViewModel.EditorID != null ? ViewModel.EditorID : 0,
                RelatedNews = ViewModel.RelatedNews,
                Status = ViewModel.Status != null ? ViewModel.Status : 0,
                IsApproved = ViewModel.IsApproved,
                IsDeleted = ViewModel.IsDeleted,
                CDate = ViewModel.CDate,
                CUser = ViewModel.CUser,
                CurrentGroup = ViewModel.CurrentGroupID,
                CurrentUser = ViewModel.CurrentUserID,
            };
        }
    }
}
