using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.ValueObjects;

/// <summary>
/// Monetary amount with currency. Default currency is Omani Rial (OMR).
/// Stored as an EF Core owned type on the owning entities.
/// </summary>
public sealed class Money : ValueObject
{
    public const string DefaultCurrency = "OMR";

    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money()
    {
        Currency = DefaultCurrency;
    }

    public Money(decimal amount, string currency = DefaultCurrency)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Money amount cannot be negative.");
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public static Money Zero(string currency = DefaultCurrency) => new(0m, currency);

    public static Money FromPercentage(Money baseAmount, decimal percentage)
        => new(decimal.Round(baseAmount.Amount * (percentage / 100m), 3, MidpointRounding.AwayFromZero), baseAmount.Currency);

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount - other.Amount, Currency);
    }

    public bool IsGreaterThan(Money other)
    {
        EnsureSameCurrency(other);
        return Amount > other.Amount;
    }

    public bool IsLessThan(Money other)
    {
        EnsureSameCurrency(other);
        return Amount < other.Amount;
    }

    private void EnsureSameCurrency(Money other)
    {
        if (!string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException($"Currency mismatch: {Currency} vs {other.Currency}.");
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:0.###} {Currency}";
}
