using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Abstracts;
using Labyrinth.Model;
using Labyrinth.Data;
using System.Data.Entity;

namespace Labyrinth.Services
{
    public class IssueServices : IIssue
    {
        ContextEntities _DB = new ContextEntities();

        public List<IssueVM> GatAllPDF(int Take, int PageID, string Filter)
        {
            throw new NotImplementedException();
        }

        public IssueVM GatPDFByNewsID(int NewsID)
        {
            var model = (from Sc in _DB.BN_GetIssuesByNewsID(NewsID)
                         select new IssueVM()
                         {
                             ID = Sc.ID,
                             FilePath = Sc.FilePath,
                         }).FirstOrDefault();
            return model;
        }

        public int Save(IssueVM ViewModel)
        {
            try
            {
                var model = Labyrinth.Data.Issue.Clone(ViewModel);

                if (ViewModel.ID > 0)
                    _DB.Entry(model).State = EntityState.Modified;
                else
                {
                    model.CDate = DateTime.Now;
                    _DB.Issues.Add(model);
                }
                _DB.SaveChanges();
                return model.ID;
            }
            catch
            {
                return 0;
            }
        }
    }
}
