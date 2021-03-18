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
    public class WebSiteOperation :IWebSiteOperation
    {
        IHtmlClearer _htmlClearer;
        IKeywordOperation _keywordOperation;

        public WebSiteOperation(IHtmlClearer htmlClearer, IKeywordOperation keywordOperation)
        {
            _htmlClearer = htmlClearer;
            _keywordOperation = keywordOperation;
        }

        public IDataResult<WebSite> GetWebSite(WebSite webSite)
        {
            WebRequest request = WebRequest.Create(webSite.Url); //2
            WebResponse response = request.GetResponse(); //4
            StreamReader responseData = new StreamReader(response.GetResponseStream(), Encoding.UTF8, false); //5
            string siteAllData = responseData.ReadToEnd(); //6
            webSite.StringWebSite = siteAllData;
            webSite.Content = _htmlClearer.RemoveHtml(siteAllData).Data;
            webSite.Title = _keywordOperation.GetTitle(webSite.StringWebSite).Data;
            webSite.Frequances = _keywordOperation.CreateFrequency(webSite.Content).Data;
            return new SuccessDataResult<WebSite>(webSite);
        }
    }
}
