namespace Mazad.SharedKernel.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message)
        : base(message) { }

    public ForbiddenException(string userId, string resource, object key)
        : base($"User '{userId}' is forbidden from accessing {resource} '{key}'.") { }
}
