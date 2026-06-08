using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Payments;

/// <summary>
/// A refund issued against a payment (e.g. returning a losing bidder's deposit).
/// </summary>
public class Refund : AggregateRoot<Guid>
{
    public Guid PaymentId { get; set; }
    public Payment? Payment { get; set; }

    public Guid UserId { get; set; }

    public Money Amount { get; set; } = Money.Zero();
    public RefundStatus Status { get; set; } = RefundStatus.Requested;
    public string? Reason { get; set; }

    /// <summary>True when the refund was credited to the internal wallet instead of the source.</summary>
    public bool ToWallet { get; set; }

    public DateTime? ProcessedAt { get; set; }
    public Guid? ProcessedByUserId { get; set; }
}
