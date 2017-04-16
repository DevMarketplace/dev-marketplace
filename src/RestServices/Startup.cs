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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using DataAccess;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace RestServices
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddControllersAsServices();
            services.AddCors();

            return ConfigureIoC(services);
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();

            var connection = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
            services.AddEntityFrameworkSqlServer().AddDbContext<DevMarketplaceDataContext>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);

            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(x =>
                {
                    x.AssemblyContainingType(typeof(Startup));
                    x.WithDefaultConventions();
                });

                config.AddRegistry<DataAccessRegistry>();
                config.AddRegistry<BusinessLogicRegistry>();

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.WithOrigins("http://localhost:6147", "https://localhost:44391")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseCors(builder => builder.WithOrigins("http://localhost:6147", "https://localhost:44391", "http://www.devmarketplace.com", "https://www.devmarketplace.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
