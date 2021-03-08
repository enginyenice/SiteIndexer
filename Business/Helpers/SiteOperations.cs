//Created By Engin Yenice
//enginyenice2626@gmail.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Helper
{
    public static class SiteOperations
    {
        public static IDataResult<WebSite> GetWebSite(WebSite webSite, List<HtmlTag> htmlTags)
        {
            WebRequest request = WebRequest.Create(webSite.Url); //2
            WebResponse response = request.GetResponse(); //4

            StreamReader responseData = new StreamReader(response.GetResponseStream(), Encoding.UTF8, false); //5
            string siteAllData = responseData.ReadToEnd(); //6
            webSite.SiteTitle = Title(siteAllData);

            foreach (var tag in htmlTags)
            {
                webSite.Content += GetTagData(siteAllData, tag.Tag);
            }

            webSite.Content = webSite.Content.Trim();
            webSite.Frequances = CreateFrequency(webSite.Content);
            return new SuccessDataResult<WebSite>(webSite);
        }

        private static string Title(string siteAllData)
        {
            int titleIndexFirst = siteAllData.IndexOf("<title>", StringComparison.Ordinal) + 7;
            int titleIndexLast = siteAllData[titleIndexFirst..].IndexOf("</title>", StringComparison.Ordinal); //8
            return siteAllData.Substring(titleIndexFirst, titleIndexLast);
        }

        private static string GetTagData(string siteAllData, string tag)
        {
            string text = "";
            int last = 0;
            while (true)
            {
                siteAllData = siteAllData[last..];
                int titleIndexFirst = siteAllData.IndexOf($"<{tag}", StringComparison.Ordinal);

                int titleIndexLast = titleIndexFirst != -1 ? siteAllData[titleIndexFirst..].IndexOf($"</{tag}>", StringComparison.Ordinal) : -1; //8

                if (titleIndexFirst == -1 || titleIndexLast == -1)
                {
                    break;
                }

                last = titleIndexFirst + titleIndexLast;
                text += RemoveHtml(siteAllData.Substring(titleIndexFirst, titleIndexLast)) + " ";
            }

            return text;
        }

        public static string RemoveHtml(string text)
        {
            text = text.ToLower();
            Regex regexScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            Regex regexHead = new Regex(@"<head[^>]*>[\s\S]*?</head>");
            Regex regexStyle = new Regex(@"<style[^>]*>[\s\S]*?</style>");
            Regex regexCode = new Regex(@"<code[^>]*>[\s\S]*?</code>");
            Regex regexImage = new Regex(@"<img[^>]* />");
            Regex regexHtml = new Regex(@"<(.|\n)*?>");
            Regex regexTab = new Regex(@"\t");
            Regex regexWhiteSpace = new Regex(@"&nbsp;");
            Regex regexNewLine = new Regex(@"<br>");
            Regex regexNewLine2 = new Regex(@"</br>");
            Regex regexNewLine3 = new Regex(@"\n");
            Regex regexRN = new Regex(@"\r\n?|\n");
            Regex regexAdditional = new Regex(@"’[a-z]+");
            Regex regexMark = new Regex(@"[“”!'^+%&/()=?_#½{[\]}\\|\-.,~:;><]");

            #region Regex Replace

            text = regexScript.Replace(text, " ");
            text = regexHead.Replace(text, " ");
            text = regexStyle.Replace(text, " ");
            text = regexCode.Replace(text, " ");
            text = regexImage.Replace(text, " ");
            text = regexTab.Replace(text, " ");
            text = regexWhiteSpace.Replace(text, " ");
            text = regexNewLine.Replace(text, " ");
            text = regexNewLine2.Replace(text, " ");
            text = regexNewLine3.Replace(text, " ");
            text = regexRN.Replace(text, " ");
            text = regexHtml.Replace(text, " ");
            text = regexAdditional.Replace(text, " ");
            text = regexMark.Replace(text, " ");

            #endregion Regex Replace

            #region Unicode Replace

            text = text.Replace("&bull;", ""); // •
            text = text.Replace("&#8217;", " ");
            text = text.Replace("&#39;", " ");
            text = text.Replace("&#8217;", "\"");
            text = text.Replace("&#8221;", "\"");
            text = text.Replace("&#8220;", "\"");
            text = text.Replace("&#231;", "ç");
            text = text.Replace("&#199;", "Ç");
            text = text.Replace("&#287;", "ğ");
            text = text.Replace("&#286;", "Ğ");
            text = text.Replace("&#351;", "ş");
            text = text.Replace("&#350;", "Ş");
            text = text.Replace("&#305;", "ı");
            text = text.Replace("&#304;", "İ");
            text = text.Replace("&#252;", "ü");
            text = text.Replace("&#220;", "ü");
            text = text.Replace("&#214;", "Ö");
            text = text.Replace("&#246;", "ö");
            text = text.Replace("&copy;", "©");

            #endregion Unicode Replace

            return text;
        }

        public static List<Frequance> CreateFrequency(string content)
        {
            List<Frequance> frequances = new List<Frequance>();
            var keywords = content.Split(" ");
            foreach (var keyword in keywords)
            {
                if (keyword != "" && keyword != " ")
                {
                    if (frequances.SingleOrDefault(p => p.Keyword == keyword.ToLower()) != null)
                    {
                        var frequance = frequances.SingleOrDefault(p => p.Keyword == keyword.ToLower());
                        frequance.Piece += 1;
                    }
                    else
                    {
                        frequances.Add(new Frequance
                        {
                            Piece = 1,
                            Keyword = keyword.ToLower()
                        });
                    }
                }
            }
            frequances = SiteOperations.RemoveTurkishConjunctions(frequances);
            return frequances.OrderByDescending(p => p.Piece).ToList();
        }

        public static List<Frequance> RemoveTurkishConjunctions(List<Frequance> frequances)
        {
            List<string> conjuctions = new List<string>
            {
                "a'nî","ama","amma","ancak","altı","altmış","az",
                "belki","bile","bir başka deyişle","bu","ben","biz","benim","bunlar","bir","beş","bin",
                "çünkü","çok",
                "da","de","dahi", "de","demek","dışında","dört","dokuz","doksan","diye",
                "eğer","encami","elli",
                "fakat",
                "gâh","gelgelelim","gibi",
                "ha","hâlbuki","hatta","hangisi",
                "ile","ille","velakin","ille velâkin", "imdi","iki","iyi","için",
                "kâh","kaldı ki","karşın","ki","kırk","kötü","kadar",
                "lakin",
                "madem","mademki","maydamı","meğerki","meğerse",
                "ne var ki","neyse","nerede","nereye","ne","niçin","neden","kim","kimi","kimin","nasıl",
                "oysa","oysaki","o","onlar","onun","onların","on","otuz",
                "seksle","sen","siz","senin","sizin","şunlar","sekiz","seksen",
                "üç",
                "ve","velakin","velev","velhâsıl","velhâsılıkelâm","veya","veyahut",
                "yedi","yirmi","yetmiş","yüz",
                "zira",
                "and","am","is","are","to","in","on","your","he","she","it","they","there","her","has","this","by","you","the","follow"
            };

            List<Frequance> Tempfrequances = new List<Frequance>();
            foreach (var item in frequances)
            {
                if (!conjuctions.Any(p => p == item.Keyword) && item.Keyword.Length > 3)
                {
                    Tempfrequances.Add(item);
                }
            }

            /* [https://tr.wiktionary.org/wiki/Kategori:T%C3%BCrk%C3%A7e_ba%C4%9Fla%C3%A7lar]
             * a'nî ama amma ancak
             * belki bile bir başka deyişle
             * çünkü
             * da / de dahi de demek dışında
             * eğer encami
             * fakat
             * gâh gelgelelim gibi
             * ha... ha... hâlbuki hatta
             * ile ille velakin ille velâkin imdi
             * kâh kaldı ki karşın ki
             * lakin
             * madem mademki maydamı meğerki meğerse
             * ne var ki neyse
             * oysa oysaki
             * seksle
             * ve velakin velev velhâsıl velhâsılıkelâm veya veyahut
             * ya... ya ya da yahut yalıňız yalnız yani yok yoksa
             * zira
             * */
            return Tempfrequances;
        }
    }
}