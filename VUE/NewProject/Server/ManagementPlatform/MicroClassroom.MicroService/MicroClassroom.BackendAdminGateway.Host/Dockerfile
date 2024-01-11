#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroClassroom.BackendAdminGateway.Host/MicroClassroom.BackendAdminGateway.Host.csproj", "MicroClassroom.BackendAdminGateway.Host/"]
RUN dotnet restore "MicroClassroom.BackendAdminGateway.Host/MicroClassroom.BackendAdminGateway.Host.csproj"
COPY . .
WORKDIR "/src/MicroClassroom.BackendAdminGateway.Host"
RUN dotnet build "MicroClassroom.BackendAdminGateway.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroClassroom.BackendAdminGateway.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroClassroom.BackendAdminGateway.Host.dll"]