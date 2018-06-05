using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Model
{
    public class EditorVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int DisplayOrder { get; set; }
        public int CUser { get; set; }
        public DateTime CDate { get; set; }
        public string CDateString { get { return CDate.ToString("yyyy-MM-dd dddd hh:mm tt", new System.Globalization.CultureInfo("ar-EG")); } set { } }
        public bool IsDeleted { get; set; }

        public Int64? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }

        public int SecID { get; set; }

    }

    public class EditorOrderVM
    {
        public List<EditorVM> EditorOrder { get; set; }

    }
}
