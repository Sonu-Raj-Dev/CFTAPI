using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DashBoardAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    string evs = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    if (evs == "Dev")
                    {
                        webBuilder.UseUrls("http://localhost:6178").UseStartup<Startup>();
                    }
                    else
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                    // Remove .UseIIS() as it's handled automatically
                });
    }
}