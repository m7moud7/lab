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
        UsersAdminVM CheckUserLogin(string Username, string Password);
        List<UsersAdminVM> GetAllUsers_DDL();
        
    }
}
