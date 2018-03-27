using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Data
{
    public partial class Editor
    {
        public static Editor Clone(EditorVM viewmodel)
        {
            return new Editor()
            {
                ID = viewmodel.ID,
                Name = viewmodel.Name,
                Email = viewmodel.Email,
                Description = viewmodel.Description,
                AttachmentID = viewmodel.AttachmentId,
                DisplayOrder = (viewmodel.DisplayOrder > 0) ? viewmodel.DisplayOrder : 100000,
                CUser = viewmodel.CUser,
                CDate = viewmodel.CDate,
                IsDeleted = viewmodel.IsDeleted,
            };
        }
    }
}
