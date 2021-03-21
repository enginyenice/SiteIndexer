//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class WebSite : IEntity
    {
        public WebSite()
        {
            SubUrls = new List<WebSite>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public float SimilarityScore { get; set; }
        public string Content { get; set; } // Html page with all tags removed.
        public string StringHtmlPage { get; set; } // Html page with tags. 
        public List<Word> Words { get; set; }
        public List<Keyword> Keywords { get; set; }
        public List<WebSite> SubUrls { get; set; }

    }
}