FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CryptoGateway.API/CryptoGateway.API.csproj", "src/CryptoGateway.API/"]
COPY ["src/CryptoGateway.Infra/CryptoGateway.Infra.csproj", "src/CryptoGateway.Infra/"]
COPY ["src/CryptoGateway.Core/CryptoGateway.Core.csproj", "src/CryptoGateway.Core/"]
COPY ["src/CryptoGateway.Domain/CryptoGateway.Domain.csproj", "src/CryptoGateway.Domain/"]
COPY ["src/CryptoGateway.Application/CryptoGateway.Application.csproj", "src/CryptoGateway.Application/"]
RUN dotnet restore "src/CryptoGateway.API/CryptoGateway.API.csproj"
COPY . .
WORKDIR "/src/CryptoGateway.API"
RUN dotnet build "CryptoGateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoGateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoGateway.API.dll"]
