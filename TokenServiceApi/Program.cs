using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace TokenServiceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServerWithAspNetIdentity";

            var seed = args.Any(x => x == "/seed");
            if (seed) args = args.Except(new[] { "/seed" }).ToArray();

            var host = BuildWebHost(args);

            if (seed)
            {
                SeedData.EnsureSeedData(host.Services);
                return;
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                })
                .Build();
        }
    }
}
