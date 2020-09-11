#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["src/API/Rumox.API.csproj", "src/API/"]

COPY ["src/Core/Core.Cache/Core.Cache.csproj", "src/Core/Core.Cache/"]
COPY ["src/Core/Core.Domain/Core.Domain.csproj", "src/Core/Core.Domain/"]
COPY ["src/Core/Core.Identity/Core.Identity.csproj", "src/Core/Core.Identity/"]
COPY ["src/Core/Core.Infra.CrossCutting.Identity/Core.Infra.CrossCutting.Identity.csproj", "src/Core/Core.Infra.CrossCutting.Identity/"]
COPY ["src/Core/Core.Infra.Log.ELK/Core.Infra.Log.ELK.csproj", "src/Core/Core.Infra.Log.ELK/"]
COPY ["src/Core/Core.Infra.Mongo/Core.Infra.Mongo.csproj", "src/Core/Core.Infra.Mongo/"]
COPY ["src/Core/Core.Infra.MySQL/Core.Infra.MySQL.csproj", "src/Core/Core.Infra.MySQL/"]

COPY ["src/CRM/CRM.Infra.CrossCutting.DI/CRM.Infra.CrossCutting.DI.csproj", "src/CRM/CRM.Infra.CrossCutting.DI/"]
COPY ["src/CRM/CRM.Infra.Data.Mongo/CRM.Infra.Data.Mongo.csproj", "src/CRM/CRM.Infra.Data.Mongo/"]
COPY ["src/CRM/CRM.Domain/CRM.Domain.csproj", "src/CRM/CRM.Domain/"]
COPY ["src/CRM/CRM.Events/CRM.Events.csproj", "src/CRM/CRM.Events/"]
COPY ["src/CRM/CRM.Infra.CrossCutting.Identity/CRM.Infra.CrossCutting.Identity.csproj", "src/CRM/CRM.Infra.CrossCutting.Identity/"]

COPY ["src/Catalogo/Catalogo.Infra.CrossCutting.DI/Catalogo.Infra.CrossCutting.DI.csproj", "src/Catalogo/Catalogo.Infra.CrossCutting.DI/"]
COPY ["src/Catalogo/Catalogo.Domain/Catalogo.Domain.csproj", "src/Catalogo/Catalogo.Domain/"]
COPY ["src/Catalogo/Catalogo.Events/Catalogo.Events.csproj", "src/Catalogo/Catalogo.Events/"]
COPY ["src/Catalogo/Catalogo.Infra.Data.MySQL/Catalogo.Infra.Data.MySQL.csproj", "src/Catalogo/Catalogo.Infra.Data.MySQL/"]

RUN dotnet restore "src/API/Rumox.API.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "Rumox.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rumox.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rumox.API.dll"]