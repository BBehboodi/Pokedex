using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TrueLayer.Pokedex.Service;

namespace TrueLayer.Pokedex.API
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddHttpClient();
      services.AddPokedexServices();
      services.AddSwaggerGen(setup =>
      {
        var contact = new OpenApiContact
        {
          Email = "behboodi.b71@gmail.com",
          Name = "behzad behboodi",
          Url = new Uri("https://www.linkedin.com/in/behzadbehboodi")
        };
        var apiInfo = new OpenApiInfo
        {
          Title = "Pokedex",
          Version = "v1",
          Contact = contact,
          Description = "Pokemon Rest API challenge"
        };
        setup.SwaggerDoc(
          name: "v1",
          apiInfo
        );
        string xmlDocument = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xmlDocumentPath = Path.Combine(AppContext.BaseDirectory, xmlDocument);
        setup.IncludeXmlComments(xmlDocumentPath);
      });
      services.AddSingleton<PokedexConfiguration>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger(setup => { setup.SerializeAsV2 = true; });
        app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex v1"));
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
