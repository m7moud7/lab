using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;

namespace Noor.Abstracts
{
    public interface IIssue
    {
        int Save(IssueVM ViewModel);
        IssueVM GatPDFByNewsID(int NewsID);
        List<IssueVM> GatAllPDF(int Take, int PageID, string Filter);
    }
}
