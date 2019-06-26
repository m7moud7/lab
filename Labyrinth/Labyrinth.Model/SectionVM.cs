using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noor.Model
{
    public class SectionVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FBPage { get; set; }
        public string TwitterPage { get; set; }
        public string GooglePlusPage { get; set; }
        public int DisplayOrder { get; set; }
        public int CUser { get; set; }
        public DateTime CDate { get; set; }
        public string CDateString { get { return CDate.ToString("yyyy-MM-dd dddd hh:mm tt", new System.Globalization.CultureInfo("ar-EG")); } set { } }
        public bool IsDeleted { get; set; }
        public int? AlbumID { get; set; }


        public int ParentID { get; set; }
        public string ParentTitle { get; set; }


        public Int64? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }


    }

    public class SectionOrderVM
    {
        public List<SectionVM> SectionOrder { get; set; }

    }
}
