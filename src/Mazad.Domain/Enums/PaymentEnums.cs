namespace Mazad.Domain.Enums;

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5
}

public enum PaymentMethod
{
    Stripe = 1,
    PayPal = 2,
    LocalGateway = 3,
    Wallet = 4,
    BankTransfer = 5
}

/// <summary>
/// What a payment is for, so it can be linked to the right entity.
/// </summary>
public enum PaymentPurpose
{
    Deposit = 1,
    DepositTopUp = 2,
    DirectSalePurchase = 3,
    AuctionSettlement = 4,
    AdvertisementFee = 5,
    WalletTopUp = 6
}

public enum RefundStatus
{
    Requested = 1,
    Approved = 2,
    Processed = 3,
    Rejected = 4
}

public enum WalletTransactionType
{
    Credit = 1,
    Debit = 2
}

public enum WalletTransactionReason
{
    DepositRefund = 1,
    AuctionRefund = 2,
    Purchase = 3,
    TopUp = 4,
    Withdrawal = 5,
    Adjustment = 6
}
