using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;
using Labyrinth.Abstracts;
using Labyrinth.Data;
using System.Data.Entity;

namespace Labyrinth.Services
{
    public class TagServices : ITag
    {
        ContextEntities _DB = new ContextEntities();

        public string GetNewsTags(int NewsID)
        {
            try
            {
                var model = (from Tg in _DB.BN_GetNewsTags(NewsID)
                             select new TagVM()
                             {
                                 ID = Tg.ID,
                                 Name = Tg.Name,
                             }).ToList();
                string TagName = "";
                if (model != null && model.Count > 0)
                    TagName = string.Join(",", model.Select(a => a.Name));
                return TagName;
            }
            catch
            {
                return "";
            }
        }

        public int QuickAddNews(string viewmodel, int NewsID)
        {
            try
            {
                if (viewmodel != null)
                {
                    var ListTags = viewmodel.Split(new string[] { "," }, StringSplitOptions.None);
                    List<TagVM> lstTags = new List<TagVM>();
                    foreach (var item in ListTags)
                    {
                        var CurrentTag = new TagVM();
                        CurrentTag.Name = RemoveSpaces(CurrentTag.Name).Trim();
                        lstTags.Add(CurrentTag);
                    }

                    List<Tag> Tagss = new List<Tag>();
                    foreach (var item in lstTags)
                    {
                        var CurrentTag = _DB.Tags.Where(a => a.Name == item.Name).FirstOrDefault();
                        if (CurrentTag == null)
                        {
                            var Tag = new Tag();
                            Tag.Name = item.Name;
                            _DB.Tags.Add(Tag);
                            Tagss.Add(Tag);
                        }
                        else
                        {
                            Tagss.Add(CurrentTag);
                        }
                    }
                    _DB.SaveChanges();
                    var OldTags = _DB.News_Tags.Where(a => a.NewsID == NewsID);
                    _DB.News_Tags.RemoveRange(OldTags);
                    _DB.SaveChanges();

                    foreach (var item in Tagss)
                    {
                        var NewsTag = new News_Tags();
                        NewsTag.NewsID = NewsID;
                        NewsTag.TagID = item.ID;
                        _DB.News_Tags.Add(NewsTag);
                    }
                    _DB.SaveChanges();
                }

                return 0;
            }
            catch
            {
                return 1;
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
