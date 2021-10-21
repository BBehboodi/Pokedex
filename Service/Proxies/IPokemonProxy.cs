using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Responses.Pokemon;

namespace TrueLayer.Pokedex.Service.Proxies
{
  internal interface IPokemonProxy
  {
    Task<PokemonResponse?> GetAsync(string name);
  }
}