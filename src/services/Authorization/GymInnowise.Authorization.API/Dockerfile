#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GymInnowise.Authorization.API/GymInnowise.Authorization.API.csproj", "GymInnowise.Authorization.API/"]
COPY ["GymInnowise.Authorization.Logic/GymInnowise.Authorization.Logic.csproj", "GymInnowise.Authorization.Logic/"]
COPY ["GymInnowise.Authorization.Configuration/GymInnowise.Authorization.Configuration.csproj", "GymInnowise.Authorization.Configuration/"]
COPY ["GymInnowise.Authorization.Persistence/GymInnowise.Authorization.Persistence.csproj", "GymInnowise.Authorization.Persistence/"]
COPY ["GymInnowise.Authorization.Shared/GymInnowise.Authorization.Shared.csproj", "GymInnowise.Authorization.Shared/"]
RUN dotnet restore "./GymInnowise.Authorization.API/GymInnowise.Authorization.API.csproj"
COPY . .
WORKDIR "/src/GymInnowise.Authorization.API"
RUN dotnet build "./GymInnowise.Authorization.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.Authorization.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.Authorization.API.dll"]