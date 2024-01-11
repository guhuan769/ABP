%Customer%
dotnet pack ./MicroClassroom.Customer/src/MicroClassroom.Customer.Domain.Shared/MicroClassroom.Customer.Domain.Shared.csproj
dotnet pack ./MicroClassroom.Customer/src/MicroClassroom.Customer.Application.Contracts/MicroClassroom.Customer.Application.Contracts.csproj
dotnet pack ./MicroClassroom.Customer/src/MicroClassroom.Customer.HttpApi/MicroClassroom.Customer.HttpApi.csproj
dotnet pack ./MicroClassroom.Customer/src/MicroClassroom.Customer.HttpApi.Client/MicroClassroom.Customer.HttpApi.Client.csproj
%Enterprise%
dotnet pack ./MicroClassroom.Enterprise/src/MicroClassroom.Enterprise.Domain.Shared/MicroClassroom.Enterprise.Domain.Shared.csproj
dotnet pack ./MicroClassroom.Enterprise/src/MicroClassroom.Enterprise.Application.Contracts/MicroClassroom.Enterprise.Application.Contracts.csproj
dotnet pack ./MicroClassroom.Enterprise/src/MicroClassroom.Enterprise.HttpApi/MicroClassroom.Enterprise.HttpApi.csproj
dotnet pack ./MicroClassroom.Enterprise/src/MicroClassroom.Enterprise.HttpApi.Client/MicroClassroom.Enterprise.HttpApi.Client.csproj
%Identity%
dotnet pack ./MicroClassroom.Identity/src/MicroClassroom.Identity.Domain.Shared/MicroClassroom.Identity.Domain.Shared.csproj
dotnet pack ./MicroClassroom.Identity/src/MicroClassroom.Identity.Application.Contracts/MicroClassroom.Identity.Application.Contracts.csproj
dotnet pack ./MicroClassroom.Identity/src/MicroClassroom.Identity.HttpApi/MicroClassroom.Identity.HttpApi.csproj
dotnet pack ./MicroClassroom.Identity/src/MicroClassroom.Identity.HttpApi.Client/MicroClassroom.Identity.HttpApi.Client.csproj
%Shared%
dotnet pack ./MicroClassroom.MicroService/MicroClassroom.Shared/MicroClassroom.Identity.Domain.Shared.csproj