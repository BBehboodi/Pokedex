# Pokedex
Pokemon Rest API challenge

## Prerequisites
- .NetFramework 5: https://dotnet.microsoft.com/download/dotnet/5.0

## Nuget packages
- Swashbuckle.AspNetCore 6.2.3: https://www.nuget.org/packages/Swashbuckle.AspNetCore
- Microsoft.Extensions.Http 5.0.0: https://www.nuget.org/packages/Microsoft.Extensions.Http
- Newtonsoft.Json 13.0.1: https://www.nuget.org/packages/Newtonsoft.Json
- Moq 4.16.1: https://www.nuget.org/packages/Moq
- NLog 4.7.11: https://www.nuget.org/packages/NLog
- NLog.Web.AspNetCore 4.14.0: https://www.nuget.org/packages/NLog.Web.AspNetCore/

## Third party APIs
The following APIs has been used to access the pokemon's information:
- https://pokeapi.co/api/v2
- https://funtranslations.com/api/shakespeare
- https://funtranslations.com/api/yoda

## Running the application
On windows, open the command prompt, Clone the repository, change your working directory to API directory, and run "dotnet run".
The application will be hosted in the following addresses: 
- http://localhost:5000
- https://localhost:5001

## API documentation
The Pokemon API is fully documented using swagger. After running the application, visit the following addresses to take a look at the documentation.
- http://localhost:5000/swagger/index.html
- https://localhost:5001/swagger/index.html

## Future implementations
- Writing integration tests.
- Versioning the data transfer objects (dtos).
- Implementing SDK project for usage of other projects (dtos in domain layer should use in SKD project).