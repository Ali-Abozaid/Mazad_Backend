namespace Mazad.Domain.Enums;

/// <summary>
/// Settlement model of an auction:
/// Cash = full settlement after winning,
/// Installment = financing arranged off-platform after winning.
/// </summary>
public enum AuctionType
{
    Cash = 1,
    Installment = 2
}

/// <summary>
/// Lifecycle of an auction.
/// </summary>
public enum AuctionStatus
{
    Draft = 1,
    PendingApproval = 2,
    Approved = 3,
    Live = 4,
    Ended = 5,
    Cancelled = 6
}

public enum BidStatus
{
    Placed = 1,
    Outbid = 2,
    Winning = 3,
    Won = 4
}

/// <summary>
/// State of a participant's deposit (العربون) for an auction.
/// </summary>
public enum DepositStatus
{
    Pending = 1,
    Paid = 2,
    TopUpRequired = 3,
    AppliedToPurchase = 4,
    Refunded = 5,
    MovedToWallet = 6,
    Forfeited = 7
}
