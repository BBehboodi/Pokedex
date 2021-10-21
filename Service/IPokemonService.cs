using System.Threading.Tasks;
using TrueLayer.Pokedex.Domain.Responses.Pokemon;

namespace TrueLayer.Pokedex.Service
{
  public interface IPokemonService
  {
    Task<ServiceResult<PokemonResponse>> GetAsync(string name);

    Task<ServiceResult<PokemonResponse>> TranslateAsync(string name);
  }
}