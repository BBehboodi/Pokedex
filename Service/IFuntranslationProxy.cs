using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Dtos.Translation;

namespace TrueLayer.Pokedex.Service
{
  internal interface IFuntranslationProxy
  {
    Task<Translation?> GetShakespeareTranslation(string description);

    Task<Translation?> GetYodaTranslation(string description);
  }
}