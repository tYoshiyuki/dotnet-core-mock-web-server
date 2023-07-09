FROM mcr.microsoft.com/dotnet/aspnet:6.0.1-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0.101-bullseye-slim AS build
WORKDIR /src
COPY ["DotNetCoreMockWebServer.csproj", "./"]
RUN dotnet restore "./DotNetCoreMockWebServer.csproj"
COPY . ./
RUN dotnet build "DotNetCoreMockWebServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DotNetCoreMockWebServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS http://*:80;https://*:443
ENTRYPOINT ["dotnet", "DotNetCoreMockWebServer.dll"]