//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Business.Helpers.Concrete
{
    public class WebSiteOperation
    {
        public static IDataResult<WebSite> GetWebSite(WebSite webSite)
        {
            WebRequest request = WebRequest.Create(webSite.Url); //2
            WebResponse response = request.GetResponse(); //4
            StreamReader responseData = new StreamReader(response.GetResponseStream(), Encoding.UTF8, false); //5
            string siteAllData = responseData.ReadToEnd(); //6
            webSite.StringWebSite = siteAllData;
            webSite.Content = HtmlClear.RemoveHtml(siteAllData);
            webSite.Frequances = FrequencyOperation.CreateFrequency(webSite.Content);
            webSite.Title = KeywordOperation.GetTitle(webSite.StringWebSite).Data;
            return new SuccessDataResult<WebSite>(webSite);
        }
    }
}