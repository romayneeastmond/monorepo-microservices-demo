#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Company.Course/Company.Course.Api/Company.Course.Api.csproj", "Company.Course.Api/"]
RUN dotnet restore "Company.Course.Api/Company.Course.Api.csproj"
COPY . .
WORKDIR "/src/Company.Course/Company.Course.Api"
RUN dotnet build "Company.Course.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Course.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Course.Api.dll"]