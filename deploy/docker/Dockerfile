# syntax=docker/dockerfile:1

ARG DOTNET_VERSION=9.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

COPY BankingPet.sln ./
COPY global.json ./
COPY Directory.Build.props ./
COPY Directory.Packages.props ./
COPY src/ ./src/

RUN dotnet restore BankingPet.sln

ARG PROJECT_PATH
RUN dotnet publish ${PROJECT_PATH} -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet"]
CMD ["App.dll"]
