using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Auctions.Events;

public sealed record AuctionWentLiveEvent(Guid AuctionId) : DomainEvent;

public sealed record BidPlacedEvent(Guid AuctionId, Guid BidId, Guid BidderId, decimal Amount) : DomainEvent;

public sealed record AuctionEndedEvent(Guid AuctionId, Guid? WinnerId, decimal? WinningAmount) : DomainEvent;

public sealed record DepositTopUpRequiredEvent(Guid AuctionId, Guid DepositId, Guid UserId, decimal AdditionalAmount) : DomainEvent;
