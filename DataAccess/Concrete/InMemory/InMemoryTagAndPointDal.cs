using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryTagAndPointDal : ITagAndPointDal
    {
        private List<TagAndPoint> tagAndPoints;

        public InMemoryTagAndPointDal()
        {
            tagAndPoints = new List<TagAndPoint>
            {
                new TagAndPoint { before = "<p",  after = "</p>",  score = 2 },
                new TagAndPoint { before = "<b",  after = "</b>",  score = 3 },
                new TagAndPoint { before = "<strong",  after = "</strong>",  score = 3 },
                new TagAndPoint { before = "<u",  after = "</u>",  score = 4 },
                new TagAndPoint { before = "<h6", after = "</h6>", score = 5 },
                new TagAndPoint { before = "<h5", after = "</h5>", score = 5 },
                new TagAndPoint { before = "<h4", after = "</h4>", score = 5 },
                new TagAndPoint { before = "<h3", after = "</h3>", score = 6 },
                new TagAndPoint { before = "<h2", after = "</h2>", score = 7 },
                new TagAndPoint { before = "<h1", after = "</h1>", score = 8 },
                new TagAndPoint { before = "<title", after = "</title>", score = 9 },
            };
        }

        public List<TagAndPoint> GetAll()
        {
            return tagAndPoints;
        }
    }
}