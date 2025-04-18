FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY ["publish/" , "publish/"]

WORKDIR /app/publish

# WORKDIR /src
# COPY ["src/shopzawy/shopzawy.csproj", "Api/"]
# COPY ["src/Modules/Common/Modules.Common.Application/Modules.Common.Application.csproj", "Modules/Common/Modules.Common.Application/"]
# COPY ["src/Modules/Common/Modules.Common.Domain/Modules.Common.Domain.csproj", "Modules/Common/Modules.Common.Domain/"]
# COPY ["src/Modules/Common/Modules.Common.Infrastructure/Modules.Common.Infrastructure.csproj", "Modules/Common/Modules.Common.Infrastructure/"]
# COPY ["src/Modules/Common/Modules.Common.Presentation/Modules.Common.Presentation.csproj", "Modules/Common/Modules.Common.Presentation/"]

# COPY ["src/Modules/Orders/Modules.Orders.Application/Modules.Orders.Application.csproj", "Modules/Orders/Modules.Orders.Application/"]
# COPY ["src/Modules/Orders/Modules.Orders.Domain/Modules.Orders.Domain.csproj", "Modules/Orders/Modules.Orders.Domain/"]
# COPY ["src/Modules/Orders/Modules.Orders.Infrastructure/Modules.Orders.Infrastructure.csproj", "Modules/Orders/Modules.Orders.Infrastructure/"]
# COPY ["src/Modules/Orders/Modules.Orders.Presentation/Modules.Orders.Presentation.csproj", "Modules/Orders/Modules.Orders.Presentation/"]

# COPY ["src/Modules/Users/Modules.Users.Application/Modules.Users.Application.csproj", "Modules/Users/Modules.Users.Application/"]
# COPY ["src/Modules/Users/Modules.Users.Domain/Modules.Users.Domain.csproj", "Modules/Users/Modules.Users.Domain/"]
# COPY ["src/Modules/Users/Modules.Users.Infrastructure/Modules.Users.Infrastructure.csproj", "Modules/Users/Modules.Users.Infrastructure/"]
# COPY ["src/Modules/Users/Modules.Users.Presentation/Modules.Users.Presentation.csproj", "Modules/Users/Modules.Users.Presentation/"]

# RUN dotnet restore "./Api/shopzawy.csproj"
# COPY . .
# WORKDIR "/src/Api"
# RUN dotnet build "./shopzawy.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "./shopzawy.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillHive.Api.dll"]