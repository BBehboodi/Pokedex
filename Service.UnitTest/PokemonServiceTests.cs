using Moq;
using System;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service;
using TrueLayer.Pokedex.Service.Proxies;
using TrueLayer.Pokedex.Service.Responses.Pokemon;
using TrueLayer.Pokedex.Service.Responses.Translation;
using Xunit;

namespace Service.UnitTest
{
  public class PokemonServiceTests
  {
    private readonly Mock<IPokemonProxy> mockedPokemonProxy;
    private readonly Mock<IFuntranslationProxy> mockedFuntranslationProxy;
    private readonly IPokemonService pokemonService;

    public PokemonServiceTests()
    {
      mockedPokemonProxy = new Mock<IPokemonProxy>();
      mockedFuntranslationProxy = new Mock<IFuntranslationProxy>();
      pokemonService = new PokemonService(mockedPokemonProxy.Object, mockedFuntranslationProxy.Object);
    }

    #region GetAsync

    [Fact]
    public async Task GetAsync_ValidName_ReturnsPokemon()
    {
      // Arrange
      string name = "ValidName";
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        Array.Empty<FlavorTextEntry>(),
        new Habitat("SomeHabitat")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);

      // Act
      var pokemonResult = await pokemonService.GetAsync(name);

      // Assert
      Assert.True(pokemonResult.Succeeded);
      Assert.Null(pokemonResult.Errors);
      Assert.NotNull(pokemonResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
    }

    [Fact]
    public async Task GetAsync_NoneExistingName_ReturnsNotFoundError()
    {
      // Arrange
      string name = "NoneExistingName";
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(() => null);

      // Act
      var pokemonResult = await pokemonService.GetAsync(name);

      // Assert
      Assert.False(pokemonResult.Succeeded);
      Assert.Contains(pokemonResult.Errors, error => error.Type == ErrorTypes.NotFound);
      Assert.Null(pokemonResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
    }

    [Fact]
    public async Task GetAsync_InvalidArgument_ReturnsInvalidArgumentError()
    {
      // Arrange
      string name = string.Empty;

      // Act
      var pokemonResult = await pokemonService.GetAsync(name);

      // Assert
      Assert.False(pokemonResult.Succeeded);
      Assert.Contains(pokemonResult.Errors, error => error.Type == ErrorTypes.InvalidArgument);
      Assert.Null(pokemonResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Never);
    }

    #endregion

    #region TranslateAsync

    [Fact]
    public async Task TranslateAsync_IsCave_ReturnsTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("cave")
      );
      var translationResponse = new TranslationResponse(
        new Success(total: 1),
        new Content(translated: "Some translated text", text: "", translation: "")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetYodaTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(translationResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.NotEqual(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_IsLegendary_ReturnsTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: true,
        new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("SomeHabitat")
      );
      var translationResponse = new TranslationResponse(
        new Success(total: 1),
        new Content(translated: "Some translated text", text: "", translation: "")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetYodaTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(translationResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.NotEqual(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_IsNotCaveAndIsNotLegendary_ReturnsTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries: new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("SomeHabitat")
      );
      var translationResponse = new TranslationResponse(
        new Success(total: 1),
        new Content(translated: "Some translated text", text: "", translation: "")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(translationResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.NotEqual(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_IsNotCaveAndIsLegendary_ReturnsTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: true,
        flavorTextEntries: new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("SomeHabitat")
      );
      var translationResponse = new TranslationResponse(
        new Success(total: 1),
        new Content(translated: "Some translated text", text: "", translation: "")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetYodaTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(translationResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.NotEqual(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_IsCaveAndIsNotLegendary_ReturnsTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries: new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("cave")
      );
      var translationResponse = new TranslationResponse(
        new Success(total: 1),
        new Content(translated: "Some translated text", text: "", translation: "")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetYodaTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(translationResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.NotEqual(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_DescriptionIsNull_ReturnsNotTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries: Array.Empty<FlavorTextEntry>(),
        new Habitat("SomeHabitat")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.Null(translationResult.Result?.Description);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
    }

    [Fact]
    public async Task TranslateAsync_TranslationNotFound_ReturnsNotTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries: new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("SomeHabitat")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(() => null);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.Equal(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_ZeroTotalTranslation_ReturnsNotTranslatedPokemon()
    {
      // Arrange
      string name = "ValidName";
      var flavorTextEntry = new FlavorTextEntry("Some text", new Language("en"));
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries: new FlavorTextEntry[] { flavorTextEntry },
        new Habitat("SomeHabitat")
      );
      var translationResponse = new TranslationResponse(
        new Success(total: 0),
        content: new Content(translated: "", text: "", translation: "")
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemonResponse);
      mockedFuntranslationProxy.Setup(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText)).ReturnsAsync(translationResponse);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.True(translationResult.Succeeded);
      Assert.NotNull(translationResult.Result);
      Assert.NotNull(translationResult.Result?.Description);
      Assert.Equal(translationResult.Result?.Description, flavorTextEntry.FlavorText);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetShakespeareTranslation(flavorTextEntry.FlavorText), Times.Once);
      mockedFuntranslationProxy.Verify(x => x.GetYodaTranslation(flavorTextEntry.FlavorText), Times.Never);
    }

    [Fact]
    public async Task TranslateAsync_NoneExistingName_ReturnsNotFoundError()
    {
      // Arrange
      string name = "NoneExistingName";
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(() => null);

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.False(translationResult.Succeeded);
      Assert.Contains(translationResult.Errors, error => error.Type == ErrorTypes.NotFound);
      Assert.Null(translationResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
    }

    [Fact]
    public async Task TranslateAsync_InvalidArgument_ReturnsInvalidArgumentError()
    {
      // Arrange
      string name = string.Empty;

      // Act
      var translationResult = await pokemonService.TranslateAsync(name);

      // Assert
      Assert.False(translationResult.Succeeded);
      Assert.Contains(translationResult.Errors, error => error.Type == ErrorTypes.InvalidArgument);
      Assert.Null(translationResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Never);
    }

    #endregion
  }
}