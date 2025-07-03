#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Identity/Lamba.Identity.Api/Lamba.Identity.Api.csproj", "src/Identity/Lamba.Identity.Api/"]
COPY ["src/Lamba.Common/Lamba.Common.csproj", "src/Lamba.Common/"]
COPY ["src/Lamba.Security/Lamba.Security.csproj", "src/Lamba.Security/"]
COPY ["src/Identity/Lamba.Identity.Application/Lamba.Identity.Application.csproj", "src/Identity/Lamba.Identity.Application/"]
COPY ["src/Lamba.Repository/Lamba.Repository.csproj", "src/Lamba.Repository/"]
COPY ["src/Lamba.Domain/Lamba.Domain.csproj", "src/Lamba.Domain/"]
COPY ["src/Lamba.Infrastructure/Lamba.Infrastructure.csproj", "src/Lamba.Infrastructure/"]
COPY ["src/Identity/Lamba.Identity.Domain/Lamba.Identity.Domain.csproj", "src/Identity/Lamba.Identity.Domain/"]
COPY ["src/Identity/Lamba.Identity.Infrastructure/Lamba.Identity.Infrastructure.csproj", "src/Identity/Lamba.Identity.Infrastructure/"]
RUN dotnet restore "./src/Identity/Lamba.Identity.Api/Lamba.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Identity/Lamba.Identity.Api"
RUN dotnet build "./Lamba.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lamba.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lamba.Identity.Api.dll"]