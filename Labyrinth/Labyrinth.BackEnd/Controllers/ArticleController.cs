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
using System.IO;

namespace Labyrinth.BackEnd.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly ISection _SectionService;
        private readonly IEditor _EditorService;
        private readonly IAttachment _AttachmentService;
        private readonly IIssue _IssueService;

        private static IArticle _ArticleService;
        private static ArticleVM _Article;

        public ArticleController()
        {
            _SectionService = new SectionServices();
            _EditorService = new EditorServices();
            _AttachmentService = new AttachmentServices();
            _IssueService = new IssueServices();

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
        public ActionResult Add(ArticleVM ViewModel, FormCollection frm)
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


                //Cover
                var coverImg = this.HttpContext.Request.Files["Coverfile"];
                if (coverImg != null && coverImg.ContentLength > 0 /*&& ViewModel.CoverImage == 0*/)
                {
                    DateTime date = new DateTime(); date = DateTime.Now;
                    string FileNameWithoutExt = date.Month.ToString() + date.Year.ToString() +
                       date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString()
                       + date.Second.ToString() + date.Millisecond.ToString();
                    string coverName = Path.GetFileNameWithoutExtension(coverImg.FileName) + "-" + FileNameWithoutExt + ".jpg";
                    ViewModel.Cover = new AttachmentVM();
                    ViewModel.Cover.Type = 3;
                    ViewModel.Cover.ALtImage = ViewModel.CoverCaption;
                    ViewModel.Cover.Caption = ViewModel.CoverCaption;
                    ViewModel.Cover.Path = coverName;
                    ViewModel.Cover.FolderName = "\\Images\\Cover\\";
                    ViewModel.Cover.IsPublish = true;
                    ViewModel.Cover.CUser = uservm.ID;
                    ViewModel.Cover.CDate = DateTime.Now;
                    ViewModel.Cover.IsDeleted = false;
                    ViewModel.CoverCaption = Path.GetFileNameWithoutExtension(this.HttpContext.Request.Files["Coverfile"].FileName);
                    coverImg.SaveAs(Server.MapPath("/Images/Cover/") + coverName);
                    ViewModel.Cover.ID = _AttachmentService.Save(ViewModel.Cover);
                    ViewModel.CoverImage = ViewModel.Cover.ID;
                }

                //Save
                var result = _ArticleService.Save(ViewModel);

                if (result > 0)
                {
                    //PDF
                    var pdffile = this.HttpContext.Request.Files["PDFfiles"];
                    if (pdffile != null && pdffile.ContentLength > 0 /*&& ViewModel.CoverImage == 0*/)
                    {
                        DateTime date = new DateTime(); date = DateTime.Now;
                        string FileNameWithoutExt = date.Month.ToString() + date.Year.ToString() +
                           date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString()
                           + date.Second.ToString() + date.Millisecond.ToString();
                        string PDFName = Path.GetFileNameWithoutExtension(pdffile.FileName) + "-" + FileNameWithoutExt + ".pdf";
                        ViewModel.PDF = new IssueVM();

                        ViewModel.PDF.NumberOfIssue = ViewModel.Title;
                        ViewModel.PDF.IssueTitle = ViewModel.Title;
                        ViewModel.PDF.Description = ViewModel.Brief;
                        ViewModel.PDF.Size = pdffile.ContentLength.ToString();
                        ViewModel.PDF.FilePath = "\\Images\\PDF\\"+ PDFName;
                        ViewModel.PDF.IsDeleted = false;
                        ViewModel.PDF.CUser = uservm.ID;
                        ViewModel.PDF.CDate = DateTime.Now;
                        ViewModel.PDF.NewsID = result;


                        ViewModel.PDFCaption = Path.GetFileNameWithoutExtension(this.HttpContext.Request.Files["PDFfiles"].FileName);
                        pdffile.SaveAs(Server.MapPath("/Images/PDF/") + PDFName);
                        ViewModel.PDF.ID = _IssueService.Save(ViewModel.PDF);
                        ViewModel.PDFFile = ViewModel.PDF.ID;
                    }

                    return RedirectToAction("Edit", "Article", new { ID = result });
                }
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

            ViewModel.RelatedNews = "";

            if (ViewModel.Type == 1)
                return View("AddArticle", ViewModel);
            else if (ViewModel.Type == 2)
                return View("AddGame", ViewModel);
            else if (ViewModel.Type == 3)
                return View("AddColoring", ViewModel);
            else if (ViewModel.Type == 4)
                return View("AddVideo", ViewModel);
            else if (ViewModel.Type == 5)
                return View("AddIssues", ViewModel);

            return View(ViewModel);
        }

        [SessionExpireFilter]
        public ActionResult AddArticle()
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

        [SessionExpireFilter]
        public ActionResult AddGame()
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

        [SessionExpireFilter]
        public ActionResult AddVideo()
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

        [SessionExpireFilter]
        public ActionResult AddColoring()
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


        [SessionExpireFilter]
        public ActionResult AddIssues()
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
        public ActionResult AddArticle(ArticleVM ViewModel)
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


                if (!string.IsNullOrEmpty(Model.RelatedNews))
                {
                    Model.SelectedRelatedNews = Model.RelatedNews.Split(',');
                    var RelatedNews = new MultiSelectList(_ArticleService.GetRelatedNews(Model.ID), "RelatedNewsID", "RelatedTitle", Model.RelatedNews.Split(','));
                    ViewBag.RelatedNewsList = RelatedNews;
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

                if (ViewModel.SelectedRelatedNews != null)
                    ViewModel.RelatedNews = String.Join(",", ViewModel.SelectedRelatedNews);


                var coverImg = this.HttpContext.Request.Files["Coverfile"];
                if (coverImg != null && coverImg.ContentLength > 0 /*&& ViewModel.CoverImage == 0*/)
                {
                    DateTime date = new DateTime(); date = DateTime.Now;
                    string FileNameWithoutExt = date.Month.ToString() + date.Year.ToString() +
                       date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString()
                       + date.Second.ToString() + date.Millisecond.ToString();
                    string coverName = Path.GetFileNameWithoutExtension(coverImg.FileName) + "-" + FileNameWithoutExt + ".jpg";
                    ViewModel.Cover = new AttachmentVM();
                    ViewModel.Cover.Type = 3;
                    ViewModel.Cover.ALtImage = ViewModel.CoverCaption;
                    ViewModel.Cover.Caption = ViewModel.CoverCaption;
                    ViewModel.Cover.Path = coverName;
                    ViewModel.Cover.FolderName = "\\Images\\Cover\\";
                    ViewModel.Cover.IsPublish = true;
                    ViewModel.Cover.CUser = uservm.ID;
                    ViewModel.Cover.CDate = DateTime.Now;
                    ViewModel.Cover.IsDeleted = false;
                    ViewModel.CoverCaption = Path.GetFileNameWithoutExtension(this.HttpContext.Request.Files["Coverfile"].FileName);
                    coverImg.SaveAs(Server.MapPath("/Images/Cover/") + coverName);
                    ViewModel.Cover.ID = _AttachmentService.Save(ViewModel.Cover);
                    ViewModel.CoverImage = ViewModel.Cover.ID;
                }

                //PDF
                var pdffile = this.HttpContext.Request.Files["PDFfiles"];
                if (pdffile != null && pdffile.ContentLength > 0 /*&& ViewModel.CoverImage == 0*/)
                {
                    DateTime date = new DateTime(); date = DateTime.Now;
                    string FileNameWithoutExt = date.Month.ToString() + date.Year.ToString() +
                       date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString()
                       + date.Second.ToString() + date.Millisecond.ToString();
                    string PDFName = Path.GetFileNameWithoutExtension(pdffile.FileName) + "-" + FileNameWithoutExt + ".pdf";
                    ViewModel.PDF = new IssueVM();

                    ViewModel.PDF.NumberOfIssue = ViewModel.Title;
                    ViewModel.PDF.IssueTitle = ViewModel.Title;
                    ViewModel.PDF.Description = ViewModel.Brief;
                    ViewModel.PDF.Size = pdffile.ContentLength.ToString();
                    ViewModel.PDF.FilePath = "\\Images\\PDF\\"+ PDFName;
                    ViewModel.PDF.IsDeleted = false;
                    ViewModel.PDF.CUser = uservm.ID;
                    ViewModel.PDF.CDate = DateTime.Now;
                    ViewModel.PDF.NewsID = ViewModel.ID;


                    ViewModel.PDFCaption = Path.GetFileNameWithoutExtension(this.HttpContext.Request.Files["PDFfiles"].FileName);
                    pdffile.SaveAs(Server.MapPath("/Images/PDF/") + PDFName);
                    ViewModel.PDF.ID = _IssueService.Save(ViewModel.PDF);
                    ViewModel.PDFFile = ViewModel.PDF.ID;
                }


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

            var model = _ArticleService.GetAllNews(20, 0, "", 0, 0, 0, true, false);

            return View(model);
        }

        [SessionExpireFilter]
        public ActionResult FetchArticles(int Take, int PageID, string Filter, int NewsID, int SecID, int TypeID, bool IsApproved, bool IsDeleted)
        {
            ViewBag.CountArticle = _ArticleService.GetAllNewsCount(Filter, NewsID, SecID, TypeID, IsApproved, IsDeleted);
            var model = _ArticleService.GetAllNews(Take, PageID, Filter, NewsID, SecID, TypeID, IsApproved, IsDeleted);
            return PartialView(model);
        }

        [SessionExpireFilter]
        public ActionResult RelatedNews(int Take)
        {
            ViewBag.SecIdList = CurrentSections();
            ViewBag.SecIdList.Insert(0, new SelectListItem { Text = "الكل", Value = "0" });

            ViewBag.CountPublishArticle = _ArticleService.GetAllNewsCount("", 0, 0, 0, true, false);

            var model = _ArticleService.GetAllNews(20, 0, "", 0, 0, 0, true, false);

            return View(model);
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