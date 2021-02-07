#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["App/Console/CoreMicroServices/CoreMicroServices.csproj", "App/Console/CoreMicroServices/"]
COPY ["DLL/MassTransitDLL/MassTransitDLL.csproj", "DLL/MassTransitDLL/"]
COPY ["DLL/BaseDLL/BaseDLL.csproj", "DLL/BaseDLL/"]
COPY ["DLL/BIZ/MiscroServes/AdminServices.Event/AdminServices.Event.csproj", "DLL/BIZ/MiscroServes/AdminServices.Event/"]
COPY ["DLL/DBAccess/DBAccessBaseDLL/DBAccessBaseDLL.csproj", "DLL/DBAccess/DBAccessBaseDLL/"]
COPY ["CoreServices.Command/AdminServices.Command.csproj", "CoreServices.Command/"]
COPY ["DLL/DBAccess/DBAccessCoreDLL/DBAccessCoreDLL.csproj", "DLL/DBAccess/DBAccessCoreDLL/"]
COPY ["DLL/WFDLL/WFDLL.csproj", "DLL/WFDLL/"]
RUN dotnet restore "App/Console/CoreMicroServices/CoreMicroServices.csproj"
COPY . .
WORKDIR "/src/App/Console/CoreMicroServices"
RUN dotnet build "CoreMicroServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreMicroServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreMicroServices.dll"]