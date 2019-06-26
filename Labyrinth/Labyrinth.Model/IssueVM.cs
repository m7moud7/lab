using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Model
{

    public class IssueVM
    {
        public int ID { get; set; }
        public int NewsID { get; set; }
        public int AttachmentID { get; set; }
        public string NumberOfIssue { get; set; }
        public string IssueTitle { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string FilePath { get; set; }

        public bool IsDeleted { get; set; }
        public int CUser { get; set; }
        public DateTime CDate { get; set; }
    }

}
