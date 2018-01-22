using Fora.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fora.Web.Infrastructure
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("ForaContext");
            var migrationAssembly = typeof(ForaContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<ForaContext>(options => options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migrationAssembly)));
        }

        public static IServiceProvider MigrateDb(this IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
                context.Database.Migrate();
            }
            return serviceProvider;
        }
    }
}
