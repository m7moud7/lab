using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Abstracts;
using Labyrinth.Data;
using Labyrinth.Model;
using System.Data.Entity;

namespace Labyrinth.Services
{
    public class ArticleServices : IArticle
    {
        ContextEntities _DB = new ContextEntities();
        private readonly ITag _TagServices;
        private readonly IUserAdmin _UserAdminServices;

        public ArticleServices()
        {
            _TagServices = new TagServices();
            _UserAdminServices = new UserAdminServices();
        }

        public int Save(ArticleVM ViewModel)
        {
            try
            {
                var model = News.Clone(ViewModel);
                if (ViewModel.ID > 0)
                    _DB.Entry(model).State = EntityState.Modified;
                else
                    _DB.News.Add(model);

                _DB.SaveChanges();

                //Add tags
                if (ViewModel.Tags != null)
                {
                    List<Tag> tagss = new List<Tag>();
                    foreach (var item in ViewModel.Tags.Split(new string[] { "," }, StringSplitOptions.None))
                    {
                        var CurrentTag = new Tag();
                        CurrentTag.Name = RemoveSpaces(item).Trim();
                        var CheckTag = _DB.Tags.Where(a => a.Name == CurrentTag.Name).FirstOrDefault();
                        if (CheckTag == null)
                        {
                            _DB.Tags.Add(CurrentTag);
                            tagss.Add(CurrentTag);
                        }
                        else
                        {
                            tagss.Add(CheckTag);
                        }
                    }
                    _DB.SaveChanges();
                    _DB.News_Tags.RemoveRange(_DB.News_Tags.Where(a => a.NewsID == model.ID));
                    foreach (var item in tagss)
                    {
                        var new_tag = new News_Tags();
                        new_tag.NewsID = model.ID;
                        new_tag.TagID = item.ID;
                        _DB.News_Tags.Add(new_tag);
                    }
                    _DB.SaveChanges();
                }

                //Save Editors
                if (ViewModel.Editors != null)
                {
                    var oldEditors = _DB.News_Editor.Where(a => a.NewsID == ViewModel.ID);

                    //if (oldEditors.Any())
                    //{
                    //    foreach (var item in oldEditors)
                    //    {
                    //        var newsversion_poll = new NewsVersion_Poll();
                    //        newsversion_poll.NewsId = oldModel.Id;
                    //        newsversion_poll.PollId = item.PollId;
                    //        _DB.NewsVersion_Poll.Add(newsversion_poll);
                    //    }
                    _DB.News_Editor.RemoveRange(oldEditors);
                    //}
                    foreach (var item in ViewModel.Editors)
                    {
                        var news_editor = new News_Editor();
                        news_editor.NewsID = model.ID;
                        news_editor.SecID = model.SecID;
                        news_editor.EditorID = item.ID;
                        _DB.News_Editor.Add(news_editor);
                    }
                }
                else
                {
                    var oldEditorone = _DB.News_Editor.Where(a => a.NewsID == model.ID).FirstOrDefault();
                    if (oldEditorone != null)
                        _DB.News_Editor.Remove(oldEditorone);
                }
                _DB.SaveChanges();

                //Add Schdeuled Publish
                if (!string.IsNullOrEmpty(ViewModel.SchdeuledPublish) && ViewModel.IsApproved == false)
                {
                    var oldone = _DB.NewsPublishes.Where(a => a.NewsID == model.ID).FirstOrDefault();
                    if (oldone != null)
                        _DB.NewsPublishes.Remove(oldone);
                    var news_pub = new NewsPublish();

                    news_pub.Type = model.Type;

                    if (ViewModel.IsTop)
                        news_pub.SecID = 0;
                    else if (ViewModel.IsSection)
                        news_pub.SecID = model.SecID;
                    else if (ViewModel.IsHome)
                        news_pub.SecID = -2;

                    news_pub.NewsID = model.ID;
                    news_pub.CDate = DateTime.Parse(ViewModel.SchdeuledPublish);
                    _DB.NewsPublishes.Add(news_pub);
                }
                else
                {
                    var oldone = _DB.NewsPublishes.Where(a => a.NewsID == model.ID).FirstOrDefault();
                    if (oldone != null)
                        _DB.NewsPublishes.Remove(oldone);
                }

                _DB.SaveChanges();


                return model.ID;
            }
            catch
            {
                return 0;
            }
        }

