namespace Mazad.Domain.Enums;

/// <summary>
/// Persisted account types. The "Visitor" is an anonymous user and is not stored.
/// </summary>
public enum UserType
{
    Customer = 1,
    IndividualSeller = 2,
    Organization = 3,
    Admin = 4
}

public enum UserStatus
{
    PendingActivation = 1,
    Active = 2,
    Suspended = 3,
    Banned = 4
}

public enum OrganizationType
{
    Ministry = 1,
    Municipality = 2,
    Company = 3,
    Bank = 4,
    Institution = 5,
    Other = 6
}

public enum OrganizationStatus
{
    PendingApproval = 1,
    Approved = 2,
    Rejected = 3,
    Suspended = 4
}
