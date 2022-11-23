FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . .
RUN dotnet restore WebApiTemplate.WebApi
RUN dotnet publish WebApiTemplate.WebApi -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "/app/WebApiTemplate.WebApi.dll"]