        public ArticleVM GetNewsByID(int ID)
        {
            try
            {
                var model = (from NS in _DB.BN_GetNewsByID(ID)
                             select new ArticleVM
                             {
                                 ID = NS.ID,
                                 SecID = NS.SecID,
                                 WrittenBy = NS.WrittenBy,
                                 Title = NS.Title,
                                 SubTitle = NS.SubTitle,
                                 Brief = NS.Brief,
                                 Story = NS.Story,
                                 AttachmentID = NS.AttachmentID,
                                 ImageCaption = NS.ImageCaption,
                                 ImagePath = NS.ImagePath,
                                 CoverID = NS.Cover,
                                 ImageCaptionCover = NS.CoverCaption,
                                 Notes = NS.Notes,
                                 Type = NS.Type.Value,
                                 //EditorID = NS.EditorID.Value,
                                 Status = NS.Status,
                                 IsApproved = NS.IsApproved,
                                 IsDeleted = NS.IsDeleted,
                                 CUser = NS.CUser.Value,
                                 CDate = NS.CDate,
                                 Embed = NS.Embed,
                                 CurrentGroupID = NS.CurrentGroup,
                                 CurrentUserID = NS.CurrentUser,
                                 RelatedNews = NS.RelatedNews,
                                 AlbumID = NS.AlbumId,
                                 CurrencyID = NS.CurrencyId,
                                 GoldID = NS.GoldId,
                             }).FirstOrDefault();
                if (model != null)
                {
                    model.Tags = _TagServices.GetNewsTags(ID);
                    model.NewsMeta = GetNewsMeta(ID);
                    //if (model.AttachmentID != 0)
                }
                return model;
            }
            catch
            {
                return new ArticleVM();
            }
        }

        public List<ArticleVM> GetAllNews(int Take, int PageID, string Filter, int NewsID, int SecID, int TypeID, bool IsApproved, bool IsDeleted)
        {
            try
            {
                var model = (from NS in _DB.BN_GetAllNews(Filter, NewsID.ToString(), SecID.ToString(), TypeID.ToString(), IsApproved, IsDeleted, Take, PageID)
                             select new ArticleVM()
                             {
                                 ID = NS.ID,
                                 Type = NS.Type.Value,
                                 Title = NS.Title,

                                 SecID = NS.SecID,
                                 SecTitle = NS.SecTitle,

                                 //EditorID = NS.EditorID.Value,
                                 //EditorName = NS.EditorName,

                                 Notes = NS.Notes,

                                 CUser = NS.CUser.Value,
                                 CDate = NS.CDate,

                                 CurrentUserID = NS.CurrentUser,
                                 CurrentGroupID = NS.CurrentGroup,

                                 Status = NS.Status,
                                 SchdeuledPublish = NS.SchdeuledPublish.ToString(),

                                 IsApproved = IsApproved,
                                 IsDeleted = IsDeleted,
                             }).ToList();
                var Users = _UserAdminServices.GetAllUsers_DDL();
                foreach (var item in model)
                {
                    try
                    {
                        if (item.CurrentUserID != null)
                            item.CurrentUserName = Users.Where(a => a.ID == item.CurrentUserID).FirstOrDefault().Fullname;
                    }
                    catch
                    {

                    }
                    //else if (item.CurrentGroupID != null)
                    //    item.CurrentGroupName = GetGroupNameByID(item.LastGroupID);
                }

                return model;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return new List<ArticleVM>();
            }
        }

