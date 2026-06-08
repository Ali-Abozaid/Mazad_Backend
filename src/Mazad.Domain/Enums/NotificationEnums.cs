namespace Mazad.Domain.Enums;

public enum NotificationType
{
    AdvertisementApproved = 1,
    AdvertisementRejected = 2,
    AuctionWon = 3,
    AuctionEnded = 4,
    DepositPaid = 5,
    DepositRefunded = 6,
    DepositTopUpRequired = 7,
    Outbid = 8,
    ListingApproved = 9,
    ListingRejected = 10,
    OrderPlaced = 11,
    General = 12
}

/// <summary>
/// The type of entity a notification points to, enabling deep links.
/// </summary>
public enum NotificationReferenceType
{
    None = 0,
    Auction = 1,
    Product = 2,
    Order = 3,
    Advertisement = 4,
    Deposit = 5,
    Payment = 6
}
