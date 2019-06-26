using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noor.Model;


namespace Noor.Abstracts
{
    public interface ISection
    {
        int Save(SectionVM viewmodel);
        string Delete(int ID, bool Type);
        SectionVM GetSectionById(int ID);

        List<SectionVM> GetAllSections(int Take, int PageID, string Filter, bool IsDeleted);
        int SectionCount(string Filter, bool IsDeleted);
        List<SectionVM> GetAllSections_DDL();

        /// For Order
        List<SectionVM> GetAllSections_ForReOrder();
        string SaveSectionsReOrder(List<SectionVM> viewmodel);

        ///  For Order Leval
        List<LookUpVM> GeAllOrderLevels();
    }
}