        public int GetAllNewsCount(string Filter, int NewsID, int SecID, int TypeID, bool IsApproved, bool IsDeleted)
        {
            var Count = 0;
            try
            {
                var RetVal = _DB.BN_GetAllNews_Count(Filter, NewsID.ToString(), SecID.ToString(), TypeID.ToString(), IsApproved, IsDeleted);
                if (RetVal != null)
                    Count = RetVal.FirstOrDefault().Value;
                return int.Parse(Count.ToString());
            }
            catch
            {
                return int.Parse(Count.ToString());
            }
        }

        private List<NewsMetaVM> GetNewsMeta(int ID)
        {
            try
            {
                return (from nm in _DB.BN_GetNewsMeta(ID)
                        select new NewsMetaVM
                        {
                            ID = nm.ID,
                            Meta = nm.Meta,
                            Value = nm.Value
                        }).ToList();
            }
            catch
            {
                return new List<NewsMetaVM>();
            }
        }

        private string RemoveSpaces(string content)
        {
            content = content.Replace("    ", " ").Replace("   ", " ").Replace("  ", " ")
                             .Replace("----", "").Replace("---", "").Replace("--", "").Replace("-", " ")
                             .Replace(".", "").Replace(@"\", "").Replace(":", "")
                             .Replace("\"", "").Replace("'", "").Replace(";", "").Replace("|", "")
                             .Replace("!", "").Replace("#", "").Replace("$", "").Replace("*", "")
                             .Replace("?", "").Replace("%", "").Replace("؟", "").Replace(">", "")
                             .Replace("<", "").Replace(",", "").Replace("(", "").Replace(")", "")
                             .Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "")
                             .Replace("؛", "").Replace("/", "").Replace("+", "").Replace("،", "");

            if (content.StartsWith(" "))
                content = content.Remove(0, 1);

            if (content.EndsWith(" "))
                content = content.Remove(content.LastIndexOf(' '));

            if (content.StartsWith("-"))
                content = content.Remove(0, 1);

            if (content.EndsWith("-"))
                content = content.Remove(content.LastIndexOf('-'));
            return content;
        }

        ///ReOrder
        public List<OrderVM> GetArticleForReOrder(int Type, int SecID = 0)
        {
            try
            {
                return (from n in _DB.BN_GetNewsReOrder(Type, SecID.ToString())
                        select new OrderVM
                        {
                            NewsID = n.NewsID,
                            NewsTitle = n.Title,
                            Index = n.Index.Value,
                            CDate = n.CDate,
                            Notes = n.Notes,
                            SecTitle = n.SecTitle,
                            OrderSecID = SecID
                        }).ToList();
            }
            catch
            {
                return new List<OrderVM>();
            }
        }

        public OrderVM GetArticleForReOrderByID(int Type, int SecID = 0, int Num = 0)
        {
            try
            {
                var model = (from n in _DB.BN_GetArticleForReOrderByID(Type, SecID, Num)
                             select new OrderVM()
                             {
                                 NewsID = n.ID,
                                 NewsTitle = n.Title,
                                 SecID = n.SecId,
                                 SecTitle = n.SecTitle,
                                 OrderSecID = n.OrderSecID,
                                 Notes = n.Notes
                             }).FirstOrDefault();

                //// -1 = Don'tMiss
                if (SecID != -50)
                {
                    if (model.OrderSecID != null)
                    {
                        if (model.OrderSecID != -50)
                        {
                            if (model.OrderSecID == 0)
                                model.SecID = 0;
                            else if (model.OrderSecID == -1)
                                model.SecID = -1;
                            else
                                model.SecID = -1000;
                        }
                    }
                }
                return model;
            }
            catch
            {
                return new OrderVM();
            }
        }

