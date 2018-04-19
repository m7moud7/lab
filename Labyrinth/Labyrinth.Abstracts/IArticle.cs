using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Abstracts
{
    public interface IArticle
    {
        int Save(ArticleVM ViewModel);
        ArticleVM GetNewsByID(int ID);

        List<ArticleVM> GetAllNews(int Take, int PageID, string Filter, int NewsID, int SecID, int TypeID, bool IsApproved, bool IsDeleted);
        int GetAllNewsCount(string Filter, int NewsID, int SecID, int TypeID, bool IsApproved, bool IsDeleted);

    }
}
