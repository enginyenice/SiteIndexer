﻿//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IIndexerService
    {
        IDataResult<WebSite> FrequanceCalculate(WebSite webSite);

        IDataResult<WebSite> KeywordGenerator(WebSite webSite);

        IDataResult<List<WebSiteRankingDto>> UrlRanking(WebSite targetSite, List<WebSite> pool);
    }
}