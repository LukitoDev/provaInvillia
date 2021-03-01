using Autofac.Extensions.DependencyInjection;
using LocaGames.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LocaGames.API
{
    public class Program
    {

        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    var dbContext = services.GetRequiredService<SqlContext>();
                    if (dbContext.Database.IsSqlServer())
                    {
                        dbContext.Database.Migrate();
                        i = 20;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(4000);
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
