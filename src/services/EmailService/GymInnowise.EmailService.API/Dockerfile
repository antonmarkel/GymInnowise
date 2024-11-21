#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5008
EXPOSE 5009

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"

WORKDIR /src
COPY ["services/EmailService/GymInnowise.EmailService.API/GymInnowise.EmailService.API.csproj", "services/EmailService/GymInnowise.EmailService.API/"]
COPY ["services/EmailService/GymInnowise.EmailService.Configuration/GymInnowise.EmailService.Configuration.csproj", "services/EmailService/GymInnowise.EmailService.Configuration/"]
COPY ["services/EmailService/GymInnowise.EmailService.Logic/GymInnowise.EmailService.Logic.csproj", "services/EmailService/GymInnowise.EmailService.Logic/"]
RUN dotnet restore "./services/EmailService/GymInnowise.EmailService.API/GymInnowise.EmailService.API.csproj"
COPY . .
WORKDIR "/src/services/EmailService/GymInnowise.EmailService.API"
RUN dotnet build "./GymInnowise.EmailService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.EmailService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.EmailService.API.dll"]