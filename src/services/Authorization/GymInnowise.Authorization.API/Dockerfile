#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"


WORKDIR /src
COPY ["services/Authorization/GymInnowise.Authorization.API/GymInnowise.Authorization.API.csproj", "services/Authorization/GymInnowise.Authorization.API/"]
COPY ["services/Authorization/GymInnowise.Authorization.Logic/GymInnowise.Authorization.Logic.csproj", "services/Authorization/GymInnowise.Authorization.Logic/"]
COPY ["services/Authorization/GymInnowise.Authorization.Configuration/GymInnowise.Authorization.Configuration.csproj", "services/Authorization/GymInnowise.Authorization.Configuration/"]
COPY ["services/Authorization/GymInnowise.Authorization.Persistence/GymInnowise.Authorization.Persistence.csproj", "services/Authorization/GymInnowise.Authorization.Persistence/"]
RUN dotnet restore "./services/Authorization/GymInnowise.Authorization.API/GymInnowise.Authorization.API.csproj"
COPY . .
WORKDIR "/src/services/Authorization/GymInnowise.Authorization.API"
RUN dotnet build "./GymInnowise.Authorization.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.Authorization.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.Authorization.API.dll"]