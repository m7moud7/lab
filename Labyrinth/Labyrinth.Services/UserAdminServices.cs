using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Abstracts;
using Labyrinth.Data;
using Labyrinth.Model;
using System.Data.Entity;

namespace Labyrinth.Services
{
    public class UserAdminServices : IUserAdmin
    {
        ContextEntities _DB = new ContextEntities();

        /// Insert & Update User
        public int Save(UsersAdminVM viewmodel)
        {
            try
            {
                var model = UsersAdmin.Clone(viewmodel);
                if (viewmodel.ID > 0)
                    _DB.Entry(model).State = EntityState.Modified;
                else
                    _DB.UsersAdmins.Add(model);

                _DB.SaveChanges();
                return model.ID;
            }
            catch
            {
                return 0;
            }
        }

        /// Delete User ==> Change IsDeleted
        public string Delete(int ID, bool IsDeleted)
        {
            try
            {
                var model = _DB.UsersAdmins.FirstOrDefault(a => a.ID == ID);
                if (model != null)
                {
                    model.IsDeleted = IsDeleted;
                    _DB.Entry(model).State = EntityState.Modified;
                    _DB.SaveChanges();
                    if (IsDeleted == false)
                        return "تم الحذف المستخدم " + model.Fullname;
                    else
                        return "تم رجوع المستخدم " + model.Fullname;
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

        /// Return Users Information
        public UsersAdminVM GetUserById(int ID)
        {
            var model = (from UA in _DB.UsersAdmins.Where(a => a.ID == ID)
                         select new UsersAdminVM()
                         {
                             ID = UA.ID,
                             Fullname = UA.Fullname,
                             Password = UA.Password,
                             Profile_Picture = UA.Profile_Picture,
                             Role = UA.Role,
                             Username = UA.Username,
                             CUser = UA.CUser.Value,
                             CDate = UA.CDate,
                             IsDeleted = UA.IsDeleted,
                         }).FirstOrDefault();
            return model;
        }

        /// Return All Sections With Search
        public List<UsersAdminVM> GetAllUsers(int Take, int PageID, string Filter, bool IsDeleted)
        {
            try
            {
                return (from Us in _DB.BN_GetAllUsers(Take, PageID, Convert.ToInt32(IsDeleted).ToString(), Filter)
                        select new UsersAdminVM()
                        {
                            ID = Us.ID,
                            Fullname = Us.Fullname,

                            Username = Us.Username,

                            IsDeleted = Us.IsDeleted,
                            CUser = Us.CUser.Value,
                            CDate = Us.CDate,
                        }).ToList();
            }
            catch
            {
                return new List<UsersAdminVM>();
            }
        }
        public int UserCount(string Filter, bool IsDeleted)
        {
            var Count = 0;
            var RetVal = _DB.BN_GetAllUsers_Count(Convert.ToInt32(IsDeleted).ToString(), Filter);
            if (RetVal != null)
                Count = RetVal.FirstOrDefault().Value;

            return int.Parse(Count.ToString());
        }

        /// Return All Sections For DropDownList Sort By Title
        public List<UsersAdminVM> GetAllUsers_DDL()
        {
            try
            {
                return (from Us in _DB.BN_GetAllUsers_DDL()
                        select new UsersAdminVM()
                        {
                            ID = Us.ID,
                            Fullname = Us.Fullname,
                            Username = Us.Username,
                        }).ToList();
            }
            catch
            {
                return new List<UsersAdminVM>();
            }
        }

        public UsersAdminVM CheckUserLogin(string Username, string Password)
        {
            try
            {
                string EncryptPassword = StringCipher.Encrypt(Password);
                var model = (from Us in _DB.BN_CheckUser(Username.ToLower(), EncryptPassword)
                             select new UsersAdminVM()
                             {
                                 ID = Us.ID,
                                 Fullname = Us.Fullname,
                                 Username = Us.Username,
                                 Profile_Picture = Us.Profile_Picture,
                                 Role = Us.Role,
                             }).FirstOrDefault();

                if (model == null)
                    return null;
                else
                    return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
