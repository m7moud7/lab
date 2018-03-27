using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labyrinth.Model;
using Labyrinth.Abstracts;
using Labyrinth.Services;

namespace Labyrinth.BackEnd.Controllers
{
    public class BaseController : Controller
    {
        public UsersAdminVM uservm
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["User"] != null)
                    return (UsersAdminVM)System.Web.HttpContext.Current.Session["User"];
                else
                    return new UsersAdminVM();
                //return System.Web.HttpContext.Current.Session["User"] == null ? new UserVm() : (UserVm)System.Web.HttpContext.Current.Session["User"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["User"] = value;
            }
        }
        public BaseController()
        {

        }
    }
}