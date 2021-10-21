using Newtonsoft.Json;

namespace TrueLayer.Pokedex.Service.Responses.Translation
{
  internal class Content
  {
    [JsonConstructor]
    public Content(string translated, string text, string translation)
    {
      Translated = translated;
      Text = text;
      Translation = translation;
    }

    [JsonProperty("translated")]
    public string Translated { get; }


    [JsonProperty("text")]
    public string Text { get; }


    [JsonProperty("translation")]
    public string Translation { get; }
  }
}