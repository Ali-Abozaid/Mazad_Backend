using Mazad.Domain.Enums;
using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Users;

/// <summary>
/// A government or private entity (ministry, municipality, company, bank, institution)
/// that can create auctions and list assets. Requires admin approval.
/// </summary>
public class Organization : AggregateRoot<Guid>, ISoftDeletable
{
    public string Name { get; set; } = string.Empty;
    public OrganizationType Type { get; set; } = OrganizationType.Other;
    public OrganizationStatus Status { get; set; } = OrganizationStatus.PendingApproval;

    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }

    /// <summary>The user account that owns/administers this organization.</summary>
    public Guid OwnerUserId { get; set; }

    public ICollection<User> Members { get; set; } = new List<User>();

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
