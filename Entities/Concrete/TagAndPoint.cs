using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TagAndPoint : IEntity
    {
        public string before { get; set; }
        public string after { get; set; }
        public int point { get; set; }
    }
}
