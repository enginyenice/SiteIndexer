using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Business
{
    public class WebSiteOperation : IWebSiteOperation
    {
        private IHtmlCleaner _htmlCleaner;
        private IKeywordOperation _keywordOperation;
        private List<String> WhiteList;

        public WebSiteOperation(IHtmlCleaner htmlCleaner, IKeywordOperation keywordOperation)
        {
            _htmlCleaner = htmlCleaner;
            _keywordOperation = keywordOperation;

            WhiteList = new List<String> { "php", "xps", "aspx", "axd", "chm", "do", "jhtml",
                                                        "jnlp", "json", "mht", "gg", "gsp", "adr", "css",
                                                        "mvc", "pac", "url", "xul", "_eml", "!bt", "asp",
                                                        "att", "cer", "cfm", "con", "htc", "htm", "html",
                                                        "js", "jsf", "jsp", "mhtml", "nzb", "rss", "vbd",
                                                        "web", "wsdl", "xfdl", "aex", "pem", "wrf", "xbel",
                                                        "alx", "ap", "ascx", "asr", "dap", "dml", "dwt",
                                                        "email", "mai", "phtml", "shtml", "wgt", "wml", "xhtml",
                                                        "crl", "pando", "pfc", "qbo"};
        }

        public IDataResult<WebSite> Finder(WebSite webSite, List<string> allUrlList, List<WebSite> globalList)
        {
            int subUrlFinderCount = 5;

            Regex regexDocType = new Regex(@"<!DOCTYPE[^>]*>");
            Regex regexScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            Regex regexHead = new Regex(@"<head[^>]*>[\s\S]*?</head>");
            Regex regexStyle = new Regex(@"<style[^>]*>[\s\S]*?</style>");
            Regex regexCode = new Regex(@"<code[^>]*>[\s\S]*?</code>");
            Regex regexATag = new Regex("(<a[^>]*>[\\s\\S]*?</a>)");
            Regex regexHref = new Regex("href=['|\"][a-zA-Z0-9:/.]+[^' | \"]+");
            Regex regexLastParams = new Regex("#+[^$]*");
            Regex regexBadLinkRemove = new Regex(" /[^ ]*");
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
            temp = regexBadLinkRemove.Replace(temp, " ");
            temp = temp.Replace("javascript:;", " ");
            temp = temp.Replace("  ", " ");

            //Standart Order
            //List<string> urlList = temp.Replace("  ", " ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //Random Order
            List<string> urlList = temp.Replace("  ", " ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).OrderBy(x => Guid.NewGuid()).ToList();

            List<string> clearList = new List<string>();

            foreach (var item in urlList)
            {
                try
                {
                    if (item.Length > 7 && (item.Substring(0, 8) == "https://" || item.Substring(0, 7) == "http://"))
                    {
                        string url = (item.Substring(item.Length - 1, 1) == "/") ? item.Substring(0, item.Length - 1) : item;
                        url = regexLastParams.Replace(url, "");
                        if (UrlControl(url, clearList, allUrlList, webSite.SubUrls).Data)
                        {
                            clearList.Add(url);
                        }
                        if (clearList.Count > (subUrlFinderCount * subUrlFinderCount * 2))
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            int i = 0;
            foreach (var item in clearList)
            {
                try
                {
                    WebSite subSite = new WebSite
                    {
                        Url = item
                    };
                    if (globalList.Any(p => p.Url == subSite.Url))
                    {
                        var selectedSite = globalList.SingleOrDefault(p => p.Url == subSite.Url);
                        //Console.WriteLine("Virtual Cache: " + selectedSite.Url);
                        subSite.Id = selectedSite.Id;
                        subSite.Url = selectedSite.Url;
                        subSite.Title = selectedSite.Title;
                        subSite.SimilarityScore = selectedSite.SimilarityScore;
                        subSite.Content = selectedSite.Content;
                        subSite.StringHtmlPage = selectedSite.StringHtmlPage;
                        subSite.Words = selectedSite.Words;
                        subSite.Keywords = selectedSite.Keywords;
                        subSite.SemanticKeywordsList = selectedSite.SemanticKeywordsList;
                        subSite.SemanticKeywords = selectedSite.SemanticKeywords;
                    }
                    else
                    {
                        subSite = GetWebSite(subSite).Data;
                        globalList.Add(subSite);
                    }
                    if (subSite.StringHtmlPage != "" && !webSite.SubUrls.Any(p => p.Url == item))
                    {
                        webSite.SubUrls.Add(subSite);
                        i++;
                    }
                }
                catch (Exception)
                {
                    // throw new Exception("URL BAĞLANTI HATASI");
                }
                //////////////////////////////////////
                ///           SUB COUNT            ///
                //////////////////////////////////////
                if (i == subUrlFinderCount)
                {
                    break;
                }
            }
            return new SuccessDataResult<WebSite>(webSite);
        }

        public IDataResult<WebSite> GetWebSite(WebSite webSite)
        {
            try
            {
                webSite.Url = webSite.Url.Trim();
                webSite.Url = (webSite.Url.Substring(webSite.Url.Length - 1, 1) == "/") ? webSite.Url.Substring(0, webSite.Url.Length - 1) : webSite.Url;
                //Console.WriteLine(webSite.Url);
                WebRequest request = WebRequest.Create(webSite.Url);
                WebResponse response = request.GetResponse();
                StreamReader responseData = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true);
                webSite.StringHtmlPage = WebUtility.HtmlDecode(responseData.ReadToEnd());
                webSite.Title = _keywordOperation.GetTitle(webSite.StringHtmlPage).Data;
                webSite.Content = _htmlCleaner.RemoveHtmlTags(webSite.StringHtmlPage).Data + " " + webSite.Title;
                webSite.Words = _keywordOperation.FrequencyGenerater(webSite.Content).Data;
                webSite.Keywords = _keywordOperation.KeywordGenerator(webSite).Data.Keywords;
            }
            catch (Exception)
            {
                webSite.StringHtmlPage = " ";
                webSite.Keywords = new List<Keyword>();
                //Console.WriteLine(webSite.Url);
            }
            return new SuccessDataResult<WebSite>(webSite);
        }

        public IDataResult<UrlTreeDto> SubUrlFinder(WebSite webSite, List<WebSite> globalList)
        {
            List<string> allUrlList = new List<string>();
            allUrlList.Add(webSite.Url);
            webSite = Finder(webSite, allUrlList, globalList).Data;
            allUrlList = UpdateAllUrlList(webSite.SubUrls, allUrlList);

            foreach (var subSite in webSite.SubUrls)
            {
                var sub = webSite.SubUrls.SingleOrDefault(p => p.Url == subSite.Url);
                sub = Finder(sub, allUrlList, globalList).Data;
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

        private IDataResult<bool> UrlControl(string url, List<string> clearList, List<string> allUrlList, List<WebSite> SubUrls)
        {
            string tempUrl = url;
            //https://
            int startIndex = tempUrl.IndexOf("/", 9, (tempUrl.Length - 9));
            if (startIndex != -1)
            {
                tempUrl = tempUrl.Substring((startIndex + 1), (tempUrl.Length - 1) - startIndex);
                if (tempUrl.Length > 0)
                {
                    while (startIndex != -1)
                    {
                        startIndex = 0;
                        startIndex = tempUrl.IndexOf(".", startIndex, (tempUrl.Length - 1));

                        if (startIndex != -1)
                        {
                            tempUrl = tempUrl.Substring((startIndex + 1), (tempUrl.Length - 1) - startIndex);
                        }
                    }
                }

                //TODO: Düzenlenecek..
                if (!WhiteList.Any(p => p == tempUrl) && tempUrl.Length <= 5)
                {
                    return new ErrorDataResult<bool>(false);
                }

                if (clearList.Any(p => p == url) || allUrlList.Any(p => p == url))
                {
                    return new ErrorDataResult<bool>(false);
                }
            }
            if (SubUrls.Any(p => p.Url == url))
            {
                return new ErrorDataResult<bool>(false);
            }
            return new SuccessDataResult<bool>(true);
        }
    }
}