using Fora.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fora.Web.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ForaContext>
    {
        public ForaContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            var builder = new DbContextOptionsBuilder<ForaContext>()
                                .UseSqlServer(config.GetConnectionString("ForaContext"));

            return new ForaContext(builder.Options);
        }
    }
}
