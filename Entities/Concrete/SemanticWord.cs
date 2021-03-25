using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SemanticWord : IEntity
    {
        public string word { get; set; }
        public List<string> similarWords { get; set; }

    }
}
