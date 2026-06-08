using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Auctions;

/// <summary>
/// A participant's deposit (العربون) for an auction. The required amount grows
/// dynamically as the price rises; the participant tops up the difference to keep bidding.
/// </summary>
public class Deposit : AggregateRoot<Guid>
{
    public Guid AuctionId { get; set; }
    public Auction? Auction { get; set; }

    public Guid UserId { get; set; }

    /// <summary>Total amount currently required to be held as deposit.</summary>
    public Money RequiredAmount { get; set; } = Money.Zero();

    /// <summary>Total amount the participant has actually paid so far.</summary>
    public Money PaidAmount { get; set; } = Money.Zero();

    /// <summary>The price level the required deposit was last calculated against.</summary>
    public Money LastCalculatedAtPrice { get; set; } = Money.Zero();

    public DepositStatus Status { get; set; } = DepositStatus.Pending;
    public DateTime? FirstPaidAt { get; set; }

    /// <summary>Outstanding amount the participant still needs to pay.</summary>
    public Money Outstanding()
        => PaidAmount.IsLessThan(RequiredAmount)
            ? RequiredAmount.Subtract(PaidAmount)
            : Money.Zero(RequiredAmount.Currency);

    public void RegisterPayment(Money amount)
    {
        PaidAmount = PaidAmount.Add(amount);
        FirstPaidAt ??= DateTime.UtcNow;
        Status = Outstanding().Amount <= 0 ? DepositStatus.Paid : DepositStatus.TopUpRequired;
    }

    /// <summary>
    /// Recalculates the required deposit for the given current price and auction settings.
    /// Returns true if a top-up is now required.
    /// </summary>
    public bool Recalculate(Auction auction)
    {
        var newRequired = Money.FromPercentage(auction.CurrentPrice, auction.DepositPercentage);

        bool shouldRecalculate = auction.Type switch
        {
            AuctionType.Cash =>
                auction.CurrentPrice.Amount >=
                auction.StartingPrice.Amount * (auction.CashDepositTriggerPercentage / 100m),
            AuctionType.Installment =>
                auction.CurrentPrice.Amount >=
                LastCalculatedAtPrice.Amount * (1 + auction.InstallmentDepositStepPercentage / 100m),
            _ => false
        };

        if (!shouldRecalculate)
            return Outstanding().Amount > 0;

        RequiredAmount = newRequired;
        LastCalculatedAtPrice = auction.CurrentPrice;

        if (Outstanding().Amount > 0)
        {
            Status = DepositStatus.TopUpRequired;
            return true;
        }

        return false;
    }
}
