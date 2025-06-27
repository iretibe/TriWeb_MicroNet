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
COPY ["Backend/Account/MicroNet.Account.Api/MicroNet.Account.Api.csproj", "Backend/Account/MicroNet.Account.Api/"]
COPY ["Backend/Account/MicroNet.Account.Application/MicroNet.Account.Application.csproj", "Backend/Account/MicroNet.Account.Application/"]
COPY ["Shared/MicroNet.Shared/MicroNet.Shared.csproj", "Shared/MicroNet.Shared/"]
COPY ["Backend/Account/MicroNet.Account.Core/MicroNet.Account.Core.csproj", "Backend/Account/MicroNet.Account.Core/"]
COPY ["Backend/Account/MicroNet.Account.Infrastructure/MicroNet.Account.Infrastructure.csproj", "Backend/Account/MicroNet.Account.Infrastructure/"]
RUN dotnet restore "./Backend/Account/MicroNet.Account.Api/MicroNet.Account.Api.csproj"
COPY . .
WORKDIR "/src/Backend/Account/MicroNet.Account.Api"
RUN dotnet build "./MicroNet.Account.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroNet.Account.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroNet.Account.Api.dll"]