using Mazad.Domain.Enums;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Users;

/// <summary>
/// A registered account on the platform. Visitors are anonymous and not persisted.
/// </summary>
public class User : AggregateRoot<Guid>, ISoftDeletable
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public UserType UserType { get; set; } = UserType.Customer;
    public UserStatus Status { get; set; } = UserStatus.PendingActivation;

    public bool EmailConfirmed { get; set; }
    public bool PhoneConfirmed { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Set when this account represents an organization member/owner.
    /// </summary>
    public Guid? OrganizationId { get; set; }
    public Organization? Organization { get; set; }

    public Wallet? Wallet { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

    public bool CanParticipate => Status == UserStatus.Active && !IsDeleted;
}
