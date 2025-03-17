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
COPY ["NotificationService/SHM.NotificationService/SHM.NotificationService.csproj", "NotificationService/SHM.NotificationService/"]
COPY ["NotificationService/SHM.NotificationService.API/SHM.NotificationService.API.csproj", "NotificationService/SHM.NotificationService.API/"]
COPY ["NotificationService/*.sln", "NotificationService/"]

RUN dotnet restore "NotificationService/SHM.NotificationService.API/SHM.NotificationService.API.csproj"

# Copy all source code
COPY ["NotificationService/", "NotificationService/"]

# Build and publish
WORKDIR "/src/NotificationService"
RUN dotnet build "SHM.NotificationService.API/SHM.NotificationService.API.csproj" -c Release -o /app/build
RUN dotnet publish "SHM.NotificationService.API/SHM.NotificationService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SHM.NotificationService.API.dll"]