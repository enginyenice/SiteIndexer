using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface IWebSiteOperation
    {
        IDataResult<WebSite> GetWebSite(WebSite webSite);

        IDataResult<UrlTreeDto> SubUrlFinder(WebSite webSite, List<WebSite> globalList);

        IDataResult<WebSite> Finder(WebSite webSite, List<string> allUrlList, List<WebSite> globalList);
    }
}