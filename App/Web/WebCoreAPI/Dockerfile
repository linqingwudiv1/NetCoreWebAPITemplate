#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["App/WebCoreAPI/WebCoreService.csproj", "App/WebCoreAPI/"]
COPY ["DLL/BIZ/BusinessCoreDLL/BusinessCoreDLL.csproj", "DLL/BIZ/BusinessCoreDLL/"]
COPY ["CoreServices.Command/AdminServices.Command.csproj", "CoreServices.Command/"]
COPY ["DLL/MassTransitDLL/MassTransitDLL.csproj", "DLL/MassTransitDLL/"]
COPY ["DLL/BaseDLL/BaseDLL.csproj", "DLL/BaseDLL/"]
COPY ["DLL/NetServiceDLL/NetApplictionServiceDLL.csproj", "DLL/NetServiceDLL/"]
COPY ["DLL/WFDLL/WFDLL.csproj", "DLL/WFDLL/"]
COPY ["DLL/OAuth2DLL/OAuth2DLL.csproj", "DLL/OAuth2DLL/"]
COPY ["DLL/DBAccess/DBAccessCoreDLL/DBAccessCoreDLL.csproj", "DLL/DBAccess/DBAccessCoreDLL/"]
COPY ["DLL/DBAccess/DBAccessBaseDLL/DBAccessBaseDLL.csproj", "DLL/DBAccess/DBAccessBaseDLL/"]
RUN dotnet restore "App/WebCoreAPI/WebCoreService.csproj"
COPY . .
WORKDIR "/src/App/WebCoreAPI"
RUN dotnet build "WebCoreService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebCoreService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebCoreService.dll"]