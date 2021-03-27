using Business.Abstract;
using Business.Helpers.Abstract;
using Core.Utilities.Results;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class IndexerManager : IIndexerService
    {
        private IWebSiteOperation _webSiteOperation;
        private IKeywordOperation _keywordOperation;
        private List<WebSite> globalList;

        public IndexerManager(IWebSiteOperation webSiteOperation, IKeywordOperation keywordOperation)
        {
            _webSiteOperation = webSiteOperation;
            _keywordOperation = keywordOperation;
        }

        //Stage One - Frequancy Calculation
        //Stage Two - Keyword Calculate
        public IDataResult<WebSite> WebSiteCalculate(WebSite webSite)
        {
            webSite = _webSiteOperation.GetWebSite(webSite).Data;
            return new SuccessDataResult<WebSite>(webSite);
        }

        //Stage Three - Ranking of a url and url set similarity
        public IDataResult<UrlSimilarityWebSiteDto> UrlSimilarityCalculate(WebSite webSite, List<WebSite> webSitePool)
        {
            InputDto input = _keywordOperation.SimilarityCalculate(webSite, webSitePool).Data;

            UrlSimilarityWebSiteDto result = new UrlSimilarityWebSiteDto();

            List<SimilarityScoreDto> tempWebSitesPool = new List<SimilarityScoreDto>();
            input.webSitePool.ForEach(p =>
            {
                tempWebSitesPool.Add(
                    new SimilarityScoreDto
                    {
                        SimilarityScore = p.SimilarityScore,
                        webSite = new KeywordWebSiteDto
                        {
                            Url = p.Url,
                            Title = p.Title,
                            Keywords = p.Keywords
                        }
                    });
            });

            result.webSite = new KeywordWebSiteDto
            {
                Url = input.webSite.Url,
                Title = input.webSite.Title,
                Keywords = input.webSite.Keywords
            };
            result.webSitePool = tempWebSitesPool.OrderByDescending(p => p.SimilarityScore).ToList();

            return new SuccessDataResult<UrlSimilarityWebSiteDto>(data: result);
        }

        //Stage Four - Ranking of a url with sub urls and url set with sub urls similarity
        public IDataResult<UrlSimilaritySubWebSiteDto> UrlSimilarityWithSubCalculate(WebSite webSite, List<WebSite> webSitePool)
        {
            //Sub Url Tree
            globalList = new List<WebSite>();
            foreach (var item in webSitePool)
            {
                globalList.Add(item);
            }
            globalList.Add(webSite);
            List<UrlTreeDto> tempUrlTree = new List<UrlTreeDto>();
            webSitePool.ForEach(p => tempUrlTree.Add(_webSiteOperation.SubUrlFinder(p, globalList).Data));

            //Adding sub urls to webSitePool
            List<WebSite> tempSubUrls = new List<WebSite>();
            webSitePool.ForEach(p =>
            {
                p.SubUrls.ForEach(l =>
                {
                    tempSubUrls.Add(l);
                    l.SubUrls.ForEach(m =>
                    {
                        tempSubUrls.Add(m);
                    });
                });
            });
            webSitePool = webSitePool.Concat(tempSubUrls).ToList();

            //Similarity calculating
            InputDto input = _keywordOperation.SimilarityCalculate(webSite, webSitePool, true).Data;

            //Return Object
            KeywordWebSiteDto tempWebSite = new KeywordWebSiteDto
            {
                Url = input.webSite.Url,
                Title = input.webSite.Title,
                Keywords = input.webSite.Keywords
            };

            List<SimilarityScoreDto> tempWebSitesPool = new List<SimilarityScoreDto>();
            input.webSitePool.ForEach(p =>
            {
                tempWebSitesPool.Add(new SimilarityScoreDto
                {
                    SimilarityScore = p.SimilarityScore,
                    webSite = new KeywordWebSiteDto
                    {
                        Url = p.Url,
                        Title = p.Title,
                        Keywords = p.Keywords,
                    }
                });
            });

            return new SuccessDataResult<UrlSimilaritySubWebSiteDto>(
                data: new UrlSimilaritySubWebSiteDto
                {
                    webSite = tempWebSite,
                    webSitePool = tempWebSitesPool,
                    UrlTree = tempUrlTree
                }
            );
        }

        //Stage Five - Stage four and Semantic Analysis
        public IDataResult<UrlSimilaritySubSemanticWebSiteDto> UrlSimilarityWithSemanticCalculate(WebSite webSite, List<WebSite> webSitePool)
        {
            globalList = new List<WebSite>();
            foreach (var item in webSitePool)
            {
                globalList.Add(item);
            }
            globalList.Add(webSite);
            //Sub Url Tree
            List<UrlTreeDto> tempUrlTree = new List<UrlTreeDto>();
            webSitePool.ForEach(p => tempUrlTree.Add(_webSiteOperation.SubUrlFinder(p, globalList).Data));

            //Adding sub urls to webSitePool
            List<WebSite> tempSubUrls = new List<WebSite>();
            webSitePool.ForEach(p =>
            {
                p.SubUrls.ForEach(l =>
                {
                    tempSubUrls.Add(l);
                    l.SubUrls.ForEach(m =>
                    {
                        tempSubUrls.Add(m);
                    });
                });
            });
            webSitePool = webSitePool.Concat(tempSubUrls).ToList();

            //Semantic keyword generate
            List<SemanticWordJsonDto> Dictionary = InMemoryGlobalSemanticWord.GetGlobalSemanticWordList();
            webSite = _keywordOperation.SemanticKeywordGeneratorForTarget(webSite, ref Dictionary).Data;

            //Similarity calculating
            InputDto input = _keywordOperation.SimilarityCalculate(webSite, webSitePool, true, true).Data;

            //Return Object
            KeywordWebSiteDto tempWebSite = new KeywordWebSiteDto
            {
                Url = input.webSite.Url,
                Title = input.webSite.Title,
                Keywords = input.webSite.Keywords
            };

            List<SimilarityScoreSemanticDto> tempWebSitesPool = new List<SimilarityScoreSemanticDto>();
            input.webSitePool.ForEach(p =>
            {
                tempWebSitesPool.Add(new SimilarityScoreSemanticDto
                {
                    SimilarityScore = p.SimilarityScore,
                    webSite = new KeywordWebSiteSemanticDto
                    {
                        Url = p.Url,
                        Title = p.Title,
                        Keywords = p.Keywords,
                        SemanticKeywords = p.SemanticKeywords
                    }
                });
            });

            return new SuccessDataResult<UrlSimilaritySubSemanticWebSiteDto>(
                data: new UrlSimilaritySubSemanticWebSiteDto
                {
                    webSite = tempWebSite,
                    webSitePool = tempWebSitesPool,
                    UrlTree = tempUrlTree
                });
        }
    }
}