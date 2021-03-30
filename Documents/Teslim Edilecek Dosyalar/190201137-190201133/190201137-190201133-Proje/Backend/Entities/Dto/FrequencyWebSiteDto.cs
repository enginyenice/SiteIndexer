using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class FrequencyWebSiteDto : IDto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public List<Word> Words { get; set; }
    }
}