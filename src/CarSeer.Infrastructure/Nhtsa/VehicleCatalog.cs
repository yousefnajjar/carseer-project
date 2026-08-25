using System.Net.Http.Json;
using System.Text.Json;
using CarSeer.Application.Interfaces;
using CarSeer.Domain.Entities;
using CarSeer.Domain.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CarSeer.Infrastructure.Nhtsa;

public sealed class VehicleCatalog(HttpClient httpClient, IMemoryCache cache, ILogger<VehicleCatalog> logger) : IVehicleCatalog
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private const string MakesCacheKey = "nhtsa:makes";

    public async Task<IReadOnlyList<Make>> GetMakesAsync(CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(MakesCacheKey, out IReadOnlyList<Make>? cached) && cached is not null)
        {
            return cached;
        }

        var response = await GetAsync<MakeDto>("vehicles/GetAllMakes?format=json", cancellationToken);

        var makes = response.Results
            .Where(dto => dto.MakeId > 0 && !string.IsNullOrWhiteSpace(dto.MakeName))
            .Select(dto => new Make(dto.MakeId, dto.MakeName.Trim()))
            .OrderBy(make => make.Name)
            .ToList();

        cache.Set(MakesCacheKey, makes, TimeSpan.FromHours(24));
        return makes;
    }

    public async Task<IReadOnlyList<VehicleType>> GetVehicleTypesAsync(int makeId,CancellationToken cancellationToken)
    {
        var cacheKey = $"nhtsa:types:{makeId}";

        if (cache.TryGetValue(cacheKey, out IReadOnlyList<VehicleType>? cached) && cached is not null)
        {
            return cached;
        }

        var response = await GetAsync<VehicleTypeDto>(
            $"vehicles/GetVehicleTypesForMakeId/{makeId}?format=json",
            cancellationToken);

        var types = response.Results
            .Where(dto => dto.VehicleTypeId > 0 && !string.IsNullOrWhiteSpace(dto.VehicleTypeName))
            .Select(dto => new VehicleType(dto.VehicleTypeId, dto.VehicleTypeName.Trim()))
            .ToList();

        cache.Set(cacheKey, types, TimeSpan.FromHours(1));
        return types;
    }

    public async Task<IReadOnlyList<VehicleModel>> GetModelsAsync(int makeId,int year,string? vehicleType,CancellationToken cancellationToken)
    {
        var path = string.IsNullOrWhiteSpace(vehicleType)
            ? $"vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json"
            : $"vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}/vehicletype/{Uri.EscapeDataString(vehicleType.Trim())}?format=json";

        var response = await GetAsync<ModelDto>(path, cancellationToken);

        return response.Results
            .Where(dto => dto.ModelId > 0 && !string.IsNullOrWhiteSpace(dto.ModelName))
            .Select(dto => new VehicleModel(
                dto.ModelId,
                dto.ModelName.Trim(),
                dto.MakeId,
                dto.MakeName?.Trim() ?? string.Empty,
                dto.VehicleTypeId,
                dto.VehicleTypeName?.Trim()))
            .ToList();
    }

    private async Task<ApiResponse<T>> GetAsync<T>(string relativePath, CancellationToken cancellationToken)
    {
        try
        {
            using var response = await httpClient.GetAsync(relativePath, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning(
                    "NHTSA request failed with {StatusCode} for {Path}",
                    (int)response.StatusCode,
                    relativePath);

                throw new VehicleCatalogException(
                    $"NHTSA returned {(int)response.StatusCode} while loading vehicle data.");
            }

            var payload = await response.Content.ReadFromJsonAsync<ApiResponse<T>>(
                JsonOptions,
                cancellationToken);

            return payload ?? new ApiResponse<T>();
        }
        catch (VehicleCatalogException)
        {
            throw;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "NHTSA request failed for {Path}", relativePath);
            throw new VehicleCatalogException("Unable to reach the NHTSA vehicle catalog. Try again shortly.", exception);
        }
    }
}
