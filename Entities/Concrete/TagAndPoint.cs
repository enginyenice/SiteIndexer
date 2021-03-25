using Core.Entities;

namespace Entities.Concrete
{
    public class TagAndPoint : IEntity
    {
        public string before { get; set; }
        public string after { get; set; }
        public int score { get; set; }
    }
}