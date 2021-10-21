using Microsoft.Extensions.DependencyInjection;

namespace TrueLayer.Pokedex.Service
{
  public static class PokedexServiceCollectionExtensions
  {
    public static void AddPokedexServices(this IServiceCollection services)
    {
      services.AddScoped<IPokemonProxy, PokemonProxy>();
      services.AddScoped<IPokemonService, PokemonService>();
    }
  }
}