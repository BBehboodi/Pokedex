using Microsoft.Extensions.DependencyInjection;
using TrueLayer.Pokedex.Service.Proxies;

namespace TrueLayer.Pokedex.Service
{
  public static class PokedexServiceCollectionExtensions
  {
    public static void AddPokedexServices(this IServiceCollection services)
    {
      services.AddScoped<IPokemonProxy, PokemonProxy>();
      services.AddScoped<IFuntranslationProxy, FuntranslationProxy>();
      services.AddScoped<IPokemonService, PokemonService>();
    }
  }
}