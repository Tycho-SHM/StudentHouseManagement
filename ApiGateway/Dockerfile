# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["ApiGateway/SHM.ApiGateway.API/SHM.ApiGateway.API.csproj", "ApiGateway/SHM.ApiGateway.API/"]
COPY ["ApiGateway/*.sln", "ApiGateway/"]

RUN dotnet restore "ApiGateway/SHM.ApiGateway.API/SHM.ApiGateway.API.csproj"

# Copy all source code
COPY ["ApiGateway/", "ApiGateway/"]

# Build and publish
WORKDIR "/src/ApiGateway"
RUN dotnet build "SHM.ApiGateway.API/SHM.ApiGateway.API.csproj" -c Release -o /app/build
RUN dotnet publish "SHM.ApiGateway.API/SHM.ApiGateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SHM.ApiGateway.API.dll"]