using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Model
{
    public class UsersAdminVM
    {
        public int ID { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Profile_Picture { get; set; }

        public int Role { get; set; }
        public bool IsDeleted { get; set; }

        public int CUser { get; set; }
        public DateTime CDate { get; set; }
    }
}
