using KristaShop.Business.Interfaces;
using KristaShop.Business.Services;
using KristaShop.DataAccess.Domain;
using KristaShop.DataAccess.Interfaces;
using KristaShop.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace KristaShop.Business.Utils
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMContentService, MBodyService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IUrlAclService, UrlAclService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IDiscountService, DiscountService>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<INomService, NomService>();

            return services;
        }

        public static IServiceCollection AddShopDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IShopRepository<>), typeof(ShopRepository<>));
            services.AddScoped(typeof(ICacheRepository<>), typeof(CacheRepository<>));

            services.AddScoped(typeof(INomRepository<>), typeof(NomRepository<>));

            /*Данная конфигурация используется для MYSQL*/
            services.AddDbContextPool<KristaShopDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("KristaShopMysql"), mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql))));

            return services;
        }
    }
}
