#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"

WORKDIR /src
COPY ["services/UserService/GymInnowise.UserService.API/GymInnowise.UserService.API.csproj", "services/UserService/GymInnowise.UserService.API/"]
COPY ["services/UserService/GymInnowise.UserService.Configuration/GymInnowise.UserService.Configuration.csproj", "services/UserService/GymInnowise.UserService.Configuration/"]
COPY ["services/UserService/GymInnowise.UserService.Logic/GymInnowise.UserService.Logic.csproj", "services/UserService/GymInnowise.UserService.Logic/"]
COPY ["services/UserService/GymInnowise.UserService.Persistence/GymInnowise.UserService.Persistence.csproj", "services/UserService/GymInnowise.UserService.Persistence/"]
RUN dotnet restore "./services/UserService/GymInnowise.UserService.API/GymInnowise.UserService.API.csproj"
COPY . .
WORKDIR "/src/services/UserService/GymInnowise.UserService.API"
RUN dotnet build "./GymInnowise.UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.UserService.API.dll"]