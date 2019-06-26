using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;

namespace Noor.Data
{
    public partial class Editor
    {
        public static Editor Clone(EditorVM ViewModel)
        {
            return new Editor()
            {
                ID = ViewModel.ID,
                Name = ViewModel.Name,
                Email = ViewModel.Email,
                Description = ViewModel.Description,
                AttachmentID = ViewModel.AttachmentId,
                DisplayOrder = (ViewModel.DisplayOrder > 0) ? ViewModel.DisplayOrder : 100000,
                CUser = ViewModel.CUser,
                CDate = ViewModel.CDate,
                IsDeleted = ViewModel.IsDeleted,
            };
        }
    }
}
