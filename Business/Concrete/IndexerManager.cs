//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Helpers.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
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

        public IDataResult<List<WebSiteRankingDto>> UrlRanking(WebSite targetSite, List<WebSite> pool)
        {
            targetSite = KeywordGenerator(targetSite).Data;
            pool.ForEach(item => item = FrequanceCalculate(item).Data);

            List<WebSiteRankingDto> webSiteRankingDtos = new List<WebSiteRankingDto>();
            foreach (var item in pool)
            {
                WebSiteRankingDto webSiteRankingDto = new WebSiteRankingDto { WebSite = item };
                foreach (var keyword in targetSite.Keywords)
                {
                    if (item.Frequances.Any(p => p.Keyword == keyword))
                        webSiteRankingDto.RankingCount += 1;
                }
                webSiteRankingDtos.Add(webSiteRankingDto);
            }
            webSiteRankingDtos = webSiteRankingDtos.OrderByDescending(p => p.RankingCount).ToList();
            return new SuccessDataResult<List<WebSiteRankingDto>>(webSiteRankingDtos);
        }
    }
}