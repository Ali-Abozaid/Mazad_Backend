namespace Mazad.SharedKernel.Exceptions;

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message)
        : base(message) { }

    public UnauthorizedException(string action, string resource, object key)
        : base($"Unauthorized to {action} {resource} '{key}'.") { }
}
