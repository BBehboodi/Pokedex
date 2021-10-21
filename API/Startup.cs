using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrueLayer.Pokedex.Service;

namespace TrueLayer.Pokedex.API
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHttpClient();
      services.AddPokedexServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGet("/", async context =>
              {
            await context.Response.WriteAsync("Hello World!");
          });
      });
    }
  }
}
