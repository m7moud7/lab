using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noor.Model
{
    public class OrderVM
    {
        public int ID { get; set; }

        public int NewsID { get; set; }
        public string NewsTitle { get; set; }

        public int SecID { get; set; }
        public string SecTitle { get; set; }


        public int Type { get; set; }
        public int Index { get; set; }
        public string Notes { get; set; }

        public DateTime CDate { get; set; }
        public int? OrderSecID { get; set; }

    }

    public class ArticleOrderVM
    {
        public List<OrderVM> ArticleOrder { get; set; }
    }

}
