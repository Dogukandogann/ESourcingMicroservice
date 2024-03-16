#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Order/ESourcing.Order/ESourcing.Order.csproj", "Services/Order/ESourcing.Order/"]
COPY ["Services/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "Services/Common/EventBusRabbitMQ/"]
COPY ["Services/Order/Ordering.Application/Ordering.Application.csproj", "Services/Order/Ordering.Application/"]
COPY ["Services/Order/Ordering.Domain/Ordering.Domain.csproj", "Services/Order/Ordering.Domain/"]
COPY ["Services/Order/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Order/Ordering.Infrastructure/"]
RUN dotnet restore "./Services/Order/ESourcing.Order/ESourcing.Order.csproj"
COPY . .
WORKDIR "/src/Services/Order/ESourcing.Order"
RUN dotnet build "./ESourcing.Order.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ESourcing.Order.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.Order.dll"]