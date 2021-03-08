//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            UrlRanking(new WebSite { Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/dostluk-ayakkabilari" },
            new List<WebSite>{
            new WebSite{Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/dostluk-ayakkabilari"},
            new WebSite{Url = "https://www.kocamanbisite.com/masallar-binbir-gece/yedi-renk-masallari-lacivert-yol"},
            new WebSite{Url = "https://www.kocamanbisite.com/masallar-binbir-gece/sehrazatin-saraya-gidisi"},
            new WebSite {Url = "https://www.kocamanbisite.com/masallar-binbir-gece/esek-okuz-ve-ciftci-alternatif-uyarlama"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/kurt-ve-yedi-kucuk-oglak-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/fareli-koyun-kavalcisi-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/fareli-koyun-kavalcisi-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/goldilocks-ve-uc-ayi-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/kirmizi-baslikli-kiz-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/yoksul-kunduraci-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/karlar-kralicesi-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/bremen-mizikacilari-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/kurbaga-prens-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/rapunzel-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/hansel-ve-gretel-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/cirkin-ordek-yavrusu-masali"},
            new WebSite {Url = "https://www.kocamanbisite.com/grimm-masallari/pamuk-prenses"},
            new WebSite {Url = "https://www.fluentu.com/blog/english-tur/ingilizce-okuma-pratigi/"},
            });
        }

        private static void UrlRanking(WebSite targetSite, List<WebSite> pool)
        {
            IIndexerService _indexerService = new IndexerManager();
            var result = _indexerService.UrlRanking(targetSite, pool);
            Console.WriteLine($"Aranan Site: {targetSite.Title}");
            foreach (var item in result.Data)
            {
                Console.WriteLine($"Havuz Sıralaması : {item.Title} -> {item.RankingCount}");
            }
        }

        private static void KeywordsGenerator()
        {
            IIndexerService _indexerService = new IndexerManager();
            WebSite webSite = new WebSite
            {
                Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/dostluk-ayakkabilari",
                Keywords = new List<string>()
            };
            var result = _indexerService.KeywordGenerator(webSite);
            Console.WriteLine("Url: {0}\nTitle: {1}\nKeywords: {2}", result.Data.Url, result.Data.Title, string.Join(",", result.Data.Keywords));
        }

        private static void FrequanceCalculate()
        {
            IIndexerService _indexerService = new IndexerManager();
            WebSite webSite = new WebSite { Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/dostluk-ayakkabilari" };

            foreach (var frequance in _indexerService.FrequanceCalculate(webSite).Data.Frequances)
            {
                Console.WriteLine($"[{frequance.Piece}]->{frequance.Keyword}");
            }
        }
    }
}