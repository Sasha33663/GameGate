#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GameGate.Market/Web/Web.csproj", "src/GameGate.Market/Web/"]
COPY ["src/GameGate.Market/Application/Application.csproj", "src/GameGate.Market/Application/"]
COPY ["src/GameGate.Market/Domain/Domain.csproj", "src/GameGate.Market/Domain/"]
COPY ["src/GameGate.Market/Infrastructure/Infrastructure.csproj", "src/GameGate.Market/Infrastructure/"]
COPY ["src/GameGate.Market/Presentation/Presentation.csproj", "src/GameGate.Market/Presentation/"]
RUN dotnet restore "./src/GameGate.Market/Web/Web.csproj"
COPY . .
WORKDIR "/src/src/GameGate.Market/Web"
RUN dotnet build "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]