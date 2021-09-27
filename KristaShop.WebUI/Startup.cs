using AutoMapper;
using ImageThumbnail.AspNetCore.Middleware;
using KristaShop.Business.Clients;
using KristaShop.Business.Interfaces;
using KristaShop.Business.Utils;
using KristaShop.DataReadOnly.Utils;
using KristaShop.WebUI.Filters;
using KristaShop.WebUI.Middleware;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace KristaShop.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Business.Utils.MappingProfile), typeof(DataReadOnly.Utils.MappingProfile), typeof(Utils.MappingProfile));
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Identity/Login/";
                    options.AccessDeniedPath = "/Identity/Login/";
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });
            services.AddShopDbContext(Configuration);
            services.AddBusiness();

            services.AddHttpClient<IAsupApiClient<RegHashViewModel>, AsupApiClient<RegHashViewModel>>();
            services.AddReadOnlyBusiness();
            services.AddReplicaDbContext(Configuration);

            services.AddScoped<PermissionFilter>();

            services.AddHttpContextAccessor();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware<LinkBasedAuthMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            ImageThumbnailOptions options = new ImageThumbnailOptions("Gallery", "Thumbnails");
            //options.ImagesDirectory = @"D:\Projects\Images";
            //options.CacheDirectoryName = @"D:\Projects\Images\";
            options.ThumbnailBackground = Color.Empty;
            app.UseImageThumbnail(options);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Gallery")),
                RequestPath = "/Gallery"
            });

            app.UseCookiePolicy();
            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Error",
                    template: "{*url}",
                    defaults: new { controller = "Error", action = "Error404" });
            });
        }
    }
}
