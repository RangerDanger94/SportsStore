﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SportsStore
{
    public class Startup
    {
        IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreIdentity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Error",
                    template: "Error",
                    defaults: new { controller = "Error", action = "Error"}
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    name: null, 
                    template: "{controller}/{action}/{id?}"
                );
            });
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
