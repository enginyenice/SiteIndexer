using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Abstract
{
    public interface IHtmlClearer
    {
        IDataResult<string> RemoveHtml(string content);
        IDataResult<string> ReplaceText(string text);
    }
}
