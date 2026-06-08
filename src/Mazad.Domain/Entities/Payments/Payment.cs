using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Payments;

/// <summary>
/// A payment processed through a gateway (Stripe / PayPal / local Oman gateway) or wallet.
/// </summary>
public class Payment : AggregateRoot<Guid>
{
    public Guid UserId { get; set; }

    public Money Amount { get; set; } = Money.Zero();
    public PaymentMethod Method { get; set; } = PaymentMethod.LocalGateway;
    public PaymentPurpose Purpose { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    /// <summary>The entity this payment relates to (auction, deposit, order, advertisement).</summary>
    public Guid? ReferenceId { get; set; }

    public string? ProviderName { get; set; }
    public string? ProviderTransactionId { get; set; }
    public string? FailureReason { get; set; }

    public DateTime? CompletedAt { get; set; }

    public ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public void MarkCompleted(string? providerTransactionId = null)
    {
        Status = PaymentStatus.Completed;
        ProviderTransactionId = providerTransactionId ?? ProviderTransactionId;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkFailed(string? reason)
    {
        Status = PaymentStatus.Failed;
        FailureReason = reason;
    }
}
