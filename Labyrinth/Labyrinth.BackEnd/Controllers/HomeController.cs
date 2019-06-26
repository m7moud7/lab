using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noor.BackEnd.App_Start;

namespace Noor.BackEnd.Controllers
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