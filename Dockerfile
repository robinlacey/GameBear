FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./GameBear/*.csproj ./GameBear/
RUN dotnet restore ./GameBear

# Copy everything else and build
COPY ./GameBear/. ./GameBear/
RUN dotnet publish ./GameBear -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/GameBear/out .
ENTRYPOINT ["dotnet", "GameBear.dll"]