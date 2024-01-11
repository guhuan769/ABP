#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroClassroom.PublicApp.Host/MicroClassroom.PublicApp.Host.csproj", "MicroClassroom.PublicApp.Host/"]
RUN dotnet restore "MicroClassroom.PublicApp.Host/MicroClassroom.PublicApp.Host.csproj"
COPY . .
WORKDIR "/src/MicroClassroom.PublicApp.Host"
RUN dotnet build "MicroClassroom.PublicApp.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroClassroom.PublicApp.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroClassroom.PublicApp.Host.dll"]