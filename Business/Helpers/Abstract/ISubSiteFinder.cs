using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Abstract
{
    public interface ISubSiteFinder
    {
        IDataResult<WebSite> Finder(WebSite webSite,List<string> allUrlList);
    }
}
