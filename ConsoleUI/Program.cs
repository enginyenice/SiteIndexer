//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //SayfadaGecenKelimelerinFrekanslariniHesaplama();
            AnahtarKelimeCikarma();
        }

        private static void AnahtarKelimeCikarma()
        {
            IIndexerService _indexerService = new IndexerManager(new InMemoryHtmlTagDal());
            List<WebSite> webSites = new List<WebSite> {
                new WebSite { Url = "https://www.sondakika.com/haber/haber-son-dakika-haberleri-ofkeli-sahis-sokaktaki-tum-evleri-atese-verdi-13974473/", Keywords = new List<string>()},
                new WebSite { Url = "https://www.internethaber.com/aydinda-ofkeli-yegen-sokaktaki-evleri-atese-verdi-itfaiyeye-tahta-kasayla-saldirdi-2168526h.htm", Keywords = new List<string>() }
        };
            var result = _indexerService.KeywordGenerator(webSites);

            foreach (var item in result.Data)
            {
                Console.WriteLine($"URL: {item.Url} \nKeywords: {string.Join("\n", item.Keywords)}");
            }
        }

        private static void SayfadaGecenKelimelerinFrekanslariniHesaplama()
        {
            IIndexerService _indexerService = new IndexerManager(new InMemoryHtmlTagDal());
            WebSite webSite = new WebSite { Url = "https://www.haberler.com/son-dakika-haberi-polis-memurunu-olduren-amerikan-katil-zanlilarina-13974607-haberi/" };
            //Console.WriteLine(_indexerService.FrequanceCalculate(webSite).Data.Content);

            foreach (var frequance in _indexerService.FrequanceCalculate(webSite).Data.Frequances)
            {
                Console.WriteLine($"[{frequance.Piece}]->{frequance.Keyword}");
            }
        }
    }
}