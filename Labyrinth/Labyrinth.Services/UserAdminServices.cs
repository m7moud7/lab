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

        public string Delete(int ID, bool Type)
        {
            throw new NotImplementedException();
        }

        public int EditorCount(string Filter, bool IsDeleted)
        {
            throw new NotImplementedException();
        }

        public List<UsersAdminVM> GetAllUsers(int Take, int PageID, string Filter, bool IsDeleted)
        {
            throw new NotImplementedException();
        }

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

        public UsersAdminVM GetUserById(int ID)
        {
            throw new NotImplementedException();
        }

        public int Save(UsersAdminVM viewmodel)
        {
            throw new NotImplementedException();
        }
    }
}
