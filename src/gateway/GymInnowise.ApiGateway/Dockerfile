#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["gateway/GymInnowise.ApiGateway/GymInnowise.ApiGateway.csproj", "gateway/GymInnowise.ApiGateway/"]
RUN dotnet restore "./gateway/GymInnowise.ApiGateway/GymInnowise.ApiGateway.csproj"
COPY . .
WORKDIR "/src/gateway/GymInnowise.ApiGateway"
RUN dotnet build "./GymInnowise.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GymInnowise.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymInnowise.ApiGateway.dll"]