﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
