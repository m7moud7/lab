using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;

namespace Noor.Abstracts
{
    public interface ITag
    {
        int QuickAddNews(string viewmodel, int NewsID);

        string GetNewsTags(int NewsID);
      
    }
}
