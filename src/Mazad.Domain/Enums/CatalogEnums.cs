namespace Mazad.Domain.Enums;

/// <summary>
/// Approval/sale lifecycle for a direct-sale product listing.
/// </summary>
public enum ListingStatus
{
    Draft = 1,
    PendingApproval = 2,
    Approved = 3,
    Rejected = 4,
    Sold = 5,
    Expired = 6,
    Withdrawn = 7
}

public enum ProductCondition
{
    New = 1,
    Used = 2,
    Refurbished = 3
}

public enum MediaType
{
    Image = 1,
    Document = 2,
    Video = 3
}

public enum FuelType
{
    Petrol = 1,
    Diesel = 2,
    Hybrid = 3,
    Electric = 4,
    Other = 5
}

public enum TransmissionType
{
    Manual = 1,
    Automatic = 2
}
