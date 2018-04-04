using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Data
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
                Cover = ViewModel.CoverID != null ? ViewModel.CoverID : 0,
                CoverCaption = ViewModel.ImageCaptionCover,
                Type = ViewModel.Type,
                EditorID = ViewModel.EditorID != null ? ViewModel.EditorID : 0,
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
