namespace CarSeer.Domain.Exceptions;

public sealed class VehicleCatalogException : Exception
{
    public VehicleCatalogException(string message) : base(message)
    {
    }

    public VehicleCatalogException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
