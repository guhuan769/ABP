%Company%
dotnet pack ./ManagementPlatform.Company/src/ManagementPlatform.Company.Domain.Shared/ManagementPlatform.Company.Domain.Shared.csproj
dotnet pack ./ManagementPlatform.Company/src/ManagementPlatform.Company.Application.Contracts/ManagementPlatform.Company.Application.Contracts.csproj
dotnet pack ./ManagementPlatform.Company/src/ManagementPlatform.Company.HttpApi/ManagementPlatform.Company.HttpApi.csproj
dotnet pack ./ManagementPlatform.Company/src/ManagementPlatform.Company.HttpApi.Client/ManagementPlatform.Company.HttpApi.Client.csproj
%Production%
dotnet pack ./ManagementPlatform.Production/src/ManagementPlatform.Production.Domain.Shared/ManagementPlatform.Production.Domain.Shared.csproj
dotnet pack ./ManagementPlatform.Production/src/ManagementPlatform.Production.Application.Contracts/ManagementPlatform.Production.Application.Contracts.csproj
dotnet pack ./ManagementPlatform.Production/src/ManagementPlatform.Production.HttpApi/ManagementPlatform.Production.HttpApi.csproj
dotnet pack ./ManagementPlatform.Production/src/ManagementPlatform.Production.HttpApi.Client/ManagementPlatform.Production.HttpApi.Client.csproj
%Identity%
dotnet pack ./ManagementPlatform.Identity/src/ManagementPlatform.Identity.Domain.Shared/ManagementPlatform.Identity.Domain.Shared.csproj
dotnet pack ./ManagementPlatform.Identity/src/ManagementPlatform.Identity.Application.Contracts/ManagementPlatform.Identity.Application.Contracts.csproj
dotnet pack ./ManagementPlatform.Identity/src/ManagementPlatform.Identity.HttpApi/ManagementPlatform.Identity.HttpApi.csproj
dotnet pack ./ManagementPlatform.Identity/src/ManagementPlatform.Identity.HttpApi.Client/ManagementPlatform.Identity.HttpApi.Client.csproj