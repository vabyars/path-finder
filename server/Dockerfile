FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Release
ENV PROGRAM_ENVIRONMENT=Deploy

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY "PathFinder.sln" "PathFinder.sln"


COPY "PathFinder.Api/PathFinder.Api.csproj" "PathFinder.Api/PathFinder.Api.csproj"
COPY "PathFinder.DataAccess/PathFinder.DataAccess.csproj" "PathFinder.DataAccess/PathFinder.DataAccess.csproj"
COPY "PathFinder.Domain/PathFinder.Domain.csproj" "PathFinder.Domain/PathFinder.Domain.csproj"
COPY "PathFinder.Infrastructure/PathFinder.Infrastructure.csproj" "PathFinder.Infrastructure/PathFinder.Infrastructure.csproj"
COPY "PathFinder.Test/PathFinder.Test.csproj" "PathFinder.Test/PathFinder.Test.csproj"

COPY "NuGet.Config" "NuGet.Config"

RUN dotnet restore "PathFinder.sln"

COPY . .
WORKDIR "/src/PathFinder.Api"
RUN dotnet publish -c Release -o /app

FROM build as dataAccess
WORKDIR /src/PathFinder.DataAccess

FROM build as domain
WORKDIR /src/PathFinder.Domain

FROM build as infrastructure
WORKDIR /src/PathFinder.Infrastructure

FROM build as tests
WORKDIR /src/PathFinder.Test

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY "ClientApp/build" "ClientApp/build"
ENV ASPNETCORE_URLS=http://*:$PORT
ENV DATABASE_URL=$DATABASE_URL
ENTRYPOINT ["dotnet", "PathFinder.Api.dll"]