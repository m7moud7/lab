using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labyrinth.Abstracts;
using Labyrinth.Services;
using Labyrinth.Model;
using Labyrinth.BackEnd.App_Start;

namespace Labyrinth.BackEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAdmin _UserAdmin;
        public UserController()
        {
            _UserAdmin = new UserAdminServices();
        }

        [SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult Add()
        {
            ViewBag.Massage = TempData["Massage"];
            return View();
        }

        //[SessionExpireFilter]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(UsersAdminVM ViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewModel.CUser = uservm.ID;
        //        ViewModel.CDate = DateTime.Now;
        //        int ID = _UserAdmin.Save(ViewModel);
        //        if (ID > 0)
        //        {
        //            TempData["Massage"] = "Done";
        //            return RedirectToAction("Edit", new { ID = ID });
        //        }
        //        else
        //        {
        //            TempData["Massage"] = "Error";
        //            return RedirectToAction("Add");
        //        }
        //    }
        //    else
        //    {
        //        TempData["Massage"] = "Error";
        //        return RedirectToAction("Add");
        //    }
        //}

        //[SessionExpireFilter]
        //public ActionResult Edit(int ID)
        //{
        //    var model = _UserAdmin.GetEditorById(ID);
        //    ViewBag.Massage = TempData["Massage"];
        //    return View(model);
        //}

        //[HttpPost]
        //[SessionExpireFilter]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditorVM ViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int ID = _UserAdmin.Save(ViewModel);
        //        if (ID > 0)
        //        {
        //            TempData["Massage"] = "Done";
        //            return RedirectToAction("Edit", new { ID = ID });
        //        }
        //        else
        //        {
        //            TempData["Massage"] = "Error";
        //            return RedirectToAction("Add");
        //        }
        //    }
        //    else
        //    {
        //        TempData["Massage"] = "Error";
        //        return RedirectToAction("Add");
        //    }
        //}

        //[SessionExpireFilter]
        //public ActionResult GetAllEditors(int Take, int PageID, string Name, bool IsDeleted)
        //{
        //    ViewBag.Count = _UserAdmin.EditorCount(Name, IsDeleted);
        //    return View(_UserAdmin.GetAllEditors(Take, PageID, Name, IsDeleted));
        //}
      
    }
}