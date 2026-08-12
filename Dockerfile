FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Restore dependencies first for caching
COPY *.csproj ./
RUN dotnet restore

# Build/publish the API
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime image for .NET 10 ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ColourApi_net10.dll"]