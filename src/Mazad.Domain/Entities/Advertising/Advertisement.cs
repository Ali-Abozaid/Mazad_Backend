using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Advertising;

/// <summary>
/// A paid or promotional advertisement (hero banner / featured slot). Admin controls
/// duration, display order and impression caps.
/// </summary>
public class Advertisement : AggregateRoot<Guid>, ISoftDeletable
{
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? LinkUrl { get; set; }

    public AdvertisementType Type { get; set; } = AdvertisementType.Featured;
    public AdvertisementPlacement Placement { get; set; } = AdvertisementPlacement.HomeFeatured;
    public AdvertisementStatus Status { get; set; } = AdvertisementStatus.PendingApproval;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DisplayOrder { get; set; }

    /// <summary>Optional cap on the number of impressions (0 = unlimited).</summary>
    public int MaxImpressions { get; set; }
    public int ImpressionCount { get; set; }
    public int ClickCount { get; set; }

    public Money? Fee { get; set; }

    /// <summary>The advertiser (individual or organization member), if not admin-created.</summary>
    public Guid? AdvertiserUserId { get; set; }
    public Guid? OrganizationId { get; set; }

    public bool IsActive =>
        Status == AdvertisementStatus.Active
        && !IsDeleted
        && DateTime.UtcNow >= StartDate
        && DateTime.UtcNow <= EndDate
        && (MaxImpressions == 0 || ImpressionCount < MaxImpressions);

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
