using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Abstract
{
    public interface IJsonReader
    {
        IDataResult<List<SemanticWordJsonDto>> getSemanticKeywords();

    }
}
