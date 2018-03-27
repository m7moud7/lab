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
        public static Section Clone(SectionVM viewmodel)
        {
            return new Section()
            {
                ID = viewmodel.ID,
                Title = viewmodel.Title,
                AttachmentID = viewmodel.AttachmentId,
                Description = viewmodel.Description,
                FBPage = viewmodel.FBPage,
                TwitterPage = viewmodel.TwitterPage,
                GooglePlusPage = viewmodel.GooglePlusPage,
                DisplayOrder = (viewmodel.DisplayOrder > 0) ? viewmodel.DisplayOrder : 100000,
                CUser = viewmodel.CUser,
                CDate = viewmodel.CDate,
                IsDeleted = viewmodel.IsDeleted,
                AlbumID = viewmodel.AlbumID,
                ParentID = viewmodel.ParentID
            };
        }
    }
}
