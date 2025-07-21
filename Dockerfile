ARG DOT_NET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/sdk:${DOT_NET_VERSION} AS build

WORKDIR /src
COPY ["FriendStuff.csproj", "."]
RUN dotnet restore "FriendStuff.csproj"
COPY . .
RUN dotnet build "FriendStuff.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
RUN dotnet publish "FriendStuff.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false   

FROM mcr.microsoft.com/dotnet/aspnet:${DOT_NET_VERSION} AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 10000

ENTRYPOINT ["dotnet", "FriendStuff.dll"]
