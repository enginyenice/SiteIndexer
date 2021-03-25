using Core.Entities;

namespace Entities.Concrete
{
    public class Keyword : Word, IEntity
    {
        public int score { get; set; }
    }

    public class SemanticKeyword : Word, IEntity
    {
        public int score { get; set; }
        public string similar { get; set; }
    }
}