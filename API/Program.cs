using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using Microsoft.Extensions.Hosting;

namespace TrueLayer.Pokedex.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(configure =>
        {
          configure.UseStartup<Startup>();
        })
        .UseNLog();

    }
  }
}
