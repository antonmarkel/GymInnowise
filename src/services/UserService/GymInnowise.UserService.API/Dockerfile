#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GymInnowise.UserService.API/GymInnowise.UserService.API.csproj", "GymInnowise.UserService.API/"]
COPY ["GymInnowise.UserService.Configuration/GymInnowise.UserService.Configuration.csproj", "GymInnowise.UserService.Configuration/"]
COPY ["GymInnowise.UserService.Logic/GymInnowise.UserService.Logic.csproj", "GymInnowise.UserService.Logic/"]
COPY ["GymInnowise.UserService.Persistence/GymInnowise.UserService.Persistence.csproj", "GymInnowise.UserService.Persistence/"]
COPY ["GymInnowise.UserService.Shared/GymInnowise.UserService.Shared.csproj", "GymInnowise.UserService.Shared/"]
RUN dotnet restore "./GymInnowise.UserService.API/GymInnowise.UserService.API.csproj"
COPY . .
WORKDIR "/src/GymInnowise.UserService.API"
RUN dotnet build "./GymInnowise.UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.UserService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.UserService.API.dll"]