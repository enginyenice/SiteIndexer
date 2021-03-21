using Business.Abstract;
using Business.Helpers.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class IndexerManager : IIndexerService
    {
       // IWordToExcludeDal _wordToExcludeDal;
        IWebSiteOperation _webSiteOperation;
        IKeywordOperation _keywordOperation;
        ISubSiteFinder _subSiteFinder;

        public IndexerManager(/*IWordToExcludeDal wordToExcludeDal,*/ IWebSiteOperation webSiteOperation, IKeywordOperation keywordOperation, ISubSiteFinder subSiteFinder)
        {
            // _wordToExcludeDal = wordToExcludeDal;
            _webSiteOperation = webSiteOperation;
            _keywordOperation = keywordOperation;
            _subSiteFinder = subSiteFinder;
        }

        //Stage One - Frequancy Calculation
        public IDataResult<WebSite> WebSiteCalculate(WebSite webSite)
        {
            webSite = _webSiteOperation.GetWebSite(webSite).Data;
            return new SuccessDataResult<WebSite>(webSite);
        }

        public IDataResult<UrlsTreeDto> SubUrlFinder(WebSite webSite)
        {
            List<string> allUrlList = new List<string>();
            webSite = _subSiteFinder.Finder(webSite,allUrlList).Data;
            allUrlList = UpdateAllUrlList(webSite.TestSubUrls, allUrlList);


            
            foreach (var subSite in webSite.TestSubUrls)
            {
                var sub = webSite.TestSubUrls.SingleOrDefault(p => p.Url == subSite.Url);
                sub = _subSiteFinder.Finder(sub, allUrlList).Data;
                allUrlList = UpdateAllUrlList(sub.TestSubUrls, allUrlList);

            }


            //Sub Url Tree
            UrlsTreeDto tempUrlsTree = new UrlsTreeDto(); //1.Seviye
            tempUrlsTree.Url = webSite.Url;
            tempUrlsTree.Title = webSite.Title;
            tempUrlsTree.SubUrls = new List<UrlsTreeDto>(); //2.Seviye
            if (webSite.TestSubUrls.Count > 0)
            {
                foreach (var subUrl in webSite.TestSubUrls)
                {
                    var treeSubUrl = new UrlsTreeDto
                    {
                        Title = subUrl.Title,
                        Url = subUrl.Url,
                        SubUrls = new List<UrlsTreeDto>() //3.Seviye
                        

                    };
                    if (subUrl.TestSubUrls.Count > 0)
                    {
                        var subsubUrlList = new List<UrlsTreeDto>();
                        foreach (var subsubUrl in subUrl.TestSubUrls)
                        {
                            var treeSubSubUrl = new UrlsTreeDto
                            {
                                Title = subsubUrl.Title,
                                Url = subsubUrl.Url
                            };
                            subsubUrlList.Add(treeSubSubUrl);
                        }
                        treeSubUrl.SubUrls = subsubUrlList;
                    }
                    tempUrlsTree.SubUrls.Add(treeSubUrl);
                }
            }

            return new SuccessDataResult<UrlsTreeDto>(tempUrlsTree);
        }

        private List<string> UpdateAllUrlList(List<WebSite> testSubUrls, List<string> allUrlList)
        {

            foreach (var subSite in testSubUrls)
            {
                if(!allUrlList.Any(p => p == subSite.Url))
                {
                    allUrlList.Add(subSite.Url);
                }
            }

            return allUrlList;
        }

        //Stage Three - Ranking of a url and url set similarity
        public IDataResult<UrlSimilarityWebSiteDto> UrlSimilarityCalculate(WebSite webSite, List<WebSite> webSitePool)
        {

            //Similarity calculating
            foreach (var item in webSitePool)
            {
                float machedKeywordsScore = 0;
                float allKeywordsScore = 0; 

                foreach (var keyword in item.Keywords)
                {
                    allKeywordsScore += keyword.frequency * keyword.score;

                    if (webSite.Keywords.Any(p => p.word == keyword.word))
                        machedKeywordsScore += keyword.frequency * keyword.score;
                }

                // if have SubUrl 
                if (item.SubUrl != null) //2.seviye %20
                {
                    foreach (var keyword in item.SubUrl.Keywords)
                    {
                        allKeywordsScore += keyword.frequency * keyword.score;

                        if (webSite.Keywords.Any(p => p.word == keyword.word))
                            machedKeywordsScore += keyword.frequency * keyword.score;

                    }
                    if (item.SubUrl.SubUrl != null)  // 3.SEViYE %5
                    {
                        
                        foreach (var keyword in item.SubUrl.SubUrl.Keywords)
                        {
                            allKeywordsScore += keyword.frequency * keyword.score;

                            if (webSite.Keywords.Any(p => p.word == keyword.word))
                                machedKeywordsScore += keyword.frequency * keyword.score;
                        }
                    }
                }

                item.SimilarityScore = (machedKeywordsScore / allKeywordsScore) * 100;
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
        /*
        public IDataResult<UrlSimilarityWithSubWebSiteDto> UrlSimilarityWithSubCalculate(WebSite webSite, List<WebSite> webSitePool)
        {
            webSite = KeywordCalculate(webSite).Data;
            webSitePool.ForEach(p => p = KeywordCalculate(p).Data);
            webSitePool.ForEach(p => p = _keywordOperation.GetSubWebSite(p).Data);
            webSitePool.ForEach(p => p.SubUrl = KeywordCalculate(p.SubUrl).Data);
            webSitePool.ForEach(p => p.SubUrl = _keywordOperation.GetSubWebSite(p.SubUrl).Data);
            webSitePool.ForEach(p => p.SubUrl.SubUrl = KeywordCalculate(p.SubUrl.SubUrl).Data);

            //Sub Url Tree
            List<UrlTreeDto> tempUrlTree = new List<UrlTreeDto>();
            foreach (var item in webSitePool)
            {
                UrlTreeDto UrlTree = new UrlTreeDto
                {
                    Url = item.Url,
                    Title = item.Title,
                };

                if (item.SubUrl != null)
                {
                    UrlTree.SubUrl = new UrlTreeDto
                    {
                        Url = item.SubUrl.Url,
                        Title = item.SubUrl.Title,
                    };
                    if (item.SubUrl.SubUrl != null)
                    {
                        UrlTree.SubUrl.SubUrl = new UrlTreeDto
                        {
                            Url = item.SubUrl.Url,
                            Title = item.SubUrl.Title,
                        };
                    }
                }
                tempUrlTree.Add(UrlTree);
            }

            //Adding sub urls to webSitePool
            List<WebSite> tempSubUrl = new List<WebSite>();
            webSitePool.ForEach(p => { tempSubUrl.Add(p.SubUrl); tempSubUrl.Add(p.SubUrl.SubUrl); });
            webSitePool = webSitePool.Concat(tempSubUrl).ToList();

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

            return new SuccessDataResult<UrlSimilarityWithSubWebSiteDto>(
                data: new UrlSimilarityWithSubWebSiteDto
                {
                    webSite = tempWebSite,
                    webSitePool = tempWebSitesPool,
                    UrlTree = tempUrlTree
                }
            );
        }
          
        */
        public IDataResult<UrlSimilarityWithSubWebSiteDto> UrlSimilarityWithSubCalculate(WebSite webSite, List<WebSite> webSitePool)
        {
          //  webSite = KeywordCalculate(webSite).Data;
//               webSitePool.ForEach(p => FrequanceCalculate(p));
            webSitePool.ForEach(p => SubUrlFinder(p));



            return null;

        }

        //Stage Five - Stage four and Semantic Analysis


    }
}