using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
   public class WebSiteKeywordDto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public List<string> Keywords { get; set; }
    }
}