        public string SaveArticlesReorder(List<OrderVM> ViewModel, int SecID, int Type)
        {
            try
            {
                _DB.Orders.Where(a => a.SecID == SecID && a.Type == Type).ToList().ForEach(p => _DB.Orders.Remove(p));
                _DB.SaveChanges();

                int _OrderCount = 1;
                var _NewOrderCount = 1;
                foreach (var item in ViewModel)
                {
                    if (SecID == 0 && Type == 1 && _OrderCount > 4 && _OrderCount < 17)
                    {
                        SecID = -1;
                        _OrderCount = _NewOrderCount;
                        _NewOrderCount++;
                    }
                    else if (SecID == 0 && Type == 1 && _OrderCount == 5)
                        OrderNews(item.NewsID, -1, Type);

                    if (_OrderCount > 20)
                        break;

                    item.Index = _OrderCount;
                    item.SecID = SecID;
                    item.Type = Type;

                    _OrderCount++;
                    var NewOrder = Order.Clone(item);
                    _DB.Orders.Add(NewOrder);
                }

                _DB.SaveChanges();
                return "تم الحفظ بنجاح";
            }
            catch
            {
                return "حدث خطأ";
            }
        }

        private string OrderNews(int NewsID, int SecID, int Type)
        {
            try
            {
                OrderVM ViewModel = new OrderVM();
                ViewModel.NewsID = NewsID;
                ViewModel.Type = Type;
                Transition_Article(ViewModel, SecID);
                return "تم الحفظ بنجاح";
            }
            catch (Exception)
            {
                return "حدث خطأ";
            }
        }

        public void Transition_Article(OrderVM ViewModel, int SecID)
        {
            try
            {
                var oldarticle = _DB.Orders.Where(a => a.NewsID == ViewModel.NewsID).FirstOrDefault();
                if (oldarticle != null)
                {
                    _DB.Orders.Remove(oldarticle);
                    _DB.SaveChanges();
                }

                var Updatedorders = _DB.Orders.Where(a => a.SecID == SecID).OrderBy(a => a.Index).ToList();
                foreach (var item in Updatedorders)
                {
                    item.Index = item.Index + 1;
                }
                _DB.SaveChanges();
                var cur = new Order();
                cur.Type = 1;
                cur.SecID = SecID;
                cur.NewsID = ViewModel.NewsID;
                cur.Index = 1;
                _DB.Orders.Add(cur);
                _DB.SaveChanges();
                var transitionLevelDeeper = _DB.OrderLevels.Where(a => a.SecIdInOrder == SecID).FirstOrDefault();
                if (transitionLevelDeeper != null)
                {
                    if (transitionLevelDeeper.TransitionLevelID != null)
                    {
                        var transitionedlevel = _DB.OrderLevels.Where(a => a.ID == transitionLevelDeeper.TransitionLevelID).FirstOrDefault();
                        if (transitionedlevel != null)
                        {
                            if (transitionedlevel.SecIdInOrder == -100)
                            {
                                var transitionedarticlevm = Updatedorders.Skip(transitionLevelDeeper.NewsCount - 1).Take(1).FirstOrDefault();
                                var article = _DB.News.Where(a => a.ID == transitionedarticlevm.NewsID).FirstOrDefault();
                                var curtrans = new OrderVM();
                                curtrans.Type = 1;
                                curtrans.SecID = article.SecID;
                                curtrans.NewsID = article.ID;
                                Transition_Article(curtrans, curtrans.SecID);
                            }
                            else
                            {
                                var transitionedarticlevm = Updatedorders.Skip(transitionLevelDeeper.NewsCount - 1).Take(1).FirstOrDefault();
                                var article = _DB.News.Where(a => a.ID == transitionedarticlevm.NewsID).FirstOrDefault();
                                var curtrans = new OrderVM();
                                curtrans.Type = 1;
                                curtrans.SecID = article.SecID;
                                curtrans.NewsID = article.ID;
                                Transition_Article(curtrans, (int)transitionedlevel.SecIdInOrder);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        ///News Procedures 

        public List<EditorVM> GetNewsEditor(int ID)
        {
            try
            {
                return (from NE in _DB.BN_News_Editor(ID)
                        select new EditorVM()
                        {
                            ID = NE.ID,
                            Name = NE.Name,
                            SecID = NE.SecID,
                        }).ToList();
            }
            catch
            {
                return new List<EditorVM>();
            }
        }


    }
}
