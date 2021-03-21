using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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
    }
}
