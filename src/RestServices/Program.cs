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

using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;

namespace RestServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var envSettingsPath = Path.Combine(currentDirectoryPath, "envsettings.json");
            var envSettings = JObject.Parse(File.ReadAllText(envSettingsPath));
            var enviromentValue = envSettings["aspNetEnvironment"].ToString();


            var host = new WebHostBuilder();
            
            if (enviromentValue.ToLower() == "development" || enviromentValue.ToLower() == "staging")
            {
                host.UseKestrel(options =>
                {
                    options.AddServerHeader = false;
                    options.UseHttps(new X509Certificate2("DevMarketplaceLocal.pfx", "1234"));
                });
            }
            else
            {
                host.UseKestrel(options =>
                {
                    options.UseConnectionLogging();
                });
            }

            if (!string.IsNullOrWhiteSpace(enviromentValue))
            {
                host.UseEnvironment(enviromentValue);
            }

            host.UseUrls("http://localhost:6170/", "https://localhost:6171/")
            .CaptureStartupErrors(true)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build()
            .Run();
        }
    }
}
