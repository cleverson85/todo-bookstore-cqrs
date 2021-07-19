#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ToDo.Service.Api/ToDo.Service.Api.csproj", "src/ToDo.Service.Api/"]
COPY ["src/ToDo.Application/ToDo.Application.csproj", "src/ToDo.Application/"]
COPY ["src/ToDo.Domain/ToDo.Domain.csproj", "src/ToDo.Domain/"]
COPY ["src/ToDo.Infra.Data/ToDo.Infra.Data.csproj", "src/ToDo.Infra.Data/"]
COPY ["src/ToDo.Domain.Core/ToDo.Domain.Core.csproj", "src/ToDo.Domain.Core/"]
COPY ["src/ToDo.Infra.CrossCutting.IoC/ToDo.Infra.CrossCutting.IoC.csproj", "src/ToDo.Infra.CrossCutting.IoC/"]
COPY ["src/ToDo.Infra.CrossCutting.Bus/ToDo.Infra.CrossCutting.Bus.csproj", "src/ToDo.Infra.CrossCutting.Bus/"]
RUN dotnet restore "src/ToDo.Service.Api/ToDo.Service.Api.csproj"
COPY . .
WORKDIR "/src/src/ToDo.Service.Api"
RUN dotnet build "ToDo.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.Service.Api.dll"]
