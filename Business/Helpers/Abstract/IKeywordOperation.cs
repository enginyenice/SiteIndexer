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

        IDataResult<WebSite> SemanticKeywordGeneratorForTarget(WebSite webSite, ref List<SemanticWordJsonDto> Dictionary);

        IDataResult<InputDto> SimilarityCalculate(WebSite webSite, List<WebSite> webSitePool,bool subUrlCheck=false,bool semanticCheck = false);

        IDataResult<string> GetTitle(string StringHtmlPage);
    }
}