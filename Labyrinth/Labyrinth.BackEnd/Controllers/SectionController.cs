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
    public class SectionController : BaseController
    {
        private readonly ISection _Section;

        public SectionController()
        {
            _Section = new SectionServices();
        }

        [SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult Add()
        {
            ViewBag.ListSection = _Section.GetAllSections_DDL().ToList().Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.ID.ToString(),
            }).ToList();
            ViewBag.ListSection.Insert(0, new SelectListItem { Text = "بدون", Value = "0" });

            ViewBag.Massage = TempData["Massage"];
            return View();
        }

        [SessionExpireFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SectionVM ViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewModel.CUser = uservm.ID;
                ViewModel.CDate = DateTime.Now;
                int ID = _Section.Save(ViewModel);
                if (ID > 0)
                {
                    TempData["Massage"] = "Done";
                    return RedirectToAction("Edit", new { ID = ID });
                }
                else
                {
                    TempData["Massage"] = "Error";
                    return RedirectToAction("Add");
                }
            }
            else
            {
                TempData["Massage"] = "Error";
                return RedirectToAction("Add");
            }
        }

        [SessionExpireFilter]
        public ActionResult Edit(int ID)
        {
            var model = _Section.GetSectionById(ID);

            ViewBag.ListSection = _Section.GetAllSections_DDL().Where(a => a.ID != ID).ToList().Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.ID.ToString(),
                Selected = (model != null && model.ParentID > 0 && model.ParentID == item.ID) ? true : false
            }).ToList();
            ViewBag.ListSection.Insert(0, new SelectListItem { Text = "بدون", Value = "0" });

            ViewBag.Massage = TempData["Massage"];

            return View(model);
        }

        [HttpPost]
        [SessionExpireFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionVM ViewModel)
        {
            if (ModelState.IsValid)
            {
                int ID = _Section.Save(ViewModel);
                if (ID > 0)
                {
                    TempData["Massage"] = "Done";
                    return RedirectToAction("Edit", new { ID = ID });
                }
                else
                {
                    TempData["Massage"] = "Error";
                    return RedirectToAction("Add");
                }
            }
            else
            {
                TempData["Massage"] = "Error";
                return RedirectToAction("Add");
            }
        }

        [SessionExpireFilter]
        public ActionResult GetAllSection(int Take, int PageID, string Name, bool IsDeleted)
        {
            ViewBag.Count = _Section.SectionCount(Name, IsDeleted);
            return View(_Section.GetAllSections(Take, PageID, Name, IsDeleted));
        }

        [SessionExpireFilter]
        public ActionResult SectionsReorder()
        {
            return View(_Section.GetAllSections_ForReOrder());
        }
        
        [SessionExpireFilter]
        public JsonResult SaveSectionsOrder(SectionOrderVM viewmodel)
        {
            string result = _Section.SaveSectionsReOrder(viewmodel.SectionOrder);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}