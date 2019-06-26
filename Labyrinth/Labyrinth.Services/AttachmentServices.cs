using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Abstracts;
using Noor.Model;
using Noor.Data;
using System.Data.Entity;

namespace Noor.Services
{
    public class AttachmentServices : IAttachment
    {
        ContextEntities _DB = new ContextEntities();

        public List<GeneralSettingsVM> GetSettings(string SetName, string SetKey)
        {
            try
            {
                return (from gs in _DB.GeneralSettings
                        where (string.IsNullOrEmpty(SetName) || gs.SetName.ToLower() == SetName.ToLower())
                        && (string.IsNullOrEmpty(SetKey) || gs.SetKey.ToLower() == SetKey.ToLower())
                        select new GeneralSettingsVM()
                        {
                            ID = gs.ID,
                            SetName = gs.SetName,
                            SetKey = gs.SetKey,
                            Val1 = gs.Val1,
                            Val2 = gs.Val2,
                            Val3 = gs.Val3,
                            Val4 = gs.Val4,
                            Val5 = gs.Val5,
                            Val6 = gs.Val6
                        }).ToList();
            }
            catch
            {
                return new List<GeneralSettingsVM>();
            }
        }

        public long Save(AttachmentVM ViewModel)
        {
            try
            {
                var model = Noor.Data.Attachment.Clone(ViewModel);

                if (ViewModel.ID > 0)
                    _DB.Entry(model).State = EntityState.Modified;
                else
                {
                    model.CDate = DateTime.Now;
                    _DB.Attachments.Add(model);
                }
                _DB.SaveChanges();
                return model.ID;
            }
            catch
            {
                return 0;
            }
        }


        public List<AttachmentVM> GatAllImage(int Take, int PageID, string Filter)
        {
            try
            {
                return (from Att in _DB.BN_GetAttachments(Filter, PageID, Take)
                        select new AttachmentVM()
                        {
                            ID = Att.ID,
                            Caption = Att.Caption,
                            ALtImage = Att.AltImage,
                            Path = Att.Path,
                            FolderName = Att.FolderName,
                            IsPublish = Att.IsPublish,
                            IsDeleted = Att.IsDeleted,
                            CDate = Att.CDate,
                            IsUsedToday = bool.Parse(Att.IsUsedToday)
                        }).ToList();
            }
            catch
            {
                return new List<AttachmentVM>();
            }
        }
    }
}
