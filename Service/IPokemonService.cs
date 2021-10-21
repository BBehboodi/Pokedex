using System.Threading.Tasks;
using TrueLayer.Pokedex.Domain.Dtos;

namespace TrueLayer.Pokedex.Service
{
  public interface IPokemonService
  {
    Task<ServiceResult<Pokemon>> GetAsync(string name);

    Task<ServiceResult<Pokemon>> TranslateAsync(string name);
  }
}