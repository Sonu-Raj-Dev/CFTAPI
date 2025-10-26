using DashBoardAPI;
using Microsoft.AspNetCore.Hosting;

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
                    webBuilder.UseUrls("http://localhost:6178").UseStartup<Startup>().UseIIS();
                }
                else
                {
                    // UseUrls("http://localhost:6178"). webBuilder.UseUrls("https://localhost:5003", "https://a141-115-96-217-13.in.ngrok.io", "https://localhost:5002").UseStartup<Startup>();
                    webBuilder.UseStartup<Startup>().UseIIS();
                }
            });

}
