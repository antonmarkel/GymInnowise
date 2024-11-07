#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5012

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"

WORKDIR /src
COPY ["services/SectionService/GymInnowise.SectionService.API/GymInnowise.SectionService.API.csproj", "services/SectionService/GymInnowise.SectionService.API/"]
COPY ["services/SectionService/GymInnowise.SectionService.Configuration/GymInnowise.SectionService.Configuration.csproj", "services/SectionService/GymInnowise.SectionService.Configuration/"]
COPY ["services/SectionService/GymInnowise.SectionService.Logic/GymInnowise.SectionService.Logic.csproj", "services/SectionService/GymInnowise.SectionService.Logic/"]
COPY ["services/SectionService/GymInnowise.SectionService.Persistence/GymInnowise.SectionService.Persistence.csproj", "services/SectionService/GymInnowise.SectionService.Persistence/"]
RUN dotnet restore "./services/SectionService/GymInnowise.SectionService.API/GymInnowise.SectionService.API.csproj"
COPY . .
WORKDIR "/src/services/SectionService/GymInnowise.SectionService.API"
RUN dotnet build "./GymInnowise.SectionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.SectionService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.SectionService.API.dll"]