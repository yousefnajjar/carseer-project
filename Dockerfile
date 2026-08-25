FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/CarSeer.Web/CarSeer.Web.csproj", "src/CarSeer.Web/"]
COPY ["src/CarSeer.Application/CarSeer.Application.csproj", "src/CarSeer.Application/"]
COPY ["src/CarSeer.Infrastructure/CarSeer.Infrastructure.csproj", "src/CarSeer.Infrastructure/"]
COPY ["src/CarSeer.Domain/CarSeer.Domain.csproj", "src/CarSeer.Domain/"]
RUN dotnet restore "src/CarSeer.Web/CarSeer.Web.csproj"

COPY . .
RUN dotnet publish "src/CarSeer.Web/CarSeer.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_ENVIRONMENT=Production

RUN useradd --create-home --uid 10001 appuser && chown -R appuser /app
USER appuser

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarSeer.Web.dll"]
