#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/APIGateway/ESourcing.APIGateway/ESourcing.APIGateway.csproj", "Services/APIGateway/ESourcing.APIGateway/"]
RUN dotnet restore "./Services/APIGateway/ESourcing.APIGateway/ESourcing.APIGateway.csproj"
COPY . .
WORKDIR "/src/Services/APIGateway/ESourcing.APIGateway"
RUN dotnet build "./ESourcing.APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ESourcing.APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.APIGateway.dll"]