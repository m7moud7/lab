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
    public class SectionServices : ISection
    {
        ContextEntities _DB = new ContextEntities();

        /// Insert & Update Section
        public int Save(SectionVM viewmodel)
        {
            try
            {
                var model = Section.Clone(viewmodel);
                if (viewmodel.ID > 0)
                    _DB.Entry(model).State = EntityState.Modified;
                else
                    _DB.Sections.Add(model);

                _DB.SaveChanges();
                return model.ID;
            }
            catch
            {
                return 0;
            }
        }

        /// Delete Section ==> Change IsDeleted
        public string Delete(int ID, bool Type)
        {
            try
            {
                var model = _DB.Sections.FirstOrDefault(a => a.ID == ID);
                if (model != null)
                {
                    model.IsDeleted = Type;
                    _DB.Entry(model).State = EntityState.Modified;
                    _DB.SaveChanges();
                    if (Type == false)
                        return "تم الحذف قسم " + model.Title;
                    else
                        return "تم رجوع قسم " + model.Title;
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

        /// Return Section Information
        public SectionVM GetSectionById(int ID)
        {
            var model = (from Sc in _DB.Sections.Where(a => a.ID == ID)
                         select new SectionVM()
                         {
                             ID = Sc.ID,
                             Title = Sc.Title,
                             AttachmentId = Sc.AttachmentID,
                             Description = Sc.Description,
                             FBPage = Sc.FBPage,
                             TwitterPage = Sc.TwitterPage,
                             GooglePlusPage = Sc.GooglePlusPage,
                             DisplayOrder = Sc.DisplayOrder,
                             CUser = Sc.CUser,
                             CDate = Sc.CDate,
                             IsDeleted = Sc.IsDeleted,
                             AlbumID = Sc.AlbumID,
                             ParentID = Sc.ParentID,
                         }).FirstOrDefault();
            return model;
        }

        /// Return All Sections With Search
        ///         

        public List<SectionVM> GetAllSections(int Take, int PageID, string Filter, bool IsDeleted)
        {
            try
            {
                return (from Sc in _DB.BN_GetAllSections(Take, PageID, Convert.ToInt32(IsDeleted).ToString(), Filter)
                        select new SectionVM()
                        {
                            ID = Sc.ID,
                            Title = Sc.Title,
                            IsDeleted = Sc.IsDeleted,
                            AttachmentPath = Sc.Path,

                            ParentID = Sc.ParentID,
                            ParentTitle = Sc.ParentTitle,

                            CUser = Sc.CUser,
                            CDate = Sc.CDate,
                        }).ToList();
            }
            catch
            {
                return new List<SectionVM>();
            }
        }
        public int SectionCount(string Filter, bool IsDeleted)
        {
            var Count = 0;
            var RetVal = _DB.BN_GetAllSections_Count(Convert.ToInt32(IsDeleted).ToString(), Filter);
            if (RetVal != null)
                Count = RetVal.FirstOrDefault().Value;

            return int.Parse(Count.ToString());
        }

        /// Return All Sections For DropDownList Sort By Title
        public List<SectionVM> GetAllSections_DDL()
        {
            try
            {
                return (from Sc in _DB.BN_GetAllSections_DDL()
                        select new SectionVM()
                        {
                            ID = Sc.ID,
                            Title = Sc.Title
                        }).ToList();
            }
            catch
            {
                return new List<SectionVM>();
            }
        }

        /// Return All Sections Order By DisplayOrder
        public List<SectionVM> GetAllSections_ForReOrder()
        {
            try
            {
                return (from Sc in _DB.BN_GetAllSections_ForReOrder()
                        select new SectionVM()
                        {
                            ID = Sc.ID,
                            Title = Sc.Title
                        }).ToList();
            }
            catch
            {
                return new List<SectionVM>();
            }
        }

        /// Save Sections Order By DisplayOrder
        public string SaveSectionsReOrder(List<SectionVM> viewmodel)
        {
            try
            {
                int Order = 1;
                foreach (var item in viewmodel)
                {
                    var CurrentSec = _DB.Sections.Where(a => a.ID == item.ID).FirstOrDefault();
                    if (CurrentSec != null)
                    {
                        CurrentSec.DisplayOrder = Order;
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



        /// Return Order Levels
        public List<LookUpVM> GeAllOrderLevels()
        {
            try
            {
                return (from OL in _DB.OrderLevels
                        select new LookUpVM
                        {
                            ID = (int)OL.SecIdInOrder,
                            Text = OL.AliasLevelName
                        }).ToList();
            }
            catch
            {
                return new List<LookUpVM>();
            }
        }


    }
}
