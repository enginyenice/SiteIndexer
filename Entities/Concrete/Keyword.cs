using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Keyword : Word, IEntity
    {
        public int score { get; set; }
    }

    public class SemanticKeyword : IEntity
    {
        public string word { get; set; }
        public List<Keyword> similarWords { get; set; }
    }
}