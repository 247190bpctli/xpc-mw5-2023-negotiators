# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /source
COPY . .
RUN dotnet restore "./eshopBackend.API/eshopBackend.API.csproj" --disable-parallel
RUN dotnet publish "./eshopBackend.API/eshopBackend.API.csproj" -c Release -o /app --no-restore

# Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "eshopBackend.API.dll"]
