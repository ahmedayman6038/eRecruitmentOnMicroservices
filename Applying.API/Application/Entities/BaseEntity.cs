﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
