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

            if (_Article != null && _Article.Editors != null)
            {
                var articles = new MultiSelectList(_EditorService.GetAllEditors_DDL().ToList(), "ID", "Name", _Article.Editors.Select(a => a.ID).AsEnumerable());
                var EditorPolls = new List<EditorVM>();
                foreach (var item in _Article.Editors)
                {
                    var Current = _EditorService.GetEditorById(item.ID);
                    articles.Where(a => a.Value == item.ID.ToString()).FirstOrDefault().Selected = true;
                    EditorPolls.Add(Current);
                }
                _Article.Editors = EditorPolls;
                _Article.SelectedEditors = _Article.EditorList.Split(',');
                ViewBag.EditorsList = articles;
            }
            else
            {
                var articles = new MultiSelectList(_EditorService.GetAllEditors_DDL().ToList(), "ID", "Name");
                ViewBag.EditorsList = articles;
            }

            return View(new ArticleVM());
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

                if (ViewModel.SelectedEditors != null)
                {
                    ViewModel.Editors = new List<EditorVM>();
                    foreach (var item in ViewModel.SelectedEditors)
                    {
                        var current = new EditorVM();
                        current.ID = int.Parse(item);
                        ViewModel.Editors.Add(current);
                    }
                }

                var result = _ArticleService.Save(ViewModel);
                if (result > 0)
                    return RedirectToAction("Edit", "Article", new { ID = result });
            }

            ViewBag.SecIdList = CurrentSections();

            if (_Article != null && _Article.Editors != null)
            {
                var articles = new MultiSelectList(_EditorService.GetAllEditors_DDL().ToList(), "ID", "Name", _Article.Editors.Select(a => a.ID).AsEnumerable());
                var EditorPolls = new List<EditorVM>();
                foreach (var item in _Article.Editors)
                {
                    var Current = _EditorService.GetEditorById(item.ID);
                    articles.Where(a => a.Value == item.ID.ToString()).FirstOrDefault().Selected = true;
                    EditorPolls.Add(Current);
                }
                _Article.Editors = EditorPolls;
                _Article.SelectedEditors = _Article.EditorList.Split(',');
                ViewBag.EditorsList = articles;
            }
            else
            {
                var articles = new MultiSelectList(_EditorService.GetAllEditors_DDL().ToList(), "ID", "Name");
                ViewBag.EditorsList = articles;
            }


            return View();
        }


        [SessionExpireFilter]
        public ActionResult Edit(int ID)
        {
            var Model = _ArticleService.GetNewsByID(ID);

            if (Session["ArticleFromEdit_" + uservm.ID] != null)
                _Article = (ArticleVM)Session["ArticleFromEdit" + uservm.ID];

            if (Model.ID > 0)
            {
                ViewBag.SecIdList = CurrentSections();

                //////Editors
                if (_Article != null && _Article.Editors != null)
                {
                    var editors = new MultiSelectList(_EditorService.GetAllEditors_DDL().ToList(), "ID", "Name", _Article.Editors.Select(a => a.ID).AsEnumerable());
                    var NewEditors = new List<EditorVM>();
                    foreach (var item in _Article.Editors)
                    {
                        var Current = _EditorService.GetEditorById(item.ID);
                        editors.Where(a => a.Value == item.ID.ToString()).FirstOrDefault().Selected = true;
                        NewEditors.Add(Current);
                    }
                    _Article.Editors = NewEditors;
                    _Article.SelectedEditors = _Article.EditorList.Split(',');
                    ViewBag.EditorsList = editors;
                }
                else
                {
                    Model.Editors = _ArticleService.GetNewsEditor(ID).ToList();

                    var ListEditors = _EditorService.GetAllEditors_DDL().ToList();

                    var editors = new MultiSelectList(ListEditors, "ID", "Name", Model.Editors.Select(a => a.ID).AsEnumerable());
                    var NewEditors = new List<EditorVM>();
                    foreach (var item in Model.Editors)
                    {
                        var Current = _EditorService.GetEditorById(item.ID);
                        editors.Where(a => a.Value == item.ID.ToString()).FirstOrDefault().Selected = true;
                        NewEditors.Add(Current);
                        Model.EditorList += item.ID.ToString() + ",";
                    }
                    Model.Editors = NewEditors;

                    if (Model.EditorList != null)
                        Model.SelectedEditors = Model.EditorList.Split(',');

                    ViewBag.EditorsList = editors;
                }

                return View(Model);
            }
            else
            {
                return RedirectToAction("Add", "Article");
            }

            //return View(Model);

        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Edit(ArticleVM ViewModel)
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
                if (result > 0)
                    return RedirectToAction("Edit", "Article", new { ID = result });
            }

            return RedirectToAction("Edit", new { ID = ViewModel.ID });
        }


        [SessionExpireFilter]
        public ActionResult GatAllArticle()
        {
            ViewBag.SecIdList = CurrentSections();
            ViewBag.SecIdList.Insert(0, new SelectListItem { Text = "الكل", Value = "0" });

            ViewBag.CountPublishArticle = _ArticleService.GetAllNewsCount("", 0, 0, 0, true, false);
            ViewBag.CountUnPublishArticle = _ArticleService.GetAllNewsCount("", 0, 0, 0, false, false);
            ViewBag.CountDeletedArticle = _ArticleService.GetAllNewsCount("", 0, 0, 0, false, true);

            var model = _ArticleService.GetAllNews(10, 0, "", 0, 0, 0, true, false);

            return View(model);
        }

        [SessionExpireFilter]
        public ActionResult FetchArticles(int Take, int PageID, string Filter, int NewsID, int SecID, int TypeID, bool IsApproved, bool IsDeleted)
        {
            ViewBag.SecIdList = CurrentSections();

            ViewBag.CountPublishArticle = _ArticleService.GetAllNewsCount(Filter, NewsID, SecID, TypeID, true, false);
            ViewBag.CountUnPublishArticle = _ArticleService.GetAllNewsCount(Filter, NewsID, SecID, TypeID, false, false);
            ViewBag.CountDeletedArticle = _ArticleService.GetAllNewsCount(Filter, NewsID, SecID, TypeID, false, true);

            var model = _ArticleService.GetAllNews(Take, PageID, Filter, NewsID, SecID, TypeID, IsApproved, IsDeleted);

            return PartialView(model);
        }

        /// <summary>
        /// Reorder
        /// </summary>
        /// <returns></returns>

        [SessionExpireFilter]
        public ActionResult ArticleReorder()
        {
            var List = CurrentSections();
            var levels = _SectionService.GeAllOrderLevels();

            int count = 0;
            foreach (var item in levels.Where(a => a.Text != "sections").ToList())
            {
                List.Insert(count++, new SelectListItem { Text = item.Text, Value = item.ID.ToString() });
            }

            ViewBag.Section = List;
            return View(_ArticleService.GetArticleForReOrder(1, int.Parse(List.FirstOrDefault().Value.ToString())));
        }

        [SessionExpireFilter]
        public ActionResult GetArticleReorderBySection(int SecID, int Type = 1)
        {
            var model = _ArticleService.GetArticleForReOrder(Type, SecID);
            return PartialView(model);
        }

        [SessionExpireFilter]
        public ActionResult GetArticleForReOrderByID(int SecID, int Type = 1, int Num = 0)
        {
            if (Num != 0)
                return PartialView(_ArticleService.GetArticleForReOrderByID(Type, SecID, Num));
            else
                return PartialView(new OrderVM());

        }

        [SessionExpireFilter]
        public JsonResult SaveArticlesOrder(ArticleOrderVM ViewModel, int SecID, int Type)
        {
            string result = _ArticleService.SaveArticlesReorder(ViewModel.ArticleOrder, SecID, Type);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}