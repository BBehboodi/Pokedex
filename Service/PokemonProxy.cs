using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service.Dtos.Pokemon;

namespace TrueLayer.Pokedex.Service
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

    public async Task<Pokemon?> GetAsync(string name)
    {
      var httpClient = httpClientFactory.CreateClient();
      var httpResponse = await httpClient.GetAsync($"{configuration.PokemonBaseUrl}v2/pokemon-species/{name}");
      if (httpResponse.StatusCode == HttpStatusCode.NotFound)
      {
        return null;
      }
      string response = await httpResponse.Content.ReadAsStringAsync();
      var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
      return pokemon;
    }
  }
}