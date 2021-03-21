//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class UrlTreeDto : IDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public UrlTreeDto SubUrl { get; set; } 
    }

    public class UrlsTreeDto : IDto
    {

        public string Title { get; set; }
        public string Url { get; set; }
        public List<UrlsTreeDto> SubUrls { get; set; }
    }
}