using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class SemanticWordJsonDto : IDto
    {
        public char letter { get; set; }
        public List<SemanticWord> data { get; set; }
    }
}
