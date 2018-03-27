using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Abstracts
{
    public interface IAttachment
    {
        List<GeneralSettingsVM> GetSettings(string SetName, string SetKey);
        long Save(AttachmentVM ViewModel);

    }
}
