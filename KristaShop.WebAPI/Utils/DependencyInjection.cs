using KristaShop.WebAPI.Data;
using KristaShop.WebAPI.Interfaces;
using KristaShop.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace KristaShop.WebAPI.Utils
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiBusiness(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        public static IServiceCollection AddAsupDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            /*Данная конфигурация используется для MYSQL*/
            services.AddDbContextPool<KristaAsupDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("KristaConnectionMysql"), mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql))));

            return services;
        }
    }
}
