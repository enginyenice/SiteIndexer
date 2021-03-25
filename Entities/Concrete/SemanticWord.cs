using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class SemanticWord : IEntity
    {
        public string word { get; set; }
        public List<string> similarWords { get; set; }
    }
}