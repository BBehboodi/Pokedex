# Pokedex
Pokemon Rest API challenge

## Prerequisites
- .NetFramework 5: https://dotnet.microsoft.com/download/dotnet/5.0

## Nuget Packages
- Swashbuckle.AspNetCore 6.2.3: https://www.nuget.org/packages/Swashbuckle.AspNetCore
- Microsoft.Extensions.Http 5.0.0: https://www.nuget.org/packages/Microsoft.Extensions.Http
- Newtonsoft.Json 13.0.1: https://www.nuget.org/packages/Newtonsoft.Json
- Moq 4.16.1: https://www.nuget.org/packages/Moq
- NLog 4.7.11: https://www.nuget.org/packages/NLog
- NLog.Web.AspNetCore 4.14.0: https://www.nuget.org/packages/NLog.Web.AspNetCore/

## Third Party APIs
The following APIs has been used to access the pokemon's information:
- https://pokeapi.co/api/v2
- https://funtranslations.com/api/shakespeare
- https://funtranslations.com/api/yoda

## Running Application
On Windows open command prompt, Clone the repository, change your working dirctory to API directory, and run "dotnet run".
Application will be host in the following addresses: 
- http://localhost:5000
- https://localhost:5001

## API Documentation
Pokemon api fully documented using swagger. after running the application, visit the following addresses to take a look at documentation.
- http://localhost:5000/swagger/index.html
- https://localhost:5001/swagger/index.html

## Futuer Implementations
- Writing intergration tests
- Versioning the data transfer objects (dtos)
- Impelementing SDK project for usage of other projects (dtos in domain layer should use in SKD project)