using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Openccpm.Web.Data;
using Openccpm.Web.Models;
using Openccpm.Web.Services;

namespace Openccpm.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
#if RELEASE 
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MS_TableConnectionString")));
#else
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
#endif

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            // 認証の設定 
            services.Configure<IdentityOptions>(options =>
            {
                // 実験のため一番ゆるいパターンのパスワード
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                // 10回間違えると30分だけロックアウトされる
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                // ユーザー名にメールアドレスを利用するため
                options.User.RequireUniqueEmail = true;
                // cookie の設定
                options.Cookies.ApplicationCookie.CookieName = "OpenccpmAuthCookie";
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);       // 150日間有効
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";                  // ログイン
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";                // ログアウト
                options.Cookies.ApplicationCookie.AccessDeniedPath = "/Account/AccessDenied";    // 拒否時
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "project_create",
                    template: "Projects/Create",
                    defaults: new { controller = "Projects", action = "Create" });

                routes.MapRoute(
                    name: "ticket_create",
                    template: "Projects/{id}/Tickets/Create",
                    defaults: new { controller = "Tickets", action = "Create" });
                routes.MapRoute(
                    name: "ticket",
                    template: "Projects/{id}/Tickets",
                    defaults: new { controller = "Tickets", action = "Index" });
                routes.MapRoute(
                    name: "project_detail",
                    template: "Projects/{id}",
                    defaults: new { controller = "Projects", action = "Details" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
