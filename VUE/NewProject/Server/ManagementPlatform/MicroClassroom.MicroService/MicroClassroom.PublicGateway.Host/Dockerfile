#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroClassroom.PublicGateway.Host/MicroClassroom.PublicGateway.Host.csproj", "MicroClassroom.PublicGateway.Host/"]
RUN dotnet restore "MicroClassroom.PublicGateway.Host/MicroClassroom.PublicGateway.Host.csproj"
COPY . .
WORKDIR "/src/MicroClassroom.PublicGateway.Host"
RUN dotnet build "MicroClassroom.PublicGateway.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroClassroom.PublicGateway.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroClassroom.PublicGateway.Host.dll"]