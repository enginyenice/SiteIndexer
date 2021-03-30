using Core.Entities;

namespace Entities.Dto
{
    public class SimilarityScoreDto : IDto
    {
        public float SimilarityScore { get; set; }
        public KeywordWebSiteDto webSite { get; set; }
    }
}