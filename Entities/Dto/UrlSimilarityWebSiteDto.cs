using Core.Entities;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class UrlSimilarityWebSiteDto : IDto
    {
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreDto> webSitePool { get; set; }
    }

    public class UrlSimilaritySubWebSiteDto : IDto
    {
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreDto> webSitePool { get; set; }
        public List<UrlTreeDto> UrlTree { get; set; }
    }

    public class UrlSimilaritySubSemanticWebSiteDto : IDto
    {
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreSemanticDto> webSitePool { get; set; }
        public List<UrlTreeDto> UrlTree { get; set; }
    }
}