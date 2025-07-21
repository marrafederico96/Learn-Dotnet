ARG  DOT_NET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/sdk:${DOT_NET_VERSION} AS build

FROM mcr.microsoft.com/dotnet/aspnet:${DOT_NET_VERSION} AS base

WORKDIR /src
COPY ["FriendStuff/FriendStuff.csproj", "FriendStuff/"]
RUN dotnet restore "FriendStuff/FriendStuff.csproj"
COPY . .
WORKDIR "/src/FriendStuff"
RUN dotnet build "FriendStuff.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
RUN dotnet publish "FriendStuff.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false   

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FriendStuff.dll"]