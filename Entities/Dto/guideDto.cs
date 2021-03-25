//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

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
