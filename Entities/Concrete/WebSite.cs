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
        public int Id { get; set; }
        public string Url { get; set; }
        public string SiteTitle { get; set; }
        public string Content { get; set; }
    }
}