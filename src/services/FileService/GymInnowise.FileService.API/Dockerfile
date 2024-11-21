#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"

WORKDIR /src
COPY ["services/FileService/GymInnowise.FileService.API/GymInnowise.FileService.API.csproj", "services/FileService/GymInnowise.FileService.API/"]
COPY ["services/FileService/GymInnowise.FileService.Logic/GymInnowise.FileService.Logic.csproj", "services/FileService/GymInnowise.FileService.Logic/"]
COPY ["services/FileService/GymInnowise.FileService.Persistence/GymInnowise.FileService.Persistence.csproj", "services/FileService/GymInnowise.FileService.Persistence/"]
COPY ["services/FileService/GymInnowise.FileService.Configuration/GymInnowise.FileService.Configuration.csproj", "services/FileService/GymInnowise.FileService.Configuration/"]
RUN dotnet restore "./services/FileService/GymInnowise.FileService.API/GymInnowise.FileService.API.csproj"
COPY . .
WORKDIR "/src/services/FileService/GymInnowise.FileService.API"
RUN dotnet build "./GymInnowise.FileService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.FileService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.FileService.API.dll"]