using Fora.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Infrastructure.Data
{
    public class ForaContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ForaContext(DbContextOptions options): base(options)
        {

        }
    }
}
