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
using System.Text.RegularExpressions;

namespace Business
{
    public class WebSiteOperation : IWebSiteOperation
    {
        IHtmlCleaner _htmlCleaner;
        IKeywordOperation _keywordOperation;

        public WebSiteOperation(IHtmlCleaner htmlCleaner, IKeywordOperation keywordOperation)
        {
            _htmlCleaner = htmlCleaner;
            _keywordOperation = keywordOperation;
        }
        public IDataResult<WebSite> GetWebSite(WebSite webSite)
        {
            try
            {
                WebRequest request = WebRequest.Create(webSite.Url);
                WebResponse response = request.GetResponse();
                StreamReader responseData = new StreamReader(response.GetResponseStream(), Encoding.UTF8, false);
                webSite.StringHtmlPage = WebUtility.HtmlDecode(responseData.ReadToEnd());
                webSite.Title = _keywordOperation.GetTitle(webSite.StringHtmlPage).Data;
                webSite.Content = _htmlCleaner.RemoveHtmlTags(webSite.StringHtmlPage).Data;
                webSite.Words = _keywordOperation.FrequencyGenerater(webSite.Content).Data;


                webSite.Keywords = _keywordOperation.KeywordGenerator(webSite).Data.Keywords;
            }
            catch (Exception)
            {

                webSite.Keywords = new List<Keyword>();
                webSite.Title = "";
                webSite.Content = "";
                webSite.StringHtmlPage = "";
                webSite.Words = new List<Word>();
                webSite.SimilarityScore = 0;
                webSite.SubUrls = new List<WebSite>();
            }

            
            
            
            
            return new SuccessDataResult<WebSite>(webSite);
        }
        public IDataResult<UrlTreeDto> SubUrlFinder(WebSite webSite)
        {
            List<string> allUrlList = new List<string>();
            webSite = Finder(webSite, allUrlList).Data;
            allUrlList = UpdateAllUrlList(webSite.SubUrls, allUrlList);



            foreach (var subSite in webSite.SubUrls)
            {
                var sub = webSite.SubUrls.SingleOrDefault(p => p.Url == subSite.Url);
                sub = Finder(sub, allUrlList).Data;
                allUrlList = UpdateAllUrlList(sub.SubUrls, allUrlList);

            }

            //Sub Url Tree
            UrlTreeDto tempUrlsTree = new UrlTreeDto(); //1.Seviye
            tempUrlsTree.Url = webSite.Url;
            tempUrlsTree.Title = webSite.Title;
            tempUrlsTree.Key = Guid.NewGuid().ToString("D");
            tempUrlsTree.Children = new List<UrlTreeDto>(); //2.Seviye
            if (webSite.SubUrls.Count > 0)
            {
                foreach (var subUrl in webSite.SubUrls)
                {
                    var treeSubUrl = new UrlTreeDto
                    {
                        Key = Guid.NewGuid().ToString("D"),
                        Title = subUrl.Title,
                        Url = subUrl.Url,
                        Children = new List<UrlTreeDto>() //3.Seviye


                    };
                    if (subUrl.SubUrls.Count > 0)
                    {
                        var subsubUrlList = new List<UrlTreeDto>();
                        foreach (var subsubUrl in subUrl.SubUrls)
                        {
                            var treeSubSubUrl = new UrlTreeDto
                            {
                                Key = Guid.NewGuid().ToString("D"),
                                Title = subsubUrl.Title,
                                Url = subsubUrl.Url
                            };
                            subsubUrlList.Add(treeSubSubUrl);
                        }
                        treeSubUrl.Children = subsubUrlList;
                    }
                    tempUrlsTree.Children.Add(treeSubUrl);
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
        public IDataResult<WebSite> Finder(WebSite webSite, List<string> allUrlList)
        {

            Regex regexDocType = new Regex(@"<!DOCTYPE[^>]*>");
            Regex regexScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            Regex regexHead = new Regex(@"<head[^>]*>[\s\S]*?</head>");
            Regex regexStyle = new Regex(@"<style[^>]*>[\s\S]*?</style>");
            Regex regexCode = new Regex(@"<code[^>]*>[\s\S]*?</code>");
            Regex regexATag = new Regex("(<a[^>]*>[\\s\\S]*?</a>)");
            Regex regexHref = new Regex("href=['|\"][a-zA-Z0-9:/.]+[^' | \"]+");
            string temp = webSite.StringHtmlPage;


            temp = regexDocType.Replace(temp, " ");
            temp = regexScript.Replace(temp, " ");
            temp = regexHead.Replace(temp, " ");
            temp = regexStyle.Replace(temp, " ");
            temp = regexCode.Replace(temp, " ");

            var result = regexATag.Matches(temp);
            temp = String.Join("  ", result);
            result = regexHref.Matches(temp);
            temp = String.Join("  ", result);
            temp = temp.Replace("'", "\"");
            temp = temp.Replace("href=\"", " ");
            temp = temp.Replace("  ", " ");
            string[] array = temp.Split(' ');

            List<string> clearList = new List<string>();
            foreach (var item in array)
            {
                if (item.Length > 0 && (item.Contains("https://") || item.Contains("http://")))
                {
                    if (!clearList.Any(p => p == item) && !allUrlList.Any(p => p == item))
                    {
                        clearList.Add(item);
                    }
                }
            }


            /*TODO: 
             * /web.php /web.asp şeklinde kısa urller gelebilir.
             * Bu urlleri (/) işareti ile tespit edip başına ana site adresi
             * eklenecektir.
            */

            int i = 0;
            foreach (var item in clearList)
            {

                try
                {
                    WebSite subSite = new WebSite
                    {
                        Url = item
                    };
                    subSite = GetWebSite(subSite).Data;
                    webSite.SubUrls.Add(subSite);
                    i++;
                }
                catch (Exception)
                {

                    // throw new Exception("URL BAĞLANTI HATASI");
                }
                if (i == 5)
                {
                    break;
                }
            }

            //Debug atıyorum..
            return new SuccessDataResult<WebSite>(webSite);
        }
    }
}
