using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labyrinth.BackEnd.App_Start;

namespace Labyrinth.BackEnd.Controllers
{
    public class HomeController : BaseController
    {
        [SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}