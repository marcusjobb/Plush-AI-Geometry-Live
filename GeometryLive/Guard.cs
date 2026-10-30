namespace GeometryLive;

/// <summary>
/// The checks every shape needs, in one place. A side or a radius must be a
/// positive, finite number; nothing else is a shape.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Returns <paramref name="value"/> when it is greater than zero and finite.
    /// </summary>
    /// <param name="value">The measurement to check.</param>
    /// <param name="parameterName">The name to blame when the check fails.</param>
    /// <returns>The same value, so a constructor can assign it directly.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is zero, negative, NaN or infinite.
    /// </exception>
    public static double Positive(double value, string parameterName)
    {
        if (!double.IsFinite(value) || value <= 0)
        {
            throw new ArgumentException(
                $"{parameterName} must be a positive number, got {value}.",
                parameterName);
        }

        return value;
    }
}
