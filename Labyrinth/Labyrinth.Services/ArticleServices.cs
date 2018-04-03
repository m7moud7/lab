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

        public ArticleServices()
        {
            _TagServices = new TagServices();
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
            //try
            //{
                var model = (from NS in _DB.BN_GetNewsById(ID)
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
                }
                return model;
            //}
            //catch
            //{
            //    return new ArticleVM();
            //}
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
    }
}
