using Core.Entities;

namespace Entities.Dto
{
    public class SimilarityScoreSemanticDto : IDto
    {
        public float SimilarityScore { get; set; }
        public KeywordWebSiteSemanticDto webSite { get; set; }
    }
}