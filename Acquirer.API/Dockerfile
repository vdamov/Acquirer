#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Acquirer.API/Acquirer.API.csproj", "Acquirer.API/"]
RUN dotnet restore "Acquirer.API/Acquirer.API.csproj"
COPY . .
WORKDIR "/src/Acquirer.API"
RUN dotnet build "Acquirer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Acquirer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acquirer.API.dll"]
