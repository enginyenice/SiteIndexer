//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class UrlRankingControllerDtos : IDto
    {
        public WebSite targetWebSite { get; set; }
        public List<WebSite> pool { get; set; }
    }
}