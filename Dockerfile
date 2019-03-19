# This Dockerfile contains Build and Release steps:
# 1. Build image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2.104-alpine3.8 AS build
WORKDIR /source

# Cache nuget restore
COPY /src/Rocket.Player/*.csproj Rocket.Player/
RUN dotnet restore Rocket.Player/Rocket.Player.csproj

# Copy sources and compile
COPY /src .
WORKDIR /source/Rocket.Player
RUN dotnet publish Rocket.Player.csproj --output /app/ --configuration Release

# 2. Release image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.2-alpine3.8
WORKDIR /app

# Copy content from Build image
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Rocket.Player.dll"]
