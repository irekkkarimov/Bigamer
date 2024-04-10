FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bigamer.Presentation/Bigamer.Presentation.csproj", "Bigamer.Presentation/"]
RUN dotnet restore "Bigamer.Presentation/Bigamer.Presentation.csproj"
COPY . .
WORKDIR "/src/Bigamer.Presentation"
RUN dotnet build "Bigamer.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bigamer.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bigamer.Presentation.dll"]
