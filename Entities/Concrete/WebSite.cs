﻿//Created By Engin Yenice
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
            Keywords = new List<string>();
            Frequances = new List<Frequance>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string StringWebSite { get; set; }
        public List<string> Keywords { get; set; }
        public List<Frequance> Frequances { get; set; }
    }
}