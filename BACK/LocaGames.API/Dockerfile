#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LocaGames.API/LocaGames.API.csproj", "LocaGames.API/"]
COPY ["LocaGames.Infra.CrossCutting/LocaGames.Infra.CrossCutting.csproj", "LocaGames.Infra.CrossCutting/"]
COPY ["LocaGames.Services/LocaGames.Service.csproj", "LocaGames.Services/"]
COPY ["LocaGames.Domain/LocaGames.Domain.csproj", "LocaGames.Domain/"]
COPY ["LocaGames.Infra.Data/LocaGames.Infra.Data.csproj", "LocaGames.Infra.Data/"]
COPY ["LocaGames.Infra.Shared/LocaGames.Infra.Shared.csproj", "LocaGames.Infra.Shared/"]
RUN dotnet restore "LocaGames.API/LocaGames.API.csproj"
COPY . .
WORKDIR "/src/LocaGames.API"
RUN dotnet build "LocaGames.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocaGames.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocaGames.API.dll"]