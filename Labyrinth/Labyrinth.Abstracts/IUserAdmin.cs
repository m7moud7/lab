using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Abstracts
{
    public interface IUserAdmin
    {
        int Save(UsersAdminVM viewmodel);
        string Delete(int ID, bool IsDeleted);
        UsersAdminVM GetUserById(int ID);

        List<UsersAdminVM> GetAllUsers(int Take, int PageID, string Filter, bool IsDeleted);
        int UserCount(string Filter, bool IsDeleted);
        List<UsersAdminVM> GetAllUsers_DDL();


        UsersAdminVM CheckUserLogin(string Username, string Password);
        
    }
}
