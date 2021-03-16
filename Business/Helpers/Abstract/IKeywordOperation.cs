using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Abstract
{
    public interface IKeywordOperation
    {
        IDataResult<WebSite> KeywordGenerator(WebSite webSite);
        IDataResult<List<Frequance>> RemoveWordsToExclude(List<Frequance> frequances);
        IDataResult<string> GetTitle(string stringWebSite);
        IDataResult<List<Frequance>> CreateFrequency(string content);
    }
}
