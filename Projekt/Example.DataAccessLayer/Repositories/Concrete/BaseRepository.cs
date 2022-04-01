﻿using Example.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DataAccessLayer.Repositories.Concrete
{
    public class BaseRepository
    {
        protected AppDbContext context;

        public BaseRepository()
        {
            context = AppDbContext.Create();
        }
    }
}
