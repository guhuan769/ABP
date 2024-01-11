#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ManagementPlatform.MicroService/ManagementPlatform.PublicGateway.Host/ManagementPlatform.PublicGateway.Host.csproj", "ManagementPlatform.MicroService/ManagementPlatform.PublicGateway.Host/"]
RUN dotnet restore "./ManagementPlatform.MicroService/ManagementPlatform.PublicGateway.Host/./ManagementPlatform.PublicGateway.Host.csproj"
COPY . .
WORKDIR "/src/ManagementPlatform.MicroService/ManagementPlatform.PublicGateway.Host"
RUN dotnet build "./ManagementPlatform.PublicGateway.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ManagementPlatform.PublicGateway.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManagementPlatform.PublicGateway.Host.dll"]