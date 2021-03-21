using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
namespace Business.Helpers.Concrete
{
    public class SubSiteFinder : ISubSiteFinder
    {
        IWebSiteOperation _webSiteOperation;

        public SubSiteFinder(IWebSiteOperation webSiteOperation)
        {
            _webSiteOperation = webSiteOperation;
        }

        public IDataResult<WebSite> Finder(WebSite webSite,List<string> allUrlList)
        {
            Regex regexATag = new Regex("(<a[^>]*>[\\s\\S]*?</a>)");
            Regex regexHref = new Regex("href=['|\"][a-zA-Z0-9:/.]+[^' | \"]+");
            string temp = webSite.StringHtmlPage;

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
                    subSite = _webSiteOperation.GetWebSite(subSite).Data;
                    webSite.SubUrls.Add(subSite);
                    i++;
                }
                catch (Exception)
                {

                    // throw new Exception("URL BAĞLANTI HATASI");
                }
                if(i == 5)
                {
                    break;
                }
            }

            //Debug atıyorum..
            return new SuccessDataResult<WebSite>(webSite);
        }
    }
}
