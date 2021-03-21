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
        public IDataResult<WebSite> Finder(WebSite webSite)
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
            string[] array = temp.Split(' ');

            /*TODO: 
             * /web.php /web.asp şeklinde kısa urller gelebilir.
             * Bu urlleri (/) işareti ile tespit edip başına ana site adresi
             * eklenecektir.
            */
            foreach (var item in array)
            {
                if (item.Length > 0 && !webSite.TestSubUrls.Any(sub => sub.Url == item))
                {
                    webSite.TestSubUrls.Add(new WebSite
                    {
                        Url = item
                    });
                }
                 
            }
            //Debug atıyorum..
            throw new NotImplementedException();

        }
    }
}
