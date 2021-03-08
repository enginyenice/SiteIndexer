//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class FrequencyWebSiteDto : IDto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public List<Frequance> Frequances { get; set; }
    }
}