using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Abstract
{
    public interface IKeywordOperation
    {
        IDataResult<List<Word>> FrequencyGenerater(string content);
        IDataResult<List<Word>> RemoveWordsToExclude(List<Word> Words);
        IDataResult<WebSite> KeywordGenerator(WebSite webSite);

        IDataResult<string> GetTitle(string StringHtmlPage);
        IDataResult<WebSite> GetSubWebSite(WebSite webSite);

    }
}
