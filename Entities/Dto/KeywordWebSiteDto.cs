using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class KeywordWebSiteDto : IDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Keyword> Keywords { get; set; }
    }
    public class KeywordWebSiteSemanticDto : IDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Keyword> Keywords { get; set; }
        public List<SemanticKeyword> SemanticKeyword { get; set; }
    }
}
