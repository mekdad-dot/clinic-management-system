#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClinicManagementSystemApi/ClinicManagementSystemApi.csproj", "ClinicManagementSystemApi/"]
COPY ["ClinicManagementSystem.Application/ClinicManagementSystem.Application.csproj", "ClinicManagementSystem.Application/"]
COPY ["ClinicManagementSystem.Core/ClinicManagementSystem.Core.csproj", "ClinicManagementSystem.Core/"]
COPY ["ClinicManagementSystem.Infrastructure/ClinicManagementSystem.Infrastructure.csproj", "ClinicManagementSystem.Infrastructure/"]
RUN dotnet restore "./ClinicManagementSystemApi/ClinicManagementSystemApi.csproj"
COPY . .
WORKDIR "/src/ClinicManagementSystemApi"
RUN dotnet build "./ClinicManagementSystemApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClinicManagementSystemApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClinicManagementSystemApi.dll"]