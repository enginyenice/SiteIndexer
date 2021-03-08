//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class WebSiteRankingDto : IDto
    {
        public int RankingCount { get; set; }

        public WebSiteRankingDto()
        {
            RankingCount = 0;
        }

        public WebSite WebSite { get; set; }
    }
}