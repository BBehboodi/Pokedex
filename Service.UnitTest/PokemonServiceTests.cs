using Moq;
using System;
using System.Threading.Tasks;
using TrueLayer.Pokedex.Service;
using TrueLayer.Pokedex.Service.Dtos.Pokemon;
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
      var pokemon = new Pokemon(
        id: 1,
        name,
        isLegendary: false,
        flavorTextEntries,
        habitat
      );
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemon);

      // Act
      var serviceResult = await pokemonService.GetAsync(name);

      // Assert
      Assert.True(serviceResult.Succeeded);
      Assert.Null(serviceResult.Errors);
      Assert.NotNull(serviceResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
    }

    [Fact]
    public async Task GetAsync_NoneExistingName_ReturnsNotFoundError()
    {
      // Arrange
      string name = "NoneExistingName";
      mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(() => null);

      // Act
      var serviceResult = await pokemonService.GetAsync(name);

      // Assert
      Assert.False(serviceResult.Succeeded);
      Assert.Contains(serviceResult.Errors, error => error.Type == ErrorTypes.NotFound);
      Assert.Null(serviceResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
    }

    [Fact]
    public async Task GetAsync_InvalidArgument_ReturnsInvalidArgumentError()
    {
      // Arrange
      string name = string.Empty;

      // Act
      var serviceResult = await pokemonService.GetAsync(name);

      // Assert
      Assert.False(serviceResult.Succeeded);
      Assert.Contains(serviceResult.Errors, error => error.Type == ErrorTypes.InvalidArgument);
      Assert.Null(serviceResult.Result);
      mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Never);
    }
  }
}