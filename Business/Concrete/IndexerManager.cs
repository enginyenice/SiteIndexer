//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Helpers.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;
namespace Business.Concrete
{
    public class IndexerManager : IIndexerService
    {
        IWordToExcludeDal _wordToExcludeDal;
        IWebSiteOperation _webSiteOperation;
        IKeywordOperation _keywordOperation;
        public IndexerManager(IWordToExcludeDal wordToExcludeDal, IWebSiteOperation webSiteOperation, IKeywordOperation keywordOperation)
        {
            _wordToExcludeDal = wordToExcludeDal;
            _webSiteOperation = webSiteOperation;
            _keywordOperation = keywordOperation;
        }

        public IDataResult<WebSite> FrequanceCalculate(WebSite webSite)
        {
            return _webSiteOperation.GetWebSite(webSite);
        }

        public IDataResult<WebSite> KeywordGenerator(WebSite webSite)
        {
            webSite = _webSiteOperation.GetWebSite(webSite).Data;
            webSite = _keywordOperation.KeywordGenerator(webSite).Data;

            return new SuccessDataResult<WebSite>(webSite);
        }

        public IDataResult<List<WebSiteRankingDto>> UrlRanking(WebSite targetSite, List<WebSite> pool)
        {
            targetSite = KeywordGenerator(targetSite).Data;
            pool.ForEach(item => item = FrequanceCalculate(item).Data);

            List<WebSiteRankingDto> webSiteRankingDtos = new List<WebSiteRankingDto>();
            foreach (var item in pool)
            {
                WebSiteRankingDto webSiteRankingDto = new WebSiteRankingDto { Title = item.Title, Url = item.Url };
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