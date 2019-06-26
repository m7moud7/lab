using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;

namespace Noor.Data
{
    public partial class Issue
    {
        public static Issue Clone(IssueVM ViewModel)
        {
            return new Issue()
            {
                ID = ViewModel.ID,
                NewsID=ViewModel.NewsID,
                AttachmentID = ViewModel.AttachmentID,
                NumberOfIssue = ViewModel.NumberOfIssue,
                IssueTitle = ViewModel.IssueTitle,
                Description = ViewModel.Description,
                Size = ViewModel.Size,
                FilePath = ViewModel.FilePath,
                CDate = ViewModel.CDate,
                CUser = ViewModel.CUser,
                IsDeleted = ViewModel.IsDeleted
            };
        }
    }
}
