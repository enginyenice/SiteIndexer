//Created By Engin Yenice
//enginyenice2626@gmail.com

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Frequance : IEntity
    {
        public string Keyword { get; set; }
        public int Piece { get; set; }
    }
}