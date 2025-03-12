# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Setup GitHub Packages NuGet source
ARG GITHUB_USERNAME
ARG GITHUB_TOKEN
ARG GITHUB_OWNER

RUN dotnet nuget add source https://nuget.pkg.github.com/${GITHUB_OWNER}/index.json \
    --name github \
    --username ${GITHUB_USERNAME} \
    --password "${GITHUB_TOKEN}" \
    --store-password-in-clear-text

# Copy csproj files and restore dependencies
COPY ["TaskService/SHM.TaskService.Model/SHM.TaskService.Model.csproj", "TaskService/SHM.TaskService.Model/"]
COPY ["TaskService/SHM.TaskService.Abstractions/SHM.TaskService.Abstractions.csproj", "TaskService/SHM.TaskService.Abstractions/"]
COPY ["TaskService/SHM.TaskService.API/SHM.TaskService.API.csproj", "TaskService/SHM.TaskService.API/"]
COPY ["TaskService/*.sln", "TaskService/"]

RUN dotnet restore "TaskService/SHM.TaskService.API/SHM.TaskService.API.csproj"

# Copy all source code
COPY ["TaskService/", "TaskService/"]

# Build and publish
WORKDIR "/src/TaskService"
RUN dotnet build "SHM.TaskService.API/SHM.TaskService.API.csproj" -c Release -o /app/build
RUN dotnet publish "SHM.TaskService.API/SHM.TaskService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SHM.TaskService.API.dll"]