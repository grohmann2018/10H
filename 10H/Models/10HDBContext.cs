﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class _10HDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
