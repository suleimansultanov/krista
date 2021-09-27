using KristaShop.DataReadOnly.Domain;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.DataReadOnly.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace KristaShop.DataReadOnly.Utils
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReadOnlyBusiness(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDictionaryService, DictionaryService>();
            services.AddTransient<ICatalogItemReadService, CatalogItemReadService>();

            return services;
        }

        public static IServiceCollection AddReplicaDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IReadOnlyRepo<>), typeof(ReadOnlyRepo<>));
            /*Данная конфигурация используется для MYSQL*/
            services.AddDbContextPool<KristaReplicaDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("KristaReplicaMysql"), mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql))));

            return services;
        }
    }
}
