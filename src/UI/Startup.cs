#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using System;
using System.Security.Cryptography.X509Certificates;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using DataAccess;
using BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UI.Utilities;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Server.Kestrel;

namespace UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets("aspnet-DevMarketPlace-77c453d8-751d-48e5-a5b1-d0ec19e5d2b0");
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<DevMarketplaceDataContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 8;

                var loginPath = Configuration.GetSection("Authentication").GetValue<string>("LogInPath");
                var logoutPath = Configuration.GetSection("Authentication").GetValue<string>("LogOutPath");

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = loginPath;
                options.Cookies.ApplicationCookie.LogoutPath = logoutPath;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Add framework services.
            services.AddMvc().AddControllersAsServices();

            return ConfigureIoC(services);
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(x =>
                {
                    x.AssemblyContainingType(typeof(Startup));
                    x.WithDefaultConventions();
                });

                var connection = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                services.AddEntityFrameworkSqlServer().AddDbContext<DevMarketplaceDataContext>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);
                //Populate the container using the service collection
                config.Populate(services);
                config.AddRegistry<DataAccessRegistry>();
                config.AddRegistry<BusinessLogicRegistry>();

                config.For<IConfiguration>().Use(Configuration).Singleton();
                config.For<IViewRenderer>().Use<ViewRender>().Singleton();
                config.For<IUrlUtilityWrapper>().Use<UrlUtilityWrapper>().Singleton();
                config.For<IValidPasswordGenerator>().Use<ValidPasswordGenerator>().Singleton();
                config.ForConcreteType<UserManagerWrapper<ApplicationUser>>()
                    .Configure.Setter<UserManager<ApplicationUser>>()
                    .Is(c => c.GetInstance<UserManager<ApplicationUser>>());

                config.ForConcreteType<SignInManagerWrapper<ApplicationUser>>()
                    .Configure.Setter<SignInManager<ApplicationUser>>()
                    .Is(c => c.GetInstance<SignInManager<ApplicationUser>>());
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeDatabase(app);
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseGitHubAuthentication(new GitHubAuthenticationOptions {
                ClientId = Configuration.GetSection("LoginProviders").GetSection("GitHub")["GitHubClientId"],
                ClientSecret = Configuration.GetSection("LoginProviders").GetSection("GitHub")["GitHubClientSecret"],
                Scope = { "user:email" }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<DevMarketplaceDataContext>().Database.Migrate();
            }
        }
    }
}
