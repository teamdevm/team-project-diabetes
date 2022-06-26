# First stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /DockerSource

COPY *.sln .
COPY Diabetes.MVC/Diabetes.MVC.csproj ./Diabetes.MVC/
COPY Diabetes.Application/Diabetes.Application.csproj ./Diabetes.Application/
COPY Diabetes.Domain/Diabetes.Domain.csproj ./Diabetes.Domain/
COPY Diabetes.Persistence/Diabetes.Persistence.csproj ./Diabetes.Persistence/

RUN dotnet restore




COPY Diabetes.MVC/. ./Diabetes.MVC/
COPY Diabetes.Application/. ./Diabetes.Application/
COPY Diabetes.Domain/. ./Diabetes.Domain/
COPY Diabetes.Persistence/. ./Diabetes.Persistence/

WORKDIR /DockerSource/Diabetes.MVC


RUN dotnet publish -c release -o /DockerOutput/Website

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /DockerOutput/Website
COPY --from=build /DockerOutput/Website ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Diabetes.MVC.dll