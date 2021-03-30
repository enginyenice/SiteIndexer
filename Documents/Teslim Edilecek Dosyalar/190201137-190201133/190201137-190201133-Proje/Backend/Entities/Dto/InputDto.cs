using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class InputDto : IDto
    {
        public WebSite webSite { get; set; }
        public List<WebSite> webSitePool { get; set; }
    }
}