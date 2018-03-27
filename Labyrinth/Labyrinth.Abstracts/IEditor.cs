using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;


namespace Labyrinth.Abstracts
{
    public interface IEditor
    {
        int Save(EditorVM viewmodel);
        string Delete(int ID, bool Type);
        EditorVM GetEditorById(int ID);

        List<EditorVM> GetAllEditors(int Take, int PageID, string Filter, bool IsDeleted);
        int EditorCount(string Filter, bool IsDeleted);
        List<EditorVM> GetAllEditors_DDL();

        /// For Order
        List<EditorVM> GetAllEditors_ForReOrder();
        string SaveEditorsReOrder(List<EditorVM> viewmodel);
    }
}
