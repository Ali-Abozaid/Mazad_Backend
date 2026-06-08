namespace Mazad.Domain.Enums;

public enum AdvertisementType
{
    HeroBanner = 1,
    Featured = 2
}

public enum AdvertisementStatus
{
    PendingApproval = 1,
    Active = 2,
    Paused = 3,
    Expired = 4,
    Rejected = 5
}

/// <summary>
/// Where an advertisement is allowed to be rendered on the site.
/// </summary>
public enum AdvertisementPlacement
{
    HomeHero = 1,
    HomeFeatured = 2,
    Sidebar = 3,
    AuctionDetails = 4
}
