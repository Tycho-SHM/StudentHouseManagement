FROM mcr.microsoft.com/playwright/dotnet:v1.52.0-noble

# Set working directory
WORKDIR /app

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
COPY ["e2eTesting/SHM.E2ETest/SHM.E2ETest.csproj", "e2eTesting/SHM.E2ETest/"]
COPY ["e2eTesting/*.sln", "e2eTesting/"]

# Copy all source code
COPY ["e2eTesting/", "e2eTesting/"]

# Build the project
RUN dotnet build "e2eTesting/SHM.E2ETest/SHM.E2ETest.csproj" -c Release

WORKDIR /app/e2eTesting/SHM.E2ETest
# Set the entry point to run tests
ENTRYPOINT ["dotnet", "test", "--configuration", "Release", "--no-build", "--logger", "trx;LogFileName=test-results.trx"]
