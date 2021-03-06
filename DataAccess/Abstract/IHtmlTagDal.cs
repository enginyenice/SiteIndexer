//Created By Engin Yenice
//enginyenice2626@gmail.com

using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IHtmlTagDal
    {
        public List<HtmlTag> GetAll();
    }
}