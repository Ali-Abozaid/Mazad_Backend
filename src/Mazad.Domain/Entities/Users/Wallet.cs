using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Users;

/// <summary>
/// Internal wallet used to hold refunded deposits and balances per user.
/// </summary>
public class Wallet : AggregateRoot<Guid>
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Money Balance { get; set; } = Money.Zero();

    public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();

    public void Credit(Money amount, WalletTransactionReason reason, string? description = null, Guid? referenceId = null)
    {
        Balance = Balance.Add(amount);
        Transactions.Add(new WalletTransaction
        {
            WalletId = Id,
            Type = WalletTransactionType.Credit,
            Reason = reason,
            Amount = amount,
            BalanceAfter = Balance,
            Description = description,
            ReferenceId = referenceId
        });
    }

    public void Debit(Money amount, WalletTransactionReason reason, string? description = null, Guid? referenceId = null)
    {
        if (Balance.IsLessThan(amount))
            throw new InvalidOperationException("Insufficient wallet balance.");

        Balance = Balance.Subtract(amount);
        Transactions.Add(new WalletTransaction
        {
            WalletId = Id,
            Type = WalletTransactionType.Debit,
            Reason = reason,
            Amount = amount,
            BalanceAfter = Balance,
            Description = description,
            ReferenceId = referenceId
        });
    }
}
