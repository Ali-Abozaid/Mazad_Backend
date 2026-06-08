namespace Mazad.SharedKernel.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string UserRole { get; }
    bool IsAuthenticated { get; }
}
