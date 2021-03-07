//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Helper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class IndexerManager : IIndexerService
    {
        private readonly IHtmlTagDal _htmlTagDal;

        public IndexerManager(IHtmlTagDal htmlTagDal)
        {
            _htmlTagDal = htmlTagDal;
        }

        public IDataResult<WebSite> FrequanceCalculate(WebSite webSite)
        {
            List<Frequance> frequances = new List<Frequance>();
            var result = SiteOperations.GetWebSite(webSite, _htmlTagDal.GetAll());
            var keywords = result.Data.Content.Split(" ");
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
            result.Data.Frequances = frequances.OrderByDescending(p => p.Piece).ToList();

            return result;
        }

        public IDataResult<List<WebSite>> KeywordGenerator(List<WebSite> webSites)
        {
            foreach (var site in webSites)
            {
                var result = FrequanceCalculate(site);
                foreach (var item in result.Data.Frequances)
                {
                    if (site.Keywords.Count >= 10) break;
                    site.Keywords.Add(item.Keyword);
                }
            }
            return new SuccessDataResult<List<WebSite>>(webSites);
        }
    }
}