using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labyrinth.BackEnd.App_Start;
using Labyrinth.Model;
using Labyrinth.Abstracts;
using Labyrinth.Services;

namespace Labyrinth.BackEnd.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly ISection _SectionService;
        private readonly IEditor _EditorService;
        private static ArticleVm _Article;

        public ArticleController()
        {
            _SectionService = new SectionServices();
            _EditorService = new EditorServices();
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

    }
}