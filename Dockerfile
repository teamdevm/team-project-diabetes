# First stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /DockerSource

COPY . .

RUN dotnet restore


COPY . .

WORKDIR /DockerSource/Diabetes.MVC


RUN dotnet publish -c release -o /DockerOutput/Website

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /DockerOutput/Website
COPY --from=build /DockerOutput/Website ./
ENTRYPOINT ["dotnet", "Diabetes.MVC.dll"]