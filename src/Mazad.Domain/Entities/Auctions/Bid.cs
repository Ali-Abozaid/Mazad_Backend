using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Auctions;

/// <summary>
/// A single bid placed by a customer on an auction.
/// </summary>
public class Bid : BaseEntity<Guid>
{
    public Guid AuctionId { get; set; }
    public Auction? Auction { get; set; }

    public Guid BidderId { get; set; }

    public Money Amount { get; set; } = Money.Zero();
    public BidStatus Status { get; set; } = BidStatus.Placed;
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

    public void MarkOutbid()
    {
        if (Status is BidStatus.Winning or BidStatus.Placed)
            Status = BidStatus.Outbid;
    }

    public void MarkWon() => Status = BidStatus.Won;
}
