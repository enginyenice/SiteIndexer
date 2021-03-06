//Created By Engin Yenice
//enginyenice2626@gmail.com

using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryHtmlTagDal : IHtmlTagDal
    {
        private readonly List<HtmlTag> htmlTags;

        public InMemoryHtmlTagDal()
        {
            htmlTags = new List<HtmlTag>
            {
                new HtmlTag{Id=1,Tag="html"},
                /*
                 * new HtmlTag{Id=1,Tag="h1"},
                new HtmlTag{Id=1,Tag="h2"},
                new HtmlTag{Id=1,Tag="h3"},
                new HtmlTag{Id=1,Tag="h4"},
                new HtmlTag{Id=1,Tag="h5"},
                new HtmlTag{Id=1,Tag="p"},
                new HtmlTag{Id=1,Tag="span"},
                new HtmlTag{Id=1,Tag="ol"},
                new HtmlTag{Id=1,Tag="ul"},
                new HtmlTag{Id=1,Tag="table"},
                */
            };
        }

        public List<HtmlTag> GetAll()
        {
            return htmlTags;
        }
    }
}