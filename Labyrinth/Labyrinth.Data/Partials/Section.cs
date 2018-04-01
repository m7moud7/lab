using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Data
{
    public partial class Section
    {
        public static Section Clone(SectionVM ViewModel)
        {
            return new Section()
            {
                ID = ViewModel.ID,
                Title = ViewModel.Title,
                AttachmentID = ViewModel.AttachmentId,
                Description = ViewModel.Description,
                FBPage = ViewModel.FBPage,
                TwitterPage = ViewModel.TwitterPage,
                GooglePlusPage = ViewModel.GooglePlusPage,
                DisplayOrder = (ViewModel.DisplayOrder > 0) ? ViewModel.DisplayOrder : 100000,
                CUser = ViewModel.CUser,
                CDate = ViewModel.CDate,
                IsDeleted = ViewModel.IsDeleted,
                AlbumID = ViewModel.AlbumID,
                ParentID = ViewModel.ParentID
            };
        }
    }
}
