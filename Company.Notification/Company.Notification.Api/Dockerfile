#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Company.Notification.Api/Company.Notification.Api.csproj", "Company.Notification.Api/"]
RUN dotnet restore "Company.Notification.Api/Company.Notification.Api.csproj"
COPY . .
WORKDIR "/src/Company.Notification.Api"
RUN dotnet build "Company.Notification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Notification.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Notification.Api.dll"]