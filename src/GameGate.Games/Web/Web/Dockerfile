#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GameGate.Games/Web/Web/Web.csproj", "src/GameGate.Games/Web/Web/"]
COPY ["src/GameGate.Games/Application/Application.csproj", "src/GameGate.Games/Application/"]
COPY ["src/GameGate.Games/Domain/Domain.csproj", "src/GameGate.Games/Domain/"]
COPY ["src/GameGate.Games/Infrastructure/Infrastructure.csproj", "src/GameGate.Games/Infrastructure/"]
COPY ["src/GameGate.Games/Presentation/Presentation.csproj", "src/GameGate.Games/Presentation/"]
RUN dotnet restore "./src/GameGate.Games/Web/Web/Web.csproj"
COPY . .
WORKDIR "/src/src/GameGate.Games/Web/Web"
RUN dotnet build "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]