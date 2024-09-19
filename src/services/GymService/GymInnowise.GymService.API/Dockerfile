#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5004
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GymInnowise.GymService.API/GymInnowise.GymService.API.csproj", "GymInnowise.GymService.API/"]
COPY ["GymInnowise.GymService.Configuration/GymInnowise.GymService.Configuration.csproj", "GymInnowise.GymService.Configuration/"]
COPY ["GymInnowise.GymService.Logic/GymInnowise.GymService.Logic.csproj", "GymInnowise.GymService.Logic/"]
COPY ["GymInnowise.GymService.Persistence/GymInnowise.GymService.Persistence.csproj", "GymInnowise.GymService.Persistence/"]
COPY ["GymInnowise.GymService.Shared/GymInnowise.GymService.Shared.csproj", "GymInnowise.GymService.Shared/"]
RUN dotnet restore "./GymInnowise.GymService.API/GymInnowise.GymService.API.csproj"
COPY . .
WORKDIR "/src/GymInnowise.GymService.API"
RUN dotnet build "./GymInnowise.GymService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.GymService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.GymService.API.dll"]