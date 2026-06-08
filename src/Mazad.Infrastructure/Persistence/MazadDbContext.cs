using Mazad.SharedKernel.Common;
using Mazad.SharedKernel.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mazad.Infrastructure.Persistence;

public class MazadDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private IDbContextTransaction? _currentTransaction;

    public MazadDbContext(DbContextOptions<MazadDbContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var aggregates = ChangeTracker.Entries<AggregateRoot<Guid>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var events = aggregates.SelectMany(a => a.DomainEvents).ToList();
        aggregates.ForEach(a => a.ClearDomainEvents());

        var result = await base.SaveChangesAsync(ct);

        foreach (var domainEvent in events)
            await _mediator.Publish(domainEvent, ct);

        return result;
    }

    public async Task BeginTransactionAsync(CancellationToken ct = default)
        => _currentTransaction = await Database.BeginTransactionAsync(ct);

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        await _currentTransaction!.CommitAsync(ct);
        _currentTransaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        await _currentTransaction!.RollbackAsync(ct);
        _currentTransaction = null;
    }
}
