using Fora.Domain.Interface;
using Fora.Domain.Models;
using Fora.Infrastructure.Data;
using Fora.Infrastructure.Repositories;
using Fora.Web.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
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

            //Data
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<UserManager<UserModel>>();
            services.AddScoped<IUserStore<UserModel>, UserStore>();
            services.AddScoped<IRoleStore<string>, RoleStore>();
        }
    }
}
