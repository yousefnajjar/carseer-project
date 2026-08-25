# Carseer

## Solution

- `src/CarSeer.Domain` — entities
- `src/CarSeer.Application` — MediatR queries and validation
- `src/CarSeer.Infrastructure` — NHTSA HTTP client, cache
- `src/CarSeer.Web` — MVC UI, `/api/makes`

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Or [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Start locally with .NET

1. Clone the repository and open a terminal in the repo root.
2. Restore and run the web project:

```powershell
dotnet restore CarSeer.sln
dotnet run --project src/CarSeer.Web --launch-profile http
```

3. Open [http://localhost:5080](http://localhost:5080).

## Start locally with Docker

1. Install and start Docker Desktop.
2. From the repo root:

```powershell
docker compose up --build
```

3. Open [http://localhost:8080](http://localhost:8080).

Stop the container with `Ctrl+C`, then `docker compose down`.

## APIs used

- Makes: `https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json`
- Types: `https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json`
- Models: `https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json`
- Models by type: add `/vehicletype/{type}`

## AWS Express Mode 

https://us-east-1.console.aws.amazon.com/ecs/v2/clusters/default/express-services/carseer-606c/resources?view=timeline&region=us-east-1