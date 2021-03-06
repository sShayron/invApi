using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using invApi.Models;

namespace invApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING")))
            {
                /* If there's a postgres connection string, use it. */
                services.AddEntityFramework()
                    .AddEntityFrameworkNpgsql()
                    .AddDbContext<ModelContext>(options => options.UseNpgsql(
                        Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING")));
            }
            else
            {
                /* Fallback to sqlite, for local development. */
                services.AddEntityFramework()
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<ModelContext>(options => options.UseSqlite("Filename=database.sqlite"));
            }

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ModelContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            });

            services.Configure<AuthenticationOptions>(options =>
            {
                options.AutomaticChallenge = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
        }
    }
}
