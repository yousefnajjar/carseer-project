using CarSeer.Domain.Entities;

namespace CarSeer.Web.Models;

public sealed class VehicleLookupViewModel
{
    public int? MakeId { get; set; }

    public string? MakeName { get; set; }

    public int? Year { get; set; }

    public string? VehicleType { get; set; }

    public IReadOnlyList<VehicleType> VehicleTypes { get; set; } = [];

    public IReadOnlyList<VehicleModel> Models { get; set; } = [];

    public string? ErrorMessage { get; set; }

    public bool HasSearched { get; set; }

    public int MinYear { get; } = 1920;

    public int MaxYear { get; } = DateTime.UtcNow.Year + 1;
}
