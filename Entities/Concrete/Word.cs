﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Word : IEntity
    {
        public string word { get; set; }
        public int frequency { get; set; }

    }
}
