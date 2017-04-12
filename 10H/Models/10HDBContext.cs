using System;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Formating> Formatings { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
