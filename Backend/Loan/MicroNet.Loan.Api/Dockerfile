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
COPY ["Backend/Loan/MicroNet.Loan.Api/MicroNet.Loan.Api.csproj", "Backend/Loan/MicroNet.Loan.Api/"]
COPY ["Shared/MicroNet.Shared/MicroNet.Shared.csproj", "Shared/MicroNet.Shared/"]
COPY ["Backend/Loan/MicroNet.Loan.Application/MicroNet.Loan.Application.csproj", "Backend/Loan/MicroNet.Loan.Application/"]
COPY ["Backend/Loan/MicroNet.Loan.Core/MicroNet.Loan.Core.csproj", "Backend/Loan/MicroNet.Loan.Core/"]
COPY ["Backend/Loan/MicroNet.Loan.Infrastructure/MicroNet.Loan.Infrastructure.csproj", "Backend/Loan/MicroNet.Loan.Infrastructure/"]
RUN dotnet restore "./Backend/Loan/MicroNet.Loan.Api/MicroNet.Loan.Api.csproj"
COPY . .
WORKDIR "/src/Backend/Loan/MicroNet.Loan.Api"
RUN dotnet build "./MicroNet.Loan.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroNet.Loan.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroNet.Loan.Api.dll"]