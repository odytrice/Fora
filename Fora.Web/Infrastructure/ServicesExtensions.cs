using Fora.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fora.Web.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, ForaContext>();
        }
    }
}
