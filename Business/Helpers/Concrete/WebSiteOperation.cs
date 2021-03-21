using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Entities.Dto;

namespace Business
{
    public class WebSiteOperation : IWebSiteOperation
    {
        IHtmlCleaner _htmlCleaner;
        IKeywordOperation _keywordOperation;
        ISubSiteFinder _subSiteFinder;

        public WebSiteOperation(IHtmlCleaner htmlCleaner, IKeywordOperation keywordOperation, ISubSiteFinder subSiteFinder)
        {
            _htmlCleaner = htmlCleaner;
            _keywordOperation = keywordOperation;
            _subSiteFinder = subSiteFinder;
        }
        public IDataResult<WebSite> GetWebSite(WebSite webSite)
        {
            WebRequest request = WebRequest.Create(webSite.Url);
            WebResponse response = request.GetResponse();
            StreamReader responseData = new StreamReader(response.GetResponseStream(), Encoding.UTF8, false);
            webSite.StringHtmlPage = WebUtility.HtmlDecode(responseData.ReadToEnd());
            webSite.Title = _keywordOperation.GetTitle(webSite.StringHtmlPage).Data;
            webSite.Content = _htmlCleaner.RemoveHtmlTags(webSite.StringHtmlPage).Data;
            webSite.Words = _keywordOperation.FrequencyGenerater(webSite.Content).Data;
         
            
            webSite.Keywords = _keywordOperation.KeywordGenerator(webSite).Data.Keywords;
            
            
            
            return new SuccessDataResult<WebSite>(webSite);
        }
        public IDataResult<UrlTreeDto> SubUrlFinder(WebSite webSite)
        {
            List<string> allUrlList = new List<string>();
            webSite = _subSiteFinder.Finder(webSite, allUrlList).Data;
            allUrlList = UpdateAllUrlList(webSite.SubUrls, allUrlList);



            foreach (var subSite in webSite.SubUrls)
            {
                var sub = webSite.SubUrls.SingleOrDefault(p => p.Url == subSite.Url);
                sub = _subSiteFinder.Finder(sub, allUrlList).Data;
                allUrlList = UpdateAllUrlList(sub.SubUrls, allUrlList);

            }


            //Sub Url Tree
            UrlTreeDto tempUrlsTree = new UrlTreeDto(); //1.Seviye
            tempUrlsTree.Url = webSite.Url;
            tempUrlsTree.Title = webSite.Title;
            tempUrlsTree.SubUrls = new List<UrlTreeDto>(); //2.Seviye
            if (webSite.SubUrls.Count > 0)
            {
                foreach (var subUrl in webSite.SubUrls)
                {
                    var treeSubUrl = new UrlTreeDto
                    {
                        Title = subUrl.Title,
                        Url = subUrl.Url,
                        SubUrls = new List<UrlTreeDto>() //3.Seviye


                    };
                    if (subUrl.SubUrls.Count > 0)
                    {
                        var subsubUrlList = new List<UrlTreeDto>();
                        foreach (var subsubUrl in subUrl.SubUrls)
                        {
                            var treeSubSubUrl = new UrlTreeDto
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

            return new SuccessDataResult<UrlTreeDto>(tempUrlsTree);
        }

        private List<string> UpdateAllUrlList(List<WebSite> testSubUrls, List<string> allUrlList)
        {

            foreach (var subSite in testSubUrls)
            {
                if (!allUrlList.Any(p => p == subSite.Url))
                {
                    allUrlList.Add(subSite.Url);
                }
            }

            return allUrlList;
        }
    }
}
