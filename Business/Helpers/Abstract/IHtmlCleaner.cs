using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Abstract
{
    public interface IHtmlCleaner
    {
        IDataResult<string> RemoveHtmlTags(string StringHtmlPage);
    }
}
