using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Catalog;

/// <summary>
/// A direct-sale listing (car, device, real estate, furniture, electronics, etc.).
/// Created by a seller and shown on the site only after admin approval.
/// </summary>
public class Product : AggregateRoot<Guid>, ISoftDeletable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; } = Money.Zero();
    public ProductCondition Condition { get; set; } = ProductCondition.Used;
    public ListingStatus Status { get; set; } = ListingStatus.Draft;

    public int Quantity { get; set; } = 1;
    public string? Location { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    /// <summary>The seller (individual or organization member) who created the listing.</summary>
    public Guid SellerId { get; set; }

    /// <summary>Set when this listing is owned by an organization.</summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>Set when the listed item is a vehicle.</summary>
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public Guid? ApprovedByUserId { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionReason { get; set; }

    public ICollection<Media> Media { get; set; } = new List<Media>();

    public bool IsVisible => Status == ListingStatus.Approved && !IsDeleted;

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
