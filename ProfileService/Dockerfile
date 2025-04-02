# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Setup GitHub Packages NuGet source
ARG GITHUB_USERNAME
ARG GITHUB_TOKEN
ARG GITHUB_OWNER

# Configure NuGet to use GitHub Packages
RUN dotnet nuget add source https://nuget.pkg.github.com/${GITHUB_OWNER}/index.json \
    --name github \
    --username ${GITHUB_USERNAME} \
    --password "${GITHUB_TOKEN}" \
    --store-password-in-clear-text

# Copy csproj files and restore dependencies
COPY ["ProfileService/SHM.ProfileService/SHM.ProfileService.csproj", "ProfileService/SHM.ProfileService/"]
COPY ["ProfileService/SHM.ProfileService.API/SHM.ProfileService.API.csproj", "ProfileService/SHM.ProfileService.API/"]
COPY ["ProfileService/SHM.ProfileService.Abstractions/SHM.ProfileService.Abstractions.csproj", "ProfileService/SHM.ProfileService.Abstractions/"]
COPY ["ProfileService/SHM.ProfileService.Model/SHM.ProfileService.Model.csproj", "ProfileService/SHM.ProfileService.Model/"]
COPY ["ProfileService/SHM.ProfileService.MongoDb/SHM.ProfileService.MongoDb.csproj", "ProfileService/SHM.ProfileService.MongoDb/"]
COPY ["ProfileService/*.sln", "ProfileService/"]

RUN dotnet restore "ProfileService/SHM.ProfileService.API/SHM.ProfileService.API.csproj"

# Copy all source code
COPY ["ProfileService/", "ProfileService/"]

# Build and publish
WORKDIR "/src/ProfileService"
RUN dotnet build "SHM.ProfileService.API/SHM.ProfileService.API.csproj" -c Release -o /app/build
RUN dotnet publish "SHM.ProfileService.API/SHM.ProfileService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SHM.ProfileService.API.dll"]