//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            IIndexerService indexerService = new IndexerManager(new InMemoryHtmlTagDal());
            WebSite webSite = new WebSite
            {
                Url = "https://tr.lipsum.com/"
            };

            Console.WriteLine(indexerService.FrequanceCalculate(webSite).Data.Content);
        }
    }
}