using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;

        public Startup (IConfiguration configuration) {
            this._configuration = configuration;

        }

        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<DutchContext> (cfg => {
                cfg.UseSqlServer (_configuration.GetConnectionString ("DutchConnectionString"));
            });

            services.AddTransient<DutchSeeder> ();

            // Add support for real mail service later
            services.AddTransient<IMailService, NullMailService> ();

            services.AddScoped<IDutchRepository, DutchRepository> ();

            services.AddControllers ();

            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/error");
            }

            app.UseStaticFiles ();
            app.UseNodeModules ();

            app.UseRouting ();

            app.UseEndpoints (cfg => {
                cfg.MapControllerRoute ("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            });

            //  app.UseMvc(cfg =>
            // {
            //     cfg.MapRoute("Fallback",
            //         "{controller}/{action}/{id?}",
            //         new { controller = "App", Action = "Index" });
            // });
        }
    }
}