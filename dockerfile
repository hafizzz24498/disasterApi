FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY disasterApi.API/disasterApi.API.csproj disasterApi.API/
COPY disasterApi.Core/disasterApi.Core.csproj disasterApi.Core/
COPY disasterApi.Domain/disasterApi.Domain.csproj disasterApi.Domain/
COPY disasterApi.Infra/disasterApi.Infra.csproj disasterApi.Infra/
RUN dotnet restore disasterApi.API/disasterApi.API.csproj

COPY . .
WORKDIR /src/disasterApi.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "disasterApi.API.dll"]