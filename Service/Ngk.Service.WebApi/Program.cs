﻿using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ngk.Service.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                    .UseConfiguration(config)
                    .UseUrls("http://*:54150")
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
