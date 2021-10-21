using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Dtos.Pokemon;

namespace TrueLayer.Pokedex.Service
{
  internal interface IPokemonProxy
  {
    Task<Pokemon?> GetAsync(string name);
  }
}