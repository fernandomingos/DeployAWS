FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DeployAWS.API/DeployAWS.API.csproj", "DeployAWS.API/"]
COPY ["DeployAWS.Application/DeployAWS.Application.csproj", "DeployAWS.Application/"]
COPY ["DeployAWS.Domain/DeployAWS.Domain.csproj", "DeployAWS.Domain/"]
COPY ["DeployAWS.Domain.Core/DeployAWS.Domain.Core.csproj", "DeployAWS.Domain.Core/"]
COPY ["DeployAWS.Domain.Services/DeployAWS.Domain.Services.csproj", "DeployAWS.Domain.Services/"]
COPY ["DeployAWS.Infrastructure/DeployAWS.Infrastructure.csproj", "DeployAWS.Infrastructure/"]
COPY ["DeployAWS.Tests/DeployAWS.Tests.csproj", "DeployAWS.Tests/"]
RUN dotnet restore "DeployAWS.API/DeployAWS.API.csproj"
COPY . .
WORKDIR "/src/DeployAWS.API"
RUN dotnet build "DeployAWS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash \
    && apt-get install nodejs -yq
RUN dotnet publish "DeployAWS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeployAWS.API.dll"]
