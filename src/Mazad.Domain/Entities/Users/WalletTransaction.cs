using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Users;

/// <summary>
/// An immutable ledger entry recording a change to a wallet's balance.
/// </summary>
public class WalletTransaction : BaseEntity<Guid>
{
    public Guid WalletId { get; set; }
    public Wallet? Wallet { get; set; }

    public WalletTransactionType Type { get; set; }
    public WalletTransactionReason Reason { get; set; }

    public Money Amount { get; set; } = Money.Zero();
    public Money BalanceAfter { get; set; } = Money.Zero();

    public string? Description { get; set; }

    /// <summary>Optional link to the source entity (auction, deposit, payment, order).</summary>
    public Guid? ReferenceId { get; set; }
}
