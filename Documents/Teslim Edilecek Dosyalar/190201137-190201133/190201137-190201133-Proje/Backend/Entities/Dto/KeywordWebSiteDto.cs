using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class KeywordWebSiteDto : IDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Keyword> Keywords { get; set; }
    }
}