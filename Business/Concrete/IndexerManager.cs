using Business.Abstract;
using Business.Helpers.Abstract;
using Core.Utilities.Results;
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
        private IJsonReader _jsonReader;

        public IndexerManager(IWebSiteOperation webSiteOperation, IKeywordOperation keywordOperation, IJsonReader jsonReader)
        {
            _webSiteOperation = webSiteOperation;
            _keywordOperation = keywordOperation;
            _jsonReader = jsonReader;
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
        {   //Similarity calculating
            foreach (var item in webSitePool)
            {
                //MaxValue = 3.40282347E+38F
                float machedKeywordsScore = 0;
                float allKeywordsScore = 0;

                foreach (var keyword in item.Keywords)
                {
                    allKeywordsScore += keyword.frequency * keyword.score;

                    if (webSite.Keywords.Any(p => p.word == keyword.word))
                        machedKeywordsScore += keyword.frequency * keyword.score;
                }

                // if have SubUrl
                if (item.SubUrls.Count > 0) //2.Seviye %20
                {
                    float lvl2MachedKeyword = 0;
                    float lvl2UrlAllKeyword = 0;

                    //lvl 2
                    foreach (var subUrl in item.SubUrls)
                    {
                        foreach (var keyword in subUrl.Keywords)
                        {
                            lvl2UrlAllKeyword += keyword.frequency * keyword.score;

                            if (webSite.Keywords.Any(p => p.word == keyword.word))
                                lvl2MachedKeyword += keyword.frequency * keyword.score;
                        }

                        if (subUrl.SubUrls.Count > 0) //3.Seviye %10
                        {
                            float lvl3MachedKeyword = 0;
                            float lvl3UrlAllKeyword = 0;

                            //lvl 3
                            foreach (var subUrl2 in subUrl.SubUrls)
                            {
                                foreach (var keyword in subUrl2.Keywords)
                                {
                                    lvl3UrlAllKeyword += keyword.frequency * keyword.score;

                                    if (webSite.Keywords.Any(p => p.word == keyword.word))
                                        lvl3MachedKeyword += keyword.frequency * keyword.score;
                                }
                            }
                            lvl2MachedKeyword += lvl3MachedKeyword;
                            lvl2UrlAllKeyword += lvl3UrlAllKeyword * 10;
                        }
                    }
                    machedKeywordsScore += lvl2MachedKeyword;
                    allKeywordsScore += lvl2UrlAllKeyword * 5;
                }

                item.SimilarityScore = (machedKeywordsScore / allKeywordsScore) * 100;
                if (float.IsNaN(item.SimilarityScore) || float.IsNegative(item.SimilarityScore))
                {
                    item.SimilarityScore = 0;
                }
            }

            List<SimilarityScoreDto> tempWebSitesPool = new List<SimilarityScoreDto>();
            webSitePool.ForEach(p =>
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

            UrlSimilarityWebSiteDto result = new UrlSimilarityWebSiteDto();
            result.webSite = new KeywordWebSiteDto
            {
                Url = webSite.Url,
                Title = webSite.Title,
                Keywords = webSite.Keywords
            };
            result.webSitePool = tempWebSitesPool.OrderByDescending(p => p.SimilarityScore).ToList();

            return new SuccessDataResult<UrlSimilarityWebSiteDto>(data: result);
        }

        //Stage Four - Ranking of a url with sub urls and url set with sub urls similarity
        public IDataResult<UrlSimilaritySubWebSiteDto> UrlSimilarityWithSubCalculate(WebSite webSite, List<WebSite> webSitePool)
        {
            List<UrlTreeDto> tempUrlTree = new List<UrlTreeDto>();
            webSitePool.ForEach(p => tempUrlTree.Add(_webSiteOperation.SubUrlFinder(p).Data));

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
            var result = UrlSimilarityCalculate(webSite, webSitePool).Data;

            KeywordWebSiteDto tempWebSite = new KeywordWebSiteDto
            {
                Url = result.webSite.Url,
                Title = result.webSite.Title,
                Keywords = result.webSite.Keywords
            };

            List<SimilarityScoreDto> tempWebSitesPool = new List<SimilarityScoreDto>();
            result.webSitePool.ForEach(p =>
            {
                tempWebSitesPool.Add(new SimilarityScoreDto
                {
                    SimilarityScore = p.SimilarityScore,
                    webSite = new KeywordWebSiteDto
                    {
                        Url = p.webSite.Url,
                        Title = p.webSite.Title,
                        Keywords = p.webSite.Keywords,
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
            List<UrlTreeDto> tempUrlTree = new List<UrlTreeDto>();
            webSitePool.ForEach(p => tempUrlTree.Add(_webSiteOperation.SubUrlFinder(p).Data));

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
            List<SemanticWordJsonDto> Dictionary = _jsonReader.getSemanticKeywords().Data;
            webSitePool.ForEach(p => p = _keywordOperation.SemanticKeywordGenerator(p, ref Dictionary).Data);

            //Url similarity calculate with SubUrl
            var result = UrlSimilarityCalculate(webSite, webSitePool).Data;

            KeywordWebSiteDto tempWebSite = new KeywordWebSiteDto
            {
                Url = result.webSite.Url,
                Title = result.webSite.Title,
                Keywords = result.webSite.Keywords
            };

            List<SimilarityScoreSemanticDto> tempWebSitesPool = new List<SimilarityScoreSemanticDto>();
            result.webSitePool.ForEach(p =>
            {
                List<SemanticKeyword> tempSemanticKeywords = new List<SemanticKeyword>();
                webSitePool.ForEach(a =>
                {
                    if (a.Url == p.webSite.Url && a.Title == p.webSite.Title)
                        tempSemanticKeywords = a.SemanticKeywords;
                });

                tempWebSitesPool.Add(new SimilarityScoreSemanticDto
                {
                    SimilarityScore = p.SimilarityScore,
                    webSite = new KeywordWebSiteSemanticDto
                    {
                        Url = p.webSite.Url,
                        Title = p.webSite.Title,
                        Keywords = p.webSite.Keywords,
                        SemanticKeyword = new List<SemanticKeyword>(tempSemanticKeywords)
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