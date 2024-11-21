#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /shared
COPY ["shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj", "shared/GymInnowise.Shared/GymInnowise.Shared/"]
RUN dotnet restore "shared/GymInnowise.Shared/GymInnowise.Shared/GymInnowise.Shared.csproj"

WORKDIR /src
COPY ["services/ReportService/GymInnowise.ReportService.API/GymInnowise.ReportService.API.csproj", "services/ReportService/GymInnowise.ReportService.API/"]
COPY ["services/ReportService/GymInnowise.ReportService.Configuration/GymInnowise.ReportService.Configuration.csproj", "services/ReportService/GymInnowise.ReportService.Configuration/"]
COPY ["services/ReportService/GymInnowise.ReportService.Logic/GymInnowise.ReportService.Logic.csproj", "services/ReportService/GymInnowise.ReportService.Logic/"]
COPY ["services/ReportService/GymInnowise.ReportService.Perstistence/GymInnowise.ReportService.Perstistence.csproj", "services/ReportService/GymInnowise.ReportService.Perstistence/"]
RUN dotnet restore "./services/ReportService/GymInnowise.ReportService.API/GymInnowise.ReportService.API.csproj"
COPY . .
WORKDIR "/src/services/ReportService/GymInnowise.ReportService.API"
RUN dotnet build "./GymInnowise.ReportService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.ReportService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.ReportService.API.dll"]