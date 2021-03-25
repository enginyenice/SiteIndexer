//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

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