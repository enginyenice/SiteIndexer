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
        private static void Main(string[] args)
        {
            // KeywordsGenerator();
            // FrequanceCalculate(); // 1
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