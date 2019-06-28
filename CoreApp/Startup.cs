using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreApp.Models;
using CoreApp.Services;
using CoreApp.CustomFilters;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI;
using Newtonsoft.Json.Serialization;
using CoreApp.Middlewares;

namespace CoreApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Register the DbContext for Application Data
            services.AddDbContext<MyAppDbContext>(options => 
                    options.UseSqlServer(
                          Configuration.GetConnectionString("AppConnection")));

            // Ends here

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            // This will provide an injection for RoleManger
            // with Role and User Management

            services.AddIdentity<IdentityUser, IdentityRole>()
                  .AddDefaultUI(UIFramework.Bootstrap4)
               .AddEntityFrameworkStores<ApplicationDbContext>();

            // Create an Identity Role Policy using Policy Builder using Authorization Service
            services.AddAuthorization(
                   options => {
                       options.AddPolicy("readpolicy", policy =>
                       {
                           policy.RequireRole("Admin", "Manager", "Clerk");
                       });

                       options.AddPolicy("writepolicy", policy =>
                       {
                           policy.RequireRole("Admin", "Manager");
                       });
                   }
                );
            // Ends Here

            // Register the DepartmentService and EmployeeService in DI as Scopped
            services.AddScoped<IService<Department,int>, DepartmentService>();
            services.AddScoped<IService<Employee, int>, EmployeeService>();
            // Ends Here

            // Adding Filter for MVC
            services.AddMvc(
                  options =>
                  {
                      options.Filters.Add(typeof(LogFilterAttribute));
                    //  options.Filters.Add(typeof(CustomExceptionFilter));
                  }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)

                  .AddJsonOptions(options => options.SerializerSettings.ContractResolver
              = new DefaultContractResolver());
              
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            // Custom Middleware
            app.UseCustomExceptionMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Department}/{action=Index}/{id?}");
            });
        }
    }
}
