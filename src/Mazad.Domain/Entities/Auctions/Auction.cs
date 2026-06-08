using Mazad.Domain.Entities.Auctions.Events;
using Mazad.Domain.Entities.Catalog;
using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Auctions;

/// <summary>
/// An auction lot. Created by an admin or an organization, it requires admin approval
/// before going live. Supports cash and installment settlement with a dynamic deposit
/// (العربون) model.
/// </summary>
public class Auction : AggregateRoot<Guid>, ISoftDeletable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public AuctionType Type { get; set; } = AuctionType.Cash;
    public AuctionStatus Status { get; set; } = AuctionStatus.Draft;

    public Money StartingPrice { get; set; } = Money.Zero();
    public Money CurrentPrice { get; set; } = Money.Zero();
    public Money? ReservePrice { get; set; }

    /// <summary>Minimum increment allowed between consecutive bids.</summary>
    public Money BidIncrement { get; set; } = Money.Zero();

    /// <summary>Base deposit percentage of the price (e.g. 10).</summary>
    public decimal DepositPercentage { get; set; } = 10m;

    /// <summary>
    /// Threshold percentage of the starting price that triggers a deposit recalculation
    /// for cash auctions (e.g. 200 = 200%).
    /// </summary>
    public decimal CashDepositTriggerPercentage { get; set; } = 200m;

    /// <summary>
    /// Price-rise step (e.g. 10%) over the last calculated price that triggers a deposit
    /// top-up for installment auctions.
    /// </summary>
    public decimal InstallmentDepositStepPercentage { get; set; } = 10m;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    /// <summary>The vehicle being auctioned, when applicable.</summary>
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    /// <summary>Creator of the auction (admin user or organization member).</summary>
    public Guid CreatedByUserId { get; set; }
    public Guid? OrganizationId { get; set; }

    public Guid? ApprovedByUserId { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionReason { get; set; }

    public Guid? WinnerId { get; set; }
    public Money? WinningAmount { get; set; }

    public int BidCount { get; set; }

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    public ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();
    public ICollection<Media> Media { get; set; } = new List<Media>();

    public bool IsLive => Status == AuctionStatus.Live && DateTime.UtcNow < EndTime;

    /// <summary>Required deposit at the current price, using the base percentage.</summary>
    public Money RequiredDepositAtCurrentPrice()
        => Money.FromPercentage(CurrentPrice, DepositPercentage);

    public void GoLive()
    {
        Status = AuctionStatus.Live;
        if (CurrentPrice.Amount == 0)
            CurrentPrice = StartingPrice;
        AddDomainEvent(new AuctionWentLiveEvent(Id));
    }

    public Bid PlaceBid(Guid bidderId, Money amount)
    {
        if (!IsLive)
            throw new InvalidOperationException("Auction is not live.");
        if (!amount.IsGreaterThan(CurrentPrice))
            throw new InvalidOperationException("Bid must be greater than the current price.");

        foreach (var existing in Bids)
            existing.MarkOutbid();

        var bid = new Bid
        {
            AuctionId = Id,
            BidderId = bidderId,
            Amount = amount,
            Status = BidStatus.Winning,
            PlacedAt = DateTime.UtcNow
        };

        Bids.Add(bid);
        CurrentPrice = amount;
        BidCount++;

        AddDomainEvent(new BidPlacedEvent(Id, bid.Id, bidderId, amount.Amount));
        return bid;
    }

    public void End()
    {
        Status = AuctionStatus.Ended;

        var winningBid = Bids
            .OrderByDescending(b => b.Amount.Amount)
            .ThenBy(b => b.PlacedAt)
            .FirstOrDefault();

        if (winningBid is not null)
        {
            WinnerId = winningBid.BidderId;
            WinningAmount = winningBid.Amount;
            winningBid.MarkWon();
        }

        AddDomainEvent(new AuctionEndedEvent(Id, WinnerId, WinningAmount?.Amount));
    }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
