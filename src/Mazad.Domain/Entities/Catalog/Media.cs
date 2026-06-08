using Mazad.Domain.Enums;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Catalog;

/// <summary>
/// A stored image/document/video (e.g. on Cloudinary or Azure Blob) attached to a
/// vehicle, product, or auction. Exactly one owning foreign key is expected to be set.
/// </summary>
public class Media : BaseEntity<Guid>
{
    public string Url { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public MediaType Type { get; set; } = MediaType.Image;
    public string? AltText { get; set; }
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }

    public Guid? VehicleId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? AuctionId { get; set; }
}
