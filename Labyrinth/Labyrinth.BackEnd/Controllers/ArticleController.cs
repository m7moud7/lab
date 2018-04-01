using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labyrinth.BackEnd.App_Start;
using Labyrinth.Model;
using Labyrinth.Abstracts;
using Labyrinth.Services;
using System.Net;

namespace Labyrinth.BackEnd.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly ISection _SectionService;
        private readonly IEditor _EditorService;
        private static IArticle _ArticleService;
        private static ArticleVM _Article;

        public ArticleController()
        {
            _SectionService = new SectionServices();
            _EditorService = new EditorServices();
            _ArticleService = new ArticleServices();
        }

        [SessionExpireFilter]
        public List<SelectListItem> CurrentSections()
        {
            List<SelectListItem> sections = new List<SelectListItem>();
            sections = _SectionService.GetAllSections_DDL().ToList().Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.ID.ToString(),
                Selected = (_Article != null && _Article.ID > 0 && _Article.ID == item.ID) ? true : false
            }).ToList();
            return sections;
        }


        [SessionExpireFilter]
        public ActionResult Add()
        {
            ViewBag.SecIdList = CurrentSections();

            ViewBag.EditorIdList = _EditorService.GetAllEditors_DDL().ToList().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.ID.ToString(),
                Selected = (_Article != null && _Article.EditorID > 0 && _Article.EditorID == item.ID) ? true : false
            }).ToList();

            return View();
        }


        [SessionExpireFilter]
        public ActionResult Edit(int ID)
        {
            ViewBag.SecIdList = CurrentSections();
            ViewBag.EditorIdList = _EditorService.GetAllEditors_DDL().ToList().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.ID.ToString(),
                Selected = (_Article != null && _Article.EditorID > 0 && _Article.EditorID == item.ID) ? true : false
            }).ToList();

            return View();
        }


        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Add(ArticleVM ViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewModel.CUser = uservm.ID;
                ViewModel.CDate = DateTime.Now;
                ViewModel.Story = WebUtility.HtmlDecode(ViewModel.Story).ToString();

                if (ViewModel.CurrentGroupID == null && ViewModel.CurrentUserID == null)
                    ViewModel.CurrentUserID = uservm.ID;

                if (ViewModel.CurrentGroupID != null && ViewModel.CurrentUserID == 0)
                    ViewModel.CurrentUserID = null;

                if (ViewModel.CurrentGroupID != null && (ViewModel.CurrentUserID > 0))
                    ViewModel.CurrentGroupID = null;

                var result = _ArticleService.Save(ViewModel);
            }

            ViewBag.SecIdList = CurrentSections();
            ViewBag.EditorIdList = _EditorService.GetAllEditors_DDL().ToList().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.ID.ToString(),
                Selected = (_Article != null && _Article.EditorID > 0 && _Article.EditorID == item.ID) ? true : false
            }).ToList();
            return View();
        }


    }
}