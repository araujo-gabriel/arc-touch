FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ArchTouch.Movies.API/ArchTouch.Movies.API.csproj", "ArchTouch.Movies.API/"]
RUN dotnet restore "ArchTouch.Movies.API/ArchTouch.Movies.API.csproj"
COPY . .
WORKDIR "/src/ArchTouch.Movies.API"
RUN dotnet build "ArchTouch.Movies.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchTouch.Movies.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchTouch.Movies.API.dll"]