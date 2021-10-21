using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Responses.Translation;

namespace TrueLayer.Pokedex.Service.Proxies
{
  internal class FuntranslationProxy : IFuntranslationProxy
  {
    private readonly IHttpClientFactory httpClientFactory;
    private readonly PokedexConfiguration configuration;

    public FuntranslationProxy(IHttpClientFactory httpClientFactory, PokedexConfiguration configuration)
    {
      this.httpClientFactory = httpClientFactory;
      this.configuration = configuration;
    }

    public async Task<TranslationResponse?> GetShakespeareTranslation(string description)
    {
      var httpClient = httpClientFactory.CreateClient();
      var httpResponse = await httpClient.GetAsync($"{configuration.FuntranslationsBaseUrl}translate/shakespeare.json?text={description}");
      string response = await httpResponse.Content.ReadAsStringAsync();
      var translationResponse = JsonConvert.DeserializeObject<TranslationResponse?>(response);
      return translationResponse;
    }

    public async Task<TranslationResponse?> GetYodaTranslation(string description)
    {
      var httpClient = httpClientFactory.CreateClient();
      var httpResponse = await httpClient.GetAsync($"{configuration.FuntranslationsBaseUrl}translate/yoda.json?text={description}");
      string response = await httpResponse.Content.ReadAsStringAsync();
      var translationResponse = JsonConvert.DeserializeObject<TranslationResponse?>(response);
      return translationResponse;
    }
  }
}