# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set working directory
WORKDIR /app

# Copy published output
COPY ./publish .

# Expose HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

# Set environment variables for HTTPS
ENV ASPNETCORE_URLS="https://+:443;http://+:80"
ENV ASPNETCORE_ENVIRONMENT="Production"

# Run the application
ENTRYPOINT ["dotnet", "SkillHive.Api.dll"]
