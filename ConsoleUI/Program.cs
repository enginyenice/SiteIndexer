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
            //FrequanceCalculate(); // 1
            KeywordsGenerator();
        }

        private static void KeywordsGenerator()
        {
            IIndexerService _indexerService = new IndexerManager(new InMemoryHtmlTagDal());
            List<WebSite> webSites = new List<WebSite> {
                new WebSite { Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/dostluk-ayakkabilari", Keywords = new List<string>()},
                new WebSite { Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/kuzeye-giden-kizaklar", Keywords = new List<string>() }
        };
            var result = _indexerService.KeywordGenerator(webSites);

            foreach (var item in result.Data)
            {
                Console.WriteLine($"URL: {item.Url}\nKeywords: {string.Join(",", item.Keywords)}");
            }
        }

        private static void FrequanceCalculate()
        {
            IIndexerService _indexerService = new IndexerManager(new InMemoryHtmlTagDal());
            WebSite webSite = new WebSite { Url = "https://www.kocamanbisite.com/cocuk-hikayeleri/dostluk-ayakkabilari" };
            //Console.WriteLine(_indexerService.FrequanceCalculate(webSite).Data.Content);

            foreach (var frequance in _indexerService.FrequanceCalculate(webSite).Data.Frequances)
            {
                Console.WriteLine($"[{frequance.Piece}]->{frequance.Keyword}");
            }
        }
    }
}