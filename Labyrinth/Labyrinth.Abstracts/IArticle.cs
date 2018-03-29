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
        int Save(AttachmentVM ViewModel);
    }
}
