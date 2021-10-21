using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Responses.Translation;

namespace TrueLayer.Pokedex.Service.Proxies
{
  internal interface IFuntranslationProxy
  {
    Task<TranslationResponse?> GetShakespeareTranslation(string description);

    Task<TranslationResponse?> GetYodaTranslation(string description);
  }
}