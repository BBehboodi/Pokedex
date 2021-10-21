using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Responses.Translation
{
  internal class TranslationResponse
  {
    [JsonConstructor]
    public TranslationResponse(Success success, Content content)
    {
      Success = success;
      Content = content;
    }

    [JsonProperty("success")]
    public Success Success { get; }

    [JsonProperty("contents")]
    public Content Content { get; }
  }
}