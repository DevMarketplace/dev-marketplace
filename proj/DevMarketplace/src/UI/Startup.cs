using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using DataAccess;
using BusinessLogic;
using DataAccess.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UI.Utilities;

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
                builder.AddUserSecrets();
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
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
