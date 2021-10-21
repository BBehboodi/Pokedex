using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Responses.Pokemon;

namespace TrueLayer.Pokedex.Service.Proxies
{
  internal class PokemonProxy : IPokemonProxy
  {
    private readonly IHttpClientFactory httpClientFactory;
    private readonly PokedexConfiguration configuration;

    public PokemonProxy(IHttpClientFactory httpClientFactory, PokedexConfiguration configuration)
    {
      this.httpClientFactory = httpClientFactory;
      this.configuration = configuration;
    }

    public async Task<PokemonResponse?> GetAsync(string name)
    {
      var httpClient = httpClientFactory.CreateClient();
      var httpResponse = await httpClient.GetAsync($"{configuration.PokemonBaseUrl}v2/pokemon-species/{name}");
      if (httpResponse.StatusCode == HttpStatusCode.NotFound)
      {
        return null;
      }
      string response = await httpResponse.Content.ReadAsStringAsync();
      var pokemonResponse = JsonConvert.DeserializeObject<PokemonResponse?>(response);
      return pokemonResponse;
    }
  }
}