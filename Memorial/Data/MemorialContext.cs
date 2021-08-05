using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Memorial.Models;

namespace Memorial.Data
{
    public class MemorialContext : DbContext
    {
        public MemorialContext (DbContextOptions<MemorialContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<BuyBooks> BuyBooks { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
