using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantReviews.DataAccess;
using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.Domain;

namespace NoteTakingApp.WebApp
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
            // ASP.NET implements a DI container
            // such that, you register services globally here,
            // and then, they'll be automatically instantiated and provided
            //    to the constructors that need them.

            // there are three "service lifetimes":
            // - singleton - one instance of the class, one object,
            //                shared among all objects that request one via ctor
            // - scoped - one instance shared within each "scope"
            //              (each HTTP request lifecycle is one scope)
            //              (the default for DbContexts)
            // - transient - no instances shared, every time a new object

            //services.AddSingleton(new Note { Text = "hello" });

            if (Configuration["OtherRepository"] == "true")
            {
                //services.AddScoped<IRepository, NonEfRepository>();
            }
            else
            {
                // "if a class asks for an IRepository, give it a Repository"
                services.AddScoped<IRepository, Repository>();
            }
            services.AddDbContext<RearlesDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RearlesDB"));
                options.LogTo(Console.WriteLine);
            });

            services.AddControllersWithViews();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "restaurantdetails",
                    pattern: "restaurantdetails/{restaurant}",
                    defaults: new { controller = "Restaurant", action = "Details" });
                endpoints.MapControllerRoute(
                    name: "restaurantindex",
                    pattern: "restaurantindex/{restaurant}",
                    defaults: new { controller = "Restaurant", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "restaurantedit",
                    pattern: "restaurantedit/{restaurant}",
                    defaults: new { controller = "Restaurant", action = "Edit" });
                endpoints.MapControllerRoute(
                    name: "restaurantdelete",
                    pattern: "restaurantdelete/{restaurant}",
                    defaults: new { controller = "Restaurant", action = "Delete" });
                endpoints.MapControllerRoute(
                    name: "reviewdetails",
                    pattern: "reviewdetails/{review}",
                    defaults: new { controller = "Review", action = "Details" });
                endpoints.MapControllerRoute(
                    name: "reviewindex",
                    pattern: "reviewindex/{review}",
                    defaults: new { controller = "Review", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "reviewedit",
                    pattern: "reviewedit/{review}",
                    defaults: new { controller = "Review", action = "Edit" });
                endpoints.MapControllerRoute(
                    name: "reviewdelete",
                    pattern: "reviewdelete/{review}",
                    defaults: new { controller = "Review", action = "Delete" });
                endpoints.MapControllerRoute(
                   name: "userdetails",
                   pattern: "userdetails/{user}",
                   defaults: new { controller = "User", action = "Details" });
                endpoints.MapControllerRoute(
                  name: "usercreate",
                  pattern: "usercreate/{user}",
                  defaults: new { controller = "User", action = "Create" });
                endpoints.MapControllerRoute(
                  name: "restaurantcreate",
                  pattern: "restaurantcreate/{restaurant}",
                  defaults: new { controller = "Restaurant", action = "Create" });
                endpoints.MapControllerRoute(
                  name: "reviewcreate",
                  pattern: "reviewcreate/{review}",
                  defaults: new { controller = "Review", action = "Create" });
                endpoints.MapControllerRoute(
                    name: "userindex",
                    pattern: "userindex/{user}",
                    defaults: new { controller = "User", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "useredit",
                    pattern: "useredit/{user}",
                    defaults: new { controller = "User", action = "Edit" });
                endpoints.MapControllerRoute(
                    name: "userdelete",
                    pattern: "userdelete/{user}",
                    defaults: new { controller = "User", action = "Delete" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
