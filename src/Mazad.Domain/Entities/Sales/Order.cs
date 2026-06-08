using Mazad.Domain.Enums;
using Mazad.Domain.ValueObjects;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Sales;

/// <summary>
/// A direct-sale purchase of an approved product by a customer.
/// </summary>
public class Order : AggregateRoot<Guid>, ISoftDeletable
{
    public string OrderNumber { get; set; } = string.Empty;

    public Guid ProductId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid SellerId { get; set; }

    public int Quantity { get; set; } = 1;
    public Money UnitPrice { get; set; } = Money.Zero();
    public Money TotalAmount { get; set; } = Money.Zero();

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public Guid? PaymentId { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public string? ShippingAddress { get; set; }
    public string? Notes { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
