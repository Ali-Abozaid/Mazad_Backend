using Mazad.Domain.Enums;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Catalog;

/// <summary>
/// Car-specific specifications. Referenced by auctions and direct-sale products
/// when the listed item is a vehicle.
/// </summary>
public class Vehicle : AggregateRoot<Guid>, ISoftDeletable
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Trim { get; set; }
    public string? Color { get; set; }
    public int Mileage { get; set; }
    public string? Vin { get; set; }
    public string? PlateNumber { get; set; }

    public FuelType FuelType { get; set; } = FuelType.Petrol;
    public TransmissionType Transmission { get; set; } = TransmissionType.Automatic;
    public int? EngineCapacityCc { get; set; }
    public ProductCondition Condition { get; set; } = ProductCondition.Used;

    public string? Description { get; set; }

    public ICollection<Media> Media { get; set; } = new List<Media>();

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
