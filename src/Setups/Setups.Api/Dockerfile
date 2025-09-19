# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Setups/Setups.Api/Setups.Api.csproj", "src/Setups/Setups.Api/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Setups/Setups.Application/Setups.Application.csproj", "src/Setups/Setups.Application/"]
COPY ["src/Setups/Setups.Domain/Setups.Domain.csproj", "src/Setups/Setups.Domain/"]
COPY ["src/Setups/Setups.Infrastructure/Setups.Infrastructure.csproj", "src/Setups/Setups.Infrastructure/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]
RUN dotnet restore "./src/Setups/Setups.Api/Setups.Api.csproj"
COPY . .
WORKDIR "/src/src/Setups/Setups.Api"
RUN dotnet build "./Setups.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Setups.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Setups.Api.dll"]