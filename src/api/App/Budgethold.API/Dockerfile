#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/Budgethold.API/Budgethold.API.csproj", "App/Budgethold.API/"]
COPY ["Infrastructure/Budgethold.Persistance/Budgethold.Persistance.csproj", "Infrastructure/Budgethold.Persistance/"]
COPY ["Common/Budgethold.Common/Budgethold.Common.csproj", "Common/Budgethold.Common/"]
COPY ["Infrastructure/Budgethold.Security/Budgethold.Security.csproj", "Infrastructure/Budgethold.Security/"]
COPY ["Common/Budgethold.ValidationExtensions/Budgethold.ValidationExtensions.csproj", "Common/Budgethold.ValidationExtensions/"]
COPY ["Core/Budgethold.Domain/Budgethold.Domain.csproj", "Core/Budgethold.Domain/"]
COPY ["Core/Budgethold.Application/Budgethold.Application.csproj", "Core/Budgethold.Application/"]
RUN dotnet restore "App/Budgethold.API/Budgethold.API.csproj"
COPY . .
WORKDIR "/src/App/Budgethold.API"
RUN dotnet build "Budgethold.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Budgethold.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budgethold.API.dll"]
