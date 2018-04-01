﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Data
{
    public partial class Attachment
    {
        public static Attachment Clone(AttachmentVM ViewModel)
        {
            return new Attachment()
            {
                ID = ViewModel.ID,
                Type = ViewModel.Type,
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
