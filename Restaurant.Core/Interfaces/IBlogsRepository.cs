﻿using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IBlogsRepository : ICrudRepository<Blog>
    {
        Task<ICollection<Blog>> GetByFilter(DateTime after, string? AuthorName = null, string? TitleContains = null);
    }
}
