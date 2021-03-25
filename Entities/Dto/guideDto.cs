using Core.Entities;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class GuideDto : IDto
    {
        public website Website { get; set; }
        public List<website> WebsitePool { get; set; }
    }

    public class website : IDto
    {
        public string Url { get; set; }
    }
}