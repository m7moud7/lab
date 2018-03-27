using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;
using Labyrinth.Abstracts;
using Labyrinth.Data;
using System.Data.Entity;

namespace Labyrinth.Services
{
    public class EditorServices : IEditor
    {
        ContextEntities _DB = new ContextEntities();

        /// Insert & Update Editor
        public int Save(EditorVM viewmodel)
        {
            try
            {
                var model = Editor.Clone(viewmodel);
                if (viewmodel.ID > 0)
                    _DB.Entry(model).State = EntityState.Modified;
                else
                    _DB.Editors.Add(model);

                _DB.SaveChanges();
                return model.ID;
            }
            catch
            {
                return 0;
            }
        }

        /// Delete Editor ==> Change IsDeleted
        public string Delete(int ID, bool Type)
        {
            try
            {
                var model = _DB.Editors.FirstOrDefault(a => a.ID == ID);
                if (model != null)
                {
                    model.IsDeleted = Type;
                    _DB.Entry(model).State = EntityState.Modified;
                    _DB.SaveChanges();
                    if (Type == false)
                        return "تم الحذف الكاتب " + model.Name;
                    else
                        return "تم رجوع الكاتب " + model.Name;
                }
                else
                {
                    return "حدث خطأ برجاء اعادة المحاوله";
                }
            }
            catch
            {
                return "حدث خطأ برجاء اعادة المحاوله";
            }
        }

        /// Return Editor Information
        public EditorVM GetEditorById(int ID)
        {
            var model = (from Sc in _DB.Editors.Where(a => a.ID == ID)
                         select new EditorVM()
                         {
                             ID = Sc.ID,
                             Name = Sc.Name,
                             AttachmentId = Sc.AttachmentID,
                             Description = Sc.Description,
                             Email = Sc.Email,
                             DisplayOrder = Sc.DisplayOrder,
                             CUser = Sc.CUser,
                             CDate = Sc.CDate,
                             IsDeleted = Sc.IsDeleted,
                         }).FirstOrDefault();
            return model;
        }

        /// Return All Editors With Search
        ///         

        public List<EditorVM> GetAllEditors(int Take, int PageID, string Filter, bool IsDeleted)
        {
            try
            {
                return (from Sc in _DB.BN_GetAllEditors(Take, PageID, Convert.ToInt32(IsDeleted).ToString(), Filter)
                        select new EditorVM()
                        {
                            ID = Sc.ID,
                            Name = Sc.Name,
                            IsDeleted = Sc.IsDeleted,
                            AttachmentPath = Sc.Path,
                            CUser = Sc.CUser,
                            CDate = Sc.CDate,
                        }).ToList();
            }
            catch
            {
                return new List<EditorVM>();
            }
        }
        public int EditorCount(string Filter, bool IsDeleted)
        {
            var Count = 0;
            var RetVal = _DB.BN_GetAllEditors_Count(Convert.ToInt32(IsDeleted).ToString(), Filter);
            if (RetVal != null)
                Count = RetVal.FirstOrDefault().Value;

            return int.Parse(Count.ToString());
        }

        /// Return All Editors For DropDownList Sort By Title
        public List<EditorVM> GetAllEditors_DDL()
        {
            try
            {
                return (from Sc in _DB.BN_GetAllEditors_DDL()
                        select new EditorVM()
                        {
                            ID = Sc.ID,
                            Name = Sc.Name
                        }).ToList();
            }
            catch
            {
                return new List<EditorVM>();
            }
        }

        /// Return All Editors Order By DisplayOrder
        public List<EditorVM> GetAllEditors_ForReOrder()
        {
            try
            {
                return (from Sc in _DB.BN_GetAllEditors_ForReOrder()
                        select new EditorVM()
                        {
                            ID = Sc.ID,
                            Name = Sc.Name
                        }).ToList();
            }
            catch
            {
                return new List<EditorVM>();
            }
        }

        /// Save Editors Order By DisplayOrder
        public string SaveEditorsReOrder(List<EditorVM> viewmodel)
        {
            try
            {
                int Order = 1;
                foreach (var item in viewmodel)
                {
                    var CurrentEditor = _DB.Editors.Where(a => a.ID == item.ID).FirstOrDefault();
                    if (CurrentEditor != null)
                    {
                        CurrentEditor.DisplayOrder = Order;
                        Order++;
                    }

                }
                _DB.SaveChanges();
                return "تم الحفظ بنجاح";
            }
            catch
            {
                return "حدث خطأ";
            }
        }
    }
}
