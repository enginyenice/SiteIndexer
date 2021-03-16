using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryTagAndPoint : ITagAndPointDal
    {
        List<TagAndPoint> tagAndPoints;
        public InMemoryTagAndPoint()
        {
            tagAndPoints = new List<TagAndPoint>
            {

                new TagAndPoint { before = "<h1", after = "</h1>", point = 10 },
                new TagAndPoint { before = "<h2", after = "</h2>", point = 9 },
                new TagAndPoint { before = "<h3", after = "</h3>", point = 8 },
                new TagAndPoint { before = "<h4", after = "</h4>", point = 7 },
                new TagAndPoint { before = "<h5", after = "</h5>", point = 6 },
                new TagAndPoint { before = "<h6", after = "</h6>", point = 5 },
                new TagAndPoint { before = "<b", after = "</b>", point = 4 }
            };
        }

        public List<TagAndPoint> GetAll()
        {
            return tagAndPoints;
        }
    }
}
