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
COPY ["Backend/Sundry/MicroNet.Sundry.Api/MicroNet.Sundry.Api.csproj", "Backend/Sundry/MicroNet.Sundry.Api/"]
COPY ["Backend/Sundry/MicroNet.Sundry.Application/MicroNet.Sundry.Application.csproj", "Backend/Sundry/MicroNet.Sundry.Application/"]
COPY ["Shared/MicroNet.Shared/MicroNet.Shared.csproj", "Shared/MicroNet.Shared/"]
COPY ["Backend/Sundry/MicroNet.Sundry.Core/MicroNet.Sundry.Core.csproj", "Backend/Sundry/MicroNet.Sundry.Core/"]
COPY ["Backend/Sundry/MicroNet.Sundry.Infrastructure/MicroNet.Sundry.Infrastructure.csproj", "Backend/Sundry/MicroNet.Sundry.Infrastructure/"]
RUN dotnet restore "./Backend/Sundry/MicroNet.Sundry.Api/MicroNet.Sundry.Api.csproj"
COPY . .
WORKDIR "/src/Backend/Sundry/MicroNet.Sundry.Api"
RUN dotnet build "./MicroNet.Sundry.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroNet.Sundry.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroNet.Sundry.Api.dll"]