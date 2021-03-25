﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IIndexerService
    {
        IDataResult<WebSite> WebSiteCalculate(WebSite webSite);

        IDataResult<UrlSimilarityWebSiteDto> UrlSimilarityCalculate(WebSite webSite, List<WebSite> webSitePool);

        IDataResult<UrlSimilaritySubWebSiteDto> UrlSimilarityWithSubCalculate(WebSite webSite, List<WebSite> webSitePool);

        IDataResult<UrlSimilaritySubSemanticWebSiteDto> UrlSimilarityWithSemanticCalculate(WebSite webSite, List<WebSite> webSitePool);
    }
}