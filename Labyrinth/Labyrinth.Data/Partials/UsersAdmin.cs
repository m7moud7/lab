using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;
namespace Labyrinth.Data
{
    public partial class UsersAdmin
    {
        public static UsersAdmin Clone(UsersAdminVM ViewModel)
        {
            return new UsersAdmin()
            {
                ID = ViewModel.ID,
                Fullname = ViewModel.Fullname,
                Username = ViewModel.Username,
                Password = ViewModel.Password,
                Profile_Picture = ViewModel.Profile_Picture,
                Role = ViewModel.Role,
                IsDeleted = ViewModel.IsDeleted,
                CUser = ViewModel.CUser,
                CDate = ViewModel.CDate,
            };
        }
    }
}
