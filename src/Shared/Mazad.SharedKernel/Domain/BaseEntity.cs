namespace Mazad.SharedKernel.Domain;

public abstract class BaseEntity<TId> : IAuditableEntity where TId : notnull
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
