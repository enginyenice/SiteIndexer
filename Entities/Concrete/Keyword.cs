using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Keyword : Word, IEntity
    {
        public int score { get; set; }
    }
}
