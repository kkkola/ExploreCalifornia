using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCaliforniaNow.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreCaliforniaNow
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FormattingServices>();
            services.AddTransient<FeatureToggle>(x => new FeatureToggle
            {
                EnableDevelopmentProperties = Configuration.GetValue<bool>("FeatureToggle:EnableDevelopmentProperties")
            }
            );
            services.AddDbContext<BlogContext>(options =>
            {
                var connectionstring = Configuration.GetConnectionString("BlogDbContext");
                options.UseSqlServer(connectionstring);
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,FeatureToggle featureToggle)
        {
            app.UseExceptionHandler("/error.html");
            if (featureToggle.EnableDevelopmentProperties)
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.Use(async (context, next) =>
                {
                    if(context.Request.Path.Value.Contains("invalid"))
                    {
                        throw new Exception("Error!");
                    }
                    await next();
                });
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseFileServer();
        }
    }
}
