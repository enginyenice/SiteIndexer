using Core.Entities;

namespace Entities.Dto
{
    public class SimilarityScoreDto : IDto
    {
        public float SimilarityScore { get; set; }
        public KeywordWebSiteDto webSite { get; set; }
    }

    public class SimilarityScoreSemanticDto : IDto
    {
        public float SimilarityScore { get; set; }
        public KeywordWebSiteSemanticDto webSite { get; set; }
    }
}