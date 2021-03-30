using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class SemanticWordJsonDto : IDto
    {
        public char letter { get; set; }
        public List<SemanticWord> data { get; set; }
    }
}