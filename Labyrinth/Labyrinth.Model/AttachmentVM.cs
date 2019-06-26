using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noor.Model
{
    public class AttachmentVM
    {
        public Int64 ID { get; set; }
        public Int16? Type { get; set; }
        public string ALtImage { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }
        public string Embed { get; set; }
        public bool IsPublish { get; set; }
        public string FolderName { get; set; }
        public DateTime CDate { get; set; }
        public int CUser { get; set; }
        public string PicsData { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUsedToday { get; set; }
    }

    public class AttachmentOrderVM
    {
        public List<AttachmentVM> imageorder { get; set; }
        //public string Alt { get; set; }
    }
}
