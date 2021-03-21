
using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class UrlSimilarityWebSiteDto : IDto
    {        
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreDto> webSitePool { get; set; }

    }
    public class UrlSimilarityWithSubWebSiteDto : IDto
    {
        public KeywordWebSiteDto webSite { get; set; }
        public List<SimilarityScoreDto> webSitePool { get; set; }
        public List<UrlTreeDto> UrlTree { get; set; }

    }
}