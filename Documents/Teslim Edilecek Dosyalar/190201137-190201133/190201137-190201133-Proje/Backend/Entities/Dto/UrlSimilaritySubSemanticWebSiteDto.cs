using Core.Entities;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class UrlSimilaritySubSemanticWebSiteDto : IDto
    {
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreSemanticDto> webSitePool { get; set; }
        public List<UrlTreeDto> UrlTree { get; set; }
    }
}