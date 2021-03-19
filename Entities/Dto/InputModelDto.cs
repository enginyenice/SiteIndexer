//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class InputModelDto : IDto
    {
        public WebSite webSite { get; set; }
        public List<WebSite> webSitePool { get; set; }
    }
}