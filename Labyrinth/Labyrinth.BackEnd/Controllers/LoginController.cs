using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noor.Abstracts;
using Noor.Services;
using Noor.Model;

namespace Noor.BackEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAdmin _User;
        public LoginController()
        {
            _User = new UserAdminServices();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersAdminVM Viewmodel)
        {
            var model = _User.CheckUserLogin(Viewmodel.Username, Viewmodel.Password);
            if (model == null)
            {
                ViewBag.CheckUser = "Null";
                return View("Login");
            }
            else
            {
                Session["User"] = model;
                return RedirectToAction("Index", "Home");
            }
        }

    }
}