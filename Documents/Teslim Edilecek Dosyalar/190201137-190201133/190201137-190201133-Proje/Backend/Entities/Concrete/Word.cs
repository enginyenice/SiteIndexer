using Core.Entities;

namespace Entities.Concrete
{
    public class Word : IEntity
    {
        public string word { get; set; }
        public int frequency { get; set; }
    }
}