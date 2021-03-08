//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Helpers.Concrete
{
    public class KeywordOperation
    {
        public static IDataResult<WebSite> KeywordGenerator(WebSite webSite)
        {
            foreach (var item in webSite.Frequances)
            {
                if (webSite.Keywords.Count >= 10) break;
                webSite.Keywords.Add(item.Keyword);
            }
            return new SuccessDataResult<WebSite>(webSite);
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
            return Tempfrequances;
        }

        public static IDataResult<string> GetTitle(string stringWebSite)
        {
            int titleIndexFirst = stringWebSite.IndexOf("<title>", StringComparison.Ordinal) + 7;
            int titleIndexLast = stringWebSite[titleIndexFirst..].IndexOf("</title>", StringComparison.Ordinal); //8
            return new SuccessDataResult<string>(data: HtmlClear.ReplaceText(stringWebSite.Substring(titleIndexFirst, titleIndexLast)));
        }
    }
}