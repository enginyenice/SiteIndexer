using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ITagAndPointDal
    {
        List<TagAndPoint> GetAll();
    }
}