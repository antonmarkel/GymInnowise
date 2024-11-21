#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"

WORKDIR /src
COPY ["services/GymService/GymInnowise.GymService.API/GymInnowise.GymService.API.csproj", "services/GymService/GymInnowise.GymService.API/"]
COPY ["services/GymService/GymInnowise.GymService.Configuration/GymInnowise.GymService.Configuration.csproj", "services/GymService/GymInnowise.GymService.Configuration/"]
COPY ["services/GymService/GymInnowise.GymService.Logic/GymInnowise.GymService.Logic.csproj", "services/GymService/GymInnowise.GymService.Logic/"]
COPY ["services/GymService/GymInnowise.GymService.Persistence/GymInnowise.GymService.Persistence.csproj", "services/GymService/GymInnowise.GymService.Persistence/"]
RUN dotnet restore "./services/GymService/GymInnowise.GymService.API/GymInnowise.GymService.API.csproj"
COPY . .
WORKDIR "/src/services/GymService/GymInnowise.GymService.API"
RUN dotnet build "./GymInnowise.GymService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.GymService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.GymService.API.dll"]