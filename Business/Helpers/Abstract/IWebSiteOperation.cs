using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface IWebSiteOperation
    {
        IDataResult<WebSite> GetWebSite(WebSite webSite);

        IDataResult<UrlTreeDto> SubUrlFinder(WebSite webSite);

        IDataResult<WebSite> Finder(WebSite webSite, List<string> allUrlList);
    }
}