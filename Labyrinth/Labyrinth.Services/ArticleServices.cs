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


        public int Save(AttachmentVM ViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
