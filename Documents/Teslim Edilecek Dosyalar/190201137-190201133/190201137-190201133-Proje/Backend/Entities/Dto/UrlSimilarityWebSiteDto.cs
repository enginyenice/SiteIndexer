using Core.Entities;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class UrlSimilarityWebSiteDto : IDto
    {
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreDto> webSitePool { get; set; }
    }
}