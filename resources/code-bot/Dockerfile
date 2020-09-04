#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["CognitiveSearchBot.csproj", ""]
RUN dotnet restore "./CognitiveSearchBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CognitiveSearchBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CognitiveSearchBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CognitiveSearchBot.dll"]