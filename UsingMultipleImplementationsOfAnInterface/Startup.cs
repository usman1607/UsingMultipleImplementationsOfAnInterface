using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingMultipleImplementationsOfAnInterface.Controllers;
using UsingMultipleImplementationsOfAnInterface.Enums;
using UsingMultipleImplementationsOfAnInterface.Services;

namespace UsingMultipleImplementationsOfAnInterface
{
    public class Startup
    {
        //public delegate ICustomLogger ServiceResolver(ServiceType serviceType);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Use an IEnumerable collection of service instances
            /*services.AddScoped<ICustomLogger, FileLogger>();
            services.AddScoped<ICustomLogger, EventLogger>();
            services.AddScoped<ICustomLogger, DbLogger>();*/

            //Use a delegate to retrieve a specific service instance         
            services.AddScoped<FileLogger>();
            services.AddScoped<EventLogger>();
            services.AddScoped<DbLogger>();

            services.AddTransient<ServiceResolver>(serviceProvider => serviceTypeName =>
            {
                switch (serviceTypeName)
                {
                    case ServiceType.FileLogger:
                        return serviceProvider.GetService<FileLogger>();
                    case ServiceType.EventLogger:
                        return serviceProvider.GetService<EventLogger>();
                    case ServiceType.DbLogger:
                        return serviceProvider.GetService<DbLogger>();
                    default:
                        return null;
                }
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
