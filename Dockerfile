FROM mcr.microsoft.com/dotnet/aspnet:9.0.2 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0.200 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY  ["MainApi/MainApi.csproj", "MainApi/"]
COPY  ["MainApi.Application/MainApi.Application.csproj", "MainApi.Application/"]
COPY  ["MainApi.Domain/MainApi.Domain.csproj", "MainApi.Domain/"]
COPY  ["MainApi.Infrastructure/MainApi.Infrastructure.csproj", "MainApi.Infrastructure/"]
COPY  ["MainApi.Persistence/MainApi.Persistence.csproj", "MainApi.Persistence/"]
RUN dotnet restore "MainApi/MainApi.csproj"
COPY . .

WORKDIR "/src/MainApi"
RUN dotnet build "MainApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MainApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainApi.dll"]