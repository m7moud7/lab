using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;

namespace Noor.Data
{
    public partial class Attachment
    {
        public static Attachment Clone(AttachmentVM ViewModel)
        {
            return new Attachment()
            {
                ID = ViewModel.ID,
                Type = ViewModel.Type.Value,
                AltImage = ViewModel.ALtImage,
                Caption = ViewModel.Caption,
                Path = ViewModel.Path,
                Notes = ViewModel.Notes,
                Embed = ViewModel.Embed,
                IsPublish = ViewModel.IsPublish,
                FolderName = ViewModel.FolderName,
                CDate = ViewModel.CDate,
                CUser = ViewModel.CUser,
                PicsData = ViewModel.PicsData,
                IsDeleted = ViewModel.IsDeleted
            };
        }
    }
}
