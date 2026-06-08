namespace Mazad.SharedKernel.Common;

public static class Guard
{
    public static void AgainstNullOrEmpty(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"'{parameterName}' cannot be null or empty.", parameterName);
    }

    public static void AgainstNull<T>(T? value, string parameterName) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(parameterName);
    }

    public static void AgainstNegativeOrZero(decimal value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentException($"'{parameterName}' must be greater than zero.", parameterName);
    }

    public static void AgainstNegative(decimal value, string parameterName)
    {
        if (value < 0)
            throw new ArgumentException($"'{parameterName}' must not be negative.", parameterName);
    }

    public static void AgainstOutOfRange(int value, int min, int max, string parameterName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(parameterName,
                $"'{parameterName}' must be between {min} and {max}.");
    }

    public static void AgainstDefault(Guid value, string parameterName)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"'{parameterName}' cannot be an empty GUID.", parameterName);
    }
}
