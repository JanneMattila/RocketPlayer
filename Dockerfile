# This Dockerfile contains Build and Release steps:
# 1. Build image
FROM microsoft/dotnet:2.1.403-sdk-alpine3.7 AS build
WORKDIR /source

# Cache nuget restore
COPY /src/Rocket.Player/*.csproj Rocket.Player/
RUN dotnet restore Rocket.Player/Rocket.Player.csproj

# Copy sources and compile
COPY /src .
WORKDIR /source/Rocket.Player
RUN dotnet publish Rocket.Player.csproj --output /app/ --configuration Release

# 2. Release image
FROM microsoft/dotnet:2.1.5-aspnetcore-runtime-alpine3.7
WORKDIR /app

# Copy content from Build image
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Rocket.Player.dll"]
