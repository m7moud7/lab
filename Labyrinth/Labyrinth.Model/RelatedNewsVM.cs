using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noor.Model
{
    public class RelatedNewsVM
    {
        public int NewsID { get; set; }
        public int? RelatedNewsID { get; set; }
        public string RelatedTitle { get; set; }
    }
}
