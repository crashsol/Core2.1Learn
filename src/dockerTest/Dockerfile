FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 60572
EXPOSE 44389

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY src/dockerTest/dockerTest.csproj src/dockerTest/
RUN dotnet restore src/dockerTest/dockerTest.csproj
COPY . .
WORKDIR /src/src/dockerTest
RUN dotnet build dockerTest.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish dockerTest.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dockerTest.dll"]
