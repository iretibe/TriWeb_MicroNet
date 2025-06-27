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
COPY ["Backend/Client/MicroNet.Client.Api/MicroNet.Client.Api.csproj", "Backend/Client/MicroNet.Client.Api/"]
COPY ["Backend/Client/MicroNet.Client.Application/MicroNet.Client.Application.csproj", "Backend/Client/MicroNet.Client.Application/"]
COPY ["Shared/MicroNet.Shared/MicroNet.Shared.csproj", "Shared/MicroNet.Shared/"]
COPY ["Backend/Client/MicroNet.Client.Core/MicroNet.Client.Core.csproj", "Backend/Client/MicroNet.Client.Core/"]
COPY ["Backend/Client/MicroNet.Client.Infrastructure/MicroNet.Client.Infrastructure.csproj", "Backend/Client/MicroNet.Client.Infrastructure/"]
RUN dotnet restore "./Backend/Client/MicroNet.Client.Api/MicroNet.Client.Api.csproj"
COPY . .
WORKDIR "/src/Backend/Client/MicroNet.Client.Api"
RUN dotnet build "./MicroNet.Client.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroNet.Client.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroNet.Client.Api.dll"]