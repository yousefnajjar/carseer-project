namespace CarSeer.Infrastructure.Nhtsa;

public sealed class NhtsaOptions
{
    public const string SectionName = "Nhtsa";
    public string BaseUrl { get; init; } = "https://vpic.nhtsa.dot.gov/api/";
    public int TimeoutSeconds { get; init; } = 30;
}
