FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
COPY ["FriendStuff.csproj", "."]
RUN dotnet restore "FriendStuff.csproj"
COPY . .
RUN dotnet build "FriendStuff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FriendStuff.csproj" -c Release -o /app/publish /p:UseAppHost=false   

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FriendStuff.dll"]
