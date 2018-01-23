using Fora.Domain.Interface.Repositories;
using Fora.Domain.Managers;
using Fora.Domain.Models;
using Fora.Infrastructure.Data;
using Fora.Infrastructure.Repositories;
using Fora.Web.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fora.Web.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, ForaContext>();

            //Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();

            //Managers
            services.AddScoped<UserManager<UserModel>>();
            services.AddScoped<TopicManager>();

            //Security
            services.AddScoped<IUserStore<UserModel>, UserStore>();
            services.AddScoped<IRoleStore<string>, RoleStore>();
        }
    }
}
