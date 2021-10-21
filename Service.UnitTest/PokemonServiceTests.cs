using Moq;
using System;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service;
using TrueLayer.Pokedex.Service.Proxies;
using TrueLayer.Pokedex.Service.Responses.Pokemon;
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

    [Fact]
    public async Task GetAsync_ValidName_ReturnsPokemon()
    {
      // Arrange
      string name = "ValidName";
      var habitat = new Habitat("SomeHabitat");
      var flavorTextEntries = Array.Empty<FlavorTextEntry>();
      var pokemonResponse = new PokemonResponse(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries,
        habitat
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
  }
}