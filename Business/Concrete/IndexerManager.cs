//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Helpers.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class IndexerManager : IIndexerService
    {
        public IDataResult<WebSite> FrequanceCalculate(WebSite webSite)
        {
            return WebSiteOperation.GetWebSite(webSite);
        }

        public IDataResult<WebSite> KeywordGenerator(WebSite webSite)
        {
            webSite = WebSiteOperation.GetWebSite(webSite).Data;
            webSite = KeywordOperation.KeywordGenerator(webSite).Data;

            return new SuccessDataResult<WebSite>(webSite);
        }
    }
}