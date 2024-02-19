FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Al-Musawi-Bank-Backend/Al-Musawi-Bank-Backend.csproj", "Al-Musawi-Bank-Backend/"]
RUN dotnet restore "Al-Musawi-Bank-Backend/Al-Musawi-Bank-Backend.csproj"
COPY . .
WORKDIR "/src/Al-Musawi-Bank-Backend"
RUN dotnet build "Al-Musawi-Bank-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Al-Musawi-Bank-Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Al-Musawi-Bank-Backend.dll"]
