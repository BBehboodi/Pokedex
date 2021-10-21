using Microsoft.Extensions.Configuration;

namespace TrueLayer.Pokedex.Service
{
  public class PokedexConfiguration
  {
    public PokedexConfiguration(IConfiguration configuration)
    {
      PokemonBaseUrl = configuration.GetSection("PokemonBaseUrl").Value;
      FuntranslationsBaseUrl = configuration.GetSection("FuntranslationsBaseUrl").Value;
    }

    public string PokemonBaseUrl { get; }

    public string FuntranslationsBaseUrl { get; }
  }
}
