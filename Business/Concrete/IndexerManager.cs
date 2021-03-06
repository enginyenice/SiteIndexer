//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Business.Helper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

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
            return SiteOperations.GetWebSite(webSite, _htmlTagDal.GetAll());
        }
    }
}