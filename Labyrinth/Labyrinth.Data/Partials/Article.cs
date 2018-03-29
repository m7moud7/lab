using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Data
{
    public partial class Article
    {
        public static Article Clone(ArticleVM viewmodel)
        {
            return new Article()
            {
                //ID = viewmodel.ID,
                //Type = viewmodel.Type,
                //AltImage = viewmodel.ALtImage,
                //Caption = viewmodel.Caption,
                //Path = viewmodel.Path,
                //Notes = viewmodel.Notes,
                //Embed = viewmodel.Embed,
                //IsPublish = viewmodel.IsPublish,
                //FolderName = viewmodel.FolderName,
                //CDate = viewmodel.CDate,
                //CUser = viewmodel.CUser,
                //PicsData = viewmodel.PicsData,
                //IsDeleted = viewmodel.IsDeleted
            };
        }
    }
}
