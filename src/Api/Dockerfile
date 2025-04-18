FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/shopizawy.Api.csproj", "API/shopizawy.Api/"]
COPY ["Modules/Common/Modules.Common.Application/Modules.Common.Application.csproj", "Modules/Common/Modules.Common.Application/"]
COPY ["Modules/Common/Modules.Common.Domain/Modules.Common.Domain.csproj", "Modules/Common/Modules.Common.Domain/"]
COPY ["Modules/Common/Modules.Common.Infrastructure/Modules.Common.Infrastructure.csproj", "Modules/Common/Modules.Common.Infrastructure/"]
COPY ["Modules/Common/Modules.Common.Presentation/Modules.Common.Presentation.csproj", "Modules/Common/Modules.Common.Presentation/"]

COPY ["Modules/Orders/Modules.Orders.Application/Modules.Orders.Application.csproj", "Modules/Orders/Modules.Orders.Application/"]
COPY ["Modules/Orders/Modules.Orders.Domain/Modules.Orders.Domain.csproj", "Modules/Orders/Modules.Orders.Domain/"]
COPY ["Modules/Orders/Modules.Orders.Infrastructure/Modules.Orders.Infrastructure.csproj", "Modules/Orders/Modules.Orders.Infrastructure/"]
COPY ["Modules/Orders/Modules.Orders.Presentation/Modules.Orders.Presentation.csproj", "Modules/Orders/Modules.Orders.Presentation/"]

COPY ["Modules/Users/Modules.Users.Application/Modules.Users.Application.csproj", "Modules/Users/Modules.Users.Application/"]
COPY ["Modules/Users/Modules.Users.Domain/Modules.Users.Domain.csproj", "Modules/Users/Modules.Users.Domain/"]
COPY ["Modules/Users/Modules.Users.Infrastructure/Modules.Users.Infrastructure.csproj", "Modules/Users/Modules.Users.Infrastructure/"]
COPY ["Modules/Users/Modules.Users.Presentation/Modules.Users.Presentation.csproj", "Modules/Users/Modules.Users.Presentation/"]
COPY ["Modules/Users/Modules.Users.IntegrationEvents/Modules.Users.IntegrationEvents.csproj", "Modules/Users/Modules.Users.IntegrationEvents/"]

RUN dotnet restore "./API/shopizawy.Api/shopizawy.Api.csproj"
COPY . .
WORKDIR "/src/API/shopizawy.Api"
RUN dotnet build "./shopizawy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./shopizawy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shopizawy.Api.dll"]