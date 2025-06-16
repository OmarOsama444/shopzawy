FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files
COPY . .

# Restore dependencies
RUN dotnet restore "./shopizawy.sln"

# Build and publish
RUN dotnet build "src/shopzawy/shopzawy.csproj" -c Release -o /app/build
RUN dotnet publish "src/shopzawy/shopzawy.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "shopzawy.dll"] 