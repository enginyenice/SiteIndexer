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
                if (frequances.Any(p => p.Keyword == keyword) && keyword != "")
                {
                    var frequance = frequances.SingleOrDefault(p => p.Keyword == keyword);
                    frequance.Piece += 1;
                }
                frequances.Add(new Frequance
                {
                    Piece = 1,
                    Keyword = keyword
                });
            }
            return result;
        }
    }
}