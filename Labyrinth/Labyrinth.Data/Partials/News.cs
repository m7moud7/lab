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
                SecID = ViewModel.SecID,
                WrittenBy = ViewModel.WrittenBy,
                Title = ViewModel.Title,
                SubTitle = ViewModel.SubTitle,
                Brief = ViewModel.Brief,
                Story = ViewModel.Story,
                AttachmentID = ViewModel.AttachmentID,
                ImageCaption = ViewModel.ImageCaption,
                Notes = ViewModel.Notes,
                Type = ViewModel.Type,
                EditorID = ViewModel.EditorID,
                Status = ViewModel.Status,
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
