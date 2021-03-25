using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface IKeywordOperation
    {
        IDataResult<List<Word>> FrequencyGenerater(string content);

        IDataResult<List<Word>> RemoveWordsToExclude(List<Word> Words);

        IDataResult<WebSite> KeywordGenerator(WebSite webSite);

        IDataResult<WebSite> SemanticKeywordGenerator(WebSite webSite, ref List<SemanticWordJsonDto> Dictionary);

        IDataResult<string> GetTitle(string StringHtmlPage);
    }
}