using Core.Utilities.Results;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface IJsonReader
    {
        IDataResult<List<SemanticWordJsonDto>> getSemanticKeywords();
    }
}