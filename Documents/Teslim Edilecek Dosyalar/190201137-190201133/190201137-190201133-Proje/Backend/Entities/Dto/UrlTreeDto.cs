using Core.Entities;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class UrlTreeDto : IDto
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<UrlTreeDto> Children { get; set; }
    }
}